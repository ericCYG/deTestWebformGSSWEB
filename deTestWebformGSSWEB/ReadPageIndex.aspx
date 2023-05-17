<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadPageIndex.aspx.cs" Inherits="deTestWebformGSSWEB.ReadPageIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="App_Start/style/StyleSheetUL.css" rel="stylesheet" />
    <style>
        * {
            /*border: 1px solid;*/
            margin:1px;
            padding:3px;
        }
        .readTableClass th,td {

            border:1px solid;

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
                    <asp:Label Text="圖書類別" runat="server" /><asp:DropDownList AppendDataBoundItems="true" ID="BOOK_CLASS_Read_droplist" runat="server" DataSourceID="BOOK_CLASS_Read_DropdownList" DataTextField="BOOK_CLASS_NAME" DataValueField="BOOK_CLASS_NAME">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="BOOK_CLASS_Read_DropdownList" ConnectionString='<%$ ConnectionStrings:GSSWEBConnectionString %>' SelectCommand="SELECT [BOOK_CLASS_NAME] FROM [BOOK_CLASS]"></asp:SqlDataSource>
                </li>
                <li>
                    <asp:Label Text="借閱人" runat="server" /><asp:DropDownList AppendDataBoundItems="true" ID="MEMBER_M_Read_droplist" runat="server" DataSourceID="MEMBER_M_Read_Dropdownlist" DataTextField="USER_ENAME" DataValueField="USER_ENAME">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="MEMBER_M_Read_Dropdownlist" ConnectionString='<%$ ConnectionStrings:GSSWEBConnectionString2 %>' SelectCommand="SELECT [USER_ENAME] FROM [MEMBER_M]"></asp:SqlDataSource>
                </li>
                <li>
                    <asp:Label Text="借閱狀態" runat="server" /><asp:DropDownList AppendDataBoundItems="true" ID="BOOK_CODE_Read_droplist" runat="server" DataSourceID="SqlDataSource2" DataTextField="CODE_NAME" DataValueField="CODE_ID">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
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
                 <a href="BookDetail.aspx?addMode=1">新</a>
                </li>
            </ul>
        </div>
        <asp:Repeater ID="indexReadRepeater" runat="server">
            <HeaderTemplate>

                 <table id="indexReadTable" class="readTableClass">
                        <thead>
                            <tr>
                                <th>圖書類別</th>
                                <th>書名</th>
                                <th>購書日期</th>
                                <th>借閱狀態</th>
                                <th>借閱人</th>
                                <th>&nbsp</th>
                            </tr>
                        </thead>
                        <tbody>

            </HeaderTemplate>
            <ItemTemplate>

                <tr>
                        <td><%# Eval("圖書類別") %></td>
                        <td><a href='<%# Eval("BOOK_ID" , "BookDetail.aspx?BOOK_ID={0}") %>' class="w3-button w3-black"> <%# Eval("書名") %></a></td>
                        <td><%# Eval("購書日期") %></td>
                        <td><%# Eval("借閱狀態") %></td>
                        <td><%# Eval("借閱人") %></td>
                        <td><input type="button" value="借閱記錄"/>
                        <a href='<%# Eval("BOOK_ID" , "BookDetail.aspx?BOOK_ID={0}&editMode=1") %>' class="w3-button w3-black">編輯</a>
                            <input type="button" value="刪除"/></td>
                    </tr>

            </ItemTemplate>
            <FooterTemplate>

                  </tbody>
                    </table>

            </FooterTemplate>
        </asp:Repeater>
    </form>
    </body>
</html>
