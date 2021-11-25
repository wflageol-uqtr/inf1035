using SimpleLisp.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLisp.Visitors
{
    interface IVisitor
    {
        void Visit(ListExpression expr);
        void Visit(LiteralExpression expr);
    }
}
