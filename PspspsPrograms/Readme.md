# Pspsps language guide

Pspspsp is an esoteric language made for fun.

The language runs on a VM, with a base memory of 1024 ints.
It has an instruction pointer pointing an instruction on the Code (instruction stack)
the code is a class containing : An array of arguments, and instructions the same length, each instruction is a byte, and an argument is an int
	It contains an InstructionSet key for versionning,
	And it also has an arry of Labels, basically each label is an integer index (defined in parsing) 
		and this index points to the instruction address of that label definition in the code.

For the following section : Stack => Memory of the program.

The base instruction set for Pspsps is made of 15 instructions :
	// Note : Any instruction using values from the top of the memory stack will remove them :
	//     This applies to : eql, cmp, add, sub, mul, div, jmp, abs, wrt
	//	   As cpy and gto do not use values from the top of the stack, they do not remove them.
FullNameInstruction	/ instruct.	/ Pspsps Name	| Argument (int) ? | Description 
1.  Push			/ psh		/ ps			| O					 // Pushes the arg integer on the stack
2.  Pop				/ pop		/ psp			| X					 // Removes the element on top of the stack
3.  Equality		/ eql		/ psps			| X					 // Compares 2 top stack elements -> 1 if equals, 0 else
4.  Compare			/ cmp		/ sp			| X					 // Compares 2 top stack elements (a, b) -> 0 if a == b, 1 if b > a, -1 if b < a
5.  Add				/ add		/ p				| X					 // Adds 2 top stack elements		(Pushes result)
6.  Subtract		/ sub		/ s				| X					 // Subtracts 2 top stack elements	(Pushes result) (a - b)
7.  Multiply		/ mul		/ pp			| X					 // Multiplies 2 top stack elements	(Pushes result)
8.  Divide			/ div		/ ss			| X					 // Divides 2 top stack elements	(Pushes result) (a / b)
9.  Jump			/ jmp		/ ppp			| X					 // (a, b) 2 top stack elements : jumps of offset a if b is truthy (b != 0) -> Adds the offset to the instruction pointer + 1
10. Label			/ lbl		/ pspsps		| O					 // Defines a label the program can go to (goto instruction)
11. Goto			/ gto		/ spspsp		| O					 // Goes to the label in argument -> Changes instruction pointer to the label address + 1
12. Absolute		/ abs		/ sps			| X					 // Pushes the absolute value of the stack top element
13. Copy			/ cpy		/ pssp			| X					 // Copies the value at address defined at memory.count - top of stack
14. Write			/ wrt		/ pspspsps		| X					 // Writes the value as ascii in the console
15. CopyAt			/ cat		/ psspss		| X					 // Copies the value at the address of stack index (top of stack)
16. JumpAt			/ jat		/ pppss			| X					 // Sets the instruction pointer to (top of stack) index
17. CurrentIns		/ cip		/ pppp			| X					 // Pushes the current instruction pointer value to the top of the stack
18. Swap			/ swp		/ psppss		| X					 // Swaps the values at defined index offset (top of stack). Ex : [..., a, b, 1, 2], swap -> [..., b, a]
19. Function		/ fnc		/ pspspss		| O					 // Defines a function of name argument | To note : A function cannot have the same name as a label, and vice versa
20. Return			/ ret		/ spspspp		| X					 // Returns from the function and goes back to call site
21. Execute			/ exe		/ sspspsp		| O					 // Calls the function of name argument
22. Char			/ chr		/ pspsspsp		| X					 // Writes corresponding character from int (top of stack)
23. SetAt			/ sat		/ pspss			| X					 // Sets the value at address a to b (from stack : [..., a, b])
24. Allocate		/ all		/ psss			| X					 // Pushes x amount of 0s on the stack (x is top of stack)
25. Modulo			/ mod		/ psssp			| X					 //	Pushes a % b (from [..., a, b])
26. Set				/ set		/ psppsppsp		| X				     // Sets the value at offset -b to a (from [..., a, b]) 
27. Exit			/ ext		/ ssss			| X					 // Exits the program execution