using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHeader : MonoBehaviour
{
    [SerializeField]
    private Button backButton = null;

    [SerializeField]
    private Text lvLabel = null;

    [SerializeField]
    private Slider staminaGauge = null;

    [SerializeField]
    private Button openCoinShopButton = null;

    [SerializeField]
    private Button openDiamondButton = null;

    [SerializeField]
    private Text diaLabel = null;

    private UserStatus statusData = null;

    private void Awake()
    {
        backButton.onClick.AddListener(() => {
            UIManager.Instance.OpenAlertWindow("TODO", "１つ前の画面へ戻す");
        });

        openCoinShopButton.onClick.AddListener(() => {
            UIManager.Instance.OpenAlertWindow("TODO", "ゲーム内で使える\n通貨を変換する\nウインドーを表示");
        });

        openDiamondButton.onClick.AddListener(() => {
            //UIManager.Instance.OpenAlertWindow("TODO", "ゲーム内通貨を購入する\nウインドーを表示");
            var diaShopWindow = UIManager.Instance.CreateWindow<UIWindow>("Window/WindowDiaShop");
            diaShopWindow.ChangeTitle("ダイアモンド購入");
            diaShopWindow.Open();
        });
    }

    private IEnumerator Start()
    {
        if (!UserDataManager.Instance.IsInitDone) 
        {
            yield break;
        }

        statusData = UserDataManager.Instance.StatusData;
    }

    void Update()
    {
        if (statusData == null) { return; }

        lvLabel.text = statusData.UserLv.ToString();

        staminaGauge.value = statusData.Stamina;

        if (statusData.Dia > 99999)
            diaLabel.text = "100K~";
        else
            diaLabel.text = string.Format("{0:#,0}", statusData.Dia);
    }

}
