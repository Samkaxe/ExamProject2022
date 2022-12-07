namespace Application.Helper;

public class FileHelper
{
    public static string GetUniqueFileName(string filename)
    {
        filename = Path.GetFileName(filename);
        return string.Concat(Path.GetFileNameWithoutExtension(filename), "_", Guid.NewGuid().ToString().AsSpan(0, 4),
            Path.GetExtension(filename));
    }
}