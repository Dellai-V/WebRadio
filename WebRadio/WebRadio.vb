
Imports System.IO

Public Class WebRadio
    Dim Radio As WMPLib.WindowsMediaPlayer = New WMPLib.WindowsMediaPlayer
    Dim ListaURL As List(Of String) = New List(Of String)
    Dim ListaNome As List(Of String) = New List(Of String)
    Dim ListaImmagine As List(Of String) = New List(Of String)
    Private Sub WebRadio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadListaRadio()
        x = My.Settings.Stazione
        ComboBox1.SelectedIndex = x
    End Sub
    Dim stato As Boolean = False
    Dim x As Integer
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If stato = False Then
            Radio.URL = ListaURL(x)
            Me.Text = ListaNome(x)
            PictureBox1.ImageLocation = ListaImmagine(x)
            stato = True
            Button1.Text = "Stop"
        Else
            Radio.URL = Nothing
            stato = False
            Button1.Text = "Play"
        End If
    End Sub
    Public Sub LoadListaRadio()
        Dim path As String = Directory.GetCurrentDirectory() & "\List"
        If File.Exists(path) Then
            ListaNome.Clear()
            ListaURL.Clear()
            ListaImmagine.Clear()
            ComboBox1.Items.Clear()
            Dim lines As List(Of String) = New List(Of String)
            Using sr As StreamReader = File.OpenText(path)
                Do While sr.Peek() >= 0
                    Dim cut() As String = sr.ReadLine().Split("|")
                    ListaNome.Add(cut(0))
                    ComboBox1.Items.Add(cut(0))
                    ListaURL.Add(cut(1))
                    ListaImmagine.Add(cut(2))
                Loop
            End Using
        End If
    End Sub
    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        ComboBox1.Visible = True
        Button1.Visible = True
        Button2.Visible = True
        Button3.Visible = True
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If WebRadio.MousePosition.IsEmpty = False And ComboBox1.DroppedDown = False And stato = True Then
            ComboBox1.Visible = False
            Button1.Visible = False
            Button2.Visible = False
            Button3.Visible = False
        End If
    End Sub
    Private Sub ListaRadioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListaRadioToolStripMenuItem.Click
        EditList.Show()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        x = ComboBox1.SelectedIndex
        My.Settings.Stazione = x
        If stato = True Then
            Radio.URL = ListaURL(x)
            Me.Text = ListaNome(x)
            PictureBox1.ImageLocation = ListaImmagine(x)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
    Dim Down As Boolean = False
    Dim MP As Point
    Dim FP As Point
    Private Sub PictureBox1_MouseMove(sender As Object, e As EventArgs) Handles PictureBox1.MouseMove
        If Down = True Then
            Me.Location = New Point(FP.X + MousePosition.X - MP.X, FP.Y + MousePosition.Y - MP.Y)
        End If
        Timer1.Stop()
        Timer1.Start()
    End Sub
    Private Sub PictureBox1_MouseDown(sender As Object, e As EventArgs) Handles PictureBox1.MouseDown
        Down = True
        MP = MousePosition
        FP = Me.Location
    End Sub
    Private Sub PictureBox1_Mouseup(sender As Object, e As EventArgs) Handles PictureBox1.MouseUp
        Down = False
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) 

    End Sub
End Class
