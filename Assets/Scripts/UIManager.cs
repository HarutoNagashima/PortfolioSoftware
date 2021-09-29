using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIManager() { }

    private static UIManager instance;

    // Screen(画面)は同時に複数存在しないルール
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
        // 他の画面が存在してたらまず片付けよう
        // 他の画面のDestroyと合わせて終了処理を呼んであげる
        if (currentScreen != null)
        {
            currentScreen.Terminate();
            currentScreen = null;
        }
    }

    private void InitNewScreen(string screenName, GameObject newScreenObject)
    {
        // 新しく遷移した画面を初期化させる
        // 文字列(クラス名）からクラスの型を取得します,
        // Activator.CreateInstance(type)をいれるとインスタンスを作成する処理
        // prefabの名前を頭にUIを使えると、ロジックを実装しているクラス名が取得できる
        var newScreenType = Type.GetType("UI" + screenName);

        var newScreen = (UIScreen)Activator.CreateInstance(newScreenType);

        newScreen.Init(newScreenObject);

        currentScreen = newScreen;
    }

    /// <summary>
    /// 画面遷移をさせる
    /// </summary>
    /// <param name="prefab">遷移先の画面のロード済みprefab</param>
    /// <param name="onComplete">画面遷移成功時によばれる処理</param>
    public void ChangeScreen(GameObject prefab, Action onComplete = null)
    {
        TerminateCurrentScreen();

        // ScreenLayerの子供として新し画面のGameObjectを生成する
        // 新し画面の初期化処理を呼んであげる

        var newScreenObject = Instantiate(prefab, ScreenLayer);
        newScreenObject.name = newScreenObject.name.Replace("(Clone)", "");

        var screenName = newScreenObject.name;
        InitNewScreen(screenName, newScreenObject);

        // 成功時のコールバックがあれば実行してあげる
        if (onComplete != null)
        {
            onComplete();
        }
    }

    /// <summary>
    /// 画面遷移をさせる
    /// </summary>
    /// <param name="prefabPath">遷移先の画面のリソースパス</param>
    /// <param name="onComplete">画面遷移成功時によばれる処理</param>
    public void ChangeScreen(string prefabPath, Action onComplete = null)
    {
        TerminateCurrentScreen();

        // ScreenLayerの子供として新し画面のGameObjectを生成する
        // 新し画面の初期化処理を呼んであげる

        var prefab = Resources.Load(prefabPath) as GameObject;
        var newScreenObject = Instantiate(prefab, ScreenLayer);
        newScreenObject.name = newScreenObject.name.Replace("(Clone)", "");

        var screenName = newScreenObject.name;
        InitNewScreen(screenName, newScreenObject);

        // 成功時のコールバックがあれば実行してあげる
        if (onComplete != null)
        {
            onComplete();
        }
    }

    /// <summary>
    /// 新規でWindowを生成する処理
    /// 生成したインスタンスのOpenを呼んで実際に開く
    /// </summary>
    /// <param name="windowPath"></param>
    /// <param name="onWindowOpen"></param>
    /// <param name="onWindowClose"></param>
    public T CreateWindow<T>(string windowPath, Action onWindowOpen = null, Action onWindowClose = null) 
        where T : UIWindow, new() // 1
    {
        // 同じwindowを開こうとしたら中断させる
        if (openedWindows.ContainsKey(windowPath)) { return (T)openedWindows[windowPath]; } 

        var prefab = Resources.Load(windowPath) as GameObject;
        var newWindowObject = Instantiate(prefab, WindowLayer);
        newWindowObject.name = newWindowObject.name.Replace("(Clone)", "");

        var uiWindow = new T();// 2
        uiWindow.Init(newWindowObject, onWindowOpen, onWindowClose);
        uiWindow.SetPath(windowPath);

        // windowを管理する処理を初期する
        // windowが開くアニメーションが終わったら呼びたい処理を渡す
        // 開いているwindowをuimanagerに登録しておく
        openedWindows[windowPath] = uiWindow;

        return uiWindow;
    }

    // ウィンドーの閉じる処理が完了した時に管理してるDictからはずす // 2
    public void RemoveWindow(string windowPath)
    {
        openedWindows.Remove(windowPath);
    }

    // 結果のメッセージを表示させる
    public void OpenAlertWindow(string title, string message) 
    {
        UIWindowCommon window = UIManager.Instance.CreateWindow<UIWindowCommon>("Window/WindowCommon");
        window.ChangeTitle(title);
        window.Setup(UIWindowCommon.Mode.Alert, message, null);
        window.Open();
    }

    // 確認ウィンドーを開く（onConfirmで確認の結果(true/false)を拾う)
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
