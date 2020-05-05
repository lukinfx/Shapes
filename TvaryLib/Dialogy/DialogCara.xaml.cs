using System;
using System.Windows;
using TvaryLib;

namespace TvaryLib
{
	public partial class DialogCara : Window
	{
		Cara mojeCara;
		public DialogCara(Cara tatoCara, string question)
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
				Souradnice souradnicePocatek = new Souradnice() { x = Convert.ToDouble(txtX.Text), y = Convert.ToDouble(txtY.Text) };
				Souradnice souradniceKonec = new Souradnice() { x = Convert.ToDouble(txtHeight.Text), y = Convert.ToDouble(txtWeidth.Text) };
				Cara.UpravCaru(mojeCara.jmeno, souradnicePocatek, souradniceKonec);
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
