using Componentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGame.Componentes;
using TextGameProyect.Utils;

namespace Character
{
    public class Player
    {
        private static GameResourceManager resManager = GameResourceManager.GetInstance();
        public String name;
        private Room currentRoom;
        //TODO lista de objetos
        public List<Item> inventory;

        private Player() {
            this.currentRoom = new Room();
            this.inventory = new List<Item>();
        }

        #region singleton impl
        private static Player _instance;
        private static readonly object _lock = new object();

        public static Player GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {

                    if (_instance == null)
                    {
                        _instance = new Player();
                    }
                }
            }
            return _instance;
        }
        #endregion


        public void ShowInventory(TextDisplayer textDisplayer)
        {
            textDisplayer.DisplayAction(resManager.rm.GetString("itemsInInventory"));
            foreach (var item in inventory)
            {
                textDisplayer.DisplayItem(item.name);
            }
        }

        public bool InInventory(Item i)
        {
            foreach (var item in inventory)
            {
                if (item.id.Equals(i.id))
                {
                    return true;
                }
            }
            return false;
        }

        public void DropItem(Item i)
        {
            this.inventory.Remove(i);
        }

        public void setRoom(Room r)
        {
            this.currentRoom = r;
        }

        public Room getRoom()
        {
            return currentRoom;
        }
    }
}
