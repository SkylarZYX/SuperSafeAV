Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Public Class Form1
    'this is the SuperSafe AV Main Script. :)'
    Function md5_hash(ByVal file_name As String)
        Return hash_generator("md5", file_name)
    End Function

    Function hash_generator(ByRef hash_type As String, ByRef file_name As String)

        Dim hash
        hash = MD5.Create

        Dim hashValue() As Byte

        Dim filestream As FileStream = File.OpenRead(file_name)
        filestream.Position = 0
        hashValue = hash.ComputeHash(filestream)
        Dim hash_hex = PrintByteArray(hashValue)

        filestream.Close()

        Return hash_hex
    End Function

    Public Function PrintByteArray(ByRef array() As Byte)

        Dim hex_Value As String = ""

        Dim i As Integer
        For i = 0 To array.Length - 1

            hex_Value += array(i).ToString("x2")
        Next i

        Return hex_Value.ToLower
    End Function

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim path As String = OpenFileDialog1.FileName
            txtFilePath.Text = path

            Dim sample As String
            sample = md5_hash(path)
            txtHash.Text = md5_hash(path)

            Using f As System.IO.FileStream = System.IO.File.OpenRead("md5.txt")
                Using s As System.IO.StreamReader = New System.IO.StreamReader(f)
                    While Not s.EndOfStream
                        Dim line As String = s.ReadLine

                        If (line = sample) Then
                            lblResult.Text = "Virus detected! Delete Virus now!"
                            lblResult.ForeColor = Color.Red
                        Else
                            lblResult.Text = "Secure!"
                            lblResult.ForeColor = Color.Green
                        End If
                    End While
                End Using
            End Using
        End If
    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
        UpdateSuperSafe.Show()
    End Sub
End Class
