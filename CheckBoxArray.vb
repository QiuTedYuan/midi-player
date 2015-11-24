Public Class CheckBoxArray
    Inherits System.Collections.CollectionBase

    Public Sub AddNewCheckBox(ByVal aCheckBox As System.Windows.Forms.CheckBox)

        ' Add the check box to the collection's internal list.
        aCheckBox.Tag = Me.Count
        Me.List.Add(aCheckBox)

    End Sub

    Default Public ReadOnly Property Item(ByVal Index As Integer) As System.Windows.Forms.CheckBox
        Get
            Return CType(Me.List.Item(Index), System.Windows.Forms.CheckBox)
        End Get
    End Property


End Class
