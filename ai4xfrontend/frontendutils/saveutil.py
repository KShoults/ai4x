"""Provides commands for saving and loading the frontend data.

These functions first use the save directory set by the setsavedir function.
Otherwise they fall back to current_working_directory/Saves. The default save
location is determined upon import or upon subsequent calls to setsavedir.

functions:
listsaves -- List the saves in the saves directory.
load -- Load the frontend state from the saves directory.
save -- Save the frontend state to the saves directory.
setsavedir -- Sets the current save directory.
"""

import os
import dill

from ai4xfrontend.frontendutils.frontendstate import FrontendState

ENGINE_SAVE_EXTENSION: str = ".esav"
FRONTEND_SAVE_LOCATION: str = ".fsav"

_savedirectory: str = os.path.join(os.getcwd(), "Saves")


def listsaves() -> list[str]:
    """List the saves in the saves directory.

    Important: This actually checks for engine saves as opposed to frontend
    saves. This is because an engine save is required to run the game whereas
    a frontend save is not necessary. The caller will likely want to also call
    frontendmarshaller's version of the same method.

    return a list[str] representing the found saves.
    """
    _checksavedirectory()

    entries: list[str] = os.listdir(_savedirectory)
    savenames: list[str] = []
    for ent in entries:
        if ent.endswith(ENGINE_SAVE_EXTENSION):
            savenames.append(ent)

    return savenames

def loadsave(savename: str) -> FrontendState:
    """Load the frontene state from the save directory.

    Arguments:
    savename: str -- The name of the save to load.

    returns an instance of frontendutils.frontendstate.FrontendState
    """
    _checksavedirectory()

    with open(os.path.join(_savedirectory, savename), mode="rb") as file:
        dump: bytes = file.read()
    state: FrontendState = dill.loads(dump)
    return state

def savegame(state: FrontendState) -> int:
    """Save the given frontend state to the save directory.

    Arguments:
    state: FrontendState -- an instance of
        frontendutils.frontendstate.FrontendState to be saved.
    """
    _checksavedirectory()

    dump: bytes = dill.dumps(state)
    with open(os.path.join(_savedirectory, state.savename), mode="wb") as file:
        file.write(dump)
    return 0

def setsavedir(newdirectory: str | None) -> None:
    """Set the current save directory.

    If savedirectory is not given it resets the saved directory to the
    current_working_directory/Saves. If the directory does not exist it
    will be created given the parent directory exists.

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
