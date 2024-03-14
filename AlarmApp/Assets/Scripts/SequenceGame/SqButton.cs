using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqButton : MonoBehaviour
{
    Color originalColor;
    [SerializeField] Color PressedColor;
    [SerializeField] Color CorrectColor;
    [SerializeField] Color WrongColor;

    bool isMorphing;
    Color morphToColor;
    [SerializeField] float speed;
    private void Awake()
    {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    public void Pressed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = CorrectColor;
        turnGray();
    }
    public void turnWhite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = PressedColor;
        turnGray();
    }
    public void turnGray()
    {
        
            isMorphing = true;
            morphToColor = originalColor;
       // gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }
    public void turnRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = WrongColor;
        turnGray();
    }
    private void Update()
    {
        if (isMorphing)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(gameObject.GetComponent<SpriteRenderer>().color, morphToColor, speed * Time.deltaTime);
            if (gameObject.GetComponent<SpriteRenderer>().color == morphToColor)
                isMorphing = false;
        }
    }
}   
