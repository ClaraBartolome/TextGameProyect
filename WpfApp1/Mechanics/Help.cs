using Character;
using GameWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGame.Componentes;
using TextGameProyect.Utils;

// https://github.com/ClaraBartolome/TextGameProyect

namespace TextGame.Mechanics
{
    public static class Help
    {
        private static World world = World.GetInstance();
        private static TextDisplayer textDisplayer = TextDisplayer.GetInstance();
        private static Player player = Player.GetInstance();
        private static GameResourceManager resManager = GameResourceManager.GetInstance();
        public static void PlayerHelp(List<String> options)
        {
            textDisplayer.DisplayAction(resManager.rm.GetString("availableActions"));
            foreach (var option in options)
            {
                textDisplayer.DisplayItem(option);
            }
            textDisplayer.Jumpline();
            textDisplayer.DisplayAction(resManager.rm.GetString("itemsInSight"));
            foreach (var item in player.getRoom().items)
            {
                textDisplayer.DisplayItem(world.GetItem(item).name);
            }
        }
    }
}
