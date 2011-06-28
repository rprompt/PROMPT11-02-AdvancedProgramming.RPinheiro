using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mod02_AdvProgramming.PhotoAlbums
{
    class Program
    {
        static void Main(string[] args)
        {

            //// Listing c:\windows desdendant files in as eager way
            //foreach (FileInfo fileInfo in DirectoryEnumerator.GetDirectoryEnumeratorEager(new DirectoryInfo("c:\\windows")))
            //{
            //    Console.WriteLine(fileInfo.FullName);
            //}

            //// Listing c:\windows desdendant files in as lazy way
            //foreach (FileInfo fileInfo in DirectoryEnumerator.GetDirectoryEnumeratorLazy(new DirectoryInfo("c:\\windows")))
            //{
            //    Console.WriteLine(fileInfo.FullName);
            //}

            //DirectoryInfo di = new DirectoryInfo("c:\\windows");
            //di.GetDirectoryEnumerator();
            ////Listing c:\windows desdendant files with an extension method for DirectoryInfo
            ////foreach (FileInfo fileInfo in new DirectoryInfo("c:\\windows").GetDirectoryEnumerator())
            //foreach (FileInfo fileInfo in di.GetDirectoryEnumerator())
            //{
            //    Console.WriteLine(fileInfo.FullName);
            //}

            //DirectoryInfo images = new DirectoryInfo("C:\\Users\\Public\\Pictures\\Sample Pictures");
            //foreach (String fileInfo in DirectoryEnumerator.GetDirectoryImages(images))
            //{
            //    Console.WriteLine(fileInfo);
            //}



            try
            {


                DirectoryInfo images = new DirectoryInfo(args[0]);


                string fileContent = new StreamReader("template.html").ReadToEnd();
                
                //fileContent = string.Format(fileContent, "SLB", "O maior");
                //Console.WriteLine(fileContent);

                StringBuilder imagesLink = new StringBuilder("");
                
                StreamWriter fileImages = new StreamWriter("imagens.html", true);

                foreach (String fileInfo in DirectoryEnumerator.GetDirectoryImages(images))
                {
                    imagesLink.AppendLine("<a href=\"" + fileInfo + "\" rel=\"lightbox-photos\" title=\"figura\"><img src=\"" + fileInfo + "\" /></a>");

                }

                //Console.WriteLine(imagesLink);

                fileContent = string.Format(fileContent, "Fotos",imagesLink);
                //fileImages.Write(fileContent);

               // using (StreamWriter fileImages = new StreamWriter("imagens.html", true))
                using (fileImages)
                {
                    fileImages.Write(fileContent.ToString());
                }


                
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("A pasta que indicou não existe");
            }
            }

    }
}
