
using SharpCompress.Common;
using SharpCompress.Archives;

public class Decompress
{
	public Decompress()
	{
	}

	public void Process()
	{
        var tempPath = "/Users/takano32/tmp";
        using (Stream stream = File.OpenRead("/Users/takano32/Downloads/Archive.7z"))
        using (var archive = ArchiveFactory.Open(stream))
        {
            var fileNames = archive.Entries.Select(x => x.Key).ToList();
            var files = fileNames.Select(x => Path.Combine(tempPath, x)).ToList();
            Console.WriteLine(fileNames);
            Console.WriteLine(files);
            archive.EntryExtractionBegin += Archive_EntryExtractionBegin;
            archive.EntryExtractionEnd += Archive_EntryExtractionEnd;
            archive.CompressedBytesRead += Archive_CompressedBytesRead;
            archive.CompressedBytesRead -= Archive_CompressedBytesRead;
            foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
            {
                entry.WriteToDirectory(tempPath, new ExtractionOptions()
                {
                    ExtractFullPath = true,
                    Overwrite = true
                });
            }
        }

    }

    private void Archive_EntryExtractionBegin(object? sender, ArchiveExtractionEventArgs<IArchiveEntry> e)
    {
        Console.WriteLine(String.Format("Start {0} Extract", e.Item.Key));
    }

    private void Archive_CompressedBytesRead(object? sender, CompressedBytesReadEventArgs e)
    {
        Console.WriteLine(e.CompressedBytesRead);
    }

    private void Archive_EntryExtractionEnd(object? sender, ArchiveExtractionEventArgs<IArchiveEntry> e)
    {
        Console.WriteLine(String.Format("End {0} Extract", e.Item.Key));
    }

}


