using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using LiquidVolumeFX;

public class VideoTubeController : TubeController
{
    public Vector3 Pos1;
    public GameObject ColorsPivot1;
    public GameObject PourSpriteObject1;

    public int currColors1;

    [HideInInspector]
    public Color[] colorsInTube1;

    [HideInInspector]
    public GameObject[] ColorObjects1;

    public LiquidVolume LiquidVolume1;
    public float ColorLayerAmount1 = 0.22f;

    private float RotStart, RotEnd;
    public bool isrotating1 = false;
    private float rotationLerp;

    private GameManager GM;
    [HideInInspector] public TubeController NextTube1;
    public SpriteRenderer tubeSR1;
    [HideInInspector] public SortingGroup sorting1;

    private bool IsAddColor;
    [Space(15)] public bool isFull1, isEmpty1, isBusy1;
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
        isEmpty1 = false;
        isFull1 = false;
        IsAddColor = false;
        currColors1 = 0;
        Pos1 = transform.position;
        GM = FindObjectOfType<GameManager>();
        GM.tubesInGame.Add(this.gameObject);
        GM.tubeControllers.Add(this);
        audioSource = GM.audioSource;

        colorsInTube1 = new Color[LiquidVolume1.liquidLayers.Length];
        for (int i = 0; i < 4; i++)
        {
            if (LiquidVolume1.liquidLayers[i].amount > 0f)
            {
                colorsInTube1[i] = LiquidVolume1.liquidLayers[i].color;
                ++currColors1;
            }
        }

        if (currColors1 == 0)
        {
            isEmpty1 = true;
            GM.AddEmpty();
        }

        if (currColors1 == 4 && colorsInTube1[0] == colorsInTube1[1] && colorsInTube1[0] == colorsInTube1[2] &&
            colorsInTube1[0] == colorsInTube1[3])
        {
            GM.AddFull(transform.position, false);
            isFull1 = true;
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

            LiquidVolume1.liquidLayers[currColors1 - 1].amount = ColorLayerAmount1 * ColorLerp;
            LiquidVolume1.UpdateLayers(true);
        }

        if (isrotating1)
        {
            //PourSpriteObject1.transform.rotation = Quaternion.identity;

            rotationLerp += Time.deltaTime;
            if (rotationLerp >= 1)
            {
                rotationLerp = 1;
                float tempAngle1 = Mathf.Lerp(0, RotEnd, rotationLerp);
                transform.eulerAngles = new Vector3(0, 0, tempAngle1);


                if (currColors1 > 1 && colorsInTube1[currColors1 - 1] == colorsInTube1[currColors1 - 2])
                {
                    IsAddColor = true;
                    rotationLerp = 0;
                    RemoveColor1();
                    RotStart = RotationData.StartAngle[currColors1 - 1];
                    RotEnd = RotationData.EndEngle[currColors1 - 1];
                }
                else
                {
                    isrotating1 = false;
                    audioSource.Stop();

                    NextTube1.isBusy = false;
                    Destroy(_pourSprite);
                    RemoveColor1();

                    StartCoroutine(ReturnToStartingPosition(_returnSpeed));
                    GM.addingColor = false;

                    if (currColors1 == 0)
                    {
                        isEmpty1 = true;
                        GM.AddEmpty();
                    }
                }
            }
            else
            {
                if (IsAddColor)
                {
                    IsAddColor = false;
                    NextTube1.AddColor(colorsInTube1[currColors1 - 1]);
                }

                float tempAngle = Mathf.Lerp(RotStart, RotEnd, rotationLerp);
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, tempAngle));

                LiquidVolume1.liquidLayers[currColors1 - 1].amount =
                Mathf.Clamp01(LiquidVolume1.liquidLayers[currColors1 - 1].amount - Time.deltaTime * 0.22f);
                LiquidVolume1.UpdateLayers(true);
            }
        }
    }

    public bool CheckCapacity1(int colorCount, int count_curr_colors, Color colr)
    {
        if (currColors1 == 0)
            return true;
        if (4 - currColors1 < colorCount)
            return false;
        if (colorsInTube1[currColors1 - 1] != colr)
            return false;

        return true;
    }

    public void PourColor1(GameObject otherTube)
    {
        NextTube1 = otherTube.GetComponent<TubeController>();

        _pourRotation = Quaternion.LookRotation(otherTube.transform.position, Vector3.forward);

        int MatchCount;
        MatchCount = 1;

        for (int i = currColors1 - 2; i >= 0; i--)
        {
            if (colorsInTube1[i] == colorsInTube1[i + 1])
                MatchCount++;
            else
                break;
        }

        if (currColors1 > 0 && NextTube1.CheckCapacity(MatchCount, currColors1, colorsInTube1[currColors1 - 1]))
        {
            GM.SaveTubes();

            float y = 0;

            if (PlayerPrefs.GetInt("Tube") > 0)
            {
                if (NextTube1.currColors == 0)
                    y = 1.03f;
                else if (NextTube1.currColors == 1)
                    y = 0.95f;
                else if (NextTube1.currColors == 2)
                    y = 0.81f;
                else if (NextTube1.currColors == 3)
                    y = 0.72f;
            }
            else
            {
                if (NextTube1.currColors == 0)
                    y = 1f;
                else if (NextTube1.currColors == 1)
                    y = 0.9f;
                else if (NextTube1.currColors == 2)
                    y = 0.8f;
                else if (NextTube1.currColors == 3)
                    y = 0.7f;
            }

            StartCoroutine(MoveToEndingPosition1(_returnSpeed, otherTube));
        }
        else StartCoroutine(ReturnToStartingPosition(_returnSpeed));
    }

    public void RemoveColor1()
    {
        currColors1--;
        LiquidVolume1.liquidLayers[currColors1].amount = 0f;

        if (currColors1 > 0)
        {
            LiquidVolume1.foamColor = LiquidVolume1.liquidLayers[currColors1 - 1].color;
        }
    }

    public void AddColor1(Color colr)
    {
        isAddingColor = true;

        ColorLerp = 0;

        LiquidVolume1.liquidLayers[currColors1].color = colr;
        LiquidVolume1.foamColor = colr;
        colorsInTube1[currColors1] = colr;
        currColors1++;

        if (isEmpty1)
        {
            isEmpty1 = false;
            GM.RemoveEmpty();
        }

        if (currColors1 == 4 && colorsInTube1[0] == colorsInTube1[1] && colorsInTube1[0] == colorsInTube1[2] &&
            colorsInTube1[0] == colorsInTube1[3])
        {
            GM.AddFull(transform.position, true);
            isFull1 = true;
        }
    }

    private void OnMouseDown()
    {
        if (!isFull1 && !isrotating1 && _canMouseDown)
        {
            GM.TubeClicked(this.gameObject);
        }
    }

    private void RotateTube()
    {
        NextTube1.isBusy = true;

        IsAddColor = true;
        rotationLerp = 0;
        {
            RotStart = RotationData.StartAngle[currColors1 - 1];
            RotEnd = RotationData.EndEngle[currColors1 - 1];
        }
        _pourSprite = Instantiate(PourSpriteObject1);
        _pourSprite.GetComponentInChildren<SpriteRenderer>().color = colorsInTube1[currColors1 - 1];
        _pourSprite.transform.position += transform.position;
        audioSource.Play();
        isrotating1 = true;
        GM.addingColor = true;
    }

    public IEnumerator MoveToEndingPosition1(float moveSpeed, GameObject otherTube)
    {
        RotStart = RotationData.StartAngle[currColors1 - 1];
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
        while (transform.position != Pos1)
        {
            transform.position = Vector3.MoveTowards(transform.position, Pos1, returnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.zero, returnSpeed / 2 * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        transform.position = Pos1;
        transform.eulerAngles = Vector3.zero;
        _canMouseDown = true;
    }
}