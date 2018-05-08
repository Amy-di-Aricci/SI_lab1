using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using AILab1.Pages;

namespace AILab1
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			//MainPage = new NavigationPage(new AILab1.MainPage());
            MainPage = new NavigationPage(new TipPage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
