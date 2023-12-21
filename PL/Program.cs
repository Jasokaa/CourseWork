using CL;

namespace PL;

internal static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            string input = ConsoleMenu.Commands();
            int returnFromUserInput = ConsoleMenu.UserInputCommands(input);
            while (returnFromUserInput != 1)
            {
                input = ConsoleMenu.Commands();
                returnFromUserInput = ConsoleMenu.UserInputCommands(input);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    } 
}