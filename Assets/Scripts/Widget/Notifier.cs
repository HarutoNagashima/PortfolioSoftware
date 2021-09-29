using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notifier : MonoBehaviour
{
    [SerializeField]
    private Text titleLabel = null;

    [SerializeField]
    private Text bodyLabel = null;

    [SerializeField]
    private Animation animationHandler = null;

    private const string NOTIFIER_ANIM = "notifier_in_out";

    private class NotifyMessage
    {
        public string Title;
        public string Body;
    }

    // 通知用メッセージをためておくキュー
    private Queue<NotifyMessage> msgQueue = null;

    private void Awake()
    {
        var listener = new Listener();
        listener.SetEvent(OnShowEvent);
        listener.Listen(Events.ShowNotify);

        msgQueue = new Queue<NotifyMessage>();
    }

    private void Start()
    {
        var animationClip = Resources.Load<AnimationClip>("Animations/notifier_in_out");
        animationHandler.AddClip(animationClip, NOTIFIER_ANIM);
    }

    private void OnShowEvent(object param)
    {
        if (param == null)
        {
            return;
        }

        string[] data = (string[])param;
        NotifyMessage message = new NotifyMessage();
        message.Title = data[0];
        message.Body = data[1];

        msgQueue.Enqueue(message);
    }

    private void Update()
    {
        if (msgQueue.Count == 0) { return; }
        if (animationHandler.isPlaying) { return; }
        NotifyMessage showMessage = msgQueue.Dequeue();
        Show(showMessage.Title, showMessage.Body);
    }

    /// <summary>
    /// 通知が残ってても全部無視（中断）させる
    /// </summary>
    public void Interrupt()
    {
        msgQueue.Clear();
        if (animationHandler.isPlaying)
        {
            animationHandler.Stop();
        }
    }

    /// <summary>
    /// 通知処理を表示する
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    public void Show(string title, string body) 
    {
        titleLabel.text = title;
        bodyLabel.text = body;

        animationHandler.Play(NOTIFIER_ANIM);
    }
}
