firework() {
  local colors=(31 33 32 36 34 35)  # Rot, Gelb, Grün, Cyan, Blau, Magenta
  local -a frames=(
    "    .     "
    "   ...    "
    "  .....   "
    " .......  "
    "  .....   "
    "   ...    "
    "    .     "
  )

  for i in {1..5}; do
    for frame in "${frames[@]}"; do
      clear
      local color=${colors[$((RANDOM % ${#colors[@]}))]}
      echo -e "\e[${color}m$frame\e[0m"
      sleep 0.1
    done
  done
  clear
  echo -e "\e[32m✔ Befehl erfolgreich abgeschlossen! 🎉\e[0m"
}

# Beispiel: Nach einem erfolgreichen Befehl
if command; then
  firework
fi

