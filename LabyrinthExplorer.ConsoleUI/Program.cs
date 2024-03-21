namespace LabyrinthExplorer.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleUI UI = new ConsoleUI();

            while (UI.RunGameStep()) { } //running game until false returned
            
            Console.ReadKey();
        }
    }
}
