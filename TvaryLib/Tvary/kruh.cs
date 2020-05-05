using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TvaryLib
{
    public class Kruh : Tvar
    {
        int thickness;
        double width;
        public double height;
        static int pocet = 0;


        public Kruh(): base(TypTvaru.Kruh)
        {
            Random random = new Random();
            thickness = 2;
            width = random.Next(20, 200);
            height = width;
            leftTop = new Souradnice()
            {
                x = random.Next(1, 300),
                y = random.Next(1, 300)
            };
            brush = System.Windows.Media.Brushes.Black;
            jmeno = "kruh" + pocet;
            pocet++;
        }

        public override void Vykresli(Canvas canvas)
        {
            Ellipse myElipse = new Ellipse();
            myElipse.Width = width;
            myElipse.Height = height;
            myElipse.Fill = brush;
            myElipse.Margin = new Thickness(leftTop.x, leftTop.y, 0, 0);
            canvas.Children.Add(myElipse);
        }

        public void UpravKruh(Souradnice souradnice, double R)
        {
            {
                height = R*2;
                width = height;
                leftTop.x = souradnice.x - R;
                leftTop.y = souradnice.y - R;
            }
        }

        public override void Dialog()
        {
            Kruh mujKruh = this;
            var inputKruh = new DialogKruh(mujKruh, "zadej nove parametry kruhu:");
            inputKruh.ShowDialog();
        }

        public override Souradnice Rozmer()
        {
            Souradnice rozmer = new Souradnice() { x = width + 20, y = height + 20 };
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
