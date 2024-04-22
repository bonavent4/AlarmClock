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

    [SerializeField] Color[] primær;
    [SerializeField] Color[] sekundær;
    [SerializeField] Color[] tærriter;
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
            if (index >= primær.Length)
                index = 0;

            SetAllColors();
        }
    }
    void SetAllColors()
    {
        SetColors(primærFarver, primær[index]);
        SetColors(sekundærFarver, sekundær[index]);
        SetColors(tærriterFarver, tærriter[index]);
        SetColors(tekstTærritærFarver, tærriter[index]);
        Camera.main.backgroundColor = primær[index];
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
