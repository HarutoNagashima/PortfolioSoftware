using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject startScreen;

    private void Awake()
    {
        if (startScreen != null)
        {
            UIManager.Instance.ChangeScreen(startScreen, () =>
            {
                Debug.Log("<color=orange>SUCCESS SCREEN CHANGE</color>");
            });
        }
    }
}
