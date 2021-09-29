using UnityEngine;
using System.Collections.Generic;

public class AnimationTest : MonoBehaviour
{
    // アニメーションを再生する為に使うComponent
    private Animation customAnimation = null;

    [SerializeField]
    private AnimationClip[] clips = null;

    // アニメーションファイル名を持っておくリスト
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
            // アニメーションファイルを名前と合わせて登録
            customAnimation.AddClip(clip, clip.name);
            // 登録した名前をとっておく
            clipNameList.Add(clip.name);
        }
    }

    void Update()
    {
        // キーボードの１と２キーが押されたら
        // Animation Componentに設定してある
        // 0番目のアニメーションと１番目のアニメーションを再生する

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            string clipName0 = clipNameList[0];
            customAnimation.PlayQueued(clipName0); // アニメーション名
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            string clipName1 = clipNameList[1];
            customAnimation.PlayQueued(clipName1);
        }

    }
}
