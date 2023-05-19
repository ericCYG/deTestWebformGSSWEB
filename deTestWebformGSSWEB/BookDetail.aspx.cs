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
    public partial class BookDetail : System.Web.UI.Page
    {
        string editModeOn, addMode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["addMode"] == "1" ) {
                addMode = "1";
                detailSaveButton.Enabled = true;
            }
            editModeOn = Request.QueryString["editMode"];
            if (editModeOn == "1")
            {
                bookDetailBorrowerDropDownList.Enabled = true;
                bookDetailLendStatusDropDownList.Enabled = true;
                detailSaveButton.Enabled = true;
                detailDeleteButton.Disabled = false;
            }
            else {
                bookDetailBorrowerDropDownList.Enabled = false;
                bookDetailLendStatusDropDownList.Enabled = false;
            }

            string bookID = "";
            if (Page.IsPostBack == false && Request.QueryString["BOOK_ID"] != null)
            {

                bookID = Request.QueryString["BOOK_ID"];
            }

            string sqlString = @"
                                                select bd.BOOK_NAME,bd.BOOK_AUTHOR,bd.BOOK_PUBLISHER,bd.BOOK_NOTE,bd.BOOK_BOUGHT_DATE,bc.BOOK_CLASS_id,bd.BOOK_STATUS ,bd.BOOK_KEEPER
                                                from BOOK_DATA bd 
                                                join BOOK_CLASS bc on bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID
                                             left   join BOOK_LEND_RECORD lend on lend.BOOK_ID = bd.BOOK_ID
                                                where bd.BOOK_ID = @K_bookID
                                                ";
            List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_bookID", value = bookID });

            DataTable dt = new Unity().exeReader(sqlString, paramatsWithValueClasses);
            foreach (DataRow item in dt.Rows)
            {

                bookDetailBookNameTextbox.Text = item["BOOK_NAME"].ToString();
                bookDetailAuthorTextbox.Text = item["BOOK_AUTHOR"].ToString();
                bookDetailPublisherTextbox.Text = item["BOOK_PUBLISHER"].ToString();
                bookDetailContentTextArea.InnerText = item["BOOK_NOTE"].ToString();
                bookDetailBOUGHTDateTextbox.Text = Convert.ToDateTime( item["BOOK_BOUGHT_DATE"]).ToString("yyyy-MM-dd");
                bookDetailClassDropDownList.SelectedValue = item["BOOK_CLASS_id"].ToString();
                //bookDetailClassDropDownList.Items.FindByText(item["BOOK_CLASS_NAME"].ToString()).Selected = true;
                //bookDetailClassDropDownList.Items.FindByValue("SC").Selected = true;
                //bookDetailClassDropDownList.SelectedValue = "SC";
                if (editModeOn == "1") {
                    bookDetailLendStatusDropDownList.SelectedValue = item["BOOK_STATUS"].ToString();
                    bookDetailBorrowerDropDownList.SelectedValue = item["BOOK_KEEPER"].ToString();
                }
            }

        }

        protected void detailSaveButton_Click(object sender, EventArgs e)
        {
            string detailSaveButton_Click_new_SqlString = @"
                                                    INSERT INTO [dbo].[BOOK_DATA]
                                                               ([BOOK_NAME]
                                                               ,[BOOK_CLASS_ID]
                                                               ,[BOOK_AUTHOR]
                                                               ,[BOOK_BOUGHT_DATE]
                                                               ,[BOOK_PUBLISHER]
                                                               ,[BOOK_NOTE]
                                                               ,[BOOK_STATUS]
                                                               ,[BOOK_KEEPER]
                                                               ,[BOOK_AMOUNT]
                                                               ,[CREATE_DATE]
                                                               ,[CREATE_USER]
                                                               ,[MODIFY_DATE]
                                                               ,[MODIFY_USER])
                                                         VALUES
                                                               (
                                                                @K_BOOK_NAME
                                                               ,@K_BOOK_CLASS_ID
                                                               ,@K_BOOK_AUTHOR
                                                               ,@K_BOOK_BOUGHT_DATE
                                                               ,@K_BOOK_PUBLISHER
                                                               ,@K_BOOK_NOTE
                                                               ,@K_BOOK_STATUS
                                                               ,@K_BOOK_KEEPER
                                                               ,@K_BOOK_AMOUNT
                                                               ,@K_CREATE_DATE
                                                               ,@K_CREATE_USER
                                                               ,@K_MODIFY_DATE
                                                               ,@K_MODIFY_USER
                                                    )";


            string detailSaveButton_Click_edit_SqlString = @"
                                                                                                    UPDATE [dbo].[BOOK_DATA]
                                                                                                    SET [BOOK_NAME] = @K_BOOK_NAME
                                                                                                    ,[BOOK_CLASS_ID] = @K_BOOK_CLASS_ID
                                                                                                    ,[BOOK_AUTHOR] = @K_BOOK_AUTHOR
                                                                                                    ,[BOOK_BOUGHT_DATE] = @K_BOOK_BOUGHT_DATE
                                                                                                    ,[BOOK_PUBLISHER] = @K_BOOK_PUBLISHER
                                                                                                    ,[BOOK_NOTE] = @K_BOOK_NOTE
                                                                                                    ,[BOOK_STATUS] = @K_BOOK_STATUS
                                                                                                    ,[BOOK_KEEPER] = @K_BOOK_KEEPER
                                                                                                    ,[BOOK_AMOUNT] = @K_BOOK_AMOUNT
                                                                                                    ,[CREATE_DATE] = @K_CREATE_DATE
                                                                                                    ,[CREATE_USER] = @K_CREATE_USER
                                                                                                    ,[MODIFY_DATE] = @K_MODIFY_DATE
                                                                                                    ,[MODIFY_USER] = @K_MODIFY_USER
                                                                                                    WHERE  BOOK_ID = @K_bookid
                                                                                                    ";


            List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_BOOK_NAME", value = bookDetailBookNameTextbox.Text });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_BOOK_CLASS_ID", value = bookDetailClassDropDownList.SelectedValue });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_BOOK_AUTHOR", value = bookDetailAuthorTextbox.Text });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_BOOK_BOUGHT_DATE", value = bookDetailBOUGHTDateTextbox.Text });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_BOOK_PUBLISHER", value = bookDetailPublisherTextbox.Text });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_BOOK_NOTE", value = bookDetailContentTextArea.Value });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_BOOK_STATUS", value = addMode == "1"? "A" : editModeOn == "1" ? bookDetailLendStatusDropDownList.SelectedValue : "A" });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_BOOK_KEEPER", value = addMode =="1" ? "" : editModeOn == "1" ? (bookDetailLendStatusDropDownList.SelectedValue == "A" ? "": bookDetailBorrowerDropDownList.SelectedValue) : "" });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_BOOK_AMOUNT", value = "" });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_CREATE_DATE", value = DateTime.Now.ToString("yyyy-MM-dd") }) ;
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_CREATE_USER", value = "0753" });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_MODIFY_DATE", value = DateTime.Now.ToString("yyyy-MM-dd") });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_MODIFY_USER", value = "0753" });
            if (editModeOn == "1") {
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_bookid", value = Request.QueryString["BOOK_ID"]});
            }

            if (addMode == "1") { 
            new Unity().exeNonQuery(detailSaveButton_Click_new_SqlString, paramatsWithValueClasses);
            }
            if (editModeOn == "1") {
                new Unity().exeNonQuery(detailSaveButton_Click_edit_SqlString, paramatsWithValueClasses);

                if (bookDetailBorrowerDropDownList.SelectedIndex != 0) {
                    new Unity().addLend(
                        Request.QueryString["BOOK_ID"],
                        bookDetailBorrowerDropDownList.SelectedValue,
                        bookDetailBOUGHTDateTextbox.Text,
                        bookDetailLendStatusDropDownList.SelectedValue
                        );
                }

            }

            //Server.Transfer("ReadPageIndex.aspx");
            Response.Redirect("ReadPageIndex.aspx");
        }
    }
}