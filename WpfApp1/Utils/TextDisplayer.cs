using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// https://github.com/ClaraBartolome/TextGameProyect

namespace TextGame.Componentes
{
    public class TextDisplayer
    {
        private const string  empty = "", openBracket = "[", closeBracket = "]", space = " ", moreThan = ">";
        private TextBlock textGame;
        private TextBlock roomNameBlock;
        private TextBox playerInput;
        private ScrollViewer scrolViewer;

        private TextDisplayer(TextBlock textRoom, TextBlock textGame, TextBox playerInput, ScrollViewer scrollList)
        {
            this.roomNameBlock = textRoom;
            this.textGame = textGame;
            this.playerInput = playerInput;
            this.scrolViewer = scrollList;
        }

        #region singleton impl
        private static TextDisplayer _instance;
        private static readonly object _lock = new object();

        public static TextDisplayer GetInstance(TextBlock textRoom = null, TextBlock textGame = null, TextBox playerInput = null, ScrollViewer scrollList = null)
        {
            if (_instance == null)
            {
                lock (_lock)
                {

                    if (_instance == null)
                    {
                        _instance = new TextDisplayer(textRoom, textGame, playerInput, scrollList);
                    }
                }
            }
            return _instance;
        }
        #endregion

        public void Jumpline()
        {
            textGame.Text = textGame.Text + "\n";
        }

        public void DisplayRoomName(string s)
        {
            roomNameBlock.Text = s;
        }

        public void DisplayAction(string text = empty, string param1 = empty, string param2 = empty) {            
            Jumpline();            
            textGame.Text = textGame.Text + text + ParseParam(param1) + ParseParam(param2);
            scrolViewer.ScrollToBottom();
        }


        private string ParseParam(string s)
        {
            if (!s.Equals(empty))
            {
                return openBracket + s + closeBracket;
            }
            return s;
        }

        public string GetPlayerInput()
        {
            return playerInput.Text;
        }

        public void DisplayItem(string s)
        {
            Jumpline();
            textGame.Text = textGame.Text + ParseParam(s);
            scrolViewer.ScrollToBottom();          
        }


        public void ClearTextBox()
        {
            playerInput.Text = "";
        }

        public void DisplayInput()
        {
            Jumpline();
            Jumpline();
            textGame.Text = textGame.Text + moreThan + playerInput.Text;
            scrolViewer.ScrollToBottom();
        }

    }
}
