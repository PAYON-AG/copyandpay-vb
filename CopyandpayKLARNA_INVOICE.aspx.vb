Imports System
Imports System.IO
Imports System.Net
Imports System.Text

Partial Class _Default
    Inherits System.Web.UI.Page

    Public CheckoutId As String = ""

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        CheckoutId = prepareCheckout()
    End Sub

    Public Function prepareCheckout() As String
        Dim url As String = "https://test.oppwa.com/v1/checkouts"
        Dim data As String = "authentication.userId=8a8294174b7ecb28014b9699220015cc" +
            "&authentication.password=sy6KJsT8" +
            "&authentication.entityId=8a8294174b7ecb28014b9699a3cf15d1" +
            "&paymentType=PA" +
            "&amount=10.00" +
            "&currency=SEK" +
            "&billing.country=SE" +
            "&customer.givenName=Joe" +
            "&customer.surname=Doe" +
            "&cart.items[0].merchantItemId=1" +
            "&cart.items[0].discount=0.00" +
            "&cart.items[0].quantity=5" +
            "&cart.items[0].name=Product 1" +
            "&cart.items[0].price=1.00" +
            "&cart.items[0].tax=6.00" +
            "&customParameters[KLARNA_CART_ITEM1_FLAGS]=32" +
            "&cart.items[1].merchantItemId=2" +
            "&cart.items[1].discount=0.00" +
            "&cart.items[1].quantity=1" +
            "&cart.items[1].name=Product 2" +
            "&cart.items[1].price=1.00" +
            "&cart.items[1].tax=6.00" +
            "&customParameters[KLARNA_CART_ITEM2_FLAGS]=32"

        Dim request As WebRequest = WebRequest.Create(url)
        request.Method = "POST"
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(data)
        request.ContentType = "application/x-www-form-urlencoded"
        request.ContentLength = byteArray.Length
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(byteArray, 0, byteArray.Length)
        dataStream.Close()
        Dim webresponse As WebResponse = request.GetResponse()
        dataStream = webresponse.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim response As String = reader.ReadToEnd()
        reader.Close()
        dataStream.Close()
        webresponse.Close()
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim dict As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(response)

        Return dict("id")
    End Function

End Class
