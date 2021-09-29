using UnityEngine;

/// <summary>
/// ScreenBのロジックを持つクラス
/// </summary>
public class UIScreenB : UIScreen
{
    public override void Init(GameObject uiObject)
    {
        base.Init(uiObject);

        // ScreenBの初期化でやって置きたいことを書く
        // ScreenB Prefabについてる PropertyコンポーネントからButtonを取得したい
        // 取得したボタンのクリックした時の動作を書きたい
        // 動作はScreenAに遷移させたい
        var propertyComponent = uiObject.GetComponent<ScreenBProperty>();
        var moveToScreenAButton = propertyComponent.moveToScreenA;
        moveToScreenAButton.onClick.AddListener( () => {

            UIManager.Instance.ChangeScreen("Screen/ScreenA");

        } );
    }

    /* () => {} は以下のようにvoid 関数と同じ意味
    private void OnClickButton() 
    { 
        // .....
    }
    */
}
