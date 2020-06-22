Imports System.Runtime.InteropServices

Public Class Card

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SCARD_IO_REQUEST

        Public dwProtocol As Integer

        Public cbPciLength As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure APDURec

        Public bCLA As Byte

        Public bINS As Byte

        Public bP1 As Byte

        Public bP2 As Byte

        Public bP3 As Byte

        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=256)> _
        Public Data() As Byte

        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)> _
        Public SW() As Byte

        Public IsSend As Boolean
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SCARD_READERSTATE

        Public RdrName As String

        Public UserData As Integer

        Public RdrCurrState As Integer

        Public RdrEventState As Integer

        Public ATRLength As Integer

        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=37)> _
        Public ATRValue() As Byte
    End Structure

    Public Const SCARD_S_SUCCESS As Integer = 0

    Public Const SCARD_ATR_LENGTH As Integer = 33

    Public Const CT_MCU As Integer = 0

    ' MCU
    Public Const CT_IIC_Auto As Integer = 1

    ' IIC (Auto Detect Memory Size)
    Public Const CT_IIC_1K As Integer = 2

    ' IIC (1K)
    Public Const CT_IIC_2K As Integer = 3

    ' IIC (2K)
    Public Const CT_IIC_4K As Integer = 4

    ' IIC (4K)
    Public Const CT_IIC_8K As Integer = 5

    ' IIC (8K)
    Public Const CT_IIC_16K As Integer = 6

    ' IIC (16K)
    Public Const CT_IIC_32K As Integer = 7

    ' IIC (32K)
    Public Const CT_IIC_64K As Integer = 8

    ' IIC (64K)
    Public Const CT_IIC_128K As Integer = 9

    ' IIC (128K)
    Public Const CT_IIC_256K As Integer = 10

    ' IIC (256K)
    Public Const CT_IIC_512K As Integer = 0

    ' IIC (512K)
    Public Const CT_IIC_1024K As Integer = 12

    ' IIC (1024K)
    Public Const CT_AT88SC153 As Integer = 13

    ' AT88SC153
    Public Const CT_AT88SC1608 As Integer = 14

    ' AT88SC1608
    Public Const CT_SLE4418 As Integer = 15

    ' SLE4418
    Public Const CT_SLE4428 As Integer = 16

    ' SLE4428
    Public Const CT_SLE4432 As Integer = 17

    ' SLE4432
    Public Const CT_SLE4442 As Integer = 18

    ' SLE4442
    Public Const CT_SLE4406 As Integer = 19

    ' SLE4406
    Public Const CT_SLE4436 As Integer = 20

    ' SLE4436
    Public Const CT_SLE5536 As Integer = 21

    ' SLE5536
    Public Const CT_MCUT0 As Integer = 22

    ' MCU T=0
    Public Const CT_MCUT1 As Integer = 23

    ' MCU T=1
    Public Const CT_MCU_Auto As Integer = 24

    ' MCU Autodetect
    Public Const SCARD_SCOPE_USER As Integer = 0

    Public Const SCARD_SCOPE_TERMINAL As Integer = 1

    Public Const SCARD_SCOPE_SYSTEM As Integer = 2

    Public Const SCARD_STATE_UNAWARE As Integer = 0

    Public Const SCARD_STATE_IGNORE As Integer = 1

    Public Const SCARD_STATE_CHANGED As Integer = 2

    Public Const SCARD_STATE_UNKNOWN As Integer = 4

    Public Const SCARD_STATE_UNAVAILABLE As Integer = 8

    Public Const SCARD_STATE_EMPTY As Integer = 16

    Public Const SCARD_STATE_PRESENT As Integer = 32

    Public Const SCARD_STATE_ATRMATCH As Integer = 64

    Public Const SCARD_STATE_EXCLUSIVE As Integer = 128

    Public Const SCARD_STATE_INUSE As Integer = 256

    Public Const SCARD_STATE_MUTE As Integer = 512

    Public Const SCARD_STATE_UNPOWERED As Integer = 1024

    Public Const SCARD_SHARE_EXCLUSIVE As Integer = 1

    Public Const SCARD_SHARE_SHARED As Integer = 2

    Public Const SCARD_SHARE_DIRECT As Integer = 3

    Public Const SCARD_LEAVE_CARD As Integer = 0

    ' Don't do anything special on close
    Public Const SCARD_RESET_CARD As Integer = 1

    ' Reset the card on close
    Public Const SCARD_UNPOWER_CARD As Integer = 2

    ' Power down the card on close
    Public Const SCARD_EJECT_CARD As Integer = 3

    ' Eject the card on close
    Public Const FILE_DEVICE_SMARTCARD As Long = 3211264

    ' Reader action IOCTLs
    Public Const IOCTL_SMARTCARD_DIRECT As Long = (FILE_DEVICE_SMARTCARD + (2050 * 4))

    Public Const IOCTL_SMARTCARD_SELECT_SLOT As Long = (FILE_DEVICE_SMARTCARD + (2051 * 4))

    Public Const IOCTL_SMARTCARD_DRAW_LCDBMP As Long = (FILE_DEVICE_SMARTCARD + (2052 * 4))

    Public Const IOCTL_SMARTCARD_DISPLAY_LCD As Long = (FILE_DEVICE_SMARTCARD + (2053 * 4))

    Public Const IOCTL_SMARTCARD_CLR_LCD As Long = (FILE_DEVICE_SMARTCARD + (2054 * 4))

    Public Const IOCTL_SMARTCARD_READ_KEYPAD As Long = (FILE_DEVICE_SMARTCARD + (2055 * 4))

    Public Const IOCTL_SMARTCARD_READ_RTC As Long = (FILE_DEVICE_SMARTCARD + (2057 * 4))

    Public Const IOCTL_SMARTCARD_SET_RTC As Long = (FILE_DEVICE_SMARTCARD + (2058 * 4))

    Public Const IOCTL_SMARTCARD_SET_OPTION As Long = (FILE_DEVICE_SMARTCARD + (2059 * 4))

    Public Const IOCTL_SMARTCARD_SET_LED As Long = (FILE_DEVICE_SMARTCARD + (2060 * 4))

    Public Const IOCTL_SMARTCARD_LOAD_KEY As Long = (FILE_DEVICE_SMARTCARD + (2062 * 4))

    Public Const IOCTL_SMARTCARD_READ_EEPROM As Long = (FILE_DEVICE_SMARTCARD + (2065 * 4))

    Public Const IOCTL_SMARTCARD_WRITE_EEPROM As Long = (FILE_DEVICE_SMARTCARD + (2066 * 4))

    Public Const IOCTL_SMARTCARD_GET_VERSION As Long = (FILE_DEVICE_SMARTCARD + (2067 * 4))

    Public Const IOCTL_SMARTCARD_GET_READER_INFO As Long = (FILE_DEVICE_SMARTCARD + (2051 * 4))

    Public Const IOCTL_SMARTCARD_SET_CARD_TYPE As Long = (FILE_DEVICE_SMARTCARD + (2060 * 4))

    Public Const IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND As Long = (FILE_DEVICE_SMARTCARD + (2079 * 4))

    Public Const SCARD_F_INTERNAL_ERROR As Integer = -2146435071

    Public Const SCARD_E_CANCELLED As Integer = -2146435070

    Public Const SCARD_E_INVALID_HANDLE As Integer = -2146435069

    Public Const SCARD_E_INVALID_PARAMETER As Integer = -2146435068

    Public Const SCARD_E_INVALID_TARGET As Integer = -2146435067

    Public Const SCARD_E_NO_MEMORY As Integer = -2146435066

    Public Const SCARD_F_WAITED_TOO_LONG As Integer = -2146435065

    Public Const SCARD_E_INSUFFICIENT_BUFFER As Integer = -2146435064

    Public Const SCARD_E_UNKNOWN_READER As Integer = -2146435063

    Public Const SCARD_E_TIMEOUT As Integer = -2146435062

    Public Const SCARD_E_SHARING_VIOLATION As Integer = -2146435061

    Public Const SCARD_E_NO_SMARTCARD As Integer = -2146435060

    Public Const SCARD_E_UNKNOWN_CARD As Integer = -2146435059

    Public Const SCARD_E_CANT_DISPOSE As Integer = -2146435058

    Public Const SCARD_E_PROTO_MISMATCH As Integer = -2146435057

    Public Const SCARD_E_NOT_READY As Integer = -2146435056

    Public Const SCARD_E_INVALID_VALUE As Integer = -2146435055

    Public Const SCARD_E_SYSTEM_CANCELLED As Integer = -2146435054

    Public Const SCARD_F_COMM_ERROR As Integer = -2146435053

    Public Const SCARD_F_UNKNOWN_ERROR As Integer = -2146435052

    Public Const SCARD_E_INVALID_ATR As Integer = -2146435051

    Public Const SCARD_E_NOT_TRANSACTED As Integer = -2146435050

    Public Const SCARD_E_READER_UNAVAILABLE As Integer = -2146435049

    Public Const SCARD_P_SHUTDOWN As Integer = -2146435048

    Public Const SCARD_E_PCI_TOO_SMALL As Integer = -2146435047

    Public Const SCARD_E_READER_UNSUPPORTED As Integer = -2146435046

    Public Const SCARD_E_DUPLICATE_READER As Integer = -2146435045

    Public Const SCARD_E_CARD_UNSUPPORTED As Integer = -2146435044

    Public Const SCARD_E_NO_SERVICE As Integer = -2146435043

    Public Const SCARD_E_SERVICE_STOPPED As Integer = -2146435042

    Public Const SCARD_W_UNSUPPORTED_CARD As Integer = -2146435041

    Public Const SCARD_W_UNRESPONSIVE_CARD As Integer = -2146435040

    Public Const SCARD_W_UNPOWERED_CARD As Integer = -2146435039

    Public Const SCARD_W_RESET_CARD As Integer = -2146435038

    Public Const SCARD_W_REMOVED_CARD As Integer = -2146435037

    Public Const SCARD_PROTOCOL_UNDEFINED As Integer = 0

    ' There is no active protocol.
    Public Const SCARD_PROTOCOL_T0 As Integer = 1

    ' T=0 is the active protocol.
    Public Const SCARD_PROTOCOL_T1 As Integer = 2

    ' T=1 is the active protocol.
    Public Const SCARD_PROTOCOL_RAW As Integer = 65536

    ' Raw is the active protocol.
    'public const int SCARD_PROTOCOL_DEFAULT = 0x80000000;      // Use implicit PTS.
    Public Const SCARD_UNKNOWN As Integer = 0

    Public Const SCARD_ABSENT As Integer = 1

    Public Const SCARD_PRESENT As Integer = 2

    Public Const SCARD_SWALLOWED As Integer = 3

    Public Const SCARD_POWERED As Integer = 4

    Public Const SCARD_NEGOTIABLE As Integer = 5

    Public Const SCARD_SPECIFIC As Integer = 6

    Public Declare Function SCardEstablishContext Lib "winscard.dll" (ByVal dwScope As Integer, ByVal pvReserved1 As Integer, ByVal pvReserved2 As Integer, ByRef phContext As Integer) As Integer

    Public Declare Function SCardReleaseContext Lib "winscard.dll" (ByVal phContext As Integer) As Integer

    Public Declare Function SCardConnectA Lib "winscard.dll" (ByVal hContext As Integer, ByVal szReaderName As String, ByVal dwShareMode As Integer, ByVal dwPrefProtocol As Integer, ByRef phCard As Integer, ByRef ActiveProtocol As Integer) As Integer

    Public Declare Function SCardBeginTransaction Lib "winscard.dll" (ByVal hCard As Integer) As Integer

    Public Declare Function SCardDisconnect Lib "winscard.dll" (ByVal hCard As Integer, ByVal Disposition As Integer) As Integer

    Public Declare Function SCardListReaderGroups Lib "winscard.dll" (ByVal hContext As Integer, ByRef mzGroups As String, ByRef pcchGroups As Integer) As Integer

    Public Declare Function SCardListReaders Lib "winscard.DLL" Alias "SCardListReadersA" (ByVal hContext As Integer, ByVal Groups() As Byte, ByVal Readers() As Byte, ByRef pcchReaders As Integer) As Integer

    Public Declare Function SCardStatus Lib "winscard.dll" (ByVal hCard As Integer, ByVal szReaderName As String, ByRef pcchReaderLen As Integer, ByRef State As Integer, ByRef Protocol As Integer, ByRef ATR As Byte, ByRef ATRLen As Integer) As Integer

    Public Declare Function SCardEndTransaction Lib "winscard.dll" (ByVal hCard As Integer, ByVal Disposition As Integer) As Integer

    Public Declare Function SCardState Lib "winscard.dll" (ByVal hCard As Integer, ByRef State As UInteger, ByRef Protocol As UInteger, ByRef ATR As Byte, ByRef ATRLen As UInteger) As Integer

    Public Overloads Declare Function SCardTransmit Lib "WinScard.dll" (ByVal hCard As IntPtr, ByRef pioSendPci As SCARD_IO_REQUEST, ByRef pbSendBuffer() As Byte, ByVal cbSendLength As Integer, ByRef pioRecvPci As SCARD_IO_REQUEST, ByRef pbRecvBuffer() As Byte, ByRef pcbRecvLength As Integer) As Integer

    Public Overloads Declare Function SCardTransmit Lib "winscard.dll" (ByVal hCard As Integer, ByRef pioSendRequest As SCARD_IO_REQUEST, ByRef SendBuff As Byte, ByVal SendBuffLen As Integer, ByRef pioRecvRequest As SCARD_IO_REQUEST, ByRef RecvBuff As Byte, ByRef RecvBuffLen As Integer) As Integer

    Public Overloads Declare Function SCardTransmit Lib "winscard.dll" (ByVal hCard As Integer, ByRef pioSendRequest As SCARD_IO_REQUEST, ByRef SendBuff() As Byte, ByVal SendBuffLen As Integer, ByRef pioRecvRequest As SCARD_IO_REQUEST, ByRef RecvBuff() As Byte, ByRef RecvBuffLen As Integer) As Integer

    Public Declare Function SCardControl Lib "winscard.dll" (ByVal hCard As Integer, ByVal dwControlCode As UInteger, ByRef SendBuff As Byte, ByVal SendBuffLen As Integer, ByRef RecvBuff As Byte, ByVal RecvBuffLen As Integer, ByRef pcbBytesReturned As Integer) As Integer

    Public Declare Function SCardGetStatusChangeA Lib "winscard.dll" (ByVal hContext As Integer, ByVal TimeOut As Integer, ByRef ReaderState As SCARD_READERSTATE, ByVal ReaderCount As Integer) As Integer

    Public Shared Function GetScardErrMsg(ByVal ReturnCode As Integer) As String
        Select Case (ReturnCode)
            Case SCARD_E_CANCELLED
                Return "The action was canceled by an SCardCancel request."
            Case SCARD_E_CANT_DISPOSE
                Return "The system could not dispose of the media in the requested manner."
            Case SCARD_E_CARD_UNSUPPORTED
                Return "The smart card does not meet minimal requirements for support."
            Case SCARD_E_DUPLICATE_READER
                Return "The reader driver didn't produce a unique reader name."
            Case SCARD_E_INSUFFICIENT_BUFFER
                Return "The data buffer for returned data is too small for the returned data."
            Case SCARD_E_INVALID_ATR
                Return "An ATR string obtained from the registry is not a valid ATR string."
            Case SCARD_E_INVALID_HANDLE
                Return "The supplied handle was invalid."
            Case SCARD_E_INVALID_PARAMETER
                Return "One or more of the supplied parameters could not be properly interpreted."
            Case SCARD_E_INVALID_TARGET
                Return "Registry startup information is missing or invalid."
            Case SCARD_E_INVALID_VALUE
                Return "One or more of the supplied parameter values could not be properly interpreted."
            Case SCARD_E_NOT_READY
                Return "The reader or card is not ready to accept commands."
            Case SCARD_E_NOT_TRANSACTED
                Return "An attempt was made to end a non-existent transaction."
            Case SCARD_E_NO_MEMORY
                Return "Not enough memory available to complete this command."
            Case SCARD_E_NO_SERVICE
                Return "The smart card resource manager is not running."
            Case SCARD_E_NO_SMARTCARD
                Return "The operation requires a smart card, but no smart card is currently in the device."
            Case SCARD_E_PCI_TOO_SMALL
                Return "The PCI receive buffer was too small."
            Case SCARD_E_PROTO_MISMATCH
                Return "The requested protocols are incompatible with the protocol currently in use with the card."
            Case SCARD_E_READER_UNAVAILABLE
                Return "The specified reader is not currently available for use."
            Case SCARD_E_READER_UNSUPPORTED
                Return "The reader driver does not meet minimal requirements for support."
            Case SCARD_E_SERVICE_STOPPED
                Return "The smart card resource manager has shut down."
            Case SCARD_E_SHARING_VIOLATION
                Return "The smart card cannot be accessed because of other outstanding connections."
            Case SCARD_E_SYSTEM_CANCELLED
                Return "The action was canceled by the system, presumably to log off or shut down."
            Case SCARD_E_TIMEOUT
                Return "The user-specified timeout value has expired."
            Case SCARD_E_UNKNOWN_CARD
                Return "The specified smart card name is not recognized."
            Case SCARD_E_UNKNOWN_READER
                Return "The specified reader name is not recognized."
            Case SCARD_F_COMM_ERROR
                Return "An internal communications error has been detected."
            Case SCARD_F_INTERNAL_ERROR
                Return "An internal consistency check failed."
            Case SCARD_F_UNKNOWN_ERROR
                Return "An internal error has been detected, but the source is unknown."
            Case SCARD_F_WAITED_TOO_LONG
                Return "An internal consistency timer has expired."
            Case SCARD_S_SUCCESS
                Return "No error was encountered."
            Case SCARD_W_REMOVED_CARD
                Return "The smart card has been removed, so that further communication is not possible."
            Case SCARD_W_RESET_CARD
                Return "The smart card has been reset, so any shared state information is invalid."
            Case SCARD_W_UNPOWERED_CARD
                Return "Power has been removed from the smart card, so that further communication is not possible."
            Case SCARD_W_UNRESPONSIVE_CARD
                Return "The smart card is not responding to a reset."
            Case SCARD_W_UNSUPPORTED_CARD
                Return "The reader cannot communicate with the card, due to ATR string configuration conflicts."
            Case Else
                Return "?"
        End Select

    End Function
End Class
