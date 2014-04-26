using System;
using System.Runtime.InteropServices;
using System.Text;

public static class Ini_File
{
	public static string path = string.Format(@"{0}\webmcam.ini", Environment.CurrentDirectory);

    [DllImport("kernel32")]
    static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")]
    static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
	
    public static string Write(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value, path);
		return value;
    }
	
    public static string Read(string section, string key)
    {
        var temp = new StringBuilder(255);
        int i = GetPrivateProfileString(section, key, "", temp, 255, path);
        return temp.ToString();
    }
	
	public static string Exists(string section, string key, string value) {
		string str = Read(section, key);
		return (str.Length > 0) ? Read(section, key) : Write(section, key, value);
	}
}