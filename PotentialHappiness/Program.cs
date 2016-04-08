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
			using (PotentialHappiness.Screens.ScreenManager manager = PotentialHappiness.Screens.ScreenManager.Instance)
			{
				manager.Run();
			}
		}
	}
}
