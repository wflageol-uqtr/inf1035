using SimpleLisp.Read;
using SimpleLisp.Visitors;
using System;

namespace SimpleLisp
{
    class Program
    {
        static void Main(string[] args)
        {
            var code = "(progn " +
                "(let l (list 1 2 3))" +
                "(let l (map square l))" +
                "(reduce + l)" +
                ")";

            var reader = new ExpressionReader();
            var expr = reader.Read(code);

            var evalVisitor = new EvalVisitor();
            expr.Accept(evalVisitor);

            var result = evalVisitor.Result;

            Console.WriteLine(result);
            //var printVisitor = new PrintVisitor();
            //result.Accept(printVisitor);

            //Console.WriteLine(printVisitor);
        }
    }
}
