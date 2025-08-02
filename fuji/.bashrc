#
# ~/.bashrc
#
export XDG_DATA_DIRS="$XDG_DATA_DIRS:/home/lordasgart/Nextcloud/Documents/Linux/applications"
export EDITOR=vim
export VISUAL=vim


alias openscad="export GTK_THEME=Adwaita:dark; export GTK2_RC_FILES=/usr/share/themes/Adwaita-dark/gtk-2.0/gtkrc; export QT_STYLE_OVERRIDE=Adwaita-Dark; openscad"
alias c="dotnet /home/lordasgart/Projects/choose/src/Choose.ConsoleApp/bin/Debug/net9.0/Choose.ConsoleApp.dll"
alias bashrc="vim ~/.bashrc"
alias profile="vim ~/.bashrc"
alias musicv1="cava"
alias musicv2="muviz"
alias record="pw-record --target 69 firefox.wav" # pw-top for number
alias scad="cd /home/lordasgart/Projects/openscad/"
alias home="cd ~"
alias cls="clear"
alias push="git push"
alias status="git status"
alias lama="ollama run tinyllama"
alias desktop="/opt/visual-studio-code/code /home/lordasgart/.local/share/applications/applications.code-workspace"
alias applications="/opt/visual-studio-code/code /home/lordasgart/.local/share/applications/applications.code-workspace"
alias imw="remmina -c /home/lordasgart/.local/share/remmina/group_rdp_192-168-178-25-80_192-168-178-25.remmina"
alias tui="taskwarrior-tui"
alias t="task"
alias n="note"
alias codebashrc="/opt/visual-studio-code/code ~/.bashrc"
alias cbashrc="/opt/visual-studio-code/code ~/.bashrc"
alias prototyping="/opt/visual-studio-code/code ~/Projects/prototyping/prototyping.code-workspace"

# If not running interactively, don't do anything
[[ $- != *i* ]] && return

alias ls='ls --color=auto'
alias grep='grep --color=auto'
PS1='[\u@\h \W]\$ '

tasks() {
    task list
}

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

note() {
    local task_number=$1
    if [ -z "$task_number" ]; then
        echo "Usage: note <task_number>"
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
        vim "$markdown_file"
    else
        echo "UUID not found for task number $task_number"
        return 2
    fi
}

depends() {
	local task_number=$1
	local depends_on_tasknumber=$2
	task $task_number modify depends:$depends_on_tasknumber
}

tuikeys() {
  cat <<EOF
uda.taskwarrior-tui.keyconfig.quit=q
uda.taskwarrior-tui.keyconfig.refresh=r
uda.taskwarrior-tui.keyconfig.go-to-bottom=G
uda.taskwarrior-tui.keyconfig.go-to-top=g
uda.taskwarrior-tui.keyconfig.down=j
uda.taskwarrior-tui.keyconfig.up=k
uda.taskwarrior-tui.keyconfig.page-down=J
uda.taskwarrior-tui.keyconfig.page-up=K
uda.taskwarrior-tui.keyconfig.delete=x
uda.taskwarrior-tui.keyconfig.done=d
uda.taskwarrior-tui.keyconfig.start-stop=s
uda.taskwarrior-tui.keyconfig.quick-tag=t
uda.taskwarrior-tui.keyconfig.undo=u
uda.taskwarrior-tui.keyconfig.edit=e
uda.taskwarrior-tui.keyconfig.modify=m
uda.taskwarrior-tui.keyconfig.shell=!
uda.taskwarrior-tui.keyconfig.log=l
uda.taskwarrior-tui.keyconfig.add=a
uda.taskwarrior-tui.keyconfig.annotate=A
uda.taskwarrior-tui.keyconfig.filter=/
uda.taskwarrior-tui.keyconfig.zoom=z
uda.taskwarrior-tui.keyconfig.context-menu=c
uda.taskwarrior-tui.keyconfig.next-tab=]
uda.taskwarrior-tui.keyconfig.previous-tab=[
EOF
}

