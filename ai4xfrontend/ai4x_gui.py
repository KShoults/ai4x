"""Control ai4x through a graphical user interface."""
#import curses

#import ai4x.frontend.frontend_interface
#from ai4xfrontend.frontendutils import menu

def ai4xgui() -> None:
    """Send a command to ai4x either through the CLI or GUI."""
#     stdscr = curses.initscr()
#     curses.noecho()
#     curses.cbreak()
#     curses.curs_set(0)
#     stdscr.keypad(True)
#     try:
#         command: str = ''
#         show_main_menu(stdscr)
#         while command != 'q':
# #            handleCommand(command, stdscr)
#             command = stdscr.getkey()
#             stdscr.clear()
#     finally:
#         curses.nocbreak()
#         stdscr.keypad(False)
#         curses.echo()
#         curses.curs_set(1)
#         curses.endwin()


#def handleCommand(command, stdscr):


# def show_main_menu(stdscr):
#     """Show the main menu.

#     This is the default when starting the GUI. Gives GUI access to the
#     new game, load game, and quit game functions.
#     """
#     # saves =
#     # for i, s in enumerate(saves[:]):
#     #     saves[i] = s[8:-5]
#     # return showMenu('Select a save', saves, stdscr)
#     options = [
#         "New Game",
#         "Load Game"
#     ]
#     selection = menu.showMenu("AI 4X", options,stdscr)
#     if selection == "New Game":
#         stdscr.addstr("Enter Save Name:" + "\n")
#         #save_name = stdscr.getstr()
