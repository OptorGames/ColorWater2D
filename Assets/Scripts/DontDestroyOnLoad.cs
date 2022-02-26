using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Ads))]
public class DontDestroyOnLoad : MonoBehaviour
{
    public Collection[] collections;
    [HideInInspector] public Ads ads;

    public static bool needCreate = true;

    private void Start()
    {
        if (needCreate)
        {
            DontDestroyOnLoad(gameObject);
            ads = GetComponent<Ads>();
            needCreate = false;
        }
    }
}
