using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

class Confuser
    {
    public static void Obfuscate(string file)
    {
        string configpath = Path.GetTempPath() + "configconfuser.crproj";
        string configconfuser = ConfuserEx.Properties.Resources.config;
        string confuserdirectory = Path.GetTempPath() + "Confuser";
        string basedir = new FileInfo(file).Directory.ToString();

        configconfuser = configconfuser.Replace("%path%", basedir + "\\Obfuscated")
            .Replace("%basedir%", basedir)
            .Replace("%stub%", file);

        File.WriteAllText(configpath, configconfuser);
        File.WriteAllBytes(Path.GetTempPath() + "confuser.zip", ConfuserEx.Properties.Resources.ConfuserEx);

        if (Directory.Exists(confuserdirectory))
        {
            Directory.Delete(confuserdirectory, true);
        }
        
        Directory.CreateDirectory(confuserdirectory);
        ZipFile.ExtractToDirectory(Path.GetTempPath() + "confuser.zip", confuserdirectory);

        ProcessStartInfo process = new ProcessStartInfo();
        process.FileName = confuserdirectory + "\\Confuser.CLI.exe";
        process.UseShellExecute = true;
        process.WindowStyle = ProcessWindowStyle.Hidden;
        process.Arguments = "-n " + configpath;
 
        Process p = Process.Start(process);
        p.WaitForExit();

        File.Delete(Path.GetTempPath() + "confuser.zip");
        File.Delete(Path.GetTempPath() + "configconfuser.crproj");
        Directory.Delete(confuserdirectory, true);

        MessageBox.Show("File saved at \n \"" + basedir + "\\Obfuscated" + new FileInfo(file).FullName + "\"", "Obfuscated", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}

