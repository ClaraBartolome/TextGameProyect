using Character;
using Componentes;
using Engine;
using GameWorld;
using Literales;
using StringExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGame.Componentes;
using TextGame.Utils;
using TextGameProyect.Utils;

namespace TextGame.Mechanics
{
    public static class Look
    {
        private static World world = World.GetInstance();
        private static TextDisplayer textDisplayer = TextDisplayer.GetInstance();
        private static Player player = Player.GetInstance();
        private static GameEngine engine = GameEngine.GetInstance();
        private static GameResourceManager resManager = GameResourceManager.GetInstance();
        public static void PlayerLook(List<string> input)
        {
            string entityToLook = string.Join(" ", input).RemoveAccent().ToLower();
            if (entityToLook != "" && !entityToLook.Equals(resManager.rm.GetString("lookAround")))
            {
                Blueprint item;
                if (!world.ItemExists(entityToLook) && !world.DoorExists(entityToLook))
                {
                    textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("notHere"), entityToLook));
                }
                else if (world.ItemExists(entityToLook) && player.getRoom().ItemInRoom(world.GetItem(entityToLook).id))
                {
                    item = world.GetItem(entityToLook);
                    if (player.getRoom().ItemInRoom(item.id))
                    {
                        textDisplayer.DisplayItem(item.name);
                        textDisplayer.DisplayItem(item.description);
                        textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("grabItem"), item.name));
                        engine.SetNextAction(resManager.rm.GetString("grab") + " " + item.name);
                    }
                    else
                    {
                        textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("notHere"), entityToLook));
                    }
                }
                else if (world.DoorExists(entityToLook) && player.getRoom().DoorInRoom(world.GetDoor(entityToLook).id))
                {
                    item = world.GetDoor(entityToLook);
                    if (item.id != -2 && player.getRoom().DoorInRoom(item.id))
                    {
                        textDisplayer.DisplayItem(item.name);
                        textDisplayer.DisplayItem(item.description);
                    }
                }
                else
                {
                    textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("notHere"), entityToLook));
                }
            }
            else
            {
                textDisplayer.DisplayAction(player.getRoom().description);
                ShowObjects(player.getRoom(), world, textDisplayer);
            }
        }
       
        private static void ShowObjects(Room room, World world, TextDisplayer textDisplayer)
        {
            textDisplayer.DisplayAction(resManager.rm.GetString("itemsInSight"));
            textDisplayer.Jumpline();
            foreach (var item in room.items)
            {
                textDisplayer.DisplayItem(world.GetItem(item).name);
            }
        }


    }

}
