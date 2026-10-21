using System.CommandLine;

namespace MyCsharpChessEngine.src.Core
{
    public class ArgParser
    {
        private readonly Option<bool> headlessOption = new("--headless")
        {
            Description = "Runs the program without displaying a window.",
            Recursive = true,
            // should allow for cli input if playing
        };
        private readonly Command playingCommand = new("play")
        {
            Description = "Allows for player input against the engine.",
        };
        private readonly Command benchCommand = new("benchmark")
        {
            Description = "Benchmarks the engine against a compatible executable.",
        };

        public readonly RootCommand RootCommand;
        public readonly ParseResult parsedArgs;

        public ArgParser(string[] args)
        {
            RootCommand = [headlessOption, playingCommand, benchCommand];
            parsedArgs = RootCommand.Parse(args);
            SetupActions();
            SetSettings();
        }

        private void DefaultAction(ParseResult result)
        {
            Console.Error.WriteLine("Error: Must provide command. See --help.");
        }

        private void SetupActions()
        {
            RootCommand.SetAction(DefaultAction);
            playingCommand.SetAction(PlayAction.Run);
            benchCommand.SetAction(BenchmarkAction.Run);
        }

        private void SetSettings()
        {
            Settings.IsHeadless = parsedArgs.GetValue(headlessOption);
        }
    }
}
