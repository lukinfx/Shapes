using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShapesLib
{
    public enum ShapeType {
        UNKNOWN, 
        Circle,
        Line,
        Rectangle
    };

    /// <summary>
    /// This is a base class for all shapes
    /// </summary>
    public class Shape
    {
        public string name;
        public Brush brush;
        public ShapeType shapeType;
        public bool isSelected;

        public virtual void PaintShape(Canvas canvas) { }
        public virtual void Dialog() { }
        public Shape()
        {
            this.shapeType = ShapeType.UNKNOWN;
        }

        public Shape(ShapeType shapeType)
        {
            this.shapeType = shapeType;
        }

        public virtual Coordinates Size()
        {
            throw new NotImplementedException();
        }

        public virtual Coordinates MarginCoordinates()
        {
            throw new NotImplementedException();
        }

        public virtual void Move(Coordinates newCoordinates)
        {
            throw new NotImplementedException();
        }
    }

    public class Tvary
    {
        private static List<Shape> listOfShapes = new List<Shape>();

        public virtual void AddShape(Shape pridavanyTvar)
        {
            listOfShapes.Add(pridavanyTvar);
        }

        public virtual void DeleteShape()
        {
            listOfShapes.RemoveAll(t => t.isSelected == true);
        }

        public virtual void ChangeName(string puvodniJmeno, string noveJmeno)
        {
            var upravovanyTvar = listOfShapes.Find(t => t.name == puvodniJmeno);
            upravovanyTvar.name = noveJmeno;
        }

        public void UnselectAll()
        {
            foreach (Shape tvar in listOfShapes)
            {
                tvar.isSelected = false;
            }
        }

        public virtual void MoveSelected(Coordinates offset)
        {
            foreach (var item in listOfShapes)
            {
                if (item.isSelected)
                {
                    item.Move(offset);
                }
            }
        }

        public virtual void DragChangeCoordinates(Coordinates newCoordinates)
        {
            foreach (var item in listOfShapes)
            {
                if (item.isSelected)
                {
                    item.Move(newCoordinates);
                }
            }
        }

        private void Border(Canvas canvas)
        {
            foreach (var shape in listOfShapes)
            {
                if (shape.isSelected) 
                { 
                    Coordinates size = shape.Size();
                    Coordinates marginCoordinates = shape.MarginCoordinates();
                    System.Windows.Shapes.Rectangle borderRectangle = new System.Windows.Shapes.Rectangle();
                    borderRectangle.Width = size.x;
                    borderRectangle.Height = size.y;

                    Pen pen = new Pen();
                    pen.DashStyle = DashStyles.DashDot;
                    var arr = new DoubleCollection() { 6, 3 };

                    borderRectangle.StrokeDashArray = arr;
                    borderRectangle.Stroke = System.Windows.Media.Brushes.Red;
                    borderRectangle.Margin = new Thickness(marginCoordinates.x, marginCoordinates.y, 0, 0);
                    canvas.Children.Add(borderRectangle);
                }
            }
            
        }


        public virtual void PaintCanvas(Canvas canvas)
        {
            canvas.Children.Clear();
            foreach (var t in listOfShapes)
            {
                t.PaintShape(canvas);
            }

            Border(canvas);
        }
        public virtual void UpdateTheListBox(ListBox listBoxNames)
        {
            listBoxNames.Items.Clear();
            foreach (var t in listOfShapes)
            {
                listBoxNames.Items.Add(t.name);
            }
        }

        public void ChangeColor(string name, RGB color)
        {
            var shape = listOfShapes.Find(t => t.name == name);
            shape.brush = new SolidColorBrush(Color.FromRgb((byte)color.red, (byte)color.green, (byte)color.blue));
        }

        public void DeleteAll(Canvas canvas1)
        {
            listOfShapes.Clear();
            canvas1.Children.Clear();
        }

        public Shape GetShape(string jmeno)
        {
            Shape mujTvar = listOfShapes.Find(t => t.name == jmeno);
            
            return mujTvar;
        }

        public void MouseClickRecogniseShape(Coordinates mouseMove)
        {
            foreach (var tvar in listOfShapes)
            {
                Coordinates size = tvar.Size();
                Coordinates margin = tvar.MarginCoordinates();

                System.Diagnostics.Debug.WriteLine($"Tvar : {margin.x} < {mouseMove.x} && {margin.x} + {size.x} > {mouseMove.x} && {margin.y} < {mouseMove.y} && {margin.y} + {size.y} > {mouseMove.y}");
                if (margin.x + 87 < mouseMove.x && margin.x + 87 + size.x > mouseMove.x && margin.y + 25 < mouseMove.y && margin.y + size.y +25 > mouseMove.y)
                {
                    if (tvar.isSelected) { tvar.isSelected = false; }
                    else { tvar.isSelected = true; }
                }
            }
        }
    }
}
