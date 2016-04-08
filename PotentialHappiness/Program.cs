using System;

namespace PotentialHappiness
{
	/// <summary>
	/// The main class.
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			PotentialHappiness.Screens.ScreenManager.Instance.Main();
		}
	}
}
