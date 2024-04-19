using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerButton : MonoBehaviour
{
    public int Hours;
    public int Minutes;

    TimeManager tManager;
    SaveData saveD;
    SettingsManager sManager;

    public GameObject snoozeText;
    public int snoozeTime;

    public bool isOn;
    [SerializeField] Animator toggleAnim;

    
    private void Awake()
    {
        tManager = FindObjectOfType<TimeManager>();
        saveD = tManager.gameObject.GetComponent<SaveData>();
        sManager = tManager.gameObject.GetComponent<SettingsManager>();
    }
    public void OpenSliderMenu()
    {
        tManager.removeTimer(gameObject, Hours, Minutes, isOn);
    }

    public void ActivateSnoozeText()
    {
        snoozeTime = int.Parse(sManager.Settings[2][saveD.saveObject.snoozeAmount[tManager.Timers.IndexOf(gameObject)]]);
        snoozeText.GetComponent<TextMeshProUGUI>().text = "+" +  snoozeTime + " min";
        snoozeText.SetActive(true);
    }
    public void ToggleOnAndOff()
    {
        if (isOn)
        {
            isOn = false;
            toggleAnim.SetBool("Toggle", false);

            tManager.gameObject.GetComponent<SaveData>().saveObject.isOn[tManager.Timers.IndexOf(gameObject)] = false;
        }
        else
        {
            toggleAnim.SetBool("Toggle", true);
            isOn = true;

            tManager.gameObject.GetComponent<SaveData>().saveObject.isOn[tManager.Timers.IndexOf(gameObject)] = true;
        }
        tManager.gameObject.GetComponent<SaveData>().saveTheData();
    }
    public void ControllToggle()
    {
        toggleAnim.SetBool("Toggle", isOn);
    }
}
