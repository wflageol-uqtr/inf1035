using SimpleLisp.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLisp.Expressions
{
    class LiteralExpression : IExpression
    {
        public string Content { get; }

        public LiteralExpression(string content)
        {
            Content = content;
        }

        public override string ToString() => Content;
        public void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}
