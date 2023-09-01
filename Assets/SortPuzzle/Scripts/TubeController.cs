using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using LiquidVolumeFX;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;

public class TubeController : MonoBehaviour
{
    private readonly Vector3 PourSpriteYOffset = new Vector3(0, 0.05f, 0);
    private readonly Vector3 PourSpriteInvertYOffset = new Vector3(0.05f, -0.3f, 0);
    private const int AdWatchCount = 1;

    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material blurredMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float _returnSpeed;
    [SerializeField] private Button adBtn;

    public event Action<TubeController> OnAdBtnClick;
    public bool IsReady => !adBtn.gameObject.activeSelf;

    public Vector3 Pos;
    public GameObject ColorsPivot;
    public GameObject PourSpriteObject;

    public int currColors;

    [HideInInspector]
    public Color[] colorsInTube;

    [HideInInspector]
    public GameObject[] ColorObjects;

    public LiquidVolume LiquidVolume;
    public float ColorLayerAmount = 0.22f;
    public float FoamThickness = 0.0052f;

    private float RotStart, RotEnd;

    public bool isrotating = false;
    private float rotationLerp;

    private IGameManager GM;
    [HideInInspector] public TubeController NextTube;
    public SpriteRenderer tubeSR;
    [HideInInspector] public SortingGroup sorting;
    private bool IsAddColor;
    [Space(15)] public bool isFull, isEmpty, isBusy;
    private bool isAddingColor;

    private float ColorLerp;

    private AudioSource audioSource;
    private bool _canMouseDown = true;

    private Vector3 _pourAngle = new Vector3(0, 0, 0f);
    private Vector3 _flaskDistance = new Vector3(0.1f, 0.6f, 0f);
    private Quaternion _pourRotation = Quaternion.identity;
    private GameObject _pourSprite;
    private float _timeCoef = 1.5f;

    private int adWatched = 0;

    private bool isInverted;

    private Flask _flask;
    private List<Color> savedColors;
    private Color hiddenColor = Color.grey;

    public void SetIsOpenedByAd(bool value)
    {
        adBtn.gameObject.SetActive(value);
        if (!value)
            return;

        meshRenderer.material = blurredMaterial;
        adWatched = 0;
    }

    private void Start()
    {
        adWatched = 0;
        isAddingColor = false;
        isEmpty = false;
        isFull = false;
        IsAddColor = false;
        currColors = 0;
        Pos = transform.position;
        GM = FindObjectOfType<GameManager>();

        audioSource = GM.audioSource;

        colorsInTube = new Color[LiquidVolume.liquidLayers.Length];
        savedColors = new List<Color>();

        for (int i = 0; i < 4; i++)
        {
            if (LiquidVolume.liquidLayers[i].amount > 0f)
            {
                colorsInTube[i] = LiquidVolume.liquidLayers[i].color;
                savedColors.Add(LiquidVolume.liquidLayers[i].color);
                ++currColors;
            }
        }

        if (currColors == 0)
        {
            isEmpty = true;
            GM.AddEmpty();
        }

        if (currColors == 4 && colorsInTube[0] == colorsInTube[1] && colorsInTube[0] == colorsInTube[2] &&
            colorsInTube[0] == colorsInTube[3])
        {
            GM.AddFull(transform.position, false);
            isFull = true;
        }

        UpdateColors();

    }

    private void Update()
    {
        if (!IsReady)
            return;

        if (isAddingColor)
        {

            ColorLerp += Time.deltaTime * _timeCoef;
            if (ColorLerp > 1)
            {
                isAddingColor = false;
                ColorLerp = 1;
            }

            LiquidVolume.liquidLayers[currColors - 1].amount = ColorLayerAmount * ColorLerp;
            LiquidVolume.UpdateLayers(true);
        }

        if (isrotating)
        {

            rotationLerp += Time.deltaTime * _timeCoef;
            if (rotationLerp >= 1)
            {
                rotationLerp = 1;


                if (currColors > 1 && colorsInTube[currColors - 1] == colorsInTube[currColors - 2])
                {
                    IsAddColor = true;
                    rotationLerp = 0;
                    RemoveColor();
                    RotStart = RotationDataObject.RotationData[PlayerPrefs.GetInt("Tube", 0)].StartAngle[currColors - 1];
                    RotEnd = RotationDataObject.RotationData[PlayerPrefs.GetInt("Tube", 0)].EndAngle[currColors - 1];
                }
                else
                {
                    isrotating = false;
                    audioSource.Stop();

                    NextTube.isBusy = false;
                    Destroy(_pourSprite);
                    RemoveColor();

                    StartCoroutine(ReturnToStartingPosition(_returnSpeed));
                    GM.addingColor = false;

                    if (currColors == 0)
                    {
                        isEmpty = true;
                        GM.AddEmpty();
                    }
                }
            }
            else
            {
                if (IsAddColor)
                {
                    IsAddColor = false;
                    NextTube.AddColor(colorsInTube[currColors - 1]);
                }

                float tempAngle = Mathf.Lerp(RotStart, RotEnd, rotationLerp);

                LiquidVolume.liquidLayers[currColors - 1].amount =
                Mathf.Clamp01(LiquidVolume.liquidLayers[currColors - 1].amount - Time.deltaTime * _timeCoef * ColorLayerAmount);
                LiquidVolume.UpdateLayers(true);
            }
        }
    }

    public bool CheckCapacity(int colorCount, int count_curr_colors, Color colr)
    {
        if (currColors == 0)
            return true;
        if (4 - currColors < colorCount)
            return false;
        if (colorsInTube[currColors - 1] != colr)
            return false;

        return true;
    }

    public void PourColor(GameObject otherTube)
    {
        NextTube = otherTube.GetComponent<TubeController>();

        _pourRotation = Quaternion.LookRotation(otherTube.transform.position, Vector3.forward);

        int MatchCount;
        MatchCount = 1;

        for (int i = currColors - 2; i >= 0; i--)
        {
            if (colorsInTube[i] == colorsInTube[i + 1])
                MatchCount++;
            else
                break;
        }

        if (currColors > 0 && NextTube.CheckCapacity(MatchCount, currColors, colorsInTube[currColors - 1]))
        {
            GM.SaveTubes();

            float y = 0;

            if (PlayerPrefs.GetInt("Tube") > 0)
            {
                if (NextTube.currColors == 0)
                    y = 1.03f;
                else if (NextTube.currColors == 1)
                    y = 0.95f;
                else if (NextTube.currColors == 2)
                    y = 0.81f;
                else if (NextTube.currColors == 3)
                    y = 0.72f;
            }
            else
            {
                if (NextTube.currColors == 0)
                    y = 1f;
                else if (NextTube.currColors == 1)
                    y = 0.9f;
                else if (NextTube.currColors == 2)
                    y = 0.8f;
                else if (NextTube.currColors == 3)
                    y = 0.7f;
            }

            StartCoroutine(MoveToEndingPosition(_returnSpeed, otherTube));
        }
        else StartCoroutine(ReturnToStartingPosition(_returnSpeed));
    }

    private void UpdateColors()
    {
        if (_flask == null || !_flask.IsHiddenColor)
            return;
        if (LiquidVolume.liquidLayers.All(x => x.amount == 0))
            return;

        var layers = LiquidVolume.liquidLayers.Where(x => x.amount > 0).ToList();
        for (int i = 0; i < LiquidVolume.liquidLayers.Length - 1; i++)
        {
            LiquidVolume.liquidLayers[i].color = hiddenColor;
        }
        var lastFilled = LiquidVolume.liquidLayers.ToList().FindLast(x => x.amount > 0); //.color = savedColors.Last();
        var index = LiquidVolume.liquidLayers.ToList().IndexOf(lastFilled);
        LiquidVolume.liquidLayers[index].color = savedColors.Last();
        LiquidVolume.UpdateLayers();
    }

    public void RemoveColor()
    {
        currColors--;
        if (currColors > 0)
            savedColors[savedColors.Count - 1] = colorsInTube[currColors - 1];
        LiquidVolume.liquidLayers[currColors].amount = 0f;

        UpdateColors();

        SetFoam();
    }

    private void SetFoam()
    {
        if (currColors > 0)
        {
            LiquidVolume.foamColor = LiquidVolume.liquidLayers[currColors - 1].color;
            LiquidVolume.foamThickness = FoamThickness;
        }
        else
        {
            LiquidVolume.foamThickness = 0;
        }

        LiquidVolume.UpdateLayers(true);
    }

    public void AddColor(Color color)
    {
        isAddingColor = true;

        ColorLerp = 0;

        LiquidVolume.liquidLayers[currColors].color = color;
        colorsInTube[currColors] = color;
        savedColors.Add(color);
        currColors++;

        SetFoam();

        if (isEmpty)
        {
            isEmpty = false;
            GM.RemoveEmpty();
        }

        if (currColors == 4 && colorsInTube[0] == colorsInTube[1] && colorsInTube[0] == colorsInTube[2] &&
            colorsInTube[0] == colorsInTube[3])
        {
            GM.AddFull(transform.position, true);
            isFull = true;
        }
    }

    private void OnMouseDown()
    {
        if (!IsReady)
            return;

        if (!isFull && !isrotating && _canMouseDown)
        {
            GM.TubeClicked(this.gameObject);
        }
    }

    private void RotateTube()
    {
        NextTube.isBusy = true;

        IsAddColor = true;
        rotationLerp = 0;
        {
            RotStart = RotationDataObject.RotationData[PlayerPrefs.GetInt("Tube", 0)].StartAngle[currColors - 1];
            RotEnd = RotationDataObject.RotationData[PlayerPrefs.GetInt("Tube", 0)].EndAngle[currColors - 1];
        }
        _pourSprite = Instantiate(PourSpriteObject);
        _pourSprite.GetComponentInChildren<SpriteRenderer>().color = colorsInTube[currColors - 1];
        _pourSprite.transform.position += transform.position;
        var offset = isInverted ? PourSpriteInvertYOffset : PourSpriteYOffset;
        _pourSprite.transform.position += offset;
        GM.HUD.Pour = _pourSprite;
        audioSource.Play();
        isrotating = true;
        GM.buttonsManager.DisableMenuButton();
        GM.addingColor = true;
    }

    public IEnumerator MoveToEndingPosition(float moveSpeed, GameObject otherTube)
    {
        float sideOffset = otherTube.transform.position.x > this.transform.position.x ? -1 : 1;
        Vector3 offset = _flaskDistance;
        if (sideOffset < 0)
        {
            isInverted = true;
            offset = new Vector3(_flaskDistance.x, _flaskDistance.y, _flaskDistance.z);
        }
        RotStart = RotationDataObject.RotationData[PlayerPrefs.GetInt("Tube", 0)].EndAngle[currColors - 1];
        _canMouseDown = false;
        var endPosition = otherTube.transform.position + offset;
        var start = transform.position;
        var startRotation = transform.eulerAngles;
        var endRotation = new Vector3(0, 0, RotStart * sideOffset);
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / moveSpeed;
            if (t > 1)
                t = 1;

            transform.position = Vector3.Lerp(start, endPosition, t);
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, t);

            yield return null;
        }


        RotateTube();
        isInverted = false;
    }

    private IEnumerator ReturnToStartingPosition(float returnTime)
    {
        float t = 0;
        _canMouseDown = false;
        var start = transform.position;
        var startRotation = transform.rotation;
        while (t < 1)
        {
            t += Time.deltaTime / returnTime;
            if (t > 1)
                t = 1;

            transform.position = Vector3.Lerp(start, Pos, t);
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(Vector3.zero), t);
            yield return null;
        }

        transform.position = Pos;
        transform.eulerAngles = Vector3.zero;
        _canMouseDown = true;
        GM.buttonsManager.EnableMenuButton();
    }

    private void OnDestroy()
    {
        if (_pourSprite != null)
        {
            Destroy(_pourSprite);
        }
    }

    private void OnBecameInvisible()
    {
        OnDestroy();
    }

    public void AdButtonClick()
    {
        OnAdBtnClick?.Invoke(this);
    }

    public void UpdateWatchCount()
    {
        adWatched++;
        if (adWatched == AdWatchCount)
        {
            adBtn.gameObject.SetActive(false);
            meshRenderer.material = normalMaterial;
        }
    }

    public void SetFlask(Flask flask)
    {
        _flask = flask;
    }
}

public interface ITubeController
{

}