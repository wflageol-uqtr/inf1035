using SimpleLisp.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLisp.Visitors
{
    class EvalVisitor : IVisitor
    {
        private Dictionary<string, IExpression> bindings = new Dictionary<string, IExpression>();

        public IExpression Result { get; private set; }

        public void Visit(ListExpression expr)
        {
            Result = Eval(expr);
        }

        public void Visit(LiteralExpression expr) => Result = expr;

        private IExpression Eval(ListExpression expr)
        {
            var fn = expr.Elements.First().ToString();

            switch(fn)
            {
                case "progn":
                    IExpression result = null;
                    foreach (var element in expr.Elements.Skip(1))
                        result = EvalExpr(element);
                    return result;
                case "let":
                    var binding = expr.Elements.ElementAt(1).ToString();
                    var content = expr.Elements.ElementAt(2);
                    return EvalLet(binding, content);
                case "list":
                    return new ListExpression(expr.Elements.Skip(1));
                case "inc":
                case "dec":
                case "double":
                case "square":
                    return EvalUnary(fn, expr);
                case "+":
                case "-":
                case "*":
                case "/":
                case "map":
                case "reduce":
                    return EvalBinary(fn, expr);
                default:
                    throw new ArgumentException();
            }
        }

        private IExpression EvalBinary(string fn, ListExpression expr)
        {
            var left = expr.Elements.ElementAt(1);
            var right = expr.Elements.ElementAt(2);

            switch (fn)
            {
                case "+":
                    return EvalAdd(left, right);
                case "-":
                    return EvalSubtract(left, right);
                case "*":
                    return EvalMultiply(left, right);
                case "/":
                    return EvalDivide(left, right);
                case "map":
                    return EvalMap(left, right);
                case "reduce":
                    return EvalReduce(left, right);
                default:
                    throw new ArgumentException();
            }
        }

        private IExpression EvalMap(IExpression function, IExpression arg)
        {
            var expressions = EvalList(arg);
            var result = expressions.Select(
                expr => EvalExpr(new ListExpression(new[] { function, expr })));

            return new ListExpression(result);
        }

        private IExpression EvalReduce(IExpression function, IExpression arg)
        {
            var expressions = EvalList(arg);
            return expressions.Skip(1).Aggregate(
                expressions.First(),
                (l, r) => EvalExpr(new ListExpression(new[] { function, l, r })));
        }

        private IExpression EvalDivide(IExpression left, IExpression right) =>
            IntegerExpr(EvalNumber(left) / EvalNumber(right));

        private IExpression EvalMultiply(IExpression left, IExpression right) =>
            IntegerExpr(EvalNumber(left) * EvalNumber(right));

        private IExpression EvalSubtract(IExpression left, IExpression right) =>
            IntegerExpr(EvalNumber(left) - EvalNumber(right));

        private IExpression EvalAdd(IExpression left, IExpression right) =>
            IntegerExpr(EvalNumber(left) + EvalNumber(right));

        private IExpression EvalUnary(string fn, ListExpression expr)
        {
            var arg = expr.Elements.ElementAt(1);

            switch(fn)
            {
                case "inc":
                    return EvalInc(arg);
                case "dec":
                    return EvalDec(arg);
                case "double":
                    return EvalDouble(arg);
                case "square":
                    return EvalSquare(arg);
                default:
                    throw new ArgumentException();
            }
        }

        private IExpression EvalSquare(IExpression arg)
        {
            var n = EvalNumber(arg);
            return IntegerExpr(n * n);
        }

        private IExpression EvalDouble(IExpression arg)
        {
            var n = EvalNumber(arg);
            return IntegerExpr(n * 2);
        }

        private IExpression EvalDec(IExpression arg)
        {
            var n = EvalNumber(arg);
            return IntegerExpr(n - 1);
        }
        
        private IExpression EvalInc(IExpression arg)
        {
            var n = EvalNumber(arg);
            return IntegerExpr(n + 1);
        }

        private IExpression EvalLet(string binding, IExpression content)
        {
            var result = EvalExpr(content);
            bindings[binding] = result;

            return result;
        }

        private IExpression EvalExpr(IExpression expr)
        {
            var literal = expr.ToString();
            if (bindings.ContainsKey(literal))
                return bindings[literal];

            expr.Accept(this);

            return Result;
        }

        private int EvalNumber(IExpression expr) =>
            int.Parse(EvalExpr(expr).ToString());

        private IEnumerable<IExpression> EvalList(IExpression expr) =>
            ((ListExpression)EvalExpr(expr)).Elements;

        private IExpression IntegerExpr(int n) => new LiteralExpression(n.ToString());    }
}
