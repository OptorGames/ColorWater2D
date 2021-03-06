using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using LiquidVolumeFX;

public class VideoTubeController : TubeController
{
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

    private float RotStart, RotEnd;
    public bool isrotating = false;
    private float rotationLerp;

    private GameManager GM;
    [HideInInspector] public TubeController NextTube;
    public SpriteRenderer tubeSR;
    [HideInInspector] public SortingGroup sorting;

    private bool IsAddColor;
    [Space(15)] public bool isFull, isEmpty, isBusy;
    private bool isAddingColor;

    private float ColorLerp;

    private AudioSource audioSource;
    private float _returnSpeed = 5;
    private bool _canMouseDown = true;

    private Vector3 _pourAngle = new Vector3(0, 0, 0f);
    private Vector3 _flaskDistance = new Vector3(0.1f, 0.6f, 0f);
    private Quaternion _pourRotation = Quaternion.identity;
    private GameObject _pourSprite;

    private void Start()
    {

        isAddingColor = false;
        isEmpty = false;
        isFull = false;
        IsAddColor = false;
        currColors = 0;
        Pos = transform.position;
        GM = FindObjectOfType<GameManager>();
        GM.tubesInGame.Add(this.gameObject);
        GM.tubeControllers.Add(this);
        audioSource = GM.audioSource;

        colorsInTube = new Color[LiquidVolume.liquidLayers.Length];
        for (int i = 0; i < 4; i++)
        {
            if (LiquidVolume.liquidLayers[i].amount > 0f)
            {
                colorsInTube[i] = LiquidVolume.liquidLayers[i].color;
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
    }

    private void Update()
    {
        if (isAddingColor)
        {

            ColorLerp += Time.deltaTime;
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
            //PourSpriteObject.transform.rotation = Quaternion.identity;

            rotationLerp += Time.deltaTime;
            if (rotationLerp >= 1)
            {
                rotationLerp = 1;
                float tempAngle1 = Mathf.Lerp(0, RotEnd, rotationLerp);
                transform.eulerAngles = new Vector3(0, 0, tempAngle1);


                if (currColors > 1 && colorsInTube[currColors - 1] == colorsInTube[currColors - 2])
                {
                    IsAddColor = true;
                    rotationLerp = 0;
                    RemoveColor();
                    RotStart = RotationData.SAngle4[currColors - 1];
                    RotEnd = RotationData.EAngle4[currColors - 1];
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
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, tempAngle));

                LiquidVolume.liquidLayers[currColors - 1].amount =
                Mathf.Clamp01(LiquidVolume.liquidLayers[currColors - 1].amount - Time.deltaTime * 0.22f);
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

    public void RemoveColor()
    {
        currColors--;
        LiquidVolume.liquidLayers[currColors].amount = 0f;

        if (currColors > 0)
        {
            LiquidVolume.foamColor = LiquidVolume.liquidLayers[currColors - 1].color;
        }
    }

    public void AddColor(Color colr)
    {
        isAddingColor = true;

        ColorLerp = 0;

        LiquidVolume.liquidLayers[currColors].color = colr;
        LiquidVolume.foamColor = colr;
        colorsInTube[currColors] = colr;
        currColors++;

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
            RotStart = RotationData.SAngle4[currColors - 1];
            RotEnd = RotationData.EAngle4[currColors - 1];
        }
        _pourSprite = Instantiate(PourSpriteObject);
        _pourSprite.GetComponentInChildren<SpriteRenderer>().color = colorsInTube[currColors - 1];
        _pourSprite.transform.position += transform.position;
        audioSource.Play();
        isrotating = true;
        GM.addingColor = true;
    }

    public IEnumerator MoveToEndingPosition(float moveSpeed, GameObject otherTube)
    {
        RotStart = RotationData.SAngle4[currColors - 1];
        _canMouseDown = false;
        while (transform.position != otherTube.transform.position + _flaskDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                otherTube.transform.position + _flaskDistance, moveSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, RotStart),
                moveSpeed / 4 * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        RotateTube();
    }

    private IEnumerator ReturnToStartingPosition(float returnSpeed)
    {
        _canMouseDown = false;
        while (transform.position != Pos)
        {
            transform.position = Vector3.MoveTowards(transform.position, Pos, returnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.zero, returnSpeed / 2 * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        transform.position = Pos;
        transform.eulerAngles = Vector3.zero;
        _canMouseDown = true;
    }
}