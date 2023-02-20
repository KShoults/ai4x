"""Package containing only the FrontendState class and supporting code.

Classes:
FrontendState
"""

class FrontendState:
    """This class contains everything needed to define the frontend.

    This class is serializeable and is to be used as the frontend save object.

    Instance variables:
    savename: str -- The name to be appended with an extension upon saving.
    """

    savename: str = ""
