Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class ZPLConverter
    Public Sub ConvertZPLToImage(strZPLCode As String, strSize As String, picImage As PictureBox, strDPM As String)
        Try
            ' Get the ZPL content and convert to bytes
            Dim zpl() As Byte = Encoding.UTF8.GetBytes(strZPLCode)

            ' Create the API URL with proper format
            'strDPM = strDPM.Substring(0, 7)
            'strDPM = strDPM.Replace(" ", "")

            'strDPM = "8"
            Dim apiUrl As String = $"http://api.labelary.com/v1/printers/" & strDPM & "/labels/" & strSize & "/0/"

            apiUrl = $"http://api.labelary.com/v1/printers/12dpmm/labels/" & strSize & "/0/"
            'Select Case strDPM
            '    Case "6 dpmm (152 dpi)"
            '        apiUrl = $"http://api.labelary.com/v1/printers/6dpmm/labels/" & strSize & "/0/"
            '    Case "8 dpmm (203 dpi)"
            '        apiUrl = $"http://api.labelary.com/v1/printers/8dpmm/labels/" & strSize & "/0/"
            '    Case "12 dpmm (300 dpi)"
            '        apiUrl = $"http://api.labelary.com/v1/printers/12dpmm/labels/" & strSize & "/0/"
            '    Case "24 dpmm (600 dpi)"
            '        apiUrl = $"http://api.labelary.com/v1/printers/24dpmm/labels/" & strSize & "/0/"

            'End Select
            ' Set up the request
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "POST"
            request.Accept = "image/png"
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = zpl.Length

            ' Write the ZPL data to request stream
            Using requestStream As Stream = request.GetRequestStream()
                requestStream.Write(zpl, 0, zpl.Length)
            End Using

            ' Get and process the response
            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Using responseStream As Stream = response.GetResponseStream()
                        ' Create a memory stream to store the image data
                        Using memoryStream As New MemoryStream()
                            responseStream.CopyTo(memoryStream)
                            memoryStream.Position = 0

                            ' Set the image to PictureBox
                            picImage.Image = Nothing
                            picImage.Image = Image.FromStream(memoryStream)
                            picImage.Refresh()
                        End Using
                    End Using
                Else
                    MessageBox.Show($"Error: Server returned status code {response.StatusCode}")
                End If
            End Using

        Catch ex As WebException
            MessageBox.Show($"Network error: {ex.Message}")
            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    MessageBox.Show($"Server response: {reader.ReadToEnd()}")
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
    End Sub


    ' Optional: Method to validate ZPL before sending
    Private Function IsValidZPL(zpl As String) As Boolean
        If String.IsNullOrWhiteSpace(zpl) Then Return False
        If Not zpl.Trim().StartsWith("^XA") Then Return False
        If Not zpl.Trim().EndsWith("^XZ") Then Return False
        Return True

    End Function
End Class