﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using krDataModels;
namespace readKR
{
    class KrDAO
    {
        private string dataSet_path = "../../DataBase/krcon11.db3";
        private string password = "_krcon2012_";
        private SQLiteConnection cnn;
        private static KrDAO _krDAO;
        //private DataSet krDataSet;
        public static KrDAO getKrDAO()
        {
            if (_krDAO == null)
            {
                _krDAO = new KrDAO();
                _krDAO.krConnect();
            }            
            return _krDAO;
        }
        private void krConnect()
        {
            cnn = new SQLiteConnection("Data Source="+dataSet_path);
            cnn.SetPassword(password);
            cnn.Open();
        }
        public DataTable getTable(string sql)
        {
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, cnn);
            adapter.Fill(dt);
            return dt;
        }     
        public DataSet getDataSet()
        {
            string sql = "select * from KRT_CATEGORY";
            string sql1 = "select * from krt_category_text";
            DataSet krDataSet = new DataSet();
            DataTable dt_krt_category=this.getTable(sql);
            DataTable dt_krt_category_text= this.getTable(sql1);
            krDataSet.Tables.Add(dt_krt_category);
            krDataSet.Tables.Add(dt_krt_category_text);
            return krDataSet;
        }
        public string getStr_CATEGORY(int categoryID)//for test
        {
            StringBuilder sb=new StringBuilder();
            string sql = "select * from KRT_CATEGORY where CATEGORY_ID="+ categoryID;
            SQLiteCommand cmd = new SQLiteCommand(sql, cnn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sb.Append(" CATEGORY_ID:\t" + reader["CATEGORY_ID"] + "\r\n ");
                sb.Append("PARENT_CATEGORY_ID:\t" + reader["PARENT_CATEGORY_ID"] + "\r\n ");
                sb.Append("CATEGORY_TREE:\t" + reader["CATEGORY_TREE"] + "\r\n ");                
                sb.Append("CATEGORY_ORDER:\t" + reader["CATEGORY_ORDER"] + "\r\n ");
                sb.Append("IS_LEAF:\t" + reader["IS_LEAF"] + "\r\n ");
                sb.Append("ADAPTED_BY:\t" + reader["ADAPTED_BY"] + "\r\n ");
                sb.Append("NOTE:\t" + reader["NOTE"] + "\r\n ");
                sb.Append("GT_L:\t" + reader["GT_L"] + "\r\n ");
                sb.Append("GT_H:\t" + reader["GT_H"] + "\r\n ");
                sb.Append("PASS_TOTAL_L:\t" + reader["PASS_TOTAL_L"] + "\r\n ");
                sb.Append("PASS_TOTAL_H:\t" + reader["PASS_TOTAL_H"] + "\r\n ");
                sb.Append("LF_L:\t" + reader["LF_L"] + "\r\n ");
                sb.Append("LF_H:\t" + reader["LF_H"] + "\r\n ");
                sb.Append("GT_L_1:\t" + reader["GT_L_1"] + "\r\n ");
                sb.Append("GT_H_1:\t" + reader["GT_H_1"] + "\r\n ");
                sb.Append("PASS_TOTAL_L_1:\t" + reader["PASS_TOTAL_L_1"] + "\r\n ");
                sb.Append("PASS_TOTAL_H_1:\t" + reader["PASS_TOTAL_H_1"] + "\r\n ");
                sb.Append("LF_L_1:\t" + reader["LF_L_1"] + "\r\n ");
                sb.Append("LF_H_1:\t" + reader["LF_H_1"] + "\r\n ");
                sb.Append("DATE_ADOPTED:\t" + reader["DATE_ADOPTED"] + "\r\n ");
                sb.Append("DATE_EXP:\t" + reader["DATE_EXP"] + "\r\n ");
                sb.Append("DATE_BUILD_L:\t" + reader["DATE_BUILD_L"] + "\r\n ");
                sb.Append("DATE_BUILD_H:\t" + reader["DATE_BUILD_H"] + "\r\n ");
                sb.Append("DATE_EFFECTIVE:\t" + reader["DATE_EFFECTIVE"] + "\r\n ");
                sb.Append("CONTRACT_DATE_L:\t" + reader["CONTRACT_DATE_L"] + "\r\n ");
                sb.Append("CONTRACT_DATE_H:\t" + reader["CONTRACT_DATE_H"] + "\r\n ");
                sb.Append("DELIVERY_DATE_L:\t" + reader["DELIVERY_DATE_L"] + "\r\n ");
                sb.Append("DELIVERY_DATE_H:\t" + reader["DELIVERY_DATE_H"] + "\r\n ");
                sb.Append("REG_DATE:\t" + reader["REG_DATE"] + "\r\n ");
            }
            return sb.ToString();
        }
        public string getStr_CATEGORY_TEXT(int categoryID, string language)//for test
        {
            StringBuilder sb = new StringBuilder();
            string sql = "select * from KRT_CATEGORY_TEXT where CATEGORY_ID=" + categoryID+ " and LOCALE_KEY='" + language+"'";
            SQLiteCommand cmd = new SQLiteCommand(sql, cnn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sb.Append(" CATEGORY_ID:\t" + reader["CATEGORY_ID"] + "\r\n ");
                sb.Append("CATEGORY_TEXT_ID:\t" + reader["CATEGORY_TEXT_ID"] + "\r\n ");
                sb.Append("LOCALE_KEY:\t" + reader["LOCALE_KEY"] + "\r\n ");
                sb.Append("CATEGORY_TITLE:\t" + reader["CATEGORY_TITLE"] + "\r\n ");
                sb.Append("CATEGORY_DESC:\t" + reader["CATEGORY_DESC"] + "\r\n ");
                sb.Append("IS_VISIBLE:\t" + reader["IS_VISIBLE"] + "\r\n ");                
            }
            return sb.ToString();
        }
        public string getXML_CATEGORY_TEXT(int categoryID, string language)//for test
        {
            StringBuilder sb = new StringBuilder();
            string sql = "select * from KRT_CATEGORY_TEXT where CATEGORY_ID=" + categoryID + " and LOCALE_KEY='" + language+ "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, cnn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sb.Append(reader["DATA_XML"]);                
            }
            return sb.ToString();
        }
        /// <summary>
        /// language=en;zh,ko
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public KRT_CATEGORY_TEXT get_CATEGORY_TEXT_byID(int categoryID, string language)
        {
            DataTable dt = new DataTable();
            KRT_CATEGORY_TEXT category_text=null;
            DataRow row = null;
            string sql = "select * from KRT_CATEGORY_TEXT where CATEGORY_ID="+ categoryID+ " and LOCALE_KEY='"+language+"'";
            SQLiteCommand cmd = new SQLiteCommand(sql, cnn);            
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, cnn);
            adapter.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                row = dt.Rows[0];
                category_text = new KRT_CATEGORY_TEXT(row);
            }
            return category_text;
        }
        public KRT_CATEGORY get_CATEGORY_byID(int categoryID)
        {
            DataTable dt = new DataTable();
            KRT_CATEGORY category = null;
            DataRow row = null;
            string sql = "select * from KRT_CATEGORY where CATEGORY_ID=" + categoryID;
            SQLiteCommand cmd = new SQLiteCommand(sql, cnn);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, cnn);
            adapter.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                row = dt.Rows[0];
                category = new KRT_CATEGORY(row);
            }
            return category;
        }
        public KRT_REF_MATERIAL get_REF_MATRRIAL
    }
}
