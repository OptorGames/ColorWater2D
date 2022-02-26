using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(JsonSaver))]
public class MenuManager : MonoBehaviour
{
    [SerializeField] private string feedback_url;

    [Space(15)]
    private DontDestroyOnLoad DontDestroy;
    private JsonSaver json;

    [Header("Collections")]
    [SerializeField] private Collection[] collections;
    private List<Collection> working_collections = new List<Collection>();
    private List<Collection> archived_collections = new List<Collection>();

    [Header("Sprites")]
    [SerializeField] private Sprite LockedLevelSprite;
    [SerializeField] private Sprite UnknownLevelSprite;

    [Header("Links")]
    [SerializeField] private GameObject Collections_Window;
    [SerializeField] private GameObject Settings_Window;
    [SerializeField] private GameObject Archive_Window;
    [SerializeField] private GameObject Design_Window;
    [SerializeField] private GameObject Menu_Window;

    [SerializeField] private Button PrevButton;
    [SerializeField] private Button NextButton;

    [SerializeField] private GameObject ToBeContinued;

    [SerializeField] private Image[] images = new Image[4];
    [SerializeField] private Slider[] progress_bars = new Slider[4];

    [Header("Archive")]
    [SerializeField] private Image[] archive_images = new Image[4];
    [SerializeField] private GameObject NothingText;

    [SerializeField] private Button PrevArchiveButton;
    [SerializeField] private Button NextArchiveButton;

    private int selected_collection;
    private int selected_archivedCollection;

    private bool need_sort_collections = true;

    private float timer = 2f;

    private void Awake()
    {
        for (int i = 0; i < collections.Length; i++)
        {
            collections[i].id = i;
        }
    }

    private void Start()
    {
        DontDestroy = FindObjectOfType<DontDestroyOnLoad>();
        json = GetComponent<JsonSaver>();

        Collections_Window.SetActive(false);
        Settings_Window.SetActive(false);
        Archive_Window.SetActive(false);
        Design_Window.SetActive(false);
        Menu_Window.SetActive(true);

        PrevArchiveButton.interactable = false;
        PrevButton.interactable = false;
        NextButton.interactable = true;

        timer = 1f;
    }

    private void Update()
    {
        if (timer < 0) SortCollections();
        else timer -= Time.deltaTime;
    }

    public void SortCollections()
    {
        if (need_sort_collections)
        {
            need_sort_collections = false;

            for (int i = 0; i < collections.Length; i++)
            {
                if (working_collections.Count < 2)
                    working_collections.Add(collections[i]);
                else break;
            }

            CheckCanSelectCollection();
        }
    }

    public void SetColletionsInfo(List<Collection> newCollentions)
    {
        need_sort_collections = false;

        for (int i = 0; i < collections.Length; i++)
        {
            if (i < newCollentions.Count)
                collections[i].isCompleted = newCollentions[i].isCompleted;

            if (collections[i].isCompleted)
            {
                collections[i].image_progress1 = 100;
                collections[i].image_progress2 = 100;
                collections[i].image_progress3 = 100;
                collections[i].image_progress4 = 100;

                archived_collections.Add(collections[i]);
            }
            else
            {
                if (i < newCollentions.Count)
                {
                    collections[i].image_progress1 = newCollentions[i].image_progress1;
                    collections[i].image_progress2 = newCollentions[i].image_progress2;
                    collections[i].image_progress3 = newCollentions[i].image_progress3;
                    collections[i].image_progress4 = newCollentions[i].image_progress4;
                }

                if (working_collections.Count < 2)
                    working_collections.Add(collections[i]);
            }
        }

        CheckCanSelectCollection();
    }

    public void Button_Previous()
    {
        selected_collection--;
        if (selected_collection == 0)
            PrevButton.interactable = false;

        NextButton.interactable = true;

        UpdateCollectionInterface();
        CheckCanSelectCollection();
    }

    public void Button_Next()
    {
        selected_collection++;

        UpdateCollectionInterface();
        CheckCanSelectCollection();
    }

    public void Button_PreviousArchive()
    {
        selected_archivedCollection--;
        if (selected_archivedCollection == 0)
            PrevArchiveButton.interactable = false;

        NextArchiveButton.interactable = true;

        CheckCanSelect_ArchivedCollection();
        Update_ArchivedCollectionInterface();
    }

    public void Button_NextArchive()
    {
        selected_archivedCollection++;

        CheckCanSelect_ArchivedCollection();
        Update_ArchivedCollectionInterface();
    }

    private void UpdateCollectionInterface()
    {
        if (selected_collection > working_collections.Count - 1)
            return;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = working_collections[selected_collection].collectionImages[i];
        }

        progress_bars[0].value = working_collections[selected_collection].image_progress1 / 100f;
        progress_bars[1].value = working_collections[selected_collection].image_progress2 / 100f;
        progress_bars[2].value = working_collections[selected_collection].image_progress3 / 100f;
        progress_bars[3].value = working_collections[selected_collection].image_progress4 / 100f;
    }

    private void CheckCanSelectCollection()
    {
        if (working_collections.Count < 1)
        {
            ToBeContinued.SetActive(true);
            for (int i = 0; i < images.Length; i++)
            {
                images[i].gameObject.SetActive(false);
                progress_bars[i].gameObject.SetActive(false);
            }
            PrevButton.gameObject.SetActive(false);
            NextButton.gameObject.SetActive(false);
        }
        else if (selected_collection == working_collections.Count - 1)
        {
            if (selected_collection == 0)
                PrevButton.interactable = false;
            else
                PrevButton.interactable = true;

            NextButton.interactable = false;
            DisableOrEnableButtons();
        }
        else
        {
            if (PrevButton.interactable == false)
            {
                DisableOrEnableButtons();
            }
        }
    }

    private void DisableOrEnableButtons()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].GetComponent<Button>().interactable = true;
        }

        if (working_collections[selected_collection].image_progress1 == 100)
        {
            if (working_collections[selected_collection].image_progress2 == 100)
            {
                if (working_collections[selected_collection].image_progress3 < 100)
                    images[3].GetComponent<Button>().interactable = false;
                else working_collections[selected_collection].isCompleted = true;
            }
            else
            {
                images[3].GetComponent<Button>().interactable = false;
                images[2].GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            images[3].GetComponent<Button>().interactable = false;
            images[2].GetComponent<Button>().interactable = false;
            images[1].GetComponent<Button>().interactable = false;
        }

        if (PrevButton.interactable == true && selected_collection == working_collections.Count - 1)
        {
            images[3].GetComponent<Button>().interactable = false;
            images[2].GetComponent<Button>().interactable = false;
            images[1].GetComponent<Button>().interactable = false;
            images[0].GetComponent<Button>().interactable = false;
        }

        bool canHide = true;
        for (int i = images.Length - 1; i >= 0; i--)
        {
            if(!images[i].GetComponent<Button>().interactable)
            {
                images[i].sprite = LockedLevelSprite;
                images[i].rectTransform.sizeDelta = new Vector2(155f, 191f);
            }
            else if(i != images.Length - 1 && images[i].GetComponent<Button>().interactable && !images[i+1].GetComponent<Button>().interactable)
            {
                images[i].sprite = UnknownLevelSprite;
                images[i].rectTransform.sizeDelta = new Vector2(140f, 187f);
            }
            else images[i].rectTransform.sizeDelta = new Vector2(260f, 260f);

            if (i == 0) return;

            if (images[i].GetComponent<Button>().interactable == true)
            {
                if (canHide)
                {
                    images[i].sprite = UnknownLevelSprite;
                    images[i].rectTransform.sizeDelta = new Vector2(140f, 187f);
                    canHide = false;
                }
                else images[i].rectTransform.sizeDelta = new Vector2(260f, 260f);
            }
            else
            {
                images[i].sprite = LockedLevelSprite;
                images[i].rectTransform.sizeDelta = new Vector2(155f, 191f);
            }
        }
    }

    private void Update_ArchivedCollectionInterface()
    {
        if (selected_archivedCollection > archived_collections.Count - 1)
            return;

        for (int i = 0; i < archive_images.Length; i++)
        {
            archive_images[i].sprite = archived_collections[selected_archivedCollection].collectionImages[i];
        }
    }

    private void CheckCanSelect_ArchivedCollection()
    {
        if (archived_collections.Count < 1)
        {
            NothingText.SetActive(true);
            for (int i = 0; i < archive_images.Length; i++)
            {
                archive_images[i].gameObject.SetActive(false);
            }
            PrevArchiveButton.gameObject.SetActive(false);
            NextArchiveButton.gameObject.SetActive(false);
        }
        else if (selected_archivedCollection == archived_collections.Count - 1)
        {
            if (selected_archivedCollection == 0)
                PrevArchiveButton.interactable = false;
            else
                PrevArchiveButton.interactable = true;

            NextArchiveButton.interactable = false;
        }
    }

    public void SaveColletionsInfo()
    {
        json.Save(collections);
    }

    public void Button_SelectCollection(int i)
    {
        DontDestroy.collections = collections;
        PlayerPrefs.SetInt("SelectedLevel", i);

        if (Collections_Window.activeSelf)
            PlayerPrefs.SetInt("SelectedCollection", working_collections[selected_collection].id);
        else if (Archive_Window.activeSelf)
            PlayerPrefs.SetInt("SelectedCollection", archived_collections[selected_archivedCollection].id);

        LevelHandler.currentLevel = PlayerPrefs.GetInt("CurrentGameLevel");
        SceneManager.LoadScene("gameplay");
    }

    public void ToArchive()
    {
        Archive_Window.SetActive(true);
        Menu_Window.SetActive(false);
        Collections_Window.SetActive(false);
    }

    public void Button_Collections()
    {
        Menu_Window.SetActive(!Menu_Window.activeSelf);
        Collections_Window.SetActive(!Collections_Window.activeSelf);

        UpdateCollectionInterface();
        CheckCanSelectCollection();
    }

    public void Button_Settings()
    {
        Menu_Window.SetActive(!Menu_Window.activeSelf);
        Settings_Window.SetActive(!Settings_Window.activeSelf);
    }

    public void FeedbackBtn()
    {
        Application.OpenURL(feedback_url);
    }

    public void Button_Archive()
    {
        Menu_Window.SetActive(!Menu_Window.activeSelf);
        Archive_Window.SetActive(!Archive_Window.activeSelf);

        Update_ArchivedCollectionInterface();
        CheckCanSelect_ArchivedCollection();
    }

    public void Button_Design()
    {
        Menu_Window.SetActive(!Menu_Window.activeSelf);
        Design_Window.SetActive(!Design_Window.activeSelf);
    }
}
