using DocumentFormat.OpenXml.Packaging;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    public class PdfMarginalizer : IMarginalizer
    {
        public void AddMarginalization(string inputPath, string outputPath, DocumentMetadata metadata)
        {
            try
            {
                using var input = PdfReader.Open(inputPath, PdfDocumentOpenMode.Modify);
                foreach (var page in input.Pages)
                {
                    XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);
                    XFont font = new XFont("Helvetica", 10, XFontStyle.Regular);
                    gfx.DrawString(metadata.ToString(), font, XBrushes.Black, new XRect(40, page.Height - 842, page.Width - 80, 20), XStringFormats.BottomRight);
                }

                input.Save(outputPath);
            }
            catch (Exception ex)
            {
                throw new MarginalizationException("Failed to marginalize PDF document.", ex);
            }
        }
    }
}
