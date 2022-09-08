using Componentes;
using Literales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TextGame.Componentes;
using TextGameProyect.Componentes;

namespace TextGame.Utils
{
    public static class XmlLoader
    {

        public static List<Key> LoadKeys()
        {
            string path = @"F:\git\TextGameProyect\WpfApp1\Assets\Keys.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Key>), new XmlRootAttribute("KeyList"));

            using (var reader = new StreamReader(path))
            {
               return (List<Key>)serializer.Deserialize(reader);
            }

        }

        public static List<Door> LoadDoors()
        {
            string path = @"F:\git\TextGameProyect\WpfApp1\Assets\Doors.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Door>), new XmlRootAttribute("DoorList"));

            using (var reader = new StreamReader(path))
            {
                return (List<Door>)serializer.Deserialize(reader);
            }
        }

        public static List<Chest> LoadChests()
        {
            string path = @"F:\git\TextGameProyect\WpfApp1\Assets\Chests.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Chest>), new XmlRootAttribute("ChestList"));

            using (var reader = new StreamReader(path))
            {
                return (List<Chest>)serializer.Deserialize(reader);
            }
        }

        public static List<Item> LoadNoInteractableItems()
        {
            string path = @"F:\git\TextGameProyect\WpfApp1\Assets\NonUsableItems.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>), new XmlRootAttribute("NonUsableItemsList"));

            using (var reader = new StreamReader(path))
            {
                return (List<Item>)serializer.Deserialize(reader);
            }
        }

        public static List<UsableFurniture> LoadInteractableItems()
        {
            string path = @"F:\git\TextGameProyect\WpfApp1\Assets\UsableItems.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<UsableFurniture>), new XmlRootAttribute("UsableItemsList"));

            using (var reader = new StreamReader(path))
            {
                return (List<UsableFurniture>)serializer.Deserialize(reader);
            }
        }

        public static List<Room> LoadRooms()
        {
            string path = @"F:\git\TextGameProyect\WpfApp1\Assets\Rooms.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Room>), new XmlRootAttribute("RoomList"));

            using (var reader = new StreamReader(path))
            {
                return (List<Room>)serializer.Deserialize(reader);
            }
        }

        public static List<string> LoadEndgameStory()
        {
            string path = @"F:\git\TextGameProyect\WpfApp1\Assets\EndgameTale.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>), new XmlRootAttribute("EndGameStoryList"));

            using (var reader = new StreamReader(path))
            {
                return (List<string>)serializer.Deserialize(reader);
            }
        }


    }
}
