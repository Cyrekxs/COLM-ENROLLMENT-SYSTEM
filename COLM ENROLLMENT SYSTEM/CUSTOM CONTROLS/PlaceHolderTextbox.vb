
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace Demo
    ''' <summary>
    ''' Represents a Windows text box control with placeholder.
    ''' </summary>
    Public Class PlaceholderTextBox
        Inherits TextBox
#Region "Properties"

        Private _placeholderText As String = DEFAULT_PLACEHOLDER
        Private _isPlaceholderActive As Boolean


        ''' <summary>
        ''' Gets a value indicating whether the Placeholder is active.
        ''' </summary>
        <Browsable(False)> _
        Public Property IsPlaceholderActive() As Boolean
            Get
                Return _isPlaceholderActive
            End Get
            Private Set(value As Boolean)
                If value <> _isPlaceholderActive Then
                    _isPlaceholderActive = value

                    OnPlaceholderActiveChanged(value)
                End If
            End Set
        End Property


        ''' <summary>
        ''' Gets or sets the placeholder in the PlaceholderTextBox.
        ''' </summary>
        <Description("The placeholder associated with the control."), Category("Placeholder"), DefaultValue(DEFAULT_PLACEHOLDER)> _
        Public Property PlaceholderText() As String
            Get
                Return _placeholderText
            End Get
            Set(value As String)
                _placeholderText = value

                ' Only use the new value if the placeholder is active.
                If Me.IsPlaceholderActive Then
                    Me.Text = value
                End If
            End Set
        End Property


        ''' <summary>
        ''' Gets or sets the current text in the TextBox.
        ''' </summary>
        <Browsable(False)> _
        Public Overrides Property Text() As String
            Get
                ' Check 'IsPlaceholderActive' to avoid this if-clause when the text is the same as the placeholder but actually it's not the placeholder.
                ' Check 'base.Text == this.Placeholder' because in some cases IsPlaceholderActive changes too late although it isn't the placeholder anymore.
                ' If you want to get the Text Property and it still contains the placeholder, an empty string will return.
                If Me.IsPlaceholderActive AndAlso MyBase.Text = Me.PlaceholderText Then
                    Return [String].Empty
                End If

                Return MyBase.Text
            End Get
            Set(value As String)
                MyBase.Text = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the foreground color of the control.
        ''' </summary>
        Public Overrides Property ForeColor() As Color
            Get
                ' We have to differentiate whether the system is asking for the ForeColor to draw it
                ' or the developer is asking for the color.
                If Me.IsPlaceholderActive AndAlso Environment.StackTrace.Contains("System.Windows.Forms.Control.InitializeDCForWmCtlColor(IntPtr dc, Int32 msg)") Then
                    Return Color.Gray
                End If

                Return MyBase.ForeColor
            End Get
            Set(value As Color)
                MyBase.ForeColor = value
            End Set
        End Property


        ''' <summary>
        ''' Occurs when the value of the IsPlaceholderActive property has changed.
        ''' </summary>
        <Description("Occurs when the value of the IsPlaceholderInside property has changed.")> _
        Public Event PlaceholderActiveChanged As EventHandler(Of PlaceholderActiveChangedEventArgs)

#End Region


#Region "Global Variables"

        ''' <summary>
        ''' Specifies the default placeholder text.
        ''' </summary>
        Const DEFAULT_PLACEHOLDER As String = "<Input>"

        ''' <summary>
        ''' Flag to avoid the TextChanged Event. Don't access directly, use Method 'ActionWithoutTextChanged(Action act)' instead.
        ''' </summary>
        Private avoidTextChanged As Boolean

#End Region


#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the PlaceholderTextBox class.
        ''' </summary>
        Public Sub New()
            ' Through this line the default placeholder gets displayed in designer
            MyBase.Text = Me.PlaceholderText

            SubscribeEvents()

            ' Set Default
            Me.IsPlaceholderActive = True
        End Sub

#End Region


#Region "Functions"

        ''' <summary>
        ''' Inserts placeholder, assigns placeholder style and sets cursor to first position.
        ''' </summary>
        Public Sub Reset()
            Me.IsPlaceholderActive = True

            ActionWithoutTextChanged(Function() InlineAssignHelper(Me.Text, Me.PlaceholderText))
            Me.[Select](0, 0)
        End Sub

        ''' <summary>
        ''' Run an action with avoiding the TextChanged event.
        ''' </summary>
        ''' <param name="act">Specifies the action to run.</param>
        Private Sub ActionWithoutTextChanged(act As Action)
            avoidTextChanged = True

            act.Invoke()

            avoidTextChanged = False
        End Sub

        ''' <summary>
        ''' Subscribe necessary Events.
        ''' </summary>
        Private Sub SubscribeEvents()
            AddHandler Me.TextChanged, AddressOf PlaceholderTextBox_TextChanged
        End Sub

#End Region


#Region "Events"

        Private Sub PlaceholderTextBox_TextChanged(sender As Object, e As EventArgs)
            ' Check flag
            If avoidTextChanged Then
                Return
            End If

            ' Run code with avoiding recursive call
            ActionWithoutTextChanged(Sub()
                                         ' If the Text is empty, insert placeholder and set cursor to to first position
                                         If [String].IsNullOrEmpty(Me.Text) Then
                                             Reset()
                                             Return
                                         End If

                                         ' If the placeholder is active, revert state to a usual TextBox
                                         If Me.IsPlaceholderActive Then
                                             Me.IsPlaceholderActive = False

                                             ' Throw away the placeholder but leave the new typed char
                                             Me.Text = Me.Text.Replace(Me.PlaceholderText, [String].Empty)

                                             ' Set Selection to last position
                                             Me.[Select](Me.TextLength, 0)
                                         End If

                                     End Sub)

            Me.Font = Me.Font
        End Sub

        Protected Overrides Sub OnGotFocus(e As EventArgs)
            ' Without this line it would highlight the placeholder when getting focus
            Me.[Select](0, 0)
            MyBase.OnGotFocus(e)
        End Sub

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            ' When you click on the placerholderTextBox and the placerholder is active, jump to first position
            If Me.IsPlaceholderActive Then
                Reset()
            End If

            MyBase.OnMouseDown(e)
        End Sub

        Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
            ' Prevents that the user can go through the placeholder with arrow keys
            If IsPlaceholderActive AndAlso (e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse e.KeyCode = Keys.Up OrElse e.KeyCode = Keys.Down) Then
                e.Handled = True
            End If

            MyBase.OnKeyDown(e)
        End Sub

        Protected Overridable Sub OnPlaceholderActiveChanged(newValue As Boolean)
            RaiseEvent PlaceholderActiveChanged(Me, New PlaceholderActiveChangedEventArgs(newValue))
        End Sub
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function

#End Region
    End Class

    ''' <summary>
    ''' Provides data for the PlaceholderActiveChanged event.
    ''' </summary>
    Public Class PlaceholderActiveChangedEventArgs
        Inherits EventArgs
        ''' <summary>
        ''' Initializes a new instance of the PlaceholderInsideChangedEventArgs class.
        ''' </summary>
        ''' <param name="newValue">The new value of the IsPlaceholderInside Property.</param>
        Public Sub New(newValue As Boolean)
            Me.NewValue = newValue
        End Sub

        ''' <summary>
        ''' Gets the new value of the IsPlaceholderActive property.
        ''' </summary>
        Public Property NewValue() As Boolean
            Get
                Return m_NewValue
            End Get
            Private Set(value As Boolean)
                m_NewValue = Value
            End Set
        End Property
        Private m_NewValue As Boolean
    End Class
End Namespace

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================
