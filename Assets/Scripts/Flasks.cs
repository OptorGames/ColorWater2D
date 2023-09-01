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
        if (_flasks.Where(x => x.GameObject.activeInHierarchy).Count() > MaxFlasksInRow)
            shelf.gameObject.SetActive(true);
        else
            shelf.gameObject.SetActive(false);
    }

    public List<Flask> FlasksList => _flasks;
}
