using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWindow : UIBase
{
    // Initが呼ばれたら、開くアニメーションを再生させる
    // アニメーションが再生完了したらwindowの開く処理が完了した時に呼び出したい処理を呼ぶ
    // 閉じるボタンの参照をして、押下されたらwindowを閉じる処理と閉じるアニメーションを再生する
    // 再生が完了したらwindowの閉じる処理が完了した時に呼び出したい処理を呼ぶ
    // Terminate(解放処理）を呼んであげる
    // windowのタイトル名を変更する機能

    private Action onOpen = null;
    private Action onClose = null;

    private Animation windowAnimation = null;

    private AnimationClip openClip = null;
    private AnimationClip closeClip = null;

    private Text titleText = null;
    protected Button okButton = null;

    private string myPath = string.Empty; // 1

    private const string OPEN_CLIP_NAME = "OPEN";
    private const string CLOSE_CLIP_NAME = "CLOSE";
    private const string OK_BUTTON_TAG = "WindowOkButton";
    private const string TITLE_TEXT_TAG = "WindowTitle";

    public void SetPath(string path)
    {
        myPath = path;
    }

    public virtual void Init(GameObject uiObject,Action onOpen,Action onClose)
    {
        base.Init(uiObject);

        this.onOpen = onOpen;
        this.onClose = onClose;

        // animation componentを取得
        windowAnimation = uiObject.GetComponent<Animation>();

        // 開く、閉じるアニメーションデータをロードさせる
        openClip = Resources.Load<AnimationClip>("Animations/window_open");
        closeClip = Resources.Load<AnimationClip>("Animations/window_close");

        // アニメーションデータを登録
        windowAnimation.AddClip(openClip, OPEN_CLIP_NAME);
        windowAnimation.AddClip(closeClip, CLOSE_CLIP_NAME);

        // OKボタンの押下時の処理
        var buttons = uiObject.GetComponentsInChildren<Button>();
        foreach( var button in buttons)
        {
            if (button.CompareTag(OK_BUTTON_TAG))
            {
                okButton = button;
                button.onClick.AddListener(CloseWindow);
            }
        }

        // Titleテキストの取得
        var labels = uiObject.GetComponentsInChildren<Text>();
        foreach (var label in labels)
        {
            if (label.CompareTag(TITLE_TEXT_TAG))
            {
                titleText = label;
            }
        }

        // 閉じるアニメーションコールバック設定
        WindowAnimationEvent wae = uiObject.GetComponent<WindowAnimationEvent>();
        if (wae)
        {
            wae.SetOnWindowCloseAction(OnCloseAnimationEnd);
        }
    }

    public void Open()
    {
        // 開くアニメーションの再生
        windowAnimation.Play(OPEN_CLIP_NAME); // ←←←
    }

    private void OnCloseAnimationEnd()
    {
        // 閉じるアニメーションが終了する時に呼ばれる
        Terminate(); // 解放処理
        UIManager.Instance.RemoveWindow(myPath); // UIManagerからはずす処理
    }

    // ウィンドーのタイトル名変更
    public void ChangeTitle(string name)
    {
        if (titleText == null) { return; }
        titleText.text = name;
    }

    public void CloseWindow() 
    {
        // 閉じるアニメーションの再生
        windowAnimation.Play(CLOSE_CLIP_NAME);
    }

    public override void ManagedUpdate(float dt) { }

    public override void Terminate()
    {
        base.Terminate();
    }
}
