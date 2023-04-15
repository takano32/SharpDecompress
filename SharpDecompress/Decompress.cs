
using SharpCompress.Common;
using SharpCompress.Archives;

public class Decompress
{
	public Decompress()
	{
	}

	public void Process()
	{
		Console.WriteLine("Decompress::Process");
        using (Stream stream = File.OpenRead("/Users/takano32/Downloads/Archive.7z"))
        using (var archive = ArchiveFactory.Open(stream))
        {
            foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
            {
                entry.WriteToDirectory("/Users/takano32/tmp", new ExtractionOptions()
                {
                    ExtractFullPath = true,
                    Overwrite = true
                });
            }
        }

    }

}


