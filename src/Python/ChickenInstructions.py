from ChickenVM import CVM

def chicken_exit(chicken: CVM):
    chicken.instruction_index = len(chicken.stack)

def chicken(chicken: CVM):
    chicken.append("chicken")

def add(chicken: CVM):
    roperand = chicken.pop()
    loperand = chicken.pop()
    chicken.append(loperand + roperand)

def subtract(chicken: CVM):
    roperand = chicken.pop()
    loperand = chicken.pop()
    chicken.append(loperand - roperand)

def multiply(chicken: CVM):
    roperand = chicken.pop()
    loperand = chicken.pop()
    chicken.append(loperand * roperand)

def compare(chicken: CVM):
    chicken.append(chicken.pop() == chicken.pop())

def load(chicken: CVM):
    if chicken.get_next_instruction() == 1:
        chicken.append(input("Chicken (user input required) :"))
    else:
        try:
            chicken.append(chicken.stack[chicken.pop()])
        except:
            chicken.append("")

def store(chicken: CVM):
    index = chicken.pop()
    value = chicken.pop()
    chicken.stack[index] = value

def jump(chicken: CVM):
    i = chicken.pop()
    if chicken.pop():
        chicken.instruction_index += i

def char(chicken: CVM):
    chicken.append(chr(chicken.pop()))

def push(chicken: CVM, n: int):
    chicken.append(n)

INSTRUCTIONS = [ # Basic chicken instructions set
    chicken_exit,
    chicken,
    add,
    subtract,
    multiply,
    compare,
    load,
    store,
    jump,
    char,
    push
]
