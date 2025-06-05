using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IMarginalizer
    {
        void AddMarginalization(string inputPath, string outputPath, DocumentMetadata metadata);
    }

}
