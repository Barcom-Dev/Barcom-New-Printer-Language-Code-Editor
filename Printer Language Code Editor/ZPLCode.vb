Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class ZPLConverter
    Public Function GetIntegerDPMValue(strDPM As String) As Integer
        If strDPM IsNot Nothing AndAlso strDPM <> "" Then
                If strDPM.Contains("6") Then
                    Return 6
                ElseIf strDPM.Contains("8") Then
                    Return 8
                ElseIf strDPM.Contains("12") Then
                    Return 12
                ElseIf strDPM.Contains("24") Then
                    Return 24
                End If
            End If
        Return 8
    End Function

    Public Function GetStringDPMValue(strDPM As String) As String
        Dim intDPM As Integer = Me.GetIntegerDPMValue(strDPM)
        Return intDPM.ToString() & "dpmm"
    End Function

    ' Fetches Image Data from Labelary API. When successful returns a 
    ' Stream object containing the image data. Else Nothing.
    Public Function FetchImageDataFromAPI(strZPLCode As String, strSize As String, dpmValue As String, format As String, quality As String) As MemoryStream
        Dim apiUrl As String = $"http://api.labelary.com/v1/printers/{dpmValue}/labels/{strSize}/0/"

        If format = "application/multi-page-pdf" Then
            apiUrl = $"http://api.labelary.com/v1/printers/{dpmValue}/labels/{strSize}/"
            format = "application/pdf"
        End If

        Console.WriteLine("API URL: " & apiUrl)
        
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(apiUrl), HttpWebRequest)
        request.Method = "POST"
        request.Accept = If(format IsNot Nothing AndAlso format <> "", format, "image/png")
        request.ContentType = "application/x-www-form-urlencoded"

        request.Headers.Add("X-Linter", "On")

        If (format Is Nothing OrElse format.Equals("image/png")) AndAlso quality IsNot Nothing Then
            If quality.Equals("Bitonal") Then
                request.Headers.Add("X-Quality", "Bitonal")
            End If
            If quality.Equals("Grayscale") Then
                request.Headers.Add("X-Quality", "Grayscale")
            End If
        End If

        Try
            Dim zpl() As Byte = Encoding.UTF8.GetBytes(strZPLCode)
            request.ContentLength = zpl.Length

            Using requestStream As Stream = request.GetRequestStream()
                requestStream.Write(zpl, 0, zpl.Length)
            End Using

            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                Using responseStream As Stream = response.GetResponseStream()
                    If response.Headers.Get("X-Warnings") IsNot Nothing Then
                        Console.WriteLine("ZPL code lint errors: " & response.Headers.Get("X-Warnings"))
                    End If
                    If response.StatusCode = HttpStatusCode.OK Then
                        Console.WriteLine("OK Response: " & response.StatusCode & " " & response.StatusDescription)
                        Dim memoryStream As New MemoryStream()
                        responseStream.CopyTo(memoryStream)
                        memoryStream.Position = 0
                        Return memoryStream
                    Else
                        Console.WriteLine("Response Issue: " & response.StatusCode & " " & response.StatusDescription)
                        Return Nothing
                    End If
                End Using
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

        Return Nothing
    End Function


    Public Sub ConvertZPLToImage2(strZPLCode As String, strSize As String, picImage As PictureBox, strDPM As String)
        Try
            If String.IsNullOrWhiteSpace(strZPLCode) OrElse String.IsNullOrWhiteSpace(strSize) Then
                MessageBox.Show("ZPL code or label size is missing")
                Return
            End If

            Dim dpmValue As String = GetStringDPMValue(strDPM)
            Dim selectedDPM As Integer = GetIntegerDPMValue(strDPM)

            Dim imageStream As Stream = FetchImageDataFromAPI(strZPLCode, strSize, dpmValue, "image/png", "grayscale")

            If imageStream Is Nothing Then
                MessageBox.Show("Failed to retrieve image from API")
                Return
            End If

            ' Extract the dpmm value and calculate the scaling ratio
            Dim baseScalingFactor As Double = 0.47 ' Works perfectly for 8dpmm
            Dim dpiScalingRatio As Double = 8.0 / selectedDPM ' Default ratio
            Dim adjustedScalingFactor As Double = baseScalingFactor * dpiScalingRatio

            Dim image As Image = Image.FromStream(imageStream)

            picImage.Image = image
            picImage.SizeMode = PictureBoxSizeMode.Normal
            picImage.Refresh()

            Dim newWidth As Integer = CInt(image.Width * adjustedScalingFactor)
            Dim newHeight As Integer = CInt(image.Height * adjustedScalingFactor)

            Dim scaledImage As New Bitmap(newWidth, newHeight)
            Using g As Graphics = Graphics.FromImage(scaledImage)
                ' Use high quality settings
                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

                ' Draw the image
                g.DrawImage(image, 0, 0, newWidth, newHeight)
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
            image.Dispose()

        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}\n" & ex.StackTrace)
        End Try
    End Sub

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
            Dim baseScalingFactor As Double = 0.47 ' Works perfectly for 8dpmm
            Dim dpiScalingRatio As Double = 1.0 ' Default ratio
            
            Dim dpmValue As String = GetStringDPMValue(strDPM)
            Dim selectedDPM As Integer = GetIntegerDPMValue(strDPM)

            ' CORRECTED: Invert the ratio - for LOWER dpi we need LARGER scaling
            ' For HIGHER dpi we need SMALLER scaling
            dpiScalingRatio = 8.0 / selectedDPM

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
                            
                            ' Calculate the adjusted scaling factor based on DPI
                            Dim adjustedScalingFactor As Double = baseScalingFactor * dpiScalingRatio

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
            MessageBox.Show($"Error: {ex.Message}\n" & ex.StackTrace)
        End Try
    End Sub


    ' Optional: Method to validate ZPL before sending
    Public Function IsValidZPL(zpl As String) As Boolean
        If String.IsNullOrWhiteSpace(zpl) Then Return False
        If Not zpl.Trim().ToUpper().StartsWith("^XA") Then Return False
        If Not zpl.Trim().ToUpper().EndsWith("^XZ") Then Return False
        Return True
    End Function
End Class