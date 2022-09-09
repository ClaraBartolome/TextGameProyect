using Componentes;
using Literales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TextGame.Componentes;

// https://github.com/ClaraBartolome/TextGameProyect

namespace TextGame.Utils
{
    public static class XmlLoader
    {
        private static string path = @"F:\git\TextGameProyect\WpfApp1\Assets\";
        private static string extension = ".xml", underline = "_";
        private static string Keys = @"KeysFolder\Keys", Doors = @"DoorsFolder\Doors", Rooms = @"RoomsFolder\Rooms", Chests = @"ChestsFolder\Chests", NonUsableItems = @"NonUsableItemsFolder\NonUsableItems", UsableItems = @"UsableItemsFolder\UsableItems", EndGameTale= @"EndTaleFolder\EndgameTale";

        private static string LoadPath(string filename)
        {
            string auxPath = Path.Combine(new string[] { path, filename +underline +Thread.CurrentThread.CurrentCulture.Parent.ToString() + extension });
            if (File.Exists(auxPath))
            {
                return auxPath;
            }
            return Path.Combine(new string[] { path, filename + extension });
        }

        public static List<Key> LoadKeys()
        {
            string path = LoadPath(Keys);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Key>), new XmlRootAttribute("KeyList"));

            using (var reader = new StreamReader(path))
            {
                var member = (List<Key>)serializer.Deserialize(reader);
                return member;
            }

        }

        public static List<Door> LoadDoors()
        {
            string path = LoadPath(Doors);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Door>), new XmlRootAttribute("DoorList"));

            using (var reader = new StreamReader(path))
            {
                return (List<Door>)serializer.Deserialize(reader);
            }
        }

        public static List<Chest> LoadChests()
        {
            string path = LoadPath(Chests);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Chest>), new XmlRootAttribute("ChestList"));

            using (var reader = new StreamReader(path))
            {
                return (List<Chest>)serializer.Deserialize(reader);
            }
        }

        public static List<Item> LoadNoInteractableItems()
        {
            string path = LoadPath(NonUsableItems);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>), new XmlRootAttribute("NonUsableItemsList"));

            using (var reader = new StreamReader(path))
            {
                return (List<Item>)serializer.Deserialize(reader);
            }
        }

        public static List<UsableFurniture> LoadInteractableItems()
        {
            string path = LoadPath(UsableItems);
            XmlSerializer serializer = new XmlSerializer(typeof(List<UsableFurniture>), new XmlRootAttribute("UsableItemsList"));

            using (var reader = new StreamReader(path))
            {
                return (List<UsableFurniture>)serializer.Deserialize(reader);
            }
        }

        public static List<Room> LoadRooms()
        {
            string path = LoadPath(Rooms);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Room>), new XmlRootAttribute("RoomList"));

            using (var reader = new StreamReader(path))
            {
                return (List<Room>)serializer.Deserialize(reader);
            }
        }

        public static List<string> LoadEndgameStory()
        {
            string path = LoadPath(EndGameTale);
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>), new XmlRootAttribute("EndGameStoryList"));

            using (var reader = new StreamReader(path))
            {
                return (List<string>)serializer.Deserialize(reader);
            }
        }


    }
}
