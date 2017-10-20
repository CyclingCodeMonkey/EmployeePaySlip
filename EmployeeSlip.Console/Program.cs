using System;
using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace EmployeeSlip.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
            SetCommandLineOptions(args);
            System.Console.ReadLine();
        }

        private static void SetCommandLineOptions(string[] args)
        {
            CommandLineApplication commandLineApplication =
                new CommandLineApplication(throwOnUnexpectedArg: false);
            CommandArgument names = null;
            commandLineApplication.Command("name",
                (target) =>
                    names = target.Argument(
                        "fullname",
                        "Enter the full name of the person to be greeted.",
                        multipleValues: true));
            CommandOption greeting = commandLineApplication.Option(
                "-$|-g |--greeting <greeting>",
                "The greeting to display. The greeting supports"
                + " a format string where {fullname} will be "
                + "substituted with the full name.",
                CommandOptionType.SingleValue);
            CommandOption uppercase = commandLineApplication.Option(
                "-u | --uppercase", "Display the greeting in uppercase.",
                CommandOptionType.NoValue);
            commandLineApplication.HelpOption("-? | -h | --help");
            commandLineApplication.OnExecute(() =>
            {
                if (greeting.HasValue())
                {
                    Greet(greeting.Value(), names.Values, uppercase.HasValue());
                }
                return 0;
            });
            commandLineApplication.Execute(args);
        }

        private static void Greet(
            string greeting, IEnumerable<string> values, bool useUppercase)
        {
            System.Console.WriteLine(greeting);
        }
    }
}
