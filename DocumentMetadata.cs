using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class DocumentMetadata
    {
        public string Number { get; set; }
        public string Date { get; set; }

        public DocumentMetadata(int counter)
        {
            Number = $"SN-{counter:D4}";
            Date = System.DateTime.Now.ToString("yyyy-MM-dd");
        }

        public override string ToString()
        {
            return $"SN: {Number}, Date: {Date}";
        }
    }

}
