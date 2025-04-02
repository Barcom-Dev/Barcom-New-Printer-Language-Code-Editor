Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class ZPLConverter
    Public Sub ConvertZPLToImage(strZPLCode As String, strSize As String, picImage As PictureBox, strDPM As String)
        Try
            ' Validate inputs
            If String.IsNullOrWhiteSpace(strZPLCode) OrElse String.IsNullOrWhiteSpace(strSize) Then
                MessageBox.Show("ZPL code or label size is missing")
                Exit Sub
            End If

            ' Get the ZPL content and convert to bytes
            Dim zpl() As Byte = Encoding.UTF8.GetBytes(strZPLCode)

            ' Extract the dpmm value and calculate the scaling ratio
            Dim dpmValue As String = "8dpmm" ' Default
            Dim baseScalingFactor As Double = 0.47 ' Works perfectly for 8dpmm
            Dim dpiScalingRatio As Double = 1.0 ' Default ratio
            Dim selectedDPM As Integer = 8 ' Default

            ' Parse the selected DPM option
            If strDPM IsNot Nothing AndAlso strDPM <> "" Then
                If strDPM.Contains("6") Then
                    dpmValue = "6dpmm"
                    selectedDPM = 6
                ElseIf strDPM.Contains("8") Then
                    dpmValue = "8dpmm"
                    selectedDPM = 8
                ElseIf strDPM.Contains("12") Then
                    dpmValue = "12dpmm"
                    selectedDPM = 12
                ElseIf strDPM.Contains("24") Then
                    dpmValue = "24dpmm"
                    selectedDPM = 24
                End If
            End If

            ' CORRECTED: Invert the ratio - for LOWER dpi we need LARGER scaling
            ' For HIGHER dpi we need SMALLER scaling
            dpiScalingRatio = 8.0 / selectedDPM

            ' Calculate the adjusted scaling factor based on DPI
            Dim adjustedScalingFactor As Double = baseScalingFactor * dpiScalingRatio

            Dim apiUrl As String = $"http://api.labelary.com/v1/printers/{dpmValue}/labels/{strSize}/0/"

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

                            ' Load the image
                            Dim originalImage As Image = Image.FromStream(memoryStream)

                            ' Create a new scaled bitmap with DPI-adjusted scaling
                            Dim newWidth As Integer = CInt(originalImage.Width * adjustedScalingFactor)
                            Dim newHeight As Integer = CInt(originalImage.Height * adjustedScalingFactor)

                            Dim scaledImage As New Bitmap(newWidth, newHeight)
                            Using g As Graphics = Graphics.FromImage(scaledImage)
                                ' Use high quality settings
                                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                                g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                                g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                                g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

                                ' Draw the image
                                g.DrawImage(originalImage, 0, 0, newWidth, newHeight)
                            End Using

                            ' Set the image to PictureBox
                            picImage.Image = Nothing
                            picImage.Image = scaledImage

                            ' Also resize PictureBox to fit the scaled image
                            picImage.Width = newWidth
                            picImage.Height = newHeight

                            picImage.SizeMode = PictureBoxSizeMode.Normal
                            picImage.Refresh()

                            ' Clean up the original image
                            originalImage.Dispose()
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