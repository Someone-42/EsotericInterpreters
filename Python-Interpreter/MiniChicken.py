import ChickenInstructionTable as t

def chicken_to_minichicken(chicken: str) -> list:
    return [len(line.split(' ')) for line in chicken.split('\n')]

def minichicken_to_chicken(minichicken: list) -> str:
    return '\n'.join([" ".join(["chicken"] * i) for i in minichicken])

def minichicken_to_instructions(minichicken: list, instruction_infos) -> list:
    return [(instruction_infos[i].name if i < 10 else (instruction_infos[10].name + ' ' + str(i - 10))) for i in mini_chicken]

def instructions_to_minichicken(instructions: list, instrcution_infos) -> str:
    d = {}
    for instruction in instrcution_infos:
        d[instruction.name] = instruction.opcode
    l = []
    for instruction in instructions:
        arr = instruction.split(' ')
        arg = 0 if len(arr) < 2 else int(arr[1])
        l.append(d[arr[0]] + arg)
    return l

if __name__=="__main__":
    # Testing stuff
    chicken_code = "chicken chicken chicken\nchicken chicken\nchicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken"
    try:
        mini_chicken = chicken_to_minichicken(chicken_code)
        assert mini_chicken == [3, 2, 13]
        print("test 1 passed")
        assert minichicken_to_chicken(mini_chicken) == chicken_code
        print("test 2 passed")
        instrs = minichicken_to_instructions(mini_chicken, t.CHICKEN_INSTRUCTIONS)
        assert instrs == ["subtract", "add", "push 3"]
        print("test 3 passed")
        assert instructions_to_minichicken(instrs, t.CHICKEN_INSTRUCTIONS) == mini_chicken
        print("test 4 passed")
    except Exception as e:
        print ("/!\ at least one test failed:", e)