using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Tai.Common.Images
{

    public enum ImageSourceType
    {
        Local,
        Remote
    }

    public enum ImageGenre
    {
        Blog,
        Butt,
        Boobs,
        Hardcore,
        Softcore,
        Lesbian,
        Blowjob,
        Femdom,
        Lezdom,
        Hentai,
        Gay,
        Maledom,
        Captions,
        General,
        Liked,
        Disliked
    }

    /// <summary>
    ///     ''' Represents a Object which can store all necessary Data related to genere-Images. 
    ///     ''' This obejct is intended for managing Images. All Data and conditions can be stored in here
    ///     ''' and retrieved from it.
    ///     ''' </summary>
    public interface ImageSettings
    {
        string LikedImageUrlsPath { get;  }
        string DislikedImageUrlsPath { get; }
        bool ForceLocalGif { get; }
        bool DisableLocalGif { get;}
        bool ForceRemoteGif { get;  }
        bool DisableRemoteGif { get;  }
        string NoLocalImagesFoundPath { get; }
        string NoUrlFilesSelectedPath { get; }
        string ImageTagListPath { get;  }

        IEnumerable<string> GetCheckedBlogImages();
        /*
        {
            throw new NotImplementedException();
        }
        */
        string UrlFileDirPath { get; } //Application.StartupPath & "\Images\System\URL Files\"
        string SystemImagesPath { get; } // Application.StartupPath & "\Images\System

        bool NoPornAllowed { get; }
        string LocalImageTagsPath { get; } // //Application.StartupPath + @"\Images\System\LocalImageTags.txt";
    }
}


namespace Tai.Common.Images
{
    public class ImageData
    {
        internal readonly ISettings _settings;
        internal readonly ImageSettings _imageSettings;
        internal readonly Random _random;
        internal readonly ILog _log;

        public ImageData(ISettings settings, ImageSettings imageSettings, Random random, ILog log)
        {
            _settings = settings;
            _imageSettings = imageSettings;
            _random = random;
            _log = log;

        }
        public ImageDataContainer GetImageData(ImageGenre genre)
        {
            Dictionary<ImageGenre, ImageDataContainer> tmpObj = GetImageData();

            return tmpObj[genre];
        }

        /// =========================================================================================================
        ///     ''' <summary>
        ///     ''' Gets a dictionary which contains all nessecary data of genere-images.
        ///     ''' </summary>
        ///     ''' <returns>Returns a dictionary which contains all nessecary data of genere-images.</returns>
        public Dictionary<ImageGenre, ImageDataContainer> GetImageData()
        {
            Dictionary<ImageGenre, ImageDataContainer> rtnDic = new Dictionary<ImageGenre, ImageDataContainer>(); // (StringComparer.OrdinalIgnoreCase)
            {
                var withBlock = rtnDic;
                withBlock.Add(ImageGenre.Blog, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Blog,
                });
                withBlock.Add(ImageGenre.Liked, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Liked,
                });
                withBlock.Add(ImageGenre.Disliked, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Disliked,
                });

                withBlock.Add(ImageGenre.Butt, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Butt,
                    LocalDirectory = _settings.CBIButts ? _settings.LBLButtPath : "",
                    LocalSubDirectories = _settings.CBButtSubDir,
                    UrlFile = _settings.UrlFileButtEnabled ? _settings.UrlFileButt : "",
                });

                withBlock.Add(ImageGenre.Boobs, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Boobs,
                    LocalDirectory = _settings.CBIBoobs ? _settings.LBLBoobPath : "",
                    LocalSubDirectories = _settings.CBBoobSubDir,
                    UrlFile = _settings.UrlFileBoobsEnabled ? _settings.UrlFileBoobs : "",
                });

                withBlock.Add(ImageGenre.Hardcore, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Hardcore,
                    LocalDirectory = _settings.CBIHardcore ? _settings.IHardcore : "",
                    LocalSubDirectories = _settings.IHardcoreSD,
                    UrlFile = _settings.UrlFileHardcoreEnabled ? _settings.UrlFileHardcore : "",
                });

                withBlock.Add(ImageGenre.Softcore, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Softcore,
                    LocalDirectory = _settings.CBISoftcore ? _settings.ISoftcore : "",
                    LocalSubDirectories = _settings.ISoftcoreSD,
                    UrlFile = _settings.UrlFileSoftcoreEnabled ? _settings.UrlFileSoftcore : "",
                });

                withBlock.Add(ImageGenre.Lesbian, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Lesbian,
                    LocalDirectory = _settings.CBILesbian ? _settings.ILesbian : "",
                    LocalSubDirectories = _settings.ILesbianSD,
                    UrlFile = _settings.UrlFileLesbianEnabled ? _settings.UrlFileLesbian : "",
                });

                withBlock.Add(ImageGenre.Blowjob, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Blowjob,
                    LocalDirectory = _settings.CBIBlowjob ? _settings.IBlowjob : "",
                    LocalSubDirectories = _settings.IBlowjobSD,
                    UrlFile = _settings.UrlFileBlowjobEnabled ? _settings.UrlFileBlowjob : "",
                });

                withBlock.Add(ImageGenre.Femdom, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Femdom,
                    LocalDirectory = _settings.CBIFemdom ? _settings.IFemdom : "",
                    LocalSubDirectories = _settings.IFemdomSD,
                    UrlFile = _settings.UrlFileFemdomEnabled ? _settings.UrlFileFemdom : "",
                });

                withBlock.Add(ImageGenre.Lezdom, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Lezdom,
                    LocalDirectory = _settings.CBILezdom ? _settings.ILezdom : "",
                    LocalSubDirectories = _settings.ILezdomSD,
                    UrlFile = _settings.UrlFileLezdomEnabled ? _settings.UrlFileLezdom : "",
                });

                withBlock.Add(ImageGenre.Hentai, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Hentai,
                    LocalDirectory = _settings.CBIHentai ? _settings.IHentai : "",
                    LocalSubDirectories = _settings.IHentaiSD,
                    UrlFile = _settings.UrlFileHentaiEnabled ? _settings.UrlFileHentai : "",
                });

                withBlock.Add(ImageGenre.Gay, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Gay,
                    LocalDirectory = _settings.CBIGay ? _settings.IGay : "",
                    LocalSubDirectories = _settings.IGaySD,
                    UrlFile = _settings.UrlFileGayEnabled ? _settings.UrlFileGay : "",
                });

                withBlock.Add(ImageGenre.Maledom, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Maledom,
                    LocalDirectory = _settings.CBIMaledom ? _settings.IMaledom : "",
                    LocalSubDirectories = _settings.IMaledomSD,
                    UrlFile = _settings.UrlFileMaledomEnabled ? _settings.UrlFileMaledom : "",
                });

                withBlock.Add(ImageGenre.Captions, new ImageDataContainer(this)
                {
                    Name = ImageGenre.Captions,
                    LocalDirectory = _settings.CBICaptions ? _settings.ICaptions : "",
                    LocalSubDirectories = _settings.ICaptionsSD,
                    UrlFile = _settings.UrlFileCaptionsEnabled ? _settings.UrlFileCaptions : "",
                });

                withBlock.Add(ImageGenre.General, new ImageDataContainer(this)
                {
                    Name = ImageGenre.General,
                    LocalDirectory = _settings.CBIGeneral ? _settings.IGeneral : "",
                    LocalSubDirectories = _settings.IGeneralSD,
                    UrlFile = _settings.UrlFileGeneralEnabled ? _settings.UrlFileGeneral : "",
                });
            }

            return rtnDic;
        }

        /// <summary>
        ///     ''' Gets a random image URI for given Genre from Local and URL-Files.
        ///     ''' </summary>
        ///     ''' <param name="genre">Determines the Genre to get a random image for.</param>
        ///     ''' <returns>The URI of a random Image.</returns>
        internal string GetRandomImage(ImageGenre genre)
        {
            // Get all definitions for Images.
            Dictionary<ImageGenre, ImageDataContainer> dicFilePaths = GetImageData();

            // get an Random Image from the Random Genre.
            return dicFilePaths[genre].Random();
        }

        /// <summary>
        ///     ''' Gets a random image URI for the given sourcetype (URL or Local).
        ///     ''' </summary>
        ///     ''' <param name="source">Determines the source to get a random image for.</param>
        ///     ''' <returns>The URI of a random Image.</returns>
        internal string GetRandomImage(ImageSourceType source)
        {
            // Get all definitions for Images.
            Dictionary<ImageGenre, ImageDataContainer> dicFilePaths = GetImageData();

            List<string> allImages = new List<string>();

            // Fetch all available ImageLocations for sourceType
            foreach (var genre in dicFilePaths.Keys)
            {
                allImages.AddRange(dicFilePaths[genre].ToList(source));
            }

            // Check if Genres are present.
            if (allImages.Count == 0)
                goto NoNeFound;

            // get an Random Image for the given SourceType
            return allImages[_random.Next(0, allImages.Count)];
        NoNeFound:
            ;

            // Return an Error-Image FilePath
            if (source == ImageSourceType.Local)
                return _imageSettings.NoLocalImagesFoundPath;
            else
                return _imageSettings.NoUrlFilesSelectedPath;
        }

        /// <summary>
        ///     ''' Gets a random image URI for the given genre and sourcetype (URL or Local)..
        ///     ''' </summary>
        ///     ''' <param name="genre">Determines the genre to get a random image for.</param>
        ///     ''' <param name="source">Determines the source to get a random image for.</param>
        ///     ''' <returns>The URI of a random Image.</returns>
        internal string GetRandomImage(ImageGenre genre, ImageSourceType source)
        {
            // Get all definitions for Images.
            Dictionary<ImageGenre, ImageDataContainer> dicFilePaths = GetImageData();

            // Check if the given Genre is found.
            if (dicFilePaths.Keys.Contains(genre))
                // get an Random Image
                return dicFilePaths[genre].Random(source);
            else
                // Return the Error-Image FilePath
                if (source == ImageSourceType.Local)
                return _imageSettings.NoLocalImagesFoundPath;
            else
                return _imageSettings.NoUrlFilesSelectedPath;
        }

        /// <summary>
        /// 	''' Gets a random image URI from all available Sources
        /// 	''' </summary>
        /// 	''' <returns>The URI of a random Image.</returns>
        internal string GetRandomImage()
        {
            // Get all definitions for Images.
            Dictionary<ImageGenre, ImageDataContainer> dicFilePaths = GetImageData();
            List<string> AllImages = new List<string>();

            // Fetch all available ImageLocations
            foreach (ImageGenre genre in dicFilePaths.Keys)
                AllImages.AddRange(dicFilePaths[genre].ToList());

            // Check if there are images
            if (AllImages.Count == 0)
                return _imageSettings.NoLocalImagesFoundPath;

            // get an Random Image from the all available Locations
            return AllImages[_random.Next(0, AllImages.Count)];
        }

        /// =========================================================================================================
        /// 	''' <summary>
        /// 	''' Removes the given path from the LocalImageTag file.
        /// 	''' </summary>
        /// 	''' <param name="path">The Path/Url to remove from the file.</param>
        internal int RemoveFromUrlFiles(string path)
        {
            int rtnInt = 0;

            List<string> CustomURLFileList = new List<string>();

            // Get all URL-Files associated with image-Genres 
            // Their location can be outside of \Images\System\URL Files\
            foreach (ImageDataContainer imgDC in GetImageData().Values)
            {
                if (imgDC.UrlFile != null && imgDC.UrlFile != "")
                    CustomURLFileList.Add(imgDC.UrlFile);
            }

            // Remove all, where the directory does not exists.
            CustomURLFileList.RemoveAll(x => !Directory.Exists(Path.GetDirectoryName(x)));
            // Remove all, where the file itself does not exists.
            CustomURLFileList.RemoveAll(x => !File.Exists(x));

            // Find the URL in all URLFiles located in Standard Directory

            // Delete the URL from all Files 
            foreach (string filePath in CustomURLFileList)
            {
                rtnInt += Common.TxtRemoveLine(filePath, path);
            }

            return rtnInt;
        }


        /// =========================================================================================================
        /// 	''' <summary>
        /// 	''' Removes the given path from the likedImage file.
        /// 	''' </summary>
        /// 	''' <param name="path">The Path/Url to remove from the file.</param>
        /// 	''' <return>The number of lines deleted.</return>
        internal int RemoveFromLikeList(string path)
        {
            return Common.TxtRemoveLine(_imageSettings.LikedImageUrlsPath, path);
        }

        /// =========================================================================================================
        /// 	''' <summary>
        /// 	''' Removes the given path from the DislikedImage file.
        /// 	''' </summary>
        /// 	''' <param name="path">The Path/Url to remove from the file.</param>
        /// 	''' <return>The number of lines deleted.</return>
        internal int RemoveFromDislikeList(string path)
        {
            return Common.TxtRemoveLine(_imageSettings.DislikedImageUrlsPath, path);
        }

        /// =========================================================================================================
        /// 	''' <summary>
        /// 	''' Removes the given path from the LocalImageTag file.
        /// 	''' </summary>
        /// 	''' <param name="path">The Path/Url to remove from the file.</param>
        /// 	''' <return>The number of lines deleted.</return>
        internal int RemoveFromLocalTagList(string path)
        {
            return Common.TxtRemoveLine(_imageSettings.ImageTagListPath, path);
        }

        /// =========================================================================================================
        /// 	''' <summary>
        /// 	''' disposes the resources from the Main Picturebox and deletes the current image from Filesystem including 
        /// 	''' TagList, LikedList and DislikedList.Applies only to Images not loacted in DommeImageDir or ContactImageDirs.
        /// 	''' <para>Rethrows all Exceptions!</para>
        /// 	''' <param name="restrictToLocal">Set TRUE to delete only LocalImages. Set to False to delete local 
        /// 	''' images as well as remote image links</param>
        /// 	''' </summary>
        private void DeleteImage(string imageLocation, bool restrictToLocal)
        {
            if (imageLocation == null)
                throw new ArgumentException("The given path was empty.");
            if (imageLocation == "")
                throw new ArgumentException("The given path was empty.");
            try
            {
                if (Common.IsUrl(imageLocation))
                {
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    // Online Images
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    Debug.Print("××××××××××××××××××××××× Deleting remote Image: " + imageLocation + "×××××××××××××××××××××××");

                    if (restrictToLocal)
                        throw new ArgumentException("Can't delete remote files!");
                    int rtnInt = 0;
                    // #################### Remove from LikeList ####################
                    rtnInt += RemoveFromLikeList(imageLocation);
                    // ################## Remove from DislikeList ###################
                    rtnInt += RemoveFromDislikeList(imageLocation);
                    // ################## Remove from LocalTagList ##################
                    rtnInt += RemoveFromLocalTagList(imageLocation);
                    // #################### Remove from URL-Lists ###################
                    rtnInt += RemoveFromUrlFiles(imageLocation);

                    // Save the Path temporary -> CLearMainPictureBox will flush ImageLocation
                    string tmpPath = imageLocation;
                    // Dispose the Image from RAM
                    //ClearMainPictureBox();
                    if (rtnInt < 1)
                        throw new Exception("The URL was not successfully deleted.");
                }
                else
                {
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    // Local Images
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    Debug.Print("××××××××××××××××××××××× Deleting local Image: " + imageLocation + "×××××××××××××××××××××××");

                    var fullPath = Path.GetFullPath(imageLocation);
                    if (fullPath.StartsWith(_imageSettings.SystemImagesPath, StringComparison.CurrentCultureIgnoreCase))
                        throw new ArgumentException("System images are not allowed to delete.");
                    if (fullPath.StartsWith(_settings.DomImageDir))
                        throw new ArgumentException("Domme-Slideshow images are not allowed to delete!");
                    if (fullPath.StartsWith(_settings.RandomImageDir))
                        throw new ArgumentException("Domme-Random Slideshow images are not allowed to delete!");
                    if (fullPath.StartsWith(_settings.Contact1ImageDir, StringComparison.CurrentCultureIgnoreCase))
                        throw new ArgumentException("Contact2-Slideshow images are not allowed to delete!");
                    if (fullPath.StartsWith(_settings.Contact2ImageDir, StringComparison.CurrentCultureIgnoreCase))
                        throw new ArgumentException("Contact2-Slideshow images are not allowed to delete!");
                    if (fullPath.StartsWith(_settings.Contact3ImageDir, StringComparison.CurrentCultureIgnoreCase))
                        throw new ArgumentException("Contact3-Slideshow images are not allowed to delete!");

                    // #################### Remove from ####################
                    RemoveFromLikeList(imageLocation);
                    // ################## Remove from DislikeList ###################
                    RemoveFromDislikeList(imageLocation);
                    // ################## Remove from LocalTagList ##################
                    RemoveFromLocalTagList(imageLocation);

                    // Save the Path temporary -> CLearMainPictureBox will flush ImageLocation
                    string tmpPath = imageLocation;
                    // Dispose the Image from RAM
                    // Delete the File from disk
                    File.Delete(tmpPath);

                    /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
                    if (File.Exists(tmpPath))
                        throw new Exception("The image was not successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // All Errors
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                throw;
            }
        }

        public string GetLocal(string LocTag,  bool relativePaths = false, string selectionType = "All")
        {
            return GetLocalImage(LocTag, _imageSettings.LocalImageTagsPath, false, selectionType);
        }

        public string GetLocalImageRelative(string LocTag, string path, bool relativePaths = false, string selectionType = "All")
        {
            //if (isDommeTag)
            //    fileToCheck = Path.GetDirectoryName(ssh.contactToUse.CurrentImage) + Path.DirectorySeparatorChar + "ImageTags.txt";

            return GetLocalImage(LocTag, path, true, selectionType);
        }

        private string GetTagPathText(string picLine)
        {

            foreach (var ext in new[] { ".JPG", ".JPEG", ".PNG", ".BMP", ".GIF" })
            {
                var idx = picLine.IndexOf(ext, StringComparison.OrdinalIgnoreCase);
                if (idx >= 0)
                    return picLine.Substring(0, idx + ext.Length);
            }
            return string.Empty;
        }

        public string GetLocal(List<string> IncludeTags = null, List<string> ExcludeTags = null)
        {
            if (File.Exists(_imageSettings.LocalImageTagsPath))
            {
                // Read all lines of given file.
                var  localTagImageList = Common.Txt2List(_imageSettings.LocalImageTagsPath);


                localTagImageList.RemoveAll(x =>
                {
                    // Remove if given include tags are missing
                    if (IncludeTags != null)
                    {
                        foreach (string tag in IncludeTags)
                        {
                            if (!x.Contains(tag.Replace("@", "")))
                                return true;
                        }
                    }
                    // Remove if given exclude tags are present
                    if (ExcludeTags != null)
                    {
                        foreach (string tag in ExcludeTags)
                        {
                            if (x.Contains(tag.Replace("@", "")))
                                return true;
                        }
                    }
                    // Remove all without valid extension (this says no spaces in file names)
                    return string.IsNullOrEmpty(GetTagPathText(x));
                });

                while (localTagImageList.Count > 0)
                {
                    int rndNumber = _random.Next(0, localTagImageList.Count);
                    // TODO: GetLocalImage: Add space char (0x20) support for filepaths.
                    string Filepath = GetTagPathText(localTagImageList[rndNumber]);

                    if (File.Exists(Filepath))
                        return Filepath;
                    else
                        localTagImageList.RemoveAt(rndNumber);
                }
            }

            return string.Empty;
        }


        private string GetLocalImage(string LocTag, string path, bool relativePaths = false, string selectionType = "All")
        {
            string fileToCheck = path; //Application.StartupPath + @"\Images\System\LocalImageTags.txt";

            // TODO-Next: @ImageTag() Implement optimized @ShowTaggedImage code.
            if (File.Exists(fileToCheck))
            {
                List<string> TagList = new List<string>();
                TagList = Common.Txt2List(fileToCheck);

                LocTag = LocTag.Replace(" ,", ",");
                LocTag = LocTag.Replace(", ", ",");

                string[] LocTagArray = LocTag.Split(',');

                List<string> TaggedList = new List<string>();

                switch (selectionType)
                {
                    case "Or":
                        {
                            for (int n = 0; n <= LocTagArray.Count() - 1; n++)
                            {
                                if (TaggedList.Count == 0)
                                {
                                    for (int i = 0; i <= TagList.Count - 1; i++)
                                    {
                                        if (TagList[i].Contains(LocTagArray[n]))
                                            TaggedList.Add(TagList[i]);
                                    }
                                }
                            }

                            break;
                        }

                    case "Any":
                        {
                            for (int i = 0; i <= TagList.Count - 1; i++)
                            {
                                for (int n = 0; n <= LocTagArray.Count() - 1; n++)
                                {
                                    if (TagList[i].Contains(LocTagArray[n]))
                                        TaggedList.Add(TagList[i]);
                                }
                            }

                            break;
                        }

                    case "First":
                        {
                            for (int i = 0; i <= TagList.Count - 1; i++)
                            {
                                for (int n = 1; n <= LocTagArray.Count() - 1; n++)
                                {
                                    if (!TagList[i].Contains(LocTagArray[n]) || !TagList[i].Contains(LocTagArray[0]))
                                    {
                                        TaggedList.Add(TagList[i]);
                                        break;
                                    }
                                }
                                if (TaggedList.Count > 0)
                                    break;
                            }

                            break;
                        }

                    default:
                        {
                            bool addImg = false;
                            for (int i = 0; i <= TagList.Count - 1; i++)
                            {
                                for (int n = 0; n <= LocTagArray.Count() - 1; n++)
                                {
                                    if (!TagList[i].Contains(LocTagArray[n]))
                                    {
                                        addImg = false;
                                        break;
                                    }
                                    else
                                    {
                                        addImg = true;
                                        continue;
                                    }
                                }
                                if (addImg)
                                    TaggedList.Add(TagList[i]);
                            }

                            break;
                        }
                }

                if (TaggedList.Count > 0)
                {
                    // imagetags format is space separated: <file> <tag> <tag*> 
                    //  but file can also contain spaces.
                    var picLine = TaggedList[_random.Next(0, TaggedList.Count)];
                    var picFile = string.Empty;

                    var idx = -1;
                    foreach (var ext in new[] { ".JPG", ".JPEG", ".PNG", ".BMP", ".GIF" })
                    {
                        idx = picLine.IndexOf(ext, StringComparison.OrdinalIgnoreCase);
                        if (idx >= 0)
                        {
                            picFile = picLine.Substring(0, idx + ext.Length);
                        }
                    }
                    if (idx >= 0)
                    {

                        if (relativePaths)
                        {
                            var folder = Path.GetDirectoryName(path);
                            return Path.Combine(folder, picFile);
                        }
                        return picFile;
                    }
                }
            }
            return string.Empty;
        }


    }

    public class ImageDataContainer
    {
        private readonly ILog _log;
        //Friend Shared ReadOnly _imageSettings.UrlFileDirPath As String = Application.StartupPath & "\Images\System\URL Files\"
        public ImageDataContainer(ISettings settings, ImageSettings imageSettings, Random random, ILog log)
        {
            _settings = settings;
            _imageSettings = imageSettings;
            _random = random;
            _log = log;
        }
        public ImageDataContainer(ImageData parent)
        {
            _settings = parent._settings;
            _imageSettings = parent._imageSettings;
            _random = parent._random;
            _log = parent._log;
        }


        // TODO: ImageDataContainer Improve the usage of System Ressources.
        public ImageGenre Name;

        /// =========================================================================================================
		/// 		''' <summary>
		/// 		''' Stores the absolute or relative Url-File-Path.
		/// 		''' <para>Do not access this direct. Use the Property instead!</para>
		/// 		''' </summary>
        private string _URLFile = "";
        /// <summary>
		/// 		''' Gets or sets the Url-File-Path. Make sure you set this value with extension. Otherwise the value won't 
		/// 		''' be accepted. The filepath can be a relatative or an absolute. A relative path will rooted with the 
		/// 		''' standard URL-File directory
		/// 		''' </summary>
		/// 		''' <returns>The absolute Url-File-Path. </returns>
        public string UrlFile
        {
            get
            {
                if (_URLFile == "")
                    return _URLFile;
                if (_URLFile.ToLower().EndsWith(".txt") == false)
                    return "";

                if (Path.IsPathRooted(_URLFile))
                    // Return an absolute FilePath
                    return _URLFile;
                else
                    // Return the relative path absolute.
                    return Path.Combine(_imageSettings.UrlFileDirPath, _URLFile);
            }
            set
            {
                if (value == null)
                    _URLFile = "";
                else if (value.ToLower().EndsWith(".txt") == false)
                    _URLFile = "";
                else
                    _URLFile = value;
            }
        }

        public string LocalDirectory = "";
        public bool LocalSubDirectories = false;
        public bool OfflineMode => _settings.OfflineMode;
        public bool SYS_NoPornAllowed => _imageSettings.NoPornAllowed;
        private readonly ISettings _settings;
        private readonly ImageSettings _imageSettings;
        private readonly Random _random;

        public int CountImages()
        {
            return ToList().Count;
        }

        public int CountImages(ImageSourceType type)
        {
            return ToList(type).Count;
        }

        /// =========================================================================================================
		/// 		''' <summary>
		/// 		''' Checks if there are images available for the current Genre.
		/// 		''' </summary>
		/// 		''' <returns></returns>
        public bool IsAvailable()
        {
            if (CountImages() > 0)
                return true;
            else
                return false;
        }

        public bool IsAvailable(ImageSourceType Type)
        {
            if (CountImages(Type) > 0)
                return true;
            else
                return false;
        }

        /// =========================================================================================================
        ///         ''' <summary>
        ///         ''' Returns a List of FilePaths or URLs.
        ///         ''' </summary>
        ///         ''' <returns>Returns a List Containing all Found Links. If none are 
        ///         ''' Found an empty List is returned</returns>
        public List<string> ToList()
        {
            List<string> rtnList = new List<string>();
            try
            {
                // If no Porn is allowed, then return Empty
                if (SYS_NoPornAllowed)
                    return rtnList;

                rtnList.AddRange(ToList(ImageSourceType.Local));

                // Load only LocalFiles if Oflline-Mode is acivated.
                if (OfflineMode == false)
                    rtnList.AddRange(ToList(ImageSourceType.Remote));
            }
            catch (Exception ex)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // All Errors
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                _log.WriteError("Failed to fetch ImageList for genre." + Name.ToString() + " : " + ex.Message, ex, "Exception at: " + Name.ToString() + ".ToList()");
                return new List<string>();
            }
            return rtnList;
        }

        /// =========================================================================================================
		/// 		''' <summary>
		/// 		''' Returns a List of FilePaths or URLs for the given Type.
		/// 		''' </summary>
		/// 		''' <param name="Type">The LoationType to Search for.</param>
		/// 		''' <returns>Returns a List Containing all Found Links. If none are 
		/// 		''' Found an empty List is returned</returns>
        public List<string> ToList(ImageSourceType Type)
        {
            List<string> rtnList = new List<string>();

            try
            {
                // If no Porn is allowed, then return Empty
                if (SYS_NoPornAllowed)
                    return rtnList;

                // If offline mode is activated then search only for local files.
                if (OfflineMode)
                    Type = ImageSourceType.Local;

                if (Name == ImageGenre.Blog)
                {
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    // Blog Images
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    if (OfflineMode | Type == ImageSourceType.Local)
                        goto exitEmpty;
                    List<string> tmpList = new List<string>();

                    // Load threadsafe all checked Items into List.
                    //object temp = FrmSettings.UIThread(() =>
                    // {
                    //    return FrmSettings.URLFileList.CheckedItems.Cast<string>.ToList;
                    //});



                    tmpList.AddRange(_imageSettings.GetCheckedBlogImages()); // change to full path ??

                    // Remove Items where File does not exist.

                    //tmpList.RemoveAll(x => !File.Exists(Application.StartupPath + @"\Images\System\URL Files\" + x + ".txt"));

                    // Check Result if Files in List
                    if (tmpList.Count < 1)
                        goto exitEmpty;

                    foreach (string fileName in tmpList)
                    {
                        // Read the URL-File
                        List<string> addList = Common.Txt2List(Path.Combine(_imageSettings.UrlFileDirPath, fileName + ".txt"));

                        // add lines from file
                        rtnList.AddRange(addList);
                    }
                }
                else if (Name == ImageGenre.Liked)
                {
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    // Liked Images
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    try
                    {
                        //if (File.Exists(  Application.StartupPath + @"\Images\System\LikedImageURLs.txt"))
                        //try
                        {
                            List<string> addlist = Common.Txt2List(_imageSettings.LikedImageUrlsPath);

                            // Remove all URLs if Offline-Mode is activated
                            if (OfflineMode | Type == ImageSourceType.Local)
                                addlist.RemoveAll(x => Common.IsUrl(x));

                            rtnList.AddRange(addlist);
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.WriteError(ex.Message, ex, "Error occured while loading Likelist");
                        goto exitEmpty;
                    }
                }
                else if (Name == ImageGenre.Disliked)
                {
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    // Disliked Images
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    try
                    {
                        //if (File.Exists(Application.StartupPath + @"\Images\System\DislikedImageURLs.txt"))
                        {
                            List<string> addlist = Common.Txt2List(_imageSettings.DislikedImageUrlsPath);

                            // Remove all URLs if Offline-Mode is activated
                            if (OfflineMode | Type == ImageSourceType.Local)
                                addlist.RemoveAll(x => Common.IsUrl(x));

                            rtnList.AddRange(addlist);
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.WriteError(ex.Message, ex, "Error occured while loading Dislikelist");
                        goto exitEmpty;
                    }
                }
                else
                {
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    // Regular Genres
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼

                    // Load Local ImageList
                    if (Type == ImageSourceType.Local && LocalDirectory != "" && LocalDirectory != null)
                    {
                        if (LocalSubDirectories == false)
                            rtnList.AddRange(DirectoryExt.GetFilesImages(LocalDirectory, SearchOption.TopDirectoryOnly));
                        else
                            rtnList.AddRange(DirectoryExt.GetFilesImages(LocalDirectory, SearchOption.AllDirectories));
                    }

                    // Load an URL-File
                    if (Type == ImageSourceType.Remote & UrlFile != "" & UrlFile != null)
                        rtnList.AddRange(Common.Txt2List(UrlFile));
                }


                if (Type == ImageSourceType.Local)
                {
                    //  My.Application.CommandLineArgs.Contains("-forceLocalGif")
                    if (_imageSettings.ForceLocalGif)
                        // Removae all non-Gif images
                        rtnList.RemoveAll(x => x.ToLower().EndsWith(".gif") == false);
                    else if (_imageSettings.DisableLocalGif) //  My.Application.CommandLineArgs.Contains("-disableLocalGif")
                        // Remove all gif images
                        rtnList.RemoveAll(x => x.ToLower().EndsWith(".gif"));
                }

                if (Type == ImageSourceType.Remote)
                {
                    if (_imageSettings.ForceRemoteGif) //My.Application.CommandLineArgs.Contains("-forceRemoteGif"))
                        // Removae all non-Gif images
                        rtnList.RemoveAll(x => x.ToLower().EndsWith(".gif") == false);
                    else if (_imageSettings.DisableRemoteGif) //  My.Application.CommandLineArgs.Contains("-disableRemoteGif"))
                        // Remove all gif images
                        rtnList.RemoveAll(x => x.ToLower().EndsWith(".gif"));
                }
            }
            catch (Exception ex)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // All Errors
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                _log.WriteError("Failed to fetch ImageList for genre." + Name.ToString() + " and Source." + Type.ToString() + " : " + ex.Message, ex, "Exception at: " + Name.ToString() + ".ToList(" + Type.ToString() + ")");
                return new List<string>();
            }

        exitEmpty:
            ;
            return rtnList;
        }

        /// =========================================================================================================
		/// 		''' <summary>
		/// 		''' Returns a random Image URL or Path from all Lists and directories.
		/// 		''' </summary>
		/// 		''' <returns>Returns a FilePath or URL. If there are no Images found the 
		/// 		''' "NoLocalImagesFound-Image-Filepath is returned.</returns>
        public string Random()
        {
            try
            {
                // Get List of Files
                List<string> tmpList = ToList();

                // Check if Links/Paths are present.
                if (tmpList.Count <= 0)
                    goto NoneFound;

                // Pick a Random Image-Path
                return tmpList[_random.Next(0, tmpList.Count)];
            }
            catch (Exception ex)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // All Errors
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                _log.WriteError(ex.Message + Environment.NewLine + ToString(), ex, "Error while choosing a random Image.");
            }

        NoneFound:
            ;

            // Return the Error-Image FilePath
            return _imageSettings.NoLocalImagesFoundPath; // Application.StartupPath + @"\Images\System\NoLocalImagesFound.jpg";
        }

        /// =========================================================================================================
		/// 		''' <summary>
		/// 		''' Returns a random Image for the given ImageGenre.
		/// 		''' </summary>
		/// 		''' <returns>Returns a FilePath or URL. If there are no Images found the 
		/// 		''' "NoLocalImagesFound-Image-Filepath is returned.</returns>
        public string Random(ImageSourceType Type)
        {
            try
            {
                // If offline mode is activated then search only for local files.
                if (OfflineMode)
                    Type = ImageSourceType.Local;

                // Get List of Files for Current Type
                List<string> tmpList = ToList(Type);

                // Check if Links/Paths are present.
                if (tmpList.Count <= 0)
                    goto NoneFound;

                // Pick a Random Image-Path
                return tmpList[_random.Next(0, tmpList.Count)];
            }
            catch (Exception ex)
            {
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                // All Errors
                // ▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨▨
                _log.WriteError(ex.Message + Environment.NewLine + ToString(), ex, "Error while choosing a random Image.");
            }

        NoneFound:
            ;

            // Return an Error-Image FilePath
            if (Type == ImageSourceType.Local)
                return _imageSettings.NoLocalImagesFoundPath; // Application.StartupPath + @"\Images\System\NoLocalImagesFound.jpg";
            else
                return _imageSettings.NoUrlFilesSelectedPath; // Application.StartupPath + @"\Images\System\NoURLFilesSelected.jpg";
        }

        public override string ToString()
        {
            return "ImageDataContainer containing:" + Environment.NewLine + "	GenreName: " + Name.ToString() + Environment.NewLine + "	URLFile: " + UrlFile.ToString() + Environment.NewLine + "	LocalDirectory: " + LocalDirectory.ToString() + Environment.NewLine + "	LocalSubDirectories: " + LocalSubDirectories.ToString() + Environment.NewLine + "	OfflineMode: " + OfflineMode.ToString();
        }



        public bool WebFileExists(string url)
        {
            // we have no idea and there's no point in checking
            return true;
        }

    }
}