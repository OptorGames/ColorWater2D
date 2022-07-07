using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ForTutorial
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private SpawnController _spawnController;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameObject _tubeForTutorial;
        [SerializeField] private GameObject _arrowPoint;
        [SerializeField] private Button _difficultyButton;
        public List<GameObject> TutorialTubes;
        public List<GameObject> TutorialArrows;

        public GameObject TubeForTutorial => _tubeForTutorial;

        public GameObject ArrowPoint => _arrowPoint;

        private void Start()
        {
            if (!PlayerPrefs.HasKey("FirstStart"))
            {
                _spawnController.level = 1;
                _spawnController.numberOfEmptyTube = 0;
                ProgressControl(1);
                _difficultyButton.enabled = false;
            }
        }

        public void RestartTutorial()
        {
            PlayerPrefs.DeleteKey("FirstStart");
            PlayerPrefs.DeleteKey("CurrentLevel_OFF");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ProgressControl(int numberForTurnOn)
        {
            for (int i = 0; i < TutorialArrows.Count; i++)
            {
                if (i == numberForTurnOn)
                {
                    TutorialArrows[i].SetActive(true);
                }
                else
                {
                    TutorialArrows[i].SetActive(false);
                }
            }

            for (int i = 0; i < TutorialTubes.Count; i++)
            {
                if (i == numberForTurnOn)
                {
                    TutorialTubes[i].GetComponent<Collider>().enabled = true;
                }
                else
                {
                    TutorialTubes[i].GetComponent<Collider>().enabled = false;
                }
            }
        }
    }
}