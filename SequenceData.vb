' class of a MIDI sequence
Public Class SequenceData

    ' attributes
    Public dataLength As Integer   ' Total number of MIDI message
    Public data() As MidiData      ' The MIDI message array


    ' class of a MIDI message
    Public Class MidiData
        Public midiMsg As Integer      ' MIDI message - fourth byte not used
        Public time As System.TimeSpan ' The time that the MIDI message occurs
    End Class

    ' constructor
    Public Sub New()
        dataLength = 0
    End Sub

    ' add a MIDI message to the MIDI sequence
    Public Sub AddSequenceData(ByRef msg As Integer, ByRef t As System.TimeSpan)

        ReDim Preserve Me.data(Me.dataLength)

        Dim midiData As New MidiData
        midiData.midiMsg = msg
        midiData.time = t

        Me.data(Me.dataLength) = midiData
        Me.dataLength = Me.dataLength + 1

    End Sub
End Class
