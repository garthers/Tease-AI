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
using Microsoft.VisualBasic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Tai.Common
{
    public enum ContactType
    {
        Nothing,
        Domme,
        Contact1,
        Contact2,
        Contact3,
        Random
    }

    [Serializable]
    public class ContactData
    {
        public ContactType Contact { get; set; } = ContactType.Nothing;

        private string tempBaseFolder { get; set; } = "";
        public string ImageFolder { get; set; } = "";

        public List<string> ImageList { get; set; } = new List<string>();

        public List<string> RecentFolders { get; set; } = new List<string>();

        public int Index { get; set; } = -1;

        public string TypeName
        {
            get
            {
                if (Contact == ContactType.Contact1)
                    return _settings.Glitter1;
                else if (Contact == ContactType.Contact2)
                    return _settings.Glitter2;
                else if (Contact == ContactType.Contact3)
                    return _settings.Glitter3;
                else if (Contact == ContactType.Random)
                    return ImageFolder.Substring(ImageFolder.LastIndexOf(@"\") + 1).Trim();
                else
                    return _settings.DomName;
            }
        }

        public string TypeHonorific
        {
            get
            {
                if (Contact == ContactType.Contact1)
                    return _settings.G1Honorific;
                else if (Contact == ContactType.Contact2)
                    return _settings.G2Honorific;
                else if (Contact == ContactType.Contact3)
                    return _settings.G3Honorific;
                else if (Contact == ContactType.Random)
                    return _settings.RandomHonorific;
                else
                    return _settings.SubHonorific;
            }
        }

        public string ShortName
        {
            get
            {
                // If Contact = ContactType.Domme Then
                // Return Settings.GlitterSN
                // Else
                return TypeName;
            }
        }
        public string TypeColorHtml
        {
            get
            {
                if (Contact == ContactType.Contact1)
                    return Common.Color2Html(_settings.GlitterNC1Color);
                else if (Contact == ContactType.Contact2)
                    return Common.Color2Html(_settings.GlitterNC2Color);
                else if (Contact == ContactType.Contact3)
                    return Common.Color2Html(_settings.GlitterNC3Color);
                else
                    return _settings.DomColor;
            }
        }

        public string TypeFont
        {
            get
            {
                return _settings.DomFont;
            }
        }

        public int TypeSize
        {
            get
            {
                return _settings.DomFontSize;
            }
        }

        public string TTSvoice
        {
            get
            {
                // Throw New NotImplementedException("Not implemented yet.")
                return "Not implemented yet.";
            }
        }

        public int TTSvolume
        {
            get
            {
                return _settings.VVolume;
            }
        }

        public int TTSrate
        {
            get
            {
                return _settings.VRate;
            }
        }

        [NonSerialized]
        [OptionalField]
        private Dictionary<string, ImageTagCacheItem> ImageTagCache = new Dictionary<string, ImageTagCacheItem>(StringComparer.OrdinalIgnoreCase);

        private readonly ISettings _settings;
        private readonly Random _random;

        public ContactData(ISettings settings, Random random)
        {
            _settings = settings;
            _random = random;
        }

        public ContactData(ContactType type, ISettings settings, Random random)
        {
            Contact = type;
            _settings = settings;
            _random = random;
            //Check_ImageDir();
        }

        /// <summary>
        /// 	''' Fixes errors caused by missing optional fields after deserialisation.
        /// 	''' </summary>
        /// 	''' <param name="sc"></param>
        [OnDeserialized]
        public void OnDeserialized(StreamingContext sc)
        {
            if (ImageTagCache == null)
                ImageTagCache = new Dictionary<string, ImageTagCacheItem>(StringComparer.OrdinalIgnoreCase);
        }

        public bool Check_ImageDir()
        {
            /*
            var tp = Contact;
            string def = getDefaultFolder();
            string val = getCurrentBaseFolder();
            string text = "";

            if (tp == ContactType.Domme)
                text = "Domme";
            else if (tp == ContactType.Contact1)
                text = "Contact 1";
            else if (tp == ContactType.Contact2)
                text = "Contact 2";
            else if (tp == ContactType.Contact3)
                text = "Contact 3";
            else if (tp == ContactType.Random)
                text = "Random";

            val = FolderCheck(text, val, def);
            */
            //if (Contact == ContactType.Random)
            //    SetBaseFolder(val);

            //if (val == def)
            //    return false;
            //else
                return true;
        }

        public string FolderCheck(string directoryDescription, string directoryPath, string defaultPath)
        {
            string rtnPath;

            // Exit if default value.
            if (directoryPath == defaultPath)
                return defaultPath;

            // check it if the directory exists.
            if (!Directory.Exists(directoryPath))
            {
                throw new Exception($"Directory {directoryDescription} does not exist {directoryPath}");
            }
            rtnPath = directoryPath;

            try
            {

                int imgCount = 0;

                foreach (var folder in Directory.EnumerateDirectories(rtnPath))
                {
                    if (_settings.CBSlideshowSubDir)
                        imgCount += DirectoryExt.GetFilesImages(folder, SearchOption.AllDirectories).Count;
                    else
                        imgCount += DirectoryExt.GetFilesImages(folder, SearchOption.TopDirectoryOnly).Count;
                    if (imgCount > 0)
                        break;
                }

                if (imgCount <= 0)
                    throw new DirectoryNotFoundException("There are no subdirectories containing images in \"" + rtnPath + "\".");

                return rtnPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking folder {rtnPath}", ex);
            }

        }

        public List<string> GetRandom(bool newFolder)
        {
            // no need to check again since you already checked when creating the contact
            //if (Check_ImageDir())
                return LoadRandom(getCurrentBaseFolder(), newFolder);
            //else
            //    return new List<string>();
        }

        public List<string> LoadRandom(string baseDirectory, bool newFolder)
        {
            if (Directory.Exists(baseDirectory) == false)
                throw new DirectoryNotFoundException("The given slideshow base diretory \"" + baseDirectory + "\" was not found.");
            string currPath;
            if (Contact == ContactType.Random & !newFolder)
            {
                currPath = DirectoryExt.GetDirectories(baseDirectory).ElementAt(_random.Next(0, DirectoryExt.GetDirectories(baseDirectory).Length));
                tempBaseFolder = currPath;
            }
            else
                currPath = baseDirectory;
            //  || currPath.Contains(FrmSettings.TbxDomImageDir.Text) && Contact == ContactType.Random) TODO
            while (currPath.Contains("#Contact") && Contact == ContactType.Random)
            {
                if (Contact == ContactType.Random & !newFolder)
                {
                    currPath = DirectoryExt.GetDirectories(baseDirectory).ElementAt(_random.Next(0, DirectoryExt.GetDirectories(baseDirectory).Length));
                    tempBaseFolder = currPath;
                }
                else
                    currPath = baseDirectory;
            }
            List<string> subDirs = DirectoryExt.GetDirectories(currPath).ToList();
            if (subDirs.Contains(_settings.DomImageDirRand))
                subDirs.Remove(_settings.DomImageDirRand);
            List<string> dirListToExclude = new List<string>();
            foreach (string tempDir in subDirs)
            {
                if (tempDir.Substring(tempDir.LastIndexOf(@"\\") + 1).StartsWith("#"))
                    dirListToExclude.Add(tempDir);
            }
            foreach (string tempDir in dirListToExclude)
                subDirs.Remove(tempDir);




            // Read all subdirectories in base folder.
            // subDirs = myDirectory.GetDirectories(currPath).ToList
            List<string> exclude = new List<string>();
        nextSubDir:
            ;

            // Check if there are folders left.
            if (subDirs.Count == 0 & exclude.Count > 0)
            {
                string first = RecentFolders[0];
                RecentFolders.Remove(first);
                exclude.Remove(first);
                subDirs.Add(first);
            }
            else if (subDirs.Count <= 0 & exclude.Count == 0)
                throw new DirectoryNotFoundException("There are no subdirectories containing images in \"" + currPath + "\".");

            // Get a random folder in base directory.
            string rndFolder = subDirs[_random.Next(0, subDirs.Count)];

            if (RecentFolders.Contains(rndFolder))
            {
                exclude.Add(rndFolder);
                subDirs.Remove(rndFolder);
                goto nextSubDir;
            }

            // Read all imagefiles in random folder.
            List<string> imageFiles = new List<string>();

            if (_settings.CBSlideshowSubDir)
                imageFiles = DirectoryExt.GetFilesImages(rndFolder, SearchOption.AllDirectories);
            else
                imageFiles = DirectoryExt.GetFilesImages(rndFolder, SearchOption.TopDirectoryOnly);

            if (imageFiles.Count <= 0)
            {
                // No imagefiles in subdirectory. Remove from list and try next folder
                subDirs.Remove(rndFolder);
                goto nextSubDir;
            }
            else
            {
                // Imagefiles found -> Everything fine and done
                RecentFolders.Add(rndFolder);
                ImageFolder = currPath;
                return imageFiles;
            }
        }

        public string getDefaultFolder()
        {
            switch (Contact)
            {
                case ContactType.Domme:
                    return _settings.DomImageDir;

                case ContactType.Contact1:
                    return _settings.Contact1ImageDir;

                case ContactType.Contact2:
                    return _settings.Contact2ImageDir;

                case ContactType.Contact3:
                    return _settings.Contact3ImageDir;

                case ContactType.Random:
                    return _settings.RandomImageDir;
            }
            throw new Exception("Unknown contact type");
        }

        public string getCurrentBaseFolder()
        {
            if (!string.IsNullOrEmpty(tempBaseFolder))
                return tempBaseFolder;
            else
                return getDefaultFolder();
        }

        public void SetBaseFolder(string path)
        {
            //Settings.Default[getMySettingsMember(tp)] = path;
            switch (Contact)
            {
                case ContactType.Domme:
                    _settings.DomImageDir = path; break;

                case ContactType.Contact1:
                    _settings.Contact1ImageDir = path; break;

                case ContactType.Contact2:
                    _settings.Contact2ImageDir = path; break;

                case ContactType.Contact3:
                    _settings.Contact3ImageDir = path; break;

                case ContactType.Random:
                    _settings.RandomImageDir = path; break;
                default:
                    throw new Exception("Unknown contact type");
            }
        }



        public void LoadNew(bool newFolder)
        {
            this.ImageList = GetRandom(newFolder);
            this.Index = 0;
            ImageTagCache.Clear();
            LastTaggedImage = "";
        }

        public void CheckInit()
        {
            if (this.Index == -1 & this.Contact != ContactType.Nothing)
                this.LoadNew(false);
            LastTaggedImage = "";
        }


        [NonSerialized]
        private string LastTaggedImage;

        public string CurrentImage
        {
            get { 
                if (ImageList.Count > 0 & Index > -1)
                    return ImageList[Index];
                else
                    return string.Empty;
            }

        }

        public string NavigateFirst()
        {
            CheckInit();
            if (this.ImageList.Count > 0)
            {
                Index = 0;
                return CurrentImage;
            }
            else
                return string.Empty;
        }

        public string NavigateForward()
        {
            CheckInit();

            if (ImageList.Count <= 0)
            {
                // No Slideshow loaded
                Index = 0;
                return string.Empty;
            }
            else if (Index >= ImageList.Count - 1 && _settings.CBNewSlideshow)
                // End of Slideshow load new one
                LoadNew(_settings.CBRandomDomme);
            else if (Index >= ImageList.Count - 1)
                // End of Slideshow return last image
                Index = ImageList.Count - 1;
            else
                // Get next Image
                Index += 1;

            return CurrentImage;
        }

        public string NavigateNextTease()
        {
            CheckInit();

            if (_settings.CBSlideshowRandom)
                // get Random Image
                Index = _random.Next(0, ImageList.Count);
            else if (_settings.NextImageChance < _random.Next(0, 101))
            {
                // Randomly backwards
                Index -= 1;
                if (Index < 0)
                    Index = 0;
            }
            else if (Index >= ImageList.Count - 1 && _settings.CBNewSlideshow)
                // End of Slideshow start new
                LoadNew(_settings.CBRandomDomme);
            else if (Index >= ImageList.Count - 1)
                // End of Slideshow return last
                Index = ImageList.Count - 1;
            else
                // Next image
                Index += 1;

            return CurrentImage;
        }

        public string NavigateBackward()
        {
            CheckInit();

            if (ImageList.Count <= 0)
            {
                // No Slideshow loaded
                Index = 0;
                return string.Empty;
            }
            else if (Index != 0)
                // End of Slideshow return first image
                Index -= 1;
            else
                // Get next Image
                Index = 0;

            return CurrentImage;
        }

        public string NavigateLast()
        {
            CheckInit();
            if (this.ImageList.Count > 0)
            {
                Index = ImageList.Count - 1;
                return CurrentImage;
            }
            else
                return string.Empty;
        }


        /// <summary>Used to cache tagged image results. </summary>
        public class ImageTagCacheItem
        {
            public List<string> TagImageList = new List<string>();
            public string LastPicked = "";

            public ImageTagCacheItem()
            {
            }
        }

        /// ========================================================================================================= 
        /// 	''' <summary>
        /// 	''' Searches for a tagged with the given Tags.
        /// 	''' </summary>
        /// 	''' <param name="imageTags">The Tags, to search for.</param>
        /// 	''' <param name="rememberResult">If set to true the result is written to cache.</param>
        /// 	''' <returns>Returns a String representing the ImageLocation for the found image. If none was found it will 
        /// 	''' return an empty string.</returns>
        public string GetTaggedImage(string imageTags, bool rememberResult = false)
        {
            var GetTaggedImage = "";
            /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
            try
            {
                /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
                ImageTagCacheItem ImagePaths = GetImageListByTag(imageTags);

            tryNextImage:
                ;

                // ===================================================================
                // Get nearest Image 
                int CurrImgIndex = ImageList.IndexOf(CurrentImage);
                string rtnPath = "";
                int CurrDist = 999999;
            // this function was constantly giving the same pic over and over to me
            // i just changed to give a random image with the required tag.
            // now it correctly changes the pic and avoid the repetition over and over

            // For Each str As String In ImagePaths.TagImageList
            // Dim IndexInList As Integer = ImageList.IndexOf(str)
            // Calculate the distance of ListIndex from the FoundFile to CurrentImage
            // Dim FileDist As Integer = IndexInList - CurrImgIndex
            // Convert negative values to positive by multipling (-) x (-) = (+) 
            // If FileDist < 0 Then FileDist *= -1
            // Check if the distance is bigger than the previous one
            // If FileDist <= CurrDist Then
            // Yes: We will set this file and save its distance
            SetForwardImage:
                ;

                // rtnPath = str
                // CurrDist = FileDist
                // ElseIf ImagePaths.LastPicked = rtnPath AndAlso New Random().Next(0, 101) > 60 Then
                // The last Picked image is the same as last time.
                // GoTo SetForwardImage
                // Else
                // As the list is in the Same order as the Slideshow-List,
                // we can stop searching, when the value is getting bigger.
                // Exit For
                // End If
                // Next
                if (ImagePaths.TagImageList.Count != 0)
                    rtnPath = ImagePaths.TagImageList.ElementAt(_random.Next(0, ImagePaths.TagImageList.Count));
                else
                    rtnPath = ImagePaths.LastPicked;
                // ===================================================================
                // Check result
                if (string.IsNullOrWhiteSpace(rtnPath))
                    // ############### List was empty ################
                    return GetTaggedImage;
                else if (!File.Exists(rtnPath))
                {
                    // ############### Image Not found ###############
                    /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
                    ImageTagCache[imageTags].TagImageList.Remove(rtnPath);
                    goto tryNextImage;
                }
                else
                {
                    // ################ image found ##################
                    /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
                    if (rememberResult)
                        ImagePaths.LastPicked = rtnPath;
                    LastTaggedImage = rtnPath;
                    return rtnPath;
                }
            }
            catch (Exception ex)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // All Errors
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                //Log.WriteError("Exception in GetDommeImage()", ex, "");
                throw;
            }
        }

        /// <summary>Returns a list of filepaths for the given tags.</summary>
        /// 	''' <param name="imageTags">The tags to retrieve the list.</param>
        /// 	''' <return>Returns a list of files for the given tags.</return>
        private ImageTagCacheItem GetImageListByTag(string imageTags)
        {

            try
            {
                /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
                if (string.IsNullOrWhiteSpace(CurrentImage))
                    return null;

                string TargetFolder = Path.GetDirectoryName(CurrentImage) + Path.DirectorySeparatorChar;
                string TagListFile = TargetFolder + "ImageTags.txt";

            redo:
                ;
                if (!File.Exists(TagListFile))
                    // ===================================================================
                    // No Tag File
                    return null;
                else if (ImageTagCache.Keys.Contains(imageTags))
                {
                    // ===================================================================
                    // Previous cached result

                    /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
                    ImageTagCacheItem rtnItem = ImageTagCache[imageTags];

                    if (rtnItem.TagImageList.Count == 0)
                        // ´############## List was empty ################
                        return null;
                    else if (!rtnItem.TagImageList[0].StartsWith(TargetFolder)
                     )
                    {
                        // ################ Wrong folder #################
                        ImageTagCache.Remove(imageTags);
                        goto redo;
                    }
                    else
                        // ################# All fine ####################
                        return rtnItem;
                }
                else
                {
                    // ===================================================================
                    // Read from File 
                    /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
                    List<string> Include = new List<string>();
                    List<string> Exclude = new List<string>();
                    List<string> PathList = Common.Txt2List(TagListFile);
                    string[] ValidExt = ".jpg|.jpeg|.bmp|.png|.gif".Split('|');

                    // Replace case insensitive "not", to safely assign tags to their lists
                    imageTags = Regex.Replace(imageTags, @"\b(not)", "--", RegexOptions.IgnoreCase);

                    // Seperate Tags in given string.
                    string[] S = imageTags.Split(new[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);

                    // Assign tags to lists.
                    S.ToList().ForEach(x =>
                    {
                        if (x.StartsWith("--"))
                            Exclude.Add(x.Replace("--", ""));
                        else
                            Include.Add(x);
                    });

                    // Filter the List.
                    PathList.RemoveAll(x =>
                    {
                    // Remove if given include tags are missing
                    foreach (string tag in Include)
                        {
                            if (!x.Contains(tag))
                                return true;
                        }
                    // Remove if given exclude tags are present
                    foreach (string notTag in Exclude)
                        {
                            if (x.Contains(notTag))
                                return true;
                        }
                    // Remove all without valid extension
                    string Ext = Path.GetExtension(x.Split()[0]).ToLower();
                        if (!ValidExt.Contains(Ext))
                            return true;
                    // Everything fine keep file
                    return false;
                    });

                    // ############################### Extract Filepaths ###############################
                    // Extract filepaths from list. An empty list doesn't matter here.
                    Regex re = new Regex(@"(?:^.*(?:\.jpg|jpeg|png|bmp|gif)){1}", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    // Get the Matches. Since we can't search a generic list, we join it. 
                    MatchCollection mc = re.Matches(string.Join(Environment.NewLine, PathList));

                    // Write the the ImagePaths back to list.
                    PathList.Clear();
                    foreach (Match ma in mc)
                        PathList.Add(TargetFolder + ma.Value);

                    // Add new item to cache and exit.
                    var r = new ImageTagCacheItem() { TagImageList = PathList };
                    ImageTagCache.Add(imageTags, r);
                    return r;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public string ApplyTextedTags(string modifyString)
        {
            var result = modifyString;
            try
            {
                // ################### Get displayed Image #####################
                string DisplayedImage;

                if (string.IsNullOrWhiteSpace(LastTaggedImage))
                    DisplayedImage = CurrentImage;
                else
                    DisplayedImage = LastTaggedImage;

                // #################### Get line for image #####################
                string TagFilePath = Path.GetDirectoryName(DisplayedImage) + @"\ImageTags.txt";
                string FileName = Path.GetFileName(DisplayedImage);

                if (!File.Exists(TagFilePath))
                    return result;

                // Read tagfile and find line for displayed image.
                string Line = Common.Txt2List(TagFilePath).Find(x => x.StartsWith(FileName, StringComparison.OrdinalIgnoreCase));
                if (Line == null)
                    return result;

                // ################### Create Regex Pattern ####################
                // 
                // Named Group <ident> is the identifier for replacement. Joined from StringArray.
                // TagGarment is used twice. Therefore it searches for "TagGarment" not followed by "Covering".
                // 
                // Named group <value> is the value to replace the identifier with. 	Allows all chars except whitespaces.
                string Pattern = string.Format(@"(?<ident>{0})(?<value>[^\s]+)", string.Join("|", new[] { "TagGarment(?!Covering)", "TagUnderwear", "TagTattoo", "TagSexToy", "TagFurniture" }));

                // ################ Find and replace matches ###################
                Regex re = new Regex(Pattern, RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(Line);

                foreach (Match Tag in mc)
                    result = result.Replace("#" + Tag.Groups["ident"].Value, Tag.Groups["value"].Value.Replace("-", " "));

                // Replace remaining tag-keywords to mask missing tags.
                result = result.Replace("#Tag", "");
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}