Public Class MenuItemArray
    Inherits System.Collections.CollectionBase

    Public Sub AddNewMenuItem(ByVal aMenuItem As System.Windows.Forms.MenuItem)

        ' Add the MenuItem to the collection's internal list.
        aMenuItem.Index = Me.Count
        Me.List.Add(aMenuItem)

    End Sub

    Default Public ReadOnly Property Item(ByVal Index As Integer) As System.Windows.Forms.MenuItem
        Get
            Return CType(Me.List.Item(Index), System.Windows.Forms.MenuItem)
        End Get
    End Property

End Class
