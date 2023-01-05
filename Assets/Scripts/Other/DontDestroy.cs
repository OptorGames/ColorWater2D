using UnityEngine;

[RequireComponent(typeof(DTDInit))]
public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance = null;
    private DTDInit dTDInit;

    private void Start()
    {
        if (instance == null)
        {
            dTDInit = GetComponent<DTDInit>();

            instance = this;

            //DontDestroyOnLoad(gameObject);
            dTDInit.enabled = true;
        }
        else
            Destroy(gameObject);
    }
}
