using System;
using System.Windows;
using TvaryLib;

namespace screensaver
{
	public partial class dialogPoloha : Window
	{
		public dialogPoloha(string question, string defaultAnswerX, string defaultAnswerY)
		{
			InitializeComponent();
			lblQuestion.Content = question;
			txtX.Text = defaultAnswerX;
			txtY.Text = defaultAnswerY;
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			txtX.SelectAll();
			txtX.Focus();
			txtY.SelectAll();
			txtY.Focus();
		}

		public Souradnice Answer
		{
			get 
			{
				try
				{
					Souradnice souradnice = new Souradnice();
					souradnice.x = Convert.ToDouble(txtX.Text);
					souradnice.y = Convert.ToDouble(txtY.Text);
					return souradnice;
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
					return null;
				}
			}
		}
	}
}
