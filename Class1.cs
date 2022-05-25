
namespace Color_change
{
    using Autodesk.AutoCAD.EditorInput;
    using Autodesk.AutoCAD.DatabaseServices;    
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.Aec.PropertyData.DatabaseServices;
    using System;
    using Autodesk.AutoCAD.Runtime;
        

public class Class1
    {

        public static PropertySetDefinition psd1;
        public static string propertySetName;
        public static string DescrStr;
        public static string caseSwitch;
        public static PropertySetDefinition def;
        public static int SolColorId;


        [CommandMethod("Change_color")]
        public static void Change_color()

        {

            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            Editor ed = acDoc.Editor;
            Database acCurDb = acDoc.Database;




            DictionaryPropertySetDefinitions dictPropSetDef1 = new DictionaryPropertySetDefinitions(acCurDb);

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                ed.WriteMessage("\nSelect a 3D solids:");
                PromptSelectionOptions pso1 = new PromptSelectionOptions();
                PromptSelectionResult result1;
                result1 = ed.GetSelection(pso1);
                SelectionSet acSSet2 = result1.Value;

                if (result1.Status == PromptStatus.OK)
                {
                    foreach (SelectedObject item in acSSet2)
                    {
                        try
                        {
                            Object obj = acTrans.GetObject(item.ObjectId, OpenMode.ForWrite);
                            Solid3d sol = obj as Solid3d;
                            ObjectIdCollection setIds = PropertyDataServices.GetPropertySets(sol);
                            foreach (ObjectId psId in setIds)
                            {
                                PropertySet pset = acTrans.GetObject(psId, OpenMode.ForWrite, false, false) as PropertySet;
                                ObjectId idDef = pset.PropertySetDefinition;
                                PropertySetDefinition def = (PropertySetDefinition)acTrans.GetObject(idDef, OpenMode.ForWrite);
                                propertySetName = "ObjektID";
                                SolColorId = pset.PropertyNameToId("Zustandsklasse");
                                string SolColor = pset.GetAt(SolColorId).ToString();
                                if (SolColor == "0")
                                {
                                    SolColor = "3";
                                }
                                else 
                                    if (SolColor == "1")
                                {
                                    SolColor = "9";
                                }
                                else
                                {
                                    if (SolColor == "2")
                                    {
                                        SolColor = "2";
                                    }
                                 else
                                    {
                                        if (SolColor == "3")
                                        {
                                            SolColor = "5";
                                        }

                                    }
                                }
                            }
                        }
                        catch {}
                    }
                }
        }
    }
}
}