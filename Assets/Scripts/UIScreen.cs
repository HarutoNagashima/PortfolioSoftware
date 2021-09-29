using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : UIBase
{

    private Listener bgmListener = null;
    private Listener jngListener = null;
    private Listener seListener = null;
    private Listener bgmStopListener = null;

    public override void Init(GameObject uiObject)
    {
        base.Init(uiObject);
        Debug.Log("<color=green>Init Called</color>");

        bgmListener = new Listener();
        bgmListener.Listen(Events.PlayBGM);
        bgmListener.SetEvent(OnPlayBGM);

        jngListener = new Listener();
        jngListener.Listen(Events.PlayJNG);
        jngListener.SetEvent(OnPlayJNG);

        seListener = new Listener();
        seListener.Listen(Events.PlaySE);
        seListener.SetEvent(OnPlaySE);

        bgmStopListener = new Listener();
        bgmStopListener.Listen(Events.StopBGM);
        bgmStopListener.SetEvent(OnStopBGM);
    }

    private void OnPlayBGM(object bgm)
    {
        if (bgm == null) { return; }

        SoundManager.Instance.SetDefaultVolume(SoundManager.AudioType.BGM);
        SoundManager.Instance.PlayBGM((BGM)bgm);
    }

    private void OnPlayJNG(object jng)
    {
        if (jng == null) { return; }
        SoundManager.Instance.PlayJNG((JNG)jng);
    }

    private void OnPlaySE(object se)
    {
        if (se == null) { return; }
        SoundManager.Instance.PlaySE((SE)se);
    }

    private void OnStopBGM(object obj) 
    {
        SoundManager.Instance.ChangeVolume(SoundManager.AudioType.BGM, 0f);
    }

    public override void Terminate()
    {
        base.Terminate();
        bgmListener.Clear();
        bgmListener = null;

        jngListener.Clear();
        jngListener = null;

        seListener.Clear();
        seListener = null;

        Debug.Log("<color=red>Terminate Called</color>");
    }
}
