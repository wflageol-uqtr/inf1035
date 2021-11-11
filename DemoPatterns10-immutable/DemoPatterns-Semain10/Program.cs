using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DemoPatterns_Semain10
{
    class Program
    {
        private static ImmutableArray<string> database = ImmutableArray.Create("Some", "Data", null, null, null);

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
        }

        class UpdateCommand : ICommand
        {
            private int index;
            private string newData;
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
                    database = database.SetItem(index, newData);
                    isExecuted = true;
                }
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

            private Stack<ImmutableArray<string>> databaseHistory = new();

            public void Execute(ICommand command)
            {
                databaseHistory.Push(database);
                command.Execute();
            }

            public void Undo()
            {
                database = databaseHistory.Pop();
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
