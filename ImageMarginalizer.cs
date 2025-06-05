using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;

namespace ConsoleApp1
{
    public class ImageMarginalizer : IMarginalizer
    {
        public void AddMarginalization(string inputPath, string outputPath, DocumentMetadata metadata)
        {
            try
            {
                using var image = Image.Load(inputPath);

                var font = SystemFonts.CreateFont("Arial", 24);
                var position = new SixLabors.ImageSharp.PointF(20, image.Height - 40);

                image.Mutate(ctx =>
                    ctx.DrawText(metadata.ToString(), font, SixLabors.ImageSharp.Color.Black, position)
                );

                image.Save(outputPath);
            }
            catch (Exception ex)
            {
                throw new MarginalizationException("Failed to marginalize Image.", ex);
            }
        }
    }
}
