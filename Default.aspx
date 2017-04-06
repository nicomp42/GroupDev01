<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assignment Github</title>
    <link rel="stylesheet" href="/CSS/skeleton.css" />
    <link rel="stylesheet" href="/CSS/normalize.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <div class="content-footer center-head">
                <h1>Assignment Github: Group collaboration with Github</h1>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="ten columns offset-by-one">
                <h5>Product with the most returns:</h5>
                <asp:Label ID="lblProductWithMostReturns" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="ten columns offset-by-one">
                <h5>Store with the most sales:</h5>
                <asp:Label ID="lblStoreWithMostSales" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="ten columns offset-by-one">
                <h5>Employee who worked the most days:</h5>
                <asp:Label ID="lblEmployeeWhoWorkedMost" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="ten columns offset-by-one">
                <h5>All of the stores in a state:</h5>
            </div>
        </div>
        <div class="row">
            <div class="two columns offset-by-one">
                <asp:Label ID="lblSelectState" runat="server" Text="Select a state:"></asp:Label>
            </div>
            <div class="two columns">
                <asp:DropDownList ID="ddlStates" runat="server"></asp:DropDownList>
            </div>
            <div class="two columns">
                <asp:Button ID="btnSelectState" runat="server" Text="Go" OnClick="btnSelectState_Click" />
            </div>
        </div>
        <div class="row">
            <div class="ten columns offset-by-one">
                <asp:ListBox ID="lbStores" runat="server"></asp:ListBox>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="ten columns offset-by-one">
                <h5>Product that has been returned the most times:</h5>
                <asp:Label ID="lblProductReturnedMostTimes" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <hr />
    </div>
    </form>
</body>
</html>
