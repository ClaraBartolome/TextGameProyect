using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TextGameProyect.Componentes
{
    [XmlType("note")]
    [XmlInclude(typeof(Note))]
    [Serializable]
    public class Note
    {
        [XmlArray("to")]  
        [XmlArrayItem("email")]
        public List<string> to;
        [XmlElement(ElementName = "from")]
        public readonly string from;
        [XmlElement(ElementName = "heading")]
        public string heading;

        public Note(List<string> emails, string sender, string receiver)
        {
            this.to = emails;
            this.from = sender;
            this.heading = receiver;
        }

        private Note()
        { }
    }
}
