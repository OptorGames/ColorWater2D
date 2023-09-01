using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoInternetBlockHandler : MonoBehaviour
{
    [SerializeField] private Button okBtn;

    private void Start()
    {
        okBtn.onClick.AddListener(OkBtnClick);
    }

    public void OkBtnClick()
    {
        if (InternetConnectionDataChecker.IsOnline())
            this.gameObject.SetActive(false);
    }
}
