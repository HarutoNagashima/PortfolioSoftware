using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWindow : UIBase
{
    // Init���Ă΂ꂽ��A�J���A�j���[�V�������Đ�������
    // �A�j���[�V�������Đ�����������window�̊J�������������������ɌĂяo�������������Ă�
    // ����{�^���̎Q�Ƃ����āA�������ꂽ��window����鏈���ƕ���A�j���[�V�������Đ�����
    // �Đ�������������window�̕��鏈���������������ɌĂяo�������������Ă�
    // Terminate(��������j���Ă�ł�����
    // window�̃^�C�g������ύX����@�\

    private Action onOpen = null;
    private Action onClose = null;

    private Animation windowAnimation = null;

    private AnimationClip openClip = null;
    private AnimationClip closeClip = null;

    private Text titleText = null;
    protected Button okButton = null;

    private string myPath = string.Empty; // 1

    private const string OPEN_CLIP_NAME = "OPEN";
    private const string CLOSE_CLIP_NAME = "CLOSE";
    private const string OK_BUTTON_TAG = "WindowOkButton";
    private const string TITLE_TEXT_TAG = "WindowTitle";

    public void SetPath(string path)
    {
        myPath = path;
    }

    public virtual void Init(GameObject uiObject,Action onOpen,Action onClose)
    {
        base.Init(uiObject);

        this.onOpen = onOpen;
        this.onClose = onClose;

        // animation component���擾
        windowAnimation = uiObject.GetComponent<Animation>();

        // �J���A����A�j���[�V�����f�[�^�����[�h������
        openClip = Resources.Load<AnimationClip>("Animations/window_open");
        closeClip = Resources.Load<AnimationClip>("Animations/window_close");

        // �A�j���[�V�����f�[�^��o�^
        windowAnimation.AddClip(openClip, OPEN_CLIP_NAME);
        windowAnimation.AddClip(closeClip, CLOSE_CLIP_NAME);

        // OK�{�^���̉������̏���
        var buttons = uiObject.GetComponentsInChildren<Button>();
        foreach( var button in buttons)
        {
            if (button.CompareTag(OK_BUTTON_TAG))
            {
                okButton = button;
                button.onClick.AddListener(CloseWindow);
            }
        }

        // Title�e�L�X�g�̎擾
        var labels = uiObject.GetComponentsInChildren<Text>();
        foreach (var label in labels)
        {
            if (label.CompareTag(TITLE_TEXT_TAG))
            {
                titleText = label;
            }
        }

        // ����A�j���[�V�����R�[���o�b�N�ݒ�
        WindowAnimationEvent wae = uiObject.GetComponent<WindowAnimationEvent>();
        if (wae)
        {
            wae.SetOnWindowCloseAction(OnCloseAnimationEnd);
        }
    }

    public void Open()
    {
        // �J���A�j���[�V�����̍Đ�
        windowAnimation.Play(OPEN_CLIP_NAME); // ������
    }

    private void OnCloseAnimationEnd()
    {
        // ����A�j���[�V�������I�����鎞�ɌĂ΂��
        Terminate(); // �������
        UIManager.Instance.RemoveWindow(myPath); // UIManager����͂�������
    }

    // �E�B���h�[�̃^�C�g�����ύX
    public void ChangeTitle(string name)
    {
        if (titleText == null) { return; }
        titleText.text = name;
    }

    public void CloseWindow() 
    {
        // ����A�j���[�V�����̍Đ�
        windowAnimation.Play(CLOSE_CLIP_NAME);
    }

    public override void ManagedUpdate(float dt) { }

    public override void Terminate()
    {
        base.Terminate();
    }
}
