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
            string inputPath = "test.docx"; 
            string extension = Path.GetExtension(inputPath).ToLower();
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath), Path.GetFileNameWithoutExtension(inputPath) + "_processed" + extension);

            string number = $"SN-{counter:D4}";
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            if (extension == ".docx")
            {
                File.Copy(inputPath, outputPath, true);
                AddMarginalizationToWord(outputPath, number, date);
            }
            else if (extension == ".pdf")
            {
                AddMarginalizationToPdf(inputPath, outputPath, number, date);
            }
            else if (extension == ".jpg" || extension == ".png")
            {
                AddMarginalizationToImage(inputPath, outputPath, number, date);
            }
            else
            {
                Console.WriteLine("Unsupported file type");
            }

            Console.WriteLine("Processed: " + outputPath);
        }

        static void AddMarginalizationToWord(string filePath, string number, string date)
        {
            using var wordDoc = WordprocessingDocument.Open(filePath, true);
            var body = wordDoc.MainDocumentPart.Document.Body;
            var para = new Paragraph(new Run(new Text($"SN: {number}, Date: {date}")))
            {
                ParagraphProperties = new ParagraphProperties(new Justification { Val = JustificationValues.Right })
            };
            body.Append(para);
            wordDoc.MainDocumentPart.Document.Save();
        }

        static void AddMarginalizationToPdf(string inputPath, string outputPath, string number, string date)
        {
            using var input = PdfReader.Open(inputPath, PdfDocumentOpenMode.Modify);
            string footer = $"SN: {number} | Date: {date}";

            foreach (var page in input.Pages)
            {
                XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);
                XFont font = new XFont("Helvetica", 10, XFontStyle.Regular);
                gfx.DrawString(footer, font, XBrushes.Black, new XRect(40, page.Height - 30, page.Width - 80, 20), XStringFormats.BottomRight);
            }

            input.Save(outputPath);
        }

        // Problem :- The image cannot be read and text added to it.
        // TODO :- Fix the issue with image processing.
        static void AddMarginalizationToImage(string inputPath, string outputPath, string number, string date)
        {
            using var image = Image.Load(inputPath);
            string footer = $"SN: {number} | Date: {date}";

            var font = SystemFonts.CreateFont("Arial", 24);
            var position = new SixLabors.ImageSharp.PointF(20, image.Height - 40);

            image.Mutate(ctx =>
                ctx.DrawText(footer, font, SixLabors.ImageSharp.Color.Black, position)
            );

            image.Save(outputPath);
        }
    }
}
