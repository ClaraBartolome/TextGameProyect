using Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TextGame.Componentes
{
    [XmlRoot("Blueprint")]
    [XmlInclude(typeof(Item))]
    [XmlInclude(typeof(Door))]
    [Serializable]
    public abstract class Blueprint
    {
        [XmlElement(ElementName = "id")]
        public int id;
        [XmlElement(ElementName = "name")]
        public string name;
        [XmlElement(ElementName = "description")]
        public string description;
        [XmlElement(ElementName = "useMessage")]
        public string message;
        [XmlElement(ElementName = "endgameTrigger")]
        public bool endgameTrigger;
    }


    public interface Container
    {
        public bool open { get; set; }
        public int keyId { get; set; }
        public ContainerType type { get; set; }

    }

    public interface Button
    {
        public int containerId { get; set; }
        public bool used { get; set; }
    }
}


[Serializable]
public enum ContainerType
{
    DOOR, CHEST
}

