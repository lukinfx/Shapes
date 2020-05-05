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
using TvaryLib;

namespace screensaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Tvary tvary = new Tvary();
        public Souradnice move1 = new Souradnice ();
        public Souradnice move2 = new Souradnice();

        public Souradnice mouseMove = new Souradnice();
        string jmeno;
        bool mouseDown;
        

        public MainWindow()
        {

        }

        private void ButtonCara_Click(object sender, RoutedEventArgs e)
        {
            Cara cara = new Cara();
            tvary.Pridej(cara);
            tvary.VykresliCanvas(canvas1);
            tvary.VykresliListBoxJmena(listBoxJmena);
            tvary.VratTvar(cara.jmeno);
        }

        private void ButtonSmaz_Click(object sender, RoutedEventArgs e)
        {
            tvary.Smaz();
            tvary.VykresliCanvas(canvas1);
            tvary.VykresliListBoxJmena(listBoxJmena);
        }

        private void ButtonObdelnik_Click(object sender, RoutedEventArgs e)
        {
            Obdelnik obdelnik = new Obdelnik();
            tvary.Pridej(obdelnik);
            tvary.VykresliCanvas(canvas1);
            tvary.VykresliListBoxJmena(listBoxJmena);
        }

        private void listBoxJmena_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tvary.ZrusOznaceni();
            
            if (listBoxJmena.SelectedItem == null)
                jmeno = "";
            else
            {
                jmeno = listBoxJmena.SelectedItem.ToString();
                //oznacTento.jeOznaceny = true;
                foreach (string oznac in listBoxJmena.SelectedItems)
                {
                    
                    Tvar oznacTento = tvary.NajdiTvar(oznac);
                    oznacTento.jeOznaceny = true;
                }
                tvary.VykresliCanvas(canvas1);
                //tvary.Ohraniceni(canvas1);
            }

            

        }
        private void ButtonKruh_Click(object sender, RoutedEventArgs e)
        {
            Kruh kruh = new Kruh();
            tvary.Pridej(kruh);
            tvary.VykresliCanvas(canvas1);
            tvary.VykresliListBoxJmena(listBoxJmena);

        }

        private void ButtonJmeno_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxJmena.SelectedItems.Count == 1)
            {
                var inputJmeno = new dialogJmeno("Jak pojmenujes svuj objekt?", "");
                if (inputJmeno.ShowDialog() == true)
                {
                    string noveJmeno = inputJmeno.Answer.ToString();
                    tvary.ZmenJmeno(jmeno, noveJmeno);
                    tvary.VykresliListBoxJmena(listBoxJmena);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Tuto funkci lze spustit pouze s jednou oznacenou polozkou", "Uprav jmeno", MessageBoxButtons.OK);
            }
        }

        private void ButtonSouradnice_Click(object sender, RoutedEventArgs e)
        {
            
            Tvar mujTvar = tvary.NajdiTvar(jmeno);
            mujTvar.Dialog();
            tvary.VykresliCanvas(canvas1);
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
                        tvary.ZmenBarvu(jmeno, barva);
                        tvary.VykresliCanvas(canvas1);
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
            tvary.SmazVse(canvas1);
            tvary.VykresliCanvas(canvas1);
            tvary.VykresliListBoxJmena(listBoxJmena);
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
                tvary.ZmenSouradnice2(move2);
                tvary.VykresliCanvas(canvas1);
                mouseMove.x = p.X;
                mouseMove.y = p.Y;
            }
        }

        private void canvas1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);
            mouseMove.x = p.X;
            mouseMove.y = p.Y;
            tvary.PoznejTvar(mouseMove);
            tvary.VykresliCanvas(canvas1);
        }
    }
}
