﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TextGame.Componentes;

namespace Componentes
{
    [Serializable]
    public class Door :Blueprint, Container
    {
        [XmlElement(ElementName = "blocked")]
        public bool isBlocked;
        public bool open { get; set; }
        public int keyId { get; set; }
        public ContainerType type { get; set; }

        public Door(int doorId = -2, string doorName = "", string des = "", bool opened = true, bool blocked = false, int keyIdDoor = -1, ContainerType doorType = ContainerType.DOOR)
        {
            this.id = doorId;
            this.name = doorName;
            this.description = des;
            this.open = opened;
            this.isBlocked = blocked;
            this.keyId = keyIdDoor;
            this.type = doorType;
        }

        private Door() { }
    }

}

