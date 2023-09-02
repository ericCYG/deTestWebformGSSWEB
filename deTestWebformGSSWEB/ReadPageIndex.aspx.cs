using deTestWebForm0509;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace deTestWebformGSSWEB
{
    public partial class ReadPageIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();

            

        }

        private void testFormLoad()
        {
           
                string sqlString = @"
                                                select  bd.BOOK_ID,bc.BOOK_CLASS_NAME 圖書類別,bd.BOOK_NAME 書名,format( bd.BOOK_BOUGHT_DATE,'yyyy/MM/dd') 購書日期,bcode.CODE_NAME 借閱狀態, ISNULL( mm.USER_ENAME,'') 借閱人
                                                from [dbo].[BOOK_CLASS] bc 
                                                join [dbo].[BOOK_DATA] bd on bc.BOOK_CLASS_ID = bd.BOOK_CLASS_ID
                                                join [dbo].[BOOK_CODE] bcode on bcode.CODE_ID = bd.BOOK_STATUS and bcode.CODE_TYPE = 'BOOK_STATUS'
                                                left JOIN [dbo].[BOOK_LEND_RECORD] LEND ON LEND.BOOK_ID = BD.BOOK_ID
                                                left JOIN [dbo].[MEMBER_M] MM ON MM.USER_ID = LEND.KEEPER_ID 
                                                where bd.BOOK_NAME  like '%'+@K_bookName+'%' ";
                if (BOOK_CLASS_Read_droplist.SelectedIndex != 0)
                {
                    sqlString += @"and bc.BOOK_CLASS_NAME = @K_className ";
                }
                if (BOOK_CODE_Read_droplist.SelectedIndex != 0)
                {
                    sqlString += @"and bd.BOOK_STATUS = @K_bookStatus ";
                }
                if (MEMBER_M_Read_droplist.SelectedIndex != 0)
                {
                    sqlString += @"and mm.USER_ENAME = @K_userEname";
                }
            sqlString += @" "; // 神聖的空白
        sqlString += @"order by bd.BOOK_BOUGHT_DATE desc";


                List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_bookName", value = BookName_TextBox.Text });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_className", value = BOOK_CLASS_Read_droplist.SelectedValue });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_bookStatus", value = BOOK_CODE_Read_droplist.SelectedValue });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_userEname", value = MEMBER_M_Read_droplist.SelectedValue });


                indexReadRepeater.DataSource = new Unity().exeReader(sqlString, paramatsWithValueClasses);
                indexReadRepeater.DataBind();
            
        }

        protected void btn_Read_Click(object sender, EventArgs e)
        {
            testFormLoad();
        }

        protected void DeleteCustomer(object sender, EventArgs e)
        {
            string deleteTarget = (sender as LinkButton).CommandArgument;

            string sqlIfLend = $" select BOOK_ID  from BOOK_DATA where BOOK_ID = {deleteTarget} and BOOK_STATUS = 'B' ";
            if (new Unity().exeScalar(sqlIfLend, null) != "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "訊息", "alert('有人借出中，不可刪除')", true);
                return;
            }

            string sqlString = @" delete  from BOOK_DATA where BOOK_ID = @K_deleteID ";
            List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_deleteID", value = deleteTarget });

            new Unity().exeNonQuery(sqlString, paramatsWithValueClasses);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "刪除成功", "alert('刪除成功')", true);
            Response.Write("<Script language='JavaScript'>alert('刪除成功');</Script>");
            Server.Transfer("ReadPageIndex.aspx");
        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            BookName_TextBox.Text = string.Empty;
            BOOK_CLASS_Read_droplist.SelectedIndex = 0;
            MEMBER_M_Read_droplist.SelectedIndex = 0;
            BOOK_CODE_Read_droplist.SelectedIndex=0;
            indexReadRepeater.DataSource = null;
            indexReadRepeater.DataBind();
        }
    }
}