namespace Application.Helper;

public class FileHelper
{
    public static string GetUniqueFileName(string filename)
    {
        filename = Path.GetFileName(filename);            //getting only file name from path
        //below line gets file name without extension =>file1.jpg===>file1
        //then adds _ and new 4 digit random characters
        //and then adds extension to end of file name
        //for example  myfile.jpg==>myfile_4512.jpg
        return string.Concat(Path.GetFileNameWithoutExtension(filename), "_", Guid.NewGuid().ToString().AsSpan(0, 4),
            Path.GetExtension(filename));
    }
}