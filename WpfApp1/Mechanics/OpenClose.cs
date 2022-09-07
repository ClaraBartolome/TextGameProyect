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
    public static class OpenClose
    {
        private static World world = World.GetInstance();
        private static TextDisplayer textDisplayer = TextDisplayer.GetInstance();
        private static Player player = Player.GetInstance();
        private static GameEngine engine = GameEngine.GetInstance();
        private static GameResourceManager resManager = GameResourceManager.GetInstance();
        public static void PlayerOpenClose(List<string> input)
        {
            string action = input[0].RemoveAccent().ToLower();
            input.RemoveAt(0);
            string entity = string.Join(" ", input).RemoveAccent().ToLower();
            if (!world.ItemExists(entity) && !world.DoorExists(entity))
            {
                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("notHere"), entity));
            }
            else if (world.DoorExists(entity)) //ES UNA PUERTA
            {
                Door door = world.GetDoor(entity);
                if (player.getRoom().DoorInRoom(door.id))
                {
                    if (door.isBlocked)
                    {
                        textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("closedWithKey"), entity));
                        Item item = world.GetItem(door.keyId);
                        if (player.InInventory(item))
                        {
                            textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("askUseKey"), entity));
                            engine.SetNextAction(resManager.rm.GetString("use") + " " + item.name + " en " + door.name);
                        }
                    }
                    else
                    {
                        if (door.open)
                        {
                            if (action == resManager.rm.GetString("close"))
                            {
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("closed"), door.name));
                            }
                            else
                            {
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("alreadyOpened"), entity));
                            }
                        }
                        else
                        {
                            if (action == resManager.rm.GetString("open"))
                            {
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("opened"), door.name));
                            }
                            else
                            {
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("alreadyClosed"), entity));
                            }
                        }
                        door.open = !door.open;
                    }
                }
                else
                {
                    textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("notHere"), entity));
                }
            }
            else if (world.ItemExists(entity)) //ES UN COFRE
            {
                Chest chest =  world.GetChest(entity);
                if (player.getRoom().ItemInRoom(chest.id))
                {
                    if (chest.isBlocked)
                    {
                        textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("closedWithKey"), entity));
                        Item item = world.GetItem(chest.keyId);
                        if (player.InInventory(item))
                        {
                            textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("askUseKey"), entity));
                            engine.SetNextAction(resManager.rm.GetString("use") + " " + item.name + " en " + chest.name);
                        }
                    }
                    else
                    {
                        if (chest.open)
                        {
                            if (action == resManager.rm.GetString("close"))
                            {
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("closed"), chest.name));
                            }
                            else
                            {
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("alreadyOpened"), entity));
                            }
                        }
                        else
                        {
                            if (action == resManager.rm.GetString("open"))
                            {
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("opened"), chest.name));
                                player.getRoom().items.AddRange(chest.itemsInside);
                                ShowObjects(chest.itemsInside);
                                engine.itemsToGrab.AddRange(chest.itemsInside);
                                chest.itemsInside.Clear();
                                textDisplayer.DisplayAction((resManager.rm.GetString("grabAllItems")));
                            }
                            else
                            {
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("alreadyClosed"), entity));
                            }
                        }
                        chest.open = !chest.open;
                    }
                }
            }
        }

        private static void ShowObjects(List<int> itemsId)
        {
            textDisplayer.DisplayAction(resManager.rm.GetString("itemsFound"));
            textDisplayer.Jumpline();
            foreach (var id in itemsId)
            {
                textDisplayer.DisplayItem(world.GetItem(id).name);
            }
        }


    }
}
