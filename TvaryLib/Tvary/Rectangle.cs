using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShapesLib
{
    public class Rectangle : Shape
    {
        
        public double width;
        public double height;
        static int pocet = 0;
        public Rectangle () : base(ShapeType.Rectangle)
        {
            Random random = new Random();
            leftTop = new Coordinates()
            {
                x = random.Next(1, 300),
                y = random.Next(1, 300)
            };
            height = random.Next(20, 200);
            width = random.Next(20, 200);
            brush = new SolidColorBrush(Color.FromRgb((byte)random.Next(1, 255), (byte)random.Next(1, 255), (byte)random.Next(1, 255)));
            name = "obdelnik" + pocet;
            pocet++;
        }

        public void ChangeRectangle(Coordinates coordinates, double height, double width)
        {
            leftTop.x = coordinates.x;
            leftTop.y = coordinates.y;
            this.height = height;
            this.width = width;
        }

        public override void PaintShape(Canvas canvas)
        {
            System.Windows.Shapes.Rectangle myRectangle = new System.Windows.Shapes.Rectangle();
            myRectangle.Width = width;
            myRectangle.Height = height;
            myRectangle.Fill = brush;
            myRectangle.Margin = new Thickness(leftTop.x, leftTop.y, 0, 0);
            canvas.Children.Add(myRectangle);
        }

        public override void Dialog()
        {
            Rectangle thisRectangle = this;
            var inputRectangle = new DialogObdelnik(thisRectangle, "zadej nove parametry obdelniku:");
            inputRectangle.ShowDialog();
        }
        public override Coordinates Size()
        {
            Coordinates size = new Coordinates() { x=width + 20, y = height+20};
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
