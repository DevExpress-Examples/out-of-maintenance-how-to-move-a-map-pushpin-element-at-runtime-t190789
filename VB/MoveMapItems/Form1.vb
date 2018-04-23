Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraMap
Imports DevExpress.Map

Namespace MoveMapItems
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
            InitMap()
        End Sub

        Private pin As MapPushpin

        Private Sub InitMap()
            Dim vlayer As VectorItemsLayer = CType(Me.mapControl1.Layers("ShapefileLayer"), VectorItemsLayer)
            Dim data As New ShapefileDataAdapter()

            data.FileUri = New Uri(String.Format("file:///{0}/Countries.shp", Environment.CurrentDirectory))
            vlayer.Data = data
        End Sub

        Private Sub mapControl1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles mapControl1.MouseDown
            Dim info As MapHitInfo = Me.mapControl1.CalcHitInfo(e.Location)
            If info.InMapPushpin Then
                Me.mapControl1.EnableScrolling = False
                For Each obj As Object In info.HitObjects
                    If obj.GetType() Is GetType(MapPushpin) Then
                    pin = DirectCast(obj, MapPushpin)
                    End If
                Next obj
            End If
        End Sub

        Private Sub mapControl1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles mapControl1.MouseMove
            If pin IsNot Nothing Then
                Dim point As CoordPoint = Me.mapControl1.ScreenPointToCoordPoint(New MapPoint(e.X, e.Y))
                pin.Location = point
            End If
        End Sub

        Private Sub mapControl1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles mapControl1.MouseUp
            If pin IsNot Nothing Then
                Dim point As CoordPoint = Me.mapControl1.ScreenPointToCoordPoint(New MapPoint(e.X, e.Y))
                pin.Location = point
                Me.mapControl1.EnableScrolling = True
                pin = Nothing
            End If
        End Sub
    End Class
End Namespace
