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

namespace ShapesLib
{
    public class Circle : Shape
    {
        int thickness;
        double width;
        public double height;
        static int pocet = 0;


        public Circle(): base(ShapeType.Circle)
        {
            Random random = new Random();
            thickness = 2;
            width = random.Next(20, 200);
            height = width;
            leftTop = new Coordinates()
            {
                x = random.Next(1, 300),
                y = random.Next(1, 300)
            };
            brush = System.Windows.Media.Brushes.Black;
            name = "kruh" + pocet;
            pocet++;
        }

        public override void PaintShape(Canvas canvas)
        {
            Ellipse myElipse = new Ellipse();
            myElipse.Width = width;
            myElipse.Height = height;
            myElipse.Fill = brush;
            myElipse.Margin = new Thickness(leftTop.x, leftTop.y, 0, 0);
            canvas.Children.Add(myElipse);
        }

        public void ChangeCircle(Coordinates coordinates, double R)
        {
            {
                height = R*2;
                width = height;
                leftTop.x = coordinates.x - R;
                leftTop.y = coordinates.y - R;
            }
        }

        public override void Dialog()
        {
            Circle thisCircle = this;
            var inputCircle = new DialogKruh(thisCircle, "zadej nove parametry kruhu:");
            inputCircle.ShowDialog();
        }

        public override Coordinates Size()
        {
            Coordinates size = new Coordinates() { x = width + 20, y = height + 20 };
            return size;
        }

        public override Coordinates MarginCoordinates()
        {
            Coordinates margin = new Coordinates();
            margin.x = leftTop.x - 10;
            margin.y = leftTop.y - 10;
            return margin;
        }
    }
}
