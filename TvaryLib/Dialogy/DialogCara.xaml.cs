using System;
using System.Windows;
using ShapesLib;

namespace ShapesLib
{
	public partial class DialogCara : Window
	{
		Line mojeCara;
		public DialogCara(Line tatoCara, string question)
		{
			InitializeComponent();
			mojeCara = tatoCara;
			lblQuestion.Content = question;
			txtX.Text = Convert.ToString(mojeCara.x1);
			txtY.Text = Convert.ToString(mojeCara.y1);
			txtHeight.Text = Convert.ToString(mojeCara.y2);
			txtWeidth.Text = Convert.ToString(mojeCara.x2);
			
		}


		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			try
			{
				Tvary tvary = new Tvary();
				Coordinates souradnicePocatek = new Coordinates() { x = Convert.ToDouble(txtX.Text), y = Convert.ToDouble(txtY.Text) };
				Coordinates souradniceKonec = new Coordinates() { x = Convert.ToDouble(txtHeight.Text), y = Convert.ToDouble(txtWeidth.Text) };
				Line.ChangeLine(mojeCara.name, souradnicePocatek, souradniceKonec);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			txtX.SelectAll();
			txtX.Focus();
			txtY.SelectAll();
			txtY.Focus();
			txtHeight.SelectAll();
			txtHeight.Focus();
			txtWeidth.SelectAll();
			txtWeidth.Focus();
		}

		public bool Answer
		{
			get 
			{
				return true;
			}
		}
	}
}
