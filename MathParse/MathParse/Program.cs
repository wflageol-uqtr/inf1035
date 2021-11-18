using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MathParse
{
    class Program
    {
        static IEnumerable<string> GeneratePostfix(string input)
        {
            var postfixStack = new Stack<string>();
            var operatorStack = new Stack<string>();

            var tokens = input.Split(' ');
            postfixStack.Push(tokens[0]);

            foreach(var token in tokens.Skip(1))
            {
                if (int.TryParse(token, out var _))
                {
                    postfixStack.Push(token);
                    if (operatorStack.Any())
                        postfixStack.Push(operatorStack.Pop());
                } else
                {
                    operatorStack.Push(token);
                }
            }

            return postfixStack.Reverse();
        }

        static int Calculate(IEnumerable<string> postfix)
        {
            var stack = new Stack<int>();

            foreach(var token in postfix)
            {
                if(int.TryParse(token, out var i)) 
                    stack.Push(i);
                else
                {
                    var n1 = stack.Pop();
                    var n2 = stack.Pop();

                    int result;
                    switch(token)
                    {
                        case "+":
                            result = n2 + n1;
                            break;
                        case "-":
                            result = n2 - n1;
                            break;
                        case "/":
                            result = n2 / n1;
                            break;
                        case "*":
                            result = n2 * n1;
                            break;
                        default:
                            throw new InvalidOperationException();
                    }

                    stack.Push(result);
                }
            }

            return stack.Pop();
        }

        static void PrintStack(IEnumerable<string> stack)
        {
            foreach (var element in stack)
                Console.Write("{0} ", element);
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            var postfix = GeneratePostfix("5 + 8 * 2");
            Console.WriteLine(Calculate(postfix));
        }
    }
}
