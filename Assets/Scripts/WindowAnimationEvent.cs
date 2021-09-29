using System;
using UnityEngine;

public class WindowAnimationEvent : MonoBehaviour
{
    // ����A�j���[�V�������Đ��I��������ɌĂяo����������
    private Action onWindowClose = null;

    // �O����A�j���[�V�����I�����ɌĂяo�������������Z�b�g������
    public void SetOnWindowCloseAction(Action closeAction) 
    {
        onWindowClose = closeAction;
    }

    // �^�C�����C���Őݒ肷��C�x���g
    public void OnAnimationEnd()
    {
        if (onWindowClose == null) { return; }
        onWindowClose();
    }
}
