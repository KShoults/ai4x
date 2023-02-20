"""Access the commands required to play ai4x through a command line interface.

functions:
start -- Processes a single ai4x command.
"""

from ai4xfrontend.frontendutils import frontendmarshaller
from ai4xfrontend.frontendutils import saveutil


def start(args) -> None:
    """Send a command to ai4x.

    arguments:
    args -- A str[] of CLI arguments.

    Prints its results.
    """
    error_code: int
    if args[0] == "new":
        if len(args) < 2:
            print("New Game expects the save name next.")
        else:
            error_code = frontendmarshaller.newgame(args[1])
            if  error_code == 0:
                print(f"New save with name {args[1]} created.")
            else:
                print(f"New save creation failed with error code {error_code}.")

    elif args[0] == "list":
        saves: list[str] = saveutil.listsaves()
        if len(saves) > 0:
            for sav in saves:
                print(sav)
        else:
            print("No saves found.")

    elif args[0] == "end":
        if len(args) < 2:
            print("End Turn expects the save name next.")
        else:
            error_code = frontendmarshaller.endturn(args[1])
            if error_code == 0:
                print (f"Save {args[1]} advanced to the next turn.")
            else:
                print (f"End Turn failed with error code {error_code}.")
