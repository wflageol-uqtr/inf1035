using SimpleLisp.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLisp.Expressions
{
    class ListExpression : IExpression
    {
        public IEnumerable<IExpression> Elements { get; }

        public ListExpression(IEnumerable<IExpression> elements)
        {
            Elements = elements;
        }

        public void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}
