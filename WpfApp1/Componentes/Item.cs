using Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TextGame.Componentes;

namespace Componentes
{
    [XmlInclude(typeof(Key))]
    [Serializable]
    public class Item: Blueprint
    {
        [XmlElement(ElementName = "type")]
        internal ItemType itemType;

        public Item(int itemId = -2, string itemName = "", string desc = "", string useMessage = "", ItemType type = ItemType.DEFAULT, bool end = false)
        {
            this.id = itemId;
            this.name = itemName;
            this.description = desc;
            this.message = useMessage;
            this.itemType = ItemType.DEFAULT;
            this.endgameTrigger = end;
        }

        public Item()
        {}
    }

    [Serializable]
    public class Key: Item
    {
        public int blueprintId;
        public Key(int keyId = -2, string keyName = "", string desc = "", string useMessage = "", int openDoor = -2)
        {           
            this.id = keyId;
            this.name = keyName;
            this.description = desc;
            this.message = useMessage;
            this.blueprintId = openDoor;
            this.itemType = ItemType.KEY;
        }
        public Key() { }
    }

    [Serializable]
    public class UsableFurniture : Item, Button
    {
        public int containerId { get; set; }
        public bool used { get; set; }

        public UsableFurniture() { }

        public UsableFurniture(int id = -2, string furName = "", string des = "", string useMessage = "", ItemType itemType = ItemType.USABLE_FURNITURE, int containerId = -2, bool isUsed = false, bool end = false) 
        {
            this.id = id;
            this.name = furName;
            this.description = des;
            this.message = useMessage;
            this.itemType = itemType;
            this.containerId = containerId;
            this.used = isUsed;
            this.endgameTrigger = end;
        }
    }


    public class Chest : Item, Container
    {
        [XmlElement(ElementName = "blocked")]
        public bool isBlocked;
        public bool open { get; set; }
        public int keyId { get; set; }
        public ContainerType type { get; set; }
        public List<int> itemsInside;

        public Chest(int chestId = -2, string chestName = "", string des = "", string useMessage = "", ItemType itemType = ItemType.CHEST, bool opened = true, bool blocked = false, int keyIdDoor = -1, ContainerType doorType = ContainerType.CHEST, List<int> itemsId = null, bool end = false)
        {
            this.id = chestId;
            this.name = chestName;
            this.description = des;
            this.message = useMessage;
            this.itemType = itemType;
            this.open = opened;
            this.isBlocked = blocked;
            this.keyId = keyIdDoor;
            this.type = doorType;
            this.itemsInside = itemsId ?? new List<int>();
            this.endgameTrigger = end;
        }

        private Chest() { }
    }

}

public enum ItemType
{
    DEFAULT, KEY, FURNITURE, NOTE, CHEST, USABLE_FURNITURE
}
