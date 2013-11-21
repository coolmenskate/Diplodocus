using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Diplodocus.Data;

namespace Diplodocus.SpritesheetToXML
{
    /// <summary>
    /// Logique d'interaction pour Type.xaml
    /// </summary>
    public partial class Type : Window
    {
        public Type()
        {
            InitializeComponent();            
        }

        public AnimationState StateChosen { get; private set; }

        private void selectionItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = (selectionItem.SelectedValue as Label).Content.ToString();
            switch ((selectionItem.SelectedValue as Label).Content.ToString())
            {
                case "Left":
                    StateChosen = AnimationState.Left;
                    break;
                case "Right":
                    StateChosen = AnimationState.Right;
                    break;
                case "Up":
                    StateChosen = AnimationState.Up;
                    break;
                case "Down":
                    StateChosen = AnimationState.Down;
                    break;
                case "Jump":
                    StateChosen = AnimationState.Jump;
                    break;
                case "Crunch":
                    StateChosen = AnimationState.Crunch;
                    break;
                case "Shoot":
                    StateChosen = AnimationState.Shoot;
                    break;
                case "Backflip":
                    StateChosen = AnimationState.Backflip;
                    break;
                case "Teleportation":
                    StateChosen = AnimationState.Teleportation;
                    break;
                case "Run to the left":
                    StateChosen = AnimationState.RunLeft;
                    break;
                case "Run to the right":
                    StateChosen = AnimationState.RunRight;
                    break;
                case "Run up":
                    StateChosen = AnimationState.RunUp;
                    break;
                case "Run down":
                    StateChosen = AnimationState.RunDown;
                    break;
                case "Death":
                    StateChosen = AnimationState.Death;
                    break;
                case "Waiting":
                    StateChosen = AnimationState.Waiting;
                    break;
            }
            this.Close();
        }
    }
}
