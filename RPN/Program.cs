using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPN_Algorithm
{
    class RPN
    {
        public static string ConvertToRPN(string initialString)
        {
            Stack<char> operationsStack = new Stack<char>();

            char lastOperation;
            var result = new List<string>();
            initialString = initialString.Replace(" ", "");

            for (int i = 0; i < initialString.Length; i++)
            {
                if (char.IsDigit(initialString[i]))
                {
                    result.Add(initialString[i].ToString());
                    i++;
                    while (i < initialString.Length && char.IsDigit(initialString[i]))
                    {
                        result[^1] += initialString[i];
                        i++;
                    }

                    i--;
                    continue;
                }

                if (IsOperation(initialString[i]))
                {
                    if (!(operationsStack.Count == 0))
                        lastOperation = operationsStack.Peek();
                    else
                    {
                        operationsStack.Push(initialString[i]);
                        continue;
                    }

                    if (OperationPriority(lastOperation) < OperationPriority(initialString[i]))
                    {
                        operationsStack.Push(initialString[i]);
                        continue;
                    }

                    else
                    {
                        result.Add(operationsStack.Pop().ToString());

                        operationsStack.Push(initialString[i]);
                        continue;
                    }
                }

                if (initialString[i].Equals('('))
                {
                    operationsStack.Push(initialString[i]);
                    continue;
                }

                if (initialString[i].Equals(')'))
                {
                    while (operationsStack.Peek() != '(')
                    {
                        result.Add(operationsStack.Pop().ToString());
                    }

                    operationsStack.Pop();
                }
            }

            while (!(operationsStack.Count == 0))
            {
                result.Add(operationsStack.Pop().ToString());
            }

            return String.Join(" ", result);
        }

        private static bool IsOperation(char c)
        {
            if (c == '+' ||
                c == '-' ||
                c == '*' ||
                c == '/')
                return true;
            else
                return false;
        }


        private static int OperationPriority(char c)
        {
            switch (c)
            {
                case '+': return 1;
                case '-': return 1;
                case '*': return 2;
                case '/': return 2;
                default: return 0;
            }
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Введите выражение в обычной форме");
            var firstinput = ConvertToRPN(Console.ReadLine());
            Console.WriteLine("Выражение в обратной польской записи:");
            Console.WriteLine(firstinput);
            var input = firstinput.Split();
            var numbers = new Stack<double>();
            foreach (var elem in input)
            {
                double num1;
                var isNumber = double.TryParse(elem, out num1);
                if (isNumber)
                    numbers.Push(num1);
                else
                {
                    double num2;
                    if (elem == "+")
                        numbers.Push(numbers.Pop() + numbers.Pop());
                    else if (elem == "*")
                        numbers.Push(numbers.Pop() * numbers.Pop());
                    else if (elem == "-")
                    {
                        num2 = numbers.Pop();
                        numbers.Push(numbers.Pop() - num2);
                    }
                    else if (elem == "/")
                    {
                        num2 = numbers.Pop();
                        if (num2 != 0.0)
                            numbers.Push(numbers.Pop() / num2);
                        else
                            Console.WriteLine("Ошибка. Невозможно делить на ноль");
                    }
                    else
                        Console.WriteLine("Ошибка. Неизвестный символ");
                }
            }
            Console.WriteLine("Результат:");
            Console.WriteLine(numbers.Pop());
        }
    }
}
