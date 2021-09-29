using System;
using UnityEngine;

public class WindowAnimationEvent : MonoBehaviour
{
    // 閉じるアニメーションが再生終わった時に呼び出したい処理
    private Action onWindowClose = null;

    // 外からアニメーション終了時に呼び出したい処理をセットさせる
    public void SetOnWindowCloseAction(Action closeAction) 
    {
        onWindowClose = closeAction;
    }

    // タイムラインで設定するイベント
    public void OnAnimationEnd()
    {
        if (onWindowClose == null) { return; }
        onWindowClose();
    }
}
