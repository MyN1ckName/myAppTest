﻿using System;
using System.Windows.Media.Imaging;

using Autodesk.Revit.UI;

public class myAppTest : IExternalApplication
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
		setOtmN.LargeImage = new BitmapImage(new Uri(@"d:\Revit\PlugIn\MyN1ckName\myAppTest\img\otv.png"));

		// Созданеи и размещение кнопки Help на панели
		PushButton help = Panel1.AddItem(new PushButtonData("Button1", "Help",
			@"C:\ProgramData\Autodesk\Revit\Addins\2017\myAppTest.dll", "Help")) as PushButton;
		help.LargeImage = new BitmapImage(new Uri(@"d:\Revit\PlugIn\MyN1ckName\myAppTest\img\help.png"));

		return Result.Succeeded;
	}

	Result IExternalApplication.OnShutdown(UIControlledApplication application)
	{
		return Result.Succeeded;
	}
}