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
        // �A�C�R���̃Z�b�g
        productIcon.sprite = Resources.Load<Sprite>(diamondMaster.RES_FILE);

        // ���i��
        productName.text = $"{diamondMaster.PRD_NAME} x {diamondMaster.GET_VALUE}";

        // �������
        bonus.text = $"+{diamondMaster.BONUS_VALUE}";

        // ���i
        cost.text = $"{diamondMaster.COST}�~";

        // �w���{�^����������
        diaButton.onClick.AddListener(() =>
        {
            if (onPurchase != null)
            {
                onPurchase(diamondMaster.ID);
            }
        });
    }
}
