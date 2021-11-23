import ChickenInstructionTable as t

def chicken_to_minichicken(chicken: str) -> list:
    return [len(line.split(' ')) for line in chicken.split('\n')]

def minichicken_to_chicken(minichicken: list) -> str:
    return '\n'.join([" ".join(["chicken"] * i) for i in minichicken])

def minichicken_to_instructions(minichicken: list) -> list:
    return [(t.CHICKEN_INSTRUCTIONS[i].chicken_name if i < 10 else (t.CHICKEN_INSTRUCTIONS[10].chicken_name + ' ' + str(i - 10))) for i in mini_chicken]

def instructions_to_minichicken(minichicken: list) -> str:
    return '\n'.join([" ".join(["chicken"] * i) for i in minichicken])

if __name__=="__main__":
    # Testing stuff
    chicken_code = "chicken chicken chicken\nchicken chicken\nchicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken"
    print(chicken_code)
    try:
        mini_chicken = chicken_to_minichicken(chicken_code)
        assert mini_chicken == [3, 2, 13]
        print("test 1 passed")
        assert minichicken_to_chicken(mini_chicken) == chicken_code
        print("test 2 passed")
        for c in minichicken_to_instructions(mini_chicken):
            print(c)
        print("test 3 passed")
    except:
        print ("/!\ at least one test failed")