using System;
using System.Windows;


namespace ShapesLib
{
	public partial class DialogKruh : Window
	{
		Circle mujKruh;
		public DialogKruh(Circle tentoKruh, string question)
		{
			InitializeComponent();
			lblQuestion.Content = question;
			Coordinates stred = new Coordinates() { x = tentoKruh.leftTop.x + ((tentoKruh.height) / 2), y = tentoKruh.leftTop.y + ((tentoKruh.height) / 2) };
			txtX.Text = Convert.ToString(stred.x);
			txtY.Text = Convert.ToString(stred.y);
			txtR.Text = Convert.ToString(tentoKruh.height / 2);
			mujKruh = tentoKruh;
		}


		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			try
			{
				Tvary tvary = new Tvary();
				Coordinates souradnice = new Coordinates() { x = Convert.ToDouble(txtX.Text), y = Convert.ToDouble(txtY.Text) };
				mujKruh.ChangeCircle(souradnice, Convert.ToDouble(txtR.Text));
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
			txtR.SelectAll();
			txtR.Focus();
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
