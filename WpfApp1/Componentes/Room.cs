using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Literales;
using TextGame.Componentes;

namespace Componentes
{
    [XmlType("room")]
    [XmlInclude(typeof(Room))]
    [Serializable]
    public class Room: Blueprint
    {
        [XmlArray(ElementName = "story")]
        [XmlArrayItem("page")]
        public List<string> story;
        [XmlArray(ElementName = "directions")]
        [XmlArrayItem("direction")]
        public List<int> directions;
        [XmlArray(ElementName = "items")]
        [XmlArrayItem(ElementName = "itemId")]
        public List<int> items = new List<int>();
        [XmlArray(ElementName = "doors")]
        [XmlArrayItem(ElementName = "door")]
        public List<int> doors;
        [XmlElement(ElementName = "endgame")]
        public bool isEndGame = false;
        [XmlElement(ElementName = "visited")]
        public bool visited;

        public Room(int roomId = -1,string roomName = "", string roomDesc = "", List<string> roomStory = null, List<int> roomDirections = null, List<int> roomItems = null , List<int> roomDoors = null, bool end = false, bool visit = false)
        {
            this.id = roomId;
            this.name = roomName;
            this.description = roomDesc;
            this.story = roomStory ?? new List<string>();
            this.directions = roomDirections ??  literales.dir_default.ToList<int>();
            this.items = roomItems ?? new List<int>();
            this.doors = roomDoors ?? literales.dir_default.ToList<int>();
            this.isEndGame = end;
            this.visited = visit;
        }

        private Room() { }

        public bool ItemInRoom(int itemId)
        {
            if (items.Contains(itemId))
            {
                return true;
            }
            return false;
        }

        public bool DoorInRoom(int doorId)
        {
            foreach (var d in doors)
            {
                if (d.Equals(doorId))
                {
                    return true;
                }
            }
            return false;
        }

        public bool RemoveItem(int itemId)
        {
            if (ItemInRoom(itemId))
            {
                items.Remove(itemId);
            }
            return false;
        }

    }
}
