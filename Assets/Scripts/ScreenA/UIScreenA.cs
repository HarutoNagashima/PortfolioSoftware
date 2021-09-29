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
                window.ChangeTitle("WindowA �e�X�g");
                window.Open(); // ������
            });

        }

        if (screenProperty.openCommonWindowAlert != null)
        {
            screenProperty.openCommonWindowAlert.onClick.AddListener(() =>
            {
                var titleMsg = "���b�Z�[�W�̕\��";
                var bodyMsg = "�\�����o���ďI��,OK�{�^���ŕ���";
                UIManager.Instance.OpenAlertWindow(titleMsg, bodyMsg);
            });
        }

        if (screenProperty.openCommonWindowConfirm != null)
        {
            screenProperty.openCommonWindowConfirm.onClick.AddListener(() =>
            {
                var titleMsg = "�����̓V�C�ɂ���";
                var bodyMsg = "�V�C�͗ǂ��ł����H";

            UIManager.Instance.OpenConfirmWindow(titleMsg, bodyMsg,
            (result) => {
                if (result)
                {
                    var titleMsg = "�m�F����";
                    var bodyMsg = "�V�C�͗ǂ������ł��ˁ`\n�𓚂��肪�Ƃ��������܂�";
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
            "�x�c",
            "�㓡",
            "���J",
            "������",
            "�ޗ�"
        };

        int index = Random.Range(0, actors.Length);
        return actors[index];
    }

    private string CreateMessage() 
    {
        string[] messages =
        {
            "���C�H�H",
            "�����\����l�ł���",
            "���x�Q�[�Z���������`",
            "�E�}�������낢��ˁO�O",
            "���T�����ƓV�C������",
            "GOTO�搶�͐키�V����炵��",
            "��ؐ搶�̂Ђ����₵���`"
        };

        int index = Random.Range(0, messages.Length);
        return messages[index];
    }
}
