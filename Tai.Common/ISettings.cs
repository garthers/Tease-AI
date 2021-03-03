﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.Specialized;

namespace Tai.Common
{
    public interface ISettings
    {
        string VideoHardcore { get; set; }
        string VideoSoftcore { get; set; }
        string VideoLesbian { get; set; }
        string VideoBlowjob { get; set; }
        string VideoFemdom { get; set; }
        string VideoFemsub { get; set; }
        string VideoJOI { get; set; }
        string VideoCH { get; set; }
        string VideoGeneral { get; set; }
        bool CBHardcore { get; set; }
        bool CBSoftcore { get; set; }
        bool CBLesbian { get; set; }
        bool CBBlowjob { get; set; }
        bool CBFemdom { get; set; }
        bool CBFemsub { get; set; }
        bool CBJOI { get; set; }
        bool CBCH { get; set; }
        bool CBGeneral { get; set; }
        string DomAvatarSave { get; set; }
        string SubAvatarSave { get; set; }
        decimal NBCensorShowMin { get; set; }
        decimal NBCensorShowMax { get; set; }
        decimal NBCensorHideMin { get; set; }
        decimal NBCensorHideMax { get; set; }
        bool CBCensorConstant { get; set; }
        bool CBHardcoreD { get; set; }
        bool CBSoftcoreD { get; set; }
        bool CBLesbianD { get; set; }
        bool CBBlowjobD { get; set; }
        bool CBFemsubD { get; set; }
        bool CBJOID { get; set; }
        bool CBCHD { get; set; }
        bool CBGeneralD { get; set; }
        bool CBFemdomD { get; set; }
        string VideoHardcoreD { get; set; }
        string VideoSoftcoreD { get; set; }
        string VideoLesbianD { get; set; }
        string VideoBlowjobD { get; set; }
        string VideoFemdomD { get; set; }
        string VideoFemsubD { get; set; }
        string VideoJOID { get; set; }
        string VideoCHD { get; set; }
        string VideoGeneralD { get; set; }
        string GlitterAV { get; set; }
        string GlitterAV1 { get; set; }
        string GlitterAV2 { get; set; }
        string GlitterAV3 { get; set; }
        string GlitterSN { get; set; }
        string Glitter1 { get; set; }
        string Glitter2 { get; set; }
        string Glitter3 { get; set; }
        Color GlitterNCDommeColor { get; set; }
        Color GlitterNC1Color { get; set; }
        Color GlitterNC2Color { get; set; }
        Color GlitterNC3Color { get; set; }
        int GlitterDSlider { get; set; }
        int Glitter1Slider { get; set; }
        int Glitter2Slider { get; set; }
        int Glitter3Slider { get; set; }
        bool CBGlitterFeed { get; set; }
        bool CBGlitter1 { get; set; }
        bool CBGlitter2 { get; set; }
        bool CBGlitter3 { get; set; }
        bool CBTease { get; set; }
        bool CBEgotist { get; set; }
        bool CBTrivia { get; set; }
        bool CBDaily { get; set; }
        bool CBCustom1 { get; set; }
        bool CBCustom2 { get; set; }
        bool CB1Bratty { get; set; }
        bool CB1Cruel { get; set; }
        bool CB1Caring { get; set; }
        bool CB1Angry { get; set; }
        bool CB1Custom1 { get; set; }
        bool CB1Custom2 { get; set; }
        bool CB2Bratty { get; set; }
        bool CB2Cruel { get; set; }
        bool CB2Caring { get; set; }
        bool CB2Angry { get; set; }
        bool CB2Custom1 { get; set; }
        bool CB2Custom2 { get; set; }
        bool CB3Bratty { get; set; }
        bool CB3Cruel { get; set; }
        bool CB3Caring { get; set; }
        bool CB3Angry { get; set; }
        bool CB3Custom1 { get; set; }
        bool CB3Custom2 { get; set; }
        string DomName { get; set; }
        string SubName { get; set; }
        string pnSetting1 { get; set; }
        string pnSetting2 { get; set; }
        string pnSetting3 { get; set; }
        string pnSetting4 { get; set; }
        string pnSetting5 { get; set; }
        string pnSetting6 { get; set; }
        string pnSetting7 { get; set; }
        string pnSetting8 { get; set; }
        string DomColor { get; set; }
        string SubColor { get; set; }
        Color DomColorColor { get; set; }
        Color SubColorColor { get; set; }
        bool CBTimeStamps { get; set; }
        bool CBShowNames { get; set; }
        bool CBInstantType { get; set; }
        bool CBBlogImageMain { get; set; }
        bool CBStretchLandscape { get; set; }
        bool CBMetronome { get; set; }
        bool CBSettingsPause { get; set; }
        int DomLevel { get; set; }
        string OrgasmAllow { get; set; }
        string OrgasmRuin { get; set; }
        bool CBAutosaveChatlog { get; set; }
        bool CBExitSaveChatlog { get; set; }
        int AvgEdgeStroking { get; set; }
        int AvgEdgeNoTouch { get; set; }
        int AvgEdgeCount { get; set; }
        int StrokeTimeTotal { get; set; }
        int NBWritingTaskMin { get; set; }
        int NBWritingTaskMax { get; set; }
        string LBLBoobPath { get; set; }
        string UrlFileBoobs { get; set; }
        string LBLButtPath { get; set; }
        string UrlFileButt { get; set; }
        bool CBBnBLocal { get; set; }
        bool CBBnBURL { get; set; }
        bool CBBoobSubDir { get; set; }
        bool CBButtSubDir { get; set; }
        bool CBEnableBnB { get; set; }
        bool CBSlideshowSubDir { get; set; }
        bool CBSlideshowRandom { get; set; }
        int DomAge { get; set; }
        string DomHair { get; set; }
        string DomEyes { get; set; }
        string DomCup { get; set; }
        string DomPersonality { get; set; }
        bool DomCrazy { get; set; }
        bool DomVulgar { get; set; }
        bool DomSupremacist { get; set; }
        bool DomLowercase { get; set; }
        bool DomNoApostrophes { get; set; }
        bool DomNoCommas { get; set; }
        bool DomNoPeriods { get; set; }
        bool DomMeMyMine { get; set; }
        string DomEmotes { get; set; }
        bool DomDenialEnd { get; set; }
        bool DomOrgasmEnd { get; set; }
        bool DomPOT { get; set; }
        bool DomLimit { get; set; }
        int DomOrgasmPer { get; set; }
        string DomPerMonth { get; set; }
        bool DomLock { get; set; }
        int DomMoodMin { get; set; }
        int DomMoodMax { get; set; }
        int AvgCockMin { get; set; }
        int AvgCockMax { get; set; }
        int SelfAgeMin { get; set; }
        int SelfAgeMax { get; set; }
        int SubAgeMin { get; set; }
        string SubAgeMax { get; set; }
        bool CBLockWindow { get; set; }
        string SubGreeting { get; set; }
        string SubYes { get; set; }
        string SubNo { get; set; }
        string SubHonorific { get; set; }
        bool CBUseHonor { get; set; }
        bool CBUseName { get; set; }
        bool CBCapHonor { get; set; }
        int SubCockSize { get; set; }
        int SubAge { get; set; }
        bool TCAgreed { get; set; }
        int TimerSTF { get; set; }
        int SubBirthMonth { get; set; }
        int SubBirthDay { get; set; }
        string SubHair { get; set; }
        string SubEyes { get; set; }
        int DomFontSize { get; set; }
        string DomFont { get; set; }
        string SubFont { get; set; }
        int SubFontSize { get; set; }
        int DomBirthMonth { get; set; }
        int DomBirthDay { get; set; }
        string DomHairLength { get; set; }
        string DomPubicHair { get; set; }
        bool DomTattoos { get; set; }
        bool DomFreckles { get; set; }
        bool CBImageInfo { get; set; }
        bool DomAVStretch { get; set; }
        bool SubAvStretch { get; set; }
        string IHardcore { get; set; }
        string ISoftcore { get; set; }
        string ILesbian { get; set; }
        string IBlowjob { get; set; }
        string IFemdom { get; set; }
        string ILezdom { get; set; }
        string IHentai { get; set; }
        string IGay { get; set; }
        string IMaledom { get; set; }
        string IGeneral { get; set; }
        bool IHardcoreSD { get; set; }
        bool ISoftcoreSD { get; set; }
        bool ILesbianSD { get; set; }
        bool IBlowjobSD { get; set; }
        bool IFemdomSD { get; set; }
        bool ILezdomSD { get; set; }
        bool IHentaiSD { get; set; }
        bool IGaySD { get; set; }
        bool IMaledomSD { get; set; }
        bool IGeneralSD { get; set; }
        bool ICaptionsSD { get; set; }
        bool CBIHardcore { get; set; }
        bool CBISoftcore { get; set; }
        bool CBILesbian { get; set; }
        bool CBIBlowjob { get; set; }
        bool CBIFemdom { get; set; }
        bool CBILezdom { get; set; }
        bool CBIHentai { get; set; }
        bool CBIGay { get; set; }
        bool CBIMaledom { get; set; }
        bool CBIGeneral { get; set; }
        bool CBICaptions { get; set; }
        string ICaptions { get; set; }
        string DomImageDir { get; set; }
        bool CBTCock { get; set; }
        bool CBTBalls { get; set; }
        bool ChastityPA { get; set; }
        bool ChastitySpikes { get; set; }
        bool SubInChastity { get; set; }
        int LongEdge { get; set; }
        bool CBLongEdgeInterrupt { get; set; }
        int HoldTheEdgeMax { get; set; }
        int HoldEdgeTimeTotal { get; set; }
        int CBTSlider { get; set; }
        bool SubCircumcised { get; set; }
        bool SubPierced { get; set; }
        int DomEmpathy { get; set; }
        bool RangeOrgasm { get; set; }
        bool RangeRuin { get; set; }
        int AllowOften { get; set; }
        int AllowSometimes { get; set; }
        int AllowRarely { get; set; }
        int RuinOften { get; set; }
        int RuinSometimes { get; set; }
        int RuinRarely { get; set; }
        bool Chastity { get; set; }
        string Safeword { get; set; }
        int CaloriesConsumed { get; set; }
        int CaloriesGoal { get; set; }
        bool VitalSubDisclaimer { get; set; }
        bool VitalSub { get; set; }
        bool VitalSubAssignments { get; set; }
        string BP1 { get; set; }
        string BP2 { get; set; }
        string BP3 { get; set; }
        string BP4 { get; set; }
        string BP5 { get; set; }
        string BP6 { get; set; }
        string BN1 { get; set; }
        string BN2 { get; set; }
        string BN3 { get; set; }
        string BN4 { get; set; }
        string BN5 { get; set; }
        string BN6 { get; set; }
        string SP1 { get; set; }
        string SP2 { get; set; }
        string SP3 { get; set; }
        string SP4 { get; set; }
        string SP5 { get; set; }
        string SP6 { get; set; }
        string SN1 { get; set; }
        string SN2 { get; set; }
        string SN3 { get; set; }
        string SN4 { get; set; }
        string SN5 { get; set; }
        string SN6 { get; set; }
        string GP1 { get; set; }
        string GP2 { get; set; }
        string GP3 { get; set; }
        string GP4 { get; set; }
        string GP5 { get; set; }
        string GP6 { get; set; }
        string GN1 { get; set; }
        string GN2 { get; set; }
        string GN3 { get; set; }
        string GN4 { get; set; }
        string GN5 { get; set; }
        string GN6 { get; set; }
        string CardBack { get; set; }
        int GoldTokens { get; set; }
        int SilverTokens { get; set; }
        int BronzeTokens { get; set; }
        int B1 { get; set; }
        int B2 { get; set; }
        int B3 { get; set; }
        int B4 { get; set; }
        int B5 { get; set; }
        int B6 { get; set; }
        int S1 { get; set; }
        int S2 { get; set; }
        int S3 { get; set; }
        int S4 { get; set; }
        int S5 { get; set; }
        int S6 { get; set; }
        int G1 { get; set; }
        int G2 { get; set; }
        int G3 { get; set; }
        int G4 { get; set; }
        int G5 { get; set; }
        int G6 { get; set; }
        bool ClearWishlist { get; set; }
        string WishlistName { get; set; }
        string WishlistPreview { get; set; }
        string WishlistTokenType { get; set; }
        int WishlistCost { get; set; }
        string WishlistNote { get; set; }
        int AvgEdgeCountRest { get; set; }
        int PersonalityIndex { get; set; }
        bool LargeUI { get; set; }
        bool CBJackintheBox { get; set; }
        int NextImageChance { get; set; }
        bool CBEdgeUseAvg { get; set; }
        bool CBLongEdgeTaunts { get; set; }
        bool CBLongEdgeInterrupts { get; set; }
        int OrgasmsRemaining { get; set; }
        bool OrgasmsLocked { get; set; }
        int TimerVTF { get; set; }
        bool CBTeaseLengthDD { get; set; }
        bool CBTauntCycleDD { get; set; }
        bool CBOwnChastity { get; set; }
        bool SmallUI { get; set; }
        bool UI768 { get; set; }
        bool CBIncludeGifs { get; set; }
        bool CBHimHer { get; set; }
        bool DomDeleteMedia { get; set; }
        int TeaseLengthMin { get; set; }
        int TeaseLengthMax { get; set; }
        int TauntCycleMin { get; set; }
        int TauntCycleMax { get; set; }
        int RedLightMin { get; set; }
        int RedLightMax { get; set; }
        int GreenLightMin { get; set; }
        int GreenLightMax { get; set; }
        string SlideshowMode { get; set; }
        DateTime OrgasmLockDate { get; set; }
        bool AuditStartup { get; set; }
        DateTime WishlistDate { get; set; }
        DateTime LastOrgasm { get; set; }
        DateTime LastRuined { get; set; }
        DateTime DateStamp { get; set; }
        DateTime TokenTasks { get; set; }
        string WebToyStart { get; set; }
        string WebToyStop { get; set; }
        bool CockToClit { get; set; }
        bool BallsToPussy { get; set; }
        string Contact1ImageDir { get; set; }
        string Contact2ImageDir { get; set; }
        string Contact3ImageDir { get; set; }
        bool CBGlitterFeedOff { get; set; }
        bool CBGlitterFeedScripts { get; set; }
        string TeaseAILanguage { get; set; }
        bool Shortcuts { get; set; }
        bool ShowShortcuts { get; set; }
        string ShortYes { get; set; }
        string ShortNo { get; set; }
        string ShortEdge { get; set; }
        string ShortSpeedUp { get; set; }
        string ShortSlowDown { get; set; }
        string ShortStop { get; set; }
        string ShortStroke { get; set; }
        string ShortCum { get; set; }
        string ShortGreet { get; set; }
        string ShortSafeword { get; set; }
        bool Patch45Tokens { get; set; }
        int WindowHeight { get; set; }
        int WindowWidth { get; set; }
        string UIColor { get; set; }
        bool TC2Agreed { get; set; }
        string LastDomTagURL { get; set; }
        int Sys_SubLeftEarly { get; set; }
        int Sys_SubLeftEarlyTotal { get; set; }
        bool AIBoxDir { get; set; }
        bool AIBoxOpen { get; set; }
        Color BackgroundColor { get; set; }
        string BackgroundImage { get; set; }
        Color ButtonColor { get; set; }
        Color TextColor { get; set; }
        Color ChatWindowColor { get; set; }
        Color ChatTextColor { get; set; }
        bool BackgroundStretch { get; set; }
        bool CBInputIcon { get; set; }
        Color DateTextColor { get; set; }
        Color DateBackColor { get; set; }
        bool CBDateTransparent { get; set; }
        bool MirrorWindows { get; set; }
        DateTime WakeUp { get; set; }
        int HoldTheEdgeMin { get; set; }
        string HoldTheEdgeMinAmount { get; set; }
        string HoldTheEdgeMaxAmount { get; set; }
        int MaxPace { get; set; }
        int MinPace { get; set; }
        int TypoChance { get; set; }
        string TBEmote { get; set; }
        string TBEmoteEnd { get; set; }
        int VVolume { get; set; }
        int VRate { get; set; }
        bool DomSadistic { get; set; }
        bool DomDegrading { get; set; }
        bool MetroOn { get; set; }
        string LS1 { get; set; }
        string LS2 { get; set; }
        string LS3 { get; set; }
        string LS4 { get; set; }
        string LS5 { get; set; }
        string LS6 { get; set; }
        string LS7 { get; set; }
        string LS8 { get; set; }
        string LS9 { get; set; }
        string LS10 { get; set; }
        int LongHoldMin { get; set; }
        string LongHoldMax { get; set; }
        string ExtremeHoldMin { get; set; }
        string ExtremeHoldMax { get; set; }
        bool CBWebtease { get; set; }
        int SplitterDistance { get; set; }
        bool SideChat { get; set; }
        bool LazySubAV { get; set; }
        bool MuteMedia { get; set; }
        bool OfflineMode { get; set; }
        bool CBNewSlideshow { get; set; }
        int TauntEdging { get; set; }
        string UrlFileHardcore { get; set; }
        string UrlFileSoftcore { get; set; }
        string UrlFileLesbian { get; set; }
        string UrlFileBlowjob { get; set; }
        string UrlFileFemdom { get; set; }
        string UrlFileLezdom { get; set; }
        string UrlFileHentai { get; set; }
        string UrlFileGay { get; set; }
        string UrlFileMaledom { get; set; }
        string UrlFileCaptions { get; set; }
        string UrlFileGeneral { get; set; }
        bool UrlFileHardcoreEnabled { get; set; }
        bool UrlFileSoftcoreEnabled { get; set; }
        bool UrlFileLesbianEnabled { get; set; }
        bool UrlFileBlowjobEnabled { get; set; }
        bool UrlFileFemdomEnabled { get; set; }
        bool UrlFileLezdomEnabled { get; set; }
        bool UrlFileHentaiEnabled { get; set; }
        bool UrlFileGayEnabled { get; set; }
        bool UrlFileMaledomEnabled { get; set; }
        bool UrlFileCaptionsEnabled { get; set; }
        bool UrlFileGeneralEnabled { get; set; }
        bool CBIBoobs { get; set; }
        bool CBIButts { get; set; }
        bool UrlFileBoobsEnabled { get; set; }
        bool UrlFileButtEnabled { get; set; }
        bool CBURLPreview { get; set; }
        decimal TaskStrokesMin { get; set; }
        decimal TaskStrokesMax { get; set; }
        decimal TaskStrokingTimeMin { get; set; }
        decimal TaskStrokingTimeMax { get; set; }
        decimal TaskEdgesMin { get; set; }
        decimal TaskEdgesMax { get; set; }
        decimal TaskEdgeHoldTimeMin { get; set; }
        decimal TaskEdgeHoldTimeMax { get; set; }
        decimal TaskCBTTimeMin { get; set; }
        decimal TaskCBTTimeMax { get; set; }
        string TasksMin { get; set; }
        string TasksMax { get; set; }
        bool WritingProgress { get; set; }
        bool TimedWriting { get; set; }
        int TypeSpeed { get; set; }
        StringCollection RecentSlideshows { get; set; }
        bool LockOrgasmChances { get; set; }
        bool MaximizeMediaWindow { get; set; }
        bool DisplaySidePanel { get; set; }
        bool DomCFNM { get; set; }
        bool GiveUpReturn { get; set; }
        string RandomImageDir { get; set; }
        bool CBRandomDomme { get; set; }
        bool CBOutputErrors { get; set; }
        string G1Honorific { get; set; }
        string G2Honorific { get; set; }
        string G3Honorific { get; set; }
        string RandomHonorific { get; set; }
        string SubSorry { get; set; }
        bool AlwaysNewSlideshow { get; set; }
        bool CbChatDisplayWarnings { get; set; }
        string DomImageDirRand { get; set; }
        bool CBAutoDomPP { get; set; }
        bool CBRandomGlitter { get; set; }
        int SplitterPosition { get; set; }
    }
}


