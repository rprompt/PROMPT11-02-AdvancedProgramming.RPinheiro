// -----------------------------------------------------------------------
// <copyright file="DirectoryEnumerator.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using System.Linq;

namespace Mod02_AdvProgramming.PhotoAlbums
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class DirectoryEnumerator
    {
        public static IEnumerable<FileInfo> GetDirectoryEnumeratorEager(DirectoryInfo di)
        {
            return di.GetFiles();
        }

        public static IEnumerable<FileInfo> GetDirectoryEnumeratorLazy(DirectoryInfo di)
        {
        //    //foreach (FileInfo f in di.EnumerateFiles())
            
        //    //{
        //    //    yield return f;
        //    //}

        //    var files = di;

        //    return files.GetFiles().s
            return di.EnumerateFiles().Concat
                (di.EnumerateDirectories().
                     SelectMany
                     (dir => GetDirectoryEnumeratorLazy(dir)));
        }

        public static IEnumerable<FileInfo> GetDirectoryEnumerator(this DirectoryInfo di)
        {
            return GetDirectoryEnumeratorLazy(di);
        }

        public static IEnumerable<string> GetDirectoryImages(DirectoryInfo di)
        {

            return di.GetDirectoryEnumerator().
                Where(fi => fi.Extension == ".jpg").
                     Select(fi1 => fi1.FullName);
            

        }
    }
}
