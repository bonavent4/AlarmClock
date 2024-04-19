using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    
    [SerializeField] GameObject startPosition;
    [SerializeField] GameObject backgroundParent;
    [SerializeField] float offset;
    List<GameObject> settingsPlaced = new List<GameObject>();

    [SerializeField] GameObject settingsButtonPrefab;
    [SerializeField] GameObject[] backgroundPrefabs;

    [SerializeField] GameObject sliderMenu;
    [SerializeField] GameObject settingsMenu;
    int index;
    
    public List<string[]> Settings = new List<string[]> { new string[] { "Easy", "Medium", "Hard"}, new string[] { "Frikvater"}, new string[] {"1", "3", "5", "7", "10", "15", "25" } };
    public int[] settingsIndex;

    [SerializeField] TextMeshProUGUI[] SettingsText;
    int timerIndex;
    public void OpenSettings(int _index)
    {
        sliderMenu.SetActive(false);
        settingsMenu.SetActive(true);
        index = _index;
        for (int i = 0; i < Settings[index].Length; i++)
        {
            GameObject g = Instantiate(settingsButtonPrefab, startPosition.transform);
            g.transform.position += new Vector3(0, -offset * i, 0);

            settingsPlaced.Add(g);

            g.GetComponentInChildren<TextMeshProUGUI>().text = Settings[index][i];
            GameObject d = null;
            if(i == 0 || i == Settings[index].Length - 1)
                 d =  Instantiate(backgroundPrefabs[0], g.transform.position, Quaternion.identity);
            else
                 d = Instantiate(backgroundPrefabs[1], g.transform.position, Quaternion.identity);

            d.transform.parent = backgroundParent.transform;
            
            if(i == Settings[index].Length - 1)
                g.GetComponent<SettingsButton>().line.SetActive(false);

            g.GetComponent<SettingsButton>().index = i;
        }
    }
    public void SettingsButton(int _index)
    {
       // Debug.Log(Settings[index][_index]);
        settingsIndex[index] = _index; 
        DestroyAndOpenSlider();
    }
    public void GoBackButton()
    {
        DestroyAndOpenSlider();
    }
    void DestroyAndOpenSlider()
    {
        
        settingsMenu.SetActive(false);
        sliderMenu.SetActive(true);
        foreach (Transform child in backgroundParent.transform)
        {
            if (child != backgroundParent.transform)
                Destroy(child.gameObject);
        }
        foreach (GameObject g in settingsPlaced)
        {
            Destroy(g);
        }
        
        SetSettingsText();
    }
    public void SetSettingsText()
    {

        for (int i = 0; i < settingsIndex.Length; i++)
        {
            SettingsText[i].text = Settings[i][settingsIndex[i]];
        }
    }
    public void settingsIndexes(int[] sI)
    {
        settingsIndex = sI;
    }
}
