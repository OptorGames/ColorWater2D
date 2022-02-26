using UnityEngine;

public class TubeController : MonoBehaviour
{
    public Vector3 Pos;
    public GameObject ColorsPivot;
    public GameObject PourSprite;

    public int currColors;
    public Color[] colorsInTube;
    public GameObject[] ColorObjects;
    public SpriteRenderer[] ColorObjects_Renderers;

    private float RotStart, RotEnd;
    public bool isrotating = false;
    private float rotationLerp;

    private GameManager GM;
    public TubeController NextTube;
    public SpriteRenderer tubeSR;

    private bool IsAddColor;
    public bool isFull, isEmpty, isBusy;
    private bool isAddingColor;

    private float ColorLerp;

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

        for (int i = 3; i >= 0; i--)
        {
            if (ColorObjects[i].activeInHierarchy)
            {
                colorsInTube[i] = ColorObjects_Renderers[i].color;
                currColors++;
            }
        }

        if (currColors == 0)
        {
            isEmpty = true;
            GM.AddEmpty();
        }

        if (currColors == 4 && colorsInTube[0] == colorsInTube[1] && colorsInTube[0] == colorsInTube[2] && colorsInTube[0] == colorsInTube[3])
        {
            GM.AddFull();
            isFull = true;

        }
    }

    private void Update()
    {
        if (isAddingColor)
        {
            ColorLerp += Time.deltaTime * 2;
            if (ColorLerp > 1)
            {
                isAddingColor = false;
                ColorLerp = 1;
            }
            ColorObjects[currColors - 1].transform.localScale = new Vector3(1.8375f, 0.25f * ColorLerp, 1);

        }

        if (isrotating)
        {
            rotationLerp += Time.deltaTime * 2;
            if (rotationLerp >= 1)
            {
                rotationLerp = 1;

                float tempAngle1 = Mathf.Lerp(0, RotEnd, rotationLerp);
                transform.eulerAngles = new Vector3(0, 0, tempAngle1);
                ColorsPivot.transform.eulerAngles = Vector3.zero;
                ColorsPivot.transform.localScale = new Vector3(10, 1 - (tempAngle1 / 180), 1);

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
                    NextTube.isBusy = false;
                    PourSprite.SetActive(false);
                    RemoveColor();
                    transform.position = Pos;

                    NextTube.tubeSR.sortingOrder = 0;
                    for (int i = 0; i < NextTube.ColorObjects_Renderers.Length; i++)
                    {
                        NextTube.ColorObjects_Renderers[i].sortingOrder = 0;
                    }

                    GM.SwitchTubes(true, Pos);
                    GM.addingColor = false;

                    transform.eulerAngles = Vector3.zero;
                    ColorsPivot.transform.eulerAngles = Vector3.zero;
                    ColorsPivot.transform.localScale = Vector3.one;

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
                transform.eulerAngles = new Vector3(0, 0, tempAngle);
                ColorsPivot.transform.eulerAngles = Vector3.zero;
                ColorsPivot.transform.localScale = new Vector3(10, 1 - (tempAngle / 180), 1);
                PourSprite.transform.eulerAngles = Vector3.zero;

                if (tempAngle > RotStart)
                {
                    ColorObjects[currColors - 1].transform.localScale = new Vector3(1, Mathf.Lerp(0, 0.25f, (RotEnd - tempAngle) / (RotEnd - RotStart)), 1);
                }
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

    public void PourColor(GameObject OtherTube)
    {
        NextTube = OtherTube.GetComponent<TubeController>();

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

            transform.position = OtherTube.transform.position + new Vector3(2f, 7f, 0f);
            RotateTube();
        }
        else transform.position = Pos;

    }

    public void RemoveColor()
    {
        currColors--;
        ColorObjects[currColors].SetActive(false);
    }

    public void AddColor(Color colr)
    {
        ColorObjects[currColors].SetActive(true);
        isAddingColor = true;

        ColorObjects[currColors].transform.localScale = new Vector3(1.8375f, 0, 1);
        ColorLerp = 0;

        ColorObjects_Renderers[currColors].color = colr;
        colorsInTube[currColors] = colr;
        currColors++;

        if (isEmpty)
        {
            isEmpty = false;
            GM.RemoveEmpty();
        }

        if (currColors == 4 && colorsInTube[0] == colorsInTube[1] && colorsInTube[0] == colorsInTube[2] && colorsInTube[0] == colorsInTube[3])
        {
            GM.AddFull();
            isFull = true;
        }
    }

    private void OnMouseDown()
    {
        if (!isFull && !isrotating)
        {
            GM.TubeClicked(this.gameObject);
        }
    }

    private void RotateTube()
    {
        NextTube.tubeSR.sortingOrder = 1;
        for (int i = 0; i < NextTube.ColorObjects_Renderers.Length; i++)
        {
            NextTube.ColorObjects_Renderers[i].sortingOrder = 1;
        }

        NextTube.isBusy = true;

        IsAddColor = true;
        rotationLerp = 0;
        {
            RotStart = RotationData.SAngle4[currColors - 1];
            RotEnd = RotationData.EAngle4[currColors - 1];
        }
        PourSprite.SetActive(true);
        PourSprite.GetComponentInChildren<SpriteRenderer>().color = colorsInTube[currColors - 1];
        isrotating = true;
        GM.addingColor = true;
        GM.SwitchTubes(false, transform.position);
    }
}
