using System;
using UnityEngine;

[Serializable]
public class LevelSettings
{
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public int FilledTubesAmount { get; private set; }
    [field: SerializeField] public int EmptyTubesAmount { get; private set; }
    [field: SerializeField] public int ColorsInTube { get; private set; } = 4;
    [field: SerializeField] public bool WithQuestionMark { get; private set; }
}
