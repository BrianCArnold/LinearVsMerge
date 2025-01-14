// See https://aka.ms/new-console-template for more information

Console.WriteLine("These are the command line args.");

for (int i = 1; i < args.Length; i++)
{
  Console.WriteLine($"- {args[i]}");

}
