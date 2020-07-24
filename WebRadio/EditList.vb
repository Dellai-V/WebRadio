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
        For n = 0 To My.Settings.NomeRadio.Count - 1
            ListView1.Items.Add(My.Settings.NomeRadio(n))
            ListaNome.Add(My.Settings.NomeRadio(n))
            ListaURL.Add(My.Settings.URLRadio(n))
            ListaImmagine.Add(My.Settings.ImmagineRadio(n))
        Next
    End Sub
    Private Sub EditList_close(sender As Object, e As EventArgs) Handles MyBase.Closing
        Dim salva As DialogResult = MessageBox.Show("Desideri salvare le modifiche?", "Vuoi Salvare?", MessageBoxButtons.YesNo)
        If salva = DialogResult.Yes Then
            SalvaListaRadio()
            WebRadio.LoadListaRadio()
        End If
    End Sub
    Private Sub SalvaListaRadio()
        My.Settings.NomeRadio.Clear()
        My.Settings.URLRadio.Clear()
        My.Settings.ImmagineRadio.Clear()
        For n = 0 To ListaNome.Count - 1
            My.Settings.NomeRadio.Add(ListaNome(n))
            My.Settings.URLRadio.Add(ListaURL(n))
            My.Settings.ImmagineRadio.Add(ListaImmagine(n))
        Next
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

    Private Sub ButtonIncolla1_Click(sender As Object, e As EventArgs) Handles ButtonIncolla1.Click
        TextBox1.Text = Clipboard.GetText
    End Sub

    Private Sub ButtonIncolla2_Click(sender As Object, e As EventArgs) Handles ButtonIncolla2.Click
        TextBox2.Text = Clipboard.GetText
    End Sub

    Private Sub ButtonIncolla3_Click(sender As Object, e As EventArgs) Handles ButtonIncolla3.Click
        TextBox3.Text = Clipboard.GetText
    End Sub
End Class