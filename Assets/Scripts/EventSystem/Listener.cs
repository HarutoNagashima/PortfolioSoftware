using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Listener
{
    /// <summary>
    /// �֘A�i�����j�������C�x���g
    /// </summary>
    public Events targetEvent { get; private set; }

    /// <summary>
    /// �C�x���g�������������̂�肽������
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
