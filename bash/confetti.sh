confetti() {
  local width=$(tput cols)
  local height=$(tput lines)
  local symbols=('*' '+' 'x' 'o' '@')
  local colors=(31 32 33 34 35 36)

  for i in {1..20}; do
    clear
    for ((j=0; j<height; j++)); do
      line=""
      for ((k=0; k<width; k++)); do
        if (( RANDOM % 20 == 0 )); then
          local color=${colors[$RANDOM % ${#colors[@]}]}
          local symbol=${symbols[$RANDOM % ${#symbols[@]}]}
          line+="\e[${color}m${symbol}\e[0m"
        else
          line+=" "
        fi
      done
      echo -e "$line"
    done
    sleep 0.1
  done
  clear
  echo -e "\e[32m✔ Fertig! 🎊\e[0m"
}

# Beispiel:
if command; then
  confetti
fi

