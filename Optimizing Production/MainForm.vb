Public Class MainForm                                                           'Copyright by Javad Evazzadeh, all right reserved.

    Public Fline(,) As Integer                                                  'Declare FactoryLine(s) Array
    Public FIMinLine(,) As Integer                                              'Declare FactoryInstantMinLine Array
    Public FOMinLine(,) As Integer                                              'Declare FactoryOptimizedMinLine Array

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        ReDim Fline(TRow.Value, TCol.Value)                                     'Redim FactoryLine array
        ReDim FIMinLine(2, TCol.Value)                                          'Redim FactoryInstantMinLine array
        ReDim FOMinLine(2, TCol.Value)                                          'Redim FactoryOptimizedMinLine array
        List1.View = View.Details                                               'change listView Display
        Tree1.Nodes.Clear()                                                     'clear treeview
        List1.Clear()                                                           'clear listview
        Randomize()                                                             'Randomize number

        List1.Columns.Add("Line", 50, HorizontalAlignment.Left)                 'add Line Column to listwiew
        For i As Integer = 0 To TCol.Value - 1                                  'Loop Until end of columns
            List1.Columns.Add("Part " & i + 1, 60, HorizontalAlignment.Center)  'add Part(s) Columns to listview
        Next
        List1.Columns.Add("TotalTime", 79, HorizontalAlignment.Center)          'add TotalTime Column to listwiew
        List1.Items.Add("OMin")                                                 'Add Min on top of ListView

        For i As Integer = 0 To TRow.Value - 1                                  'Loop until end of Lines(Rows)
            Dim TotalTime As Integer = 0                                        'declare a variable for save TotalTime
            Tree1.Nodes.Add("Line " & i + 1)                                    'add Line Node to TreeView
            Tree1.Nodes.Item(i).BackColor = Color.LightBlue                     'Change TreeView backColor
            List1.Items.Add(i + 1)                                              'add Line Number to ListView
            For j As Integer = 0 To TCol.Value - 1                              'Loop until end of parts(Columns)
                Fline(i, j) = Int((PartMaxTime.Value - PartMinTime.Value + 1) * Rnd() + PartMinTime.Value)
                Tree1.Nodes(i).Nodes.Add(Fline(i, j) & " min")                  'Add Random Numbers(Data) to TreeView
                List1.Items(i + 1).SubItems.Add(Fline(i, j) & " min")           'Add Random Numbers(Data) to ListView
            Next
        Next
        List1.Items.Add("IMin")                                                 'Add Instant Min on bottom of ListView
        If TCol.Value * TRow.Value + TRow.Value <= 18 Then Tree1.ExpandAll() '  'If canshow all then show all
        CalcTotalTimeFEL()                                                      'Call CalculateTotalTimeforeachline Sub
    End Sub

    Private Sub Tree1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Tree1.AfterSelect
        Dim UserTime As String                                                  'Declare user Time Variable
        If Len(Tree1.SelectedNode.FullPath) > 8 Then                            'If User Select a child node then give user Time
            UserTime = InputBox("زمان جدیدی را برای این مرحله وارد کنید", "ورود اطلاعات", 5)            'For selected Part
            If IsNumeric(UserTime) Then                                         'If user input Correct Value type (Number) then
                If UserTime >= PartMinTime.Value And UserTime <= PartMaxTime.Value Then 'If user input correct value(min-max) then
                    Dim Tmp As Integer                                          'Declare variable for Find Line and Part Number  
                    Tmp = (Mid(Tree1.SelectedNode.FullPath, 5, InStr(Tree1.SelectedNode.FullPath, "\") - 5) - 1)
                    Fline(Tmp, Tree1.SelectedNode.Index) = UserTime             'assign correct time to Factory Lines Array
                    Tree1.SelectedNode.Text = UserTime & " min"                 'Update Time of Selected Part in TreeView
                    'Update Time of Selected part in ListView
                    List1.Items(Tmp + 1).SubItems(Tree1.SelectedNode.Index + 1).Text = UserTime & " min"
                Else                                                            'else show related message
                    MsgBox("زمان وارد شده صحیح نیست، زمانی بین حداقل و حداکثر وارد کنید", vbCritical, "خطا")
                End If
            Else                                                                'else show related message
                MsgBox("مقدار وارد شده صحیح نیست، تنها اعداد مجاز هستند", vbCritical, "خطا")
            End If
        End If
        CalcTotalTimeFEL()                                                      'Call CalculateTotalTimeforeachline Sub
    End Sub

    Private Sub CalcTotalTimeFEL()                                              'Calculate Total Time for each line'''
        Dim ShortestLineNum As Integer = TRow.Value + 1                         'Declare shortestLineNum 
        Dim Min As Integer = TCol.Value * 30                                    'Declare MinLine For Factory
        For row As Integer = 0 To TRow.Value - 1                                'Loop until end of Lines(Rows)
            Dim TotalTimeFEL As Integer = 0                                     'declare a variable for save TotalTime
            For col As Integer = 0 To TCol.Value - 1                            'Loop until end of parts(Columns)
                TotalTimeFEL += Fline(row, col)                                 'Calculate Total time for each Line
            Next
            If Min > TotalTimeFEL Then                                          'if min greater than totaltime of line
                Min = TotalTimeFEL                                              'assign totaltimefel to min
                ShortestLineNum = row                                           'assign current row to shortestLineNum
            End If

            Try                                                                 'Try edit totoal time for each line
                List1.Items(row + 1).SubItems(Convert.ToInt32(TCol.Value) + 1).Text = (TotalTimeFEL & " min")
            Catch ex As Exception                                               'on Catch add total time(first time)
                List1.Items(row + 1).SubItems.Add(TotalTimeFEL & " min")
            End Try
            List1.Items(row + 1).BackColor = Color.FloralWhite                  'change backcolor of current row
        Next
        List1.Items(ShortestLineNum + 1).BackColor = Color.NavajoWhite          'change backcolor of shortest row
        CalcInstantMin()                                                        'Call CalcInstantMin Sub
        CalcOptimizedMin()                                                      'Call CalcOptimizedMin
    End Sub

    Private Sub CalcInstantMin()
        Dim TransferCount As Integer = 0                                        'Declare Transfer count
        Dim TotalMin As Integer = 0                                             'Declare Instant Min-Total Min
        Dim PreviousMinLine As Integer                                          'Store min line for previous column
        List1.Items(List1.Items.Count - 1).SubItems(0).Text = "IMin"            'change Instant min title

        For col As Integer = 0 To TCol.Value - 1                                'Loop until end of Parts (Col)
            Dim Min() As Integer = {TRow.Value + 1, PartMaxTime.Value + 1}      'Store min & Line value for each column
            For row As Integer = 0 To TRow.Value - 1                            'Loop until end of Lines(Rows)
                If Fline(row, col) <= Min(1) Then                               'if fline(row,col) < min then
                    Min(1) = Fline(row, col)                                    'assign fline(row,col) min value to min
                    Min(0) = row                                                'assign minrow line to min
                End If
            Next                                                                'find min value & line for each column
            If col = 0 Then                                                     'on first column
                FIMinLine(0, col) = Min(0)                                      'assign minline of column to FIMinLine
                FIMinLine(1, col) = Fline(Min(0), col)                          'assign minvalue of column to FIMinLine
                PreviousMinLine = Min(0)
            Else                                                                'else if fline(previousminline,col) <= min+transfer
                If Fline(PreviousMinLine, col) <= Min(1) + Convert.ToInt32(TTime.Value) Then
                    FIMinLine(0, col) = PreviousMinLine                         'assign PreviousMinLine of column to FIMinLine
                    FIMinLine(1, col) = Fline(PreviousMinLine, col)             'assign Fline(PreviousMinLine, col)to FIMinLine
                Else                                                            'else(if this line is diffrent with previous)
                    FIMinLine(0, col) = Min(0)                                  'assign minline of column to FIMinLine
                    FIMinLine(1, col) = Min(1)                                  'assign minvalue of column to FIMinLine
                    PreviousMinLine = Min(0)                                    'assign minLine to previousminline
                    TransferCount += 1                                          'increase transfer
                End If
            End If
            TotalMin += FIMinLine(1, col)                                       'add current column min to totalmin
            Try                                                                 'edit subitem except first time 
                List1.Items(List1.Items.Count - 1).SubItems(col + 1).Text = "L" & FIMinLine(0, col) + 1 & "-" & FIMinLine(1, col) & " min"
            Catch ex As Exception                                               'add subitem if not exist (first time)
                List1.Items(List1.Items.Count - 1).SubItems.Add("L" & FIMinLine(0, col) + 1 & "-" & FIMinLine(1, col) & " min")
            End Try
        Next                                                                    'Calculate Instant Min for factory
        Try                                                                     'edit subitem except first time 
            List1.Items(List1.Items.Count - 1).SubItems(Convert.ToInt32(TCol.Value) + 1).Text = TotalMin & "+" & TransferCount * TTime.Value & " min"
        Catch ex As Exception                                                   'add subitem if not exist (first time)
            List1.Items(List1.Items.Count - 1).SubItems.Add(TotalMin & "+" & TransferCount * TTime.Value & " min")
        End Try
        If TransferCount = 0 Then List1.Items(List1.Items.Count - 1).SubItems(0).Text = "IMin(L" & PreviousMinLine + 1 & ")"

        List1.Items(List1.Items.Count - 1).BackColor = Color.Orange             'change instant min backcolor
    End Sub

    Private Sub CalcOptimizedMin()
        Dim TransferCount As Integer = 0                                        'Declare Transfer count
        Dim Transfer As Integer = 0
        Dim TotalMin As Integer = 0                                             'Declare Instant Min-Total Min
        Dim FTmpV(TRow.Value, TCol.Value) As Integer
        Dim FTmpL(TRow.Value, TCol.Value) As Integer

        For col As Integer = 0 To TCol.Value - 1
            Dim SL As New List(Of Integer)
            For row As Integer = 0 To TRow.Value - 1
                SL.Add(Fline(row, col) & "070" & row)
            Next
            SL.Sort()
            For row As Integer = 0 To TRow.Value - 1
                FTmpV(row, col) = SL(row).ToString.Substring(0, InStr(SL(row), "070") - 1)
                FTmpL(row, col) = (Mid(SL(row), InStr(SL(row), "070") + 3))
            Next
        Next

        For col As Integer = 0 To TCol.Value - 1
            Dim SL As New List(Of Integer)
            Dim CN(TRow.Value, 2) As Integer
            Dim MCN As Integer = 0


            If col = 0 Then
                If FTmpL(0, col) = FTmpL(0, col + 1) Then
                    If FTmpL(0, col + 1) = FTmpL(0, col + 2) Then
                        If FTmpL(0, col + 2) = FTmpL(0, col + 3) Then
                            MCN = FTmpV(0, col) + FTmpV(0, col + 1) + FTmpV(0, col + 2) + FTmpV(0, col + 3)
                        Else
                            MCN = FTmpV(0, col) + FTmpV(0, col + 1) + FTmpV(0, col + 2) + TTime.Value + FTmpV(0, col + 3)
                            Transfer += 1
                        End If
                    Else
                        If FTmpL(0, col + 2) = FTmpL(0, col + 3) Then
                            MCN = FTmpV(0, col) + FTmpV(0, col + 1) + TTime.Value + FTmpV(0, col + 2) + FTmpV(0, col + 3)
                            Transfer += 1
                        Else
                            MCN = FTmpV(0, col) + FTmpV(0, col + 1) + TTime.Value + FTmpV(0, col + 2) + TTime.Value + FTmpV(0, col + 3)
                            Transfer += 2
                        End If
                    End If
                Else
                    If FTmpL(0, col + 1) = FTmpL(0, col + 2) Then
                        If FTmpL(0, col + 2) = FTmpL(0, col + 3) Then
                            MCN = FTmpV(0, col) + TTime.Value + FTmpV(0, col + 1) + FTmpV(0, col + 2) + FTmpV(0, col + 3)
                            Transfer += 1
                        Else
                            MCN = FTmpV(0, col) + TTime.Value + FTmpV(0, col + 1) + FTmpV(0, col + 2) + TTime.Value + FTmpV(0, col + 3)
                            Transfer += 2
                        End If
                    Else
                        If FTmpL(0, col + 2) = FTmpL(0, col + 3) Then
                            MCN = FTmpV(0, col) + TTime.Value + FTmpV(0, col + 1) + TTime.Value + FTmpV(0, col + 2) + FTmpV(0, col + 3)
                            Transfer += 2
                        Else
                            MCN = FTmpV(0, col) + TTime.Value + FTmpV(0, col + 1) + TTime.Value + FTmpV(0, col + 2) + TTime.Value + FTmpV(0, col + 3)
                            Transfer += 3
                        End If
                    End If
                End If
            ElseIf col >= TCol.Value - 1 Then
                If FOMinLine(0, col - 1) = FTmpL(0, col) Then
                    MCN = FOMinLine(1, col - 1) + FTmpV(0, col)
                Else
                    MCN = FOMinLine(1, col - 1) + TTime.Value + FTmpV(0, col)
                    Transfer += 1
                End If
            Else
                '                MsgBox(FTmpV(0, col))
                If FOMinLine(0, col - 1) = FTmpL(0, col) Then
                    If FTmpL(0, col) = FTmpL(0, col + 1) Then
                        If FTmpL(0, col + 1) = FTmpL(0, col + 2) Then
                            MCN = FOMinLine(1, col - 1) + FTmpV(0, col) + FTmpV(0, col + 1) + FTmpV(0, col + 2)
                        Else
                            MCN = FOMinLine(1, col - 1) + FTmpV(0, col) + FTmpV(0, col + 1) + TTime.Value + FTmpV(0, col + 2)
                            Transfer += 1
                        End If
                    Else
                        If FTmpL(0, col + 1) = FTmpL(0, col + 2) Then
                            MCN = FOMinLine(1, col - 1) + FTmpV(0, col) + TTime.Value + FTmpV(0, col + 1) + FTmpV(0, col + 2)
                            Transfer += 1
                        Else
                            MCN = FOMinLine(1, col - 1) + FTmpV(0, col) + TTime.Value + FTmpV(0, col + 1) + TTime.Value + FTmpV(0, col + 2)
                            Transfer += 2
                        End If
                    End If
                Else
                    If FTmpL(0, col) = FTmpL(0, col + 1) Then
                        If FTmpL(0, col + 1) = FTmpL(0, col + 2) Then
                            MCN = FOMinLine(1, col - 1) + TTime.Value + FTmpV(0, col) + FTmpV(0, col + 1) + FTmpV(0, col + 2)
                            Transfer += 1
                        Else
                            MCN = FOMinLine(1, col - 1) + TTime.Value + FTmpV(0, col) + FTmpV(0, col + 1) + TTime.Value + FTmpV(0, col + 2)
                            Transfer += 2
                        End If
                    Else
                        If FTmpL(0, col + 1) = FTmpL(0, col + 2) Then
                            MCN = FOMinLine(1, col - 1) + TTime.Value + FTmpV(0, col) + TTime.Value + FTmpV(0, col + 1) + FTmpV(0, col + 2)
                            Transfer += 2
                        Else
                            MCN = FOMinLine(1, col - 1) + TTime.Value + FTmpV(0, col) + TTime.Value + FTmpV(0, col + 1) + TTime.Value + FTmpV(0, col + 2)
                            Transfer += 3
                        End If
                    End If
                End If
            End If

            If col = 0 Then
                For row As Integer = 0 To TRow.Value - 1
                    SL.Add(Fline(FTmpL(row, col), col) + Fline(FTmpL(row, col), col + 1) + Fline(FTmpL(row, col), col + 2) + Fline(FTmpL(row, col), col + 3) & "070" & row)
                Next
            ElseIf col >= TCol.Value - 1 Then
                For row As Integer = 0 To TRow.Value - 1
                    SL.Add(Fline(FTmpL(row, col), col - 1) + Fline(FTmpL(row, col), col) & "070" & row)
                Next
            Else
                For row As Integer = 0 To TRow.Value - 1
                    SL.Add(Fline(FTmpL(row, col), col - 1) + Fline(FTmpL(row, col), col) + Fline(FTmpL(row, col), col + 1) + Fline(FTmpL(row, col), col + 2) & "070" & row)
                Next
            End If
            SL.Sort()
            For row As Integer = 0 To TRow.Value - 1
                CN(row, 1) = SL(row).ToString.Substring(0, InStr(SL(row), "070") - 1)
                CN(row, 0) = (Mid(SL(row), InStr(SL(row), "070") + 3))
            Next

            If MCN <= CN(0, 1) Then
                FOMinLine(0, col) = FTmpL(0, col)
                FOMinLine(1, col) = FTmpV(0, col)
            Else
                FOMinLine(0, col) = FTmpL(CN(0, 0), col)
                FOMinLine(1, col) = Fline(FTmpL(CN(0, 0), col), col)
            End If
            If col <> 0 Then If FOMinLine(0, col - 1) <> FOMinLine(0, col) Then TransferCount += 1

            Try                                                                 'edit subitem except first time 
                List1.Items(0).SubItems(col + 1).Text = "L" & FOMinLine(0, col) + 1 & "-" & FOMinLine(1, col) & " min"
            Catch ex As Exception                                               'add subitem if not exist (first time)
                List1.Items(0).SubItems.Add("L" & FOMinLine(0, col) + 1 & "-" & FOMinLine(1, col) & " min")
            End Try

            TotalMin += FOMinLine(1, col)                                       'add current column min to totalmin
        Next

        Try                                                                     'edit subitem except first time 
            List1.Items(0).SubItems(Convert.ToInt32(TCol.Value) + 1).Text = TotalMin & "+" & TransferCount * TTime.Value & " min"
        Catch ex As Exception                                                   'add subitem if not exist (first time)
            List1.Items(0).SubItems.Add(TotalMin & "+" & TransferCount * TTime.Value & " min")
        End Try

        List1.Items(0).BackColor = Color.GreenYellow             'change instant min backcolor
    End Sub

    Private Sub TTime_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TTime.ValueChanged
        Try                                                                     'Try call CalcMin Sub
            CalcInstantMin()
            CalcOptimizedMin()
        Catch ex As Exception                                                   'Only First time occur
        End Try
    End Sub

    Private Sub Programmer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Programmer.Click
        MsgBox("E-Mail: J.Evazzadeh@Gmail.com" & Chr(13) & "Mobile: 0935-726-9759", , "Javad Evazzadeh (870382401)")
    End Sub

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BtnRefresh_Click(e, EventArgs.Empty)
    End Sub
End Class                                                                       'Copyright by Javad Evazzadeh, all right reserved.
