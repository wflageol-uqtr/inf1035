using System;
using System.Collections.Generic;

namespace FileWalker
{
    interface IElement
    {
        string Name { get; }
        void Accept(IVisitor visitor);
    }

    class Folder : IElement
    {
        private List<IElement> children = new List<IElement>();

        public string Name { get; }
        public Folder(string name)
        {
            Name = name;
        }

        public void Add(IElement childElement)
        {
            children.Add(childElement);
        }

        public void Accept(IVisitor visitor)
        {
            foreach(var child in children)
            {
                child.Accept(visitor);
            }
            visitor.Visit(this);
        }
    }

    class File : IElement
    {
        public string Name { get; }
        public File(string name)
        {
            Name = name;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    interface IVisitor
    {
        void Visit(File file);
        void Visit(Folder folder);
    }

    class PrintVisitor : IVisitor
    {
        public void Visit(File file)
        {
            Console.WriteLine(file.Name);
        }

        public void Visit(Folder folder)
        {
            Console.WriteLine("Folder: {0}", folder.Name);
        }
    }

    class CountVisitor : IVisitor
    {
        public int Count { get; private set; }

        public void Visit(File file)
        {
            Count++;
        }

        public void Visit(Folder folder)
        {
            
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var folder = new Folder("Folder 1");
            var file = new File("readme.txt");
            folder.Add(file);
            var subfolder = new Folder("Folder 2");
            folder.Add(subfolder);

            var folder2 = new Folder("Folder 2");
            var file2 = new File("context.txt");
            folder2.Add(file2);

            subfolder.Add(file2);

            var visitor = new CountVisitor();
            folder.Accept(visitor);
            Console.WriteLine(visitor.Count);
        }
    }
}
