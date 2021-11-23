class ChickenInstructionInfo:
    __slots__=("opcode", "name", "chicken_name")
    def __init__(self, opcode: int, name: str, chicken_name: str) -> None:
        self.opcode = opcode
        self.name = name
        self.chicken_name = chicken_name

    def __str__(self):
        return str(self.opcode) + '|' + self.chicken_name

CHICKEN_INSTRUCTIONS = [
    ChickenInstructionInfo(0, "exit", "axe"),
    ChickenInstructionInfo(1, "chicken", "chicken"),
    ChickenInstructionInfo(2, "add", "add"),
    ChickenInstructionInfo(3, "subtract", "fox"),
    ChickenInstructionInfo(4, "multiply", "rooster"),
    ChickenInstructionInfo(5, "compare", "compare"),
    ChickenInstructionInfo(6, "load", "pick"),
    ChickenInstructionInfo(7, "store", "peck"),
    ChickenInstructionInfo(8, "jump", "fr"),
    ChickenInstructionInfo(9, "char", "BBQ"),
    ChickenInstructionInfo(10, "push", "wing")
]