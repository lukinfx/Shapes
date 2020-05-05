using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShapesLib;

namespace screensaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Tvary tvary = new Tvary();
        public Coordinates move1 = new Coordinates ();
        public Coordinates move2 = new Coordinates();

        public Coordinates mouseMove = new Coordinates();
        string jmeno;
        bool mouseDown;
        

        public MainWindow()
        {

        }

        private void ButtonCara_Click(object sender, RoutedEventArgs e)
        {
            ShapesLib.Line cara = new ShapesLib.Line();
            tvary.AddShape(cara);
            tvary.PaintCanvas(canvas1);
            tvary.UpdateTheListBox(listBoxJmena);
        }

        private void ButtonSmaz_Click(object sender, RoutedEventArgs e)
        {
            tvary.DeleteShape();
            tvary.PaintCanvas(canvas1);
            tvary.UpdateTheListBox(listBoxJmena);
        }

        private void ButtonObdelnik_Click(object sender, RoutedEventArgs e)
        {
            ShapesLib.Rectangle obdelnik = new ShapesLib.Rectangle();
            tvary.AddShape(obdelnik);
            tvary.PaintCanvas(canvas1);
            tvary.UpdateTheListBox(listBoxJmena);
        }

        private void listBoxJmena_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tvary.UnselectAll();
            
            if (listBoxJmena.SelectedItem == null)
                jmeno = "";
            else
            {
                jmeno = listBoxJmena.SelectedItem.ToString();
                //oznacTento.jeOznaceny = true;
                foreach (string oznac in listBoxJmena.SelectedItems)
                {

                    ShapesLib.Shape oznacTento = tvary.returnShape(oznac);
                    oznacTento.isSelected = true;
                }
                tvary.PaintCanvas(canvas1);
                //tvary.Ohraniceni(canvas1);
            }

            

        }
        private void ButtonKruh_Click(object sender, RoutedEventArgs e)
        {
            Circle kruh = new Circle();
            tvary.AddShape(kruh);
            tvary.PaintCanvas(canvas1);
            tvary.UpdateTheListBox(listBoxJmena);

        }

        private void ButtonJmeno_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxJmena.SelectedItems.Count == 1)
            {
                var inputJmeno = new dialogJmeno("Jak pojmenujes svuj objekt?", "");
                if (inputJmeno.ShowDialog() == true)
                {
                    string noveJmeno = inputJmeno.Answer.ToString();
                    tvary.ChangeName(jmeno, noveJmeno);
                    tvary.UpdateTheListBox(listBoxJmena);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Tuto funkci lze spustit pouze s jednou oznacenou polozkou", "Uprav jmeno", MessageBoxButtons.OK);
            }
        }

        private void ButtonSouradnice_Click(object sender, RoutedEventArgs e)
        {

            ShapesLib.Shape mujTvar = tvary.returnShape(jmeno);
            mujTvar.Dialog();
            tvary.PaintCanvas(canvas1);
        }


        private void ButtonBarva_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxJmena.SelectedItems.Count == 1)
            {
                var inputBarva = new dialogBarva("Zadej barvu","");
                if (inputBarva.ShowDialog() == true)
                {
                    RGB barva = inputBarva.Answer;
                    if (barva != null)
                    {
                        tvary.ChangeColor(jmeno, barva);
                        tvary.PaintCanvas(canvas1);
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Tuto funkci lze spustit pouze s jednou oznacenou polozkou", "Uprav barvu", MessageBoxButtons.OK);
            }
        }

        private void ButtonSmazVse_Click(object sender, RoutedEventArgs e)
        {
            tvary.DeleteAll(canvas1);
            tvary.PaintCanvas(canvas1);
            tvary.UpdateTheListBox(listBoxJmena);
        }

        private void CreateSaveBitmap(Canvas canvas, string filename)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            // needed otherwise the image output is black
            canvas.Measure(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight));
            //canvas.Arrange(new Rect(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight)));

            renderBitmap.Render(canvas);

            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream file = File.Create(filename))
            {
                encoder.Save(file);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.

            // Update the text box color if the user clicks OK 
            MyDialog.ShowDialog();*/

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "PNG file format|*.png";
            if (saveFileDialog.ShowDialog() == true)
                CreateSaveBitmap(canvas1, saveFileDialog.FileName); 

            //CreateSaveBitmap(canvas1, @"C:\temp\out.png");
        }

        private void canvas1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDown = true;
            Point p = e.GetPosition(this);
            mouseMove.x = p.X;
            mouseMove.y = p.Y;
            System.Diagnostics.Debug.WriteLine($"MouseDown : x={p.X} y={p.Y}");
            
        }

        private void canvas1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
            
            if (Mouse.LeftButton == MouseButtonState.Pressed) 
            {
                Point p = e.GetPosition(this);
                move2.x = p.X - mouseMove.x;
                move2.y = p.Y - mouseMove.y;
                tvary.DragChangeCoordinates(move2);
                tvary.PaintCanvas(canvas1);
                mouseMove.x = p.X;
                mouseMove.y = p.Y;
            }
        }

        private void canvas1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);
            mouseMove.x = p.X;
            mouseMove.y = p.Y;
            tvary.MouseClickRecogniseShape(mouseMove);
            tvary.PaintCanvas(canvas1);
        }
    }
}
