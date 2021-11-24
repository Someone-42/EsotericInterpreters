class ChickenInstructionInfo:
    __slots__=("opcode", "name")
    def __init__(self, opcode: int, name: str) -> None:
        self.opcode = opcode
        self.name = name

    def __str__(self):
        return str(self.opcode) + '|' + self.name

CHICKEN_INSTRUCTIONS = [
    ChickenInstructionInfo(0, "exit"),
    ChickenInstructionInfo(1, "chicken"),
    ChickenInstructionInfo(2, "add"),
    ChickenInstructionInfo(3, "subtract"),
    ChickenInstructionInfo(4, "multiply"),
    ChickenInstructionInfo(5, "compare"),
    ChickenInstructionInfo(6, "load"),
    ChickenInstructionInfo(7, "store"),
    ChickenInstructionInfo(8, "jump"),
    ChickenInstructionInfo(9, "char"),
    ChickenInstructionInfo(10, "push")
]
CHICKEN_CHICKEN_INSTRUCTIONS = [
    ChickenInstructionInfo(0, "axe"),
    ChickenInstructionInfo(1, "chicken"),
    ChickenInstructionInfo(2, "add"),
    ChickenInstructionInfo(3, "fox"),
    ChickenInstructionInfo(4, "rooster"),
    ChickenInstructionInfo(5, "compare"),
    ChickenInstructionInfo(6, "pick"),
    ChickenInstructionInfo(7, "peck"),
    ChickenInstructionInfo(8, "fr"),
    ChickenInstructionInfo(9, "BBQ"),
    ChickenInstructionInfo(10, "wing")
]