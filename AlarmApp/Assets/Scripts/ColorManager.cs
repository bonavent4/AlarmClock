using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorManager : MonoBehaviour
{
    [SerializeField] Image[] prim�rFarver;
    [SerializeField] Image[] sekund�rFarver;
    [SerializeField] Image[] t�rriterFarver;
    [SerializeField] TextMeshProUGUI[] tekstT�rrit�rFarver;

    [SerializeField] Color[] prim�r;
    [SerializeField] Color[] sekund�r;
    [SerializeField] Color[] t�rriter;
    [SerializeField] int index;

    private void Awake()
    {
        SetAllColors();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("cc");
            index++;
            if (index >= prim�r.Length)
                index = 0;

            SetAllColors();
        }
    }
    void SetAllColors()
    {
        SetColors(prim�rFarver, prim�r[index]);
        SetColors(sekund�rFarver, sekund�r[index]);
        SetColors(t�rriterFarver, t�rriter[index]);
        SetColors(tekstT�rrit�rFarver, t�rriter[index]);
        Camera.main.backgroundColor = prim�r[index];
    }
    void SetColors(Image[] imageFarver, Color Farve)
    {
        for (int i = 0; i < imageFarver.Length; i++)
        {
            imageFarver[i].color = new Color(Farve.r, Farve.g, Farve.b, imageFarver[i].color.a);
        }
    }
    void SetColors(TextMeshProUGUI[] imageFarver, Color Farve)
    {
        for (int i = 0; i < imageFarver.Length; i++)
        {
            imageFarver[i].color = new Color(Farve.r, Farve.g, Farve.b, imageFarver[i].color.a);
        }
    }
}
