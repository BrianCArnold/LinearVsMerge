internal class Program
{
  private static void Main(string[] args)
  {
    List<string> TheListOfArgs = new();

    Console.WriteLine($"Next we do some stuff with {nameof(TheListOfArgs)}");

    foreach (var str in args.Skip(1))
    {
      TheListOfArgs.Add(str);
    }

    Console.WriteLine("And then we send the list of args to a function to process (print) them");

    ProcessArgs(TheListOfArgs);
  }

  private static void ProcessArgs(IList<string> args)
  {

    Console.WriteLine("These are the command line args.");
    for (int i = 1; i < args.Count(); i++)
    {
      Console.WriteLine($"- {args[i]}");

    }
  }
}
