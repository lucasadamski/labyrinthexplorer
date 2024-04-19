
using System.Reflection;

namespace LabyrinthExplorer.ConsoleUI
{
    public static class UISettings
    {
        public static string PATH_SOUND_GAME_OVER = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Sounds\GameOver.wav";
        public static string PATH_SOUND_ITEM_PICKUP = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Sounds\KeyPickUp.wav";
        public static string PATH_SOUND_LAB_EX_LOGO = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Sounds\LebirynthExplorer.wav";
        public static string PATH_SOUND_LUCADA_LOGO = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Sounds\LucadaSoftware.wav";
        public static string PATH_SOUND_RECEIVE_DAMAGE = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Sounds\ReceivedDamage.wav";
    }
}
