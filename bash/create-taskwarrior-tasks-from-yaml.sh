#!/usr/bin/env bash


# Requires: yq (https://github.com/mikefarah/yq), taskwarrior

# Accept YAML file path as first argument, default to 'tasks.yaml' if not provided
YAML_FILE="${1:-tasks.yaml}"

declare -A TASK_IDS

add_task() {
    local name="$1"
    local parent_id="$2"
    local id

    # Add the task, get its ID
    if [ -n "$parent_id" ]; then
        id=$(task add "$name" depends:$parent_id | grep -oE 'Created task ([0-9]+)' | awk '{print $3}')
    else
        id=$(task add "$name" | grep -oE 'Created task ([0-9]+)' | awk '{print $3}')
    fi
    echo "$id"
}

parse_yaml() {
    local parent_id="$1"
    local prefix="$2"
    local keys
    keys=$(yq e "$prefix | keys" "$YAML_FILE" | sed 's/- //g')

    for key in $keys; do
        local task_name
        task_name=$(yq e "$prefix.$key.task" "$YAML_FILE")
        if [ "$task_name" == "null" ]; then
            task_name="$key"
        fi

        local id
        id=$(add_task "$task_name" "$parent_id")
        TASK_IDS["$prefix.$key"]=$id

        # Check for subtasks
        local subkeys
        subkeys=$(yq e "$prefix.$key | keys" "$YAML_FILE" | sed 's/- //g')
        if [ -n "$subkeys" ] && [ "$subkeys" != "task" ]; then
            parse_yaml "$id" "$prefix.$key"
        fi
    done
}

# Start parsing from the root
parse_yaml "" "."

echo "Tasks added with dependencies."