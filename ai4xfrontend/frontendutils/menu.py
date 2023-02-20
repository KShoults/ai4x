import curses

def showMenu(header, options, stdscr):
    selectedIndex = 0
    while True:
        stdscr.addstr(header + '\n')
        for i, s in enumerate(options):
            if i == selectedIndex:
                stdscr.addstr(str(i+1) + ': ' + s + '\n', curses.A_STANDOUT)
            else:
                stdscr.addstr(str(i+1) + ': ' + s + '\n')
        command = stdscr.getkey()
        stdscr.clear()
        if command == 'KEY_UP':
            selectedIndex -= 1
        elif command == 'KEY_DOWN':
            selectedIndex += 1
        elif command == '\n':
            return options[selectedIndex]
        elif command == 'q':
            return False
        elif isValidInt(command, len(options)-1):
            return options[int(command)-1]
        if selectedIndex < 0:
            selectedIndex = 0
        if selectedIndex >= len(options):
            selectedIndex = len(options)-1

def isValidInt(s, max):
    try:
        i = int(s)
        if 1 <= i <= max:
            return True
        else:
            return False
    except ValueError:
        return False
            