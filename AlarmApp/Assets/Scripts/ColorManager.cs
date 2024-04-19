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

    [SerializeField] Color prim�r;
    [SerializeField] Color sekund�r;
    [SerializeField] Color t�rriter;

    private void Awake()
    {
        SetColors(prim�rFarver, prim�r);
        SetColors(sekund�rFarver, sekund�r);
        SetColors(t�rriterFarver, t�rriter);
        SetColors(tekstT�rrit�rFarver, t�rriter);
        Camera.main.backgroundColor = prim�r;
    }
    void SetColors(Image[] imageFarver, Color Farve)
    {
        for (int i = 0; i < imageFarver.Length; i++)
        {
            imageFarver[i].color = new Color(Farve.r, Farve.b, Farve.g, imageFarver[i].color.a);
        }
    }
    void SetColors(TextMeshProUGUI[] imageFarver, Color Farve)
    {
        for (int i = 0; i < imageFarver.Length; i++)
        {
            imageFarver[i].color = Farve;
        }
    }
}
