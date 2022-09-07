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
using TextGameProyect.Utils;

namespace TextGame.Mechanics
{
    public static class Use
    {
        private static World world = World.GetInstance();
        private static TextDisplayer textDisplayer = TextDisplayer.GetInstance();
        private static Player player = Player.GetInstance();
        private static GameEngine engine = GameEngine.GetInstance();
        private static GameResourceManager resManager = GameResourceManager.GetInstance();
        public static void PlayerUse(List<string> entityToUse)
        {
            int? index = entityToUse.FindIndex(n => n == resManager.rm.GetString("in") || n == resManager.rm.GetString("with"));
            List<string> aux = GetItemNames(index, entityToUse);
            string firstItemName = aux[0];
            string secondItemName = aux[1];

            if (world.ItemExists(firstItemName) && player.getRoom().ItemInRoom(world.GetItem(firstItemName).id) || player.InInventory(world.GetItem(firstItemName)))
            {
                switch (world.GetItem(firstItemName).itemType)
                {
                    case ItemType.KEY:
                        {
                            UseItemKey(firstItemName, secondItemName, textDisplayer, player);
                            break;
                        }
                    case ItemType.NOTE:
                    case ItemType.CHEST:
                    case ItemType.FURNITURE:
                    case ItemType.DEFAULT:
                        {
                            if (!secondItemName.Equals(""))
                            {
                                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("noUseWith"), firstItemName, secondItemName));
                            }
                            else
                            {
                                Item item = world.GetItem(firstItemName);
                                DisplayItemMessage(item.name, item.message);
                            }
                            break;
                        }
                    default: { textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("noUseWith"), firstItemName, secondItemName)); ; break; }
                
                }
                
            }
            else
            {
                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("noUse"), firstItemName));
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

        private static void UseItemKey(string firstItemName, string secondItemName, TextDisplayer textDisplayer, Player player)
        {

            Key firstItem = (Key)world.GetItem(firstItemName);

            //Comprobamos si el objeto 2 es una puerta o un contenedor
            if (!secondItemName.Equals(resManager.rm.GetString("empty")) && world.DoorExists(secondItemName) || world.GetItem(secondItemName).itemType == ItemType.CHEST)
            {
                if (world.DoorExists(secondItemName))
                {
                    UseKeyWithDoor(firstItem, secondItemName);
                }
                else {
                    UseKeyWithChest(firstItem, secondItemName);
                }
                
            }else if (secondItemName.Equals(resManager.rm.GetString("empty"))) // Esta usando el objeto solo
            {
                DisplayItemMessage(firstItem.name, firstItem.message);
            }
            else
            {
                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("noUseWith"), firstItemName, secondItemName));
            }

        }

        private static void UseKeyWithDoor(Key key, string doorName) 
        {
            Door door = world.GetDoor(doorName);
            // si la puerta existe y esta en la sala
            if (player.getRoom().DoorInRoom(door.id))
            {
                if (door.id == key.blueprintId)
                {
                    door.isBlocked = !door.isBlocked;
                    if (!door.isBlocked)
                    {
                        textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("opened"), door.name));
                        door.open = true;
                    }
                    else
                    {
                        textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("closed"), door.name));
                        door.open = false;
                    }
                }
                else
                {
                    textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("noUseWith"), key.name, door.name));
                }
            }
            else
            {
                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("notHere"), door.name));
            }
        }

        private static void UseKeyWithChest(Key key, string chestName)
        {
            Chest chest = (Chest)world.GetChest(chestName);

            if (player.getRoom().ItemInRoom(chest.id))
            {
                if (chest.keyId.Equals(key.id))
                {
                    chest.isBlocked = !chest.isBlocked;
                    if (!chest.isBlocked)
                    {
                        textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("unblocked"), chest.name));
                        textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("askToOpen"), chest.name));
                        engine.SetNextAction(resManager.rm.GetString("open") + " " + chest.name);
                    }
                    else
                    {
                        textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("closed"), chest.name));
                    }

                }
                else
                {
                    textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("noUseWith"), key.name, chest.name));
                }
            }
            else
            {
                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("notHere"), chest.name));
            }

        }

        private static void DisplayItemMessage(string name, string message)
        {
            if (!message.Equals(""))
            {
                textDisplayer.DisplayAction(message);
            }
            else
            {
                textDisplayer.DisplayAction(String.Format(resManager.rm.GetString("noUse"), name));
            }
        }

    }
}
