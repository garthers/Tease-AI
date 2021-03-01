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

namespace Tai.Common {

    public enum ImageGenre {

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
        Disliked,
    }

[Serializable]
public class CustomSlideshow : Dictionary<string, ImageGenre>, ISerializable, IDeserializationCallback
{
    public CustomSlideshow()
    {
    }

    public CustomSlideshow(Dictionary<string, ImageGenre> copy) : base(copy)
    {
    }

    /// <summary>
	/// 	''' Constructor needed to deserialize -> inherited from Dictionary.
	/// 	''' </summary>
	/// 	''' <param name="info"></param>
	/// 	''' <param name="context"></param>
    protected CustomSlideshow(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }


    public void AddRange(List<string> List, ImageGenre genre)
    {
        foreach (string str in List)
        {
            if (str != null & str != "" & this.ContainsKey(str) == false)
                Add(str, genre);
        }
    }

    /// <summary>
	/// 	''' Random Number provider.
	/// 	''' </summary>
    [NonSerialized]
    private System.Security.Cryptography.RNGCryptoServiceProvider Rand = new System.Security.Cryptography.RNGCryptoServiceProvider();

    /// <summary>
	/// 	''' Returns a random integer between a min and max value.
	/// 	''' </summary>
	/// 	''' <param name="min"></param>
	/// 	''' <param name="max"></param>
	/// 	''' <returns></returns>
    private int RandomInteger(int min, int max)
    {
        max -= 1;
        uint scale = uint.MaxValue;
        while (scale == uint.MaxValue)
        {
            // Get four random bytes.
            byte[] four_bytes = new byte[4];
            Rand.GetBytes(four_bytes);

            // Convert that into an uint.
            scale = BitConverter.ToUInt32(four_bytes, 0);
        }

        // Add min to the scaled difference between max and min.
        return System.Convert.ToInt32(min + (max - min) * (scale / System.Convert.ToDouble(uint.MaxValue)));
    }



    [NonSerialized]
    private ImageGenre lastgenre;
    /// <summary>
	/// 	''' Gets or sets the current image index.
	/// 	''' </summary>
	/// 	''' <returns>Returns the current Image index or Nothing/Null if the Slideshow is empty.</returns>
	/// 	''' <remarks>Dictionary-Keys are not(!) always the same. But it seems to work unless 
	/// 	''' you modify the dictionary entries. 
	/// 	''' For further details goto: https://msdn.microsoft.com/en-us/library/yt2fy5zk.aspx. </remarks>
    public int Index { get; set; } = -1;


    public string CurrentImage()
    {
        if (Keys.Count > 0)
            return Keys.ElementAt(Index);
        else
            return null;
    }

    public string FirstImage()
    {
        Index = 0;
        return CurrentImage();
    }

    public string NextImage()
    {
        Index += 1;
        if (Index > Keys.Count - 1)
            Index = Keys.Count - 1;
        return CurrentImage();
    }

    public string PreviousImage()
    {
        Index -= 1;
        if (Index < 0)
            Index = 0;
        return CurrentImage();
    }

    public string LastImage()
    {
        Index = Keys.Count - 1;
        return CurrentImage();
    }


    public string GetRandom(bool preferOffline)
    {
        try
        {
            List<string> imagelist = new List<string>();
            int retryCounter = 3;

            // ####################### Pick Genre #######################
            // Get a list of all loaded genres.
            List<ImageGenre> Genrelist = this.Values.Distinct().ToList();

            // Set the maximum retries to get a new new genre
            retryCounter = System.Convert.ToInt32(Genrelist.Count / (double)4);
        PickNewGenre:
            ;

            // Pick a random genre.
            ImageGenre rndGenre = Genrelist[RandomInteger(0, Genrelist.Count)];

            // Check if picked genre is same as last one, but don't force it.
            if (lastgenre == rndGenre & retryCounter > 0)
            {
                // try to pick a new genre.
                retryCounter -= 1;
                goto PickNewGenre;
            }
            else
                // Set current genre for next time.
                lastgenre = rndGenre;

            // Create a list all paths for the picked genre.
            foreach (string str in Keys)
            {
                if (this[str] == rndGenre)
                    imagelist.Add(str);
            }

            // ####################### Pick Image #######################
            // Set maximum retries to get a local image.
            retryCounter = 50;
        pickNextImage:
            ;

            // Pick a random image.
            int rndindex = RandomInteger(0, imagelist.Count);
            string rndPick = imagelist[rndindex];

            // Check if a local image is prefered. If so pick a new path.
            if (preferOffline && Common.IsUrl(rndPick) && imagelist.Count > 1 && retryCounter > 0)
            {
                retryCounter -= 1;
                imagelist.RemoveAt(rndindex);
                goto pickNextImage;
            }
            else
            {
                // Find index of Key
                for (var i = 0; i <= Keys.Count - 1; i++)
                {
                    if (Keys.ElementAt(i) == rndPick)
                    {
                        Index = i;
                        break;
                    }
                }

                return rndPick;
            }
        }
        catch (Exception)
        {
            // @@@@@@@@@@@@@@@@@@@@@@@ Exception @@@@@@@@@@@@@@@@@@@@@@@@
            throw;
        }
    }

    public void RemoveGenre(ImageGenre genre)
    {
        for (var i = this.Keys.Count - 1; i >= 0; i += -1)
        {
            string lazytext = Keys.ElementAt(i);
            if (this[lazytext] == genre)
                this.Remove(lazytext);
        }
    }
}

}