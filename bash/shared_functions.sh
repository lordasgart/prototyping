autopush() {
	git stage .
	git commit -m auto
	git pull --rebase
	git push
}
pushall() {
	local ORIGINAL_DIR
        ORIGINAL_DIR="$(pwd)"  # Save the current directory
	cd ~/Projects/prototyping
	autopush
	cd ~/Projects/lordasgart
	autopush
	cd ~/Projects/openscad
	autopush
	cd "$ORIGINAL_DIR" || return  # Restore original directory
}
# PS1='[\u@\h \W]\$ '

note1() {
    local task_number=$1
    if [ -z "$task_number" ]; then
        echo "Usage: note <task_number>"
        return 1
    fi

    local md_file
    md_file=$(pwsh -Command "& { . /home/lordasgart/Projects/prototyping/get-task-uuid.ps1; note '$task_number' }")

    if [ -n "$md_file" ]; then
        vim "$md_file"
    else
        echo "No markdown file returned from PowerShell."
        return 2
    fi
}

catnote() {
    local task_number=$1
    if [ -z "$task_number" ]; then
        echo "Usage: catnote <task_number>"
        return 1
    fi

    # Get task info and extract UUID using grep and sed
    local uuid=$(task "$task_number" info | grep -oP 'UUID\s+\K[a-f0-9-]+')

    if [ -n "$uuid" ]; then
        local home_dir="$HOME"
        local task_dir="$home_dir/.task"
        local markdown_file="$task_dir/$uuid.md"

        # Create directory if it doesn't exist
        [ -d "$task_dir" ] || mkdir -p "$task_dir"

        # Create file if it doesn't exist
        [ -f "$markdown_file" ] || touch "$markdown_file"

        # Open the markdown file
        cat "$markdown_file"
    else
        echo "UUID not found for task number $task_number"
        return 2
    fi
}