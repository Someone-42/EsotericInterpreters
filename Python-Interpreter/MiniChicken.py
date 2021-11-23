
def chicken_to_minichicken(chicken: str) -> list:
    return [len(line.split(' ')) for line in chicken.split('\n')]

def minichicken_to_chicken(minichicken: list) -> str:
    return '\n'.join([" ".join(["chicken"] * i) for i in minichicken])

if __name__=="__main__":
    # Testing stuff
    chicken_code = "chicken chicken chicken\nchicken chicken\nchicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken"
    print(chicken_code)
    try:
        mini_chiken = chicken_to_minichicken(chicken_code)
        assert mini_chiken == [3, 2, 13]
        print("test 1 passed")
        assert minichicken_to_chicken(mini_chiken) == chicken_code
        print("test 2 passed")
    except:
        print ("/!\ at least one test failed")