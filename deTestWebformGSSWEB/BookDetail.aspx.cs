using deTestWebForm0509;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace deTestWebformGSSWEB
{
    public partial class BookDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string editModeOn = Request.QueryString["editMode"];
            if (editModeOn == "1")
            {
                bookDetailBorrowerDropDownList.Enabled = true;
                bookDetailLendStatusDropDownList.Enabled = true;
            }
            else {
                bookDetailBorrowerDropDownList.Enabled = false;
                bookDetailLendStatusDropDownList.Enabled = false;
            }

            string bookName = "";
            if (Page.IsPostBack == false)
            {

                bookName = Request.QueryString["BOOK_ID"];
            }

            string sqlString = @"
                                                select bd.BOOK_NAME,bd.BOOK_AUTHOR,bd.BOOK_PUBLISHER,bd.BOOK_NOTE,bd.BOOK_BOUGHT_DATE,bc.BOOK_CLASS_id,bd.BOOK_STATUS ,lend.KEEPER_ID
                                                from BOOK_DATA bd 
                                                join BOOK_CLASS bc on bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID
                                                join BOOK_LEND_RECORD lend on lend.BOOK_ID = bd.BOOK_ID
                                                where bd.BOOK_ID = @K_bookID
                                                ";
            List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_bookID", value = bookName });

            DataTable dt = new Unity().exeReader(sqlString, paramatsWithValueClasses);
            foreach (DataRow item in dt.Rows)
            {

                bookDetailBookNameTextbox.Text = item["BOOK_NAME"].ToString();
                bookDetailAuthorTextbox.Text = item["BOOK_AUTHOR"].ToString();
                bookDetailPublisherTextbox.Text = item["BOOK_PUBLISHER"].ToString();
                bookDetailContentTextArea.InnerText = item["BOOK_NOTE"].ToString();
                bookDetailBOUGHTDateTextbox.Text = item["BOOK_BOUGHT_DATE"].ToString();
                bookDetailClassDropDownList.SelectedValue = item["BOOK_CLASS_id"].ToString();
                //bookDetailClassDropDownList.Items.FindByText(item["BOOK_CLASS_NAME"].ToString()).Selected = true;
                //bookDetailClassDropDownList.Items.FindByValue("SC").Selected = true;
                //bookDetailClassDropDownList.SelectedValue = "SC";
                if (editModeOn == "1") {
                    bookDetailLendStatusDropDownList.SelectedValue = item["BOOK_STATUS"].ToString();
                    bookDetailBorrowerDropDownList.SelectedValue = item["KEEPER_ID"].ToString();
                }
            }

        }
    }
}