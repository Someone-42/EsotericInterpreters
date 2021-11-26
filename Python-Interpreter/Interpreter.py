class CVM: #Like JVM but for chicken
    __slots__ = ("stack", "instruction_index")
    def __init__(self):
        self.stack = [
            None,
            input
        ]
        self.stack[0] = self.stack
        self.instruction_index = 2

    def execute(self, minichicken: list):
        self.instruction_index = len(self.stack) # Points to the beginning of the added instructions
        self.stack += minichicken[:] # Adds the instructions without modifying the list
        self.stack.append(0) # Exit code
        while self.instruction_index < len(self.stack):
            try:
                INSTRUCTIONS[self.stack[self.instruction_index]](self)
                self.instruction_index += 1
            except:
                print("CHICKEN ERROR")

def chicken_exit(chicken: CVM):
    chicken.instruction_index = len(chicken.stack)

def chicken(chicken: CVM):
    chicken.stack.append("chicken")

def add(chicken: CVM):
    chicken.stack.append(chicken.stack.pop() + chicken.stack.pop())

def subtract(chicken: CVM):
    chicken.stack.append(chicken.stack.pop() - chicken.stack.pop())

INSTRUCTIONS = [
    chicken_exit,
    chicken,
    add
]

if __name__ == "__main__": # Tests
    import Parser as p
    import InstructionInfoTables as ii

    try:
        pass
    except Exception as e:
        print("/!\\ at least one test failed :", e)

    instructions = p.string_to_instruction_list("""
    chicken
    chicken
    add
    exit
    """)
    mini_chicken = p.instructions_to_minichicken(instructions, ii.CHICKEN_INSTRUCTIONS)
    print(mini_chicken)
    vm = CVM()
    vm.execute(mini_chicken)
    print(vm.stack[-1])