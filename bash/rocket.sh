rocket() {
  local rocket=(
    "   ^   "
    "  / \  "
    " |---| "
    " |   | "
    " |---| "
    "  / \  "
    "  | |  "
    " /   \ "
  )

  for i in {1..10}; do
    clear
    for ((j=0; j<i; j++)); do
      echo
    done
    for line in "${rocket[@]}"; do
      echo -e "\e[33m$line\e[0m"
    done
    sleep 0.2
  done
  clear
  echo -e "\e[32m✔ Rakete gestartet! 🚀\e[0m"
}

# Beispiel:
#if command; then
#  rocket
#fi
