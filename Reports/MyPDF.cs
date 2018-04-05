using iTextSharp.text;
using iTextSharp.text.exceptions;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Fresenius.Reports
{
    public static class MyPDF
    {
        const string ITextFolder = "wwwroot";
        const string ResourcesFolder = "resources";
   

        public static string Author => "FreseBel and K";





        public static string GetBaseDir()
        {
            //var currentAssembly = typeof(MyPDF).GetTypeInfo().Assembly;
            //var root = Path.GetDirectoryName(currentAssembly.Location);
            //var idx = root.IndexOf($"{Path.DirectorySeparatorChar}bin", StringComparison.OrdinalIgnoreCase);
            //return root.Substring(0, idx);

            //var file = Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles", "images", "banner1.svg");

            return Directory.GetCurrentDirectory();
        }

        public static string GetImagePath(string fileName)
        {

            return Path.Combine(GetBaseDir(), ITextFolder, "images", fileName);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetOutputFileName([CallerMemberName] string methodName = null)
        {
            return Path.Combine(GetOutputFolder(), $"{methodName}.pdf");
        }

        public static string GetOutputFolder()
        {
            var dir = Path.Combine(GetBaseDir(), "bin", "out");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }

        public static string GetPosterPath(string fileName)
        {
            return Path.Combine(GetBaseDir(), ITextFolder, ResourcesFolder, "posters", fileName);
        }

        public static string GetTahomaFontPath()
        {
            return Path.Combine(GetBaseDir(), ITextFolder, "Fonts", "tahoma.ttf");
        }

        public static string GetArialUnicodeMSFontPath()
        {
            return Path.Combine(GetBaseDir(), ITextFolder, ResourcesFolder, "fonts", "arialuni.ttf");
        }

        public static string GetSimSunFontPath()
        {
            return Path.Combine(GetBaseDir(), ITextFolder, ResourcesFolder, "fonts", "simsun.ttf");
        }

        public static string GetThaiFontPath()
        {
            return Path.Combine(GetBaseDir(), ITextFolder, ResourcesFolder, "fonts", "thsarabunnew.ttf");
        }

        public static string GetTxtPath(string fileName)
        {
            return Path.Combine(GetBaseDir(), ITextFolder, ResourcesFolder, "txt", fileName);
        }

        public static string GetPdfsPath(string fileName)
        {
            return Path.Combine(GetBaseDir(), ITextFolder, ResourcesFolder, "pdfs", fileName);
        }

        public static Font GetUnicodeFont(
                    string fontName, string fontFilePath, float size, int style, BaseColor color)
        {
            if (!FontFactory.IsRegistered(fontName))
            {
                FontFactory.Register(fontFilePath);
            }
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, size, style, color);
        }

        public static void VerifyPdfFileIsReadable(byte[] file)
        {
            PdfReader reader = null;
            try
            {
                reader = new PdfReader(file);
                var author = reader.Info["Author"] as string;
                if (string.IsNullOrWhiteSpace(author) || !author.Equals(Author))
                {
                    throw new InvalidPdfException("This is not a valid PDF file.");
                }
            }
            finally
            {
                reader?.Close();
            }
        }

        public static void VerifyPdfFileIsReadable(string filePath)
        {
            VerifyPdfFileIsReadable(File.ReadAllBytes(filePath));
        }
    }
}
