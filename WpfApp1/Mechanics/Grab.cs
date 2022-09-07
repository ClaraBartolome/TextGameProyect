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
    public static class Grab
    {
        private static World world = World.GetInstance();
        private static TextDisplayer textDisplayer = TextDisplayer.GetInstance();
        private static Player player = Player.GetInstance();
        private static GameResourceManager resManager = GameResourceManager.GetInstance();

        public static void PlayerGrab(List<string> input)
        {
            string entityToGrab = string.Join(" ", input).RemoveAccent().ToLower();
            if (world.ItemExists(entityToGrab) && player.getRoom().ItemInRoom(world.GetItem(entityToGrab).id))
            {
                Item item = world.GetItem(entityToGrab);
                if (item.itemType == ItemType.FURNITURE)
                {
                    textDisplayer.DisplayAction(resManager.rm.GetString("noGrab"));
                }
                else
                {
                    player.inventory.Add(item);
                    player.getRoom().RemoveItem(item.id);
                    textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("itemGrabbed"), item.name));
                }
                
            }
            else
            {
                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("notHere"), entityToGrab));
            }
        }
    }
}
