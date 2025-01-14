// See https://aka.ms/new-console-template for more information

Console.WriteLine("These are the command line args.");

args = args.Skip(1).ToArray();

for (int i = 0; i < args.Length; i++)
{
  Console.WriteLine($"- {args[i]}");

}
