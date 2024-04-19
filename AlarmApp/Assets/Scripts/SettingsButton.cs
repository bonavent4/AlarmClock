using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject line;
    public int index;

    public void ClickedButton()
    {
        FindObjectOfType<SettingsManager>().SettingsButton(index);
    }
}
