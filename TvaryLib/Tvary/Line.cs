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

namespace ShapesLib
{
    public class Line : Shape
    {
        public double x1;
        public double x2;
        public double y1;
        public double y2;
        int thickness;

        static int pocet = 0;

        public Line() : base(ShapeType.Line)
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
            name = "cara" + pocet;
            pocet++;

            brush = System.Windows.Media.Brushes.Black; //new SolidColorBrush(Color.FromRgb((byte)random.Next(1, 255), (byte)random.Next(1, 255), (byte)random.Next(1, 255)));
        }

        public static void ChangeLine(string name, Coordinates coordinatesStart, Coordinates coordinatesEnd)
        {
            Tvary shapes = new Tvary();
            Line line = (Line)shapes.GetShape(name);
            line.x1 = coordinatesStart.x;
            line.y1 = coordinatesStart.y;
            line.x2 = coordinatesEnd.x;
            line.y2 = coordinatesEnd.y;

        }

        public override void PaintShape(Canvas canvas)
        {
            System.Windows.Shapes.Line myLine = new System.Windows.Shapes.Line();
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
            Line thisLine = this;
            var inputCara = new DialogCara(thisLine, "Zadej nove parametry cary");
            inputCara.ShowDialog();
        }

        public override Coordinates GetSize ()
        {
            Coordinates size = new Coordinates() { x = Math.Abs(x2 - x1) , y = Math.Abs(y2 - y1) };
            return size;
        }

        public override void Move(Coordinates offset)
        {
            x1 = x1 + offset.x;
            y1 = y1 + offset.y;

            x2 = x2 + offset.x;
            y2 = y2 + offset.y;
        }

        public override Coordinates GetCoordinates()
        {
            return new Coordinates() { x = Math.Min(x1, x2), y = Math.Min(y1,y2) };
        }

    }
}
