using System;
#if DEBUG
using System.Runtime.InteropServices;
#endif

namespace PotentialHappiness
{
	/// <summary>
	/// The main class.
	/// </summary>
	public static class Program
	{
#if DEBUG
		[DllImport("kernel32")]
		static extern bool AllocConsole();
#endif
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
#if DEBUG
			AllocConsole();
#endif
			Log("*** Potential Happiness ***");

			using (PotentialHappiness.Screens.ScreenManager manager = PotentialHappiness.Screens.ScreenManager.Instance)
			{
				manager.Run();
			}
		}

		public static void Log(string str)
		{
#if DEBUG
			Console.WriteLine(str);
#endif
		}
	}
}
