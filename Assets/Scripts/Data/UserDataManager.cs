using UnityEngine;
using System.IO;

// シングルトンクラス
public class UserDataManager
{
    // データファイルの保存・読み込み先
    public static readonly string DATA_FILE = Application.persistentDataPath + "/UserData.txt";

    // 新規ユーザーのデータファイル
    private static readonly string DEFAULT_DATA_FILE = "Data/DefaultUserStatus";

    public static UserDataManager Instance
    {
        get
        {
            if (inst == null)
            {
                inst = new UserDataManager();
            }

            return inst;
        }
    }

    private static UserDataManager inst = null;

    // Userデータの種類が増える場合以下で追加して行く
    public UserStatus StatusData { get; private set; } = null;

    // public UserHoge HogeData  { get; private set; } = null;
    // ...
    // ...
    // ...

    private UserDataManager() { }

    // 初期化済みかどうか
    public bool IsInitDone { get; private set; } = false;
    
    public void Init() 
    {
        Debug.LogError(DATA_FILE);
        // 新規ユーザーの場合
        if (!File.Exists(DATA_FILE))
        {
            string defaultData = Resources.Load<TextAsset>(DEFAULT_DATA_FILE).text.Trim();
            StatusData = JsonUtility.FromJson<UserStatus>(defaultData);
            File.WriteAllText(DATA_FILE, defaultData);
        }
        else // 既存ユーザーの場合
        {
            string userData = File.ReadAllText(DATA_FILE).Trim();
            StatusData = JsonUtility.FromJson<UserStatus>(userData);
        }

        IsInitDone = true;
    }

    // データの更新
    public void UpdateUserStatus(UserStatus newData) 
    {
        StatusData.UpdateData( newData );
        if (File.Exists(DATA_FILE))
        {
            File.Delete(DATA_FILE);
        }

        string saveData = JsonUtility.ToJson(StatusData);
        File.WriteAllText(DATA_FILE, saveData);
    }
}
