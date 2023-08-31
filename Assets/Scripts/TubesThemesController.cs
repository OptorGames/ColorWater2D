using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TubesThemesController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _themes;
    [SerializeField] private Transform _themesParent;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private HudHandler _hudHandler;
    [SerializeField] private SpawnController _spawnController;

    private bool _isAdTubeOpen;

    void Start()
    {
        SelectTheme();
    }

    public void SetTheme()
    {
        _isAdTubeOpen = _gameManager.tubeControllers.Last().IsReady;

        var lastCombination = _gameManager.GetLastTubesCombination();
        _gameManager.tubeControllers = new List<TubeController>();
        _gameManager.tubesInGame = new List<GameObject>();
        _gameManager.EmptyTubes = 0;
        _gameManager.FullTubes = 0;
        _gameManager.islevelStart = true;

        SelectTheme();

        _spawnController.RefillTubes(lastCombination.tubes);

        UpdateGameManagerInfo();

        StartCoroutine(_gameManager.LevelInitialized());

        //_gameManager.EmptyTubes = lastCombination.empty;
        //_gameManager.FullTubes = lastCombination.full;
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

    private void UpdateGameManagerInfo()
    {
        foreach (var flask in _spawnController.Flasks)
        {
            if (flask.GameObject.activeInHierarchy)
                _gameManager.AddTube(flask.GameObject);
        }

        if (PlayerPrefs.HasKey("FirstStart") && !_isAdTubeOpen)
            _gameManager.tubeControllers.Last().SetIsOpenedByAd(true);
    }
}
