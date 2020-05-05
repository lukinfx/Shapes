using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TvaryLib
{
    public class Cara : Tvar
    {
        
        public double x1;
        public double x2;
        public double y1;
        public double y2;
        int thickness;

        static int pocet = 0;

        public Cara() : base(TypTvaru.Cara)
        {
            Random random = new Random();
            x2 = random.Next(1, 300);
            y2 = random.Next(1, 300);
            x1 = random.Next(1, 300);
            y1 = random.Next(1, 300);
            /*leftTop = new Souradnice()
            {
                x = random.Next(1, 300),
                y = random.Next(1, 300)
            };*/
            thickness = 3;
            jmeno = "cara" + pocet;
            pocet++;

            brush = System.Windows.Media.Brushes.Black; //new SolidColorBrush(Color.FromRgb((byte)random.Next(1, 255), (byte)random.Next(1, 255), (byte)random.Next(1, 255)));
        }

        public static void UpravCaru(string jmeno, Souradnice souradnicePocatek, Souradnice souradniceKonec)
        {
            Tvary tvary = new Tvary();
            Cara cara = (Cara)tvary.NajdiTvar(jmeno);
            cara.x1 = souradnicePocatek.x;
            cara.y1 = souradnicePocatek.y;
            cara.x2 = souradniceKonec.x;
            cara.y2 = souradniceKonec.y;

        }

        public override void Vykresli(Canvas canvas)
        {
            Line myLine = new Line();
            myLine.Stroke = brush;
            myLine.X1  = x1;
            myLine.X2 = x2;
            myLine.Y1 = y1;
            myLine.Y2 = y2;
            myLine.StrokeThickness = thickness;
            canvas.Children.Add(myLine);
        }

        public override void Dialog()
        {
            Cara mojeCara = this;
            var inputCara = new DialogCara(mojeCara, "Zadej nove parametry cary");
            inputCara.ShowDialog();
        }

        public override Souradnice Rozmer ()
        {
            Souradnice rozmer = new Souradnice() { x = Math.Abs(x2 - x1) + 20 , y = Math.Abs(y2 - y1) + 20 };
            return rozmer;
        }

        public override Souradnice Pocatek()
        {
            Souradnice pocatek = new Souradnice();
            if (x1 <= x2)
            {
                pocatek.x = x1 -10;
            }
            else pocatek.x = x2 - 10;
            
            if (y1 <= y2)
            {
                pocatek.y = y1-10;
            }
            else pocatek.y = y2 - 10;

            return pocatek;
        }
        public override void ZmenSouradnice(Souradnice noveSouradnice)
        {
            x1 = x1 + noveSouradnice.x;
            x2 = x2 + noveSouradnice.x;
            y1 = y1 + noveSouradnice.y;
            y2 = y2 + noveSouradnice.y;
        }
    }
}
