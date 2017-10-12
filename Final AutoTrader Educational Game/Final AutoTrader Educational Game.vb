Public Class GameArea
    Dim position As Integer
    Dim anspos As Integer = 0
    Dim gamespeed As Integer = 8
    Dim AnsCount As Integer = 0
    Dim Lives As Integer = 3
    Dim GameMode As Char
    Dim CorrectAnswer As Integer
    Dim opList As New List(Of String)
    Structure ScoreStructure
        Dim Name As String
        Dim Score As Integer
        Dim LastGame As Boolean
    End Structure
    Dim hardscores(7) As ScoreStructure
    Dim Medscores(7) As ScoreStructure
    Dim EasyScores(7) As ScoreStructure
    Dim hardscorefile As String = CurDir() + "\hardscores.txt"
    Dim medscorefile As String = CurDir() + "\medscores.txt"
    Dim easyscorefile As String = CurDir() + "\easyscores.txt"
    Dim NoOfHardScorers As Integer
    Dim NoOfEasyScorers As Integer
    Dim NoOfMedScorers As Integer
    Dim ValidName As Boolean = True
    Dim Duration As Integer
    Dim Gamebegan As Boolean = False

    Private Sub GameArea_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Scores.Hide()
        GamePlayArea.Hide()
        opList.Add("+")
        opList.Add("-")
        opList.Add("x")
        opList.Add("÷")
        GameMode = "E"
        GenerateQ()
        position = Me.Size.Width / 2
        ResetPos()
        Timer2.Enabled = True
        'Timer1.Enabled = True
    End Sub
    Private Sub ResetPos()
        picRoad.Width = Me.Size.Width / 2
        picRoad.Height = Me.Size.Height
        picRoad.Left = Me.Size.Width / 4
        picCar.Left = position
        picCar.Top = Me.Size.Height - 150
        lblAns1.Left = Me.Size.Width / 4
        lblAns2.Left = Me.Size.Width * 3 / 8
        lblAns3.Left = Me.Size.Width * 4 / 8
        lblAns4.Left = Me.Size.Width * 5 / 8
        lblStatAns1.Left = Me.Size.Width / 4
        lblStatAns2.Left = Me.Size.Width * 3 / 8
        lblStatAns3.Left = Me.Size.Width * 4 / 8
        lblStatAns4.Left = Me.Size.Width * 5 / 8
        lblStatAns1.Top = 50
        lblStatAns2.Top = 50
        lblStatAns3.Top = 50
        lblStatAns4.Top = 50
        lblQuestion.Top = 50
        lblAns1.Width = Me.Size.Width / 8
        lblAns2.Width = Me.Size.Width / 8
        lblAns3.Width = Me.Size.Width / 8
        lblAns4.Width = Me.Size.Width / 8
        lblAns1.Top = 50
        lblAns2.Top = 50
        lblAns3.Top = 50
        lblAns4.Top = 50
        lblStatAns1.Width = Me.Size.Width / 8
        lblStatAns2.Width = Me.Size.Width / 8
        lblStatAns3.Width = Me.Size.Width / 8
        lblStatAns4.Width = Me.Size.Width / 8
        picLanes1.Top = 0
        picLanes2.Top = picLanes1.Bottom + Me.Size.Height / 6
        PicLanes3.Top = picLanes2.Bottom + Me.Size.Height / 6
        picLanes4.Top = PicLanes3.Bottom + Me.Size.Height / 6
        picLanes5.Top = PicLanes3.Bottom + Me.Size.Height / 6
        picLanes1.Left = Me.Size.Width / 4
        picLanes2.Left = Me.Size.Width / 4
        PicLanes3.Left = Me.Size.Width / 4
        picLanes4.Left = Me.Size.Width / 4
        picLanes5.Left = Me.Size.Width / 4
        picLanes1.Width = Me.Size.Width / 2
        picLanes2.Width = Me.Size.Width / 2
        PicLanes3.Width = Me.Size.Width / 2
        picLanes4.Width = Me.Size.Width / 2
        picLanes5.Width = Me.Size.Width / 2
        lblLives.Left = 0
        picLife1.Left = lblLives.Right
        PicLife2.Left = picLife1.Right
        picLife3.Left = PicLife2.Right
        lblLives.Top = Me.Size.Height - 100
        picLife1.Top = Me.Size.Height - 100
        PicLife2.Top = Me.Size.Height - 100
        picLife3.Top = Me.Size.Height - 100
        lblCountdown.Left = Me.Size.Width / 4
        lblCountdown.Width = Me.Size.Width / 2
    End Sub

    Private Sub GameArea_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim key As Integer = e.KeyValue
        If Gamebegan = True Then
            If key = 37 And position > Me.Size.Width / 4 Then
                position -= 13
            ElseIf key = 39 And position < Me.Size.Width * 3 / 4 - picCar.Width Then
                position += 13
            End If
            picCar.Left = position
        End If
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Gamebegan = True Then
            anspos += gamespeed
            lblAns1.Top = anspos
            lblAns2.Top = anspos
            lblAns3.Top = anspos
            lblAns4.Top = anspos
            picLanes1.Top += gamespeed
            picLanes2.Top += gamespeed
            PicLanes3.Top += gamespeed
            picLanes4.Top += gamespeed
            picLanes5.Top += gamespeed
            If picLanes5.Top > Me.Size.Height Then
                picLanes5.Top = picLanes1.Top - Me.Size.Height / 6 - picLanes5.Height
            End If
            If picLanes4.Top > Me.Size.Height Then
                picLanes4.Top = picLanes5.Top - Me.Size.Height / 6 - picLanes4.Height
            End If
            If PicLanes3.Top > Me.Size.Height Then
                PicLanes3.Top = picLanes4.Top - Me.Size.Height / 6 - PicLanes3.Height
            End If
            If picLanes2.Top > Me.Size.Height Then
                picLanes2.Top = PicLanes3.Top - Me.Size.Height / 6 - picLanes2.Height
            End If
            If picLanes1.Top > Me.Size.Height Then
                picLanes1.Top = picLanes2.Top - Me.Size.Height / 6 - picLanes1.Height
            End If
            If anspos > picCar.Top Then
                AnswerSelect()
            End If
            If anspos > picCar.Bottom Then
                AnsCount += 1
                If AnsCount Mod 5 = 0 Then
                    gamespeed += 1
                End If
                anspos = -20
                lblAnsCount.Text = "Score: " + AnsCount.ToString
                GenerateQ()
            End If
        End If
    End Sub

    Private Sub GenerateQ()
        Dim qdiff As Integer = 4
        If GameMode = "E" Then qdiff = 2
        Dim flag As Boolean = False
        Randomize()
        Dim RandomOp As String = opList(Int(Rnd() * qdiff))
        Do
            Dim num1 As Integer = Int(Rnd() * 10)
            Dim num2 As Integer = Int(Rnd() * 10)
            If RandomOp = "÷" Then
                If num2 = 0 Then
                    Exit Do
                Else
                    If num1 Mod num2 = 0 Then
                        Dim answer As Integer = AnswerCorrect(num1, num2, RandomOp)
                        Dim WrongAns1 As Integer = AnswerWrong(answer)
                        Dim WrongAns2 As Integer = AnswerWrong(answer)
                        Dim WrongAns3 As Integer = AnswerWrongClose(answer)
                        CorrectAnswer = Int(Rnd() * 3) + 1
                        Select Case CorrectAnswer
                            Case 1
                                lblStatAns1.Text = "  " + answer.ToString
                                lblStatAns2.Text = "  " + WrongAns1.ToString
                                lblStatAns3.Text = "  " + WrongAns2.ToString
                                lblStatAns4.Text = "  " + WrongAns3.ToString
                            Case 2
                                lblStatAns2.Text = "  " + answer.ToString
                                lblStatAns1.Text = "  " + WrongAns1.ToString
                                lblStatAns3.Text = "  " + WrongAns2.ToString
                                lblStatAns4.Text = "  " + WrongAns3.ToString
                            Case 3
                                lblStatAns3.Text = "  " + answer.ToString
                                lblStatAns2.Text = "  " + WrongAns1.ToString
                                lblStatAns1.Text = "  " + WrongAns2.ToString
                                lblStatAns4.Text = "  " + WrongAns3.ToString
                            Case 4
                                lblStatAns4.Text = "  " + answer.ToString
                                lblStatAns2.Text = "  " + WrongAns1.ToString
                                lblStatAns3.Text = "  " + WrongAns2.ToString
                                lblStatAns1.Text = "  " + WrongAns3.ToString
                        End Select
                        lblQuestion.Text = num1 & " " & RandomOp & " " & num2
                        flag = True
                    End If
                End If
            Else
                Dim answer As Integer = AnswerCorrect(num1, num2, RandomOp)
                Dim WrongAns1 As Integer = AnswerWrong(answer)
                Dim WrongAns2 As Integer = AnswerWrong(answer)
                Dim WrongAns3 As Integer = AnswerWrongClose(answer)
                CorrectAnswer = Int(Rnd() * 3) + 1
                Select Case CorrectAnswer
                    Case 1
                        lblStatAns1.Text = "  " + answer.ToString
                        lblStatAns2.Text = "  " + WrongAns1.ToString
                        lblStatAns3.Text = "  " + WrongAns2.ToString
                        lblStatAns4.Text = "  " + WrongAns3.ToString
                    Case 2
                        lblStatAns2.Text = "  " + answer.ToString
                        lblStatAns1.Text = "  " + WrongAns1.ToString
                        lblStatAns3.Text = "  " + WrongAns2.ToString
                        lblStatAns4.Text = "  " + WrongAns3.ToString
                    Case 3
                        lblStatAns3.Text = "  " + answer.ToString
                        lblStatAns2.Text = "  " + WrongAns1.ToString
                        lblStatAns1.Text = "  " + WrongAns2.ToString
                        lblStatAns4.Text = "  " + WrongAns3.ToString
                    Case 4
                        lblStatAns4.Text = "  " + answer.ToString
                        lblStatAns2.Text = "  " + WrongAns1.ToString
                        lblStatAns3.Text = "  " + WrongAns2.ToString
                        lblStatAns1.Text = "  " + WrongAns3.ToString
                End Select
                lblQuestion.Text = num1 & " " & RandomOp & " " & num2 & " = "
                flag = True
            End If
        Loop Until flag = True

    End Sub
    Private Function AnswerWrong(ByVal answer As Integer)
        Dim flag As Boolean = False
        Dim returnwrong As Integer = 0
        Do
            Randomize()

            Dim rn As Integer = Int(Rnd() * 20)
            If rn <> 0 Then
                returnwrong = answer + rn
                flag = True
            End If
        Loop Until flag = True
        Return returnwrong
    End Function

    Private Function AnswerWrongClose(ByVal answer As Integer)
        Dim flag As Boolean = False
        Dim returnwrong As Integer = 0
        Do
            Randomize()
            Dim rn As Integer = Int(-5 + Rnd() * 10)
            If rn <> 0 Then
                returnwrong = answer + rn
                flag = True
            End If
        Loop Until flag = True
        Return returnwrong
    End Function
    Private Function AnswerCorrect(ByVal num1 As Integer, ByVal num2 As Integer, ByVal op As String)
        Dim calculation As Double = 0
        Select Case op
            Case "+"
                calculation = num1 + num2
            Case "-"
                calculation = num1 - num2
            Case "x"
                calculation = num1 * num2
            Case "÷"
                calculation = num1 / num2
        End Select
        Return calculation
    End Function

    Private Sub AnswerSelect()
        Select Case CorrectAnswer
            Case 1
                If picCar.Right > lblAns1.Right Then
                    LifeLost()
                End If
            Case 2
                If picCar.Left < lblAns2.Left Then
                    LifeLost()
                End If
                If picCar.Right > lblAns2.Right Then
                    LifeLost()
                End If
            Case 3
                If picCar.Left < lblAns3.Left Then
                    LifeLost()
                End If
                If picCar.Right > lblAns3.Right Then
                    LifeLost()
                End If
            Case 4
                If picCar.Left < lblAns4.Left Then
                    LifeLost()
                End If
        End Select
    End Sub
    Private Sub LifeLost()
        Timer1.Enabled = False
        Timer2.Enabled = False
        If Lives > 1 Then
            Lives -= 1
            If Lives = 2 Then
                picLife3.Hide()
            ElseIf Lives = 1 Then

                PicLife2.Hide()
            End If
            MsgBox("You have lost a life. " + Lives.ToString + " lives remaining.")
            Timer1.Enabled = True
        Else
            picLife1.Hide()
            GameOver()
            Lives = 3
        End If
        ResetPos()
        anspos = 0
        Duration = 0
    End Sub
    Private Sub GameOver()
        If My.Computer.FileSystem.FileExists(hardscorefile) Then
            Dim Contents As String = My.Computer.FileSystem.ReadAllText(hardscorefile)
            Dim record() As String = Contents.Split(";")
            For i As Integer = 0 To record.Length - 1
                If record(i).Trim <> String.Empty Then
                    Dim field() As String = record(i).Split(",")
                    With hardscores(i + 1)
                        .Name = field(0)
                        .Score = CInt(field(1))
                        .LastGame = False
                    End With
                End If
            Next
            NoOfHardScorers = record.Length - 1
            If GameMode = "H" Then
                HighScoreTabs.SelectedTab = HardScoreTab
                hardscores(NoOfHardScorers + 1).Score = AnsCount
                hardscores(NoOfHardScorers + 1).LastGame = True
                NoOfHardScorers += 1
            End If
            Dim swapped As Boolean
            Do
                swapped = False
                For i = 1 To NoOfHardScorers
                    If hardscores(i).Score < hardscores(i + 1).Score Then
                        hardscores(7) = hardscores(i)
                        hardscores(i) = hardscores(i + 1)
                        hardscores(i + 1) = hardscores(7)
                        swapped = True
                    End If
                Next
            Loop Until swapped = False
        Else
            hardscores(1).Score = AnsCount
            txthScorer1.Enabled = True
            txthScorer1.BackColor = Color.LightGreen
            txtHScore1.BackColor = Color.LightGreen
            NoOfHardScorers = 1
            ValidName = False
        End If
        If hardscores(1).LastGame = True Then
            txthScorer1.Enabled = True
            ValidName = False
            txtHScore1.BackColor = Color.LightGreen
        End If
        txthScorer1.Text = hardscores(1).Name
        txtHScore1.Text = hardscores(1).Score
        If hardscores(2).LastGame = True Then
            txthScorer2.Enabled = True
            ValidName = False
            txthScorer2.BackColor = Color.LightGreen
            txtHScore2.BackColor = Color.LightGreen
        End If
        txthScorer2.Text = hardscores(2).Name
        txtHScore2.Text = hardscores(2).Score
        If hardscores(3).LastGame = True Then
            txthScorer3.Enabled = True
            ValidName = False
            txthScorer3.BackColor = Color.LightGreen
            txtHScore3.BackColor = Color.LightGreen
        End If
        txthScorer3.Text = hardscores(3).Name
        txtHScore3.Text = hardscores(3).Score
        If hardscores(4).LastGame = True Then
            txthScorer4.Enabled = True
            ValidName = False
            txthScorer4.BackColor = Color.LightGreen
            txtHScore4.BackColor = Color.LightGreen
        End If
        txthScorer4.Text = hardscores(4).Name
        txtHScore4.Text = hardscores(4).Score
        If hardscores(5).LastGame = True Then
            txtHScorer5.Enabled = True
            ValidName = False
            txtHScorer5.BackColor = Color.LightGreen
            txtHScore5.BackColor = Color.LightGreen
        End If
        txtHScorer5.Text = hardscores(5).Name
        txtHScore5.Text = hardscores(5).Score
        Scores.Show()
        If My.Computer.FileSystem.FileExists(medscorefile) Then
            Dim Contents As String = My.Computer.FileSystem.ReadAllText(medscorefile)
            Dim record() As String = Contents.Split(";")
            For i As Integer = 0 To record.Length - 1
                If record(i).Trim <> String.Empty Then
                    Dim field() As String = record(i).Split(",")
                    With Medscores(i + 1)
                        .Name = field(0)
                        .Score = CInt(field(1))
                        .LastGame = False
                    End With
                End If
            Next
            NoOfMedScorers = record.Length - 1
            If GameMode = "M" Then
                HighScoreTabs.SelectedTab = MedScoreTab
                Medscores(NoOfMedScorers + 1).Score = AnsCount
                Medscores(NoOfMedScorers + 1).LastGame = True
                NoOfMedScorers += 1
            End If
            Dim swapped As Boolean
            Do
                swapped = False
                For i = 1 To NoOfMedScorers
                    If Medscores(i).Score < Medscores(i + 1).Score Then
                        Medscores(7) = Medscores(i)
                        Medscores(i) = Medscores(i + 1)
                        Medscores(i + 1) = Medscores(7)
                        swapped = True
                    End If
                Next
            Loop Until swapped = False
        Else
            Medscores(1).Score = AnsCount
            txtmScorer1.Enabled = True
            txtmScorer1.BackColor = Color.LightGreen
            txtmScore1.BackColor = Color.LightGreen
            txtmScore2.Text = "-"
            txtmScore3.Text = "-"
            txtmScore4.Text = "-"
            txtMScore5.Text = "-"
            NoOfMedScorers = 1
            ValidName = False
        End If
        If Medscores(1).LastGame = True Then
            txtmScorer1.Enabled = True
            ValidName = False
            txtmScore1.BackColor = Color.LightGreen
        End If
        txtmScorer1.Text = Medscores(1).Name
        txtmScore1.Text = Medscores(1).Score
        If Medscores(2).LastGame = True Then
            txtmScorer2.Enabled = True
            ValidName = False
            txtmScorer2.BackColor = Color.LightGreen
            txtmScore2.BackColor = Color.LightGreen
        End If
        txtmScorer2.Text = Medscores(2).Name
        txtmScore2.Text = Medscores(2).Score
        If Medscores(3).LastGame = True Then
            txtmScorer3.Enabled = True
            ValidName = False
            txtmScorer3.BackColor = Color.LightGreen
            txtmScore3.BackColor = Color.LightGreen
        End If
        txtmScorer3.Text = Medscores(3).Name
        txtmScore3.Text = Medscores(3).Score
        If Medscores(4).LastGame = True Then
            txtmScorer4.Enabled = True
            ValidName = False
            txtmScorer4.BackColor = Color.LightGreen
            txtmScore4.BackColor = Color.LightGreen
        End If
        txtmScorer4.Text = Medscores(4).Name
        txtmScore4.Text = Medscores(4).Score
        If Medscores(5).LastGame = True Then
            txtmScorer5.Enabled = True
            ValidName = False
            txtmScorer5.BackColor = Color.LightGreen
            txtMScore5.BackColor = Color.LightGreen
        End If
        txtmScorer5.Text = Medscores(5).Name
        txtMScore5.Text = Medscores(5).Score
        If My.Computer.FileSystem.FileExists(easyscorefile) Then
            Dim Contents As String = My.Computer.FileSystem.ReadAllText(easyscorefile)
            Dim record() As String = Contents.Split(";")
            For i As Integer = 0 To record.Length - 1
                If record(i).Trim <> String.Empty Then
                    Dim field() As String = record(i).Split(",")
                    With EasyScores(i + 1)
                        .Name = field(0)
                        .Score = CInt(field(1))
                        .LastGame = False
                    End With
                End If
            Next
            NoOfEasyScorers = record.Length - 1
            If GameMode = "E" Then
                HighScoreTabs.SelectedTab = EasyScoreTab
                EasyScores(NoOfEasyScorers + 1).Score = AnsCount
                EasyScores(NoOfEasyScorers + 1).LastGame = True
                NoOfEasyScorers += 1
            End If
            Dim swapped As Boolean
            Do
                swapped = False
                For i = 1 To NoOfEasyScorers
                    If EasyScores(i).Score < EasyScores(i + 1).Score Then
                        EasyScores(7) = EasyScores(i)
                        EasyScores(i) = EasyScores(i + 1)
                        EasyScores(i + 1) = EasyScores(7)
                        swapped = True
                    End If
                Next
            Loop Until swapped = False
        Else
            EasyScores(1).Score = AnsCount
            txtEScorer1.Enabled = True
            txtEScorer1.BackColor = Color.LightGreen
            txtEScore1.BackColor = Color.LightGreen
            NoOfEasyScorers = 1
            ValidName = False
        End If
        If EasyScores(1).LastGame = True Then
            txtEScorer1.Enabled = True
            ValidName = False
            txtEScore1.BackColor = Color.LightGreen
        End If
        txtEScorer1.Text = EasyScores(1).Name
        txtEScore1.Text = EasyScores(1).Score
        If EasyScores(2).LastGame = True Then
            txtEScorer2.Enabled = True
            ValidName = False
            txtEScorer2.BackColor = Color.LightGreen
            txtEScore2.BackColor = Color.LightGreen
        End If
        txtEScorer2.Text = EasyScores(2).Name
        txtEScore2.Text = EasyScores(2).Score
        If EasyScores(3).LastGame = True Then
            txtEScorer3.Enabled = True
            ValidName = False
            txtEScorer3.BackColor = Color.LightGreen
            txtEScore3.BackColor = Color.LightGreen
        End If
        txtEScorer3.Text = EasyScores(3).Name
        txtEScore3.Text = EasyScores(3).Score
        If EasyScores(4).LastGame = True Then
            txtEScorer4.Enabled = True
            ValidName = False
            txtEScorer4.BackColor = Color.LightGreen
            txtEScore4.BackColor = Color.LightGreen
        End If
        txtEScorer4.Text = EasyScores(4).Name
        txtEScore4.Text = EasyScores(4).Score
        If EasyScores(5).LastGame = True Then
            txtEScorer5.Enabled = True
            ValidName = False
            txtEScorer5.BackColor = Color.LightGreen
            txtEScore5.BackColor = Color.LightGreen
        End If
        txtEScorer5.Text = EasyScores(5).Name
        txtEScore5.Text = EasyScores(5).Score
        GamePlayArea.Hide()
        Scores.Show()
        Timer2.Enabled = False
    End Sub

    Private Sub picEasyDiffBtn_MouseHover(sender As Object, e As EventArgs) Handles picEasyDiffBtn.MouseHover
        Me.picEasyDiffBtn.Image = My.Resources.EasyDifficultyHover
    End Sub
    Private Sub picEasyDiffBtn_MouseLeave(sender As Object, e As EventArgs) Handles picEasyDiffBtn.MouseLeave
        Me.picEasyDiffBtn.Image = My.Resources.EasyDifficultyButton
    End Sub
    Private Sub picMediumDiffBtn_MouseHover(sender As Object, e As EventArgs) Handles picMediumDiffBtn.MouseHover
        Me.picMediumDiffBtn.Image = My.Resources.MediumDifficultyHover
    End Sub
    Private Sub picMediumDiffBtn_MouseLeave(sender As Object, e As EventArgs) Handles picMediumDiffBtn.MouseLeave
        Me.picMediumDiffBtn.Image = My.Resources.MediumDifficultyButton
    End Sub
    Private Sub picHardDiffBtn_MouseHover(sender As Object, e As EventArgs) Handles picHardDiffBtn.MouseHover
        Me.picHardDiffBtn.Image = My.Resources.HardDifficultyHover
    End Sub
    Private Sub picHardDiffBtn_MouseLeave(sender As Object, e As EventArgs) Handles picHardDiffBtn.MouseLeave
        Me.picHardDiffBtn.Image = My.Resources.HardDifficultyButton
    End Sub

    Private Sub picEasyDiffBtn_Click(sender As Object, e As EventArgs) Handles picEasyDiffBtn.Click
        'run easy version of game
        StartMenu.Hide()
        GamePlayArea.Show()
        GameMode = "E"
        'Timer1.Interval = 25
        Timer1.Enabled = True
        Timer2.Enabled = True
        Gamebegan = False
        lblCountdown.Visible = True
    End Sub
    Private Sub picMediumDiffBtn_Click(sender As Object, e As EventArgs) Handles picMediumDiffBtn.Click
        'run medium difficulty version of the game
        StartMenu.Hide()
        GamePlayArea.Show()
        GameMode = "M"
        Timer1.Interval = 50
        'gamespeed = 13
        Timer1.Enabled = True
        Timer2.Enabled = True
        Gamebegan = False
        lblCountdown.Visible = True
    End Sub
    Private Sub picHardDiffBtn_Click(sender As Object, e As EventArgs) Handles picHardDiffBtn.Click
        'run hard difficulty version of the game
        StartMenu.Hide()
        GamePlayArea.Show()
        GameMode = "H"
        Timer1.Interval = 25
        'gamespeed = 18
        Timer1.Enabled = True
        Timer2.Enabled = True
        Gamebegan = False
        lblCountdown.Visible = True
    End Sub

    Private Sub Valiname(ByVal Entry As String)
        If Entry.contains(",") Or Entry.Contains(";") Or Entry = Nothing Then
            ValidName = False
        Else
            ValidName = True
        End If
    End Sub


    Private Sub txtmScorer1_TextChanged(sender As Object, e As EventArgs) Handles txtmScorer1.TextChanged
        Valiname(txtmScorer1.Text)
        Medscores(1).Name = txtmScorer1.Text
    End Sub
    Private Sub txtmScorer2_TextChanged(sender As Object, e As EventArgs) Handles txtmScorer2.TextChanged
        Valiname(txtmScorer2.Text)
        Medscores(2).Name = txtmScorer2.Text
    End Sub
    Private Sub txtmScorer3_TextChanged(sender As Object, e As EventArgs) Handles txtmScorer3.TextChanged
        Valiname(txtmScorer3.Text)
        Medscores(3).Name = txtmScorer3.Text
    End Sub
    Private Sub txtmScorer4_TextChanged(sender As Object, e As EventArgs) Handles txtmScorer4.TextChanged
        Valiname(txtmScorer4.Text)
        Medscores(4).Name = txtmScorer4.Text
    End Sub
    Private Sub txtmScorer5_TextChanged(sender As Object, e As EventArgs) Handles txtmScorer5.TextChanged
        Valiname(txtmScorer5.Text)
        Medscores(5).Name = txtmScorer5.Text
    End Sub

    Private Sub txthScorer1_TextChanged(sender As Object, e As EventArgs) Handles txthScorer1.TextChanged
        Valiname(txthScorer1.Text)
        hardscores(1).Name = txthScorer1.Text
    End Sub
    Private Sub txthScorer2_TextChanged(sender As Object, e As EventArgs) Handles txthScorer2.TextChanged
        Valiname(txthScorer2.Text)
        hardscores(2).Name = txthScorer2.Text
    End Sub
    Private Sub txthScorer3_TextChanged(sender As Object, e As EventArgs) Handles txthScorer3.TextChanged
        Valiname(txthScorer3.Text)
        hardscores(3).Name = txthScorer3.Text
    End Sub
    Private Sub txthScorer4_TextChanged(sender As Object, e As EventArgs) Handles txthScorer4.TextChanged
        Valiname(txthScorer4.Text)
        hardscores(4).Name = txthScorer4.Text
    End Sub
    Private Sub txtHScorer5_TextChanged(sender As Object, e As EventArgs) Handles txtHScorer5.TextChanged
        Valiname(txtHScorer5.Text)
        hardscores(5).Name = txtHScorer5.Text
    End Sub

    Private Sub txtEScorer1_TextChanged(sender As Object, e As EventArgs) Handles txtEScorer1.TextChanged
        Valiname(txtEScorer1.Text)
        EasyScores(1).Name = txtEScorer1.Text
    End Sub
    Private Sub txtEScorer2_TextChanged(sender As Object, e As EventArgs) Handles txtEScorer2.TextChanged
        Valiname(txtEScorer2.Text)
        EasyScores(2).Name = txtEScorer2.Text
    End Sub
    Private Sub txtEScorer3_TextChanged(sender As Object, e As EventArgs) Handles txtEScorer3.TextChanged
        Valiname(txtEScorer3.Text)
        EasyScores(3).Name = txtEScorer3.Text
    End Sub
    Private Sub txtEScorer4_TextChanged(sender As Object, e As EventArgs) Handles txtEScorer4.TextChanged
        Valiname(txtEScorer4.Text)
        EasyScores(4).Name = txtEScorer4.Text
    End Sub
    Private Sub txtEScorer5_TextChanged(sender As Object, e As EventArgs) Handles txtEScorer5.TextChanged
        Valiname(txtEScorer5.Text)
        EasyScores(5).Name = txtEScorer5.Text
    End Sub

    Private Sub btnReplay_MouseHover(sender As Object, e As EventArgs) Handles btnReplay.MouseHover
        btnReplay.Image = My.Resources.PlayAgainButtonHover
    End Sub
    Private Sub btnReplay_MouseLeave(sender As Object, e As EventArgs) Handles btnReplay.MouseLeave
        btnReplay.Image = My.Resources.PlayAgainButton
    End Sub
    Private Sub btnReplay_Click(sender As Object, e As EventArgs) Handles btnReplay.Click
        If txtEScorer1.Text = Nothing And NoOfEasyScorers > 0 Then ValidName = False
        If txtEScorer2.Text = Nothing And NoOfEasyScorers > 1 Then ValidName = False
        If txtEScorer3.Text = Nothing And NoOfEasyScorers > 2 Then ValidName = False
        If txtEScorer4.Text = Nothing And NoOfEasyScorers > 3 Then ValidName = False
        If txtEScorer5.Text = Nothing And NoOfEasyScorers > 4 Then ValidName = False
        If txtmScorer1.Text = Nothing And NoOfMedScorers > 0 Then ValidName = False
        If txtmScorer2.Text = Nothing And NoOfMedScorers > 1 Then ValidName = False
        If txtmScorer3.Text = Nothing And NoOfMedScorers > 2 Then ValidName = False
        If txtmScorer4.Text = Nothing And NoOfMedScorers > 3 Then ValidName = False
        If txtmScorer5.Text = Nothing And NoOfMedScorers > 4 Then ValidName = False
        If txthScorer1.Text = Nothing And NoOfHardScorers > 0 Then ValidName = False
        If txthScorer2.Text = Nothing And NoOfHardScorers > 1 Then ValidName = False
        If txthScorer3.Text = Nothing And NoOfHardScorers > 2 Then ValidName = False
        If txthScorer4.Text = Nothing And NoOfHardScorers > 3 Then ValidName = False
        If txtHScorer5.Text = Nothing And NoOfHardScorers > 4 Then ValidName = False
        If ValidName = True Then
            Dim RecordCount As Integer = 5
            If NoOfHardScorers < RecordCount Then RecordCount = NoOfHardScorers
            Dim tf As String = ""
            For i As Integer = 1 To RecordCount
                tf = tf + hardscores(i).Name + "," + hardscores(i).Score.ToString + ";"
            Next
            My.Computer.FileSystem.WriteAllText(hardscorefile, tf, False)
            RecordCount = 5
            If NoOfMedScorers < RecordCount Then RecordCount = NoOfMedScorers
            tf = ""
            For i As Integer = 1 To RecordCount
                tf = tf + Medscores(i).Name + "," + Medscores(i).Score.ToString + ";"
            Next
            My.Computer.FileSystem.WriteAllText(medscorefile, tf, False)
            RecordCount = 5
            If NoOfEasyScorers < RecordCount Then RecordCount = NoOfEasyScorers
            tf = ""
            For i As Integer = 1 To RecordCount
                tf = tf + EasyScores(i).Name + "," + EasyScores(i).Score.ToString + ";"
            Next
            My.Computer.FileSystem.WriteAllText(easyscorefile, tf, False)
            Scores.Hide()
            GamePlayArea.Hide()
            StartMenu.Show()
            picLife1.Visible = True
            PicLife2.Visible = True
            picLife3.Visible = True
            AnsCount = 0
            Duration = 0
            lblAnsCount.Text = "Score: 0"
            picCar.Left = Me.Size.Width / 2 - picCar.Width / 2
            position = Me.Size.Width / 2 - picCar.Width / 2
            ResetPos()
        Else
            MsgBox("You Need To Enter A Valid Name.")
        End If
    End Sub

    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        If txtEScorer1.Text = Nothing And NoOfEasyScorers > 0 Then ValidName = False
        If txtEScorer2.Text = Nothing And NoOfEasyScorers > 1 Then ValidName = False
        If txtEScorer3.Text = Nothing And NoOfEasyScorers > 2 Then ValidName = False
        If txtEScorer4.Text = Nothing And NoOfEasyScorers > 3 Then ValidName = False
        If txtEScorer5.Text = Nothing And NoOfEasyScorers > 4 Then ValidName = False
        If txtmScorer1.Text = Nothing And NoOfMedScorers > 0 Then ValidName = False
        If txtmScorer2.Text = Nothing And NoOfMedScorers > 1 Then ValidName = False
        If txtmScorer3.Text = Nothing And NoOfMedScorers > 2 Then ValidName = False
        If txtmScorer4.Text = Nothing And NoOfMedScorers > 3 Then ValidName = False
        If txtmScorer5.Text = Nothing And NoOfMedScorers > 4 Then ValidName = False
        If txthScorer1.Text = Nothing And NoOfHardScorers > 0 Then ValidName = False
        If txthScorer2.Text = Nothing And NoOfHardScorers > 1 Then ValidName = False
        If txthScorer3.Text = Nothing And NoOfHardScorers > 2 Then ValidName = False
        If txthScorer4.Text = Nothing And NoOfHardScorers > 3 Then ValidName = False
        If txtHScorer5.Text = Nothing And NoOfHardScorers > 4 Then ValidName = False
        If ValidName = True Then
            Dim RecordCount As Integer = 5
            If NoOfHardScorers < RecordCount Then RecordCount = NoOfHardScorers
            Dim tf As String = ""
            For i As Integer = 1 To RecordCount
                tf = tf + hardscores(i).Name + "," + hardscores(i).Score.ToString + ";"
            Next
            My.Computer.FileSystem.WriteAllText(hardscorefile, tf, False)
            RecordCount = 5
            If NoOfMedScorers < RecordCount Then RecordCount = NoOfMedScorers
            tf = ""
            For i As Integer = 1 To RecordCount
                tf = tf + Medscores(i).Name + "," + Medscores(i).Score.ToString + ";"
            Next
            My.Computer.FileSystem.WriteAllText(medscorefile, tf, False)
            RecordCount = 5
            If NoOfEasyScorers < RecordCount Then RecordCount = NoOfEasyScorers
            tf = ""
            For i As Integer = 1 To RecordCount
                tf = tf + EasyScores(i).Name + "," + EasyScores(i).Score.ToString + ";"
            Next
            My.Computer.FileSystem.WriteAllText(easyscorefile, tf, False)
            Me.Close()
        Else
            MsgBox("You Need To Enter A Valid Name.")
        End If
    End Sub
    Private Sub btnQuit_MouseHover(sender As Object, e As EventArgs) Handles btnQuit.MouseHover
        btnQuit.Image = My.Resources.QuitButtonHover
    End Sub
    Private Sub btnQuit_MouseLeave(sender As Object, e As EventArgs) Handles btnQuit.MouseLeave
        btnQuit.Image = My.Resources.QuitButton
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If Duration = 100 Then
            lblCountdown.Text = "   3"
        End If
        Duration += 1
        If Duration = 200 Then
            lblCountdown.Text = "   2"
        ElseIf Duration = 300 Then
            lblCountdown.Text = "   1"
        ElseIf Duration = 400 Then
            lblCountdown.Visible = False
            lblCountdown.Text = Nothing
            Gamebegan = True
        Else
            lblCountdown.Font = New Font("Tw Cen MT Condensed", 100 + Duration Mod 100)
        End If
    End Sub
End Class
