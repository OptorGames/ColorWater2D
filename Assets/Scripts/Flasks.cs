using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasks : MonoBehaviour
{
    [SerializeField] private List<Flask> _flasks;

    public List<Flask> FlasksList => _flasks;
}
