using UnityEngine;
using System.IO;

// �V���O���g���N���X
public class UserDataManager
{
    // �f�[�^�t�@�C���̕ۑ��E�ǂݍ��ݐ�
    public static readonly string DATA_FILE = Application.persistentDataPath + "/UserData.txt";

    // �V�K���[�U�[�̃f�[�^�t�@�C��
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

    // User�f�[�^�̎�ނ�������ꍇ�ȉ��Œǉ����čs��
    public UserStatus StatusData { get; private set; } = null;

    // public UserHoge HogeData  { get; private set; } = null;
    // ...
    // ...
    // ...

    private UserDataManager() { }

    // �������ς݂��ǂ���
    public bool IsInitDone { get; private set; } = false;
    
    public void Init() 
    {
        Debug.LogError(DATA_FILE);
        // �V�K���[�U�[�̏ꍇ
        if (!File.Exists(DATA_FILE))
        {
            string defaultData = Resources.Load<TextAsset>(DEFAULT_DATA_FILE).text.Trim();
            StatusData = JsonUtility.FromJson<UserStatus>(defaultData);
            File.WriteAllText(DATA_FILE, defaultData);
        }
        else // �������[�U�[�̏ꍇ
        {
            string userData = File.ReadAllText(DATA_FILE).Trim();
            StatusData = JsonUtility.FromJson<UserStatus>(userData);
        }

        IsInitDone = true;
    }

    // �f�[�^�̍X�V
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
