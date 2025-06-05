
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
    public class WordMarginalizer : IMarginalizer
    {
        public void AddMarginalization(string inputPath, string outputPath, DocumentMetadata metadata)
        {
            try
            {
                File.Copy(inputPath, outputPath, true);
                using var wordDoc = WordprocessingDocument.Open(outputPath, true);
                var body = wordDoc.MainDocumentPart.Document.Body;
                var para = new Paragraph(new Run(new Text(metadata.ToString())))
                {
                    ParagraphProperties = new ParagraphProperties(new Justification { Val = JustificationValues.Right })
                };
                body.Append(para);
                wordDoc.MainDocumentPart.Document.Save();
            }
            catch (Exception ex)
            {
                throw new MarginalizationException("Failed to marginalize Word document.", ex);
            }
        }
    }

}
