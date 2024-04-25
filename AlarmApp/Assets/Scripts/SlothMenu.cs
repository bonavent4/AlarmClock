using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlothMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] GameObject[] optionsButtons;

    [SerializeField] List<Sprite[]> options = new List<Sprite[]>();
    [SerializeField] List<Vector2[]> optionsOffset = new List<Vector2[]>();
    //points it costs to unlcok clothes
    List<int[]> clothingCost = new List<int[]> { new int[] {5, 10, 15, 20}, new int[] {10, 15, 20}, new int[] {10} };

    [SerializeField] Sprite[] allHats;
    [SerializeField] Vector2[] allHatsOffset;
    [SerializeField] Sprite[] allFace;
    [SerializeField] Vector2[] allFaceOffset;
    [SerializeField] Sprite[] allClothes;
    [SerializeField] Vector2[] allClothesOffset;
    int index = 100;

    [SerializeField] GameObject lightUpBackground;
    [SerializeField] GameObject[] slothClothes;

    SaveData SaveD;
    SlothManager slothM;

    //colors
    [SerializeField] Color[] clothesColors;
    [SerializeField] GameObject colorMenu;
    [SerializeField] Image colorShower;

    //menues
    [SerializeField] GameObject menu;
    [SerializeField] GameObject timerMenu;

    //savedata
    List<bool[]> ownedClothes = new List<bool[]>();

    private void Awake()
    {
        SaveD = gameObject.GetComponent<SaveData>();
        slothM = gameObject.GetComponent<SlothManager>();
    }
    private void Start()
    {
        
        options = new List<Sprite[]> { allHats, allFace, allClothes };
        optionsOffset = new List<Vector2[]> { allHatsOffset, allFaceOffset, allClothesOffset };

        ownedClothes = new List<bool[]> { SaveD.saveObject.hatsOwned, SaveD.saveObject.faceOwned, SaveD.saveObject.clothingOwned};

        SlothMenuButton(0);

        for (int i = 0; i < slothClothes.Length; i++)
        {
            if(SaveD.saveObject.slothClothIndexes[i] >= 0)
            {
                slothClothes[i].SetActive(true);
                slothClothes[i].GetComponent<Image>().sprite = options[i][SaveD.saveObject.slothClothIndexes[i]];
            }
        }
        setColor();
    }
    public void SlothMenuButton(int _index)
    {
        if(_index != index)
        {
            index = _index;
            for (int i = 0; i < optionsButtons.Length; i++)
            {
                if(i > options[index].Length - 1)
                    optionsButtons[i].SetActive(false);
                else
                {
                    optionsButtons[i].SetActive(true);
                    GameObject g = optionsButtons[i].GetComponentInChildren<RectMask2D>().gameObject;
                    g.GetComponent<Image>().sprite = options[index][i];
                    g.transform.localPosition = optionsOffset[index][i];

                    //unlocked or not
                    if (ownedClothes[index][i])
                        optionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                    else
                        optionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = clothingCost[index][i] + " ZCoins";
                }
            }
            if (SaveD.saveObject.slothClothIndexes[index] < 0)
                lightUpBackground.SetActive(false);
            else
            {
                lightUpBackground.SetActive(true);
                lightUpBackground.transform.position = optionsButtons[SaveD.saveObject.slothClothIndexes[index]].transform.position;
            }
            // hvis man er på tøjmenuen
            if (index == 2)
                colorMenu.SetActive(true);
            else
                colorMenu.SetActive(false);
                
        }
    }
    public void SlothOptionButton(int _index)
    {
        if(SaveD.saveObject.slothClothIndexes[index] != _index)
        {
            if(ownedClothes[index][_index] == true || (ownedClothes[index][_index] == false && SaveD.saveObject.Points >= clothingCost[index][_index]))
            {
                if(ownedClothes[index][_index] != true)
                {
                    ownedClothes[index][_index] = true;
                    slothM.AddOrTakePoints(-clothingCost[index][_index]);
                    optionsButtons[_index].GetComponentInChildren<TextMeshProUGUI>().text = "";
                }

                SaveD.saveObject.slothClothIndexes[index] = _index;
                slothClothes[index].SetActive(true);
                slothClothes[index].GetComponent<Image>().sprite = options[index][_index];
                lightUpBackground.SetActive(true);
                lightUpBackground.transform.position = optionsButtons[_index].transform.position;
            }
        }
        else
        {
            SaveD.saveObject.slothClothIndexes[index] = -1;
            slothClothes[index].SetActive(false);
            lightUpBackground.SetActive(false);
        }
        SaveD.saveTheData();
    }
    public void ColorArrowButton(int number)
    {
        SaveD.saveObject.ClothingColorIndex += number;
        if (SaveD.saveObject.ClothingColorIndex > clothesColors.Length - 1)
            SaveD.saveObject.ClothingColorIndex = 0;
        if (SaveD.saveObject.ClothingColorIndex < 0)
            SaveD.saveObject.ClothingColorIndex = clothesColors.Length - 1;

        SaveD.saveTheData();
        setColor();
    }
    void setColor()
    {
        slothClothes[2].GetComponent<Image>().color = clothesColors[SaveD.saveObject.ClothingColorIndex];
        colorShower.color = clothesColors[SaveD.saveObject.ClothingColorIndex];
    }
    //open and close menues
    public void OpenAndCloseMenues(bool slothMenuActive)
    {
        menu.SetActive(slothMenuActive);
        timerMenu.SetActive(!slothMenuActive);

        if (slothMenuActive)
            slothM.PlaceSloth(0);
        else
            slothM.SlothInactive();
    }

}
