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
    public static class Common
    {


        public static void LoadKeys()
        {
            string path = @"F:\git\TextGameProyect\WpfApp1\Assets\Keys.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Key>), new XmlRootAttribute("KeyList"));

            using (var reader = new StreamReader(path))
            {
               var members = (List<Key>)serializer.Deserialize(reader);
            }

        }

        public static void LoadItems()
        {
            string path = @"F:\git\TextGameProyect\WpfApp1\Assets\Items.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>), new XmlRootAttribute("ItemList"));

            using (var reader = new StreamReader(path))
            {
                var members = (List<Item>)serializer.Deserialize(reader);
            }

        }

        public static void LoadDoors()
        {
            string path = @"F:\git\TextGameProyect\WpfApp1\Assets\Doors.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Door>), new XmlRootAttribute("DoorList"));

            using (var reader = new StreamReader(path))
            {
                var members = (List<Door>)serializer.Deserialize(reader);
            }
        }

        public static void LoadRooms()
        {
            string path = @"F:\git\TextGameProyect\WpfApp1\Assets\Rooms.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Room>), new XmlRootAttribute("RoomList"));

            using (var reader = new StreamReader(path))
            {
                var members = (List<Room>)serializer.Deserialize(reader);
            }
        }

        public static void ArrayTest()
        {
            string path = @"F:\git\TextGameProyect\WpfApp1\Assets\Test.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Note>), new XmlRootAttribute("NoteList"));
            using (var reader = new StreamReader(path))
            {
                var members = (List<Note>)serializer.Deserialize(reader);
            }
        }

    }
}
