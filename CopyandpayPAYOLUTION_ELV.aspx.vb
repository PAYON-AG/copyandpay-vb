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
            "&currency=EUR" +
            "&customer.surname=Jones" +
            "&customer.givenName=Jane" +
            "&customer.birthDate=1970-01-01" +
            "&billing.city=Test" +
            "&billing.country=DE" +
            "&billing.street1=123 Test Street" +
            "&billing.postcode=TE1 2ST" +
            "&customer.email=test@test.com" +
            "&customer.phone=1234567890" +
            "&customer.ip=123.123.123.123" +
            "&customParameters[PAYOLUTION_ITEM_PRICE_1]=2.00" +
            "&customParameters[PAYOLUTION_ITEM_DESCR_1]=Test item #1" +
            "&customParameters[PAYOLUTION_ITEM_PRICE_1]=3.00" +
            "&customParameters[PAYOLUTION_ITEM_DESCR_1]=Test item #2" +
            "&testMode=EXTERNAL"

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
