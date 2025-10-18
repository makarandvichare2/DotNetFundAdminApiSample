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
