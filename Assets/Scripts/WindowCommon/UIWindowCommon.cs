using System;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowCommon : UIWindow
{
    public enum Mode
    {
        Alert, // 結果を表示するのみ
        Confirm, // 確認させる
    }

    private Text bodyText = null;

    private Button cancelButton = null;

    private Mode windowMode = Mode.Alert;

    private Action<bool> onConfirmResult = null;

    public override void Init(GameObject uiObject, Action onOpen, Action onClose)
    {
        base.Init(uiObject, onOpen, onClose);

        // メンバー変数の初期化（参照付け)

        var windowCommonProperty = uiObject.GetComponent<WindowCommonProperty>();

        bodyText = windowCommonProperty.bodyText;

        cancelButton = windowCommonProperty.cancelButton;
    }

    public void Setup(Mode mode, string bodyMessage, Action<bool> onConfrim)
    {
        windowMode = mode;
        bodyText.text = bodyMessage;
        onConfirmResult = onConfrim;
        SetupDisplay();
    }

    private void SetupDisplay() 
    {
        switch (windowMode)
        {
            case Mode.Alert:
                // ...
                cancelButton.gameObject.SetActive(false);
                break;
            case Mode.Confirm:
                cancelButton.gameObject.SetActive(true);

                // OK
                okButton.onClick.RemoveAllListeners();
                okButton.onClick.AddListener(() => { OnConfrim(true); });
                
                // Cancel
                cancelButton.onClick.AddListener(() => { OnConfrim(false); });

                break;
        }
    }

    private void OnConfrim(bool result)
    {
        base.CloseWindow();

        if (onConfirmResult != null)
        {
            onConfirmResult(result);
        }
    }

        // 要件
        // [1]外部から本文となるテキストを引き受けて
        // 表示させる機能

        // [2]結果を表示させるモードなのか
        // OK,Cancelを選択させるモードなのかを設定でき
        // 表示させる

        // [3]選択モードとした場合、選択結果をCallback側に戻す


    }
