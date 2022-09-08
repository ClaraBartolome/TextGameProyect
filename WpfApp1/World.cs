using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Componentes;
using StringExtensions;

namespace GameWorld
{
    public static class WorldData
    {
        //DIRECTIONS SIGUE NESO
        public static List<Room> rooms = new List<Room>
        {
            new Room(0, "Habitacion principal", "Es una habitación normal, al norte ves una puerta y en el suelo una llave.",  new List<string>
            {
                "Despiertas en una habitación, al fondo de la sala, ves una puerta."
            }, roomItems: new List<int>{ 0, 1, 2, 4 }, roomDirections: new List<int>{1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }, roomDoors: new List<int>{ -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }),
            new Room(1,"Habitacion final", "", end: true)
        };

        public static List<Item> notes = new List<Item>
        {
            new Item(itemId: 3, itemName: "nota", desc: "Esta es una nota y al leerla te cuenta cosas", useMessage: "Informacion importante para el jugador", type: ItemType.NOTE),
        };

        public static List<Key> keys = new List<Key>
        {
            new Key(keyId: 0, keyName:"Llave de la puerta de la habitación", desc: "Llave que abre la puerta de la habitación", openDoor: 0),
            new Key(keyId: 1, keyName:"Llave", desc: "Es una llave que abre un cofre", useMessage: "No ves que sentido tiene usar una llave en el aire", openDoor: 2),
        };

        public static List<Chest> chests = new List<Chest>
        {
            new Chest(chestId: 2, chestName: "Cofre", des: "Es un cofre de prueba", opened: false, blocked: true, keyIdDoor: 1, itemsId: new List<int> {3})
        };

        public static List<UsableFurniture> usableFurnitures = new List<UsableFurniture>
        {
            new UsableFurniture(id: 4, furName: "Botón", des: "Es un boton, parece que se puede pulsar", useMessage: "Has pulsado el boton, se oye un ruido a lo lejos", itemType: ItemType.USABLE_FURNITURE, containerId: 2, isUsed: false)
        };

        public static List<Door> doors = new List<Door>
        {
            new Door(doorId: 0, doorName: "puerta de la habitación", des:"Puerta cerrada, parece que necesitas una llave para abrirla", opened: false, blocked: true, keyIdDoor: 0),
            new Door(doorId: 1, doorName: "puerta del puente de mando", opened: false, blocked: false),
        };

        public static List<string> endGameTale = new List<string>
            {
                "Tras mucho esfuerzo, llegas al final."
            };
    }

    public sealed class World
    {
        private World(List<Room> roomsList = null, List<Door> doorsList = null, List<Key> keyList = null, List<Chest> chestList = null, List<Item> notesList = null, List<UsableFurniture> usableFurnitures = null, List<string> endgameStory = null) 
        {
            this.Rooms = roomsList ?? new List<Room>();
            this.Doors = doorsList ?? new List<Door>();
            this.Keys = keyList ?? new List<Key>();
            this.Chests = chestList ?? new List<Chest>();
            this.ItemsNoInteractables = notesList ?? new List<Item>();
            this.UsableFurnitures = usableFurnitures ?? new List<UsableFurniture>();
            this.EndgameStory = endgameStory ?? new List<string>();
            //ITEMS TIENE QUE TENERLAS TODOS LOS ITEMS PORQUE ES AL QUE VAMOS A IR A COMPROBAR SI EXISTEN
            this.Items = new List<Item>();
            this.Items.AddRange(this.Keys);
            this.Items.AddRange(this.Chests);
            this.Items.AddRange(this.ItemsNoInteractables);
            this.Items.AddRange(this.UsableFurnitures);
        }

        private List<Room> Rooms { get; set; }
        private List<Item> Items { get; set; }
        private List<Door> Doors { get; set; }
        private List<Key> Keys { get; set; }
        private List<Chest> Chests { get; set; }
        private List<Item> ItemsNoInteractables { get; set; }
        private List<UsableFurniture> UsableFurnitures { get; set; }

        private List<string> EndgameStory { get; set; }



        #region singleton impl
        private static World _instance;
        private static readonly object _lock = new object();

        public static World GetInstance(List<Room> roomsList = null, List<Door> doorsList = null, List<Key> keyList = null, List<Chest> chestList = null, List<Item> notesList = null, List < UsableFurniture > usableFurnitures = null, List<string> endgameStory = null)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    
                    if (_instance == null)
                    {
                        _instance = new World(roomsList, doorsList, keyList, chestList, notesList, usableFurnitures, endgameStory);
                    }
                }
            }
            return _instance;
        }
        #endregion

        public Item GetItem(int itemId)
        {
            if (Items.Find(i => i.id == itemId) != null)
            {
                return Items.Find(i => i.id == itemId);
            }
            return new Item();
        }

        public Item GetItem(string itemName)
        {
            if (ItemExists(itemName))
            {
                if (Items.Find(i => i.name.RemoveAccent().ToLower().Equals(itemName)) != null)
                {
                    return Items.Find(i => i.name.RemoveAccent().ToLower().Equals(itemName));
                }
            }
            return new Item();
        }


        public Door GetDoor(int doorId)
        {
            if (Doors.Find(d => d.id == doorId) != null)
            {
                return this.Doors.Find(d => d.id == doorId);
            }
            return new Door();
        }

        public Door GetDoor(string doorName)
        {
            if (DoorExists(doorName))
            {
                return this.Doors.Find(d => d.name.RemoveAccent().ToLower().Equals(doorName));
            }
            return new Door();
        }

        public Room GetRoom(int roomId)
        {
            if (Rooms.Find(r => r.id == roomId) != null)
            {
                return Rooms.Find(r => r.id == roomId);
            }
            return new Room();
        }

        public bool RemoveItem(int roomId, int itemId)
        {
            Room r = GetRoom(roomId);
            return r.RemoveItem(itemId);
        }

        public bool RoomExists(int roomId)
        {
            if (Rooms.Find(r => r.id == roomId) != null)
            {
                return true;
            }
            return false;
        }

        public bool DoorExists(int doorId)
        {
            if (Doors.Find(d => d.id == doorId) != null)
            {
                return true;
            }
            return false;
        }

        public bool DoorExists(string doorName)
        {
            if (Doors.Find(d => d.name.RemoveAccent().ToLower().Equals(doorName)) != null)
            {
                return true;
            }
            return false;
        }

        public bool ItemExists(int itemId)
        {
            if (Items.Find(i => i.id == itemId) != null)
            {
                return true;
            }
            return false;
        }

        public bool ItemExists(string itemName)
        {
            if (Items.Find(i => i.name.RemoveAccent().ToLower().Equals(itemName)) != null)
            {
                return true;
            }
            return false;
        }

        public Key GetKey(int itemId)
        {
            if (Keys.Find(i => i.id == itemId) != null)
            {
                return Keys.Find(i => i.id == itemId);
            }
            return new Key();
        }

        public Key GetKey(string itemName)
        {
            if (ItemExists(itemName))
            {
                return Keys.Find(i => i.name.RemoveAccent().ToLower().Equals(itemName));
            }
            return new Key();

        }

        public Chest GetChest(int itemId)
        {
            if (Chests.Find(i => i.id == itemId) != null)
            {
                return Chests.Find(i => i.id == itemId);
            }
            return new Chest();
        }

        public Chest GetChest(string itemName)
        {
            if (Chests.Find(i => i.name.RemoveAccent().ToLower().Equals(itemName)) != null)
            {
                return Chests.Find(i => i.name.RemoveAccent().ToLower().Equals(itemName));
            }
            return new Chest();

        }

        public UsableFurniture GetUsFurniture(int itemId)
        {
            if (UsableFurnitures.Find(i => i.id == itemId) != null)
            {
                return UsableFurnitures.Find(i => i.id == itemId);
            }
            return new UsableFurniture();
        }

        public UsableFurniture GetUsFurniture(string itemName)
        {
            if (UsableFurnitures.Find(i => i.name.RemoveAccent().ToLower().Equals(itemName)) != null)
            {
                return UsableFurnitures.Find(i => i.name.RemoveAccent().ToLower().Equals(itemName));
            }
            return new UsableFurniture();

        }

        public List<string> GetEndStory()
        {
            return EndgameStory;
        }
    }

}
