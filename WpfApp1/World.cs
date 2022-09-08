using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Componentes;
using StringExtensions;
using TextGame.Utils;

namespace GameWorld
{
    public static class WorldData
    {
        
        //DIRECTIONS SIGUE NORTE, NORESTE, ESTE, SUDESTE, SUR, SUROESTE, OESTE, NOROESTE, ARRIBA Y ABAJO
        
        public static List<Room> rooms = XmlLoader.LoadRooms();
        public static List<Door> doors = XmlLoader.LoadDoors();
        public static List<Item> noInteractableItems = XmlLoader.LoadNoInteractableItems();
        public static List<Key> keys = XmlLoader.LoadKeys();
        public static List<Chest> chests = XmlLoader.LoadChests();
        public static List<UsableFurniture> usableFurnitures = XmlLoader.LoadInteractableItems();
        public static List<string> endGameTale = XmlLoader.LoadEndgameStory();

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
