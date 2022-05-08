Public Class Form1
    Dim floor As Integer = 50
    Dim state_up, state_down, state_left, state_right, state_shift As Integer
    Dim f11 As Boolean = False
    Dim visible_data As Boolean = False
    Dim now_flash As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_object()
    End Sub
    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer_phy.Tick
        Select Case Chara.v_h
            Case > 0
                Chara.act = 5
            Case < 0
                If state_up = 1 Then
                    Chara.act = 6
                Else
                    Chara.act = 7
                End If
            Case = 0
                If state_left = 0 And state_right = 0 Then
                    If state_down = 1 Then
                        Chara.act = 1
                    Else
                        Chara.act = 0
                    End If
                ElseIf state_down = 1 Then
                    Chara.act = 4
                ElseIf state_shift = 1 Then
                    Chara.act = 3
                Else
                    Chara.act = 2
                End If
        End Select
        If Chara.act = 6 Then
            Chara.g = -4
            If Chara.v_h < -2 Then
                Chara.v_h = -2
            End If
        Else
            Chara.g = -8
        End If
        If Chara.act = 3 Then
            Chara.speed = 3
        Else
            Chara.speed = 2.25
        End If
        If Chara.act = 4 And Chara.shovel_time < 3 Then
            Chara.speed = 10 / (Chara.shovel_time + 2) - 2
            Chara.shovel_time += Timer_phy.Interval / 1000
        Else
            Chara.shovel_time = 0
        End If
        If state_left = 1 Or state_right = 1 Then
            If state_left = 1 Then Chara.lr = 0
            If state_right = 1 Then Chara.lr = 1
            Chara.Lr_move()
        End If
        Chara.Fall_event()
    End Sub
    Private Sub Timer_anime_Tick(sender As Object, e As EventArgs) Handles Timer_anime.Tick
        Chara.flash += 1
        Refresh()
        If Chara.flash > Chara.anime_data(Chara.act) Then
            Chara.flash = 0
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        visible_data = Not (visible_data)
    End Sub

    Private Sub Timer_data_Tick(sender As Object, e As EventArgs) Handles Timer_data.Tick
        If visible_data Then
            Label1.Text = "動作編號:" & Chara.act & vbCrLf
            Label1.Text &= "影格數:" & Chara.flash & vbCrLf
            Label1.Text &= "左右:" & Chara.lr & vbCrLf
            Label1.Text &= "位置:(" & Format(Chara.x, "0.0") & "," & Format(Chara.y, "0.0") & ")" & vbCrLf
            Label1.Text &= "縱向速度:" & Format(Chara.v_h, "0.##") & vbCrLf
            Label1.Text &= "橫向最大速度:" & Format(Chara.speed, "0.##") & vbCrLf
            Label1.Text &= "點我以切換顯示模式"
        Else
            Label1.Text = "動作編號:" & Chara.act & vbCrLf
            Label1.Text &= "左右:" & Chara.lr & vbCrLf
            Label1.Text &= "位置:(" & Format(Chara.x, "0.0") & "," & Format(Chara.y, "0.0") & ")" & vbCrLf
            Label1.Text &= "點我以切換顯示模式"
        End If
    End Sub
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        f11 = Not (f11)
        If f11 = False Then
            FormBorderStyle = FormBorderStyle.Sizable
            WindowState = FormWindowState.Normal
        Else
            FormBorderStyle = FormBorderStyle.None
            WindowState = FormWindowState.Maximized
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        If MsgBox("確定結束?", vbYesNo) = 6 Then End
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 37 Then state_left = 1
        If e.KeyCode = 38 Then
            state_up = 1
            If Chara.v_h = 0 Then Chara.v_h = 6
        End If
        If e.KeyCode = 39 Then state_right = 1
        If e.KeyCode = 40 Then state_down = 1

        If e.KeyCode = 16 Then state_shift = 1

    End Sub
    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = 37 Then state_left = 0
        If e.KeyCode = 38 Then state_up = 0
        If e.KeyCode = 39 Then state_right = 0
        If e.KeyCode = 40 Then state_down = 0

        If e.KeyCode = 16 Then state_shift = 0

        If e.KeyCode = 114 Then

        End If
        If e.KeyCode = 27 Then 'esc
            If MsgBox("確定結束?", vbYesNo) = 6 Then End
        End If
        If e.KeyCode = 122 Then  'F11
            f11 = Not (f11)
            If f11 = False Then
                FormBorderStyle = FormBorderStyle.Sizable
                WindowState = FormWindowState.Normal
            Else
                FormBorderStyle = FormBorderStyle.None
                WindowState = FormWindowState.Maximized
            End If
        End If
    End Sub
    Sub Load_object()
        Dim path() As String = IO.Directory.GetFiles("anime_pic\")
        For Each n In path
            Dim n_path As String = n
            Dim n_s = n.Split("\")
            n = n_s(1)
            n_s = n.Split(".")
            n = n_s(0)
            Load_anime(Chara, Val(n), n(n.Length - 1), n_path)
        Next
    End Sub
    Sub Load_anime(ByRef P As Charactor, ByVal fps As Integer, ByVal act As String, ByVal path As String)
        Dim act_n As Integer
        Select Case act
            Case "A"       '掛機
                act_n = 0
            Case "D"     '蹲下
                act_n = 1
            Case "W"      '走路
                act_n = 2
            Case "R"      '跑步
                act_n = 3
            Case "S"       '滑鏟
                act_n = 4
            Case "J"       '跳躍
                act_n = 5
            Case "SF"    '緩降
                act_n = 6
            Case "F"     '降落
                act_n = 7
        End Select
        P.anime(act_n) = Image.FromFile(path)
        P.anime_data(act_n) = fps
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim g As Graphics = e.Graphics
        Dim rect As New Rectangle(now_flash * Chara.anime(Chara.act).Width / Chara.anime_data(Chara.act), 0, Chara.anime(Chara.act).Width, Chara.anime(Chara.act).Height)  'x,y,w,h
        Dim unit As GraphicsUnit = GraphicsUnit.Pixel
        g.DrawImage(Chara.anime(Chara.act), Chara.x - cam.x, cam.y - Chara.y + Height - Chara.anime(Chara.act).Height)   'screen_x,screen_y,rect,unit
    End Sub
End Class



