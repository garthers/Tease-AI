Imports Tai.Common
Imports System.Collections.Specialized
Imports System.Drawing

Public Class MySettingsWrapper
    Implements ISettings
    Private ReadOnly _settings As My.MySettings

    Public Sub New()
        _settings = My.Settings
    End Sub
    Public Property VideoHardcore As String Implements ISettings.VideoHardcore
        Get
            Return _settings.VideoHardcore
        End Get
        Set
            _settings.VideoHardcore = Value
        End Set
    End Property
    Public Property VideoSoftcore As String Implements ISettings.VideoSoftcore
        Get
            Return _settings.VideoSoftcore
        End Get
        Set
            _settings.VideoSoftcore = Value
        End Set
    End Property
    Public Property VideoLesbian As String Implements ISettings.VideoLesbian
        Get
            Return _settings.VideoLesbian
        End Get
        Set
            _settings.VideoLesbian = Value
        End Set
    End Property
    Public Property VideoBlowjob As String Implements ISettings.VideoBlowjob
        Get
            Return _settings.VideoBlowjob
        End Get
        Set
            _settings.VideoBlowjob = Value
        End Set
    End Property
    Public Property VideoFemdom As String Implements ISettings.VideoFemdom
        Get
            Return _settings.VideoFemdom
        End Get
        Set
            _settings.VideoFemdom = Value
        End Set
    End Property
    Public Property VideoFemsub As String Implements ISettings.VideoFemsub
        Get
            Return _settings.VideoFemsub
        End Get
        Set
            _settings.VideoFemsub = Value
        End Set
    End Property
    Public Property VideoJOI As String Implements ISettings.VideoJOI
        Get
            Return _settings.VideoJOI
        End Get
        Set
            _settings.VideoJOI = Value
        End Set
    End Property
    Public Property VideoCH As String Implements ISettings.VideoCH
        Get
            Return _settings.VideoCH
        End Get
        Set
            _settings.VideoCH = Value
        End Set
    End Property
    Public Property VideoGeneral As String Implements ISettings.VideoGeneral
        Get
            Return _settings.VideoGeneral
        End Get
        Set
            _settings.VideoGeneral = Value
        End Set
    End Property
    Public Property CBHardcore As Boolean Implements ISettings.CBHardcore
        Get
            Return _settings.CBHardcore
        End Get
        Set
            _settings.CBHardcore = Value
        End Set
    End Property
    Public Property CBSoftcore As Boolean Implements ISettings.CBSoftcore
        Get
            Return _settings.CBSoftcore
        End Get
        Set
            _settings.CBSoftcore = Value
        End Set
    End Property
    Public Property CBLesbian As Boolean Implements ISettings.CBLesbian
        Get
            Return _settings.CBLesbian
        End Get
        Set
            _settings.CBLesbian = Value
        End Set
    End Property
    Public Property CBBlowjob As Boolean Implements ISettings.CBBlowjob
        Get
            Return _settings.CBBlowjob
        End Get
        Set
            _settings.CBBlowjob = Value
        End Set
    End Property
    Public Property CBFemdom As Boolean Implements ISettings.CBFemdom
        Get
            Return _settings.CBFemdom
        End Get
        Set
            _settings.CBFemdom = Value
        End Set
    End Property
    Public Property CBFemsub As Boolean Implements ISettings.CBFemsub
        Get
            Return _settings.CBFemsub
        End Get
        Set
            _settings.CBFemsub = Value
        End Set
    End Property
    Public Property CBJOI As Boolean Implements ISettings.CBJOI
        Get
            Return _settings.CBJOI
        End Get
        Set
            _settings.CBJOI = Value
        End Set
    End Property
    Public Property CBCH As Boolean Implements ISettings.CBCH
        Get
            Return _settings.CBCH
        End Get
        Set
            _settings.CBCH = Value
        End Set
    End Property
    Public Property CBGeneral As Boolean Implements ISettings.CBGeneral
        Get
            Return _settings.CBGeneral
        End Get
        Set
            _settings.CBGeneral = Value
        End Set
    End Property
    Public Property DomAvatarSave As String Implements ISettings.DomAvatarSave
        Get
            Return _settings.DomAvatarSave
        End Get
        Set
            _settings.DomAvatarSave = Value
        End Set
    End Property
    Public Property SubAvatarSave As String Implements ISettings.SubAvatarSave
        Get
            Return _settings.SubAvatarSave
        End Get
        Set
            _settings.SubAvatarSave = Value
        End Set
    End Property
    Public Property NBCensorShowMin As Decimal Implements ISettings.NBCensorShowMin
        Get
            Return _settings.NBCensorShowMin
        End Get
        Set
            _settings.NBCensorShowMin = Value
        End Set
    End Property
    Public Property NBCensorShowMax As Decimal Implements ISettings.NBCensorShowMax
        Get
            Return _settings.NBCensorShowMax
        End Get
        Set
            _settings.NBCensorShowMax = Value
        End Set
    End Property
    Public Property NBCensorHideMin As Decimal Implements ISettings.NBCensorHideMin
        Get
            Return _settings.NBCensorHideMin
        End Get
        Set
            _settings.NBCensorHideMin = Value
        End Set
    End Property
    Public Property NBCensorHideMax As Decimal Implements ISettings.NBCensorHideMax
        Get
            Return _settings.NBCensorHideMax
        End Get
        Set
            _settings.NBCensorHideMax = Value
        End Set
    End Property
    Public Property CBCensorConstant As Boolean Implements ISettings.CBCensorConstant
        Get
            Return _settings.CBCensorConstant
        End Get
        Set
            _settings.CBCensorConstant = Value
        End Set
    End Property
    Public Property CBHardcoreD As Boolean Implements ISettings.CBHardcoreD
        Get
            Return _settings.CBHardcoreD
        End Get
        Set
            _settings.CBHardcoreD = Value
        End Set
    End Property
    Public Property CBSoftcoreD As Boolean Implements ISettings.CBSoftcoreD
        Get
            Return _settings.CBSoftcoreD
        End Get
        Set
            _settings.CBSoftcoreD = Value
        End Set
    End Property
    Public Property CBLesbianD As Boolean Implements ISettings.CBLesbianD
        Get
            Return _settings.CBLesbianD
        End Get
        Set
            _settings.CBLesbianD = Value
        End Set
    End Property
    Public Property CBBlowjobD As Boolean Implements ISettings.CBBlowjobD
        Get
            Return _settings.CBBlowjobD
        End Get
        Set
            _settings.CBBlowjobD = Value
        End Set
    End Property
    Public Property CBFemsubD As Boolean Implements ISettings.CBFemsubD
        Get
            Return _settings.CBFemsubD
        End Get
        Set
            _settings.CBFemsubD = Value
        End Set
    End Property
    Public Property CBJOID As Boolean Implements ISettings.CBJOID
        Get
            Return _settings.CBJOID
        End Get
        Set
            _settings.CBJOID = Value
        End Set
    End Property
    Public Property CBCHD As Boolean Implements ISettings.CBCHD
        Get
            Return _settings.CBCHD
        End Get
        Set
            _settings.CBCHD = Value
        End Set
    End Property
    Public Property CBGeneralD As Boolean Implements ISettings.CBGeneralD
        Get
            Return _settings.CBGeneralD
        End Get
        Set
            _settings.CBGeneralD = Value
        End Set
    End Property
    Public Property CBFemdomD As Boolean Implements ISettings.CBFemdomD
        Get
            Return _settings.CBFemdomD
        End Get
        Set
            _settings.CBFemdomD = Value
        End Set
    End Property
    Public Property VideoHardcoreD As String Implements ISettings.VideoHardcoreD
        Get
            Return _settings.VideoHardcoreD
        End Get
        Set
            _settings.VideoHardcoreD = Value
        End Set
    End Property
    Public Property VideoSoftcoreD As String Implements ISettings.VideoSoftcoreD
        Get
            Return _settings.VideoSoftcoreD
        End Get
        Set
            _settings.VideoSoftcoreD = Value
        End Set
    End Property
    Public Property VideoLesbianD As String Implements ISettings.VideoLesbianD
        Get
            Return _settings.VideoLesbianD
        End Get
        Set
            _settings.VideoLesbianD = Value
        End Set
    End Property
    Public Property VideoBlowjobD As String Implements ISettings.VideoBlowjobD
        Get
            Return _settings.VideoBlowjobD
        End Get
        Set
            _settings.VideoBlowjobD = Value
        End Set
    End Property
    Public Property VideoFemdomD As String Implements ISettings.VideoFemdomD
        Get
            Return _settings.VideoFemdomD
        End Get
        Set
            _settings.VideoFemdomD = Value
        End Set
    End Property
    Public Property VideoFemsubD As String Implements ISettings.VideoFemsubD
        Get
            Return _settings.VideoFemsubD
        End Get
        Set
            _settings.VideoFemsubD = Value
        End Set
    End Property
    Public Property VideoJOID As String Implements ISettings.VideoJOID
        Get
            Return _settings.VideoJOID
        End Get
        Set
            _settings.VideoJOID = Value
        End Set
    End Property
    Public Property VideoCHD As String Implements ISettings.VideoCHD
        Get
            Return _settings.VideoCHD
        End Get
        Set
            _settings.VideoCHD = Value
        End Set
    End Property
    Public Property VideoGeneralD As String Implements ISettings.VideoGeneralD
        Get
            Return _settings.VideoGeneralD
        End Get
        Set
            _settings.VideoGeneralD = Value
        End Set
    End Property
    Public Property GlitterAV As String Implements ISettings.GlitterAV
        Get
            Return _settings.GlitterAV
        End Get
        Set
            _settings.GlitterAV = Value
        End Set
    End Property
    Public Property GlitterAV1 As String Implements ISettings.GlitterAV1
        Get
            Return _settings.GlitterAV1
        End Get
        Set
            _settings.GlitterAV1 = Value
        End Set
    End Property
    Public Property GlitterAV2 As String Implements ISettings.GlitterAV2
        Get
            Return _settings.GlitterAV2
        End Get
        Set
            _settings.GlitterAV2 = Value
        End Set
    End Property
    Public Property GlitterAV3 As String Implements ISettings.GlitterAV3
        Get
            Return _settings.GlitterAV3
        End Get
        Set
            _settings.GlitterAV3 = Value
        End Set
    End Property
    Public Property GlitterSN As String Implements ISettings.GlitterSN
        Get
            Return _settings.GlitterSN
        End Get
        Set
            _settings.GlitterSN = Value
        End Set
    End Property
    Public Property Glitter1 As String Implements ISettings.Glitter1
        Get
            Return _settings.Glitter1
        End Get
        Set
            _settings.Glitter1 = Value
        End Set
    End Property
    Public Property Glitter2 As String Implements ISettings.Glitter2
        Get
            Return _settings.Glitter2
        End Get
        Set
            _settings.Glitter2 = Value
        End Set
    End Property
    Public Property Glitter3 As String Implements ISettings.Glitter3
        Get
            Return _settings.Glitter3
        End Get
        Set
            _settings.Glitter3 = Value
        End Set
    End Property
    Public Property GlitterNCDommeColor As Color Implements ISettings.GlitterNCDommeColor
        Get
            Return _settings.GlitterNCDommeColor
        End Get
        Set
            _settings.GlitterNCDommeColor = Value
        End Set
    End Property
    Public Property GlitterNC1Color As Color Implements ISettings.GlitterNC1Color
        Get
            Return _settings.GlitterNC1Color
        End Get
        Set
            _settings.GlitterNC1Color = Value
        End Set
    End Property
    Public Property GlitterNC2Color As Color Implements ISettings.GlitterNC2Color
        Get
            Return _settings.GlitterNC2Color
        End Get
        Set
            _settings.GlitterNC2Color = Value
        End Set
    End Property
    Public Property GlitterNC3Color As Color Implements ISettings.GlitterNC3Color
        Get
            Return _settings.GlitterNC3Color
        End Get
        Set
            _settings.GlitterNC3Color = Value
        End Set
    End Property
    Public Property GlitterDSlider As Integer Implements ISettings.GlitterDSlider
        Get
            Return _settings.GlitterDSlider
        End Get
        Set
            _settings.GlitterDSlider = Value
        End Set
    End Property
    Public Property Glitter1Slider As Integer Implements ISettings.Glitter1Slider
        Get
            Return _settings.Glitter1Slider
        End Get
        Set
            _settings.Glitter1Slider = Value
        End Set
    End Property
    Public Property Glitter2Slider As Integer Implements ISettings.Glitter2Slider
        Get
            Return _settings.Glitter2Slider
        End Get
        Set
            _settings.Glitter2Slider = Value
        End Set
    End Property
    Public Property Glitter3Slider As Integer Implements ISettings.Glitter3Slider
        Get
            Return _settings.Glitter3Slider
        End Get
        Set
            _settings.Glitter3Slider = Value
        End Set
    End Property
    Public Property CBGlitterFeed As Boolean Implements ISettings.CBGlitterFeed
        Get
            Return _settings.CBGlitterFeed
        End Get
        Set
            _settings.CBGlitterFeed = Value
        End Set
    End Property
    Public Property CBGlitter1 As Boolean Implements ISettings.CBGlitter1
        Get
            Return _settings.CBGlitter1
        End Get
        Set
            _settings.CBGlitter1 = Value
        End Set
    End Property
    Public Property CBGlitter2 As Boolean Implements ISettings.CBGlitter2
        Get
            Return _settings.CBGlitter2
        End Get
        Set
            _settings.CBGlitter2 = Value
        End Set
    End Property
    Public Property CBGlitter3 As Boolean Implements ISettings.CBGlitter3
        Get
            Return _settings.CBGlitter3
        End Get
        Set
            _settings.CBGlitter3 = Value
        End Set
    End Property
    Public Property CBTease As Boolean Implements ISettings.CBTease
        Get
            Return _settings.CBTease
        End Get
        Set
            _settings.CBTease = Value
        End Set
    End Property
    Public Property CBEgotist As Boolean Implements ISettings.CBEgotist
        Get
            Return _settings.CBEgotist
        End Get
        Set
            _settings.CBEgotist = Value
        End Set
    End Property
    Public Property CBTrivia As Boolean Implements ISettings.CBTrivia
        Get
            Return _settings.CBTrivia
        End Get
        Set
            _settings.CBTrivia = Value
        End Set
    End Property
    Public Property CBDaily As Boolean Implements ISettings.CBDaily
        Get
            Return _settings.CBDaily
        End Get
        Set
            _settings.CBDaily = Value
        End Set
    End Property
    Public Property CBCustom1 As Boolean Implements ISettings.CBCustom1
        Get
            Return _settings.CBCustom1
        End Get
        Set
            _settings.CBCustom1 = Value
        End Set
    End Property
    Public Property CBCustom2 As Boolean Implements ISettings.CBCustom2
        Get
            Return _settings.CBCustom2
        End Get
        Set
            _settings.CBCustom2 = Value
        End Set
    End Property
    Public Property CB1Bratty As Boolean Implements ISettings.CB1Bratty
        Get
            Return _settings.CB1Bratty
        End Get
        Set
            _settings.CB1Bratty = Value
        End Set
    End Property
    Public Property CB1Cruel As Boolean Implements ISettings.CB1Cruel
        Get
            Return _settings.CB1Cruel
        End Get
        Set
            _settings.CB1Cruel = Value
        End Set
    End Property
    Public Property CB1Caring As Boolean Implements ISettings.CB1Caring
        Get
            Return _settings.CB1Caring
        End Get
        Set
            _settings.CB1Caring = Value
        End Set
    End Property
    Public Property CB1Angry As Boolean Implements ISettings.CB1Angry
        Get
            Return _settings.CB1Angry
        End Get
        Set
            _settings.CB1Angry = Value
        End Set
    End Property
    Public Property CB1Custom1 As Boolean Implements ISettings.CB1Custom1
        Get
            Return _settings.CB1Custom1
        End Get
        Set
            _settings.CB1Custom1 = Value
        End Set
    End Property
    Public Property CB1Custom2 As Boolean Implements ISettings.CB1Custom2
        Get
            Return _settings.CB1Custom2
        End Get
        Set
            _settings.CB1Custom2 = Value
        End Set
    End Property
    Public Property CB2Bratty As Boolean Implements ISettings.CB2Bratty
        Get
            Return _settings.CB2Bratty
        End Get
        Set
            _settings.CB2Bratty = Value
        End Set
    End Property
    Public Property CB2Cruel As Boolean Implements ISettings.CB2Cruel
        Get
            Return _settings.CB2Cruel
        End Get
        Set
            _settings.CB2Cruel = Value
        End Set
    End Property
    Public Property CB2Caring As Boolean Implements ISettings.CB2Caring
        Get
            Return _settings.CB2Caring
        End Get
        Set
            _settings.CB2Caring = Value
        End Set
    End Property
    Public Property CB2Angry As Boolean Implements ISettings.CB2Angry
        Get
            Return _settings.CB2Angry
        End Get
        Set
            _settings.CB2Angry = Value
        End Set
    End Property
    Public Property CB2Custom1 As Boolean Implements ISettings.CB2Custom1
        Get
            Return _settings.CB2Custom1
        End Get
        Set
            _settings.CB2Custom1 = Value
        End Set
    End Property
    Public Property CB2Custom2 As Boolean Implements ISettings.CB2Custom2
        Get
            Return _settings.CB2Custom2
        End Get
        Set
            _settings.CB2Custom2 = Value
        End Set
    End Property
    Public Property CB3Bratty As Boolean Implements ISettings.CB3Bratty
        Get
            Return _settings.CB3Bratty
        End Get
        Set
            _settings.CB3Bratty = Value
        End Set
    End Property
    Public Property CB3Cruel As Boolean Implements ISettings.CB3Cruel
        Get
            Return _settings.CB3Cruel
        End Get
        Set
            _settings.CB3Cruel = Value
        End Set
    End Property
    Public Property CB3Caring As Boolean Implements ISettings.CB3Caring
        Get
            Return _settings.CB3Caring
        End Get
        Set
            _settings.CB3Caring = Value
        End Set
    End Property
    Public Property CB3Angry As Boolean Implements ISettings.CB3Angry
        Get
            Return _settings.CB3Angry
        End Get
        Set
            _settings.CB3Angry = Value
        End Set
    End Property
    Public Property CB3Custom1 As Boolean Implements ISettings.CB3Custom1
        Get
            Return _settings.CB3Custom1
        End Get
        Set
            _settings.CB3Custom1 = Value
        End Set
    End Property
    Public Property CB3Custom2 As Boolean Implements ISettings.CB3Custom2
        Get
            Return _settings.CB3Custom2
        End Get
        Set
            _settings.CB3Custom2 = Value
        End Set
    End Property
    Public Property DomName As String Implements ISettings.DomName
        Get
            Return _settings.DomName
        End Get
        Set
            _settings.DomName = Value
        End Set
    End Property
    Public Property SubName As String Implements ISettings.SubName
        Get
            Return _settings.SubName
        End Get
        Set
            _settings.SubName = Value
        End Set
    End Property
    Public Property pnSetting1 As String Implements ISettings.pnSetting1
        Get
            Return _settings.pnSetting1
        End Get
        Set
            _settings.pnSetting1 = Value
        End Set
    End Property
    Public Property pnSetting2 As String Implements ISettings.pnSetting2
        Get
            Return _settings.pnSetting2
        End Get
        Set
            _settings.pnSetting2 = Value
        End Set
    End Property
    Public Property pnSetting3 As String Implements ISettings.pnSetting3
        Get
            Return _settings.pnSetting3
        End Get
        Set
            _settings.pnSetting3 = Value
        End Set
    End Property
    Public Property pnSetting4 As String Implements ISettings.pnSetting4
        Get
            Return _settings.pnSetting4
        End Get
        Set
            _settings.pnSetting4 = Value
        End Set
    End Property
    Public Property pnSetting5 As String Implements ISettings.pnSetting5
        Get
            Return _settings.pnSetting5
        End Get
        Set
            _settings.pnSetting5 = Value
        End Set
    End Property
    Public Property pnSetting6 As String Implements ISettings.pnSetting6
        Get
            Return _settings.pnSetting6
        End Get
        Set
            _settings.pnSetting6 = Value
        End Set
    End Property
    Public Property pnSetting7 As String Implements ISettings.pnSetting7
        Get
            Return _settings.pnSetting7
        End Get
        Set
            _settings.pnSetting7 = Value
        End Set
    End Property
    Public Property pnSetting8 As String Implements ISettings.pnSetting8
        Get
            Return _settings.pnSetting8
        End Get
        Set
            _settings.pnSetting8 = Value
        End Set
    End Property
    Public Property DomColor As String Implements ISettings.DomColor
        Get
            Return _settings.DomColor
        End Get
        Set
            _settings.DomColor = Value
        End Set
    End Property
    Public Property SubColor As String Implements ISettings.SubColor
        Get
            Return _settings.SubColor
        End Get
        Set
            _settings.SubColor = Value
        End Set
    End Property
    Public Property DomColorColor As Color Implements ISettings.DomColorColor
        Get
            Return _settings.DomColorColor
        End Get
        Set
            _settings.DomColorColor = Value
        End Set
    End Property
    Public Property SubColorColor As Color Implements ISettings.SubColorColor
        Get
            Return _settings.SubColorColor
        End Get
        Set
            _settings.SubColorColor = Value
        End Set
    End Property
    Public Property CBTimeStamps As Boolean Implements ISettings.CBTimeStamps
        Get
            Return _settings.CBTimeStamps
        End Get
        Set
            _settings.CBTimeStamps = Value
        End Set
    End Property
    Public Property CBShowNames As Boolean Implements ISettings.CBShowNames
        Get
            Return _settings.CBShowNames
        End Get
        Set
            _settings.CBShowNames = Value
        End Set
    End Property
    Public Property CBInstantType As Boolean Implements ISettings.CBInstantType
        Get
            Return _settings.CBInstantType
        End Get
        Set
            _settings.CBInstantType = Value
        End Set
    End Property
    Public Property CBBlogImageMain As Boolean Implements ISettings.CBBlogImageMain
        Get
            Return _settings.CBBlogImageMain
        End Get
        Set
            _settings.CBBlogImageMain = Value
        End Set
    End Property
    Public Property CBStretchLandscape As Boolean Implements ISettings.CBStretchLandscape
        Get
            Return _settings.CBStretchLandscape
        End Get
        Set
            _settings.CBStretchLandscape = Value
        End Set
    End Property
    Public Property CBMetronome As Boolean Implements ISettings.CBMetronome
        Get
            Return _settings.CBMetronome
        End Get
        Set
            _settings.CBMetronome = Value
        End Set
    End Property
    Public Property CBSettingsPause As Boolean Implements ISettings.CBSettingsPause
        Get
            Return _settings.CBSettingsPause
        End Get
        Set
            _settings.CBSettingsPause = Value
        End Set
    End Property
    Public Property DomLevel As Integer Implements ISettings.DomLevel
        Get
            Return _settings.DomLevel
        End Get
        Set
            _settings.DomLevel = Value
        End Set
    End Property
    Public Property OrgasmAllow As String Implements ISettings.OrgasmAllow
        Get
            Return _settings.OrgasmAllow
        End Get
        Set
            _settings.OrgasmAllow = Value
        End Set
    End Property
    Public Property OrgasmRuin As String Implements ISettings.OrgasmRuin
        Get
            Return _settings.OrgasmRuin
        End Get
        Set
            _settings.OrgasmRuin = Value
        End Set
    End Property
    Public Property CBAutosaveChatlog As Boolean Implements ISettings.CBAutosaveChatlog
        Get
            Return _settings.CBAutosaveChatlog
        End Get
        Set
            _settings.CBAutosaveChatlog = Value
        End Set
    End Property
    Public Property CBExitSaveChatlog As Boolean Implements ISettings.CBExitSaveChatlog
        Get
            Return _settings.CBExitSaveChatlog
        End Get
        Set
            _settings.CBExitSaveChatlog = Value
        End Set
    End Property
    Public Property AvgEdgeStroking As Integer Implements ISettings.AvgEdgeStroking
        Get
            Return _settings.AvgEdgeStroking
        End Get
        Set
            _settings.AvgEdgeStroking = Value
        End Set
    End Property
    Public Property AvgEdgeNoTouch As Integer Implements ISettings.AvgEdgeNoTouch
        Get
            Return _settings.AvgEdgeNoTouch
        End Get
        Set
            _settings.AvgEdgeNoTouch = Value
        End Set
    End Property
    Public Property AvgEdgeCount As Integer Implements ISettings.AvgEdgeCount
        Get
            Return _settings.AvgEdgeCount
        End Get
        Set
            _settings.AvgEdgeCount = Value
        End Set
    End Property
    Public Property StrokeTimeTotal As Integer Implements ISettings.StrokeTimeTotal
        Get
            Return _settings.StrokeTimeTotal
        End Get
        Set
            _settings.StrokeTimeTotal = Value
        End Set
    End Property
    Public Property NBWritingTaskMin As Integer Implements ISettings.NBWritingTaskMin
        Get
            Return _settings.NBWritingTaskMin
        End Get
        Set
            _settings.NBWritingTaskMin = Value
        End Set
    End Property
    Public Property NBWritingTaskMax As Integer Implements ISettings.NBWritingTaskMax
        Get
            Return _settings.NBWritingTaskMax
        End Get
        Set
            _settings.NBWritingTaskMax = Value
        End Set
    End Property
    Public Property LBLBoobPath As String Implements ISettings.LBLBoobPath
        Get
            Return _settings.LBLBoobPath
        End Get
        Set
            _settings.LBLBoobPath = Value
        End Set
    End Property
    Public Property UrlFileBoobs As String Implements ISettings.UrlFileBoobs
        Get
            Return _settings.UrlFileBoobs
        End Get
        Set
            _settings.UrlFileBoobs = Value
        End Set
    End Property
    Public Property LBLButtPath As String Implements ISettings.LBLButtPath
        Get
            Return _settings.LBLButtPath
        End Get
        Set
            _settings.LBLButtPath = Value
        End Set
    End Property
    Public Property UrlFileButt As String Implements ISettings.UrlFileButt
        Get
            Return _settings.UrlFileButt
        End Get
        Set
            _settings.UrlFileButt = Value
        End Set
    End Property
    Public Property CBBnBLocal As Boolean Implements ISettings.CBBnBLocal
        Get
            Return _settings.CBBnBLocal
        End Get
        Set
            _settings.CBBnBLocal = Value
        End Set
    End Property
    Public Property CBBnBURL As Boolean Implements ISettings.CBBnBURL
        Get
            Return _settings.CBBnBURL
        End Get
        Set
            _settings.CBBnBURL = Value
        End Set
    End Property
    Public Property CBBoobSubDir As Boolean Implements ISettings.CBBoobSubDir
        Get
            Return _settings.CBBoobSubDir
        End Get
        Set
            _settings.CBBoobSubDir = Value
        End Set
    End Property
    Public Property CBButtSubDir As Boolean Implements ISettings.CBButtSubDir
        Get
            Return _settings.CBButtSubDir
        End Get
        Set
            _settings.CBButtSubDir = Value
        End Set
    End Property
    Public Property CBEnableBnB As Boolean Implements ISettings.CBEnableBnB
        Get
            Return _settings.CBEnableBnB
        End Get
        Set
            _settings.CBEnableBnB = Value
        End Set
    End Property
    Public Property CBSlideshowSubDir As Boolean Implements ISettings.CBSlideshowSubDir
        Get
            Return _settings.CBSlideshowSubDir
        End Get
        Set
            _settings.CBSlideshowSubDir = Value
        End Set
    End Property
    Public Property CBSlideshowRandom As Boolean Implements ISettings.CBSlideshowRandom
        Get
            Return _settings.CBSlideshowRandom
        End Get
        Set
            _settings.CBSlideshowRandom = Value
        End Set
    End Property
    Public Property DomAge As Integer Implements ISettings.DomAge
        Get
            Return _settings.DomAge
        End Get
        Set
            _settings.DomAge = Value
        End Set
    End Property
    Public Property DomHair As String Implements ISettings.DomHair
        Get
            Return _settings.DomHair
        End Get
        Set
            _settings.DomHair = Value
        End Set
    End Property
    Public Property DomEyes As String Implements ISettings.DomEyes
        Get
            Return _settings.DomEyes
        End Get
        Set
            _settings.DomEyes = Value
        End Set
    End Property
    Public Property DomCup As String Implements ISettings.DomCup
        Get
            Return _settings.DomCup
        End Get
        Set
            _settings.DomCup = Value
        End Set
    End Property
    Public Property DomPersonality As String Implements ISettings.DomPersonality
        Get
            Return _settings.DomPersonality
        End Get
        Set
            _settings.DomPersonality = Value
        End Set
    End Property
    Public Property DomCrazy As Boolean Implements ISettings.DomCrazy
        Get
            Return _settings.DomCrazy
        End Get
        Set
            _settings.DomCrazy = Value
        End Set
    End Property
    Public Property DomVulgar As Boolean Implements ISettings.DomVulgar
        Get
            Return _settings.DomVulgar
        End Get
        Set
            _settings.DomVulgar = Value
        End Set
    End Property
    Public Property DomSupremacist As Boolean Implements ISettings.DomSupremacist
        Get
            Return _settings.DomSupremacist
        End Get
        Set
            _settings.DomSupremacist = Value
        End Set
    End Property
    Public Property DomLowercase As Boolean Implements ISettings.DomLowercase
        Get
            Return _settings.DomLowercase
        End Get
        Set
            _settings.DomLowercase = Value
        End Set
    End Property
    Public Property DomNoApostrophes As Boolean Implements ISettings.DomNoApostrophes
        Get
            Return _settings.DomNoApostrophes
        End Get
        Set
            _settings.DomNoApostrophes = Value
        End Set
    End Property
    Public Property DomNoCommas As Boolean Implements ISettings.DomNoCommas
        Get
            Return _settings.DomNoCommas
        End Get
        Set
            _settings.DomNoCommas = Value
        End Set
    End Property
    Public Property DomNoPeriods As Boolean Implements ISettings.DomNoPeriods
        Get
            Return _settings.DomNoPeriods
        End Get
        Set
            _settings.DomNoPeriods = Value
        End Set
    End Property
    Public Property DomMeMyMine As Boolean Implements ISettings.DomMeMyMine
        Get
            Return _settings.DomMeMyMine
        End Get
        Set
            _settings.DomMeMyMine = Value
        End Set
    End Property
    Public Property DomEmotes As String Implements ISettings.DomEmotes
        Get
            Return _settings.DomEmotes
        End Get
        Set
            _settings.DomEmotes = Value
        End Set
    End Property
    Public Property DomDenialEnd As Boolean Implements ISettings.DomDenialEnd
        Get
            Return _settings.DomDenialEnd
        End Get
        Set
            _settings.DomDenialEnd = Value
        End Set
    End Property
    Public Property DomOrgasmEnd As Boolean Implements ISettings.DomOrgasmEnd
        Get
            Return _settings.DomOrgasmEnd
        End Get
        Set
            _settings.DomOrgasmEnd = Value
        End Set
    End Property
    Public Property DomPOT As Boolean Implements ISettings.DomPOT
        Get
            Return _settings.DomPOT
        End Get
        Set
            _settings.DomPOT = Value
        End Set
    End Property
    Public Property DomLimit As Boolean Implements ISettings.DomLimit
        Get
            Return _settings.DomLimit
        End Get
        Set
            _settings.DomLimit = Value
        End Set
    End Property
    Public Property DomOrgasmPer As Integer Implements ISettings.DomOrgasmPer
        Get
            Return _settings.DomOrgasmPer
        End Get
        Set
            _settings.DomOrgasmPer = Value
        End Set
    End Property
    Public Property DomPerMonth As String Implements ISettings.DomPerMonth
        Get
            Return _settings.DomPerMonth
        End Get
        Set
            _settings.DomPerMonth = Value
        End Set
    End Property
    Public Property DomLock As Boolean Implements ISettings.DomLock
        Get
            Return _settings.DomLock
        End Get
        Set
            _settings.DomLock = Value
        End Set
    End Property
    Public Property DomMoodMin As Integer Implements ISettings.DomMoodMin
        Get
            Return _settings.DomMoodMin
        End Get
        Set
            _settings.DomMoodMin = Value
        End Set
    End Property
    Public Property DomMoodMax As Integer Implements ISettings.DomMoodMax
        Get
            Return _settings.DomMoodMax
        End Get
        Set
            _settings.DomMoodMax = Value
        End Set
    End Property
    Public Property AvgCockMin As Integer Implements ISettings.AvgCockMin
        Get
            Return _settings.AvgCockMin
        End Get
        Set
            _settings.AvgCockMin = Value
        End Set
    End Property
    Public Property AvgCockMax As Integer Implements ISettings.AvgCockMax
        Get
            Return _settings.AvgCockMax
        End Get
        Set
            _settings.AvgCockMax = Value
        End Set
    End Property
    Public Property SelfAgeMin As Integer Implements ISettings.SelfAgeMin
        Get
            Return _settings.SelfAgeMin
        End Get
        Set
            _settings.SelfAgeMin = Value
        End Set
    End Property
    Public Property SelfAgeMax As Integer Implements ISettings.SelfAgeMax
        Get
            Return _settings.SelfAgeMax
        End Get
        Set
            _settings.SelfAgeMax = Value
        End Set
    End Property
    Public Property SubAgeMin As Integer Implements ISettings.SubAgeMin
        Get
            Return _settings.SubAgeMin
        End Get
        Set
            _settings.SubAgeMin = Value
        End Set
    End Property
    Public Property SubAgeMax As String Implements ISettings.SubAgeMax
        Get
            Return _settings.SubAgeMax
        End Get
        Set
            _settings.SubAgeMax = Value
        End Set
    End Property
    Public Property CBLockWindow As Boolean Implements ISettings.CBLockWindow
        Get
            Return _settings.CBLockWindow
        End Get
        Set
            _settings.CBLockWindow = Value
        End Set
    End Property
    Public Property SubGreeting As String Implements ISettings.SubGreeting
        Get
            Return _settings.SubGreeting
        End Get
        Set
            _settings.SubGreeting = Value
        End Set
    End Property
    Public Property SubYes As String Implements ISettings.SubYes
        Get
            Return _settings.SubYes
        End Get
        Set
            _settings.SubYes = Value
        End Set
    End Property
    Public Property SubNo As String Implements ISettings.SubNo
        Get
            Return _settings.SubNo
        End Get
        Set
            _settings.SubNo = Value
        End Set
    End Property
    Public Property SubHonorific As String Implements ISettings.SubHonorific
        Get
            Return _settings.SubHonorific
        End Get
        Set
            _settings.SubHonorific = Value
        End Set
    End Property
    Public Property CBUseHonor As Boolean Implements ISettings.CBUseHonor
        Get
            Return _settings.CBUseHonor
        End Get
        Set
            _settings.CBUseHonor = Value
        End Set
    End Property
    Public Property CBUseName As Boolean Implements ISettings.CBUseName
        Get
            Return _settings.CBUseName
        End Get
        Set
            _settings.CBUseName = Value
        End Set
    End Property
    Public Property CBCapHonor As Boolean Implements ISettings.CBCapHonor
        Get
            Return _settings.CBCapHonor
        End Get
        Set
            _settings.CBCapHonor = Value
        End Set
    End Property
    Public Property SubCockSize As Integer Implements ISettings.SubCockSize
        Get
            Return _settings.SubCockSize
        End Get
        Set
            _settings.SubCockSize = Value
        End Set
    End Property
    Public Property SubAge As Integer Implements ISettings.SubAge
        Get
            Return _settings.SubAge
        End Get
        Set
            _settings.SubAge = Value
        End Set
    End Property
    Public Property TCAgreed As Boolean Implements ISettings.TCAgreed
        Get
            Return _settings.TCAgreed
        End Get
        Set
            _settings.TCAgreed = Value
        End Set
    End Property
    Public Property TimerSTF As Integer Implements ISettings.TimerSTF
        Get
            Return _settings.TimerSTF
        End Get
        Set
            _settings.TimerSTF = Value
        End Set
    End Property
    Public Property SubBirthMonth As Integer Implements ISettings.SubBirthMonth
        Get
            Return _settings.SubBirthMonth
        End Get
        Set
            _settings.SubBirthMonth = Value
        End Set
    End Property
    Public Property SubBirthDay As Integer Implements ISettings.SubBirthDay
        Get
            Return _settings.SubBirthDay
        End Get
        Set
            _settings.SubBirthDay = Value
        End Set
    End Property
    Public Property SubHair As String Implements ISettings.SubHair
        Get
            Return _settings.SubHair
        End Get
        Set
            _settings.SubHair = Value
        End Set
    End Property
    Public Property SubEyes As String Implements ISettings.SubEyes
        Get
            Return _settings.SubEyes
        End Get
        Set
            _settings.SubEyes = Value
        End Set
    End Property
    Public Property DomFontSize As Integer Implements ISettings.DomFontSize
        Get
            Return _settings.DomFontSize
        End Get
        Set
            _settings.DomFontSize = Value
        End Set
    End Property
    Public Property DomFont As String Implements ISettings.DomFont
        Get
            Return _settings.DomFont
        End Get
        Set
            _settings.DomFont = Value
        End Set
    End Property
    Public Property SubFont As String Implements ISettings.SubFont
        Get
            Return _settings.SubFont
        End Get
        Set
            _settings.SubFont = Value
        End Set
    End Property
    Public Property SubFontSize As Integer Implements ISettings.SubFontSize
        Get
            Return _settings.SubFontSize
        End Get
        Set
            _settings.SubFontSize = Value
        End Set
    End Property
    Public Property DomBirthMonth As Integer Implements ISettings.DomBirthMonth
        Get
            Return _settings.DomBirthMonth
        End Get
        Set
            _settings.DomBirthMonth = Value
        End Set
    End Property
    Public Property DomBirthDay As Integer Implements ISettings.DomBirthDay
        Get
            Return _settings.DomBirthDay
        End Get
        Set
            _settings.DomBirthDay = Value
        End Set
    End Property
    Public Property DomHairLength As String Implements ISettings.DomHairLength
        Get
            Return _settings.DomHairLength
        End Get
        Set
            _settings.DomHairLength = Value
        End Set
    End Property
    Public Property DomPubicHair As String Implements ISettings.DomPubicHair
        Get
            Return _settings.DomPubicHair
        End Get
        Set
            _settings.DomPubicHair = Value
        End Set
    End Property
    Public Property DomTattoos As Boolean Implements ISettings.DomTattoos
        Get
            Return _settings.DomTattoos
        End Get
        Set
            _settings.DomTattoos = Value
        End Set
    End Property
    Public Property DomFreckles As Boolean Implements ISettings.DomFreckles
        Get
            Return _settings.DomFreckles
        End Get
        Set
            _settings.DomFreckles = Value
        End Set
    End Property
    Public Property CBImageInfo As Boolean Implements ISettings.CBImageInfo
        Get
            Return _settings.CBImageInfo
        End Get
        Set
            _settings.CBImageInfo = Value
        End Set
    End Property
    Public Property DomAVStretch As Boolean Implements ISettings.DomAVStretch
        Get
            Return _settings.DomAVStretch
        End Get
        Set
            _settings.DomAVStretch = Value
        End Set
    End Property
    Public Property SubAvStretch As Boolean Implements ISettings.SubAvStretch
        Get
            Return _settings.SubAvStretch
        End Get
        Set
            _settings.SubAvStretch = Value
        End Set
    End Property
    Public Property IHardcore As String Implements ISettings.IHardcore
        Get
            Return _settings.IHardcore
        End Get
        Set
            _settings.IHardcore = Value
        End Set
    End Property
    Public Property ISoftcore As String Implements ISettings.ISoftcore
        Get
            Return _settings.ISoftcore
        End Get
        Set
            _settings.ISoftcore = Value
        End Set
    End Property
    Public Property ILesbian As String Implements ISettings.ILesbian
        Get
            Return _settings.ILesbian
        End Get
        Set
            _settings.ILesbian = Value
        End Set
    End Property
    Public Property IBlowjob As String Implements ISettings.IBlowjob
        Get
            Return _settings.IBlowjob
        End Get
        Set
            _settings.IBlowjob = Value
        End Set
    End Property
    Public Property IFemdom As String Implements ISettings.IFemdom
        Get
            Return _settings.IFemdom
        End Get
        Set
            _settings.IFemdom = Value
        End Set
    End Property
    Public Property ILezdom As String Implements ISettings.ILezdom
        Get
            Return _settings.ILezdom
        End Get
        Set
            _settings.ILezdom = Value
        End Set
    End Property
    Public Property IHentai As String Implements ISettings.IHentai
        Get
            Return _settings.IHentai
        End Get
        Set
            _settings.IHentai = Value
        End Set
    End Property
    Public Property IGay As String Implements ISettings.IGay
        Get
            Return _settings.IGay
        End Get
        Set
            _settings.IGay = Value
        End Set
    End Property
    Public Property IMaledom As String Implements ISettings.IMaledom
        Get
            Return _settings.IMaledom
        End Get
        Set
            _settings.IMaledom = Value
        End Set
    End Property
    Public Property IGeneral As String Implements ISettings.IGeneral
        Get
            Return _settings.IGeneral
        End Get
        Set
            _settings.IGeneral = Value
        End Set
    End Property
    Public Property IHardcoreSD As Boolean Implements ISettings.IHardcoreSD
        Get
            Return _settings.IHardcoreSD
        End Get
        Set
            _settings.IHardcoreSD = Value
        End Set
    End Property
    Public Property ISoftcoreSD As Boolean Implements ISettings.ISoftcoreSD
        Get
            Return _settings.ISoftcoreSD
        End Get
        Set
            _settings.ISoftcoreSD = Value
        End Set
    End Property
    Public Property ILesbianSD As Boolean Implements ISettings.ILesbianSD
        Get
            Return _settings.ILesbianSD
        End Get
        Set
            _settings.ILesbianSD = Value
        End Set
    End Property
    Public Property IBlowjobSD As Boolean Implements ISettings.IBlowjobSD
        Get
            Return _settings.IBlowjobSD
        End Get
        Set
            _settings.IBlowjobSD = Value
        End Set
    End Property
    Public Property IFemdomSD As Boolean Implements ISettings.IFemdomSD
        Get
            Return _settings.IFemdomSD
        End Get
        Set
            _settings.IFemdomSD = Value
        End Set
    End Property
    Public Property ILezdomSD As Boolean Implements ISettings.ILezdomSD
        Get
            Return _settings.ILezdomSD
        End Get
        Set
            _settings.ILezdomSD = Value
        End Set
    End Property
    Public Property IHentaiSD As Boolean Implements ISettings.IHentaiSD
        Get
            Return _settings.IHentaiSD
        End Get
        Set
            _settings.IHentaiSD = Value
        End Set
    End Property
    Public Property IGaySD As Boolean Implements ISettings.IGaySD
        Get
            Return _settings.IGaySD
        End Get
        Set
            _settings.IGaySD = Value
        End Set
    End Property
    Public Property IMaledomSD As Boolean Implements ISettings.IMaledomSD
        Get
            Return _settings.IMaledomSD
        End Get
        Set
            _settings.IMaledomSD = Value
        End Set
    End Property
    Public Property IGeneralSD As Boolean Implements ISettings.IGeneralSD
        Get
            Return _settings.IGeneralSD
        End Get
        Set
            _settings.IGeneralSD = Value
        End Set
    End Property
    Public Property ICaptionsSD As Boolean Implements ISettings.ICaptionsSD
        Get
            Return _settings.ICaptionsSD
        End Get
        Set
            _settings.ICaptionsSD = Value
        End Set
    End Property
    Public Property CBIHardcore As Boolean Implements ISettings.CBIHardcore
        Get
            Return _settings.CBIHardcore
        End Get
        Set
            _settings.CBIHardcore = Value
        End Set
    End Property
    Public Property CBISoftcore As Boolean Implements ISettings.CBISoftcore
        Get
            Return _settings.CBISoftcore
        End Get
        Set
            _settings.CBISoftcore = Value
        End Set
    End Property
    Public Property CBILesbian As Boolean Implements ISettings.CBILesbian
        Get
            Return _settings.CBILesbian
        End Get
        Set
            _settings.CBILesbian = Value
        End Set
    End Property
    Public Property CBIBlowjob As Boolean Implements ISettings.CBIBlowjob
        Get
            Return _settings.CBIBlowjob
        End Get
        Set
            _settings.CBIBlowjob = Value
        End Set
    End Property
    Public Property CBIFemdom As Boolean Implements ISettings.CBIFemdom
        Get
            Return _settings.CBIFemdom
        End Get
        Set
            _settings.CBIFemdom = Value
        End Set
    End Property
    Public Property CBILezdom As Boolean Implements ISettings.CBILezdom
        Get
            Return _settings.CBILezdom
        End Get
        Set
            _settings.CBILezdom = Value
        End Set
    End Property
    Public Property CBIHentai As Boolean Implements ISettings.CBIHentai
        Get
            Return _settings.CBIHentai
        End Get
        Set
            _settings.CBIHentai = Value
        End Set
    End Property
    Public Property CBIGay As Boolean Implements ISettings.CBIGay
        Get
            Return _settings.CBIGay
        End Get
        Set
            _settings.CBIGay = Value
        End Set
    End Property
    Public Property CBIMaledom As Boolean Implements ISettings.CBIMaledom
        Get
            Return _settings.CBIMaledom
        End Get
        Set
            _settings.CBIMaledom = Value
        End Set
    End Property
    Public Property CBIGeneral As Boolean Implements ISettings.CBIGeneral
        Get
            Return _settings.CBIGeneral
        End Get
        Set
            _settings.CBIGeneral = Value
        End Set
    End Property
    Public Property CBICaptions As Boolean Implements ISettings.CBICaptions
        Get
            Return _settings.CBICaptions
        End Get
        Set
            _settings.CBICaptions = Value
        End Set
    End Property
    Public Property ICaptions As String Implements ISettings.ICaptions
        Get
            Return _settings.ICaptions
        End Get
        Set
            _settings.ICaptions = Value
        End Set
    End Property
    Public Property DomImageDir As String Implements ISettings.DomImageDir
        Get
            Return _settings.DomImageDir
        End Get
        Set
            _settings.DomImageDir = Value
        End Set
    End Property
    Public Property CBTCock As Boolean Implements ISettings.CBTCock
        Get
            Return _settings.CBTCock
        End Get
        Set
            _settings.CBTCock = Value
        End Set
    End Property
    Public Property CBTBalls As Boolean Implements ISettings.CBTBalls
        Get
            Return _settings.CBTBalls
        End Get
        Set
            _settings.CBTBalls = Value
        End Set
    End Property
    Public Property ChastityPA As Boolean Implements ISettings.ChastityPA
        Get
            Return _settings.ChastityPA
        End Get
        Set
            _settings.ChastityPA = Value
        End Set
    End Property
    Public Property ChastitySpikes As Boolean Implements ISettings.ChastitySpikes
        Get
            Return _settings.ChastitySpikes
        End Get
        Set
            _settings.ChastitySpikes = Value
        End Set
    End Property
    Public Property SubInChastity As Boolean Implements ISettings.SubInChastity
        Get
            Return _settings.SubInChastity
        End Get
        Set
            _settings.SubInChastity = Value
        End Set
    End Property
    Public Property LongEdge As Integer Implements ISettings.LongEdge
        Get
            Return _settings.LongEdge
        End Get
        Set
            _settings.LongEdge = Value
        End Set
    End Property
    Public Property CBLongEdgeInterrupt As Boolean Implements ISettings.CBLongEdgeInterrupt
        Get
            Return _settings.CBLongEdgeInterrupt
        End Get
        Set
            _settings.CBLongEdgeInterrupt = Value
        End Set
    End Property
    Public Property HoldTheEdgeMax As Integer Implements ISettings.HoldTheEdgeMax
        Get
            Return _settings.HoldTheEdgeMax
        End Get
        Set
            _settings.HoldTheEdgeMax = Value
        End Set
    End Property
    Public Property HoldEdgeTimeTotal As Integer Implements ISettings.HoldEdgeTimeTotal
        Get
            Return _settings.HoldEdgeTimeTotal
        End Get
        Set
            _settings.HoldEdgeTimeTotal = Value
        End Set
    End Property
    Public Property CBTSlider As Integer Implements ISettings.CBTSlider
        Get
            Return _settings.CBTSlider
        End Get
        Set
            _settings.CBTSlider = Value
        End Set
    End Property
    Public Property SubCircumcised As Boolean Implements ISettings.SubCircumcised
        Get
            Return _settings.SubCircumcised
        End Get
        Set
            _settings.SubCircumcised = Value
        End Set
    End Property
    Public Property SubPierced As Boolean Implements ISettings.SubPierced
        Get
            Return _settings.SubPierced
        End Get
        Set
            _settings.SubPierced = Value
        End Set
    End Property
    Public Property DomEmpathy As Integer Implements ISettings.DomEmpathy
        Get
            Return _settings.DomEmpathy
        End Get
        Set
            _settings.DomEmpathy = Value
        End Set
    End Property
    Public Property RangeOrgasm As Boolean Implements ISettings.RangeOrgasm
        Get
            Return _settings.RangeOrgasm
        End Get
        Set
            _settings.RangeOrgasm = Value
        End Set
    End Property
    Public Property RangeRuin As Boolean Implements ISettings.RangeRuin
        Get
            Return _settings.RangeRuin
        End Get
        Set
            _settings.RangeRuin = Value
        End Set
    End Property
    Public Property AllowOften As Integer Implements ISettings.AllowOften
        Get
            Return _settings.AllowOften
        End Get
        Set
            _settings.AllowOften = Value
        End Set
    End Property
    Public Property AllowSometimes As Integer Implements ISettings.AllowSometimes
        Get
            Return _settings.AllowSometimes
        End Get
        Set
            _settings.AllowSometimes = Value
        End Set
    End Property
    Public Property AllowRarely As Integer Implements ISettings.AllowRarely
        Get
            Return _settings.AllowRarely
        End Get
        Set
            _settings.AllowRarely = Value
        End Set
    End Property
    Public Property RuinOften As Integer Implements ISettings.RuinOften
        Get
            Return _settings.RuinOften
        End Get
        Set
            _settings.RuinOften = Value
        End Set
    End Property
    Public Property RuinSometimes As Integer Implements ISettings.RuinSometimes
        Get
            Return _settings.RuinSometimes
        End Get
        Set
            _settings.RuinSometimes = Value
        End Set
    End Property
    Public Property RuinRarely As Integer Implements ISettings.RuinRarely
        Get
            Return _settings.RuinRarely
        End Get
        Set
            _settings.RuinRarely = Value
        End Set
    End Property
    Public Property Chastity As Boolean Implements ISettings.Chastity
        Get
            Return _settings.Chastity
        End Get
        Set
            _settings.Chastity = Value
        End Set
    End Property
    Public Property Safeword As String Implements ISettings.Safeword
        Get
            Return _settings.Safeword
        End Get
        Set
            _settings.Safeword = Value
        End Set
    End Property
    Public Property CaloriesConsumed As Integer Implements ISettings.CaloriesConsumed
        Get
            Return _settings.CaloriesConsumed
        End Get
        Set
            _settings.CaloriesConsumed = Value
        End Set
    End Property
    Public Property CaloriesGoal As Integer Implements ISettings.CaloriesGoal
        Get
            Return _settings.CaloriesGoal
        End Get
        Set
            _settings.CaloriesGoal = Value
        End Set
    End Property
    Public Property VitalSubDisclaimer As Boolean Implements ISettings.VitalSubDisclaimer
        Get
            Return _settings.VitalSubDisclaimer
        End Get
        Set
            _settings.VitalSubDisclaimer = Value
        End Set
    End Property
    Public Property VitalSub As Boolean Implements ISettings.VitalSub
        Get
            Return _settings.VitalSub
        End Get
        Set
            _settings.VitalSub = Value
        End Set
    End Property
    Public Property VitalSubAssignments As Boolean Implements ISettings.VitalSubAssignments
        Get
            Return _settings.VitalSubAssignments
        End Get
        Set
            _settings.VitalSubAssignments = Value
        End Set
    End Property
    Public Property BP1 As String Implements ISettings.BP1
        Get
            Return _settings.BP1
        End Get
        Set
            _settings.BP1 = Value
        End Set
    End Property
    Public Property BP2 As String Implements ISettings.BP2
        Get
            Return _settings.BP2
        End Get
        Set
            _settings.BP2 = Value
        End Set
    End Property
    Public Property BP3 As String Implements ISettings.BP3
        Get
            Return _settings.BP3
        End Get
        Set
            _settings.BP3 = Value
        End Set
    End Property
    Public Property BP4 As String Implements ISettings.BP4
        Get
            Return _settings.BP4
        End Get
        Set
            _settings.BP4 = Value
        End Set
    End Property
    Public Property BP5 As String Implements ISettings.BP5
        Get
            Return _settings.BP5
        End Get
        Set
            _settings.BP5 = Value
        End Set
    End Property
    Public Property BP6 As String Implements ISettings.BP6
        Get
            Return _settings.BP6
        End Get
        Set
            _settings.BP6 = Value
        End Set
    End Property
    Public Property BN1 As String Implements ISettings.BN1
        Get
            Return _settings.BN1
        End Get
        Set
            _settings.BN1 = Value
        End Set
    End Property
    Public Property BN2 As String Implements ISettings.BN2
        Get
            Return _settings.BN2
        End Get
        Set
            _settings.BN2 = Value
        End Set
    End Property
    Public Property BN3 As String Implements ISettings.BN3
        Get
            Return _settings.BN3
        End Get
        Set
            _settings.BN3 = Value
        End Set
    End Property
    Public Property BN4 As String Implements ISettings.BN4
        Get
            Return _settings.BN4
        End Get
        Set
            _settings.BN4 = Value
        End Set
    End Property
    Public Property BN5 As String Implements ISettings.BN5
        Get
            Return _settings.BN5
        End Get
        Set
            _settings.BN5 = Value
        End Set
    End Property
    Public Property BN6 As String Implements ISettings.BN6
        Get
            Return _settings.BN6
        End Get
        Set
            _settings.BN6 = Value
        End Set
    End Property
    Public Property SP1 As String Implements ISettings.SP1
        Get
            Return _settings.SP1
        End Get
        Set
            _settings.SP1 = Value
        End Set
    End Property
    Public Property SP2 As String Implements ISettings.SP2
        Get
            Return _settings.SP2
        End Get
        Set
            _settings.SP2 = Value
        End Set
    End Property
    Public Property SP3 As String Implements ISettings.SP3
        Get
            Return _settings.SP3
        End Get
        Set
            _settings.SP3 = Value
        End Set
    End Property
    Public Property SP4 As String Implements ISettings.SP4
        Get
            Return _settings.SP4
        End Get
        Set
            _settings.SP4 = Value
        End Set
    End Property
    Public Property SP5 As String Implements ISettings.SP5
        Get
            Return _settings.SP5
        End Get
        Set
            _settings.SP5 = Value
        End Set
    End Property
    Public Property SP6 As String Implements ISettings.SP6
        Get
            Return _settings.SP6
        End Get
        Set
            _settings.SP6 = Value
        End Set
    End Property
    Public Property SN1 As String Implements ISettings.SN1
        Get
            Return _settings.SN1
        End Get
        Set
            _settings.SN1 = Value
        End Set
    End Property
    Public Property SN2 As String Implements ISettings.SN2
        Get
            Return _settings.SN2
        End Get
        Set
            _settings.SN2 = Value
        End Set
    End Property
    Public Property SN3 As String Implements ISettings.SN3
        Get
            Return _settings.SN3
        End Get
        Set
            _settings.SN3 = Value
        End Set
    End Property
    Public Property SN4 As String Implements ISettings.SN4
        Get
            Return _settings.SN4
        End Get
        Set
            _settings.SN4 = Value
        End Set
    End Property
    Public Property SN5 As String Implements ISettings.SN5
        Get
            Return _settings.SN5
        End Get
        Set
            _settings.SN5 = Value
        End Set
    End Property
    Public Property SN6 As String Implements ISettings.SN6
        Get
            Return _settings.SN6
        End Get
        Set
            _settings.SN6 = Value
        End Set
    End Property
    Public Property GP1 As String Implements ISettings.GP1
        Get
            Return _settings.GP1
        End Get
        Set
            _settings.GP1 = Value
        End Set
    End Property
    Public Property GP2 As String Implements ISettings.GP2
        Get
            Return _settings.GP2
        End Get
        Set
            _settings.GP2 = Value
        End Set
    End Property
    Public Property GP3 As String Implements ISettings.GP3
        Get
            Return _settings.GP3
        End Get
        Set
            _settings.GP3 = Value
        End Set
    End Property
    Public Property GP4 As String Implements ISettings.GP4
        Get
            Return _settings.GP4
        End Get
        Set
            _settings.GP4 = Value
        End Set
    End Property
    Public Property GP5 As String Implements ISettings.GP5
        Get
            Return _settings.GP5
        End Get
        Set
            _settings.GP5 = Value
        End Set
    End Property
    Public Property GP6 As String Implements ISettings.GP6
        Get
            Return _settings.GP6
        End Get
        Set
            _settings.GP6 = Value
        End Set
    End Property
    Public Property GN1 As String Implements ISettings.GN1
        Get
            Return _settings.GN1
        End Get
        Set
            _settings.GN1 = Value
        End Set
    End Property
    Public Property GN2 As String Implements ISettings.GN2
        Get
            Return _settings.GN2
        End Get
        Set
            _settings.GN2 = Value
        End Set
    End Property
    Public Property GN3 As String Implements ISettings.GN3
        Get
            Return _settings.GN3
        End Get
        Set
            _settings.GN3 = Value
        End Set
    End Property
    Public Property GN4 As String Implements ISettings.GN4
        Get
            Return _settings.GN4
        End Get
        Set
            _settings.GN4 = Value
        End Set
    End Property
    Public Property GN5 As String Implements ISettings.GN5
        Get
            Return _settings.GN5
        End Get
        Set
            _settings.GN5 = Value
        End Set
    End Property
    Public Property GN6 As String Implements ISettings.GN6
        Get
            Return _settings.GN6
        End Get
        Set
            _settings.GN6 = Value
        End Set
    End Property
    Public Property CardBack As String Implements ISettings.CardBack
        Get
            Return _settings.CardBack
        End Get
        Set
            _settings.CardBack = Value
        End Set
    End Property
    Public Property GoldTokens As Integer Implements ISettings.GoldTokens
        Get
            Return _settings.GoldTokens
        End Get
        Set
            _settings.GoldTokens = Value
        End Set
    End Property
    Public Property SilverTokens As Integer Implements ISettings.SilverTokens
        Get
            Return _settings.SilverTokens
        End Get
        Set
            _settings.SilverTokens = Value
        End Set
    End Property
    Public Property BronzeTokens As Integer Implements ISettings.BronzeTokens
        Get
            Return _settings.BronzeTokens
        End Get
        Set
            _settings.BronzeTokens = Value
        End Set
    End Property
    Public Property B1 As Integer Implements ISettings.B1
        Get
            Return _settings.B1
        End Get
        Set
            _settings.B1 = Value
        End Set
    End Property
    Public Property B2 As Integer Implements ISettings.B2
        Get
            Return _settings.B2
        End Get
        Set
            _settings.B2 = Value
        End Set
    End Property
    Public Property B3 As Integer Implements ISettings.B3
        Get
            Return _settings.B3
        End Get
        Set
            _settings.B3 = Value
        End Set
    End Property
    Public Property B4 As Integer Implements ISettings.B4
        Get
            Return _settings.B4
        End Get
        Set
            _settings.B4 = Value
        End Set
    End Property
    Public Property B5 As Integer Implements ISettings.B5
        Get
            Return _settings.B5
        End Get
        Set
            _settings.B5 = Value
        End Set
    End Property
    Public Property B6 As Integer Implements ISettings.B6
        Get
            Return _settings.B6
        End Get
        Set
            _settings.B6 = Value
        End Set
    End Property
    Public Property S1 As Integer Implements ISettings.S1
        Get
            Return _settings.S1
        End Get
        Set
            _settings.S1 = Value
        End Set
    End Property
    Public Property S2 As Integer Implements ISettings.S2
        Get
            Return _settings.S2
        End Get
        Set
            _settings.S2 = Value
        End Set
    End Property
    Public Property S3 As Integer Implements ISettings.S3
        Get
            Return _settings.S3
        End Get
        Set
            _settings.S3 = Value
        End Set
    End Property
    Public Property S4 As Integer Implements ISettings.S4
        Get
            Return _settings.S4
        End Get
        Set
            _settings.S4 = Value
        End Set
    End Property
    Public Property S5 As Integer Implements ISettings.S5
        Get
            Return _settings.S5
        End Get
        Set
            _settings.S5 = Value
        End Set
    End Property
    Public Property S6 As Integer Implements ISettings.S6
        Get
            Return _settings.S6
        End Get
        Set
            _settings.S6 = Value
        End Set
    End Property
    Public Property G1 As Integer Implements ISettings.G1
        Get
            Return _settings.G1
        End Get
        Set
            _settings.G1 = Value
        End Set
    End Property
    Public Property G2 As Integer Implements ISettings.G2
        Get
            Return _settings.G2
        End Get
        Set
            _settings.G2 = Value
        End Set
    End Property
    Public Property G3 As Integer Implements ISettings.G3
        Get
            Return _settings.G3
        End Get
        Set
            _settings.G3 = Value
        End Set
    End Property
    Public Property G4 As Integer Implements ISettings.G4
        Get
            Return _settings.G4
        End Get
        Set
            _settings.G4 = Value
        End Set
    End Property
    Public Property G5 As Integer Implements ISettings.G5
        Get
            Return _settings.G5
        End Get
        Set
            _settings.G5 = Value
        End Set
    End Property
    Public Property G6 As Integer Implements ISettings.G6
        Get
            Return _settings.G6
        End Get
        Set
            _settings.G6 = Value
        End Set
    End Property
    Public Property ClearWishlist As Boolean Implements ISettings.ClearWishlist
        Get
            Return _settings.ClearWishlist
        End Get
        Set
            _settings.ClearWishlist = Value
        End Set
    End Property
    Public Property WishlistName As String Implements ISettings.WishlistName
        Get
            Return _settings.WishlistName
        End Get
        Set
            _settings.WishlistName = Value
        End Set
    End Property
    Public Property WishlistPreview As String Implements ISettings.WishlistPreview
        Get
            Return _settings.WishlistPreview
        End Get
        Set
            _settings.WishlistPreview = Value
        End Set
    End Property
    Public Property WishlistTokenType As String Implements ISettings.WishlistTokenType
        Get
            Return _settings.WishlistTokenType
        End Get
        Set
            _settings.WishlistTokenType = Value
        End Set
    End Property
    Public Property WishlistCost As Integer Implements ISettings.WishlistCost
        Get
            Return _settings.WishlistCost
        End Get
        Set
            _settings.WishlistCost = Value
        End Set
    End Property
    Public Property WishlistNote As String Implements ISettings.WishlistNote
        Get
            Return _settings.WishlistNote
        End Get
        Set
            _settings.WishlistNote = Value
        End Set
    End Property
    Public Property AvgEdgeCountRest As Integer Implements ISettings.AvgEdgeCountRest
        Get
            Return _settings.AvgEdgeCountRest
        End Get
        Set
            _settings.AvgEdgeCountRest = Value
        End Set
    End Property
    Public Property PersonalityIndex As Integer Implements ISettings.PersonalityIndex
        Get
            Return _settings.PersonalityIndex
        End Get
        Set
            _settings.PersonalityIndex = Value
        End Set
    End Property
    Public Property LargeUI As Boolean Implements ISettings.LargeUI
        Get
            Return _settings.LargeUI
        End Get
        Set
            _settings.LargeUI = Value
        End Set
    End Property
    Public Property CBJackintheBox As Boolean Implements ISettings.CBJackintheBox
        Get
            Return _settings.CBJackintheBox
        End Get
        Set
            _settings.CBJackintheBox = Value
        End Set
    End Property
    Public Property NextImageChance As Integer Implements ISettings.NextImageChance
        Get
            Return _settings.NextImageChance
        End Get
        Set
            _settings.NextImageChance = Value
        End Set
    End Property
    Public Property CBEdgeUseAvg As Boolean Implements ISettings.CBEdgeUseAvg
        Get
            Return _settings.CBEdgeUseAvg
        End Get
        Set
            _settings.CBEdgeUseAvg = Value
        End Set
    End Property
    Public Property CBLongEdgeTaunts As Boolean Implements ISettings.CBLongEdgeTaunts
        Get
            Return _settings.CBLongEdgeTaunts
        End Get
        Set
            _settings.CBLongEdgeTaunts = Value
        End Set
    End Property
    Public Property CBLongEdgeInterrupts As Boolean Implements ISettings.CBLongEdgeInterrupts
        Get
            Return _settings.CBLongEdgeInterrupts
        End Get
        Set
            _settings.CBLongEdgeInterrupts = Value
        End Set
    End Property
    Public Property OrgasmsRemaining As Integer Implements ISettings.OrgasmsRemaining
        Get
            Return _settings.OrgasmsRemaining
        End Get
        Set
            _settings.OrgasmsRemaining = Value
        End Set
    End Property
    Public Property OrgasmsLocked As Boolean Implements ISettings.OrgasmsLocked
        Get
            Return _settings.OrgasmsLocked
        End Get
        Set
            _settings.OrgasmsLocked = Value
        End Set
    End Property
    Public Property TimerVTF As Integer Implements ISettings.TimerVTF
        Get
            Return _settings.TimerVTF
        End Get
        Set
            _settings.TimerVTF = Value
        End Set
    End Property
    Public Property CBTeaseLengthDD As Boolean Implements ISettings.CBTeaseLengthDD
        Get
            Return _settings.CBTeaseLengthDD
        End Get
        Set
            _settings.CBTeaseLengthDD = Value
        End Set
    End Property
    Public Property CBTauntCycleDD As Boolean Implements ISettings.CBTauntCycleDD
        Get
            Return _settings.CBTauntCycleDD
        End Get
        Set
            _settings.CBTauntCycleDD = Value
        End Set
    End Property
    Public Property CBOwnChastity As Boolean Implements ISettings.CBOwnChastity
        Get
            Return _settings.CBOwnChastity
        End Get
        Set
            _settings.CBOwnChastity = Value
        End Set
    End Property
    Public Property SmallUI As Boolean Implements ISettings.SmallUI
        Get
            Return _settings.SmallUI
        End Get
        Set
            _settings.SmallUI = Value
        End Set
    End Property
    Public Property UI768 As Boolean Implements ISettings.UI768
        Get
            Return _settings.UI768
        End Get
        Set
            _settings.UI768 = Value
        End Set
    End Property
    Public Property CBIncludeGifs As Boolean Implements ISettings.CBIncludeGifs
        Get
            Return _settings.CBIncludeGifs
        End Get
        Set
            _settings.CBIncludeGifs = Value
        End Set
    End Property
    Public Property CBHimHer As Boolean Implements ISettings.CBHimHer
        Get
            Return _settings.CBHimHer
        End Get
        Set
            _settings.CBHimHer = Value
        End Set
    End Property
    Public Property DomDeleteMedia As Boolean Implements ISettings.DomDeleteMedia
        Get
            Return _settings.DomDeleteMedia
        End Get
        Set
            _settings.DomDeleteMedia = Value
        End Set
    End Property
    Public Property TeaseLengthMin As Integer Implements ISettings.TeaseLengthMin
        Get
            Return _settings.TeaseLengthMin
        End Get
        Set
            _settings.TeaseLengthMin = Value
        End Set
    End Property
    Public Property TeaseLengthMax As Integer Implements ISettings.TeaseLengthMax
        Get
            Return _settings.TeaseLengthMax
        End Get
        Set
            _settings.TeaseLengthMax = Value
        End Set
    End Property
    Public Property TauntCycleMin As Integer Implements ISettings.TauntCycleMin
        Get
            Return _settings.TauntCycleMin
        End Get
        Set
            _settings.TauntCycleMin = Value
        End Set
    End Property
    Public Property TauntCycleMax As Integer Implements ISettings.TauntCycleMax
        Get
            Return _settings.TauntCycleMax
        End Get
        Set
            _settings.TauntCycleMax = Value
        End Set
    End Property
    Public Property RedLightMin As Integer Implements ISettings.RedLightMin
        Get
            Return _settings.RedLightMin
        End Get
        Set
            _settings.RedLightMin = Value
        End Set
    End Property
    Public Property RedLightMax As Integer Implements ISettings.RedLightMax
        Get
            Return _settings.RedLightMax
        End Get
        Set
            _settings.RedLightMax = Value
        End Set
    End Property
    Public Property GreenLightMin As Integer Implements ISettings.GreenLightMin
        Get
            Return _settings.GreenLightMin
        End Get
        Set
            _settings.GreenLightMin = Value
        End Set
    End Property
    Public Property GreenLightMax As Integer Implements ISettings.GreenLightMax
        Get
            Return _settings.GreenLightMax
        End Get
        Set
            _settings.GreenLightMax = Value
        End Set
    End Property
    Public Property SlideshowMode As String Implements ISettings.SlideshowMode
        Get
            Return _settings.SlideshowMode
        End Get
        Set
            _settings.SlideshowMode = Value
        End Set
    End Property
    Public Property OrgasmLockDate As DateTime Implements ISettings.OrgasmLockDate
        Get
            Return _settings.OrgasmLockDate
        End Get
        Set
            _settings.OrgasmLockDate = Value
        End Set
    End Property
    Public Property AuditStartup As Boolean Implements ISettings.AuditStartup
        Get
            Return _settings.AuditStartup
        End Get
        Set
            _settings.AuditStartup = Value
        End Set
    End Property
    Public Property WishlistDate As DateTime Implements ISettings.WishlistDate
        Get
            Return _settings.WishlistDate
        End Get
        Set
            _settings.WishlistDate = Value
        End Set
    End Property
    Public Property LastOrgasm As DateTime Implements ISettings.LastOrgasm
        Get
            Return _settings.LastOrgasm
        End Get
        Set
            _settings.LastOrgasm = Value
        End Set
    End Property
    Public Property LastRuined As DateTime Implements ISettings.LastRuined
        Get
            Return _settings.LastRuined
        End Get
        Set
            _settings.LastRuined = Value
        End Set
    End Property
    Public Property DateStamp As DateTime Implements ISettings.DateStamp
        Get
            Return _settings.DateStamp
        End Get
        Set
            _settings.DateStamp = Value
        End Set
    End Property
    Public Property TokenTasks As DateTime Implements ISettings.TokenTasks
        Get
            Return _settings.TokenTasks
        End Get
        Set
            _settings.TokenTasks = Value
        End Set
    End Property
    Public Property WebToyStart As String Implements ISettings.WebToyStart
        Get
            Return _settings.WebToyStart
        End Get
        Set
            _settings.WebToyStart = Value
        End Set
    End Property
    Public Property WebToyStop As String Implements ISettings.WebToyStop
        Get
            Return _settings.WebToyStop
        End Get
        Set
            _settings.WebToyStop = Value
        End Set
    End Property
    Public Property CockToClit As Boolean Implements ISettings.CockToClit
        Get
            Return _settings.CockToClit
        End Get
        Set
            _settings.CockToClit = Value
        End Set
    End Property
    Public Property BallsToPussy As Boolean Implements ISettings.BallsToPussy
        Get
            Return _settings.BallsToPussy
        End Get
        Set
            _settings.BallsToPussy = Value
        End Set
    End Property
    Public Property Contact1ImageDir As String Implements ISettings.Contact1ImageDir
        Get
            Return _settings.Contact1ImageDir
        End Get
        Set
            _settings.Contact1ImageDir = Value
        End Set
    End Property
    Public Property Contact2ImageDir As String Implements ISettings.Contact2ImageDir
        Get
            Return _settings.Contact2ImageDir
        End Get
        Set
            _settings.Contact2ImageDir = Value
        End Set
    End Property
    Public Property Contact3ImageDir As String Implements ISettings.Contact3ImageDir
        Get
            Return _settings.Contact3ImageDir
        End Get
        Set
            _settings.Contact3ImageDir = Value
        End Set
    End Property
    Public Property CBGlitterFeedOff As Boolean Implements ISettings.CBGlitterFeedOff
        Get
            Return _settings.CBGlitterFeedOff
        End Get
        Set
            _settings.CBGlitterFeedOff = Value
        End Set
    End Property
    Public Property CBGlitterFeedScripts As Boolean Implements ISettings.CBGlitterFeedScripts
        Get
            Return _settings.CBGlitterFeedScripts
        End Get
        Set
            _settings.CBGlitterFeedScripts = Value
        End Set
    End Property
    Public Property TeaseAILanguage As String Implements ISettings.TeaseAILanguage
        Get
            Return _settings.TeaseAILanguage
        End Get
        Set
            _settings.TeaseAILanguage = Value
        End Set
    End Property
    Public Property Shortcuts As Boolean Implements ISettings.Shortcuts
        Get
            Return _settings.Shortcuts
        End Get
        Set
            _settings.Shortcuts = Value
        End Set
    End Property
    Public Property ShowShortcuts As Boolean Implements ISettings.ShowShortcuts
        Get
            Return _settings.ShowShortcuts
        End Get
        Set
            _settings.ShowShortcuts = Value
        End Set
    End Property
    Public Property ShortYes As String Implements ISettings.ShortYes
        Get
            Return _settings.ShortYes
        End Get
        Set
            _settings.ShortYes = Value
        End Set
    End Property
    Public Property ShortNo As String Implements ISettings.ShortNo
        Get
            Return _settings.ShortNo
        End Get
        Set
            _settings.ShortNo = Value
        End Set
    End Property
    Public Property ShortEdge As String Implements ISettings.ShortEdge
        Get
            Return _settings.ShortEdge
        End Get
        Set
            _settings.ShortEdge = Value
        End Set
    End Property
    Public Property ShortSpeedUp As String Implements ISettings.ShortSpeedUp
        Get
            Return _settings.ShortSpeedUp
        End Get
        Set
            _settings.ShortSpeedUp = Value
        End Set
    End Property
    Public Property ShortSlowDown As String Implements ISettings.ShortSlowDown
        Get
            Return _settings.ShortSlowDown
        End Get
        Set
            _settings.ShortSlowDown = Value
        End Set
    End Property
    Public Property ShortStop As String Implements ISettings.ShortStop
        Get
            Return _settings.ShortStop
        End Get
        Set
            _settings.ShortStop = Value
        End Set
    End Property
    Public Property ShortStroke As String Implements ISettings.ShortStroke
        Get
            Return _settings.ShortStroke
        End Get
        Set
            _settings.ShortStroke = Value
        End Set
    End Property
    Public Property ShortCum As String Implements ISettings.ShortCum
        Get
            Return _settings.ShortCum
        End Get
        Set
            _settings.ShortCum = Value
        End Set
    End Property
    Public Property ShortGreet As String Implements ISettings.ShortGreet
        Get
            Return _settings.ShortGreet
        End Get
        Set
            _settings.ShortGreet = Value
        End Set
    End Property
    Public Property ShortSafeword As String Implements ISettings.ShortSafeword
        Get
            Return _settings.ShortSafeword
        End Get
        Set
            _settings.ShortSafeword = Value
        End Set
    End Property
    Public Property Patch45Tokens As Boolean Implements ISettings.Patch45Tokens
        Get
            Return _settings.Patch45Tokens
        End Get
        Set
            _settings.Patch45Tokens = Value
        End Set
    End Property
    Public Property WindowHeight As Integer Implements ISettings.WindowHeight
        Get
            Return _settings.WindowHeight
        End Get
        Set
            _settings.WindowHeight = Value
        End Set
    End Property
    Public Property WindowWidth As Integer Implements ISettings.WindowWidth
        Get
            Return _settings.WindowWidth
        End Get
        Set
            _settings.WindowWidth = Value
        End Set
    End Property
    Public Property UIColor As String Implements ISettings.UIColor
        Get
            Return _settings.UIColor
        End Get
        Set
            _settings.UIColor = Value
        End Set
    End Property
    Public Property TC2Agreed As Boolean Implements ISettings.TC2Agreed
        Get
            Return _settings.TC2Agreed
        End Get
        Set
            _settings.TC2Agreed = Value
        End Set
    End Property
    Public Property LastDomTagURL As String Implements ISettings.LastDomTagURL
        Get
            Return _settings.LastDomTagURL
        End Get
        Set
            _settings.LastDomTagURL = Value
        End Set
    End Property
    Public Property Sys_SubLeftEarly As Integer Implements ISettings.Sys_SubLeftEarly
        Get
            Return _settings.Sys_SubLeftEarly
        End Get
        Set
            _settings.Sys_SubLeftEarly = Value
        End Set
    End Property
    Public Property Sys_SubLeftEarlyTotal As Integer Implements ISettings.Sys_SubLeftEarlyTotal
        Get
            Return _settings.Sys_SubLeftEarlyTotal
        End Get
        Set
            _settings.Sys_SubLeftEarlyTotal = Value
        End Set
    End Property
    Public Property AIBoxDir As Boolean Implements ISettings.AIBoxDir
        Get
            Return _settings.AIBoxDir
        End Get
        Set
            _settings.AIBoxDir = Value
        End Set
    End Property
    Public Property AIBoxOpen As Boolean Implements ISettings.AIBoxOpen
        Get
            Return _settings.AIBoxOpen
        End Get
        Set
            _settings.AIBoxOpen = Value
        End Set
    End Property
    Public Property BackgroundColor As Color Implements ISettings.BackgroundColor
        Get
            Return _settings.BackgroundColor
        End Get
        Set
            _settings.BackgroundColor = Value
        End Set
    End Property
    Public Property BackgroundImage As String Implements ISettings.BackgroundImage
        Get
            Return _settings.BackgroundImage
        End Get
        Set
            _settings.BackgroundImage = Value
        End Set
    End Property
    Public Property ButtonColor As Color Implements ISettings.ButtonColor
        Get
            Return _settings.ButtonColor
        End Get
        Set
            _settings.ButtonColor = Value
        End Set
    End Property
    Public Property TextColor As Color Implements ISettings.TextColor
        Get
            Return _settings.TextColor
        End Get
        Set
            _settings.TextColor = Value
        End Set
    End Property
    Public Property ChatWindowColor As Color Implements ISettings.ChatWindowColor
        Get
            Return _settings.ChatWindowColor
        End Get
        Set
            _settings.ChatWindowColor = Value
        End Set
    End Property
    Public Property ChatTextColor As Color Implements ISettings.ChatTextColor
        Get
            Return _settings.ChatTextColor
        End Get
        Set
            _settings.ChatTextColor = Value
        End Set
    End Property
    Public Property BackgroundStretch As Boolean Implements ISettings.BackgroundStretch
        Get
            Return _settings.BackgroundStretch
        End Get
        Set
            _settings.BackgroundStretch = Value
        End Set
    End Property
    Public Property CBInputIcon As Boolean Implements ISettings.CBInputIcon
        Get
            Return _settings.CBInputIcon
        End Get
        Set
            _settings.CBInputIcon = Value
        End Set
    End Property
    Public Property DateTextColor As Color Implements ISettings.DateTextColor
        Get
            Return _settings.DateTextColor
        End Get
        Set
            _settings.DateTextColor = Value
        End Set
    End Property
    Public Property DateBackColor As Color Implements ISettings.DateBackColor
        Get
            Return _settings.DateBackColor
        End Get
        Set
            _settings.DateBackColor = Value
        End Set
    End Property
    Public Property CBDateTransparent As Boolean Implements ISettings.CBDateTransparent
        Get
            Return _settings.CBDateTransparent
        End Get
        Set
            _settings.CBDateTransparent = Value
        End Set
    End Property
    Public Property MirrorWindows As Boolean Implements ISettings.MirrorWindows
        Get
            Return _settings.MirrorWindows
        End Get
        Set
            _settings.MirrorWindows = Value
        End Set
    End Property
    Public Property WakeUp As DateTime Implements ISettings.WakeUp
        Get
            Return _settings.WakeUp
        End Get
        Set
            _settings.WakeUp = Value
        End Set
    End Property
    Public Property HoldTheEdgeMin As Integer Implements ISettings.HoldTheEdgeMin
        Get
            Return _settings.HoldTheEdgeMin
        End Get
        Set
            _settings.HoldTheEdgeMin = Value
        End Set
    End Property
    Public Property HoldTheEdgeMinAmount As String Implements ISettings.HoldTheEdgeMinAmount
        Get
            Return _settings.HoldTheEdgeMinAmount
        End Get
        Set
            _settings.HoldTheEdgeMinAmount = Value
        End Set
    End Property
    Public Property HoldTheEdgeMaxAmount As String Implements ISettings.HoldTheEdgeMaxAmount
        Get
            Return _settings.HoldTheEdgeMaxAmount
        End Get
        Set
            _settings.HoldTheEdgeMaxAmount = Value
        End Set
    End Property
    Public Property MaxPace As Integer Implements ISettings.MaxPace
        Get
            Return _settings.MaxPace
        End Get
        Set
            _settings.MaxPace = Value
        End Set
    End Property
    Public Property MinPace As Integer Implements ISettings.MinPace
        Get
            Return _settings.MinPace
        End Get
        Set
            _settings.MinPace = Value
        End Set
    End Property
    Public Property TypoChance As Integer Implements ISettings.TypoChance
        Get
            Return _settings.TypoChance
        End Get
        Set
            _settings.TypoChance = Value
        End Set
    End Property
    Public Property TBEmote As String Implements ISettings.TBEmote
        Get
            Return _settings.TBEmote
        End Get
        Set
            _settings.TBEmote = Value
        End Set
    End Property
    Public Property TBEmoteEnd As String Implements ISettings.TBEmoteEnd
        Get
            Return _settings.TBEmoteEnd
        End Get
        Set
            _settings.TBEmoteEnd = Value
        End Set
    End Property
    Public Property VVolume As Integer Implements ISettings.VVolume
        Get
            Return _settings.VVolume
        End Get
        Set
            _settings.VVolume = Value
        End Set
    End Property
    Public Property VRate As Integer Implements ISettings.VRate
        Get
            Return _settings.VRate
        End Get
        Set
            _settings.VRate = Value
        End Set
    End Property
    Public Property DomSadistic As Boolean Implements ISettings.DomSadistic
        Get
            Return _settings.DomSadistic
        End Get
        Set
            _settings.DomSadistic = Value
        End Set
    End Property
    Public Property DomDegrading As Boolean Implements ISettings.DomDegrading
        Get
            Return _settings.DomDegrading
        End Get
        Set
            _settings.DomDegrading = Value
        End Set
    End Property
    Public Property MetroOn As Boolean Implements ISettings.MetroOn
        Get
            Return _settings.MetroOn
        End Get
        Set
            _settings.MetroOn = Value
        End Set
    End Property
    Public Property LS1 As String Implements ISettings.LS1
        Get
            Return _settings.LS1
        End Get
        Set
            _settings.LS1 = Value
        End Set
    End Property
    Public Property LS2 As String Implements ISettings.LS2
        Get
            Return _settings.LS2
        End Get
        Set
            _settings.LS2 = Value
        End Set
    End Property
    Public Property LS3 As String Implements ISettings.LS3
        Get
            Return _settings.LS3
        End Get
        Set
            _settings.LS3 = Value
        End Set
    End Property
    Public Property LS4 As String Implements ISettings.LS4
        Get
            Return _settings.LS4
        End Get
        Set
            _settings.LS4 = Value
        End Set
    End Property
    Public Property LS5 As String Implements ISettings.LS5
        Get
            Return _settings.LS5
        End Get
        Set
            _settings.LS5 = Value
        End Set
    End Property
    Public Property LS6 As String Implements ISettings.LS6
        Get
            Return _settings.LS6
        End Get
        Set
            _settings.LS6 = Value
        End Set
    End Property
    Public Property LS7 As String Implements ISettings.LS7
        Get
            Return _settings.LS7
        End Get
        Set
            _settings.LS7 = Value
        End Set
    End Property
    Public Property LS8 As String Implements ISettings.LS8
        Get
            Return _settings.LS8
        End Get
        Set
            _settings.LS8 = Value
        End Set
    End Property
    Public Property LS9 As String Implements ISettings.LS9
        Get
            Return _settings.LS9
        End Get
        Set
            _settings.LS9 = Value
        End Set
    End Property
    Public Property LS10 As String Implements ISettings.LS10
        Get
            Return _settings.LS10
        End Get
        Set
            _settings.LS10 = Value
        End Set
    End Property
    Public Property LongHoldMin As Integer Implements ISettings.LongHoldMin
        Get
            Return _settings.LongHoldMin
        End Get
        Set
            _settings.LongHoldMin = Value
        End Set
    End Property
    Public Property LongHoldMax As String Implements ISettings.LongHoldMax
        Get
            Return _settings.LongHoldMax
        End Get
        Set
            _settings.LongHoldMax = Value
        End Set
    End Property
    Public Property ExtremeHoldMin As String Implements ISettings.ExtremeHoldMin
        Get
            Return _settings.ExtremeHoldMin
        End Get
        Set
            _settings.ExtremeHoldMin = Value
        End Set
    End Property
    Public Property ExtremeHoldMax As String Implements ISettings.ExtremeHoldMax
        Get
            Return _settings.ExtremeHoldMax
        End Get
        Set
            _settings.ExtremeHoldMax = Value
        End Set
    End Property
    Public Property CBWebtease As Boolean Implements ISettings.CBWebtease
        Get
            Return _settings.CBWebtease
        End Get
        Set
            _settings.CBWebtease = Value
        End Set
    End Property
    Public Property SplitterDistance As Integer Implements ISettings.SplitterDistance
        Get
            Return _settings.SplitterDistance
        End Get
        Set
            _settings.SplitterDistance = Value
        End Set
    End Property
    Public Property SideChat As Boolean Implements ISettings.SideChat
        Get
            Return _settings.SideChat
        End Get
        Set
            _settings.SideChat = Value
        End Set
    End Property
    Public Property LazySubAV As Boolean Implements ISettings.LazySubAV
        Get
            Return _settings.LazySubAV
        End Get
        Set
            _settings.LazySubAV = Value
        End Set
    End Property
    Public Property MuteMedia As Boolean Implements ISettings.MuteMedia
        Get
            Return _settings.MuteMedia
        End Get
        Set
            _settings.MuteMedia = Value
        End Set
    End Property
    Public Property OfflineMode As Boolean Implements ISettings.OfflineMode
        Get
            Return _settings.OfflineMode
        End Get
        Set
            _settings.OfflineMode = Value
        End Set
    End Property
    Public Property CBNewSlideshow As Boolean Implements ISettings.CBNewSlideshow
        Get
            Return _settings.CBNewSlideshow
        End Get
        Set
            _settings.CBNewSlideshow = Value
        End Set
    End Property
    Public Property TauntEdging As Integer Implements ISettings.TauntEdging
        Get
            Return _settings.TauntEdging
        End Get
        Set
            _settings.TauntEdging = Value
        End Set
    End Property
    Public Property UrlFileHardcore As String Implements ISettings.UrlFileHardcore
        Get
            Return _settings.UrlFileHardcore
        End Get
        Set
            _settings.UrlFileHardcore = Value
        End Set
    End Property
    Public Property UrlFileSoftcore As String Implements ISettings.UrlFileSoftcore
        Get
            Return _settings.UrlFileSoftcore
        End Get
        Set
            _settings.UrlFileSoftcore = Value
        End Set
    End Property
    Public Property UrlFileLesbian As String Implements ISettings.UrlFileLesbian
        Get
            Return _settings.UrlFileLesbian
        End Get
        Set
            _settings.UrlFileLesbian = Value
        End Set
    End Property
    Public Property UrlFileBlowjob As String Implements ISettings.UrlFileBlowjob
        Get
            Return _settings.UrlFileBlowjob
        End Get
        Set
            _settings.UrlFileBlowjob = Value
        End Set
    End Property
    Public Property UrlFileFemdom As String Implements ISettings.UrlFileFemdom
        Get
            Return _settings.UrlFileFemdom
        End Get
        Set
            _settings.UrlFileFemdom = Value
        End Set
    End Property
    Public Property UrlFileLezdom As String Implements ISettings.UrlFileLezdom
        Get
            Return _settings.UrlFileLezdom
        End Get
        Set
            _settings.UrlFileLezdom = Value
        End Set
    End Property
    Public Property UrlFileHentai As String Implements ISettings.UrlFileHentai
        Get
            Return _settings.UrlFileHentai
        End Get
        Set
            _settings.UrlFileHentai = Value
        End Set
    End Property
    Public Property UrlFileGay As String Implements ISettings.UrlFileGay
        Get
            Return _settings.UrlFileGay
        End Get
        Set
            _settings.UrlFileGay = Value
        End Set
    End Property
    Public Property UrlFileMaledom As String Implements ISettings.UrlFileMaledom
        Get
            Return _settings.UrlFileMaledom
        End Get
        Set
            _settings.UrlFileMaledom = Value
        End Set
    End Property
    Public Property UrlFileCaptions As String Implements ISettings.UrlFileCaptions
        Get
            Return _settings.UrlFileCaptions
        End Get
        Set
            _settings.UrlFileCaptions = Value
        End Set
    End Property
    Public Property UrlFileGeneral As String Implements ISettings.UrlFileGeneral
        Get
            Return _settings.UrlFileGeneral
        End Get
        Set
            _settings.UrlFileGeneral = Value
        End Set
    End Property
    Public Property UrlFileHardcoreEnabled As Boolean Implements ISettings.UrlFileHardcoreEnabled
        Get
            Return _settings.UrlFileHardcoreEnabled
        End Get
        Set
            _settings.UrlFileHardcoreEnabled = Value
        End Set
    End Property
    Public Property UrlFileSoftcoreEnabled As Boolean Implements ISettings.UrlFileSoftcoreEnabled
        Get
            Return _settings.UrlFileSoftcoreEnabled
        End Get
        Set
            _settings.UrlFileSoftcoreEnabled = Value
        End Set
    End Property
    Public Property UrlFileLesbianEnabled As Boolean Implements ISettings.UrlFileLesbianEnabled
        Get
            Return _settings.UrlFileLesbianEnabled
        End Get
        Set
            _settings.UrlFileLesbianEnabled = Value
        End Set
    End Property
    Public Property UrlFileBlowjobEnabled As Boolean Implements ISettings.UrlFileBlowjobEnabled
        Get
            Return _settings.UrlFileBlowjobEnabled
        End Get
        Set
            _settings.UrlFileBlowjobEnabled = Value
        End Set
    End Property
    Public Property UrlFileFemdomEnabled As Boolean Implements ISettings.UrlFileFemdomEnabled
        Get
            Return _settings.UrlFileFemdomEnabled
        End Get
        Set
            _settings.UrlFileFemdomEnabled = Value
        End Set
    End Property
    Public Property UrlFileLezdomEnabled As Boolean Implements ISettings.UrlFileLezdomEnabled
        Get
            Return _settings.UrlFileLezdomEnabled
        End Get
        Set
            _settings.UrlFileLezdomEnabled = Value
        End Set
    End Property
    Public Property UrlFileHentaiEnabled As Boolean Implements ISettings.UrlFileHentaiEnabled
        Get
            Return _settings.UrlFileHentaiEnabled
        End Get
        Set
            _settings.UrlFileHentaiEnabled = Value
        End Set
    End Property
    Public Property UrlFileGayEnabled As Boolean Implements ISettings.UrlFileGayEnabled
        Get
            Return _settings.UrlFileGayEnabled
        End Get
        Set
            _settings.UrlFileGayEnabled = Value
        End Set
    End Property
    Public Property UrlFileMaledomEnabled As Boolean Implements ISettings.UrlFileMaledomEnabled
        Get
            Return _settings.UrlFileMaledomEnabled
        End Get
        Set
            _settings.UrlFileMaledomEnabled = Value
        End Set
    End Property
    Public Property UrlFileCaptionsEnabled As Boolean Implements ISettings.UrlFileCaptionsEnabled
        Get
            Return _settings.UrlFileCaptionsEnabled
        End Get
        Set
            _settings.UrlFileCaptionsEnabled = Value
        End Set
    End Property
    Public Property UrlFileGeneralEnabled As Boolean Implements ISettings.UrlFileGeneralEnabled
        Get
            Return _settings.UrlFileGeneralEnabled
        End Get
        Set
            _settings.UrlFileGeneralEnabled = Value
        End Set
    End Property
    Public Property CBIBoobs As Boolean Implements ISettings.CBIBoobs
        Get
            Return _settings.CBIBoobs
        End Get
        Set
            _settings.CBIBoobs = Value
        End Set
    End Property
    Public Property CBIButts As Boolean Implements ISettings.CBIButts
        Get
            Return _settings.CBIButts
        End Get
        Set
            _settings.CBIButts = Value
        End Set
    End Property
    Public Property UrlFileBoobsEnabled As Boolean Implements ISettings.UrlFileBoobsEnabled
        Get
            Return _settings.UrlFileBoobsEnabled
        End Get
        Set
            _settings.UrlFileBoobsEnabled = Value
        End Set
    End Property
    Public Property UrlFileButtEnabled As Boolean Implements ISettings.UrlFileButtEnabled
        Get
            Return _settings.UrlFileButtEnabled
        End Get
        Set
            _settings.UrlFileButtEnabled = Value
        End Set
    End Property
    Public Property CBURLPreview As Boolean Implements ISettings.CBURLPreview
        Get
            Return _settings.CBURLPreview
        End Get
        Set
            _settings.CBURLPreview = Value
        End Set
    End Property
    Public Property TaskStrokesMin As Decimal Implements ISettings.TaskStrokesMin
        Get
            Return _settings.TaskStrokesMin
        End Get
        Set
            _settings.TaskStrokesMin = Value
        End Set
    End Property
    Public Property TaskStrokesMax As Decimal Implements ISettings.TaskStrokesMax
        Get
            Return _settings.TaskStrokesMax
        End Get
        Set
            _settings.TaskStrokesMax = Value
        End Set
    End Property
    Public Property TaskStrokingTimeMin As Decimal Implements ISettings.TaskStrokingTimeMin
        Get
            Return _settings.TaskStrokingTimeMin
        End Get
        Set
            _settings.TaskStrokingTimeMin = Value
        End Set
    End Property
    Public Property TaskStrokingTimeMax As Decimal Implements ISettings.TaskStrokingTimeMax
        Get
            Return _settings.TaskStrokingTimeMax
        End Get
        Set
            _settings.TaskStrokingTimeMax = Value
        End Set
    End Property
    Public Property TaskEdgesMin As Decimal Implements ISettings.TaskEdgesMin
        Get
            Return _settings.TaskEdgesMin
        End Get
        Set
            _settings.TaskEdgesMin = Value
        End Set
    End Property
    Public Property TaskEdgesMax As Decimal Implements ISettings.TaskEdgesMax
        Get
            Return _settings.TaskEdgesMax
        End Get
        Set
            _settings.TaskEdgesMax = Value
        End Set
    End Property
    Public Property TaskEdgeHoldTimeMin As Decimal Implements ISettings.TaskEdgeHoldTimeMin
        Get
            Return _settings.TaskEdgeHoldTimeMin
        End Get
        Set
            _settings.TaskEdgeHoldTimeMin = Value
        End Set
    End Property
    Public Property TaskEdgeHoldTimeMax As Decimal Implements ISettings.TaskEdgeHoldTimeMax
        Get
            Return _settings.TaskEdgeHoldTimeMax
        End Get
        Set
            _settings.TaskEdgeHoldTimeMax = Value
        End Set
    End Property
    Public Property TaskCBTTimeMin As Decimal Implements ISettings.TaskCBTTimeMin
        Get
            Return _settings.TaskCBTTimeMin
        End Get
        Set
            _settings.TaskCBTTimeMin = Value
        End Set
    End Property
    Public Property TaskCBTTimeMax As Decimal Implements ISettings.TaskCBTTimeMax
        Get
            Return _settings.TaskCBTTimeMax
        End Get
        Set
            _settings.TaskCBTTimeMax = Value
        End Set
    End Property
    Public Property TasksMin As String Implements ISettings.TasksMin
        Get
            Return _settings.TasksMin
        End Get
        Set
            _settings.TasksMin = Value
        End Set
    End Property
    Public Property TasksMax As String Implements ISettings.TasksMax
        Get
            Return _settings.TasksMax
        End Get
        Set
            _settings.TasksMax = Value
        End Set
    End Property
    Public Property WritingProgress As Boolean Implements ISettings.WritingProgress
        Get
            Return _settings.WritingProgress
        End Get
        Set
            _settings.WritingProgress = Value
        End Set
    End Property
    Public Property TimedWriting As Boolean Implements ISettings.TimedWriting
        Get
            Return _settings.TimedWriting
        End Get
        Set
            _settings.TimedWriting = Value
        End Set
    End Property
    Public Property TypeSpeed As Integer Implements ISettings.TypeSpeed
        Get
            Return _settings.TypeSpeed
        End Get
        Set
            _settings.TypeSpeed = Value
        End Set
    End Property
    Public Property RecentSlideshows As StringCollection Implements ISettings.RecentSlideshows
        Get
            Return _settings.RecentSlideshows
        End Get
        Set
            _settings.RecentSlideshows = Value
        End Set
    End Property
    Public Property LockOrgasmChances As Boolean Implements ISettings.LockOrgasmChances
        Get
            Return _settings.LockOrgasmChances
        End Get
        Set
            _settings.LockOrgasmChances = Value
        End Set
    End Property
    Public Property MaximizeMediaWindow As Boolean Implements ISettings.MaximizeMediaWindow
        Get
            Return _settings.MaximizeMediaWindow
        End Get
        Set
            _settings.MaximizeMediaWindow = Value
        End Set
    End Property
    Public Property DisplaySidePanel As Boolean Implements ISettings.DisplaySidePanel
        Get
            Return _settings.DisplaySidePanel
        End Get
        Set
            _settings.DisplaySidePanel = Value
        End Set
    End Property
    Public Property DomCFNM As Boolean Implements ISettings.DomCFNM
        Get
            Return _settings.DomCFNM
        End Get
        Set
            _settings.DomCFNM = Value
        End Set
    End Property
    Public Property GiveUpReturn As Boolean Implements ISettings.GiveUpReturn
        Get
            Return _settings.GiveUpReturn
        End Get
        Set
            _settings.GiveUpReturn = Value
        End Set
    End Property
    Public Property RandomImageDir As String Implements ISettings.RandomImageDir
        Get
            Return _settings.RandomImageDir
        End Get
        Set
            _settings.RandomImageDir = Value
        End Set
    End Property
    Public Property CBRandomDomme As Boolean Implements ISettings.CBRandomDomme
        Get
            Return _settings.CBRandomDomme
        End Get
        Set
            _settings.CBRandomDomme = Value
        End Set
    End Property
    Public Property CBOutputErrors As Boolean Implements ISettings.CBOutputErrors
        Get
            Return _settings.CBOutputErrors
        End Get
        Set
            _settings.CBOutputErrors = Value
        End Set
    End Property
    Public Property G1Honorific As String Implements ISettings.G1Honorific
        Get
            Return _settings.G1Honorific
        End Get
        Set
            _settings.G1Honorific = Value
        End Set
    End Property
    Public Property G2Honorific As String Implements ISettings.G2Honorific
        Get
            Return _settings.G2Honorific
        End Get
        Set
            _settings.G2Honorific = Value
        End Set
    End Property
    Public Property G3Honorific As String Implements ISettings.G3Honorific
        Get
            Return _settings.G3Honorific
        End Get
        Set
            _settings.G3Honorific = Value
        End Set
    End Property
    Public Property RandomHonorific As String Implements ISettings.RandomHonorific
        Get
            Return _settings.RandomHonorific
        End Get
        Set
            _settings.RandomHonorific = Value
        End Set
    End Property
    Public Property SubSorry As String Implements ISettings.SubSorry
        Get
            Return _settings.SubSorry
        End Get
        Set
            _settings.SubSorry = Value
        End Set
    End Property
    Public Property AlwaysNewSlideshow As Boolean Implements ISettings.AlwaysNewSlideshow
        Get
            Return _settings.AlwaysNewSlideshow
        End Get
        Set
            _settings.AlwaysNewSlideshow = Value
        End Set
    End Property
    Public Property CbChatDisplayWarnings As Boolean Implements ISettings.CbChatDisplayWarnings
        Get
            Return _settings.CbChatDisplayWarnings
        End Get
        Set
            _settings.CbChatDisplayWarnings = Value
        End Set
    End Property
    Public Property DomImageDirRand As String Implements ISettings.DomImageDirRand
        Get
            Return _settings.DomImageDirRand
        End Get
        Set
            _settings.DomImageDirRand = Value
        End Set
    End Property
    Public Property CBAutoDomPP As Boolean Implements ISettings.CBAutoDomPP
        Get
            Return _settings.CBAutoDomPP
        End Get
        Set
            _settings.CBAutoDomPP = Value
        End Set
    End Property
    Public Property CBRandomGlitter As Boolean Implements ISettings.CBRandomGlitter
        Get
            Return _settings.CBRandomGlitter
        End Get
        Set
            _settings.CBRandomGlitter = Value
        End Set
    End Property
    Public Property SplitterPosition As Integer Implements ISettings.SplitterPosition
        Get
            Return _settings.SplitterPosition
        End Get
        Set
            _settings.SplitterPosition = Value
        End Set
    End Property
End Class
