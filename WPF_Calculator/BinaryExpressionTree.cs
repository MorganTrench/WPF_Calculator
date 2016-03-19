using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace WPF_Calculator
{
    public class BinaryExpressionTree
    {
        // Define operators
        enum associativity { Left, Right };
        private struct operatorProperties
        {
            public operatorProperties(int p, associativity a) { precedence = p; assoc = a; }
            public int precedence;
            public associativity assoc;

        }

        Expression rootExpression = null;

        Dictionary<String, operatorProperties> operatorData = new Dictionary<String, operatorProperties>()
        {
            {"+", new operatorProperties(2, associativity.Left)},
            {"-", new operatorProperties(2, associativity.Left)},
            {"*", new operatorProperties(3, associativity.Left)},
            {"/", new operatorProperties(3, associativity.Left)},
            {"^", new operatorProperties(4, associativity.Right)}
        };

        public BinaryExpressionTree(String raw)
        {
            rootExpression = generateTree(inFixToPostFix(raw));
        }

        // return the evaluated tree
        public float evaluateTree()
        {
            return rootExpression.evaluate();
        }

        // Converts Infix expressions to Postfix Expressions
        private String inFixToPostFix(String input)
        {

            // Potential Improvements
            // Use queue instead of array for tokens, if memory matters
            // Add ability to handle functions

            // Initialization
            String[] tokens = input.Split(' ');
            String output = "";
            Stack<String> operatorStack = new Stack<String>();

            // Shunting Yard Algorithm
            // https://www.wikiwand.com/en/Shunting-yard_algorithm
            for (int i = 0; i < tokens.Length; i++)
            {
                String token = tokens[i];
                bool isOp = isOperator(token);
                bool isNumber = (!isOp) && (token != "(" && token != ")");

                // If token is a number, add it to the output queue
                if (isNumber)
                    output += tokens[i] + " ";

                // Insert function stuff here

                // If token is an operator (o1)
                if (isOp)
                {
                    while (operatorStack.Count > 0) // exists o2
                    {
                        if ( isOperator(operatorStack.Peek()) &&
                            ((operatorData[token].assoc == associativity.Left) && (operatorData[token].precedence <= operatorData[operatorStack.Peek()].precedence))
                            || ((operatorData[token].assoc == associativity.Right) && (operatorData[token].precedence < operatorData[operatorStack.Peek()].precedence)))
                            output += operatorStack.Pop() + " ";
                        else
                            break;
                    }
                    operatorStack.Push(token);
                } else if (token == "(")
                {
                    operatorStack.Push(token);
                } else if (token == ")")
                {
                    // Lazy evaluation will prevent any errors here
                    while ((operatorStack.Count > 0) && (operatorStack.Peek() != "("))
                    {
                        output += operatorStack.Pop() + " ";
                    }
                    // operator stack now has left parenthesis at top or is empty
                    if (operatorStack.Count == 0)
                        throw new Exception("Mismatched Parenthesis - Location 1");

                    // TODO Add function matching here
                    operatorStack.Pop();

                }
            }
            while (operatorStack.Count > 0)
            {
                if (operatorStack.Peek() == "(")
                    throw new Exception("Mismatched Parenthesis - Location 2");
                output += operatorStack.Pop() + " ";
            }

            return output.Trim();
        }

        // Returns a BET that reflects the PostFix expression argument
        private Expression generateTree(string postFixed)
        {
            // https://www.wikiwand.com/en/Binary_expression_tree#/Construction_of_an_expression_tree
            String[] tokens = postFixed.Split(' ');
            Stack<Expression> construct = new Stack<Expression>();
            for(int i = 0; i < tokens.Length; i++)
            {
                String token = tokens[i];
                if (token == "")
                    continue;

                if (!isOperator(token))
                {
                    construct.Push(new Constant(float.Parse(token)));
                } else
                {
                    // T1
                    Expression rightChild = construct.Pop();
                    // T2
                    Expression leftChild = construct.Pop();
                    // Node( T2, T1)
                    construct.Push(getOperatorExpression(token, leftChild, rightChild));
                }
            }
            return construct.Pop();
        }

        /// Helper Functions ///
        // Checks if the given token is contained in operatorData
        private bool isOperator(String token)
        {
            return operatorData.ContainsKey(token);
        }

        // Takes an operator token and intended tree children, and returns an Expression representation
        private Expression getOperatorExpression(String optoken, Expression left, Expression right)
        {
            //TODO Come up with a way to do this without enumerating every operator, perhaps by somehow pointing to the right constructor using the operatorData dictionary
            switch (optoken)
            {
                case "+":
                    return new Add(left, right);
                case "-":
                    return new Sub(left, right);
                case "*":
                    return new Multi(left, right);
                case "/":
                    return new Div(left, right);
                case "^":
                    return new Pow(left, right);
                default:
                    throw new Exception("Unmatched Operator Token");
            }
        }
    }

}

