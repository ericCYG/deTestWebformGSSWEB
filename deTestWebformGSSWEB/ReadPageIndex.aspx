<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadPageIndex.aspx.cs" Inherits="deTestWebformGSSWEB.ReadPageIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        * {
            /*border: 1px solid;*/
            padding:3px;
        }

        span {
            float: left;
            width: 10em;
            margin-right: 1em;
        }

        ul {
            list-style: none;
            padding-top: 3px;
            padding-left: 2em;
            padding-bottom: 3px;
        }

        li {
            line-height: 24px;
            margin-top: 5px;
            margin-bottom: 5px;
        }

        .readLabel {
            width: 30%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ul>
                <li>
                    <asp:Label Text="圖書維護" runat="server" /><br />
                </li>
                <li>
                    <asp:Label Text="書名" runat="server" /><asp:TextBox ID="BookName_TextBox" runat="server"></asp:TextBox>
                </li>
                <li>
                    <asp:Label Text="圖書類別" runat="server" /><asp:DropDownList ID="BOOK_CLASS_Read_droplist" runat="server" DataSourceID="BOOK_CLASS_Read_DropdownList" DataTextField="BOOK_CLASS_NAME" DataValueField="BOOK_CLASS_NAME"></asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="BOOK_CLASS_Read_DropdownList" ConnectionString='<%$ ConnectionStrings:GSSWEBConnectionString %>' SelectCommand="SELECT [BOOK_CLASS_NAME] FROM [BOOK_CLASS]"></asp:SqlDataSource>
                </li>
                <li>
                    <asp:Label Text="借閱人" runat="server" /><asp:DropDownList ID="MEMBER_M_Read_droplist" runat="server" DataSourceID="MEMBER_M_Read_Dropdownlist" DataTextField="USER_ENAME" DataValueField="USER_ENAME"></asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="MEMBER_M_Read_Dropdownlist" ConnectionString='<%$ ConnectionStrings:GSSWEBConnectionString2 %>' SelectCommand="SELECT [USER_ENAME] FROM [MEMBER_M]"></asp:SqlDataSource>
                </li>
                <li>
                    <asp:Label Text="借閱狀態" runat="server" /><asp:DropDownList ID="BOOK_CODE_Read_droplist" runat="server" DataSourceID="SqlDataSource2" DataTextField="CODE_NAME" DataValueField="CODE_ID"></asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:GSSWEBConnectionString %>' SelectCommand="SELECT [CODE_NAME], [CODE_ID] FROM [BOOK_CODE] WHERE ([CODE_TYPE] = @CODE_TYPE)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="BOOK_STATUS" Name="CODE_TYPE" Type="String"></asp:Parameter>
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource1"></asp:SqlDataSource>
                    <asp:SqlDataSource runat="server" ID="BOOK_CODE_Read_dropdownlist" ConnectionString='<%$ ConnectionStrings:GSSWEBConnectionString3 %>' SelectCommand="SELECT DISTINCT [CODE_NAME] FROM [BOOK_CODE] WHERE ([CODE_TYPE] = @CODE_TYPE)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="BOOK_STATUS" Name="CODE_TYPE" Type="String"></asp:Parameter>
                        </SelectParameters>
                    </asp:SqlDataSource>
                </li>
                <li>
                    <span>&nbsp</span>
                    <asp:Button ID="btn_Read" runat="server" Text="查詢" />
                    <asp:Button ID="btn_Clear" runat="server" Text="清除" />
                    <asp:Button ID="btn_Add" runat="server" Text="新增" />
                </li>
            </ul>
        </div>
        <asp:GridView ID="Read_GridView" runat="server"></asp:GridView>
    </form>
    </body>
</html>
