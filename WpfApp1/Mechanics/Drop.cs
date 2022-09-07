using Character;
using Componentes;
using GameWorld;
using StringExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGame.Componentes;
using TextGameProyect.Utils;

namespace TextGame.Mechanics
{
    public static class Drop
    {
        private static World world = World.GetInstance();
        private static TextDisplayer textDisplayer = TextDisplayer.GetInstance();
        private static Player player = Player.GetInstance();
        private static GameResourceManager resManager = GameResourceManager.GetInstance();
        public static void PlayerDrop(List<string> input)
        {
            string entityToDrop = string.Join(" ", input).RemoveAccent().ToLower();
            if (world.ItemExists(entityToDrop) && player.InInventory(world.GetItem(entityToDrop)))
            {
                Item item = world.GetItem(entityToDrop);
                player.getRoom().items.Add(item.id);
                player.DropItem(item);
                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("itemDropped"), item.name));
            }
            else
            {
                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("itemNotInInventory"), entityToDrop));
            }
        }
    }
}
