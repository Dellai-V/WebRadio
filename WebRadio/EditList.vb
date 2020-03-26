Imports System.IO

Public Class EditList
    Dim ListaURL As List(Of String) = New List(Of String)
    Dim ListaNome As List(Of String) = New List(Of String)
    Dim ListaImmagine As List(Of String) = New List(Of String)
    Private Sub EditList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadListaRadio()
    End Sub
    Private Sub EditList_close(sender As Object, e As EventArgs) Handles MyBase.Closing
        Dim salva As DialogResult = MessageBox.Show("Desideri salvare le modifiche", "Salva", MessageBoxButtons.YesNo)
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
        If Not x = Nothing Then
            ListaNome(x) = TextBox1.Text
            ListView1.Items(x).Text = TextBox1.Text
            ListaURL(x) = TextBox2.Text
            ListaImmagine(x) = TextBox3.Text
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not x = Nothing Then
            ListaNome.Remove(ListaNome(x))
            ListaURL.Remove(ListaURL(x))
            ListaImmagine.Remove(ListaImmagine(x))
            x = Nothing
            ListView1.Items.Clear()
            For n = 0 To ListaNome.Count - 1
                ListView1.Items.Add(ListaNome(n))
            Next
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ListaNome.Add(TextBox1.Text)
        ListView1.Items.Add(TextBox1.Text)
        ListaURL.Add(TextBox2.Text)
        ListaImmagine.Add(TextBox3.Text)
    End Sub
End Class