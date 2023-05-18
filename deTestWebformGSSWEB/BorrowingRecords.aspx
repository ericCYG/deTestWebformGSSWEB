<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BorrowingRecords.aspx.cs" Inherits="deTestWebformGSSWEB.BorrowingRecords" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="borrowingRecordsRepeater" runat="server">
                <HeaderTemplate>

                    <table class="readTableClass">
                        <thead>
                            <tr>
                                <th>借閱日期</th>
                                <th>借閱人員編號</th>
                                <th>英文姓名</th>
                                <th>中文姓名</th>

                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>

                        <td><%# Eval("LEND_DATE") %></td>
                        <td><%# Eval("KEEPER_ID") %></td>
                        <td><%# Eval("USER_ENAME") %></td>
                        <td><%# Eval("USER_CNAME") %></td>

                    </tr>

                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>

                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
