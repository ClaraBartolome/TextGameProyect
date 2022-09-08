using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Character;
using Componentes;
using Literales;
using StringExtensions;
using GameWorld;
using TextGame.Mechanics;
using TextGame.Componentes;
using TextGameProyect.Utils;

// https://github.com/ClaraBartolome/TextGameProyect

namespace Engine
{
   
public class GameEngine
    {
        

        private static Player player = Player.GetInstance();

        private static World world = World.GetInstance();

        private static TextDisplayer textDisplayer = TextDisplayer.GetInstance();
        private static GameResourceManager resManager = GameResourceManager.GetInstance();

        private string nextAction = "";
        public List<int> itemsToGrab = new List<int>();
        public bool engameTrigger = false;

        public void SetNextAction(string s)
        {
            nextAction = s;
        }

        private static List<string> default_commands = new List<string>
        {
            resManager.rm.GetString("use"),
            resManager.rm.GetString("open"),
            resManager.rm.GetString("look"),
            resManager.rm.GetString("close"),
            resManager.rm.GetString("exit"),
            resManager.rm.GetString("go"),
            resManager.rm.GetString("inventory"),
            resManager.rm.GetString("grab"),
            resManager.rm.GetString("drop"),
            resManager.rm.GetString("help"),
            resManager.rm.GetString("help"),
        };

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

        private Dictionary<string, Action<List<string>>> actions = new Dictionary<string, Action<List<string>>>
        {
            { resManager.rm.GetString("north"),(List<string> input) => Movement.PlayerMovement(input) },
            { resManager.rm.GetString("south"),(List<string> input) => Movement.PlayerMovement(input) },
            { resManager.rm.GetString("east"),(List<string> input) => Movement.PlayerMovement(input) },
            { resManager.rm.GetString("west"),(List<string> input) => Movement.PlayerMovement(input) },
            { resManager.rm.GetString("northeast"),(List<string> input) => Movement.PlayerMovement(input) },
            { resManager.rm.GetString("southwest"),(List<string> input) => Movement.PlayerMovement(input) },
            { resManager.rm.GetString("northwest"),(List<string> input) => Movement.PlayerMovement(input) },
            { resManager.rm.GetString("southeast"),(List<string> input) => Movement.PlayerMovement(input) },
            { resManager.rm.GetString("up"),(List<string> input) => Movement.PlayerMovement(input) },
            { resManager.rm.GetString("down"),(List<string> input) => Movement.PlayerMovement(input) },
            { resManager.rm.GetString("go to"),(List<string> input) => Movement.PlayerMovement(input) },
            { resManager.rm.GetString("go"),(List<string> input) => Movement.PlayerMovement(input) },
            { resManager.rm.GetString("use"),(List<string> input) => Use.PlayerUse(input) },
            { resManager.rm.GetString("read"),(List<string> input) => Use.PlayerUse(input) },
            { resManager.rm.GetString("grab"),(List<string> input) => Grab.PlayerGrab(input) },
            { resManager.rm.GetString("drop"),(List<string> input) => Drop.PlayerDrop(input) },
            { resManager.rm.GetString("look"),(List<string> input) => Look.PlayerLook(input) },
            { resManager.rm.GetString("open"),(List<string> input) => OpenClose.PlayerOpenClose(input) },
            { resManager.rm.GetString("close"),(List<string> input) => OpenClose.PlayerOpenClose(input) },
            { resManager.rm.GetString("help"),(List<string> input) => Help.PlayerHelp(default_commands) },
            { resManager.rm.GetString("inventory"),(List<string> input) => player.ShowInventory(textDisplayer) },
            { resManager.rm.GetString("pockets"),(List<string> input) => player.ShowInventory(textDisplayer) },
            { resManager.rm.GetString("exit"),(List<string> input) => System.Environment.Exit(0) },
        };


        private GameEngine() { }

        #region singleton impl
        private static GameEngine _instance;
        private static readonly object _lock = new object();

        public static GameEngine GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {

                    if (_instance == null)
                    {
                        _instance = new GameEngine();
                    }
                }
            }
            return _instance;
        }
        #endregion

        public void Start()
        {
            int currRoomId = 0;

            if (world.RoomExists(currRoomId)){
                player.setRoom(world.GetRoom(currRoomId));
                GameLoop();
            }
            else
            {
                textDisplayer.DisplayAction(resManager.rm.GetString("roomNotFound"));
            }
        }

        public void GameLoop()
        {
            Room room = player.getRoom();
            textDisplayer.DisplayRoomName(room.name);
            if (!room.visited && room.story.Count != 0)
            {
                foreach (var story in room.story)
                {
                    textDisplayer.DisplayAction(story);
                }
                textDisplayer.Jumpline();
            }
            textDisplayer.DisplayAction(player.getRoom().description);
             if (player.getRoom().endgameTrigger)
            {
                Endgame();
            } 
        }

        public void Endgame()
        {
            List<string> text = world.GetEndStory();
            if (text.Count != 0)
            {
                foreach (var page in text)
                {
                    textDisplayer.DisplayAction(page);
                }
            }
            else
            {
                textDisplayer.DisplayAction(resManager.rm.GetString("endgame_text"));
            }
            engameTrigger = true;
        }

        public void playerInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && !engameTrigger)
            {
                    textDisplayer.DisplayInput();
                    if (nextAction.Equals("") && itemsToGrab.Count.Equals(0))
                    {
                        Response(textDisplayer.GetPlayerInput());
                    }
                    else
                    {
                        BinaryResponse(textDisplayer.GetPlayerInput());
                    }
                    textDisplayer.ClearTextBox();
                
            }
        }

        private void BinaryResponse(string response)
        {
            if (response.RemoveAccent().ToLower().Equals(resManager.rm.GetString("yes")))
            {
                if (itemsToGrab.Count > 0)
                {
                    foreach (int i in itemsToGrab)
                    {
                        if (world.ItemExists(i))
                        {
                            nextAction = resManager.rm.GetString("grab") + " " + world.GetItem(i).name;
                            Response(nextAction);
                        }
                    }
                    itemsToGrab.Clear();
                }
                else
                {
                    Response(nextAction);
                }
            } else if (response.RemoveAccent().ToLower().Equals(resManager.rm.GetString("no")))
            {
            }
            else
            {
                textDisplayer.DisplayAction(resManager.rm.GetString("noUnderstand"));
            }
            nextAction = "";
            
        }

        private void Response(string s)
        {
            String response = s;
            List<string> input = response.Split(' ').ToList();
            string actionChosen = input[0].RemoveAccent().ToLower();
            input.RemoveAt(0);

            if (actions.ContainsKey(actionChosen) && !(actionChosen.Equals(resManager.rm.GetString("open")) || actionChosen.Equals(resManager.rm.GetString("close"))) && !default_directions.Contains(actionChosen))
            {
                actions[actionChosen].Invoke(input);
            }else if(actionChosen.Equals(resManager.rm.GetString("open")) || actionChosen.Equals(resManager.rm.GetString("close")))
            {
                //PEGAR ACTION
                input.Insert(0, actionChosen);
                actions[actionChosen].Invoke(input);
            }else if (default_directions.Contains(actionChosen))
            {
                actions[actionChosen].Invoke(new List<string> { actionChosen});
            }
            else
            {
                textDisplayer.DisplayAction("No puedes hacer eso");
            }
        }
    }

    
}







