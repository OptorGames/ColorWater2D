using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pour : MonoBehaviour
{
    [SerializeField] private GameObject _pour;
    float tempAngle;
    float coef;
    Vector3 rotation;

    private void Awake()
    {
        rotation = _pour.transform.rotation.eulerAngles;
    }
    void Update()
    {
        coef += Time.deltaTime;
        tempAngle = 45f * Mathf.Clamp01(coef);
        
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, tempAngle));
        //transform.Rotate(Vector3.zero, Space.World);
        //Debug.LogError(transform.eulerAngles);
    }

    private void LateUpdate()
    {
        _pour.transform.eulerAngles = rotation;

    }
}
