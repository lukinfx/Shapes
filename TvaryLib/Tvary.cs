using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TvaryLib
{
    public enum TypTvaru {
        UNKNOWN, 
        Kruh,
        Cara,
        Obdelnik
    };

    /// <summary>
    /// This is a base class for all shapes
    /// </summary>
    public class Tvar
    {
        public string jmeno;
        public Souradnice leftTop;
        public Brush brush;
        public TypTvaru typTvaru;
        public bool jeOznaceny;

        public virtual void Vykresli(Canvas canvas) { }
        public virtual void Dialog() { }
        public Tvar()
        {
            this.typTvaru = TypTvaru.UNKNOWN;
        }

        public Tvar(TypTvaru typTvaru)
        {
            this.typTvaru = typTvaru;
        }

        public virtual Souradnice Rozmer()
        {
            throw new NotImplementedException();
        }

        public virtual Souradnice Pocatek()
        {
            throw new NotImplementedException();
        }

        public virtual void ZmenSouradnice(Souradnice noveSouradnice)
        {

            leftTop.x = leftTop.x + noveSouradnice.x;
            leftTop.y = leftTop.y + noveSouradnice.y;
        }
    }

    public class Tvary
    {
        private static List<Tvar> seznam = new List<Tvar>();

        public virtual void Pridej(Tvar pridavanyTvar)
        {
            seznam.Add(pridavanyTvar);
        }

        public virtual void Smaz()
        {
            seznam.RemoveAll(t => t.jeOznaceny == true);
        }

        public virtual void ZmenJmeno(string puvodniJmeno, string noveJmeno)
        {
            var upravovanyTvar = seznam.Find(t => t.jmeno == puvodniJmeno);
            upravovanyTvar.jmeno = noveJmeno;
        }

        public void ZrusOznaceni()
        {
            foreach (Tvar tvar in seznam)
            {
                tvar.jeOznaceny = false;
            }
        }

        public virtual void ZmenSouradnice(Souradnice noveSouradnice)
        {
            foreach (var item in seznam)
            {
                if (item.jeOznaceny)
                {
                    item.leftTop.x = noveSouradnice.x;
                    item.leftTop.y = noveSouradnice.y;
                }
            }
        }

        public virtual void ZmenSouradnice2(Souradnice noveSouradnice)
        {
            foreach (var item in seznam)
            {
                if (item.jeOznaceny)
                {
                    item.ZmenSouradnice(noveSouradnice);
                }
            }
        }

        public virtual Souradnice VratSouradnice(string puvodniJmeno)
        {
            var upravovanyTvar = seznam.Find(t => t.jmeno == puvodniJmeno);
            Souradnice souradnice = new Souradnice();
            souradnice.x = upravovanyTvar.leftTop.x;
            souradnice.y = upravovanyTvar.leftTop.y;
            return souradnice;
        }

        private void Ohraniceni(Canvas canvas)
        {
            foreach (var tvar in seznam)
            {
                if (tvar.jeOznaceny) 
                { 
                    Souradnice rozmer = tvar.Rozmer();
                    Souradnice pocatek = tvar.Pocatek();
                    Rectangle ohraniceni = new Rectangle();
                    ohraniceni.Width = rozmer.x;
                    ohraniceni.Height = rozmer.y;

                    Pen pen = new Pen();
                    pen.DashStyle = DashStyles.DashDot;
                    var arr = new DoubleCollection() { 6, 3 };

                    ohraniceni.StrokeDashArray = arr;
                    ohraniceni.Stroke = System.Windows.Media.Brushes.Red;
                    ohraniceni.Margin = new Thickness(pocatek.x, pocatek.y, 0, 0);
                    canvas.Children.Add(ohraniceni);
                }
            }
            
        }


        public virtual void VykresliCanvas(Canvas canvas)
        {
            canvas.Children.Clear();
            foreach (var t in seznam)
            {
                t.Vykresli(canvas);
            }

            Ohraniceni(canvas);
        }
        public virtual void VykresliListBoxJmena(ListBox jmena)
        {
            jmena.Items.Clear();
            foreach (var t in seznam)
            {
                jmena.Items.Add(t.jmeno);
            }
        }

        public void ZmenBarvu(string jmeno, RGB barva)
        {
            var upravovanyTvar = seznam.Find(t => t.jmeno == jmeno);
            upravovanyTvar.brush = new SolidColorBrush(Color.FromRgb((byte)barva.red, (byte)barva.green, (byte)barva.blue));
        }

        public void SmazVse(Canvas canvas1)
        {
            seznam.Clear();
            canvas1.Children.Clear();
        }

        public TypTvaru VratTvar(string jmeno)
        {
            var mujTvar = seznam.Find(t => t.jmeno == jmeno);
            return mujTvar.typTvaru;
        }

        public Tvar NajdiTvar(string jmeno)
        {
            Tvar mujTvar = seznam.Find(t => t.jmeno == jmeno);
            
            return mujTvar;
        }

        public void PoznejTvar(Souradnice mouseMove)
        {
            foreach (var tvar in seznam)
            {
                Souradnice rozmer = tvar.Rozmer();
                Souradnice pocatek = tvar.Pocatek();

                System.Diagnostics.Debug.WriteLine($"Tvar : {pocatek.x} < {mouseMove.x} && {pocatek.x} + {rozmer.x} > {mouseMove.x} && {pocatek.y} < {mouseMove.y} && {pocatek.y} + {rozmer.y} > {mouseMove.y}");
                if (pocatek.x + 87 < mouseMove.x && pocatek.x + 87 + rozmer.x > mouseMove.x && pocatek.y + 25 < mouseMove.y && pocatek.y + rozmer.y +25 > mouseMove.y)
                {
                    if (tvar.jeOznaceny) { tvar.jeOznaceny = false; }
                    else { tvar.jeOznaceny = true; }
                }
            }
        }
    }
}
