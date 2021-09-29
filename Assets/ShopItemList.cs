using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemList : MonoBehaviour
{
    // templateへの参照
    [SerializeField]
    private ShopItem listItem = null;

    [SerializeField]
    private Entity_Diamond masterData = null;

    [SerializeField]
    private Transform listItemParent = null;

    void Start()
    {
        // マスタデータ分回して
        foreach ( var sheet in masterData.sheets)
        {
            foreach ( var row in sheet.list)
            {
                // ショップアイテムのテンプレートを複製
                GameObject newItem = GameObject.Instantiate(listItem.gameObject, listItemParent) as GameObject;
                newItem.SetActive(true);

                // データの反映
                ShopItem newShopItem = newItem.GetComponent<ShopItem>();
                newShopItem.Init(row, OnPurchase);
            }
        }
    }

    void OnPurchase(int masterID)
    {
        UIManager.Instance.OpenConfirmWindow(
            "ダイアモンド購入",
            "購入処理を行います\nよろしいですか？",
            (result) => { 
            
                if (result)
                {
                    var data = UserDataManager.Instance.StatusData.Clone() as UserStatus;
                    
                    // マスタデータ分回して
                    foreach (var sheet in masterData.sheets)
                    {
                        foreach (var row in sheet.list)
                        {
                            if (row.ID == masterID)
                            {
                                data.Dia += row.GET_VALUE + row.BONUS_VALUE;
                                break;
                            }
                        }
                    }

                    UserDataManager.Instance.UpdateUserStatus(data);
                }
            }
            );
    }

    
}
