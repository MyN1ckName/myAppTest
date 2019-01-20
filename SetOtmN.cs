using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;

[TransactionAttribute(TransactionMode.Manual)]
[RegenerationAttribute(RegenerationOption.Manual)]
public class SetOtmN : IExternalCommand
{
	public Result Execute(ExternalCommandData commandData,
		ref string messege,
		ElementSet elements)
	{
		UIApplication uiApp = commandData.Application;
		Document doc = uiApp.ActiveUIDocument.Document;

		try
		{
			Reference pickedRef = null;
			Selection sel = uiApp.ActiveUIDocument.Selection;
			WindowPickFilter selFiter = new WindowPickFilter();
			pickedRef = sel.PickObject(ObjectType.Element, selFiter, "Выберите отверстие");

			Element elem = doc.GetElement(pickedRef.ElementId);

			Parameter baseLvl = elem.LookupParameter("Рзм.ВысотаБазовогоУровня");
			Parameter offsetLvl = elem.LookupParameter("Рзм.СмещениеОтУровня");

			using (Transaction t = new Transaction(doc, "SetParametrs"))
			{
				t.Start("Set");
				try
				{
					baseLvl.Set(GetBaseLvl(doc, elem));
					offsetLvl.Set(GetOffsetLvl(elem));
				}
				catch
				{ 
					t.Commit();
				}				
			}
		}

		catch (Autodesk.Revit.Exceptions.OperationCanceledException)
		{
			return Result.Cancelled;
		}

		catch (Exception ex)
		{
			messege = ex.Message;
			return Result.Failed;
		}

		return Result.Succeeded;
	}

	public double GetBaseLvl(Document doc, Element elem) //Получение значения параметра "Базовый уровень"
	{
		Element lvl = doc.GetElement(elem.get_Parameter(BuiltInParameter.FAMILY_LEVEL_PARAM).AsElementId());
		var baseLvl = lvl.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble();
		return baseLvl;
	}
	public double GetOffsetLvl(Element elem) //Получение значения параметра "Высота верхнего бруса"
	{
		var offsetLvL = elem.get_Parameter(BuiltInParameter.INSTANCE_HEAD_HEIGHT_PARAM).AsDouble();
		return offsetLvL;
	}
}

public class WindowPickFilter : ISelectionFilter
{
	public bool AllowElement(Element element)
	{
		return (element.Category.Id.IntegerValue.Equals((int)BuiltInCategory.OST_Windows));
	}
	public bool AllowReference(Reference reference, XYZ position)
	{
		return false;
	}
}