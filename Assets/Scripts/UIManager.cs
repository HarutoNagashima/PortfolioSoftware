using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIManager() { }

    private static UIManager instance;

    // Screen(���)�͓����ɕ������݂��Ȃ����[��
    [SerializeField]
    private Transform ScreenLayer;

    [SerializeField]
    private Transform WindowLayer;

    [SerializeField]
    private Transform OverlayLayer;

    private UIScreen currentScreen = null;

    private Dictionary<string,UIWindow> openedWindows = null; // 1

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                var uimanagerGameObject = new GameObject("UIManager", typeof(UIManager));
                instance = uimanagerGameObject.GetComponent<UIManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        openedWindows = new Dictionary<string, UIWindow>();

        UserDataManager.Instance.Init();
    }

    private void TerminateCurrentScreen() 
    {
        // ���̉�ʂ����݂��Ă���܂��Еt���悤
        // ���̉�ʂ�Destroy�ƍ��킹�ďI���������Ă�ł�����
        if (currentScreen != null)
        {
            currentScreen.Terminate();
            currentScreen = null;
        }
    }

    private void InitNewScreen(string screenName, GameObject newScreenObject)
    {
        // �V�����J�ڂ�����ʂ�������������
        // ������(�N���X���j����N���X�̌^���擾���܂�,
        // Activator.CreateInstance(type)�������ƃC���X�^���X���쐬���鏈��
        // prefab�̖��O�𓪂�UI���g����ƁA���W�b�N���������Ă���N���X�����擾�ł���
        var newScreenType = Type.GetType("UI" + screenName);

        var newScreen = (UIScreen)Activator.CreateInstance(newScreenType);

        newScreen.Init(newScreenObject);

        currentScreen = newScreen;
    }

    /// <summary>
    /// ��ʑJ�ڂ�������
    /// </summary>
    /// <param name="prefab">�J�ڐ�̉�ʂ̃��[�h�ς�prefab</param>
    /// <param name="onComplete">��ʑJ�ڐ������ɂ�΂�鏈��</param>
    public void ChangeScreen(GameObject prefab, Action onComplete = null)
    {
        TerminateCurrentScreen();

        // ScreenLayer�̎q���Ƃ��ĐV����ʂ�GameObject�𐶐�����
        // �V����ʂ̏������������Ă�ł�����

        var newScreenObject = Instantiate(prefab, ScreenLayer);
        newScreenObject.name = newScreenObject.name.Replace("(Clone)", "");

        var screenName = newScreenObject.name;
        InitNewScreen(screenName, newScreenObject);

        // �������̃R�[���o�b�N������Ύ��s���Ă�����
        if (onComplete != null)
        {
            onComplete();
        }
    }

    /// <summary>
    /// ��ʑJ�ڂ�������
    /// </summary>
    /// <param name="prefabPath">�J�ڐ�̉�ʂ̃��\�[�X�p�X</param>
    /// <param name="onComplete">��ʑJ�ڐ������ɂ�΂�鏈��</param>
    public void ChangeScreen(string prefabPath, Action onComplete = null)
    {
        TerminateCurrentScreen();

        // ScreenLayer�̎q���Ƃ��ĐV����ʂ�GameObject�𐶐�����
        // �V����ʂ̏������������Ă�ł�����

        var prefab = Resources.Load(prefabPath) as GameObject;
        var newScreenObject = Instantiate(prefab, ScreenLayer);
        newScreenObject.name = newScreenObject.name.Replace("(Clone)", "");

        var screenName = newScreenObject.name;
        InitNewScreen(screenName, newScreenObject);

        // �������̃R�[���o�b�N������Ύ��s���Ă�����
        if (onComplete != null)
        {
            onComplete();
        }
    }

    /// <summary>
    /// �V�K��Window�𐶐����鏈��
    /// ���������C���X�^���X��Open���Ă�Ŏ��ۂɊJ��
    /// </summary>
    /// <param name="windowPath"></param>
    /// <param name="onWindowOpen"></param>
    /// <param name="onWindowClose"></param>
    public T CreateWindow<T>(string windowPath, Action onWindowOpen = null, Action onWindowClose = null) 
        where T : UIWindow, new() // 1
    {
        // ����window���J�����Ƃ����璆�f������
        if (openedWindows.ContainsKey(windowPath)) { return (T)openedWindows[windowPath]; } 

        var prefab = Resources.Load(windowPath) as GameObject;
        var newWindowObject = Instantiate(prefab, WindowLayer);
        newWindowObject.name = newWindowObject.name.Replace("(Clone)", "");

        var uiWindow = new T();// 2
        uiWindow.Init(newWindowObject, onWindowOpen, onWindowClose);
        uiWindow.SetPath(windowPath);

        // window���Ǘ����鏈������������
        // window���J���A�j���[�V�������I�������Ăт���������n��
        // �J���Ă���window��uimanager�ɓo�^���Ă���
        openedWindows[windowPath] = uiWindow;

        return uiWindow;
    }

    // �E�B���h�[�̕��鏈���������������ɊǗ����Ă�Dict����͂��� // 2
    public void RemoveWindow(string windowPath)
    {
        openedWindows.Remove(windowPath);
    }

    // ���ʂ̃��b�Z�[�W��\��������
    public void OpenAlertWindow(string title, string message) 
    {
        UIWindowCommon window = UIManager.Instance.CreateWindow<UIWindowCommon>("Window/WindowCommon");
        window.ChangeTitle(title);
        window.Setup(UIWindowCommon.Mode.Alert, message, null);
        window.Open();
    }

    // �m�F�E�B���h�[���J���ionConfirm�Ŋm�F�̌���(true/false)���E��)
    public void OpenConfirmWindow(string title, string message, Action<bool> onConfirm)
    {
        UIWindowCommon window = UIManager.Instance.CreateWindow<UIWindowCommon>("Window/WindowCommon");
        window.ChangeTitle(title);
        window.Setup(UIWindowCommon.Mode.Confirm, message, onConfirm);
        window.Open();
    }

    private void Update()
    {
        if (!UserDataManager.Instance.IsInitDone)
        {
            return;
        }

        currentScreen?.ManagedUpdate(Time.deltaTime);
    }

   
}
