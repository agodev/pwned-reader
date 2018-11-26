using CommandLine;

public class Options
{
    [Option('f', "file", Required = true, HelpText = "Input file to be processed.")]
    public string InputFile { get; set; }

    [Option('p', "pass", Required = true, HelpText = "The password to search for")]
    public string Password { get; set; }
}
