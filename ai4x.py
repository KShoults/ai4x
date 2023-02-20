"""A standalone script for utilizing the ai4x_frontend package.

Possible CLI arguments:
new -- create a new save file (expects the save name next)
list -- list the existing saves
end -- end the current turn (expects the save name next)
"""

import sys

from ai4xfrontend import ai4x_cli
from ai4xfrontend import ai4x_gui


if __name__ == "__main__":
    if len(sys.argv) > 1:
        ai4x_cli.start(sys.argv[1:])
    else:
        ai4x_gui.start()
