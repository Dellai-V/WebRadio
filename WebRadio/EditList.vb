Imports System.IO
Public Class EditList
    Dim ListaURL As List(Of String) = New List(Of String)
    Dim ListaNome As List(Of String) = New List(Of String)
    Dim ListaImmagine As List(Of String) = New List(Of String)
    Private Sub EditList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.Trasparenza = True Then
            Opacity = 0.85
        Else
            Opacity = 1
        End If
        LoadListaRadio()
    End Sub
    Private Sub EditList_close(sender As Object, e As EventArgs) Handles MyBase.Closing
        Dim salva As DialogResult = MessageBox.Show("Desideri salvare le modifiche?", "Vuoi Salvare?", MessageBoxButtons.YesNo)
        If salva = DialogResult.Yes Then
            SalvaListaRadio()
            WebRadio.LoadListaRadio()
        End If
    End Sub
    Private Sub SalvaListaRadio()
        Dim path As String = Directory.GetCurrentDirectory() & "\List"
        Dim lists As List(Of String) = New List(Of String)
        For n = 0 To ListaNome.Count - 1
            lists.Add(ListaNome(n) & "|" & ListaURL(n) & "|" & ListaImmagine(n))
        Next
        System.IO.File.WriteAllLines(path, lists)
    End Sub
    Private Sub LoadListaRadio()
        Dim path As String = Directory.GetCurrentDirectory() & "\List"
        If File.Exists(path) Then
            Dim lines As List(Of String) = New List(Of String)
            Using sr As StreamReader = File.OpenText(path)
                Do While sr.Peek() >= 0
                    Dim cut() As String = sr.ReadLine().Split("|")
                    ListaNome.Add(cut(0))
                    ListView1.Items.Add(cut(0))
                    ListaURL.Add(cut(1))
                    ListaImmagine.Add(cut(2))
                Loop
            End Using
        Else
            Dim Dialogo As DialogResult = MessageBox.Show("Non è presente una lista radio, vuoi scaricarla?", "Scarica la Lista?", MessageBoxButtons.YesNo)
            If Dialogo = DialogResult.Yes Then
                Process.Start("https://github.com/Dellai-V/WebRadio/releases/download/list/List")
            End If
        End If
    End Sub
    Dim x As Integer
    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count = 1 Then
            x = ListView1.SelectedItems(0).Index
        End If
        TextBox1.Text = ListaNome(x)
        TextBox2.Text = ListaURL(x)
        TextBox3.Text = ListaImmagine(x)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text <> Nothing And TextBox2.Text <> Nothing And TextBox3.Text <> Nothing Then
            If TextBox2.Text.Contains("https://") Or TextBox2.Text.Contains("http://") Then
                ListaNome(x) = TextBox1.Text
                ListView1.Items(x).Text = TextBox1.Text
                ListaURL(x) = TextBox2.Text
                ListaImmagine(x) = TextBox3.Text
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not x = Nothing Then
            ListaNome.RemoveRange(x, 1)
            ListaURL.RemoveRange(x, 1)
            ListaImmagine.RemoveRange(x, 1)
            x = Nothing
            ListView1.Items.Clear()
            For n = 0 To ListaNome.Count - 1
                ListView1.Items.Add(ListaNome(n))
            Next
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text <> Nothing And TextBox2.Text <> Nothing And TextBox3.Text <> Nothing Then
            If TextBox2.Text.Contains("https://") Or TextBox2.Text.Contains("http://") Then
                ListaNome.Add(TextBox1.Text)
                ListView1.Items.Add(TextBox1.Text)
                ListaURL.Add(TextBox2.Text)
                ListaImmagine.Add(TextBox3.Text)
            End If
        End If
    End Sub
End Class