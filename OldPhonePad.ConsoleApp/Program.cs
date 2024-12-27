using Microsoft.Extensions.DependencyInjection;
using OldPhonePad.ConsoleApp;
using OldPhonePad.Core.Interfaces;

public class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = Startup.ConfigureServices();

        var oldPhonePadToTextService =
            serviceProvider.GetService<IOldPhonePadToTextService>()
            ?? throw new Exception("IOldPhonePadToTextService not found");

        Console.WriteLine("*** Instructions *** ");
        Console.WriteLine("1. Enter valid old keypad input to get the text output.");
        Console.WriteLine("2. Type \"exit\" to end the program.\n");

        while (true)
        {
            Console.Write("Enter old keypad input: ");

            var input = Console.ReadLine();

            if (input == "exit")
            {
                Console.WriteLine("Exiting Program...");
                break;
            }

            try
            {
                var output = oldPhonePadToTextService.ConvertOldPhonePadInputToText(input ?? "");

                Console.WriteLine($"Output: {output}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n");
            }
        }
    }
}