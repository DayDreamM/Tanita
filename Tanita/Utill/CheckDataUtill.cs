using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows;
using Tanita.model;
using XWSJ.Common;

namespace Tanita.Utill
{
    class CheckDataUtill
    {
        public Dictionary<string, string> GetDicByBackStr(string backStr)
        {
            char[] separator = new char[] { ',' };
            string[] strArray = backStr.Replace("\"", "").Split(separator);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(backStr))
            {
                for (int i = 0; i < strArray.Length-1; i++)
                {
                    string inputData = strArray[i];
                    if (ValidateHelper.IsNumber(inputData) || (inputData.IndexOf("~") > -1))
                    {
                        i++;
                    }
                    else
                    {
                        dictionary.Add(strArray[i], strArray[i + 1]);
                        i++;
                    }
                }
            }
            return dictionary;
        }
        public DataTable DBExcelToDataTable(string pathName, string sheetName = "")
        {
            DataTable dt = new DataTable();
            string ConnectionString = string.Empty;
            FileInfo file = new FileInfo(pathName);
            if (!file.Exists) { throw new Exception("文件不存在"); }
            string extension = file.Extension;
            switch (extension)
            {
                case ".xls":
                    ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=no;IMEX=1;'";
                    break;
                case ".xlsx":
                    ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathName + ";Extended Properties='Excel 12.0;HDR=no;IMEX=1;'";
                    break;
                default:
                    ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=no;IMEX=1;'";
                    break;
            }
            OleDbConnection con = new OleDbConnection(ConnectionString);
            try
            {
                con.Open();
                if (sheetName != "") 
                {   
                    OleDbCommand cmd = new OleDbCommand("select * from [" + sheetName + "$] where F1 is not null ", con);
                    OleDbDataAdapter apt = new OleDbDataAdapter(cmd);
                    try
                    {
                        apt.Fill(dt);
                    }
                    catch (Exception ex) { throw new Exception("该Excel文件中未找到指定工作表名," + ex.Message); }
                    dt.TableName = sheetName;
                }
                else
                {
                    //默认读取第一个有数据的工作表
                    var tables = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { });
                    if (tables.Rows.Count == 0)
                    { throw new Exception("Excel必须包含一个表"); }
                    foreach (DataRow row in tables.Rows)
                    {
                        string strSheetTableName = row["TABLE_NAME"].ToString();
                        //过滤无效SheetName   
                        if (strSheetTableName.Contains("$") && strSheetTableName.Replace("'", "").EndsWith("$"))
                        {
                            DataTable tableColumns = con.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, strSheetTableName, null });
                            if (tableColumns.Rows.Count < 2)                     //工作表列数
                                continue;
                            OleDbCommand cmd = new OleDbCommand("select * from [" + strSheetTableName + "] where F1 is not null", con);
                            OleDbDataAdapter apt = new OleDbDataAdapter(cmd);
                            apt.Fill(dt);
                            dt.TableName = strSheetTableName.Replace("$", "").Replace("'", "");
                            break;
                        }
                    }
                }
                if (dt.Rows.Count < 2)
                    throw new Exception("表必须包含数据");
                //重构字段名
                DataRow headRow = dt.Rows[0];
                foreach (DataColumn c in dt.Columns)
                {
                    string headValue = (headRow[c.ColumnName] == DBNull.Value || headRow[c.ColumnName] == null) ? "" : headRow[c.ColumnName].ToString().Trim();
                    if (headValue.Length == 0)
                    { throw new Exception("必须输入列标题"); }
                    if (dt.Columns.Contains(headValue))
                    { throw new Exception("不能用重复的列标题：" + headValue); }
                    c.ColumnName = headValue;
                }
                dt.Rows.RemoveAt(0);
                return dt;
            }
            catch (Exception)
            {
                MessageBox.Show("批量导入Excel文件中存在为空的必填项,请检查后再导入。", "提示");
                return null;
            }
            finally
            { con.Close(); }
        }
        public bool GetCheckDataByBackStr(string backStr, out StudentCheckData checkData, out string errMsg)
        {
            backStr = backStr.Replace(",DT,", ",Da,");
            backStr = backStr.Replace("\"", "");
            Dictionary<string, string> dicByBackStr = GetDicByBackStr(backStr);
            errMsg = "";
            checkData = new StudentCheckData();
            string jsonByDic = JsonHelper.GetJsonByDic(dicByBackStr);
            checkData = JsonHelper.DeserializeAnonymousType(jsonByDic, checkData, out errMsg);
            bool flag = !string.IsNullOrEmpty(errMsg);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                checkData.jsonContent = backStr;
                checkData.Pdate = DateTime.Now;
                result = true;
            }
            return result;
        }
        public bool GetTeacherCheckDataByBackStr(string backStr, out TeacherCheckData checkData, out string errMsg)
        {
            backStr = backStr.Replace(",DT,", ",Da,");
            backStr = backStr.Replace("\"", "");
            Dictionary<string, string> dicByBackStr = GetDicByBackStr(backStr);
            errMsg = "";
            checkData = new TeacherCheckData();
            string jsonByDic = JsonHelper.GetJsonByDic(dicByBackStr);
            checkData = JsonHelper.DeserializeAnonymousType(jsonByDic, checkData, out errMsg);
            bool flag = !string.IsNullOrEmpty(errMsg);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                checkData.jsonContent = backStr;
                checkData.Pdate = DateTime.Now;
                result = true;
            }
            return result;
        }

        public string DictionaryListToString(Dictionary<string, string> listInfo)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string key in listInfo.Keys)
            {
                builder.Append(key + "," + listInfo[key] + ",");
            }
            return builder.ToString();
        }
    }
}
