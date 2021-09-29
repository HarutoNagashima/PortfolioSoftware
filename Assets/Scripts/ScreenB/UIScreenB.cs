using UnityEngine;

/// <summary>
/// ScreenB�̃��W�b�N�����N���X
/// </summary>
public class UIScreenB : UIScreen
{
    public override void Init(GameObject uiObject)
    {
        base.Init(uiObject);

        // ScreenB�̏������ł���Ēu���������Ƃ�����
        // ScreenB Prefab�ɂ��Ă� Property�R���|�[�l���g����Button���擾������
        // �擾�����{�^���̃N���b�N�������̓������������
        // �����ScreenA�ɑJ�ڂ�������
        var propertyComponent = uiObject.GetComponent<ScreenBProperty>();
        var moveToScreenAButton = propertyComponent.moveToScreenA;
        moveToScreenAButton.onClick.AddListener( () => {

            UIManager.Instance.ChangeScreen("Screen/ScreenA");

        } );
    }

    /* () => {} �͈ȉ��̂悤��void �֐��Ɠ����Ӗ�
    private void OnClickButton() 
    { 
        // .....
    }
    */
}
