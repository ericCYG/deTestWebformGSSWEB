using deTestWebForm0509;
using System;
using System.Collections.Generic;
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
            //Repeater1.DataSource = new Unity().exeReader(@"select * from [dbo].[deTestWebFormMember]", null);
            //Repeater1.DataBind();

            //new Unity().exeNonQuery(sqlString, paramatsWithValueClasses);

            //Response.Redirect("q2.aspx");

            string sqlString = @"
                                                select  bd.BOOK_ID,bc.BOOK_CLASS_NAME 圖書類別,bd.BOOK_NAME 書名,format( bd.BOOK_BOUGHT_DATE,'yyyy/MM/dd') 購書日期,bcode.CODE_NAME 借閱狀態, ISNULL( mm.USER_ENAME,'') 借閱人
                                                from [dbo].[BOOK_CLASS] bc 
                                                join [dbo].[BOOK_DATA] bd on bc.BOOK_CLASS_ID = bd.BOOK_CLASS_ID
                                                join [dbo].[BOOK_CODE] bcode on bcode.CODE_ID = bd.BOOK_STATUS and bcode.CODE_TYPE = 'BOOK_STATUS'
                                                left JOIN [dbo].[BOOK_LEND_RECORD] LEND ON LEND.BOOK_ID = BD.BOOK_ID
                                                left JOIN [dbo].[MEMBER_M] MM ON MM.USER_ID = LEND.KEEPER_ID 
                                                where bd.BOOK_NAME  like '%'+@K_bookName+'%' 
                                                and bc.BOOK_CLASS_NAME = @K_className
                                                or bd.BOOK_STATUS = @K_bookStatus
                                                or mm.USER_ENAME = @K_userEname
                                                ";
            List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_bookName", value = BookName_TextBox.Text });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_className", value = BOOK_CLASS_Read_droplist.SelectedValue});
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_bookStatus", value = BOOK_CODE_Read_droplist.SelectedValue });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_userEname", value = MEMBER_M_Read_droplist.SelectedValue });


            indexReadRepeater.DataSource = new Unity().exeReader(sqlString, paramatsWithValueClasses);
            indexReadRepeater.DataBind();
        }



    }
}