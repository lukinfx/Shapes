using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

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
    [XmlInclude(typeof(Line)), XmlInclude(typeof(Circle)), XmlInclude(typeof(Rectangle))]
    public class Shape
    {
        public string name;
        
        [XmlIgnore]
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

        public virtual Coordinates GetSize()
        {
            throw new NotImplementedException();
        }

        public virtual void Move(Coordinates offset)
        {
            throw new NotImplementedException();
        }

        public virtual Coordinates GetCoordinates()
        {
            throw new NotImplementedException();
        }

        public Rect GetBoundingRect()
        {
            var rect = new Rect()
            {
                X = GetCoordinates().x,
                Y = GetCoordinates().y,
                Width = GetSize().x,
                Height = GetSize().y
            };
            return rect;
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

        private void PaintBorder(Canvas canvas)
        {
            foreach (var shape in listOfShapes)
            {
                if (shape.isSelected) 
                {
                    var boundingRect = shape.GetBoundingRect();
                    boundingRect.Inflate(10, 10);

                    System.Windows.Shapes.Rectangle borderRectangle = new System.Windows.Shapes.Rectangle();
                    Canvas.SetLeft(borderRectangle, boundingRect.X);
                    Canvas.SetTop(borderRectangle, boundingRect.Y);
                    borderRectangle.Width = boundingRect.Width;
                    borderRectangle.Height = boundingRect.Height;

                    var arr = new DoubleCollection() { 6, 3 };
                    borderRectangle.StrokeDashArray = arr;
                    borderRectangle.Stroke = Brushes.Red;

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

            PaintBorder(canvas);
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
                var boundingRect = tvar.GetBoundingRect();
                boundingRect.Inflate(10, 10);

                var mouseRect = new Rect(mouseMove.x, mouseMove.y, 0, 0);

                if (boundingRect.IntersectsWith(mouseRect))
                {
                    tvar.isSelected = !tvar.isSelected;
                }
            }
        }
        public void SaveXaml ()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML (*.xml)|*.xml|All Files (*.*)|*.*";
            var result = dlg.ShowDialog();
            if (result.Value != true) return;


            // Make the XmlSerializer.
            XmlSerializer serializer =
                new XmlSerializer(typeof(List<ShapesLib.Shape>));
            using (FileStream stream = File.Create(dlg.FileName))
            {
                serializer.Serialize(stream, listOfShapes);
            }
        }

        public void OpenXaml(Canvas canvas)
        {
            listOfShapes.Clear();
            canvas.Children.Clear();
            // Get the name of the file where we should save the segments.
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML (*.xml)|*.xml|All Files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result.Value != true) return;

            // Make the XmlSerializer.
            XmlSerializer serializer =
                new XmlSerializer(typeof(List<ShapesLib.Shape>));
            using (FileStream stream =
                File.Open(dlg.FileName, FileMode.Open))
            {
                List<ShapesLib.Shape> segments =
                    (List<ShapesLib.Shape>)serializer.Deserialize(stream);
                // Add the loaded segments.
                foreach (Shape segment in segments)
                {
                    segment.PaintShape(canvas);
                    listOfShapes.Add(segment);
                }
            }
        }
    }
}
