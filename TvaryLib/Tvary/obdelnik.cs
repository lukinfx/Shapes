using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TvaryLib
{
    public class Obdelnik : Tvar
    {
        
        public double width;
        public double height;
        static int pocet = 0;
        public Obdelnik () : base(TypTvaru.Obdelnik)
        {
            Random random = new Random();
            leftTop = new Souradnice()
            {
                x = random.Next(1, 300),
                y = random.Next(1, 300)
            };
            height = random.Next(20, 200);
            width = random.Next(20, 200);
            brush = new SolidColorBrush(Color.FromRgb((byte)random.Next(1, 255), (byte)random.Next(1, 255), (byte)random.Next(1, 255)));
            jmeno = "obdelnik" + pocet;
            pocet++;
        }

        public void Uprav(Souradnice souradnice, double height, double width)
        {
            leftTop.x = souradnice.x;
            leftTop.y = souradnice.y;
            this.height = height;
            this.width = width;
        }

        public override void Vykresli(Canvas canvas)
        {
            Rectangle myRectangle = new Rectangle();
            myRectangle.Width = width;
            myRectangle.Height = height;
            myRectangle.Fill = brush;
            myRectangle.Margin = new Thickness(leftTop.x, leftTop.y, 0, 0);
            canvas.Children.Add(myRectangle);
        }

        public override void Dialog()
        {
            Obdelnik obdelnik = this;
            var inputObdelnik = new DialogObdelnik(obdelnik, "zadej nove parametry obdelniku:");
            inputObdelnik.ShowDialog();
        }
        public override Souradnice Rozmer()
        {
            Souradnice rozmer = new Souradnice() { x=width + 20, y = height+20};
            return rozmer;
        }

        public override Souradnice Pocatek()
        {
            Souradnice pocatek = new Souradnice();
            pocatek.x = leftTop.x - 10;
            pocatek.y = leftTop.y - 10;
            return pocatek;
        }
    }
}
