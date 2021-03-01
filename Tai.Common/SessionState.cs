// ===========================================================================================
// 
// SessionState-Class
// 
// 
// This File contains a Class to store all nessesary informations about a session.
// 
// This Class is Version-Tolerant Serializable!
// 
// Please take a look at: https://msdn.microsoft.com/en-us/library/ms229752(v=vs.110).aspx
// ===========================================================================================
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Collections;

namespace Tai.Common
{
    /// <summary>fake class</summary>
    public class MyUITypeEditor
    {

    }


    /// <summary>
    /// ''' Class to store/serialize and deserialize all nessecary session(!) informations.
    /// ''' </summary>
    /// ''' <remarks>
    /// ''' After loading from file, the object needs to be set and activated!  
    /// ''' <para></para>
    /// ''' To ensure compatiblity between versions, new added fields have to be marked as 
    /// ''' <see cref="OptionalFieldAttribute"/>. The inital value if an old version is loaded, 
    /// ''' without that field, has to be set in <see cref="SessionState.onDeserialized_FixFields(StreamingContext)"/>.
    /// ''' For more inforations take a look at: https://msdn.microsoft.com/en-us/library/ms733734(v=vs.110).aspx
    /// ''' </remarks>
    /// 
    [Serializable]
    public partial class SessionState
    {
        const string EditorGenericStringList = "System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

        /// <summary> The oldest compatible version of a SessinState. </summary>
        [NonSerialized]
        const string MINVERSION = "0.56.0.0";

        /// <summary>Contains the Tease-AI Version of this session.</summary>
        [Category("Program-Info")]
        [ReadOnly(true)]
        [Description("Contains the Tease-AI Version of this session.")]
        public string Version { get => Assembly.GetExecutingAssembly().GetName().Version.ToString(); }


        public string DomPersonality { get; set; }

        public string Chat { get; set; }

        public Random randomizer { get; set; } = new Random();
        public string ScriptOperator { get; set; }

        public bool DomTyping { get; set; }

        public bool CheckYes { get; set; }
        public bool CheckNo { get; set; }

        public bool Playlist { get; set; }

        [Editor(EditorGenericStringList, typeof(MyUITypeEditor))]
        public List<string> PlaylistFile { get; set; } = new List<string>();
        public int PlaylistCurrent { get; set; }


        public int TeaseTick { get; set; }
        public bool Responding { get; set; }

        public int StrokeTauntVal { get; set; } = -1;
        public bool newSlideshow { get; set; } = false;
        [Category("Video")]
        public int TempStrokeTauntVal { get; set; }
        [Category("Video")]
        public string TempFileText { get; set; }

        /// <summary>Gets or sets the current TauntFile path.</summary>
        [Category("Taunts")]
        [Description("Path of current Taunt-file")]
        public string TauntText { get; set; }

        /// <summary>Gets or sets the length of a taunt group.</summary>
        [Category("Taunts")]
        [Description("Current line group size.")]
        public int StrokeTauntCount { get; set; }

        /// <summary>Gets or sets the current taunt lines. </summary>
        [Category("Taunts")]
        [Description("Current taunt lines.")]
        [Editor(EditorGenericStringList, typeof(MyUITypeEditor))]
        public List<string> TauntLines { get; set; } = new List<string>();

        [Category("Taunts")]
        [ReadOnly(true)]
        [Description("Indicates if taunt filtering is active")]
        public bool StrokeFilter { get; set; }

        /// <summary> Gets or Sets the current taunt line index. </summary>
        [Category("Taunts")]
        [Description("The current taunt line index.")]
        public int TauntTextCount { get; set; }


        [Category("Script")]
        public string FileText { get; set; }
        [Category("Script")]
        public int ScriptTick { get; set; }
        [Category("Script")]
        public int StringLength { get; set; }
        [Category("Script")]
        public string FileGoto { get; set; }
        [Category("Script")]
        public bool SkipGotoLine { get; set; }

        public string ChatString { get; set; }
        [Description("Used for regular talk.")]
        public string DomTask { get; set; } = "Null";
        [Description("Used for responses.")]
        public string DomChat { get; set; } = "Null";
        public int TypeDelay { get; set; }
        public int TempVal { get; set; }
        public bool NullResponse { get; set; }

        public string TaskFile { get; set; }
        public string TaskText { get; set; }
        public string TaskTextDir { get; set; }

        public int tempResponseLine { get; set; }
        public int nameErrors { get; set; } = 0;
        public bool wrongAttempt { get; set; }

        public string ResponseFile { get; set; }
        public int ResponseLine { get; set; }


        [Category("CBT")]
        public bool CBTBothActive { get; set; }
        [Category("CBT")]
        public bool CBTBothFlag { get; set; }
        [Category("CBT")]
        public bool CBTBothFirst { get; set; } = true;

        [Category("CBT")]
        public bool CBTCockActive { get; set; }
        [Category("CBT")]
        public bool CBTBallsActive { get; set; }

        [Category("CBT")]
        public bool CBTCockFlag { get; set; }
        [Category("CBT")]
        public bool CBTBallsFlag { get; set; }

        [Category("CBT")]
        public bool CBTBallsFirst { get; set; } = true;
        [Category("CBT")]
        public bool CBTCockFirst { get; set; } = true;

        [Category("CBT")]
        public int CBTBallsCount { get; set; }
        [Category("CBT")]
        public int CBTCockCount { get; set; }

        public int TasksCount { get; set; } = 0;

        public bool GotoDommeLevel { get; set; }

        public int DommeMood { get; set; } = -1;

        public bool AFK { get; set; }


        public int glitterDommeNumber { get; set; } = 0;
        public bool HypnoGen { get; set; }
        public bool Induction { get; set; }
        public string TempHypno { get; set; }

        public int StrokeTick { get; set; }
        /// <summary> Gets or sets time until next taunt.</summary>
        [Category("Taunts")]
        [Description("Time until next taunt")]
        public int StrokeTauntTick { get; set; }



        public int StrokeTimeTotal { get; set; }
        public int HoldEdgeTime { get; set; }
        public int HoldEdgeTimeTotal { get; set; }

        public int EdgeTauntInt { get; set; }

        public bool DomTypeCheck { get; set; }
        public bool TypeToggle { get; set; }
        public bool IsTyping { get; set; }
        public bool SubWroteLast { get; set; } = false;
        [Description("True if sub has been asked a Yes/No Question.")]
        public bool YesOrNo { get; set; } = false;
        public bool GotoFlag { get; set; }

        /// <summary>Gets or Sets if a taunt demanded CBT.</summary>
        [Category("Taunts")]
        [Description(@"Indicates if a taunt demanded CBT. CBT-Tasks are taken from ""[Personality Path]\CBT\CBT.txt"". The calling line in Taunt-file is completly replaced.")]
        public bool CBT { get; set; }

        public bool RunningScript { get; set; }

        public bool BeforeTease { get; set; }
        public bool SubStroking { get; set; } = false;
        public bool SubEdging { get; set; } = false;
        public bool SubHoldingEdge { get; set; } = false;
        public bool EndTease { get; set; }

        public bool ShowModule { get; set; } = false;
        public bool ModuleEnd { get; set; }

        public bool giveUpReturn { get; set; }
        public ContactData contactToUse { get; set; }
        public List<string> currentlyPresentContacts { get; set; } = new List<string>();
        public bool DivideText { get; set; }

        public int HoldEdgeTick { get; set; }
        public int HoldEdgeChance { get; set; }

        public bool EdgeHold { get; set; }
        public bool EdgeNoHold { get; set; }
        public bool EdgeToRuin { get; set; }
        public bool EdgeToRuinSecret { get; set; } = true;
        public bool LongEdge { get; set; }

        [Obsolete("Set to true but never used.")]
        public bool AskedToGiveUp { get; set; }
        public bool AskedToGiveUpSection { get; set; }
        public bool SubGaveUp { get; set; }
        public bool AskedToSpeedUp { get; set; } = false;
        public bool AskedToSlowDown { get; set; } = false;


        [Category("Video")]
        public bool LockVideo { get; set; }
        [Category("Video")]
        public bool DommeVideo { get; set; }
        [Category("Video")]
        public bool JumpVideo { get; set; }
        [Category("Video")]
        public bool NoSpecialVideo { get; set; }
        [Category("Video")]
        public bool RandomizerVideo { get; set; }
        [Category("Video")]
        public bool RandomizerVideoTease { get; set; }
        [Category("Video")]
        public string ScriptVideoTease { get; set; }
        [Category("Video")]
        public bool ScriptVideoTeaseFlag { get; set; }
        [Category("Video")]
        public bool VideoCheck { get; set; }
        [Category("Video")]
        public int VideoTauntTick { get; set; }
        [Category("Video")]
        public bool VideoTease { get; set; }
        [Category("Video")]
        public int VideoTick { get; set; }
        [Category("Video")]
        public string VideoType { get; set; } = "General";
        [Category("Video")]
        public string VidFile { get; set; }
        [Category("Video")]
        public string VTPath { get; set; }
        [Category("Video")]
        [Obsolete("Used in #VTLenth but delivers a wrong value.")]
        public int VTLength { get; set; }
        [Category("Video - Avoid the Edge")]
        public int AtECountdown { get; set; }
        [Category("Video - Avoid the Edge")]
        public bool AvoidTheEdgeGame { get; set; }
        [Category("Video - Avoid the Edge")]
        public bool AvoidTheEdgeStroking { get; set; }
        [Category("Video - Avoid the Edge")]
        public int AvoidTheEdgeTick { get; set; }
        [Category("Video - Censorship")]
        public bool CensorshipGame { get; set; }
        [Category("Video - Censorship")]
        public int CensorshipTick { get; set; }
        [Category("Video - Red light green light")]
        public bool RedLight { get; set; }
        [Category("Video - Red light green light")]
        public bool RLGLGame { get; set; }
        [Category("Video - Red light green light")]
        public int RLGLTauntTick { get; set; }
        [Category("Video - Red light green light")]
        public int RLGLTick { get; set; }
        [Category("Video")]
        [Obsolete("Never set to TRUE")]
        public bool NoVideo { get; set; }


        [Category("Glitter")]
        [Description("File paths in this list are executed next. If empty a random script will appear.")]
        [Editor(EditorGenericStringList, typeof(MyUITypeEditor))]
        public List<string> UpdateList { get; set; } = new List<string>();

        [Category("Glitter")]
        [Description("Time until the next glitter script is exceuted.")]
        public int UpdatesTick { get; set; } = 120;
        [Category("Glitter")]
        [Description("Set to True while a glitter script is running.")]
        public bool UpdatingPost { get; set; }

        [Category("Glitter")]
        [Description("Time until next status post.")]
        public int UpdateStageTick { get; set; }

        [Category("Glitter")]
        [Description("Domme's post text.")]
        public string StatusText { get; set; }
        [Category("Glitter")]
        [Description("Contact1 post text. If the line starts with \">\" it has been already sent or skipped.")]
        public string StatusText1 { get; set; }
        [Category("Glitter")]
        [Description("Contact2 post text. If the line starts with \">\" it has been already sent or skipped.")]
        public string StatusText2 { get; set; }
        [Category("Glitter")]
        [Description("Contact3 post text. If the line starts with \">\" it has been already sent or skipped.")]
        public string StatusText3 { get; set; }

        [Category("Glitter")]
        [Description("Chance of skipping Contact 1's post.")]
        public int StatusChance1 { get; set; }
        [Category("Glitter")]
        [Description("Chance of skipping Contact 2's post.")]
        public int StatusChance2 { get; set; }
        [Category("Glitter")]
        [Description("Chance of skipping Contact 3's post.")]
        public int StatusChance3 { get; set; }



        [Editor(EditorGenericStringList, typeof(MyUITypeEditor))]
        public List<string> LocalTagImageList { get; set; } = new List<string>();

        [Obsolete("Proccessing values belong into the corresponding function.")]
        public string PetName { get; set; }

        [Category("Taunts")]
        public int TempScriptCount { get; set; }

        public int SlideshowTimerTick { get; set; }





        public int LastScriptCountdown { get; set; }
        public bool LastScript { get; set; }

        public bool SaidHello { get; set; } = false;
        public bool justStarted { get; set; } = false;


        public int AvgEdgeStroking { get; set; }
        public int AvgEdgeNoTouch { get; set; }
        public int EdgeCountTick { get; set; }
        public bool AvgEdgeStrokingFlag { get; set; }
        public int AvgEdgeCount { get; set; }
        public int AvgEdgeCountRest { get; set; }
        public int EdgeTickCheck { get; set; }

        public bool EdgeNOT { get; set; }
        public bool isLink { get; set; }

        public bool AlreadyStrokingEdge { get; set; }

        [Category("WritingTask")]
        public int WritingTaskLinesAmount { get; set; }
        [Category("WritingTask")]
        public int WritingTaskLinesWritten { get; set; }
        [Category("WritingTask")]
        public int WritingTaskLinesRemaining { get; set; }
        [Category("WritingTask")]
        public int WritingTaskMistakesAllowed { get; set; }
        [Category("WritingTask")]
        public int WritingTaskMistakesMade { get; set; }
        [Category("WritingTask")]
        public bool WritingTaskFlag { get; set; } = false;
        [Category("WritingTask")]
        public float WritingTaskCurrentTime { get; set; }

        public bool FirstRound { get; set; }
        public int StartStrokingCount { get; set; } = 0;
        public bool TeaseVideo { get; set; }

        [Category("Games - TnA")]
        [Description("This list contains all boob-images of TNA game.")]
        [Editor(EditorGenericStringList, typeof(MyUITypeEditor))]
        public List<string> BoobList { get; set; } = new List<string>();
        [Category("Games - TnA")]
        [Description("This list contains all butt-images of TNA game.")]
        [Editor(EditorGenericStringList, typeof(MyUITypeEditor))]
        public List<string> AssList { get; set; } = new List<string>();
        [Category("Games - TnA")]
        public bool AssImage { get; set; } = false;
        [Category("Games - TnA")]
        public bool BoobImage { get; set; } = false;

        [Category("Bookmark")]
        public bool BookmarkModule { get; set; } = false;
        [Category("Bookmark")]
        public string BookmarkModuleFile { get; set; }
        [Category("Bookmark")]
        public int BookmarkModuleLine { get; set; }
        [Category("Bookmark")]
        public bool BookmarkLink { get; set; } = false;
        [Category("Bookmark")]
        public string BookmarkLinkFile { get; set; }
        [Category("Bookmark")]
        public int BookmarkLinkLine { get; set; }

        public int WaitTick { get; set; } = -1;




        public bool OrgasmDenied { get; set; }
        public bool OrgasmAllowed { get; set; }
        public bool OrgasmRuined { get; set; }
        public string LastOrgasmType { get; set; } = "";

        public int CaloriesConsumed { get; set; }
        public int CaloriesGoal { get; set; }

        [Category("Tokens")]
        [ReadOnly(true)]
        public int GoldTokens { get; set; }
        [Category("Tokens")]
        [ReadOnly(true)]
        public int SilverTokens { get; set; }
        [Category("Tokens")]
        [ReadOnly(true)]
        public int BronzeTokens { get; set; }

        public bool EdgeFound { get; set; }

        public bool OrgasmYesNo { get; set; } = false;

        [Category("Images")]
        [Description("Determines if the custom slideshow should run.")]
        public bool CustomSlideEnabled { get; set; }
        [Category("Images")]
        [Description("Stores all images and genre informations for CustomSlideshow")]
        public CustomSlideshow CustomSlideshow { get; set; } = new CustomSlideshow();

        [Category("Images")]
        [Obsolete("DommeImageSTR is obsolete. Do not implement in new code.")]
        public string DommeImageSTR { get; set; }

        [Obsolete("JustShowedBlogImage is obsolete. Do not implement in new code.")]
        [Category("Images")]
        public bool JustShowedBlogImage { get; set; } = false;
        [Category("Images")]
        public bool JustShowedSlideshowImage { get; set; } = false;
        [Category("Images")]
        public bool LockImage { get; set; }
        [Category("Images")]
        public string RandomSlideshowCategory { get; set; }
        [Category("Images")]
        [Description("True if main slideshow is loaded.")]
        public bool SlideshowLoaded { get; set; }
        [Category("Images")]
        public ContactData SlideshowMain { get; set; }
        [Category("Images")]
        public ContactData SlideshowContact1 { get; set; }
        [Category("Images")]
        public ContactData SlideshowContact2 { get; set; }
        [Category("Images")]
        public ContactData SlideshowContact3 { get; set; }
        [Category("Images")]
        public ContactData SlideshowContactRandom { get; set; }
        [Category("Custom Task")]
        public bool CustomTask { get; set; }
        [Category("Custom Task")]
        public bool CustomTaskFirst { get; set; } = true;
        [Category("Custom Task")]
        public string CustomTaskText { get; set; }
        [Category("Custom Task")]
        public string CustomTaskTextFirst { get; set; }
        [Category("Custom Task")]
        public bool CustomTaskActive { get; set; }


        [Category("Risky Pick")]
        public bool RiskyDeal { get; set; }
        [Category("Risky Pick")]
        public bool RiskyEdges { get; set; }
        [Category("Risky Pick")]
        public bool RiskyDelay { get; set; }

        public bool SysMes { get; set; }
        public bool EmoMes { get; set; }

        public string Group { get; set; } = "D";
        public bool GlitterTease { get; set; }
        public int AddContactTick { get; set; }
        public bool contact1Present { get; set; }
        public bool contact2Present { get; set; }
        public bool contact3Present { get; set; }
        public bool dommePresent { get; set; } = true;
        public bool Contact1Edge { get; set; }
        public bool Contact2Edge { get; set; }
        public bool Contact3Edge { get; set; }

        public bool Contact1Stroke { get; set; }
        public bool Contact2Stroke { get; set; }
        public bool Contact3Stroke { get; set; }

        public string tempDomName { get; set; } = "";
        public string tempHonorific { get; set; }
        public string tempDomHonorific { get; set; }
        public string shortName { get; set; }
        public string domAvatarImage { get; set; } // it's a URL now
        public string currentWriteTask { get; set; }
        public bool randomWriteTask { get; set; }
        public bool dontCheck { get; set; }


        /// <summary>Gets or sets current stack for @CallReturn( command.</summary>
        [Category("@CallReturn(")]
        [Browsable(false)]
        public Stack CallReturns = new Stack();

        [Category("@CallReturn(")]
        [Description("Updated after returning to calling script.")]
        public string ReturnSubState { get; set; }

        public int SessionEdges { get; set; }


        public bool StrokeFaster { get; set; }
        public bool StrokeFastest { get; set; }
        public bool StrokeSlower { get; set; }
        public bool StrokeSlowest { get; set; }

        public bool InputFlag { get; set; }
        public string InputString { get; set; }

        public bool RapidCode { get; set; }
        public bool RapidFire { get; set; }

        [Category("Typos")]
        public bool CorrectedTypo { get; set; }
        [Category("Typos")]
        public string CorrectedWord { get; set; }
        [Category("Typos")]
        public int TypoSwitch { get; set; } = 1;
        [Category("Typos")]
        public bool TyposDisabled { get; set; }

        [Description("True if Interrupts are disabled.")]
        public bool DoNotDisturb { get; set; }

        public int EdgeHoldSeconds { get; set; }
        public bool EdgeHoldFlag { get; set; }


        public bool InputIcon { get; set; }




        public int StrokePace { get; set; } = 0;

        public string GeneralTime { get; set; }

        public int TimeoutTick { get; set; }

        /// <summary>
        ///     ''' This Variable contains the Path of origin of the displayed Image. CheckDommeTag() uses 
        ///     ''' this string to get the curremt ImageData for the DommeTagApp.
        ///     ''' </summary>
        [Category("Images")]
        public string ImageLocation { get; set; } = "";

        public string ResponseYes { get; set; }
        public string ResponseNo { get; set; }

        public string SetModule { get; set; } = "";
        public string SetLink { get; set; } = "";
        public string SetModuleGoto { get; set; } = "";
        public string SetLinkGoto { get; set; } = "";


        public bool OrgasmRestricted { get; set; }

        public string FollowUp { get; set; } = "";

        public bool WorshipMode { get; set; } = false;
        public string WorshipTarget { get; set; } = "";

        public bool HoldTaunts { get; set; } = false;
        public bool LongHold { get; set; } = false;
        public bool ExtremeHold { get; set; } = false;
        public bool LongTaunts { get; set; }
        public bool ExtremeTaunts { get; set; }

        [Category("Multiple Edges")]
        public bool MultipleEdges { get; set; }
        [Category("Multiple Edges")]
        public int MultipleEdgesAmount { get; set; }
        [Category("Multiple Edges")]
        public int MultipleEdgesInterval { get; set; }
        [Category("Multiple Edges")]
        public int MultipleEdgesTick { get; set; }
        [Category("Multiple Edges")]
        public string MultipleEdgesMetronome { get; set; } = "";

        [Category("Modes")]
        public Dictionary<string, Mode> Modes { get; set; } = new Dictionary<string, Mode>(System.StringComparer.OrdinalIgnoreCase);
        [Category("Modes")]
        public Mode edgeMode { get; set; } = new Mode();
        [Category("Modes")]
        public Mode cameMode { get; set; } = new Mode();
        [Category("Modes")]
        public Mode ruinMode { get; set; } = new Mode();
        [Category("Modes")]
        public Mode yesMode { get; set; } = new Mode();
        [Category("Modes")]
        public Mode noMode { get; set; } = new Mode();

        public bool SecondSession { get; set; }

        public subAnswers checkAnswers { get; set; }

        /// <summary>
        ///     ''' Set to true if the sub is on the edge and the domme had decided to not to stop stroking.
        ///     ''' </summary>
        ///     ''' <remarks>
        ///     ''' Uses following vocabulary Files:
        ///     ''' #SYS_TauntEdging.txt when the taunting begins.
        ///     ''' #SYS_TauntEdgingAsked.txt if the sub continues to tell he's on the edge.
        ///     ''' </remarks>
        [Description("If True stroking continues when sub is on edge.")]
        public bool TauntEdging { get; set; } = false;

        public List<string> CountDownList { get; set; } = new List<string>();
        public List<string> CountUpList { get; set; } = new List<string>();

        public bool MultiTauntPictureHold { get; set; }

        public bool EndSession { get; set; }

        public string VideoGenre { get; set; }

        public int StrokeCounter { get; set; }
        [Category("Video")]
        public int JumpPercentVid { get; set; }
        [Category("Video")]
        public int JumpPercentAudio { get; set; }

        [Category("Images")]
        public ContactData SlideshowMasterDomme { get; set; }
        public int StrokePaceMem { get; set; }


        public bool serialized_WMP_Visible;
        public string serialized_WMP_URL;
        public double serialized_WMP_Position;
        public long serialized_WMP_Playstate;



        // ===============================================================================
        // Timer enable states
        // ===============================================================================
        public bool AudibleMetronome_enabled = false;
        public bool AvoidTheEdge_enabled = false;
        public bool AvoidTheEdgeResume_enabled = false;
        public bool AvoidTheEdgeTaunts_enabled = false;
        public bool CensorshipTimer_enabled = false;
        public bool Contact1Timer_enabled = false;
        public bool Contact2Timer_enabled = false;
        public bool Contact3Timer_enabled = false;
        public bool ContactTimer_enabled = false;
        public bool CustomSlideshowTimer_enabled = false;
        public bool DelayTimer_enabled = false;
        public bool EdgeCountTimer_enabled = false;
        public bool EdgeTauntTimer_enabled = false;
        public bool HoldEdgeTauntTimer_enabled = false;
        public bool HoldEdgeTimer_enabled = false;
        public bool IsTypingTimer_enabled = true;
        public bool RLGLTauntTimer_enabled = false;
        public bool RLGLTimer_enabled = false;
        public bool SendTimer_enabled = false;
        public bool SlideshowTimer_enabled = false;
        public bool StrokeTauntTimer_enabled = false;
        public bool StrokeTimer_enabled = false;
        public bool StrokeTimeTotalTimer_enabled = true;
        public bool StupidTimer_enabled = false;
        public bool TeaseTimer_enabled = false;
        public bool Timer1_enabled = false;
        public bool TnASlides_enabled = false;
        public bool UpdateStageTimer_enabled = false;
        public bool UpdatesTimer_enabled = true;
        public bool VideoTauntTimer_enabled = false;
        public bool WaitTimer_enabled = false;
        public bool WMPTimer_enabled = true;

        // ===============================================================================
        // Timer intervals
        // ===============================================================================
        public int AudibleMetronome_Interval = 30;
        public int AvoidTheEdge_Interval = 1000;
        public int AvoidTheEdgeResume_Interval = 1000;
        public int AvoidTheEdgeTaunts_Interval = 1000;
        public int CensorshipTimer_Interval = 1000;
        public int Contact1Timer_Interval = 1000;
        public int Contact2Timer_Interval = 1000;
        public int Contact3Timer_Interval = 1000;
        public int ContactTimer_Interval = 1000;
        public int CustomSlideshowTimer_Interval = 1000;
        public int DelayTimer_Interval = 1000;
        public int EdgeCountTimer_Interval = 1000;
        public int EdgeTauntTimer_Interval = 1000;
        public int HoldEdgeTauntTimer_Interval = 1000;
        public int HoldEdgeTimer_Interval = 1000;
        public int IsTypingTimer_Interval = 110;
        public int RLGLTauntTimer_Interval = 1000;
        public int RLGLTimer_Interval = 1000;
        public int SendTimer_Interval = 110;
        public int SlideshowTimer_Interval = 1000;
        public int StrokeTauntTimer_Interval = 1000;
        public int StrokeTimer_Interval = 1000;
        public int StrokeTimeTotalTimer_Interval = 1000;
        public int StupidTimer_Interval = 300;
        public int TeaseTimer_Interval = 1000;
        public int Timer1_Interval = 110;
        public int TnASlides_Interval = 334;
        public int UpdateStageTimer_Interval = 1000;
        public int UpdatesTimer_Interval = 1000;
        public int VideoTauntTimer_Interval = 1000;
        public int WaitTimer_Interval = 1000;
        public int WMPTimer_Interval = 1000;



        public List<string> serialized_Flags = new List<string>();
        public List<string> serialized_FlagsTemp = new List<string>();
        public Dictionary<string, string> serialized_Variables = new Dictionary<string, string>();




        [NonSerialized]
        [OptionalField]
        public FileClass Files;
        [NonSerialized]
        [OptionalField]
        public FoldersClass Folders;

        //[NonSerialized]
        //private Form1 ActivationForm;


        /// <summary>
        /// 	''' Creates a new unactivaed instance.
        /// 	''' </summary>
        public SessionState()
        {
            // not actually a component
            InitializeComponent();
        }
        /// <summary>
        ///     ''' Creates a new instance and activates it on the given Form.
        ///     ''' </summary>
        ///     ''' <param name="ActivationForm">The Form on which to apply the session.</param>

        public SessionState(object ActivationForm)
        {
            InitializeComponent();
            Activate(ActivationForm);
        }

        private void InitializeComponent()
        {
            Files = new FileClass(this);
            Folders = new FoldersClass(this);

            randomizer = new Random();

            DomPersonality = My.Settings.DomPersonality;

            StrokeTimeTotal = My.Settings.StrokeTimeTotal;

            HoldEdgeTimeTotal = My.Settings.HoldEdgeTimeTotal;

            GoldTokens = My.Settings.GoldTokens > 0 ? My.Settings.GoldTokens : 0;
            SilverTokens = My.Settings.SilverTokens > 0 ? My.Settings.SilverTokens : 0;
            BronzeTokens = My.Settings.BronzeTokens > 0 ? My.Settings.BronzeTokens : 0;

            AvgEdgeStroking = My.Settings.AvgEdgeStroking;
            AvgEdgeNoTouch = My.Settings.AvgEdgeNoTouch;
            AvgEdgeCount = My.Settings.AvgEdgeCount;
            AvgEdgeCountRest = My.Settings.AvgEdgeCountRest;

            giveUpReturn = My.Settings.GiveUpReturn;
            DommeMood = randomizer.Next(My.Settings.DomMoodMin, My.Settings.DomMoodMax + 1);

            // SlideshowMain = New ContactData(ContactType.Domme)
            // SlideshowContact1 = New ContactData(ContactType.Contact1)
            // SlideshowContact2 = New ContactData(ContactType.Contact2)
            // SlideshowContact3 = New ContactData(ContactType.Contact3)
            // SlideshowContactRandom = New ContactData(ContactType.Random)
            // currentlyPresentContacts = New List(Of String)
            // currentlyPresentContacts.Add(SlideshowMain.TypeName)

            CaloriesConsumed = My.Settings.CaloriesConsumed;
            checkAnswers = new subAnswers();
        }




        public SessionState Clone()
        {
            return (SessionState)this.MemberwiseClone();
        }


        /// <summary>
        /// 	''' This Method is started after deserialization. It is used to fix issues with optional fields.
        /// 	''' To ensure future compatiblity, all additional added members in SessioState-Class have to be marked
        /// 	''' as optional and initialized using this method.
        /// 	''' </summary>
        /// 	''' <param name="sc"></param>
        [OnDeserialized]
        public void onDeserialized_FixFields(StreamingContext sc)
        {
            // Suppress obsolete warnings.

            // #Disable Warning BC40000 - I can't compile this in VS2010. Changed to the three lines below as per Notay's advice - 1885

            /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
            // Marked as <NonSerialized> <OptionalField> have to be initialized on every deserialization.
            if (Files == null)
                Files = new FileClass(this);
            if (Folders == null)
                Folders = new FoldersClass(this);
        }



        /// <summary>
        /// 	''' Stores a running session to disk. 
        /// 	''' </summary>
        /// 	''' <param name="serializeForm">The Form to gather informations(Timer-States etc.) from.</param>
        /// 	''' <exception cref="Exception">Rethrows all exceptions.</exception>
        /// 	''' <remarks>This function is invoked on the given Form's UI-Thread.</remarks>
        public void FetchFormData(object serializeForm)
        {
#if TODO
            if (serializeForm != null && serializeForm.InvokeRequired)
            {
                // Calling from another Thread -> Invoke on controls UI-Thread
                Action<Form1> Act = s1 => FetchFormData(s1);
                serializeForm.Invoke(Act);
                return;
            }
            // Called from Controls UI-Thread -> Execute Code.


            {
                var withBlock = serializeForm;
                DomPersonality = withBlock.dompersonalitycombobox.SelectedItem.ToString;

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Get Timer EnableStates
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                AvoidTheEdge_enabled = withBlock.AvoidTheEdge.Enabled;
                AvoidTheEdgeResume_enabled = withBlock.AvoidTheEdgeResume.Enabled;
                AvoidTheEdgeTaunts_enabled = withBlock.AvoidTheEdgeTaunts.Enabled;
                CensorshipTimer_enabled = withBlock.CensorshipTimer.Enabled;
                CustomSlideshowTimer_enabled = withBlock.CustomSlideshowTimer.Enabled;
                EdgeCountTimer_enabled = withBlock.EdgeCountTimer.Enabled;
                EdgeTauntTimer_enabled = withBlock.EdgeTauntTimer.Enabled;
                HoldEdgeTauntTimer_enabled = withBlock.HoldEdgeTauntTimer.Enabled;
                HoldEdgeTimer_enabled = withBlock.HoldEdgeTimer.Enabled;
                IsTypingTimer_enabled = withBlock.IsTypingTimer.Enabled;
                RLGLTauntTimer_enabled = withBlock.RLGLTauntTimer.Enabled;
                RLGLTimer_enabled = withBlock.RLGLTimer.Enabled;
                SendTimer_enabled = withBlock.SendTimer.Enabled;
                SlideshowTimer_enabled = withBlock.SlideshowTimer.Enabled;
                StrokeTauntTimer_enabled = withBlock.StrokeTauntTimer.Enabled;
                StrokeTimer_enabled = withBlock.StrokeTimer.Enabled;
                StrokeTimeTotalTimer_enabled = withBlock.StrokeTimeTotalTimer.Enabled;
                StupidTimer_enabled = withBlock.StupidTimer.Enabled;
                TeaseTimer_enabled = withBlock.TeaseTimer.Enabled;
                Timer1_enabled = withBlock.Timer1.Enabled;
                TnASlides_enabled = withBlock.TnASlides.Enabled;
                UpdateStageTimer_enabled = withBlock.UpdateStageTimer.Enabled;
                UpdatesTimer_enabled = withBlock.UpdatesTimer.Enabled;
                VideoTauntTimer_enabled = withBlock.VideoTauntTimer.Enabled;
                WaitTimer_enabled = withBlock.WaitTimer.Enabled;
                WMPTimer_enabled = withBlock.WMPTimer.Enabled;
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Get Timer Intervals
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                AvoidTheEdge_Interval = withBlock.AvoidTheEdge.Interval;
                AvoidTheEdgeResume_Interval = withBlock.AvoidTheEdgeResume.Interval;
                AvoidTheEdgeTaunts_Interval = withBlock.AvoidTheEdgeTaunts.Interval;
                CensorshipTimer_Interval = withBlock.CensorshipTimer.Interval;
                CustomSlideshowTimer_Interval = withBlock.CustomSlideshowTimer.Interval;
                EdgeCountTimer_Interval = withBlock.EdgeCountTimer.Interval;
                EdgeTauntTimer_Interval = withBlock.EdgeTauntTimer.Interval;
                HoldEdgeTauntTimer_Interval = withBlock.HoldEdgeTauntTimer.Interval;
                HoldEdgeTimer_Interval = withBlock.HoldEdgeTimer.Interval;
                IsTypingTimer_Interval = withBlock.IsTypingTimer.Interval;
                RLGLTauntTimer_Interval = withBlock.RLGLTauntTimer.Interval;
                RLGLTimer_Interval = withBlock.RLGLTimer.Interval;
                SendTimer_Interval = withBlock.SendTimer.Interval;
                SlideshowTimer_Interval = withBlock.SlideshowTimer.Interval;
                StrokeTauntTimer_Interval = withBlock.StrokeTauntTimer.Interval;
                StrokeTimer_Interval = withBlock.StrokeTimer.Interval;
                StrokeTimeTotalTimer_Interval = withBlock.StrokeTimeTotalTimer.Interval;
                StupidTimer_Interval = withBlock.StupidTimer.Interval;
                TeaseTimer_Interval = withBlock.TeaseTimer.Interval;
                Timer1_Interval = withBlock.Timer1.Interval;
                TnASlides_Interval = withBlock.TnASlides.Interval;
                UpdateStageTimer_Interval = withBlock.UpdateStageTimer.Interval;
                UpdatesTimer_Interval = withBlock.UpdatesTimer.Interval;
                VideoTauntTimer_Interval = withBlock.VideoTauntTimer.Interval;
                WaitTimer_Interval = withBlock.WaitTimer.Interval;
                WMPTimer_Interval = withBlock.WMPTimer.Interval;

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Get WMP-Data
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                serialized_WMP_Visible = withBlock.DomWMP.Visible;
                serialized_WMP_URL = withBlock.DomWMP.URL;
                serialized_WMP_Playstate = withBlock.DomWMP.playState;
                serialized_WMP_Position = withBlock.DomWMP.Ctlcontrols.currentPosition;

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Get Flags
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                if (serialized_Flags == null)
                    serialized_Flags = new List<string>();

                serialized_Flags.Clear();

                if (Directory.Exists(Folders.Flags))
                {
                    foreach (string fn in Directory.GetFiles(Folders.Flags))
                        serialized_Flags.Add(Path.GetFileName(fn));
                }

                // Get temporary Flags
                if (serialized_FlagsTemp == null)
                    serialized_FlagsTemp = new List<string>();
                serialized_FlagsTemp.Clear();

                if (Directory.Exists(Folders.TempFlags))
                {
                    foreach (string fn in Directory.GetFiles(Folders.TempFlags))
                        serialized_FlagsTemp.Add(Path.GetFileName(fn));
                }

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Get Variables
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼



                if (serialized_Variables == null)
                    serialized_Variables = new Dictionary<string, string>();


                serialized_Variables.Clear();

                if (Directory.Exists(Folders.Variables))
                {
                    foreach (string fn in Directory.GetFiles(Folders.Variables))
                    {
                        string val = TxtReadLine(fn);
                        serialized_Variables.Add(Path.GetFileName(fn), val);
                    }
                }
            }
#endif
        }

        /// <summary>
        ///     ''' Activates the current SessionState.
        ///     ''' </summary>
        ///     ''' <param name="activateForm">The Form to start the session on.</param>
        public void Activate(object activateForm)
        {
#if TODO
            // Check if InvokeIsRequired
            if (activateForm != null && activateForm.InvokeRequired)
            {
                // Calling from another Thread -> Invoke on controls UI-Thread
                Action<Form1> Act = s1 => Activate(s1);
                activateForm.Invoke(Act);
            }
            // Called from Controls UI-Thread -> Execute Code.

            ActivationForm = activateForm;

            {
                var withBlock = activateForm;
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Disable Timers to avoid Exceptions
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                withBlock.AvoidTheEdge.Enabled = false;
                withBlock.AvoidTheEdgeResume.Enabled = false;
                withBlock.AvoidTheEdgeTaunts.Enabled = false;
                withBlock.CensorshipTimer.Enabled = false;
                withBlock.CustomSlideshowTimer.Enabled = false;
                withBlock.EdgeCountTimer.Enabled = false;
                withBlock.EdgeTauntTimer.Enabled = false;
                withBlock.HoldEdgeTauntTimer.Enabled = false;
                withBlock.HoldEdgeTimer.Enabled = false;
                withBlock.IsTypingTimer.Enabled = false;
                withBlock.RLGLTauntTimer.Enabled = false;
                withBlock.RLGLTimer.Enabled = false;
                withBlock.SendTimer.Enabled = false;
                withBlock.SlideshowTimer.Enabled = false;
                withBlock.StrokeTauntTimer.Enabled = false;
                withBlock.StrokeTimer.Enabled = false;
                withBlock.StrokeTimeTotalTimer.Enabled = false;
                withBlock.StupidTimer.Enabled = false;
                withBlock.TeaseTimer.Enabled = false;
                withBlock.Timer1.Enabled = false;
                withBlock.TnASlides.Enabled = false;
                withBlock.UpdateStageTimer.Enabled = false;
                withBlock.UpdatesTimer.Enabled = false;
                withBlock.VideoTauntTimer.Enabled = false;
                withBlock.WaitTimer.Enabled = false;
                withBlock.WMPTimer.Enabled = false;

                if (withBlock.ssh != null)
                    withBlock.ssh.Dispose();
                withBlock.ssh = this;

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Set Domme Personality
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                if (DomPersonality == string.Empty)
                    DomPersonality = My.Settings.DomPersonality;

                if (withBlock.dompersonalitycombobox.Items.Contains(DomPersonality) == false)
                    throw new Exception("The personality \"" + DomPersonality + "\" was not found.");
                else
                    withBlock.dompersonalitycombobox.SelectedItem = DomPersonality;

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Restore Variables 
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                if (serialized_Variables.Count > 0)
                {
                    if (Directory.Exists(Folders.Variables) == false)
                        Directory.CreateDirectory(Folders.Variables);
                    else
                        foreach (string fn in Directory.GetFiles(Folders.Variables))
                            File.Delete(fn);

                    foreach (string fn in serialized_Variables.Keys)
                        My.Computer.FileSystem.WriteAllText(Folders.Variables + fn, serialized_Variables[fn], false);
                }

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Restore flags 
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                if (serialized_Flags.Count > 0)
                {
                    if (Directory.Exists(Folders.Flags) == false)
                        Directory.CreateDirectory(Folders.Flags);
                    else
                        foreach (string fn in Directory.GetFiles(Folders.Flags))
                            File.Delete(fn);

                    foreach (string fn in serialized_Flags)
                    {
                        using (FileStream fs = new FileStream(Folders.Flags + fn, FileMode.Create))
                        {
                        }
                    }
                }
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Restore temporary flags 
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                if (Directory.Exists(Folders.TempFlags) == false)
                    Directory.CreateDirectory(Folders.TempFlags);
                else
                    foreach (string fn in Directory.GetFiles(Folders.TempFlags))
                        File.Delete(fn);

                if (serialized_FlagsTemp.Count > 0)
                {
                    foreach (string fn in serialized_FlagsTemp)
                    {
                        using (FileStream fs = new FileStream(Folders.TempFlags + fn, FileMode.Create))
                        {
                        }
                    }
                }
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Set Slideshows
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                if (SlideshowMain == null)
                    SlideshowMain = new ContactData(ContactType.Domme);
                if (SlideshowContact1 == null)
                    SlideshowContact1 = new ContactData(ContactType.Contact1);
                if (SlideshowContact2 == null)
                    SlideshowContact2 = new ContactData(ContactType.Contact2);
                if (SlideshowContact3 == null)
                    SlideshowContact3 = new ContactData(ContactType.Contact3);
                if (currentlyPresentContacts.Count == 0)
                    currentlyPresentContacts.Add(SlideshowMain.TypeName);

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Set Picturebox & WMP
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                if (isURL(ImageLocation))
                    withBlock.ShowImage(ImageLocation, true);
                else if (File.Exists(ImageLocation))
                    withBlock.ShowImage(ImageLocation, true);
                else if (SlideshowLoaded == true & SlideshowMain.ImageList.Count > 0 && File.Exists(SlideshowMain.CurrentImage))
                    withBlock.ShowImage(SlideshowMain.CurrentImage, true);
                else
                    withBlock.ClearMainPictureBox();

                withBlock.mainPictureBox.Visible = !serialized_WMP_Visible;
                withBlock.DomWMP.Visible = serialized_WMP_Visible;
                withBlock.DomWMP.URL = serialized_WMP_URL;
                withBlock.DomWMP.Ctlcontrols.currentPosition = serialized_WMP_Position;

                if (serialized_WMP_Playstate <= 1)
                    withBlock.DomWMP.Ctlcontrols.stop();
                else if (serialized_WMP_Playstate == 2)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    while (!withBlock.DomWMP.playState == WMPPlayState.wmppsPlaying | sw.ElapsedMilliseconds > 5000)
                        Application.DoEvents();

                    withBlock.DomWMP.Ctlcontrols.pause();
                }
                else if (serialized_WMP_Playstate == 3)
                    withBlock.DomWMP.Ctlcontrols.play();

                // Hide Cencorshipbar , if no game is running 
                if (CensorshipGame == true | CensorshipTimer_enabled == false)
                    withBlock.CensorshipBar.Visible = false;

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Set Chat and StrokePace
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼

                activateForm.ChatUpdate();

                // To update the threadsafe Metronome StrokePace 
                // Only needs to be done on activation
                withBlock.StrokePace = StrokePace;

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Set Timer Intervals
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                withBlock.AvoidTheEdge.Interval = AvoidTheEdge_Interval;
                withBlock.AvoidTheEdgeResume.Interval = AvoidTheEdgeResume_Interval;
                withBlock.AvoidTheEdgeTaunts.Interval = AvoidTheEdgeTaunts_Interval;
                withBlock.CensorshipTimer.Interval = CensorshipTimer_Interval;
                withBlock.CustomSlideshowTimer.Interval = CustomSlideshowTimer_Interval;
                withBlock.EdgeCountTimer.Interval = EdgeCountTimer_Interval;
                withBlock.EdgeTauntTimer.Interval = EdgeTauntTimer_Interval;
                withBlock.HoldEdgeTauntTimer.Interval = HoldEdgeTauntTimer_Interval;
                withBlock.HoldEdgeTimer.Interval = HoldEdgeTimer_Interval;
                withBlock.IsTypingTimer.Interval = IsTypingTimer_Interval;
                withBlock.RLGLTauntTimer.Interval = RLGLTauntTimer_Interval;
                withBlock.RLGLTimer.Interval = RLGLTimer_Interval;
                withBlock.SendTimer.Interval = SendTimer_Interval;
                withBlock.SlideshowTimer.Interval = SlideshowTimer_Interval;
                withBlock.StrokeTauntTimer.Interval = StrokeTauntTimer_Interval;
                withBlock.StrokeTimer.Interval = StrokeTimer_Interval;
                withBlock.StrokeTimeTotalTimer.Interval = StrokeTimeTotalTimer_Interval;
                withBlock.StupidTimer.Interval = StupidTimer_Interval;
                withBlock.TeaseTimer.Interval = TeaseTimer_Interval;
                withBlock.Timer1.Interval = Timer1_Interval;
                withBlock.TnASlides.Interval = TnASlides_Interval;
                withBlock.UpdateStageTimer.Interval = UpdateStageTimer_Interval;
                withBlock.UpdatesTimer.Interval = UpdatesTimer_Interval;
                withBlock.VideoTauntTimer.Interval = VideoTauntTimer_Interval;
                withBlock.WaitTimer.Interval = WaitTimer_Interval;
                withBlock.WMPTimer.Interval = WMPTimer_Interval;

                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                // Set Timer EnableStates
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                withBlock.AvoidTheEdge.Enabled = AvoidTheEdge_enabled;
                withBlock.AvoidTheEdgeResume.Enabled = AvoidTheEdgeResume_enabled;
                withBlock.AvoidTheEdgeTaunts.Enabled = AvoidTheEdgeTaunts_enabled;
                withBlock.CensorshipTimer.Enabled = CensorshipTimer_enabled;
                withBlock.CustomSlideshowTimer.Enabled = CustomSlideshowTimer_enabled;
                withBlock.EdgeCountTimer.Enabled = EdgeCountTimer_enabled;
                withBlock.EdgeTauntTimer.Enabled = EdgeTauntTimer_enabled;
                withBlock.HoldEdgeTauntTimer.Enabled = HoldEdgeTauntTimer_enabled;
                withBlock.HoldEdgeTimer.Enabled = HoldEdgeTimer_enabled;
                withBlock.IsTypingTimer.Enabled = IsTypingTimer_enabled;
                withBlock.RLGLTauntTimer.Enabled = RLGLTauntTimer_enabled;
                withBlock.RLGLTimer.Enabled = RLGLTimer_enabled;
                withBlock.SendTimer.Enabled = SendTimer_enabled;
                withBlock.SlideshowTimer.Enabled = SlideshowTimer_enabled;
                withBlock.StrokeTauntTimer.Enabled = StrokeTauntTimer_enabled;
                withBlock.StrokeTimer.Enabled = StrokeTimer_enabled;
                withBlock.StrokeTimeTotalTimer.Enabled = StrokeTimeTotalTimer_enabled;
                withBlock.StupidTimer.Enabled = StupidTimer_enabled;
                withBlock.TeaseTimer.Enabled = TeaseTimer_enabled;
                withBlock.Timer1.Enabled = Timer1_enabled;
                withBlock.TnASlides.Enabled = TnASlides_enabled;
                withBlock.UpdateStageTimer.Enabled = UpdateStageTimer_enabled;
                withBlock.UpdatesTimer.Enabled = UpdatesTimer_enabled;
                withBlock.VideoTauntTimer.Enabled = VideoTauntTimer_enabled;
                withBlock.WaitTimer.Enabled = WaitTimer_enabled;
                withBlock.WMPTimer.Enabled = WMPTimer_enabled;
            }
#endif
        }


        /// <summary>
        /// 	''' Loads and activates a stored session from the given path.
        /// 	''' </summary>
        /// 	''' <param name="filepath">The path to load the state from.</param>
        /// 	''' <param name="setActive">The form to activate the session on.</param>
        /// 	''' <exception cref="Exception">Rethrows all exceptions.</exception>
        /// 	''' <remarks>This function is invoked on the <see cref="ActivationForm"/>'s UI-Thread.</remarks>
        public void Load(string filepath, bool setActive)
        {
#if TODO
            try
            {
                // Check if InvokeIsRequired
                if (setActive == true && ActivationForm != null && ActivationForm.InvokeRequired)
                {
                    // Calling from another Thread -> Invoke on controls UI-Thread
                    Action<string, bool> Act = (s1, s2) => Load(s1, s2);
                    ActivationForm.Invoke(Act);
                    return;
                }
                // Called from Controls UI-Thread -> Execute Code.

                if (setActive && ActivationForm == null)
                    throw new InvalidOperationException("It is impossible to reactivate a not activated session.");


                SessionState tmpState = (SessionState)BinaryDeserialize(filepath);


                if (tmpState.Version == null)
                    throw new SerializationException("The session you are trying to open has been created with an unknown version of Tease-AI.");
                else if (new Version(tmpState.Version) < new Version(MINVERSION))
                    throw new SerializationException("The session you are trying to open has been created with version " + Version.ToString() + ". This version of Tease-AI is only capable to continue sessions from version " + MINVERSION + " or above.");


                if (setActive)
                    tmpState.Activate(ActivationForm);
            }
            catch (Exception ex)
            {
                throw;
            }
#endif
        }

        /// <summary>
        ///     ''' Stores the SessionState to disk.
        ///     ''' </summary>
        ///     ''' <param name="filePath">The filepath to store the object to.</param>
        public void Save(string filePath)
        {
#if TODO
            try
            {
                if (ActivationForm != null)
                    FetchFormData(ActivationForm);

                BinarySerialize(this, filePath);
            }
            catch (Exception ex)
            {
                throw;
            }
#endif
        }

        /// <summary>
        ///     ''' Resets the current session. Basically it disposes the current <see cref="SessionState"/>-instance
        ///     ''' and applies a new <see cref="SessionState"/>-instance to <see cref="Form1.ssh"/>.
        ///     ''' <para></para>
        ///     ''' After calling this method you have to update your object references.
        ///     ''' </summary>
        ///     ''' <returns>True if the sesstion state has been activated.</returns>
        ///     ''' <remarks>
        ///     ''' If the current session is activated, the function is invoked on <see cref="ActivationForm"/>'s 
        ///     ''' UI-Thread.</remarks>
        public bool Reset()
        {
#if TODO
            // Check if InvokeIsRequired
            if (ActivationForm != null && ActivationForm.InvokeRequired)
                // Calling from another Thread -> Invoke on controls UI-Thread
                return System.Convert.ToBoolean(ActivationForm.UIThread(Reset));
            // Called from Controls UI-Thread -> Execute Code.

            FrmSettings.TBHonorific.Text = My.Settings.SubHonorific;

            Dispose();

            if (ActivationForm != null)
            {
                ActivationForm.ssh = new SessionState(ActivationForm);
                return true;
            }
            else
                return false;
#endif
            return true;
        }

        public static string[] obtainSplitParts(string splitMe, bool isChat)
        {
            splitMe = "[" + splitMe + "] Null";
            string[] Splits = splitMe.Split(new char[] { ']' });
            Splits[0] = Splits[0].Replace("[", "");
            do
            {
                Splits[0] = Splits[0].Replace("  ", " ");
                Splits[0] = Splits[0].Replace(" ,", ",");
                Splits[0] = Splits[0].Replace(", ", ",");
                Splits[0] = Splits[0].Replace("'", "");
            }
            while (!!Splits[0].Contains("  ") & !Splits[0].Contains(", ") & !Splits[0].Contains(" ,") & !Splits[0].Contains("'"));
            if (isChat)
                // che(32) is the code for empty space - ' '
                return Splits[0].Split(new char[] { ' ', ',' });
            else
                return Splits[0].Split(new char[] { ',' });
        }

    }
}