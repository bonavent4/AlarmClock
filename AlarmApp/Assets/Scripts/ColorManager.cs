using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    

    [SerializeField] Image[] primærFarver;
    [SerializeField] Image[] sekundærFarver;
    [SerializeField] Image[] tærriterFarver;

    [SerializeField] Color primær;
    [SerializeField] Color sekundær;
    [SerializeField] Color tærriter;

    private void Awake()
    {
        SetColors(primærFarver, primær);
        SetColors(sekundærFarver, sekundær);
        SetColors(tærriterFarver, tærriter);
        Camera.main.backgroundColor = primær;
    }
    void SetColors(Image[] imageFarver, Color Farve)
    {
        for (int i = 0; i < imageFarver.Length; i++)
        {
            imageFarver[i].color = Farve;
        }
    }
}
