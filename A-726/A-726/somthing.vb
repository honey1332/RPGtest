Public Class Charactor
    Public lr As Integer = 0 '左右，右是1 左0
    Public x As Single = 0
    Public y As Single = 0
    Public v_h As Single = 0   '向上速度
    Public jump As Integer = 0  '跳躍參數 0正常 1跳躍中
    Public speed As Single = 0 '速度 m/s
    Public shovel_time As Single = 0 '滑鏟持續時間
    Public flash As Integer = 0 '目前影格數
    Public act As Integer = 0  '目前動作
    Public g As Integer = -8    '落下加速度
    Public anime() As Image
    Public anime_data() As Integer  '最大影格數
    Public Sub Fall_event()
        v_h += g * Form1.Timer_phy.Interval / 1000
        If y + v_h > 0 Then
            y += v_h * screen_magn * Form1.Timer_phy.Interval / 1000
        Else
            y = 0
            v_h = 0
        End If
    End Sub
    Public Sub Lr_move()
        Select Case lr
            Case 0
                x -= speed * screen_magn * Form1.Timer_phy.Interval / 1000
            Case 1
                x += speed * screen_magn * Form1.Timer_phy.Interval / 1000
        End Select

    End Sub
End Class
Public Class Camara
    Public x, y As Integer
    Public speed_x As Single
    Public speed_y As Single
    Sub Follow_charactor(ByRef target As Charactor)

    End Sub
End Class