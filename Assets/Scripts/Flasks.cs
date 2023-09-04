using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flasks : MonoBehaviour
{
    private const int MaxFlasksInRow = 6;
    [SerializeField] private List<Flask> _flasks;
    [SerializeField] private GameObject shelf;

    public void UpdateShelfVisibility()
    {    
        shelf.gameObject.SetActive(_flasks.TakeLast(MaxFlasksInRow).Any(x=>x.GameObject.activeInHierarchy));
    }

    public List<Flask> FlasksList => _flasks;
}
