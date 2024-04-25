using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlothManager : MonoBehaviour
{
    //placing of sloth
    [SerializeField] GameObject sloth;
    [SerializeField] GameObject[] slothPlacements;

    // Point System
    SaveData saveD;

    [SerializeField] TextMeshProUGUI pointsText;

    [SerializeField] GameObject pointsAddedText;

    private void Awake()
    {
        saveD = gameObject.GetComponent<SaveData>();
    }
    private void Start()
    {
        pointsText.text = ": " + saveD.saveObject.Points.ToString();
    }

    //placing of sloth
    public void PlaceSloth(int _index)
    {
        sloth.SetActive(true);
        sloth.transform.parent = slothPlacements[_index].transform;
        sloth.transform.localPosition = new Vector3(0, 0, 0);
        sloth.transform.localScale = new Vector3(1, 1, 1);
    }
    public void SlothInactive()
    {
        sloth.SetActive(false);
    }

    // Point System
    public void AddOrTakePoints(int number)
    {
        saveD.saveObject.Points += number;
        pointsText.text = ": " + saveD.saveObject.Points.ToString();
        saveD.saveTheData();

        if(number > 0)
        {
            pointsAddedText.SetActive(true);
            pointsAddedText.GetComponent<TextMeshProUGUI>().text = "+" + number + " ZCoins";
            Invoke("PointsTextInactive", 3);
        }
    }
    void PointsTextInactive()
    {
        pointsAddedText.SetActive(false);
    }
}
