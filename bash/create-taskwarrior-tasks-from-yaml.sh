

# Requires: taskwarrior


add_task() {
    local name="$1"
    local child_ids="$2"
    local id
    if [ -n "$child_ids" ]; then
        id=$(task add "$name" depends:$child_ids | grep -oE 'Created task ([0-9]+)' | awk '{print $3}')
    else
        id=$(task add "$name" | grep -oE 'Created task ([0-9]+)' | awk '{print $3}')
    fi
    echo "$id"
}


create_taskwarrior_tasks_from_yaml() {
    local YAML_FILE="${1:-tasks.yaml}"
    local -a task_names
    local -a indent_levels
    local -a task_ids
    local -a child_map
    local line indent task_name i

    # Read all lines and store their indent and name
    while IFS= read -r line || [ -n "$line" ]; do
        [[ -z "$line" ]] && continue
        indent=$(echo "$line" | sed -E 's/^([ ]*).*/\1/' | wc -c)
        indent=$((indent - 1))
        task_name=$(echo "$line" | sed -E 's/^\s*task:\s*//;s/^\s*//;s/\s*$//')
        task_names+=("$task_name")
        indent_levels+=("$indent")
    done < "$YAML_FILE"

    # Build child map: for each task, collect only immediate children (next deeper indentation)
    for ((i=0; i<${#task_names[@]}; i++)); do
        child_map[$i]=""
        local this_indent=${indent_levels[$i]}
        local children=""
        for ((j=i+1; j<${#task_names[@]}; j++)); do
            if [ ${indent_levels[$j]} -eq $((this_indent + 2)) ]; then
                children+="$j "
            elif [ ${indent_levels[$j]} -le $this_indent ]; then
                break
            fi
        done
        child_map[$i]="$children"
    done

    # Create tasks in reverse order so children are created before parents
    for ((i=${#task_names[@]}-1; i>=0; i--)); do
        local child_ids=""
        for child_idx in ${child_map[$i]}; do
            child_ids+="${task_ids[$child_idx]},"
        done
        child_ids=${child_ids%,} # Remove trailing comma
        task_ids[$i]=$(add_task "${task_names[$i]}" "$child_ids")
    done
    echo "Tasks added with dependencies."
}