using System;
using System.Threading.Tasks;
using Dropbox.Api;
using System.Linq;

public class DropboxV1
{
 static string token = "Acess Token";
 static void Main(string [] args)
    {
	// Using the online compiler assuming command line arguments would be passed
	string [] commandLineArgs = {}; // Variable equivalent to actual command line arguments
        var task = Task.Run(() => DropboxV1.Dropbox(commandLineArgs));
        task.Wait();
    }
 static async Task Dropbox(string [] args)
    {
       using (var dbx = new DropboxClient(token)) // recursive = true if all subfolders are required as well 
        {
	       string folder = args.Length > 0 ? args[0] : string.Empty; // If sub-folder location is not mentioned in params assume root
	       var list = await dbx.Files.ListFolderAsync(folder);
               foreach (var item in list.Entries.Where(i => i.IsFolder))
	       {
		       Console.WriteLine("folder: {0} [path='{1}']", item.Name, item.PathLower);
	       }
	       foreach (var item in list.Entries.Where(i => i.IsFile))
	       {
		       Console.WriteLine("file: {0} [path='{1}']", item.Name, item.PathLower);
	       }
        }
    }
}
