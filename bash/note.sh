note() {
    local task_number=$1
    if [ -z "$task_number" ]; then
        echo "Usage: note <task_number>"
        return 1
    fi

    # Get task info and extract UUID using grep and sed
    local task_info=$(task "$task_number" info)
    local uuid=$(echo "$task_info" | grep -oP 'UUID\s+\K[a-f0-9-]+')
    local description=$(echo "$task_info" | grep -oP 'Description\s+\K.+$')

    if [ -n "$uuid" ]; then
        local home_dir="$HOME"
        local task_dir="$home_dir/.task"
        local markdown_file="$task_dir/$uuid.md"

        # Create directory if it doesn't exist
        [ -d "$task_dir" ] || mkdir -p "$task_dir"

        # Create file if it doesn't exist and add header
        if [ ! -f "$markdown_file" ]; then
            echo "# $description" > "$markdown_file"
            echo "" >> "$markdown_file"
        fi

        # Open the markdown file
        vim "$markdown_file"
    else
        echo "UUID not found for task number $task_number"
        return 2
    fi
}