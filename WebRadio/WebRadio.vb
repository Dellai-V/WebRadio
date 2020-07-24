Imports System.IO
Public Class WebRadio
    Dim Radio As WMPLib.WindowsMediaPlayer = New WMPLib.WindowsMediaPlayer
    Private Sub WebRadio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tema()
        If My.Settings.Trasparenza = True Then
            Opacity = 0.85
        Else
            Opacity = 1
        End If
        LoadListaRadio()
    End Sub
    Public Sub LoadListaRadio()
        ComboBox1.Items.Clear()
        If Not My.Settings.NomeRadio.Count = 0 Then
            For n = 0 To My.Settings.NomeRadio.Count - 1
                ComboBox1.Items.Add(My.Settings.NomeRadio(n))
            Next
            ComboBox1.SelectedIndex = My.Settings.Stazione
        Else
            EditList.Show()
        End If
    End Sub
    Dim stato As Boolean = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If stato = False Then
                Radio.URL = My.Settings.URLRadio(My.Settings.Stazione)
                Me.Text = My.Settings.NomeRadio(My.Settings.Stazione)
                PictureBox1.ImageLocation = My.Settings.ImmagineRadio(My.Settings.Stazione)
                stato = True
                Button1.Text = "Stop"
                Timer2.Start()
            Else
                Radio.URL = Nothing
                stato = False
                Button1.Text = "Play"
                Timer2.Stop()
            End If
        Catch ex As Exception
            Radio.URL = Nothing
            stato = False
            Button1.Text = "Play"
            Timer2.Stop()
            Label1.Text = "Errore !"
        End Try
    End Sub

    Private Sub PictureBox1_MouseHover(sender As Object, e As EventArgs) Handles PictureBox1.MouseHover
        ComboBox1.Visible = True
        Button1.Visible = True
        Button2.Visible = True
        Button3.Visible = True
        Label1.Visible = True
        Timer1.Stop()
        Timer1.Start()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim mo As Boolean = False
        If Me.Location.X < MousePosition.X And MousePosition.X < Me.Location.X + 200 Then
            If Me.Location.Y < MousePosition.Y And MousePosition.Y < Me.Location.Y + 200 Then
                mo = True
            End If
        End If
        If ComboBox1.DroppedDown = False And stato = True And mo = False Then
            ComboBox1.Visible = False
            Button1.Visible = False
            Button2.Visible = False
            Button3.Visible = False
            Label1.Visible = False
        End If

    End Sub
    Private Sub ListaRadioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListaRadioToolStripMenuItem.Click
        EditList.Show()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        My.Settings.Stazione = ComboBox1.SelectedIndex
        If stato = True Then
            Radio.URL = My.Settings.URLRadio(My.Settings.Stazione)
            Me.Text = My.Settings.NomeRadio(My.Settings.Stazione)
            PictureBox1.ImageLocation = My.Settings.ImmagineRadio(My.Settings.Stazione)
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
    End Sub
    Private Sub PictureBox1_MouseDown(sender As Object, e As EventArgs) Handles PictureBox1.MouseDown
        Down = True
        MP = MousePosition
        FP = Me.Location
    End Sub
    Private Sub PictureBox1_Mouseup(sender As Object, e As EventArgs) Handles PictureBox1.MouseUp
        Down = False
    End Sub
    Private Sub AggiornaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AggiornaToolStripMenuItem.Click
        Process.Start("https://github.com/Dellai-V/WebRadio/releases/")
    End Sub
    Private Sub ToolStripComboBox1_Click(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        If ToolStripComboBox1.SelectedIndex = 0 Then
            My.Settings.Trasparenza = True
            Opacity = 0.85
        Else
            My.Settings.Trasparenza = False
            Opacity = 1
        End If
    End Sub
    Private Sub TrasparenzaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrasparenzaToolStripMenuItem.MouseEnter
        If My.Settings.Trasparenza = True Then
            ToolStripComboBox1.SelectedIndex = 0
        Else
            ToolStripComboBox1.SelectedIndex = 1
        End If
    End Sub
    Private Sub ToolStripComboBox2_Click(sender As Object, e As EventArgs) Handles ToolStripComboBox2.SelectedIndexChanged
        If ToolStripComboBox2.SelectedIndex = 0 Then
            My.Settings.Tema = True
        Else
            My.Settings.Tema = False
        End If
        Tema()
    End Sub
    Private Sub TemaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TemaToolStripMenuItem.MouseEnter
        If My.Settings.Tema = True Then
            ToolStripComboBox2.SelectedIndex = 0
        Else
            ToolStripComboBox2.SelectedIndex = 1
        End If
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Dim b = Radio.network
        If b.bufferingProgress < 100 Then
            Label1.Text = "Buffer : " & b.bufferingProgress & " %"
            Label1.Visible = True
        ElseIf b.sourceProtocol = Nothing Then
            If Radio.playState = 10 Then
                Label1.Text = "Offline"
            Else
                Label1.Text = Radio.status
            End If
            Label1.Visible = True
        Else
            Label1.Text = My.Settings.NomeRadio(My.Settings.Stazione)
        End If
    End Sub
    Private Sub Tema()
        If My.Settings.Tema = True Then
            'Chiaro
            Me.BackColor = Color.White
            ComboBox1.BackColor = Color.White
            ComboBox1.ForeColor = Color.FromArgb(30, 30, 30)
            Button1.BackColor = Color.DarkGray
            Button1.ForeColor = Color.FromArgb(30, 30, 30)
            Button2.ForeColor = Color.FromArgb(50, 50, 50)
            Button3.ForeColor = Color.FromArgb(50, 50, 50)
            Button3.FlatAppearance.MouseOverBackColor = Color.DarkGray
            Button3.FlatAppearance.MouseDownBackColor = Color.DarkGray
            Label1.ForeColor = Color.FromArgb(50, 50, 50)
        Else 'Scuro
            Me.BackColor = Color.FromArgb(30, 30, 30)
            ComboBox1.BackColor = Color.FromArgb(30, 30, 30)
            ComboBox1.ForeColor = Color.White
            Button1.BackColor = Color.FromArgb(50, 50, 50)
            Button1.ForeColor = Color.White
            Button2.ForeColor = Color.DarkGray
            Button3.ForeColor = Color.DarkGray
            Button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50)
            Button3.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 50, 50)
            Label1.ForeColor = Color.DarkGray
        End If
    End Sub
End Class
