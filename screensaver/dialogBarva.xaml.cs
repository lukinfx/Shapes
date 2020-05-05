using System;
using System.Windows;
using ShapesLib;

namespace screensaver
{
	public partial class dialogBarva : Window
	{
		RGB myRGB = new RGB();
		public dialogBarva(string question, string defaultAnswer = "")
		{
			InitializeComponent();
			lblQuestion.Content = question;
			txtR.Text = defaultAnswer;
			txtG.Text = defaultAnswer;
			txtB.Text = defaultAnswer;
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			txtR.SelectAll();
			txtR.Focus();
			txtG.SelectAll();
			txtG.Focus();
			txtB.SelectAll();
			txtB.Focus();
		}
			
		public RGB Answer
		{
			get 
			{
				myRGB.red = Convert.ToDouble(txtR.Text);
				myRGB.green = Convert.ToDouble(txtG.Text);
				myRGB.blue = Convert.ToDouble(txtB.Text);
				return myRGB; 
			}
		}

		private void Blue_Click(object sender, RoutedEventArgs e)
		{
			txtB.Text = "255";
			txtG.Text = "0";
			txtR.Text = "0";
		}
	}
}
