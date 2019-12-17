using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ExcelDataReader;
using Task20Locators.Base;

namespace Helpers.Task20Locators.Base
{
    public static class ExcelReader
    {
        // Path to the excel file with test data
        public static string excelPath = Settings.baseDir + @"\TutBy\TrainingTestData.xlsx";

        // Table name in Excel file
        public static string loginTestsTableName = "LoginTests";

        // Data retrieved from Excel file
        private static List<Data> excelDataCollection = new List<Data>();

        // Method to retrieve specified table from specified excel file and return that table
        private static DataTable ExcelToDataTable(string fileName, string tableName)
        {
            using (FileStream stream = File.Open(fileName = excelPath, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    // Get all tables
                    DataTableCollection allTables = result.Tables;

                    // Store specified table as DataTable
                    DataTable table = allTables[tableName];

                    // return expected table
                    return table;
                }
            }
        }

        // Retrieves data from Excel and put in into excelDataCollection object
        public static void CreateDataCollection(string fileName, string tableName)
        {
            DataTable table = ExcelToDataTable(fileName, tableName);

            for (int row = 1; row <= table.Rows.Count; row++)
            {
                string datasetName = null;

                for (int column = 0; column < table.Columns.Count; column++)
                {
                    Data data = new Data();

                    if (column == 0)
                    {
                        datasetName = table.Rows[row - 1][column].ToString();
                        continue;
                    }

                    data.datasetName = datasetName;

                    data.columnName = table.Columns[column].ColumnName;

                    data.cellValue = table.Rows[row - 1][column].ToString();

                    excelDataCollection.Add(data);
                }
            }
        }

        // Retrieves specific values from excelDataCollection object
        public static string GetFromExcel(Dataset datasetName, Field field)
        {
            try
            {
                string value = (from data in excelDataCollection
                                where data.columnName == field.ToString() && data.datasetName == datasetName.ToString()
                                select data.cellValue).SingleOrDefault();

                return value;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }

    // Auxiliary class for values from Excel document
    class Data
    {
        public string datasetName { get; set; }
        public string columnName { get; set; }
        public string cellValue { get; set; }
    }
}
