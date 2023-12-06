using System.Text.RegularExpressions;

Console.WriteLine("This app helps to group files to folder by filename template");
Console.WriteLine("IMG_YYYYMMDD_******.jpg -> /YYYY/YYYY-MM/IMG_YYYYMMDD_******.jpg");

String pattern = @"^IMG_(\d{4})(\d{2})(\d{2})_.*\.jpg$";
String sourcePath = Path.Join(@"D:\photo\Redmi Note 10 Pro 2023\Photo");
String subfolder;

int count = 0;
int limit = 5000;

foreach(var file in Directory.GetFiles(sourcePath)){
    string filename = Path.GetFileName(file);
    var match = Regex.Match(filename, pattern);
    subfolder = ( match.Success )
        ?Path.Join(sourcePath, match.Groups[1].Value, String.Join("-", match.Groups[1].Value, match.Groups[2].Value) )
        :Path.Join(sourcePath, "ungroupped" );
    
    if (!Directory.Exists(subfolder))
        Directory.CreateDirectory(subfolder);
    Console.WriteLine(count +"\t"+ filename+"\tcopied to "+subfolder); 
    File.Copy(file, Path.Combine(subfolder, filename));

    if (++count>limit) break;
}