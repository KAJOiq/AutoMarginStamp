using System;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;

namespace ConsoleApp1
{
    class Program
    {
        static int counter = 1;

        static void Main()
        {
            try
            {
                string inputPath = "test.pdf";
                string extension = Path.GetExtension(inputPath).ToLower();
                string outputPath = Path.Combine(Path.GetDirectoryName(inputPath), Path.GetFileNameWithoutExtension(inputPath) + "_processed" + extension);

                var metadata = new DocumentMetadata(counter);

                IMarginalizer marginalizer = extension switch
                {
                    ".docx" => new WordMarginalizer(),
                    ".pdf" => new PdfMarginalizer(),
                    ".jpg" or ".png" => new ImageMarginalizer(),
                    _ => throw new MarginalizationException($"File type '{extension}' is not supported.")
                };

                marginalizer.AddMarginalization(inputPath, outputPath, metadata);
                Console.WriteLine("Processed: " + outputPath);
            }
            catch (MarginalizationException mex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + mex.Message);
                if (mex.InnerException != null)
                    Console.WriteLine("↳ Details: " + mex.InnerException.Message);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unexpected error: " + ex.Message);
                Console.ResetColor();
            }
        }
    }


}