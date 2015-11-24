Option Strict Off
Option Explicit On
Module modSequence
	' data struct of a MIDI message
	Public Structure MIDI_Data
		Dim MidiMsg As Integer ' MIDI message
        Dim Time As System.TimeSpan ' the time that the MIDI message to be sent
	End Structure
	
	' data struct of a MIDI sequence
	Public Structure Sequence_Data
		Dim Data_Length As Integer ' Total number of MIDI message
		Dim Data() As MIDI_Data ' The MIDI message array
	End Structure
	
	' add a MIDI message to the MIDI sequence
    Public Sub Add_Sequence_Data(ByRef sequence As Sequence_Data, ByRef msg As Integer, ByRef t As System.TimeSpan)

        ReDim Preserve sequence.Data(sequence.Data_Length)

        Dim midiData As MIDI_Data
        midiData.MidiMsg = msg
        midiData.Time = t

        sequence.Data(sequence.Data_Length) = midiData
        sequence.Data_Length = sequence.Data_Length + 1

    End Sub
End Module