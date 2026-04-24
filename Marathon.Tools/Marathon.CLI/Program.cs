using Marathon.CLI.Commands;
using Spectre.Console.Cli;

namespace Marathon.CLI
{
    public static class Program
    {
        public const string Name = "Marathon.CLI";

        public static void Main(string[] in_args)
        {
            Console.Title = Name;

            var app = new CommandApp();

            app.Configure(cfg =>
            {
                cfg.SetApplicationName(Name);
                cfg.TrimTrailingPeriods(false);

                cfg.AddCommand<FileCommand>("file");
                cfg.AddCommand<ListCommand>("list");
                cfg.AddCommand<TreeCommand>("tree");
                cfg.AddCommand<SearchCommand>("search");
            });

            app.SetDefaultCommand<FileCommand>();
            app.Run(in_args);
        }
    }
}