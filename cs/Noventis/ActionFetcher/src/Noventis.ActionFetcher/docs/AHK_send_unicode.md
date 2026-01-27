# AHK send unicode and mouse wheel

To send Unicode characters like **ä, ö, ü, ß** in AutoHotkey (AHK), you can use the `Send` command with the `{U+xxxx}` syntax, where `xxxx` is the Unicode hexadecimal code for the character.

Here are the Unicode codes for these characters:
- ä = U+00E4
- ö = U+00F6
- ü = U+00FC
- ß = U+00DF

Example AHK script to send these characters:

```ahk
; Sends ä, ö, ü, ß using Unicode codes
Send, {U+00E4}{U+00F6}{U+00FC}{U+00DF}
```

Alternatively, you can send them directly if your script file is saved with UTF-8 encoding and the target application supports it:

```ahk
; Sends ä, ö, ü, ß directly
Send, äöüß
```

**Tips:**
- Make sure your AHK script is saved in UTF-8 encoding without BOM if you use direct characters.
- The `{U+xxxx}` method works even if the script encoding is not UTF-8, as it sends Unicode via hex codes.
- Some programs may require different input methods for Unicode characters, but `{U+xxxx}` works in most cases.

If you want a quick hotkey example:

```ahk
^!a::Send, {U+00E4} ; Ctrl+Alt+a sends ä
^!o::Send, {U+00F6} ; Ctrl+Alt+o sends ö
^!u::Send, {U+00FC} ; Ctrl+Alt+u sends ü
^!s::Send, {U+00DF} ; Ctrl+Alt+s sends ß
```

This way, pressing Ctrl+Alt and the corresponding letter sends the special character. Let me know if you want help with a specific use case!

In AutoHotkey (AHK), you can simulate mouse wheel scrolling up and down using the built-in commands for the mouse wheel: `WheelUp` and `WheelDown`. These can be used in scripts to send scroll events or to trigger actions when the wheel is scrolled.

### To simulate scrolling up or down:

```ahk
; Scroll up (one notch)
Send, {WheelUp}

; Scroll down (one notch)
Send, {WheelDown}
```

### Example: Bind keys to scroll up and down

```ahk
; Press Ctrl+Up to scroll up
^Up::Send, {WheelUp}

; Press Ctrl+Down to scroll down
^Down::Send, {WheelDown}
```

### Example: Scroll multiple notches

If you want to scroll multiple notches at once, repeat the command or use a loop:

```ahk
; Scroll up 3 notches
Loop, 3
{
    Send, {WheelUp}
}

; Scroll down 5 notches
Loop, 5
{
    Send, {WheelDown}
}
```

### Detect mouse wheel scrolling (to trigger actions)

You can also create hotkeys that react when you physically scroll the mouse wheel:

```ahk
WheelUp::MsgBox, You scrolled up!
WheelDown::MsgBox, You scrolled down!
```

---

**Summary:**

- Use `{WheelUp}` to scroll up.
- Use `{WheelDown}` to scroll down.
- Repeat or loop to scroll multiple steps.
- Bind to keys or mouse wheel events to customize behavior.

If you want to integrate this with other input or automate scrolling in a specific application, I can help tailor the script further!