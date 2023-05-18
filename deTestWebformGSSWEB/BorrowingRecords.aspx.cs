using deTestWebForm0509;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace deTestWebformGSSWEB
{
    public partial class BorrowingRecords : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string bookID = Request.QueryString["BOOK_ID"];

            string sql = @"
                                        select * from BOOK_LEND_RECORD join MEMBER_M on MEMBER_M.USER_ID = BOOK_LEND_RECORD.KEEPER_ID where BOOK_ID = @K_bookID
                                        
                                        ";
            List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_bookID", value = bookID });

            DataTable dt = new Unity().exeReader(sql, paramatsWithValueClasses);

            borrowingRecordsRepeater.DataSource = new Unity().exeReader(sql, paramatsWithValueClasses);
            borrowingRecordsRepeater.DataBind();

        }
    }
}