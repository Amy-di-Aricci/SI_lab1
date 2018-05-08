using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AILab1.Models;
using System.Diagnostics;

namespace AILab1
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            MamdaniSystem m = new MamdaniSystem();
            float y = m.ProcessRules(0f, 0f);
            Debug.WriteLine(String.Format("WYJŚCIE MAMDAMI (0, 0): {0:f}", y));
            float z = m.ProcessRules(10f, 10f);
            Debug.WriteLine(String.Format("WYJŚCIE MAMDAMI (10, 10): {0:f}", z));
            float a = m.ProcessRules(9.99f, 10f);
            Debug.WriteLine(String.Format("WYJŚCIE MAMDAMI (9.99, 10): {0:f}", a));
        }
	}
}
