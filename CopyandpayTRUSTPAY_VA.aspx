<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CopyandpayTRUSTPAY_VA.aspx.vb" Inherits="_Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>COPYandPAY VB</title>
    <script src="https://test.oppwa.com/v1/paymentWidgets.js?checkoutId=<%=CheckoutId%>"></script>
</head>
<body>
    <form action="http://localhost:51521/Status.aspx" class="paymentWidgets">TRUSTPAY_VA</form>
</body>
</html>
