using Character;
using Componentes;
using GameWorld;
using Literales;
using StringExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGame.Componentes;
using TextGameProyect.Utils;

namespace TextGame.Mechanics
{
    public static class Movement
    {
        private static World world = World.GetInstance();
        private static TextDisplayer textDisplayer = TextDisplayer.GetInstance();
        private static Player player = Player.GetInstance();
        private static GameResourceManager resManager = GameResourceManager.GetInstance();
        private static List<string> default_directions = new List<string>
        {
            resManager.rm.GetString("north"),
            resManager.rm.GetString("northeast"),
            resManager.rm.GetString("east"),
            resManager.rm.GetString("southeast"),
            resManager.rm.GetString("south"),
            resManager.rm.GetString("southwest"),
            resManager.rm.GetString("west"),
            resManager.rm.GetString("northwest"),
            resManager.rm.GetString("up"),
            resManager.rm.GetString("down"),
        };

        //NECESITO QUE AVISE AL MOTOR CUANDO CAMBIE DE HABITACION
        public static void PlayerMovement(List<string> input)
        {
            string direction = input.Last().RemoveAccent().ToLower();

            if (default_directions.Contains(direction))
            {
                int directionId = default_directions.FindIndex(d => d.Equals(direction));
                if (CheckPath(directionId, player))
                {
                    if (IsPathBlocked(directionId, player))
                    {
                        MoveToRoom(player.getRoom().directions[directionId], player);
                    }
                    else
                    {
                        textDisplayer.DisplayAction(resManager.rm.GetString("pathBlocked"));
                    }
                }
                else
                {
                    textDisplayer.DisplayAction(resManager.rm.GetString("noPath"));
                }
            }
            else
            {
                textDisplayer.DisplayAction(resManager.rm.GetString("noPath"));
            }
        }

        private static Boolean CheckPath(int direction, Player player)
        {

            return player.getRoom().directions[direction] != -1;
        }

        private static Boolean IsPathBlocked(int direction, Player player)
        {
            if (player.getRoom().doors[direction] != -1)
            {
                Door door = world.GetDoor(player.getRoom().doors[direction]);
                return door.open;
            }
            return true;

        }

        private static Boolean MoveToRoom(int roomId, Player player)
        {
            Room auxRoom = world.GetRoom(roomId);
            if (auxRoom.id != -1)
            {
                player.setRoom(auxRoom);
                return true;
            }
            return false;
        }
    }
}
