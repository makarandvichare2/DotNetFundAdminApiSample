using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Infrastructure.Helpers
{
    public static class FileHelpers
    {
        public static string GetSampleDataFilePath(string fileName)
        {
            return Path.Combine(
                                Directory.GetParent(Environment.CurrentDirectory).FullName,
                                "FundAdministration.Infrastructure",
                                "SampleData",
                                fileName);
        }
        
    }
}
