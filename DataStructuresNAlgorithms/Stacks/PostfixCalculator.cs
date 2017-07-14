using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresNAlgorithms.Stacks
{
    /// <summary>
    /// Also called Polish Notation
    /// Operator follow the operands
    /// Infix => 5+2
    /// Postfix => 5 2 + 
    /// </summary>
    public class PostfixCalculator
    {
        private Stack<double> Values { get; set; }

        public PostfixCalculator()
        {
            
        }

        public double Calculate(string[] args)
        {
            Values = new Stack<double>();
            foreach (var token in args)
            {
                double val;
                if (double.TryParse(token, out val))
                {
                    Values.Push(val);
                }
                else
                {
                    double rhs;
                    double lhs;
                    try
                    {
                        rhs = Values.Pop();
                        lhs = Values.Pop();
                    }
                    catch(Exception e)
                    {
                        throw new ArgumentException($"Invalid Postfix calculation sequence, {e.Message}");
                    }
                    switch (token)
                    {
                        case "+":
                            Values.Push(lhs + rhs);
                            break;
                        case "-":
                            Values.Push(lhs - rhs);
                            break;
                        case "*":
                            Values.Push(lhs * rhs);
                            break;
                        case "/":
                            Values.Push(lhs / rhs);
                            break;
                        case "%":
                            Values.Push(lhs / rhs);
                            break;
                        default: throw new ArgumentException($"Unrecognized token {token}");
                    }
                }
            }
            return Values.Pop();
        }

    }
}
