import Parser as p
import InstructionInfoTables as ii
from ChickenVM import CVM
import ChickenInstructions as ins

def hello_world():
    f = open("ChickenPrograms/chicken_helloworld.ck", "r") # You may need to change the path here, as VSC sets the current working directory as root folder
    
    mini_chicken = p.chicken_to_minichicken(f.read())
    vm = CVM(ins.INSTRUCTIONS)
    vm.execute(mini_chicken)
    print(vm.stack[-1])

if __name__ == "__main__": # Tests

    try:
        print("Starting Tests :")
        hello_world()
        print("Test 1 passed - Hello world program")
        print("All Tests passed !")
    except Exception as e:
        print("/!\\ at least one test failed :", e)
