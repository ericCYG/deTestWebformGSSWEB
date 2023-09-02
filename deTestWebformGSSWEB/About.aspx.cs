using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace deTestWebformGSSWEB
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["md5"] == null && Request.QueryString["md5"] != "")
            {
                ViewState["md5"] = Request.QueryString["md5"];

            }
            else if (ViewState["md5"] != null)
            {
                Label1.Text = "viewstateMD5Null";
            }
            else if (Request.QueryString["md5"] == "")
            {
                Label2.Text = "requstQueryStringmd5null";
            }

            Label3.Text = ViewState["md5"].ToString();
        }
    }
}