using UnityEditor;
using System.IO;

public class FileUtil
{
    [MenuItem("Data/Delete User Data")]
    private static void DeleteUserFile()
    {
        if (File.Exists(UserDataManager.DATA_FILE))
        {
            File.Delete(UserDataManager.DATA_FILE);
        }
    }
}