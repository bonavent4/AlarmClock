using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Numbers : MonoBehaviour
{
    public GameObject numberList;
    [SerializeField] GameObject numberPrefab;
    public float offset;
    public int amountOfNumbers;
    float currentOffset;
    public float startY;
    [SerializeField] float force;
    public bool isBeingTouched;
    [SerializeField] float maxYVel;
    public int number;
    private void Awake()
    {
        startY = numberList.transform.position.y;
        instantiateNumbers();
    }
    private void Update()
    {
        if (!isBeingTouched)
        {
            float multiplier = 1;
            if (number != (int)Mathf.Round((numberList.transform.position.y - startY) / offset))
            {
                number = (int)Mathf.Round((numberList.transform.position.y - startY) / offset);

                if (number < 0)
                    number = 0;
                if (number > amountOfNumbers)
                    number = amountOfNumbers;
            }
            if (Mathf.Round((numberList.transform.position.y - startY) / offset) < -1 || Mathf.Round((numberList.transform.position.y - startY) / offset) > amountOfNumbers + 1)
            {
                numberList.GetComponent<Rigidbody2D>().velocity = new Vector2(0, numberList.GetComponent<Rigidbody2D>().velocity.y / 30);
                  multiplier = 2;
            }
            if (numberList.GetComponent<Rigidbody2D>().velocity.y < maxYVel && numberList.GetComponent<Rigidbody2D>().velocity.y > -maxYVel)
            {
                    numberList.GetComponent<Rigidbody2D>().AddForce(transform.up * force * Time.deltaTime * (number - (numberList.transform.position.y - startY) / offset) * multiplier + new Vector3(0, -numberList.GetComponent<Rigidbody2D>().velocity.y, 0));
               
                
            }
        }
      
    }
    void instantiateNumbers()
    {
        for (int i = 0; i <= amountOfNumbers; i++)
        {
            GameObject g = Instantiate(numberPrefab, numberList.transform);
            g.transform.position += new Vector3(0, currentOffset, 0);
            currentOffset -= offset;
            string extra = "";
            if (i < 10)
                extra = "0";
            g.GetComponent<TextMeshProUGUI>().text = extra + i.ToString();
        }
    }
}

