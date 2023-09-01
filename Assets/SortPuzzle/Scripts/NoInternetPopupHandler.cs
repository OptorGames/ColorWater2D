using UnityEngine;
using UnityEngine.UI;

public class NoInternetPopupHandler : MonoBehaviour
{
    [SerializeField] private Button okBtn;

    private void Start()
    {
        okBtn.onClick.AddListener(OkBtnClick);
    }

    public void OkBtnClick()
    {
        this.gameObject.SetActive(false);
    }
}
