using System;
using System.Windows;

namespace ShapesLib
{
	public partial class DialogObdelnik : Window
	{
		Rectangle obdelnik;
		public DialogObdelnik(Rectangle tentoObdelnik, string question)
		{
			InitializeComponent();
			lblQuestion.Content = question;
			txtX.Text = Convert.ToString(tentoObdelnik.leftTop.x);
			txtY.Text = Convert.ToString(tentoObdelnik.leftTop.y);
			txtHeight.Text = Convert.ToString(tentoObdelnik.height);
			txtWeidth.Text = Convert.ToString(tentoObdelnik.width);
			obdelnik = tentoObdelnik;
		}


		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			try
			{
				Tvary tvary = new Tvary();
				Coordinates souradnice = new Coordinates() { x = Convert.ToDouble(txtX.Text), y = Convert.ToDouble(txtY.Text) };
				obdelnik.ChangeRectangle(souradnice, Convert.ToDouble(txtHeight.Text), Convert.ToDouble(txtWeidth.Text));
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
