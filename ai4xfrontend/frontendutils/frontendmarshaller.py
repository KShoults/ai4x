"""Send commands to ai4x's engine.

Functions:
newGame
listSaves
loadGame
endTurn
saveExit
"""

import json
import os
import sys

import clr

with open(os.path.join(".vscode", "settings.json"),
          encoding="utf-8") as _settingsfile:
    assembly_path: str = os.path.expanduser(
        json.load(_settingsfile)["publishOutputDirectory"])


sys.path.append(assembly_path)
clr.AddReference("AI4XEngine")

# pylint: disable-next=wrong-import-position
from EngineInterface import EngineInt # pyright: ignore[reportMissingImports]

_savedirectory: str = os.path.join(os.getcwd(), "Saves")

def newgame(save_name: str) -> int:
    """Command the engine to create a new save called saveName.

    First argument is the name that the new save should have.

    Returns an error code.
    """
    _checksavedirectory()

    return EngineInt.NewGame(os.path.join(_savedirectory, save_name))


def endturn(save_name: str) -> int:
    """Command the engine to advance a save by one turn.

    First argument is the name of the save to advance.

    Returns an error code.
    """
    _checksavedirectory()

    return EngineInt.EndTurn(os.path.join(_savedirectory, save_name), [])

def setsavedir(newdirectory: str | None) -> None:
    """Set the current save directory.

    If savedirectory is not given it resets the saved directory to the
    current_working_directory/Saves. If the directory does not exist it
    will be created given the parent directory exists. The caller will
    likely want to also call saveutil's version of the same method.

    Optional Arguments:
    savedirectory: str -- The new save directory.
    """
    if newdirectory is None:
        localsavedirectory: str = os.path.join(os.getcwd(), "Saves")
        if not os.path.exists(localsavedirectory):
            os.mkdir(localsavedirectory)
        _savedirectory = localsavedirectory
    else:
        if os.path.exists(newdirectory):
            _savedirectory = newdirectory
        else:
            try:
                os.mkdir(newdirectory)
            except FileNotFoundError as exc:
                print("Error: Attempted to create save directory where parent"
                + " directory does not exist.")
                raise exc



def _checksavedirectory() -> None:
    """Non-Public function to create the save directory if it doesn't exist."""
    if not os.path.exists(_savedirectory):
        os.mkdir(_savedirectory)
