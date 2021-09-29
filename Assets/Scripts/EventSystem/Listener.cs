using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Listener
{
    /// <summary>
    /// 関連（発生）したいイベント
    /// </summary>
    public Events targetEvent { get; private set; }

    /// <summary>
    /// イベントが発生した時のやりたい処理
    /// </summary>
    public Action<object> onEvent { get; private set; }

    public void SetEvent(Action<object> callback)
    {
        this.onEvent = callback;
    }

    public void Listen(Events listenEvent)
    {
        this.targetEvent = listenEvent;
        Dispatcher.Instance.AddListener(this);
    }

    public void Clear()
    {
        this.onEvent = null;
    }
}
