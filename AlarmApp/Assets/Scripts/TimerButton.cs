using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerButton : MonoBehaviour
{
    public int Hours;
    public int Minutes;

    TimeManager tManager;

    public GameObject snoozeText;
    public int snoozeTime;

    public bool isOn;
    [SerializeField] Animator toggleAnim;
    private void Awake()
    {
        tManager = FindObjectOfType<TimeManager>();
    }
    public void OpenSliderMenu()
    {
        tManager.removeTimer(gameObject, Hours, Minutes, isOn);
    }

    public void ActivateSnoozeText()
    {
        snoozeText.GetComponent<TextMeshProUGUI>().text = "+" + snoozeTime + " min";
        snoozeText.SetActive(true);
    }
    public void ToggleOnAndOff()
    {
        if (isOn)
        {
            isOn = false;
            toggleAnim.SetBool("Toggle", false);
        }
        else
        {
            toggleAnim.SetBool("Toggle", true);
            isOn = true;
        }
    }
    public void ControllToggle()
    {
        toggleAnim.SetBool("Toggle", isOn);
    }
}
