using System;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowCommon : UIWindow
{
    public enum Mode
    {
        Alert, // ���ʂ�\������̂�
        Confirm, // �m�F������
    }

    private Text bodyText = null;

    private Button cancelButton = null;

    private Mode windowMode = Mode.Alert;

    private Action<bool> onConfirmResult = null;

    public override void Init(GameObject uiObject, Action onOpen, Action onClose)
    {
        base.Init(uiObject, onOpen, onClose);

        // �����o�[�ϐ��̏������i�Q�ƕt��)

        var windowCommonProperty = uiObject.GetComponent<WindowCommonProperty>();

        bodyText = windowCommonProperty.bodyText;

        cancelButton = windowCommonProperty.cancelButton;
    }

    public void Setup(Mode mode, string bodyMessage, Action<bool> onConfrim)
    {
        windowMode = mode;
        bodyText.text = bodyMessage;
        onConfirmResult = onConfrim;
        SetupDisplay();
    }

    private void SetupDisplay() 
    {
        switch (windowMode)
        {
            case Mode.Alert:
                // ...
                cancelButton.gameObject.SetActive(false);
                break;
            case Mode.Confirm:
                cancelButton.gameObject.SetActive(true);

                // OK
                okButton.onClick.RemoveAllListeners();
                okButton.onClick.AddListener(() => { OnConfrim(true); });
                
                // Cancel
                cancelButton.onClick.AddListener(() => { OnConfrim(false); });

                break;
        }
    }

    private void OnConfrim(bool result)
    {
        base.CloseWindow();

        if (onConfirmResult != null)
        {
            onConfirmResult(result);
        }
    }

        // �v��
        // [1]�O������{���ƂȂ�e�L�X�g�������󂯂�
        // �\��������@�\

        // [2]���ʂ�\�������郂�[�h�Ȃ̂�
        // OK,Cancel��I�������郂�[�h�Ȃ̂���ݒ�ł�
        // �\��������

        // [3]�I�����[�h�Ƃ����ꍇ�A�I�����ʂ�Callback���ɖ߂�


    }
