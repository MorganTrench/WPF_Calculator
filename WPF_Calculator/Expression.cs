using System;
using System.Diagnostics;

namespace WPF_Calculator
{
    public abstract class Expression
    {
        //Fields
        protected Expression left, right;

        // Constructor
        public Expression(Expression l, Expression r)
        {
            this.left = l;
            this.right = r;
        }
        public abstract float evaluate();
        public abstract String toString();
    }

    public class Constant : Expression
    {
        //Fields
        private float value;
        // Constructor
        public Constant(float val) : base(null, null) { value = val; }

        public override float evaluate()
        {
            return value;
        }

        public override string toString()
        {
            return "" + value;
        }
    }

    public class Add : Expression
    {
        // Constructor
        public Add(Expression l, Expression r) : base(l, r) { }

        public override float evaluate()
        {
            float result = left.evaluate() + right.evaluate();
            return result;
        }

        public override string toString()
        {
            String temp = "";
            if (left != null)
                temp += "(" + left.toString();

            if ((left != null) && (right != null))
                temp += " + ";

                if (right != null)
                temp += right.toString() + ")";
            return temp;
        }
    }

    public class Sub : Expression
    {
        // Constructor
        public Sub(Expression l, Expression r) : base(l, r) { }

        public override float evaluate()
        {
            float result = left.evaluate() - right.evaluate();
            return result;
        }

        public override string toString()
        {
            String temp = "";
            if (left != null)
                temp += "(" + left.toString();

            if ((left != null) && (right != null))
                temp += " - ";

            if (right != null)
                temp += right.toString() + ")";
            return temp;
        }
    }

    public class Multi : Expression
    {
        // Constructor
        public Multi(Expression l, Expression r) : base(l, r) { }

        public override float evaluate()
        {
            float result = left.evaluate() * right.evaluate();
            return result;
        }

        public override string toString()
        {
            String temp = "";
            if (left != null)
                temp += "(" + left.toString();

            if ((left != null) && (right != null))
                temp += " * ";

            if (right != null)
                temp += right.toString() + ")";
            return temp;
        }
    }

    public class Div : Expression
    {
        // Constructor
        public Div(Expression l, Expression r) : base(l, r) { }

        public override float evaluate()
        {
            float result = left.evaluate() / right.evaluate();
            return result;
        }

        public override string toString()
        {
            String temp = "";
            if (left != null)
                temp += "(" + left.toString();

            if ((left != null) && (right != null))
                temp += " / ";

            if (right != null)
                temp += right.toString() + ")";
            return temp;
        }
    }

    public class Pow : Expression
    {
        // Constructor
        public Pow(Expression l, Expression r) : base(l, r) { }

        public override float evaluate()
        {
            float result = (float) Math.Pow(left.evaluate(), right.evaluate());
            return result;
        }

        public override string toString()
        {
            String temp = "";
            if (left != null)
                temp += "(" + left.toString();

            if ((left != null) && (right != null))
                temp += " ^ ";

            if (right != null)
                temp += right.toString() + ")";
            return temp;
        }
    }

}
