using System;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace myAppTest
{

	[TransactionAttribute(TransactionMode.Manual)]
	[RegenerationAttribute(RegenerationOption.Manual)]
	public class Help : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData,
			ref string messege,
			ElementSet elements)
		{
			TaskDialog.Show("Help",
				"Ask for help" + "\r\n" +
				"Egorov Aleksandr" + "\r\n" +
				"mymailegorov@gmail.com");

			return Result.Succeeded;
		}
	}
}