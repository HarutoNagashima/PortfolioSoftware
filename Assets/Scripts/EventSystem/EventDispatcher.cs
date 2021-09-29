using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EventDispatcher : MonoBehaviour
{
    public Events targetEvent;

    public string[] param;

    public BGM playBGM;

    public JNG playJNG;

    public SE playSE;

    private void Awake()
    {
        var button = GetComponent<Button>();
        
        button.onClick.AddListener(() =>
        {
            switch (targetEvent)
            {
                case Events.PlayBGM:
                    Dispatcher.Instance.Dispatch(targetEvent, playBGM);
                    break;
                case Events.PlayJNG:
                    Dispatcher.Instance.Dispatch(targetEvent, playJNG);
                    break;
                case Events.PlaySE:
                    Dispatcher.Instance.Dispatch(targetEvent, playSE);
                    break;
                default:
                    Dispatcher.Instance.Dispatch(targetEvent, param);
                    break;
            }

            // ÉRÅ[ÉhÇ≈SEÇÃçƒê∂ó·
            // SoundManager.Instance.PlaySE(SE.ButtonTap);
            // Dispatcher.Instance.Dispatch(Events.PlaySE, SE.ButtonTap);
        });
    }
}
