namespace Gherkin.Benchmarks;

public class TestFileProvider
{
    public static IEnumerable<object[]> GetValidTestFiles()
    {
        return GetTestFiles("good");
    }

    public static IEnumerable<object[]> GetInvalidTestFiles()
    {
        return GetTestFiles("bad");
    }

    private static IEnumerable<object[]> GetTestFiles(string category)
    {
        string testFileFolder = GetTestFileFolder(category);

        return Directory.GetFiles(testFileFolder, "*.feature")
                        .Where(f => Path.GetFileName(f) != "escaped_pipes.feature")
                        .Select(f => new object[] { Path.GetFileName(f) });
    }

    public static string GetTestFileFolder(string category)
    {
        var inputFolder = Environment.CurrentDirectory;
#if DEBUG
        // Artefacts are not created in subdirectories, so we don't need to go any higher.
#elif NET6_0_OR_GREATER
        inputFolder = Path.Combine(inputFolder, "..", "..", "..", "..");
#endif
        return Path.GetFullPath(Path.Combine(inputFolder, "..", "..", "..", "..", "..", "testdata", category));
    }
}
