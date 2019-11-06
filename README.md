# InterpretedProgrammingLanguage written in .NET
This is a simple interpreted programming language. You can see some sample programs below written in the langauge. These can be run in the .NET project included.

There are two projects one defines the abstract syntax tree, and the other is a quick and dirty parser that does not use any lexing. 

Enjoy!
## Calculate Fibonacci Numbers Recursively
```
fib| a,b
	if: a < 200
		c = sum:a,b
		print: fib> b,c
	end
	return b
t = fib> 1, 1
print: t
end
```

## Mathematical Functions

```

Mult| a,b
	x = 0
	answer = 0
	if: b< 0
		b = -b
		a = -a
	end
	while: x<b
		answer = sum: answer,a
		x = sum: x,1
	end
	return answer

Mod| a,b
	mult = a
	x = 0
	while: mult<sum: b, 1
		print: mult
		mult = sum: mult, a
		x = sum: x,1
	end
	return x

Power| a, b
	x = 0
	answer = 1

	while: x<b
		answer = Mult> answer,a
		x = sum: x,1
	end
	return answer
print: Mod> 3,15
end
```

## Nested Loop Example
```
i = 0
j = 0

while: i<30
	j = 0
	while: j<20
		if: j< 10
			sum: sum> i, j
		end
		j= sum: j, 1
	end 
	i = sum: i, 1
end
end

```
