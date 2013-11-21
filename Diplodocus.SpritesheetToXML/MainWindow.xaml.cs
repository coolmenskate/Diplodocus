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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Diplodocus.Data;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using System.Xml;

namespace Diplodocus.SpritesheetToXML
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int nRows, nColumns;
        private string filePath;
        private List<AnimationState> states = new List<AnimationState>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void new_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileExplorer = new OpenFileDialog();
            fileExplorer.InitialDirectory = "c:\\";
            fileExplorer.Title = "Select a spritesheet image";
            fileExplorer.Filter = "Image Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png";
            if (fileExplorer.ShowDialog() == true)
            {
                spritesheet.Source = new BitmapImage(new Uri(fileExplorer.FileName));
                filePath = fileExplorer.FileName;
            }
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Xna.Framework.Point start = Microsoft.Xna.Framework.Point.Zero, end = Microsoft.Xna.Framework.Point.Zero;
            bool isFirst = false;
            int j = 0;
            List<AnimationDescription> animationsXML = new List<AnimationDescription>();
            foreach (UIElement objet in grille.Children)
            {
                if (objet is Rectangle)
                {
                    if (isFirst == false)
                    {
                        start = new Microsoft.Xna.Framework.Point((int)objet.GetValue(Grid.ColumnProperty), (int)objet.GetValue(Grid.RowProperty));
                        isFirst = true;
                    }
                    else
                    {
                        end = new Microsoft.Xna.Framework.Point((int)objet.GetValue(Grid.ColumnProperty), (int)objet.GetValue(Grid.RowProperty));
                        AnimationDescription nouvelleAnimation = new AnimationDescription(start, end, states[j], (end.X + (end.Y-1) * nColumns) - (start.X + (start.Y-1) * nColumns));
                        isFirst = false;
                        animationsXML.Add(nouvelleAnimation);
                        j++;
                    }
                }
            }

            SpriteSheetDescription sheet = new SpriteSheetDescription();
            sheet.Animations = animationsXML;
            sheet.SpriteSheetFilePath = filePath;
            sheet.Frame = new Microsoft.Xna.Framework.Rectangle(0, 0, (int)spritesheet.Source.Width / nColumns, (int)spritesheet.Source.Height / nRows);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            string directory = filePath;
            int i = filePath.Length-1;
            while(i >= 0 && filePath[i] != '.')
            {
                directory = directory.Remove(directory.Length - 1);
                i--;
            }
            directory = directory.Remove(directory.Length - 1);
            using (XmlWriter xml = XmlWriter.Create(directory+".xml", settings))
            {
                IntermediateSerializer.Serialize<SpriteSheetDescription>(xml, sheet, null);
            }

        }

        private void saveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void setGrid_Click(object sender, RoutedEventArgs e)
        {
            if (!(Int32.TryParse(textColumns.Text, out nColumns) && Int32.TryParse(textRows.Text, out nRows)))
            {
                textColumns.Text = null;
                textRows.Text = null;
                nColumns = 0;
                nRows = 0;
            }
            else
            {
                if(grille.ColumnDefinitions.Count > 0)
                    grille.ColumnDefinitions.RemoveRange(0, grille.ColumnDefinitions.Count);
                for (int i = 0; i < nColumns; i++)
                {
                    ColumnDefinition column = new ColumnDefinition();
                    column.Width = new GridLength(spritesheet.Source.Width / nColumns);
                    grille.ColumnDefinitions.Add(column);
                }
                spritesheet.SetValue(Grid.ColumnSpanProperty, nColumns);

                if (grille.RowDefinitions.Count > 0)
                    grille.RowDefinitions.RemoveRange(0, grille.RowDefinitions.Count);
                for (int i = 0; i < nRows; i++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(spritesheet.Source.Height / nRows);
                    grille.RowDefinitions.Add(row);
                }
                spritesheet.SetValue(Grid.ColumnSpanProperty, nColumns);
                spritesheet.SetValue(Grid.RowSpanProperty, nRows);
            }

        }


        private void spritesheet_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(grille);
            int columnWhereIsTheMouse = (int)(position.X / (spritesheet.Source.Width / nColumns));
            int rowWhereIsTheMouse = (int)(position.Y / (spritesheet.Source.Height / nRows));

            bool isAlreadyThere = false;
            for (int i = 0; i < grille.Children.Count && isAlreadyThere == false; i++)
            {
                int column = (int)grille.Children[i].GetValue(Grid.ColumnProperty);
                int row = (int)grille.Children[i].GetValue(Grid.RowProperty);
                
                if (grille.Children[i] is Rectangle && column == columnWhereIsTheMouse && row == rowWhereIsTheMouse)
                {
                    isAlreadyThere = true;
                    grille.Children.Remove(grille.Children[i]);
                }
            }

            if (isAlreadyThere == false)
            {
                Rectangle selection = new Rectangle();
                selection.Fill = Brushes.Firebrick;
                selection.Opacity = 0.5;
                grille.Children.Add(selection);
                grille.Children[grille.Children.Count - 1].SetValue(Grid.ColumnProperty, columnWhereIsTheMouse);
                grille.Children[grille.Children.Count - 1].SetValue(Grid.RowProperty, rowWhereIsTheMouse);
            }
            if (grille.Children.Count % 2 == 1 && grille.Children.Count > 1)
            {
                Type chooseAnimationWindow = new Type();
                chooseAnimationWindow.ShowDialog();
                states.Add(chooseAnimationWindow.StateChosen);
            }
            
        }

    }
}
