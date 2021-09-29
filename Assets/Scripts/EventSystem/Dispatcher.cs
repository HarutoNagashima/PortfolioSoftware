using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispatcher
{
    private Dictionary<Events, List<Listener>> listeners = null;

    public Dispatcher()
    {
        listeners = new Dictionary<Events, List<Listener>>();
    }

    private static Dispatcher instance = null;
    public static Dispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Dispatcher();
            }
            return instance;
        }
    }

    /// <summary>
    /// イベントリスナーを登録する処理
    /// </summary>
    /// <param name="listener"></param>
    public void AddListener(Listener listener)
    {
        if (!listeners.ContainsKey(listener.targetEvent))
        {
            List<Listener> listenerList = new List<Listener>();
            listeners[listener.targetEvent] = listenerList;
        }

        listeners[listener.targetEvent].Add(listener);
    }

    /// <summary>
    /// イベントリスナーを消す処理
    /// </summary>
    /// <param name="targetEvent"></param>
    public void ClearListener(Events targetEvent)
    {
        if (!listeners.ContainsKey(targetEvent))
        {
            return;
        }

        var listenerList = listeners[targetEvent];
        foreach(var listener in listenerList)
        {
            listener.Clear();
        }

        listenerList.Clear();
    }

    /// <summary>
    /// イベントの発火処理
    /// </summary>
    /// <param name="targetEvent"></param>
    /// <param name="paramters"></param>
    public void Dispatch(Events targetEvent, object paramters) 
    {
        if (!listeners.ContainsKey(targetEvent))
        {
            Debug.LogError($"発火させたい{targetEvent}を関連してる処理がありません");
            return;
        }

        var listenerList = listeners[targetEvent];
        foreach (var listener in listenerList)
        {
            if (listener.onEvent != null)
            {
                listener.onEvent(paramters);
            }
        }
    }
}
