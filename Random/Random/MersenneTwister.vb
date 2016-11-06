Option Strict On
Imports System
Imports System.Math



''' <summary>
''' A random number generator With a uniform distribution Using the Mersenne 
''' Twister algorithm.
''' </summary>


Public Class MersenneTwister
        Private Const N As Integer = 624
        Private Const M As Integer = 397
        Private Const MATRIX_A As UInteger = &H9908B0DFUI
        Private Const UPPER_MASK As UInteger = &H80000000UI
        Private Const LOWER_MASK As UInteger = &H7FFFFFFFUI

        Private mt(N - 1) As UInteger
        Private mti As Integer = N + 1

        ''' 
        ''' Create a new Mersenne Twister random number generator.
        ''' 
        Public Sub New()
            Me.New(CUInt(Date.Now.Millisecond))
        End Sub

        ''' 
        ''' Create a new Mersenne Twister random number generator with a
        ''' particular seed.
        ''' 
        ''' The seed for the generator. 
        Public Sub New(ByVal seed As UInteger)
            mt(0) = seed
            For mti = 1 To N - 1
                mt(mti) = CUInt((1812433253UL * (mt(mti - 1) Xor (mt(mti - 1) >> 30)) + CUInt(mti)) And &HFFFFFFFFUL)
            Next
        End Sub
        ''' ''' Create a new Mersenne Twister random number generator with a ''' 
        ''' particular initial key. 
        ''' ''' ''' The initial key. 
        Public Sub New(ByVal initialKey() As UInteger)
            Me.New(19650218UI)
            Dim i, j, k As Integer
            i = 1 : j = 0
            k = CInt(IIf(N > initialKey.Length, N, initialKey.Length))
            For k = k To 1 Step -1
                mt(i) = CUInt(((mt(i) Xor ((mt(i - 1) Xor (mt(i - 1) >> 30)) * 1664525UL)) + initialKey(j) + CUInt(j)) And &HFFFFFFFFUI)
                i += 1 : j += 1
                If i >= N Then mt(0) = mt(N - 1) : i = 1
                If j >= initialKey.Length Then j = 0
            Next
            For k = N - 1 To 1 Step -1
                mt(i) = CUInt(((mt(i) Xor ((mt(i - 1) Xor (mt(i - 1) >> 30)) * 1566083941UL)) - CUInt(i)) And &HFFFFFFFFUI)
                i += 1
                If i >= N Then mt(0) = mt(N - 1) : i = 1
            Next
            mt(0) = &H80000000UI
        End Sub
        ''' ''' Generates a random number between 0 and System.UInt32.MaxValue. 
        Public Function GenerateUInt32() As UInteger
            Dim y As UInteger
            Static mag01() As UInteger = {&H0UI, MATRIX_A}
            If mti >= N Then
                Dim kk As Integer
                Debug.Assert(mti <> N + 1, "Failed initialization")
                For kk = 0 To N - M - 1
                    y = (mt(kk) And UPPER_MASK) Or (mt(kk + 1) And LOWER_MASK)
                    mt(kk) = mt(kk + M) Xor (y >> 1) Xor mag01(CInt(y And &H1))
                Next
                For kk = kk To N - 2
                    y = (mt(kk) And UPPER_MASK) Or (mt(kk + 1) And LOWER_MASK)
                    mt(kk) = mt(kk + (M - N)) Xor (y >> 1) Xor mag01(CInt(y And &H1))
                Next
                y = (mt(N - 1) And UPPER_MASK) Or (mt(0) And LOWER_MASK)
                mt(N - 1) = mt(M - 1) Xor (y >> 1) Xor mag01(CInt(y And &H1))
                mti = 0
            End If
            y = mt(mti)
            mti += 1
            ' Tempering 
            y = y Xor (y >> 11)
            y = y Xor ((y << 7) And &H9D2C5680UI)
            y = y Xor ((y << 15) And &HEFC60000UI)
            y = y Xor (y >> 18)
            Return y
        End Function
        ''' ''' Generates a random integer between 0 and System.Int32.MaxValue.
        Public Function GenerateInt32() As Integer
            Return CInt(GenerateUInt32() >> 1)
        End Function
        ''' <summary>
        '''   Generates a random Integer between 0 And maxValue. 
        '''   The maximum value. Must be greater than zero. 
        ''' </summary>
        Public Function GenerateInt32(ByVal maxValue As Integer) As Integer
            Return GenerateInt32(0, maxValue)
        End Function
        ''' ''' Generates a random integer between minValue and maxValue. ''' 
        ''' ''' The lower bound. 
        ''' ''' The upper bound. 
        Public Function GenerateInt32(ByVal minValue As Integer, ByVal maxValue As Integer) As Integer
            Return CInt(Math.Floor((maxValue - minValue + 1) * GenerateDouble() + minValue))
        End Function
        ''' ''' Generates a random floating point number between 0 and 1. 
        Public Function GenerateDouble() As Double
            Return GenerateUInt32() * (1.0 / 4294967295.0)
        End Function

        Public Function GenerateDouble(ByVal MinValue As Double, ByVal MaxValue As Double) As Double
            'Return CDbl((Math.Floor(MaxValue - MinValue + 1) * GenerateDouble() + MinValue))
            Return CDbl(((MaxValue - MinValue) * GenerateDouble()) + MinValue)
        End Function
        ''' <summary>
        ''' Returns a number based on a gaussian curve
        ''' Takes the mean and the STD
        ''' </summary>
        ''' <param name="Mean"></param>
        ''' <param name="StDev"></param>
        ''' <returns></returns>
        Public Function GetGaussian(ByVal Mean As Double, ByVal StDev As Double) As Double
            Dim x1, x2, w, y1 As Double
            Static y2 As Double
            Static Use_Last As Integer = 0

            If Use_Last = 1 Then
                y1 = y2
                Use_Last = 0
            Else
                Do
                    x1 = 2.0 * GenerateDouble() - 1
                    x2 = 2.0 * GenerateDouble() - 1
                    w = x1 * x1 + x2 * x2
                Loop While w >= 1.0

                w = Sqrt((-2.0 * Log(w)) / w)
                y1 = x1 * w
                y2 = x2 * w
                Use_Last = 1

            End If

            Return (Mean + y1 * StDev)

        End Function
    End Class



