<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ContactBook.Contact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
        .auto-style2 {
            width: 55px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="hfcontactID" runat="server" />
        <table style="width:100%;">
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>

                </td>

            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Mobile"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtmobile" runat="server" TextMode="Number"></asp:TextBox>

                </td>
            </tr>
             <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="Address"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td colspan="2" class="auto-style1">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />

                </td>
                
            </tr>
                
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
                    
                    
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                   
                </td>
            </tr>
                

        </table>
        <br/>
        <asp:GridView ID="gvContact" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                <asp:BoundField DataField="Address" HeaderText="Address" />
                <asp:TemplateField>
                    <ItemTemplate>
                          <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="#000099" NavigateUrl='<%# Eval("ContactID", "Contact.aspx?ContactID={0}") %>' Text="View"></asp:HyperLink>
                        <%--<asp:LinkButton ID="linkView" runat="server" Text='<# %Eval("ContactID") %>' OnClick="lnk_OnClick">View</asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
