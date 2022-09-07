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
            int? index = input.FindIndex(n => n == resManager.rm.GetString("in") || n == resManager.rm.GetString("with"));
            List<string> aux = GetItemNames(index, input);
            string entityContainer = aux[0];
            string entityKey = aux[1];
            if (!entityKey.Equals(""))// El jugador esta intentando abrir con llave
            {
                Use.PlayerUse(new List<string> {entityKey, resManager.rm.GetString("with"), entityContainer });
            }
            if (!world.ItemExists(entityContainer) && !world.DoorExists(entityContainer))
            {
                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("notHere"), entityContainer));
            }
            else if (world.DoorExists(entityContainer)) //ES UNA PUERTA
            {
                Door door = world.GetDoor(entityContainer);
                if (player.getRoom().DoorInRoom(door.id))
                {
                    if (door.isBlocked)
                    {
                        textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("closedWithKey"), entityContainer));
                        Item item = world.GetItem(door.keyId);
                        if (player.InInventory(item))
                        {
                            textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("askUseKey"), entityContainer));
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
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("alreadyOpened"), entityContainer));
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
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("alreadyClosed"), entityContainer));
                            }
                        }
                        door.open = !door.open;
                    }
                }
                else
                {
                    textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("notHere"), entityContainer));
                }
            }
            else if (world.ItemExists(entityContainer)) //ES UN COFRE
            {
                Chest chest =  world.GetChest(entityContainer);
                if (player.getRoom().ItemInRoom(chest.id))
                {
                    if (chest.isBlocked)
                    {
                        textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("closedWithKey"), entityContainer));
                        Item item = world.GetItem(chest.keyId);
                        if (player.InInventory(item))
                        {
                            textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("askUseKey"), entityContainer));
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
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("alreadyOpened"), entityContainer));
                                
                            }
                        }
                        else
                        {
                            if (action == resManager.rm.GetString("open"))
                            {
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("opened"), chest.name));
                                player.getRoom().items.AddRange(chest.itemsInside);
                                ShowObjects(chest.itemsInside);
                                List<int> listAux = chest.itemsInside.ToList<int>();
                                engine.itemsToGrab = chest.itemsInside.ToList<int>();
                                chest.itemsInside.Clear();
                                textDisplayer.DisplayAction((resManager.rm.GetString("grabAllItems")));
                            }
                            else
                            {
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("alreadyClosed"), entityContainer));
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

        private static List<string> GetItemNames(int? index, List<string> entity)
        {
            if (index.Value == -1) //solo usa el objeto
            {
                return new List<string> {
                    string.Join(" ", string.Join(" ", entity).RemoveAccent().ToLower()),
                    ""
                };
            }
            else
            {
                return new List<string> {
                    string.Join(" ", entity.GetRange(0, index.Value)).RemoveAccent().ToLower(),
                    string.Join(" ", entity.GetRange(index.Value + 1, entity.Count - index.Value - 1)).RemoveAccent().ToLower()

                };
            }
        }


    }
}
