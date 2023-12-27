﻿using Java.Util;
using MvvmHelpers;
using MySqlConnector;
using SmartPharma5.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SmartPharma5.ModelView
{
    public class BarSeriesMV :BaseViewModel
    {
        // public string xml_file = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Dashboard>\r\n  <Title Text=\"Sample Dashboard\" />\r\n  <DataSources>\r\n    <ExcelDataSource Name=\"Excel Data Source 1\" FileName=\"|DataDirectory|\\Data\\Sales.xlsx\" ComponentName=\"dashboardExcelDataSource1\">\r\n      <Options Type=\"DevExpress.DataAccess.Excel.ExcelSourceOptions\" SkipEmptyRows=\"true\" UseFirstRowAsHeader=\"true\" SkipHiddenColumns=\"true\" SkipHiddenRows=\"true\">\r\n        <ImportSettings Type=\"DevExpress.DataAccess.Excel.ExcelWorksheetSettings\" WorksheetName=\"Sheet1\" />\r\n      </Options>\r\n      <Schema>\r\n        <FieldInfo Name=\"Category\" Type=\"System.String\" Selected=\"true\" />\r\n        <FieldInfo Name=\"Product\" Type=\"System.String\" Selected=\"true\" />\r\n        <FieldInfo Name=\"State\" Type=\"System.String\" Selected=\"true\" />\r\n        <FieldInfo Name=\"UnitsSoldYTD\" Type=\"System.Double\" Selected=\"true\" />\r\n        <FieldInfo Name=\"UnitsSoldYTDTarget\" Type=\"System.Double\" Selected=\"true\" />\r\n        <FieldInfo Name=\"RevenueQTD\" Type=\"System.Double\" Selected=\"true\" />\r\n        <FieldInfo Name=\"RevenueQTDTarget\" Type=\"System.Double\" Selected=\"true\" />\r\n        <FieldInfo Name=\"RevenueYTD\" Type=\"System.Double\" Selected=\"true\" />\r\n        <FieldInfo Name=\"RevenueYTDTarget\" Type=\"System.Double\" Selected=\"true\" />\r\n      </Schema>\r\n      <ResultSchema>\r\n        <View>\r\n          <Field Name=\"Category\" Type=\"String\" />\r\n          <Field Name=\"Product\" Type=\"String\" />\r\n          <Field Name=\"State\" Type=\"String\" />\r\n          <Field Name=\"UnitsSoldYTD\" Type=\"Double\" />\r\n          <Field Name=\"UnitsSoldYTDTarget\" Type=\"Double\" />\r\n          <Field Name=\"RevenueQTD\" Type=\"Double\" />\r\n          <Field Name=\"RevenueQTDTarget\" Type=\"Double\" />\r\n          <Field Name=\"RevenueYTD\" Type=\"Double\" />\r\n          <Field Name=\"RevenueYTDTarget\" Type=\"Double\" />\r\n        </View>\r\n      </ResultSchema>\r\n    </ExcelDataSource>\r\n    <SqlDataSource Name=\"SQL Data Source 1\" ComponentName=\"dashboardSqlDataSource1\">\r\n      <Connection Name=\"141.94.195.211_testprod_Connection\" ProviderKey=\"MySql\">\r\n        <Parameters>\r\n          <Parameter Name=\"server\" Value=\"141.94.195.211\" />\r\n          <Parameter Name=\"database\" Value=\"testprod\" />\r\n          <Parameter Name=\"read only\" Value=\"1\" />\r\n          <Parameter Name=\"generateConnectionHelper\" Value=\"false\" />\r\n          <Parameter Name=\"Port\" Value=\"3306\" />\r\n          <Parameter Name=\"userid\" Value=\"\" />\r\n          <Parameter Name=\"password\" Value=\"\" />\r\n        </Parameters>\r\n      </Connection>\r\n      <ConnectionOptions CloseConnection=\"true\" />\r\n    </SqlDataSource>\r\n    <SqlDataSource Name=\"SQL Data Source 2\" ComponentName=\"dashboardSqlDataSource2\">\r\n      <Connection Name=\"141.94.195.211_testprod_Connection\" ProviderKey=\"MySql\">\r\n        <Parameters>\r\n          <Parameter Name=\"server\" Value=\"141.94.195.211\" />\r\n          <Parameter Name=\"database\" Value=\"testprod\" />\r\n          <Parameter Name=\"read only\" Value=\"1\" />\r\n          <Parameter Name=\"generateConnectionHelper\" Value=\"false\" />\r\n          <Parameter Name=\"Port\" Value=\"3306\" />\r\n          <Parameter Name=\"userid\" Value=\"\" />\r\n          <Parameter Name=\"password\" Value=\"\" />\r\n        </Parameters>\r\n      </Connection>\r\n      <Query Type=\"CustomSqlQuery\" Name=\"Query\">\r\n        <Sql>select * from commercial_partner ;</Sql>\r\n      </Query>\r\n      <ResultSchema>\r\n        <DataSet Name=\"SQL Data Source 2\">\r\n          <View Name=\"Query\">\r\n            <Field Name=\"Id\" Type=\"UInt32\" />\r\n            <Field Name=\"name\" Type=\"String\" />\r\n            <Field Name=\"reference\" Type=\"String\" />\r\n            <Field Name=\"create_date\" Type=\"DateTime\" />\r\n            <Field Name=\"chec_socity\" Type=\"Boolean\" />\r\n            <Field Name=\"website\" Type=\"String\" />\r\n            <Field Name=\"phone\" Type=\"String\" />\r\n            <Field Name=\"mobile\" Type=\"String\" />\r\n            <Field Name=\"fax\" Type=\"String\" />\r\n            <Field Name=\"email\" Type=\"String\" />\r\n            <Field Name=\"note\" Type=\"String\" />\r\n            <Field Name=\"customer\" Type=\"Boolean\" />\r\n            <Field Name=\"supplier\" Type=\"Boolean\" />\r\n            <Field Name=\"payment_method_supplier\" Type=\"UInt32\" />\r\n            <Field Name=\"payment_condition_supplier\" Type=\"UInt32\" />\r\n            <Field Name=\"payment_condition_customer\" Type=\"UInt32\" />\r\n            <Field Name=\"socity\" Type=\"UInt32\" />\r\n            <Field Name=\"number\" Type=\"String\" />\r\n            <Field Name=\"street\" Type=\"String\" />\r\n            <Field Name=\"city\" Type=\"String\" />\r\n            <Field Name=\"state\" Type=\"String\" />\r\n            <Field Name=\"country\" Type=\"String\" />\r\n            <Field Name=\"postal_code\" Type=\"String\" />\r\n            <Field Name=\"delivery_number\" Type=\"String\" />\r\n            <Field Name=\"delivery_street\" Type=\"String\" />\r\n            <Field Name=\"delivery_city\" Type=\"String\" />\r\n            <Field Name=\"delivery_state\" Type=\"String\" />\r\n            <Field Name=\"delivery_country\" Type=\"String\" />\r\n            <Field Name=\"delivery_postal_code\" Type=\"String\" />\r\n            <Field Name=\"credit_limit\" Type=\"Decimal\" />\r\n            <Field Name=\"currency\" Type=\"UInt32\" />\r\n            <Field Name=\"job_position\" Type=\"String\" />\r\n            <Field Name=\"customs_code\" Type=\"String\" />\r\n            <Field Name=\"vat_code\" Type=\"String\" />\r\n            <Field Name=\"trade_register\" Type=\"String\" />\r\n            <Field Name=\"picture\" Type=\"ByteArray\" />\r\n            <Field Name=\"payment_method_customer\" Type=\"UInt32\" />\r\n            <Field Name=\"rest_amount\" Type=\"Decimal\" />\r\n            <Field Name=\"due_date\" Type=\"DateTime\" />\r\n            <Field Name=\"actif\" Type=\"Boolean\" />\r\n            <Field Name=\"customer_discount\" Type=\"Decimal\" />\r\n            <Field Name=\"supplier_discount\" Type=\"Decimal\" />\r\n            <Field Name=\"vat_exemption\" Type=\"Boolean\" />\r\n            <Field Name=\"custumer_withholding_tax\" Type=\"UInt32\" />\r\n            <Field Name=\"supplier_withholding_tax\" Type=\"UInt32\" />\r\n            <Field Name=\"activity\" Type=\"String\" />\r\n            <Field Name=\"category\" Type=\"UInt32\" />\r\n            <Field Name=\"sale_agent\" Type=\"UInt32\" />\r\n            <Field Name=\"purchase_agent\" Type=\"UInt32\" />\r\n            <Field Name=\"exoneration_purchase_tax1\" Type=\"Boolean\" />\r\n            <Field Name=\"exoneration_purchase_tax2\" Type=\"Boolean\" />\r\n            <Field Name=\"exoneration_purchase_tax3\" Type=\"Boolean\" />\r\n            <Field Name=\"exoneration_purchase_tax4\" Type=\"Boolean\" />\r\n            <Field Name=\"exoneration_purchase_tax5\" Type=\"Boolean\" />\r\n            <Field Name=\"exoneration_sale_tax1\" Type=\"Boolean\" />\r\n            <Field Name=\"exoneration_sale_tax2\" Type=\"Boolean\" />\r\n            <Field Name=\"exoneration_sale_tax3\" Type=\"Boolean\" />\r\n            <Field Name=\"exoneration_sale_tax4\" Type=\"Boolean\" />\r\n            <Field Name=\"exoneration_sale_tax5\" Type=\"Boolean\" />\r\n          </View>\r\n        </DataSet>\r\n      </ResultSchema>\r\n      <ConnectionOptions CloseConnection=\"true\" />\r\n    </SqlDataSource>\r\n  </DataSources>\r\n  <Parameters>\r\n    <Parameter Name=\"BeginDATE\" Type=\"System.DateTime\" Value=\"2000-01-01T00:00:00\" />\r\n    <Parameter Name=\"EndDate\" Type=\"System.DateTime\" Value=\"2000-01-01T00:00:00\" />\r\n    <Parameter Name=\"User\" Type=\"System.Int16\" Value=\"0\" />\r\n    <Parameter Name=\"Name\" Value=\"\" />\r\n    <Parameter Name=\"total\" Type=\"System.Decimal\" Value=\"0\" />\r\n  </Parameters>\r\n  <Items>\r\n    <Chart ComponentName=\"chartDashboardItem1\" Name=\"Chart 1\" DataSource=\"dashboardSqlDataSource2\" DataMember=\"Query\">\r\n      <DataItems>\r\n        <Measure DataMember=\"rest_amount\" DefaultId=\"DataItem0\" />\r\n        <Dimension DataMember=\"category\" TopNCount=\"200\" TopNMeasure=\"DataItem0\" DefaultId=\"DataItem1\" />\r\n      </DataItems>\r\n      <Arguments>\r\n        <Argument DefaultId=\"DataItem1\" />\r\n      </Arguments>\r\n      <Panes>\r\n        <Pane Name=\"Pane 1\">\r\n          <Series>\r\n            <Simple>\r\n              <Value DefaultId=\"DataItem0\" />\r\n            </Simple>\r\n          </Series>\r\n        </Pane>\r\n      </Panes>\r\n    </Chart>\r\n    <Chart ComponentName=\"chartDashboardItem2\" Name=\"Chart 2\" DataSource=\"dashboardSqlDataSource2\" DataMember=\"Query\">\r\n      <DataItems>\r\n        <Dimension DataMember=\"customer\" DefaultId=\"DataItem0\" />\r\n        <Measure DataMember=\"rest_amount\" DefaultId=\"DataItem1\" />\r\n        <Measure DataMember=\"sale_agent\" DefaultId=\"DataItem2\" />\r\n        <Measure DataMember=\"payment_method_customer\" DefaultId=\"DataItem3\" />\r\n      </DataItems>\r\n      <Arguments>\r\n        <Argument DefaultId=\"DataItem0\" />\r\n      </Arguments>\r\n      <Panes>\r\n        <Pane Name=\"Pane 1\">\r\n          <Series>\r\n            <Simple>\r\n              <Value DefaultId=\"DataItem1\" />\r\n            </Simple>\r\n            <Simple>\r\n              <Value DefaultId=\"DataItem2\" />\r\n            </Simple>\r\n            <Simple>\r\n              <Value DefaultId=\"DataItem3\" />\r\n            </Simple>\r\n          </Series>\r\n        </Pane>\r\n      </Panes>\r\n    </Chart>\r\n  </Items>\r\n  <LayoutTree>\r\n    <LayoutGroup Orientation=\"Vertical\" Weight=\"100\">\r\n      <LayoutGroup>\r\n        <LayoutItem DashboardItem=\"chartDashboardItem1\" />\r\n        <LayoutItem DashboardItem=\"chartDashboardItem2\" />\r\n      </LayoutGroup>\r\n    </LayoutGroup>\r\n  </LayoutTree>\r\n</Dashboard>";
        public string xml_file = "";
        /* private DataTabel successpopupmessage;
         public string SuccessPopupMessage { get => successpopupmessage; set => SetProperty(ref successpopupmessage, value); }*/

        public DataTable allDataForDashbord;
        public DataTable AllDataForDashbord { get => allDataForDashbord; set => SetProperty(ref allDataForDashbord, value); }


        public List<BarSeriesModel> list_BarSeries;
        public List<BarSeriesModel> List_BarSeries { get => list_BarSeries; set => SetProperty(ref list_BarSeries, value); }

        public List<PieSerieModel> list_Pie;
        public List<PieSerieModel> List_Pie { get => list_Pie; set => SetProperty(ref list_Pie, value); }
        





        public BarSeriesMV(string XML)
        {
            this.xml_file = XML;
            List<string> Result = new List<string>();
            List<string> ResultPie = new List<string>();
            Result = ExtractChartTags(xml_file);
            ResultPie = ExtractPieTags(xml_file);
            List<QueryModel> querrys = GetQueryContents(this.xml_file);
            string REQUETTE = ExtractSqlFromXml(querrys[0].Contenu).Replace("&lt;", "<").Replace("&gt;", ">");

            //AllDataForDashbord = Task.Run(() => GetGridListForDashbOARD()).Result;

            List_BarSeries = BarSeriesModel.GetAllBarItems(Result, REQUETTE);
            List_Pie = PieSerieModel.GetAllPieItems(ResultPie, REQUETTE);











        }
        private static string GetValueFromMeasureElement(XElement measureElement)
        {
            return measureElement != null ? measureElement.Attribute("DataMember").Value : string.Empty;
        }
        public static string ExtractSqlFromXml(string xml)
        {
            string pattern = @"<Sql>(.*?)<\/Sql>";
            Match match = Regex.Match(xml, pattern, RegexOptions.Singleline);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return "Balise <Sql> non trouvée dans la chaîne XML.";
        }
        public static List<QueryModel> GetQueryContents(string xmlString)
        {
            List<QueryModel> queryList = new List<QueryModel>();
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.LoadXml(xmlString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de lecture XML : {ex.Message}");
                // Peut-être que vous voulez gérer l'exception d'une manière spécifique ici.
                // Pour l'exemple, la fonction renvoie une liste vide en cas d'erreur.
                return queryList;
            }

            XmlNodeList queryNodes = xmlDoc.SelectNodes("//Query[@Type='CustomSqlQuery']");

            foreach (XmlNode queryNode in queryNodes)
            {
                string name = queryNode.Attributes["Name"]?.Value;
                string contenu = queryNode.OuterXml;
                queryList.Add(new QueryModel(name, contenu));
            }

            return queryList;
        }
        public  List<LandBarItem> GetListFromDataTable()
        {
            List<LandBarItem> Result = new List<LandBarItem>();
            /*
              IList<LandAreaItem> items = querydirectandindirect.AsEnumerable().Select(row => new LandAreaItem((int)(row.Field<uint?>("agent") is null ? 0 : Convert.ToInt32(row.Field<uint?>("agent"))), row.Field<string>("fullNamAgent"), (int)row.Field<decimal>("TotalHT"))).ToList();
                        var list = items.GroupBy(p => p.Name).Select(g => new LandAreaItem(g.Max(q => q.Id), g.Key, g.Sum(x => x.Number))).ToList();
                        list = list.Where(x => x.Number > 0 && x.Name != "" && x.Id != 0).ToList().OrderByDescending(x => x.Number).ToList();
                        LandAreasTOBbyAgent = new List<LandAreaItem>(list);
             */
            string argument = "Category";
            string value1 = "rest_amount";
            string value2 = "rest_amount";
            string value3 = "rest_amount";
            try
            {
                 //Result = this.AllDataForDashbord.AsEnumerable().Select(row => new LandBarItem(Convert.ToString(row.Field<Object>(argument)), Convert.ToDouble(row.Field<Object>(value1)), Convert.ToDouble(row.Field<Object>(value2)), Convert.ToDouble(row.Field<Object>(value3)))).ToList();
                //Result = Result.GroupBy(p => p.Name).Select(g => new LandBarItem(g.Key, g.Sum(x => x.Number1), g.Sum(x => x.Number2), g.Sum(x => x.Number3))).ToList();
               
            }
            catch (Exception ex)
            {

            }
            return Result;

        }

        static List<string> ExtractPieTags(string inputXaml)
        {
            List<string> chartTags = new List<string>();

            // Utiliser une expression régulière pour extraire les balises <Chart>
            Regex regex = new Regex("<Pie[^>]*>.*?</Pie>", RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(inputXaml);

            foreach (Match match in matches)
            {
                chartTags.Add(match.Value);
            }

            return chartTags;
        }

        static List<string> ExtractChartTags(string inputXaml)
        {
            List<string> chartTags = new List<string>();

            // Utiliser une expression régulière pour extraire les balises <Chart>
            Regex regex = new Regex("<Chart[^>]*>.*?</Chart>", RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(inputXaml);

            foreach (Match match in matches)
            {
                chartTags.Add(match.Value);
            }

            return chartTags;
        }
        public DataTable GetGridListForDashbOARD()
        {
            DataTable dt = new DataTable();
            string sqlCmd = "select * from commercial_partner where id<10;";

            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCmd, DbConnection.con);
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.Fill(dt);
            DbConnection.Deconnecter();
            return dt;
        }
    }
}

public class LandBarItem{

    public string Name { get; set; } = string.Empty;
    public double Number1 { get; set; } = 0;

    public double Number2 { get; set; } = 0;

    public double Number3 { get; set; } = 0;

    public int Id_BarSerie { get; set; }

  


    public LandBarItem()
    {

    }
    public LandBarItem(int id_barserie, string name)
    {
        Name = name;
        Id_BarSerie = id_barserie;

    }

    public LandBarItem(int id_barserie,string name, double number1, double number2, double number3)
    {
        Name = name;
        Number1 = number1;
        Number2 = number2;
        Number3 = number3;
        Id_BarSerie= id_barserie;

    }

    public LandBarItem(int id_barserie, string name, double number1, double number2)
    {
        Name = name;
        Number1 = number1;
        Number2 = number2;
        Id_BarSerie = id_barserie;

    }
    public LandBarItem(int id_barserie, string name, double number1)
    {
        Name = name;
        Number1 = number1;
        Id_BarSerie = id_barserie;

    }
}
