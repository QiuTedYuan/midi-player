Imports System.IO

' class of a MIDI File
Public Class MIDIFile

    Structure MIDICommandType
        Dim timeCode As Long
        Dim basicCommand As Byte
        Dim channelQualifier As Byte
        Dim commandQualifier As Byte
        Dim dataLength As Byte
        Dim dataType As String
        Dim dataName As String
        Dim actualData As String
        Dim description As String
    End Structure

    Structure MIDIMThd
        Dim headerName As String
        Dim dataLength As Long
        Dim fileType As Integer
        Dim numberOfTracks As Integer
        Dim deltaTimeTicks As Integer
    End Structure

    Structure MIDIMTrk
        Dim headerName As String
        Dim dataLength As Long
        Dim trackEvents() As MIDICommandType
    End Structure

    Private midiCommands() As MIDICommandType
    Private noteTable() As String

    Private fs As FileStream
    Private reader As BinaryReader
    Private writer As BinaryWriter

    Private fileHeader As MIDIMThd
    Private trackData() As MIDIMTrk

    Public Sub New()
    End Sub

    Public Sub New(ByVal midiFile As String)
        DecodeMIDI(midiFile)
    End Sub

    Private Sub AddMIDICommand(ByVal basicCommand As Byte, ByVal channelQualifier As Byte, ByVal commandQualifier As Byte, ByVal dataLength As Byte, ByVal dataType As String, ByVal dataName As String, ByVal description As String)
        If midiCommands Is Nothing Then
            ReDim midiCommands(0)
        Else
            ReDim Preserve midiCommands(midiCommands.Length)
        End If

        midiCommands(midiCommands.Length - 1).basicCommand = basicCommand
        midiCommands(midiCommands.Length - 1).channelQualifier = channelQualifier
        midiCommands(midiCommands.Length - 1).commandQualifier = commandQualifier
        midiCommands(midiCommands.Length - 1).dataLength = dataLength
        midiCommands(midiCommands.Length - 1).dataType = dataType
        midiCommands(midiCommands.Length - 1).dataName = dataName
        midiCommands(midiCommands.Length - 1).description = description
    End Sub

    Private Sub SetupMIDICommandTable()
        noteTable = New String() {"C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"}

        'Channel Commands

        AddMIDICommand(&H8, &H0, &H0, 2, "MusicNote;ByteDecimal", "Note;Velocity", "nn vv Note off (key is released)")
        AddMIDICommand(&H9, &H0, &H0, 2, "MusicNote;ByteDecimal", "Note;Velocity", "nn vv Note on (key is pressed)")
        AddMIDICommand(&HA, &H0, &H0, 2, "MusicNote;ByteDecimal", "Note;Velocity", "nn vv Key after-touch")
        AddMIDICommand(&HB, &H0, &H0, 2, "ByteHex;ByteDecimal", "Controller;Velocity", "cc vv Control Change")
        AddMIDICommand(&HC, &H0, &H0, 1, "ByteDecimal", "Patch Number", "pp    Program (patch) change")
        AddMIDICommand(&HD, &H0, &H0, 1, "ByteDecimal", "Channel", "cc    Channel after-touch")
        AddMIDICommand(&HE, &H0, &H0, 2, "ByteDecimal", "Bottom;Top", "bb tt Pitch wheel change (2000H is normal or no change)")

        ' System Commands

        AddMIDICommand(&HF, &H0, &H0, 99, "ByteHex", "SysEx Commands", "System Exclusive command - follwed by data stream to device")
        AddMIDICommand(&HF, &H8, &H0, 0, "", "", "Timing clock used when synchronisation is required")
        AddMIDICommand(&HF, &HA, &H0, 0, "", "", "Start current sequence")
        AddMIDICommand(&HF, &HB, &H0, 0, "", "", "Continue a stopped sequence where left off")
        AddMIDICommand(&HF, &HC, &H0, 0, "", "", "Stop a sequence")

        'Meta Commands

        AddMIDICommand(&HF, &HF, &H0, 99, "2ByteDecimal", "Tracks sequence number", "ssss  Sets the tracks sequence number.")
        AddMIDICommand(&HF, &HF, &H1, 99, "String", "Text", "tt    Text event- any text you want.")
        AddMIDICommand(&HF, &HF, &H2, 99, "String", "Copyright information", "tt    Same as text event, but used for copyright info.")
        AddMIDICommand(&HF, &HF, &H3, 99, "String", "Track name", "tt    Sequence or Track name")
        AddMIDICommand(&HF, &HF, &H4, 99, "String", "Track instrument name", "tt    Track instrument name")
        AddMIDICommand(&HF, &HF, &H5, 99, "String", "Lyric", "tt    Lyric")
        AddMIDICommand(&HF, &HF, &H6, 99, "String", "Marker", "tt    Marker")
        AddMIDICommand(&HF, &HF, &H7, 99, "String", "Cue point", "tt    Cue point")
        AddMIDICommand(&HF, &HF, &H2F, 99, "", "", "This event must come at the end of each track")
        AddMIDICommand(&HF, &HF, &H51, 99, "3ByteDecimal", "Tempo", "tttttt    Set tempo")
        AddMIDICommand(&HF, &HF, &H58, 99, "3ByteDecimal", "numerator;denominator;ticks;beats (32nd notes to quarter)", "nn dd cc bb   Time Signature")
        AddMIDICommand(&HF, &HF, &H59, 99, "ByteDecimal", "Sharps/Flats;Major/minor", "sf mi Key signature")
        AddMIDICommand(&HF, &HF, &H7F, 99, "ByteHex", "Sequencer specific information", "dd    Sequencer specific information")
        AddMIDICommand(&H0, &H0, &H0, 99, "ByteHex", "Unknown Data", "Unknown Command")
    End Sub

    Public Sub DecodeMIDI(ByVal midiFile As String)
        Dim teIndex As Long

        Dim wLong As Long
        Dim wShort As Integer
        Dim wString As String
        Dim wByte As Byte
        Dim i As Integer

        Dim dataTypeArray() As String
        Dim dtaIndex As Integer
        Dim dataNameArray() As String
        Dim dnaIndex As Integer
        Dim prevDataName As String

        Dim commandIndex As Integer

        Dim teDataArray() As String
        Dim teDataArrayIndex As Integer

        SetupMIDICommandTable()

        On Error GoTo 0

        fs = New FileStream(midiFile, FileMode.Open)
        reader = New BinaryReader(fs)

        If fs.Length < 14 Then
            MsgBox("The File is too short to contain the Header Chunk")
            reader.Close()
            Exit Sub
        End If

        fileHeader.headerName = Space(4)
        fileHeader.headerName = New System.Text.ASCIIEncoding().GetString(reader.ReadBytes(4))

        'MThd header chunk

        'Chunch ID
        If fileHeader.headerName <> "MThd" Then
            MsgBox("This is not a MIDI file." & vbCrLf & "The header at the start is not MTHd" & vbCrLf & "It is " & fileHeader.headerName)
            reader.Close()
            Exit Sub
        End If

        'Chunk length

        wLong = GetNumberFromFile(4)

        If wLong <> 6 Then
            MsgBox("This is not a MIDI file." & vbCrLf & "The header at the start is not 6 bytes long" & vbCrLf & "It is " & wLong)
            reader.Close()
            Exit Sub
        End If

        fileHeader.dataLength = Format(wLong, "0")

        'MIDI File Type -   0 = Single Track
        '                   1 = Multi-track sync
        '                   2 = Multi-track async

        wShort = GetNumberFromFile(2)

        If wShort < 0 Or wShort > 2 Then
            MsgBox("This is not a MIDI file." & vbCrLf & "The header at the start does not indicate a valid file type" & vbCrLf & "It is " & wShort)
            reader.Close()
            Exit Sub
        End If

        fileHeader.fileType = Format(wShort, "0")

        'Number of tracks

        wShort = GetNumberFromFile(2)

        fileHeader.numberOfTracks = Format(wShort, "0")

        ' delta time stuff

        wShort = GetNumberFromFile(2)

        fileHeader.deltaTimeTicks = Format(wShort, "0")

        ' MTrk Chunks

        Do

            If fs.Length - fs.Position < 8 Then
                reader.Close()
                Exit Sub
            End If

            If trackData Is Nothing Then
                ReDim trackData(0)
            Else
                ReDim Preserve trackData(trackData.Length)
            End If

            trackData(trackData.Length - 1).headerName = Space(4)
            trackData(trackData.Length - 1).headerName = New System.Text.ASCIIEncoding().GetString(reader.ReadBytes(4))

            'MTrk header chunk

            'Chunk ID
            If trackData(trackData.Length - 1).headerName <> "MTrk" Then
                MsgBox("This is not a Track chunk." & vbCrLf & "The header at the start is not 'MTrk'" & vbCrLf & "It is " & trackData(trackData.Length - 1).headerName)
                reader.Close()
                Exit Sub
            End If

            'Chunk length
            wLong = GetNumberFromFile(4)

            trackData(trackData.Length - 1).dataLength = Format(wLong, "0")

            wByte = reader.ReadByte()

            ' Do Track Events

            '//// NOTE:     //////////////////////////////////////////////////////////////
            '           This code can only deal with unknown events (i.e. commands omitted
            '           from the table
            '           if those commands are follwed by a data length byte.
            '           Otherwise chaos ensues.
            '/////////////////////////////////////////////////////////////////////////////

            ReDim trackData(trackData.Length - 1).trackEvents(0)
            teIndex = 0

            Do
                If fs.Position = fs.Length Then
                    reader.Close()
                    Exit Sub
                End If

                trackData(trackData.Length - 1).trackEvents(teIndex).timeCode = Format(ReadTimeCode(wByte), "0")

                If fs.Position = fs.Length Then
                    reader.Close()
                    Exit Sub
                End If

                'Get command
                wByte = reader.ReadByte()

                'if the next byte has the MSB set off (i.e. it is < 128) then
                'it is a "running command", and so the timecode is follwed directly by the data.

                If wByte < 128 Then
                    If teIndex > 0 Then
                        trackData(trackData.Length - 1).trackEvents(teIndex) = trackData(trackData.Length - 1).trackEvents(teIndex - 1)
                        trackData(trackData.Length - 1).trackEvents(teIndex).timeCode = 0
                        fs.Position = fs.Position - 1
                    End If
                Else
                    'Split Command Byte into two 4-bit nybbles (Basic Command and Channel Qualifier)
                    'and use them to lacate the appropriate entry in the Command Table
                    trackData(trackData.Length - 1).trackEvents(teIndex).basicCommand = (wByte And &HF0) / 16
                    commandIndex = trackData(trackData.Length - 1).trackEvents(teIndex).basicCommand - 8

                    trackData(trackData.Length - 1).trackEvents(teIndex).basicCommand = midiCommands(commandIndex).basicCommand

                    trackData(trackData.Length - 1).trackEvents(teIndex).channelQualifier = wByte And &HF

                    'Test for System and Meta commands and look up table as appropriate
                    If trackData(trackData.Length - 1).trackEvents(teIndex).basicCommand = &HF Then
                        'System and Meta commands
                        For i = commandIndex To UBound(midiCommands) - 1
                            If trackData(trackData.Length - 1).trackEvents(teIndex).channelQualifier = midiCommands(i).channelQualifier Then
                                Exit For
                            End If
                        Next i

                        commandIndex = i

                        'Test for Meta Commands
                        If trackData(trackData.Length - 1).trackEvents(teIndex).channelQualifier = &HF Then
                            'Meta Commands are follwed immediately by a Command Qualifier
                            'which identifies the specific Meta command
                            'the table must be searched again to find
                            'that specific meta command's entry
                            If fs.Position = fs.Length Then
                                reader.Close()
                                Exit Sub
                            End If

                            wByte = reader.ReadByte()

                            trackData(trackData.Length - 1).trackEvents(teIndex).commandQualifier = wByte

                            For i = commandIndex To UBound(midiCommands) - 1
                                If trackData(trackData.Length - 1).trackEvents(teIndex).commandQualifier = midiCommands(i).commandQualifier Then
                                    Exit For
                                End If
                            Next i

                            commandIndex = i
                            trackData(trackData.Length - 1).trackEvents(teIndex).commandQualifier = midiCommands(commandIndex).commandQualifier
                        Else

                            'System Commands have no Command Qlaifier and no data
                            trackData(trackData.Length - 1).trackEvents(teIndex).commandQualifier = 0

                        End If

                    Else
                        'Channel Commands have no Command Qlaifierand the length of data is
                        'fixed by the command, not by a data length byte
                        trackData(trackData.Length - 1).trackEvents(teIndex).commandQualifier = 0

                    End If
                    'running commands inherit all their characteristics from the preceding
                    'command and are immediately followed by theit data length byte
                    '(if appropriate) and/or their data
                End If

                'The command table entry is now complete.

                'Test for the need to read a data length byte
                If midiCommands(commandIndex).dataLength = 99 Then
                    'a data length byte is required, so read it
                    If fs.Position = fs.Length Then
                        reader.Close()
                        Exit Sub
                    End If

                    trackData(trackData.Length - 1).trackEvents(teIndex).dataLength = reader.ReadByte()

                Else
                    'No data length byte is required. The fixed length of the data
                    'can be read from the table entry
                    trackData(trackData.Length - 1).trackEvents(teIndex).dataLength = midiCommands(commandIndex).dataLength

                End If

                'Now read the data

                trackData(trackData.Length - 1).trackEvents(teIndex).dataType = midiCommands(commandIndex).dataType
                trackData(trackData.Length - 1).trackEvents(teIndex).dataName = midiCommands(commandIndex).dataName

                trackData(trackData.Length - 1).trackEvents(teIndex).actualData = ""
                i = trackData(trackData.Length - 1).trackEvents(teIndex).dataLength
                dataTypeArray = Split(midiCommands(commandIndex).dataType, ";")
                If UBound(dataTypeArray) = -1 Then
                    ReDim dataTypeArray(0)
                    dataTypeArray(0) = "ByteHex"
                End If

                dtaIndex = 0

                trackData(trackData.Length - 1).trackEvents(teIndex).dataName = ""
                dataNameArray = Split(midiCommands(commandIndex).dataName, ";")
                If UBound(dataNameArray) = -1 Then
                    ReDim dataNameArray(0)
                    dataNameArray(0) = ""
                End If

                dnaIndex = 0
                prevDataName = ""
                ReDim teDataArray(0)
                teDataArrayIndex = 0

                Do While i > 0

                    ReDim teDataArray(teDataArrayIndex)

                    Select Case dataTypeArray(dtaIndex)
                        Case "String"

                            wString = Space(trackData(trackData.Length - 1).trackEvents(teIndex).dataLength)
                            If fs.Length - fs.Position < trackData(trackData.Length - 1).trackEvents(teIndex).dataLength Then
                                reader.Close()
                                Exit Sub
                            End If
                            teDataArray(teDataArrayIndex) = New System.Text.ASCIIEncoding().GetString(reader.ReadBytes(trackData(trackData.Length - 1).trackEvents(teIndex).dataLength))
                            i = 0

                        Case "ByteDecimal"

                            wByte = GetNumberFromFile(1)
                            teDataArray(teDataArrayIndex) = Str$(wByte)
                            i = i - 1

                        Case "2ByteDecimal"

                            wShort = GetNumberFromFile(2)
                            teDataArray(teDataArrayIndex) = Str$(wShort)
                            i = i - 2

                        Case "3ByteDecimal"

                            wLong = GetNumberFromFile(3)
                            teDataArray(teDataArrayIndex) = Str$(wLong)
                            i = i - 3

                        Case "4ByteDecimal"

                            wLong = GetNumberFromFile(4)
                            teDataArray(teDataArrayIndex) = Str$(wLong)
                            i = i - 4

                        Case "ByteHex"

                            wByte = GetNumberFromFile(1)
                            teDataArray(teDataArrayIndex) = Right("00" & Hex(wByte), 2) & " "
                            i = i - 1

                        Case "MusicNote"

                            wByte = GetNumberFromFile(1)
                            teDataArray(teDataArrayIndex) = Str$(wByte)

                            i = i - 1

                        Case Else

                            wByte = GetNumberFromFile(1)
                            trackData(trackData.Length - 1).trackEvents(teIndex).actualData = trackData(trackData.Length - 1).trackEvents(teIndex).actualData & Right("00" & Hex(wByte), 2) & " "
                            i = i - 1

                    End Select

                    If dtaIndex < UBound(dataTypeArray) Then
                        dtaIndex = dtaIndex + 1
                    End If

                    trackData(trackData.Length - 1).trackEvents(teIndex).actualData = trackData(trackData.Length - 1).trackEvents(teIndex).actualData & teDataArray(teDataArrayIndex)

                    If prevDataName <> dataNameArray(dnaIndex) And dataNameArray(dnaIndex) <> "" Then
                        trackData(trackData.Length - 1).trackEvents(teIndex).dataName = trackData(trackData.Length - 1).trackEvents(teIndex).dataName & ", "
                    End If

                    prevDataName = dataNameArray(dnaIndex)

                    trackData(trackData.Length - 1).trackEvents(teIndex).dataName = trackData(trackData.Length - 1).trackEvents(teIndex).dataName & teDataArray(teDataArrayIndex)

                    If dnaIndex < UBound(dataNameArray) Then
                        dnaIndex = dnaIndex + 1
                    End If

                    teDataArrayIndex = teDataArrayIndex + 1

                Loop

                If Len(trackData(trackData.Length - 1).trackEvents(teIndex).dataName) > 2 Then
                    trackData(trackData.Length - 1).trackEvents(teIndex).dataName = Mid$(trackData(trackData.Length - 1).trackEvents(teIndex).dataName, 3)
                End If

                'the command has now been completed
                trackData(trackData.Length - 1).trackEvents(teIndex).description = midiCommands(commandIndex).description

                If fs.Position = fs.Length Then
                    reader.Close()
                    Exit Sub
                End If

                'Test for end of track chunk

                If trackData(trackData.Length - 1).trackEvents(teIndex).basicCommand = &HF And trackData(trackData.Length - 1).trackEvents(teIndex).channelQualifier = &HF And trackData(trackData.Length - 1).trackEvents(teIndex).commandQualifier = &H2F Then
                    Exit Do
                End If

                wByte = reader.ReadByte()

                teIndex = teIndex + 1
                ReDim Preserve trackData(trackData.Length - 1).trackEvents(teIndex)

                'Next Track Event
            Loop

            'Did you exit loop on end-of-track?

            'If trackData(trackData.Length - 1).trackEvents(teIndex).basicCommand = &HF And trackData(trackData.Length - 1).trackEvents(teIndex).channelQualifier = &HF And trackData(trackData.Length - 1).trackEvents(teIndex).commandQualifier = &H2F Then
            'Exit Do
            'End If


            'Next Track Chunk
        Loop

        reader.Close()
    End Sub

    Private Function ReadTimeCode(ByVal firstByte As Byte) As Long
        Dim wByte As Byte
        Dim timeCode As Long

        timeCode = 0

        wByte = firstByte

        Do Until wByte < 128
            timeCode = (timeCode * 128) + wByte - 128

            If fs.Position = fs.Length Then
                Exit Do
            End If

            wByte = reader.ReadByte()
        Loop

        Return (timeCode * 128) + wByte
    End Function

    Private Function GetNumberFromFile(ByVal numBytes As Integer) As Long
        Dim wByte As Byte
        Dim wLong As Long
        Dim i As Integer

        wLong = 0
        For i = 1 To numBytes
            wByte = reader.ReadByte()
            wLong = (wLong * 256) + wByte
        Next i

        Return wLong
    End Function

    Private Function FindTempo(ByVal trackNum As Integer) As Integer
        Dim i As Integer
        Dim tempo As Long = 0

        For i = 0 To trackData(trackNum).trackEvents.Length - 1
            With trackData(trackNum).trackEvents(i)
                If .basicCommand = &HF And .channelQualifier = &HF And .commandQualifier = &H51 Then
                    tempo = CLng(.dataName)
                    Exit For
                End If
            End With
        Next

        If tempo <> 0 Then
            tempo = 60000000 / tempo
        End If

        Return CInt(tempo)
    End Function

    Public Function ConvertToSequence() As SequenceData()
        If trackData Is Nothing Then
            Return Nothing
        End If

        Dim midiSequence As SequenceData
        Dim midiSequences() As SequenceData = Nothing
        Dim ticksPerBeat As Integer = 0
        Dim framesPerSec As Integer
        Dim ticksPerFrame As Integer
        Dim dataArray() As String
        Dim midimsg As Integer
        Dim i As Integer, j As Integer
        Dim tempo As Integer
        Dim time As Long

        If fileHeader.deltaTimeTicks < &H8000 Then
            ticksPerBeat = fileHeader.deltaTimeTicks
        Else
            framesPerSec = CInt(fileHeader.deltaTimeTicks / 256) - 128
            ticksPerFrame = CInt(fileHeader.deltaTimeTicks & &HFF)
        End If

        For i = 0 To fileHeader.numberOfTracks - 1
            If i >= trackData.Length Then
                Exit For
            End If

            tempo = 120

            midiSequence = New SequenceData
            time = 0

            For j = 0 To trackData(i).trackEvents.Length - 1
                midimsg = 0

                With trackData(i).trackEvents(j)
                    If ticksPerBeat > 0 Then
                        time += CLng(.timeCode / (ticksPerBeat * tempo) * 60 * TimeSpan.TicksPerSecond)
                    Else
                        time += CLng(.timeCode / (ticksPerBeat * tempo) * 60 * TimeSpan.TicksPerSecond)
                    End If

                    If .basicCommand = &HF And .channelQualifier = &HF And .commandQualifier = &H51 Then
                        tempo = 60000000 / CLng(.dataName)
                    End If

                    Select Case .basicCommand
                        Case &H8
                            dataArray = .dataName.Split(",")
                            midimsg = &H80 + (CInt(dataArray(0)) * &H100) + (CInt(dataArray(1)) * &H10000) + .channelQualifier
                        Case &H9
                            dataArray = .dataName.Split(",")
                            midimsg = &H90 + (CInt(dataArray(0)) * &H100) + (CInt(dataArray(1)) * &H10000) + .channelQualifier
                        Case &HA
                            dataArray = .dataName.Split(",")
                            midimsg = &HA0 + (CInt(dataArray(0)) * &H100) + (CInt(dataArray(1)) * &H10000) + .channelQualifier
                        Case &HB
                            dataArray = .dataName.Split(",")
                            midimsg = &HB0 + (Integer.Parse(dataArray(0), Globalization.NumberStyles.HexNumber) * &H100) + (CInt(dataArray(1)) * &H10000) + .channelQualifier
                        Case &HC
                            midimsg = &HA0 + (CInt(.dataName) * &H100) + .channelQualifier
                        Case &HD
                            midimsg = &HD0 + (CInt(.dataName) * &H100) + .channelQualifier
                        Case &HE
                            dataArray = .dataName.Split(",")
                            midimsg = &HE0 + (CInt(dataArray(0)) * &H100) + (CInt(dataArray(1)) * &H10000) + .channelQualifier
                    End Select
                End With

                If midimsg > 0 Then
                    midiSequence.AddSequenceData(midimsg, New TimeSpan(time))
                End If
            Next

            If midiSequence.dataLength > 0 Then
                If midiSequences Is Nothing Then
                    ReDim midiSequences(0)
                Else
                    ReDim Preserve midiSequences(midiSequences.Length)
                End If
                midiSequences(midiSequences.Length - 1) = midiSequence
            End If
        Next

        Return midiSequences
    End Function

End Class