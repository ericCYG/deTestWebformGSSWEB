
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace deTestWebForm0509
{
    public class Unity
    {

        public static string ExceptionWrong = "";
        public static string ExceptionWrong2 = "";

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=GSSWEB;Integrated Security=True");

        }

        public int exeNonQuery(string sql, List<ParamatsWithValueClass> paramatsList)
        {
            int ifSuccess = -1;
            ExceptionWrong = "";

            using (SqlConnection con = GetSqlConnection())
            {

                SqlCommand scomm = new SqlCommand(sql);
                scomm.Connection = con;
                con.Open();
                SqlTransaction transactionMan = con.BeginTransaction();
                scomm.Transaction = transactionMan;

                try
                {
                    if (paramatsList != null)
                    {
                        foreach (ParamatsWithValueClass item in paramatsList)
                        {
                            scomm.Parameters.AddWithValue(item.key, item.value);
                        }
                    }


                    ifSuccess = scomm.ExecuteNonQuery();
                    transactionMan.Commit();
                }
                catch (Exception ex)
                {
                    transactionMan.Rollback();

                    ExceptionWrong = sql + "。(前面為sql) exeNonQuery 失敗。(錯誤訊息) " + ex.Message;
                    Console.WriteLine(ExceptionWrong);
                }
                finally
                {
                    con.Close();
                }

            }
            return ifSuccess;

        }
        public string exeScalar(string sql, List<ParamatsWithValueClass> paramatsList)
        {
            ExceptionWrong = "";
            string ifSuccess = "";

            using (SqlConnection con = GetSqlConnection())
            {

                SqlCommand scomm = new SqlCommand(sql);
                scomm.Connection = con;
                con.Open();

                SqlTransaction transactionMan = con.BeginTransaction();
                scomm.Transaction = transactionMan;
                try
                {
                    if (paramatsList != null)
                    {
                        foreach (ParamatsWithValueClass item in paramatsList)
                        {
                            scomm.Parameters.AddWithValue(item.key, item.value);
                        }
                    }
                    var ifSuccessVar = scomm.ExecuteScalar();

                    if (ifSuccessVar == null) { ifSuccess = "0"; } else { ifSuccess = ifSuccessVar.ToString(); }

                    transactionMan.Commit();
                }
                catch (Exception ex)
                {
                    transactionMan.Rollback();
                    ExceptionWrong = sql + "。(前面為sql) exeScalar 失敗。(錯誤訊息) " + ex.Message;
                    Console.WriteLine(ExceptionWrong);
                }
                finally
                {
                    con.Close();
                }

            }
            return ifSuccess;

        }
        public DataTable exeReader(string sql, List<ParamatsWithValueClass> paramatsList)
        {
            ExceptionWrong = "";
            DataTable schemaTable = new DataTable();
            using (SqlConnection con = GetSqlConnection())
            {
                SqlCommand scomm = new SqlCommand(sql);
                scomm.Connection = con;
                con.Open();
                SqlTransaction transactionMan = con.BeginTransaction();
                scomm.Transaction = transactionMan;
                SqlDataReader sr = null;
                try
                {
                    if (paramatsList != null)
                    {
                        foreach (ParamatsWithValueClass item in paramatsList)
                        {
                            scomm.Parameters.AddWithValue(item.key, item.value);
                        }
                    }
                    sr = scomm.ExecuteReader();
                    schemaTable.Load(sr);
                    sr.Close();
                    transactionMan.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        sr.Close();
                        transactionMan.Rollback();
                    }
                    catch (Exception eex)
                    {
                        ExceptionWrong = sql + "。(前面為sql) exeReader Rollback 失敗。(錯誤訊息) " + eex.Message;
                        Console.WriteLine(ExceptionWrong);
                        return null;
                    }

                    ExceptionWrong = sql + "。(前面為sql) exeReader 失敗。(錯誤訊息) " + ex.Message;
                    Console.WriteLine(ExceptionWrong);
                }
                finally
                {
                    sr.Close();
                    con.Close();
                }
            }
            return schemaTable;
        }

        public void addLend(string bookid, string Borrower, string bookDetailBOUGHTDateTextbox,string bookDetailLendStatusDropDownList)
        {
            string addLend_sql = "";
            if (exeReader($"select KEEPER_ID from BOOK_LEND_RECORD where KEEPER_ID = {Borrower} and BOOK_ID = {bookid}", null).Rows.Count >= 1)
            {
                addLend_sql = @"
                                                UPDATE [dbo].[BOOK_LEND_RECORD]
                                                   SET 
                                                      [KEEPER_ID] = @K_KEEPER_ID
                                                      ,[LEND_DATE] = @K_LEND_DATE
                                                      ,[CRE_DATE] = @K_CRE_DATE
                                                      ,[CRE_USR] = @K_CRE_USR
                                                      ,[MOD_DATE] = @K_MOD_DATE
                                                      ,[MOD_USR] = @K_MOD_USR
                                                 WHERE  BOOK_ID = @K_bookid
                                                ";
                List<ParamatsWithValueClass> updateLendParamatsWithValueClasses = new List<ParamatsWithValueClass>();
                updateLendParamatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_KEEPER_ID", value = Borrower });
                updateLendParamatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_LEND_DATE", value = Convert.ToDateTime(bookDetailBOUGHTDateTextbox).ToString("yyyy-MM-dd") });
                updateLendParamatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_CRE_DATE", value = Convert.ToDateTime(bookDetailBOUGHTDateTextbox).ToString("yyyy-MM-dd") });
                updateLendParamatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_CRE_USR", value = Borrower });
                updateLendParamatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_MOD_DATE", value = DateTime.Now.ToString("yyyy-MM-dd") });
                updateLendParamatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_MOD_USR", value = Borrower });

                exeNonQuery(addLend_sql, updateLendParamatsWithValueClasses);
            }
            else
            {

                addLend_sql = @"
                                        INSERT INTO [dbo].[BOOK_LEND_RECORD]
                                                   ([BOOK_ID]
                                                   ,[KEEPER_ID]
                                                   ,[LEND_DATE]
                                                   ,[CRE_DATE]
                                                   ,[CRE_USR]
                                                   ,[MOD_DATE]
                                                   ,[MOD_USR])
                                             VALUES
                                                   (
                                                    @K_BOOK_ID
                                                   ,@K_KEEPER_ID
                                                   ,@K_LEND_DATE
                                                   ,@K_CRE_DATE
                                                   ,@K_CRE_USR
                                                   ,@K_MOD_DATE
                                                   ,@K_MOD_USR
                                                    )";


                List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_BOOK_ID", value = bookid });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_KEEPER_ID", value = Borrower });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_LEND_DATE", value = Convert.ToDateTime(bookDetailBOUGHTDateTextbox).ToString("yyyy-MM-dd") });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_CRE_DATE", value = Convert.ToDateTime(bookDetailBOUGHTDateTextbox).ToString("yyyy-MM-dd") });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_CRE_USR", value = Borrower });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_MOD_DATE", value = DateTime.Now.ToString("yyyy-MM-dd") });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_MOD_USR", value = Borrower });

                exeNonQuery(addLend_sql, paramatsWithValueClasses);
            }

            if (bookDetailLendStatusDropDownList == "B") {
                string sql = @"
                                            UPDATE [dbo].[BOOK_DATA]
                                               SET 
                                                  [BOOK_STATUS] = @K_BOOK_STATUS
                                                  ,[BOOK_KEEPER] = @K_BOOK_KEEPER
                                             WHERE BOOK_ID = @K_boodID
                                            ";

                List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_BOOK_STATUS", value = bookDetailLendStatusDropDownList });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_BOOK_KEEPER", value = Borrower });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_boodID", value = bookid });

                exeNonQuery(sql,paramatsWithValueClasses);
            }
        }

    }
}