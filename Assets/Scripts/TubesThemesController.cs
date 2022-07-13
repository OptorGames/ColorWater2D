using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubesThemesController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _themes;
    [SerializeField] private Transform _themesParent;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private HudHandler _hudHandler;
    [SerializeField] private SpawnController _spawnController;
    
    void Start()
    {
        SelectTheme();
    }

    public void SetTheme()
    {

        SelectTheme();
        if (_gameManager.SavedTubes.Count > 0)
        {
            var lastCombination = _gameManager.GetLastTubesCombination();
            _gameManager.tubeControllers = new List<TubeController>();
            _gameManager.tubesInGame = new List<GameObject>();
            _gameManager.FullTubes = lastCombination.full;
            _gameManager.EmptyTubes = lastCombination.empty;


            _spawnController.RefillTubes(lastCombination.tubes);
        }
        else
        {
            _gameManager.StartLevel();

            _spawnController.level = _gameManager.curr_level;
            _spawnController.SpawnObject();
        }
    }

    private void SelectTheme()
    {
        foreach (Transform child in _themesParent)
        {
            Destroy(child.gameObject);
        }
        var theme = Instantiate(_themes[PlayerPrefs.GetInt("Tube", 0)], _themesParent.transform);
        _spawnController.Flasks = theme.GetComponent<Flasks>().FlasksList;
        _hudHandler.Flasks = theme;
    }
}
