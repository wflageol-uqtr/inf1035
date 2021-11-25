using SimpleLisp.Expressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLisp.Read
{
    class ExpressionReader
    {
        private StringReader reader;

        internal IExpression Read(string code)
        {
            reader = new StringReader(code);

            return ReadExpression();
        }

        private IExpression ReadListExpression()
        {
            var expressions = new List<IExpression>();

            var expression = ReadExpression();
            while(expression != null)
            {
                expressions.Add(expression);
                expression = ReadExpression();
            }

            return new ListExpression(expressions);
        }

        private IExpression ReadExpression()
        {
            SkipSpaces();
            char next = Peek();
            if (next == ')')
            {
                Read();
                return null;
            }
            else if (next == '(')
            {
                Read();
                return ReadListExpression();
            }
            else
                return ReadLiteralExpression();
        }

        private IExpression ReadLiteralExpression()
        {
            string content = ReadUntil(new[] { ' ', ')' });

            return new LiteralExpression(content);
        }

        private void SkipSpaces()
        {
            while (Peek() == ' ')
                Read();
        }

        private string ReadUntil(IEnumerable<char> endChars)
        {
            var builder = new StringBuilder();

            char next = Peek();
            if (next == -1)
                throw new ArgumentException();
            while(!endChars.Contains(next) && next != -1)
            {
                builder.Append(Read());
                next = Peek();
            }

            return builder.ToString();
        }

        private char Read() => (char)reader.Read();
        private char Peek() => (char)reader.Peek();
    }
}
