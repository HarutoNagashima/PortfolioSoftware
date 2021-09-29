using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemList : MonoBehaviour
{
    // template�ւ̎Q��
    [SerializeField]
    private ShopItem listItem = null;

    [SerializeField]
    private Entity_Diamond masterData = null;

    [SerializeField]
    private Transform listItemParent = null;

    void Start()
    {
        // �}�X�^�f�[�^���񂵂�
        foreach ( var sheet in masterData.sheets)
        {
            foreach ( var row in sheet.list)
            {
                // �V���b�v�A�C�e���̃e���v���[�g�𕡐�
                GameObject newItem = GameObject.Instantiate(listItem.gameObject, listItemParent) as GameObject;
                newItem.SetActive(true);

                // �f�[�^�̔��f
                ShopItem newShopItem = newItem.GetComponent<ShopItem>();
                newShopItem.Init(row, OnPurchase);
            }
        }
    }

    void OnPurchase(int masterID)
    {
        UIManager.Instance.OpenConfirmWindow(
            "�_�C�A�����h�w��",
            "�w���������s���܂�\n��낵���ł����H",
            (result) => { 
            
                if (result)
                {
                    var data = UserDataManager.Instance.StatusData.Clone() as UserStatus;
                    
                    // �}�X�^�f�[�^���񂵂�
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
