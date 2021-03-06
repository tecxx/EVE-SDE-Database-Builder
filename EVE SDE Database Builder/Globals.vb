﻿
' Common types and variables for the program
Public Module Globals

    Public CancelImport As Boolean = False
    Public frmErrorText As String = ""

    Public AllSettings As New ProgramSettings
    Public UserApplicationSettings As ApplicationSettings

    ''' <summary>
    ''' Displays error message from Try/Catch Exceptions
    ''' </summary>
    ''' <param name="ex">Exepction variable for displaying exception text</param>
    Public Sub ShowErrorMessage(ex As Exception)
        Dim msg As String = ex.Message
        If Not CancelImport Then
            If Not IsNothing(ex.InnerException) Then
                msg &= vbCrLf & vbCrLf & "Inner Exception: " & ex.InnerException.ToString
            End If
            Call MsgBox(msg, vbExclamation, Application.ProductName)
        End If
    End Sub

    ''' <summary>
    ''' Writes a sent message to the Errors.log file
    ''' </summary>
    ''' <param name="ErrorMsg">Message to write to log file</param>
    Public Sub WriteMsgToLog(ByVal ErrorMsg As String)
        Dim FilePath As String = "Errors.log"
        Dim AllText() As String

        If Not IO.File.Exists(FilePath) Then
            Dim sw As IO.StreamWriter = IO.File.CreateText(FilePath)
            sw.Close()
        End If

        ' This is an easier way to get all of the strings in the file.
        AllText = IO.File.ReadAllLines(FilePath)
        ' This will append the string to the end of the file.
        My.Computer.FileSystem.WriteAllText(FilePath, CStr(Now) & ", " & ErrorMsg & Environment.NewLine, True)


    End Sub

    ' Initializes the main form grid 
    Private Delegate Sub InitRow(ByVal Position As Integer)
    Public Sub InitGridRow(ByVal Postion As Integer)

        Dim f1 As frmMain = DirectCast(My.Application.OpenForms.Item("frmMain"), frmMain)
        f1.Invoke(New InitRow(AddressOf f1.InitGridRow), Postion)
        Application.DoEvents()
    End Sub

    ' Updates the main form grid
    Private Delegate Sub UpdateRowProgress(ByVal Position As Integer, ByVal Count As Integer, ByVal TotalRecords As Integer)
    Public Sub UpdateGridRowProgress(ByVal Postion As Integer, ByVal Count As Integer, ByVal TotalRecords As Integer)

        Dim f1 As frmMain = DirectCast(My.Application.OpenForms.Item("frmMain"), frmMain)
        f1.Invoke(New UpdateRowProgress(AddressOf f1.UpdateGridRowProgress), Postion, Count, TotalRecords)
        Application.DoEvents()
    End Sub

    ' Finalizes the main form grid
    Private Delegate Sub FinalizeRow(ByVal Position As Integer)
    Public Sub FinalizeGridRow(ByVal Postion As Integer)

        Dim f1 As frmMain = DirectCast(My.Application.OpenForms.Item("frmMain"), frmMain)
        f1.Invoke(New FinalizeRow(AddressOf f1.FinalizeGridRow), Postion)
        Application.DoEvents()
    End Sub

    ' Initializes the progressbar on the main form
    Private Delegate Sub InitMainProgress(MaxCount As Long, UpdateText As String)
    Public Sub InitalizeMainProgressBar(MaxCount As Long, UpdateText As String)

        Dim f1 As frmMain = DirectCast(My.Application.OpenForms.Item("frmMain"), frmMain)
        f1.Invoke(New InitMainProgress(AddressOf f1.InitalizeProgress), MaxCount, UpdateText)
        Application.DoEvents()
    End Sub

    ' Clears the progress bar and label on the main form
    Private Delegate Sub ClearMainProgress()
    Public Sub ClearMainProgressBar()

        Dim f1 As frmMain = DirectCast(My.Application.OpenForms.Item("frmMain"), frmMain)
        f1.Invoke(New ClearMainProgress(AddressOf f1.ClearProgress))
        Application.DoEvents()
    End Sub

    ' Updates the main progress bar and label on the main form
    Private Delegate Sub UpdateMainProgress(ByVal Count As Long, ByVal UpdateText As String)
    Public Sub UpdateMainProgressBar(Count As Long, UpdateText As String)

        Dim f1 As frmMain = DirectCast(My.Application.OpenForms.Item("frmMain"), frmMain)
        f1.Invoke(New UpdateMainProgress(AddressOf f1.UpdateProgress), Count, UpdateText)
        Application.DoEvents()
    End Sub

End Module
