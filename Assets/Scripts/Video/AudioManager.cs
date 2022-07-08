using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _shortLaugh;
    [SerializeField] private AudioSource _longLaugh;
    
    public void ShortLaugh()
    {
        _shortLaugh.Play();
    }

    public void LongLaugh()
    {
        _longLaugh.Play();
    }
}
