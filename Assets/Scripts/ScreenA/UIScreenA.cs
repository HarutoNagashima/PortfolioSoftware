using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenA : UIScreen
{
    public override void Init(GameObject uiObject)
    {
        base.Init(uiObject);

        var screenProperty = uiObject.GetComponent<ScreenAProperty>();
        if (screenProperty.moveToScreenB != null)
        {
            screenProperty.moveToScreenB.onClick.AddListener(() => {
                UIManager.Instance.ChangeScreen("Screen/ScreenB",
                    () => {
                        Debug.Log("Screen B Loaded");
                    });
            });
        }

        if (screenProperty.openWindowA != null)
        {
            screenProperty.openWindowA.onClick.AddListener(() =>
            {
                UIWindow window = UIManager.Instance.CreateWindow<UIWindow>("Window/WindowA");
                window.ChangeTitle("WindowA テスト");
                window.Open(); // ←←←
            });

        }

        if (screenProperty.openCommonWindowAlert != null)
        {
            screenProperty.openCommonWindowAlert.onClick.AddListener(() =>
            {
                var titleMsg = "メッセージの表示";
                var bodyMsg = "表示を出して終了,OKボタンで閉じる";
                UIManager.Instance.OpenAlertWindow(titleMsg, bodyMsg);
            });
        }

        if (screenProperty.openCommonWindowConfirm != null)
        {
            screenProperty.openCommonWindowConfirm.onClick.AddListener(() =>
            {
                var titleMsg = "今日の天気について";
                var bodyMsg = "天気は良いですか？";

            UIManager.Instance.OpenConfirmWindow(titleMsg, bodyMsg,
            (result) => {
                if (result)
                {
                    var titleMsg = "確認結果";
                    var bodyMsg = "天気は良かったですね～\n解答ありがとうございます";
                    UIManager.Instance.OpenAlertWindow(titleMsg, bodyMsg);
                }
                });
            });
        }        
    }


    /*
     (reuslt) => {}
     void callback(bool result)
    {
    
    } 
     */

    private float elapsedTime = 0f;

    public override void ManagedUpdate(float dt)
    {
        base.ManagedUpdate(dt);

        elapsedTime += dt;
        
        if (elapsedTime >= 5f)
        {
            elapsedTime = 0f;
            
            var data = UserDataManager.Instance.StatusData.Clone() as UserStatus;
            data.UserLv++;

            UserDataManager.Instance.UpdateUserStatus(data);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            string[] texts = { CreateTitle(), CreateMessage() };
            Dispatcher.Instance.Dispatch(Events.ShowNotify, texts);
        }
    }

    private string CreateTitle() 
    {
        string[] actors =
        {
            "富田",
            "後藤",
            "染谷",
            "たけし",
            "奈良"
        };

        int index = Random.Range(0, actors.Length);
        return actors[index];
    }

    private string CreateMessage() 
    {
        string[] messages =
        {
            "元気？？",
            "α発表会お疲れ様でした",
            "今度ゲーセンいこう～",
            "ウマ娘おもろいよね＾＾",
            "今週ずっと天気が悪い",
            "GOTO先生は戦う坊さんらしい",
            "鈴木先生のひげあやしい～"
        };

        int index = Random.Range(0, messages.Length);
        return messages[index];
    }
}
