using Componentes;
using Engine;
using GameWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using TextGame.Componentes;
using TextGame.Utils;

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        TextDisplayer textDisplayer;
        GameEngine engine;

        public MainWindow()
        {
            InitializeComponent();
            World myWorld = World.GetInstance(roomsList: WorldData.rooms, doorsList: WorldData.doors, keyList: WorldData.keys, chestList: WorldData.chests, WorldData.noInteractableItems, usableFurnitures: WorldData.usableFurnitures, WorldData.endGameTale);
            textDisplayer = TextDisplayer.GetInstance(roomName, textGame, playerInput, scrollText);
            engine = GameEngine.GetInstance();
            playerInput.KeyDown += engine.playerInput_KeyDown;
            engine.Start();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void maxButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                sizeMaxButton.IsEnabled = true;
                sizeMinButton.IsEnabled = false;
                sizeMinButton.Visibility = Visibility.Collapsed;
                sizeMaxButton.Visibility = Visibility.Visible;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                sizeMaxButton.IsEnabled = false;
                sizeMinButton.IsEnabled = true;
                sizeMinButton.Visibility = Visibility.Visible;
                sizeMaxButton.Visibility = Visibility.Collapsed;
            }
        }

        private void player_mouseDown(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }


    }

}
