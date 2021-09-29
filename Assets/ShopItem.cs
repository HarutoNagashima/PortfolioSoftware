using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private Image productIcon = null;

    [SerializeField]
    private Text productName = null;

    [SerializeField]
    private Text bonus = null;

    [SerializeField]
    private Text cost = null;

    [SerializeField]
    private Button diaButton = null;

    public void Init(Entity_Diamond.Param diamondMaster, Action<int> onPurchase) 
    {
        // アイコンのセット
        productIcon.sprite = Resources.Load<Sprite>(diamondMaster.RES_FILE);

        // 商品名
        productName.text = $"{diamondMaster.PRD_NAME} x {diamondMaster.GET_VALUE}";

        // お得情報
        bonus.text = $"+{diamondMaster.BONUS_VALUE}";

        // 価格
        cost.text = $"{diamondMaster.COST}円";

        // 購入ボタン押下処理
        diaButton.onClick.AddListener(() =>
        {
            if (onPurchase != null)
            {
                onPurchase(diamondMaster.ID);
            }
        });
    }
}
