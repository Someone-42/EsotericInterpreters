import InstructionInfoTables as iit

class CVM: #Like JVM but for chicken
    __slots__ = ("stack", "instruction_index", "instruction_set")
    def __init__(self, instruction_set):
        self.instruction_set = instruction_set
        self.stack = [
            None,
            ""
        ]
        self.stack[0] = self.stack
        self.instruction_index = 2

    def pop(self):
        """Pops the value at the end of the stack and returns it"""
        return self.stack.pop()

    def get_next_instruction(self):
        """Returns the next instruction, and increments the instruction pointer"""
        self.instruction_index += 1
        return self.stack[self.instruction_index]

    def get_at(self, index):
        """Returns the element at `index` in the stack"""
        return self.stack[index]

    def append(self, element):
        self.stack.append(element)

    def exit(self):
        self.instruction_index = len(self.stack)

    def execute(self, minichicken: list):
        """Executes the given Chicken code"""
        debug = True
        self.instruction_index = len(self.stack) - 1 # Points to the beginning of the added instructions
        self.stack += minichicken[:] # Adds the instructions without modifying the list
        self.stack.append(0)
        while self.instruction_index < len(self.stack):
            try:
                instruction = self.get_next_instruction()
                if instruction >= 10:
                    self.instruction_set[10](self, instruction - 10)
                else:
                    self.instruction_set[instruction](self)
            except Exception as e:
                print("CHICKEN ERROR", e)