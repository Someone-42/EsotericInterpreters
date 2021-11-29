import Parser as p
import InstructionInfoTables as ii
from ChickenVM import CVM
import ChickenInstructions as ins

def hello_world():
    f = open("ChickenPrograms/chicken_helloworld.ck", "r") # You may need to change the path here, as VSC sets the current working directory as root folder
    
    mini_chicken = p.chicken_to_minichicken(f.read())
    vm = CVM(ins.INSTRUCTIONS)
    vm.execute(mini_chicken)
    return vm.stack[-1]

def compare_isgreater(a, b):
    f = open("ChickenPrograms/is_a_greater_than_b.cki", "r") # You may need to change the path here, as VSC sets the current working directory as root folder
    
    mini_chicken = [
        10 + a,
        10 + b,
    ] + p.instructions_to_minichicken(p.string_to_instruction_list(f.read()), ii.CHICKEN_INSTRUCTIONS)

    vm = CVM(ins.INSTRUCTIONS)
    vm.execute(mini_chicken)
    
    return vm.stack[-1]
    
def chickens99(n):
    f = open("ChickenPrograms/99_chickens.ck", "r") # You may need to change the path here, as VSC sets the current working directory as root folder
    
    mini_chicken = p.chicken_to_minichicken(f.read())

    vm = CVM(ins.INSTRUCTIONS)
    vm.execute(mini_chicken, n)

    return vm.stack[-1]
    
if __name__ == "__main__": # Tests
    print(chickens99(9))
    exit(0)
    try:
        print("Starting Tests :")
        assert hello_world() == "Hello world"
        print("Test 1 passed - Hello world program")
        assert compare_isgreater(42, 54) == 0
        print("Test 2 passed")
        assert compare_isgreater((2 ** 31) - 4, 1) == 1
        print("Test 3 passed")
        assert chickens99()
        print("Test 4 passed")
        print("All Tests passed !")
    except Exception as e:
        print("/!\\ at least one test failed :", e)
