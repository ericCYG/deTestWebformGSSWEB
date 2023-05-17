<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="deTestWebformGSSWEB.BookDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="App_Start/style/StyleSheetUL.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ul>
                <li>
                    <asp:Label ID="bookDetailBookName" runat="server" Text="書名"></asp:Label>
                    <asp:TextBox ID="bookDetailBookNameTextbox" runat="server" required="required"/>

                </li>
                <li>
                    <asp:Label ID="bookDetailAuthor" runat="server" Text="作者"></asp:Label>
                    <asp:TextBox ID="bookDetailAuthorTextbox" runat="server" />

                </li>
                <li>
                    <asp:Label ID="bookDetailPublisher" runat="server" Text="出版商"></asp:Label>
                    <asp:TextBox ID="bookDetailPublisherTextbox" runat="server" />

                </li>
                <li>
                    <asp:Label ID="bookDetailContent" runat="server" Text="內容簡介"></asp:Label>
                    <textarea id="bookDetailContentTextArea" cols="20" rows="2" name="bookDetailContentTextAreaName" runat="server"></textarea>

                </li>
                <li>
                    <asp:Label ID="bookDetailBOUGHTDate" runat="server" Text="購書日期"></asp:Label>
                    
                    <asp:TextBox  id="bookDetailBOUGHTDateTextbox" runat="server" />

                </li>
                <li>
                    <asp:Label ID="bookDetailBookClass" runat="server" Text="圖書類別"></asp:Label>
                    <asp:DropDownList ID="bookDetailClassDropDownList" required="required" AppendDataBoundItems="true" runat="server" DataSourceID="bookDetail_bookClass" DataTextField="BOOK_CLASS_NAME" DataValueField="BOOK_CLASS_ID">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>

                    <asp:SqlDataSource runat="server" ID="bookDetail_bookClass" ConnectionString='<%$ ConnectionStrings:bookLend %>' SelectCommand="SELECT [BOOK_CLASS_ID], [BOOK_CLASS_NAME] FROM [BOOK_CLASS]"></asp:SqlDataSource>
                </li>
                <li>
                    <asp:Label ID="bookDetailBorrower" runat="server" Text="借閱人"></asp:Label>
                    <asp:DropDownList Enabled="false" ID="bookDetailBorrowerDropDownList" AppendDataBoundItems="true" runat="server" DataSourceID="bookDetailBrowerDropdownlist" DataTextField="name" DataValueField="USER_ID">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>

                    <asp:SqlDataSource runat="server" ID="bookDetailBrowerDropdownlist" ConnectionString='<%$ ConnectionStrings:bookLend %>' SelectCommand="SELECT USER_ENAME + USER_CNAME AS name, USER_ID FROM MEMBER_M"></asp:SqlDataSource>
                </li>
                <li>
                    <asp:Label ID="bookDetailLendStatus" runat="server" Text="借閱狀態"></asp:Label>
                    <asp:DropDownList Enabled="false" ID="bookDetailLendStatusDropDownList" runat="server" DataSourceID="bookDetialLendStatus" DataTextField="CODE_NAME" DataValueField="CODE_ID"></asp:DropDownList>

                    <asp:SqlDataSource runat="server" ID="bookDetialLendStatus" ConnectionString='<%$ ConnectionStrings:bookLend %>' SelectCommand="SELECT [CODE_NAME], [CODE_ID] FROM [BOOK_CODE] WHERE ([CODE_TYPE] = @CODE_TYPE) order by [CODE_ID]">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="BOOK_STATUS" Name="CODE_TYPE" Type="String"></asp:Parameter>
                        </SelectParameters>
                    </asp:SqlDataSource>
                </li>
                <li >
                    <span>&nbsp</span>
                    <input type="submit" id="detailSaveButton" disabled="disabled" value="存檔"  runat="server" />
                    <input type="button" id="detailDeleteButton" disabled="disabled"  value="刪除"  runat="server" />
                </li>
            </ul>
        </div>
    </form>
</body>
</html>
