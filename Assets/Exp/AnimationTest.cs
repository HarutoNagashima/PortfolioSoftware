using UnityEngine;
using System.Collections.Generic;

public class AnimationTest : MonoBehaviour
{
    // �A�j���[�V�������Đ�����ׂɎg��Component
    private Animation customAnimation = null;

    [SerializeField]
    private AnimationClip[] clips = null;

    // �A�j���[�V�����t�@�C�����������Ă������X�g
    private List<string> clipNameList = null;

    void Start()
    {
        clipNameList = new List<string>();
        customAnimation = GetComponent<Animation>();

        if ( clips == null)
        {
            return;
        }

        foreach (AnimationClip clip in clips)
        {
            // �A�j���[�V�����t�@�C���𖼑O�ƍ��킹�ēo�^
            customAnimation.AddClip(clip, clip.name);
            // �o�^�������O���Ƃ��Ă���
            clipNameList.Add(clip.name);
        }
    }

    void Update()
    {
        // �L�[�{�[�h�̂P�ƂQ�L�[�������ꂽ��
        // Animation Component�ɐݒ肵�Ă���
        // 0�Ԗڂ̃A�j���[�V�����ƂP�Ԗڂ̃A�j���[�V�������Đ�����

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            string clipName0 = clipNameList[0];
            customAnimation.PlayQueued(clipName0); // �A�j���[�V������
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            string clipName1 = clipNameList[1];
            customAnimation.PlayQueued(clipName1);
        }

    }
}
