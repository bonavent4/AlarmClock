using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorManager : MonoBehaviour
{
    [SerializeField] Image[] primærFarver;
    [SerializeField] Image[] sekundærFarver;
    [SerializeField] Image[] tærriterFarver;
    [SerializeField] TextMeshProUGUI[] tekstTærritærFarver;

    [SerializeField] Color primær;
    [SerializeField] Color sekundær;
    [SerializeField] Color tærriter;

    private void Awake()
    {
        SetColors(primærFarver, primær);
        SetColors(sekundærFarver, sekundær);
        SetColors(tærriterFarver, tærriter);
        SetColors(tekstTærritærFarver, tærriter);
        Camera.main.backgroundColor = primær;
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
