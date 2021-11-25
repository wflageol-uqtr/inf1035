using SimpleLisp.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLisp.Expressions
{
    interface IExpression
    {
        void Accept(IVisitor visitor);
    }
}
