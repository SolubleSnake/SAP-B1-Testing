using System;
using System.Collections.Generic;
using System.Xml;
using SAPbouiCOM.Framework;

namespace Test_Add_On
{
    [FormAttribute("Test_Add_On.Form1", "Form1.b1f")]
    class Form1 : UserFormBase
    {
        public Form1()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_1").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            //this.LoadAfter += new LoadAfterHandler(this.Form_LoadAfter);

        }

        private void Form_LoadAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            throw new System.NotImplementedException();
        }

        private void OnCustomInitialize()
        {

        }

        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Grid Grid0;

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Grid0.DataTable.ExecuteQuery(SqlQuery);
        }

        string SqlQuery = @"SELECT
   --T1.[DocNum],
   --T1.[DocDate],
   T0.[ItemCode],
   T0.[DiscPrcnt],
   T0.[Currency], 
   count(*) AS 'COUNT'

FROM [ice-sap9-server\SQLEXPRESS].[DB_18584_config].[dbo].[Items] T2 
   INNER JOIN INV1 T0 ON T2.[ItemNum] COLLATE DATABASE_DEFAULT = T0.[ItemCode] COLLATE DATABASE_DEFAULT
   INNER JOIN OINV T1 ON T0.[DocEntry] = T1.[DocEntry]
 
WHERE T2.[ItemCat]>25 AND DATEDIFF(dd, T1.[DocDate], GETDATE())<365 AND T1.[CardCode]<>'S00311'

GROUP BY T0.[ItemCode], T0.[Currency], T0.[DiscPrcnt]

ORDER BY T0.[ItemCode], T0.[Currency], T0.[DiscPrcnt]";
    }
}