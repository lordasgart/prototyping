# create finish method to set a passed task number to done
finish() {
  if [ -z "$1" ]; then
    echo "Bitte geben Sie eine Task-Nummer an."
    return 1
  fi
  task "$1" done
  if [ $? -eq 0 ]; then
    confetti
    echo -e "\e[32m✔ Aufgabe $1 als erledigt markiert!\e[0m"
  else
    echo -e "\e[31m✘ Fehler beim Markieren der Aufgabe $1 als erledigt.\e[0m"
  fi
}