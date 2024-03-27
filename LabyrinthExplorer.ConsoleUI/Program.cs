namespace LabyrinthExplorer.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleUI UI = new ConsoleUI(); //creates User Interface, UI creates GameEngine inside

            while (UI.RunGameStep()) { } //running game until false returned
            
            Console.ReadKey();
        }
    }
}
