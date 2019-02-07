using System;

using Autodesk.Revit.UI;

namespace myAppTest
{
	public class CrtRibbon : IExternalApplication
	{
		public Result OnStartup(UIControlledApplication application)
		{
			// Создание вкладки "myApp"
			String tabName = "myApp";
			application.CreateRibbonTab(tabName);

			// Создание панели "This Panel Name"
			RibbonPanel Panel1 = application.CreateRibbonPanel(tabName, "Panel");

			// Созданеи и размещение кнопки на панели
			PushButton setOtmN = Panel1.AddItem(new PushButtonData("Button2", "Set otm",
				@"C:\ProgramData\Autodesk\Revit\Addins\2017\myAppTest.dll", "SetOtmN")) as PushButton;
			setOtmN.LargeImage = LoadImage("otv");

			// Созданеи и размещение кнопки Help на панели
			PushButton help = Panel1.AddItem(new PushButtonData("Button1", "Help",
				@"C:\ProgramData\Autodesk\Revit\Addins\2017\myAppTest.dll", "Help")) as PushButton;
			help.LargeImage = LoadImage("help");

			return Result.Succeeded;
		}

		Result IExternalApplication.OnShutdown(UIControlledApplication application)
		{
			return Result.Succeeded;
		}

		System.Windows.Media.Imaging.BitmapImage LoadImage(string imageName)
		{
			return new System.Windows.Media.Imaging.BitmapImage(
				new Uri("pack://application:,,,/myAppTest;component/img/" + imageName + ".png", UriKind.Absolute));
		}
	}
}
