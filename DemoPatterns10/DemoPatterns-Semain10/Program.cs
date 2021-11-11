using System;
using System.Collections;
using System.Collections.Generic;

namespace DemoPatterns_Semain10
{
    class Program
    {
        private static string[] database = { "Some", "Data", null, null, null };

        interface ICommandFactory
        {
            ICommand CreateDeleteCommand(int index);
            ICommand CreateUpdateCommand(int index, string newData);
        }

        class CommandFactory : ICommandFactory
        {
            public ICommand CreateUpdateCommand(int index, string newData) => new UpdateCommand(index, newData);
            public ICommand CreateDeleteCommand(int index) => throw new NotImplementedException();
        }

        class CommandLoggerFactory : ICommandFactory
        {
            public ICommand CreateUpdateCommand(int index, string newData)
                => new CommandLogger(new UpdateCommand(index, newData));
            public ICommand CreateDeleteCommand(int index) => throw new NotImplementedException();
        }

        interface ICommand
        {
            void Execute();
            void Undo();
        }

        class UpdateCommand : ICommand
        {
            private int index;
            private string newData;
            private string oldValue;
            private bool isExecuted = false;

            public UpdateCommand(int index, string newData)
            {
                this.index = index;
                this.newData = newData;
            }

            public void Execute()
            {
                if (!isExecuted)
                {
                    oldValue = database[index];
                    database[index] = newData;
                    isExecuted = true;
                }
            }

            public void Undo()
            {
                if (isExecuted)
                {
                    database[index] = oldValue;
                    isExecuted = false;
                }
                else
                    throw new InvalidOperationException();
            }

            public override string ToString() => string.Format("UpdateCommand: Index {0}, New Data: {1}", index, newData);
        }

        class DeleteCommand : ICommand
        {
            public void Execute()
            {
                throw new NotImplementedException();
            }

            public void Undo()
            {
                throw new NotImplementedException();
            }
        }

        class CommandLogger : ICommand
        {
            private ICommand command;

            public CommandLogger(ICommand command)
            {
                this.command = command;
            }

            public void Execute()
            {
                command.Execute();
                Console.WriteLine("Executed {0}.", command);
            }

            public void Undo()
            {
                command.Undo();
                Console.WriteLine("Undid {0}.", command);
            }
        }

        class ObservableCommand : ICommand
        {
            private ICommand command;
            private List<Action<ICommand>> observers = new List<Action<ICommand>>();

            public ObservableCommand(ICommand command)
            {
                this.command = command;
            }

            public void Attach(Action<ICommand> observer) => observers.Add(observer);
            public void Detach(Action<ICommand> observer) => observers.Remove(observer);

            private void Notify()
            {
                foreach (var observer in observers)
                    observer(command);
            }

            public void Execute()
            {
                command.Execute();
                Notify();
            }

            public void Undo()
            {
                command.Undo();
            }
        }

        sealed class CommandInvoker
        {
            private static CommandInvoker instance;

            public static CommandInvoker Instance
            { 
                get 
                {
                    if (instance == null)
                        instance = new CommandInvoker();

                    return instance;
                }
            }

            private CommandInvoker() { }

            private Stack<ICommand> executedCommands = new Stack<ICommand>();

            public void Execute(ICommand command)
            {
                executedCommands.Push(command);
                command.Execute();
            }

            public void Undo()
            {
                if (executedCommands.Peek() == null)
                    throw new InvalidOperationException();
                else
                    executedCommands.Pop().Undo();
            }
        }

        private static void PrintDatabase()
        {
            Console.WriteLine("Base de données :");
            foreach (var data in database)
                Console.WriteLine(data);
        }

        static void Main(string[] args)
        {
            PrintDatabase();

            var factory = new CommandLoggerFactory();
            var invoker = CommandInvoker.Instance;

            var command = factory.CreateUpdateCommand(0, "Hello");
            invoker.Execute(command);

            var command2 = factory.CreateUpdateCommand(0, "Lots");
            invoker.Execute(command2);

            PrintDatabase();

            invoker.Undo();

            PrintDatabase();
        }
    }
}
