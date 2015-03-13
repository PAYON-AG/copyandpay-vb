Imports System.Net
Imports System.IO

Partial Class Status
    Inherits System.Web.UI.Page
    Public Result As String = ""

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        getPaymentStatus(Request.Form("id"))
    End Sub

    Private Sub getPaymentStatus(checkoutId As String)
        Dim url As String = "https://test.oppwa.com/v1/checkouts/" + checkoutId + "/payment"
        Dim request As WebRequest = WebRequest.Create(url)
        Dim webresponse As WebResponse = request.GetResponse()
        Dim dataStream As Stream = webresponse.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim response As String = reader.ReadToEnd()
        reader.Close()
        dataStream.Close()
        webresponse.Close()
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim dict As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(response)

        If dict("result")("code").StartsWith("000") Then
            Result = "SUCCESS <br/><br/> Here is the result of your transaction: <br/><br/>"
            Result += response
        Else
            Result = "ERROR <br/><br/> Here is the result of your transaction: <br/><br/>"
            Result += response
        End If
    End Sub
End Class
