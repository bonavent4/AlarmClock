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
    [SerializeField] List<GameObject> numbers = new List<GameObject>();
    [SerializeField] List<float> numbersStartY = new List<float>();
    [SerializeField] float multiplier;

    float previousY;
    private void Awake()
    {
        startY = numberList.transform.position.y;
        instantiateNumbers();
    }
    private void Update()
    {
        if (number != (int)Mathf.Round((numberList.transform.position.y - startY) / offset))
        {
            number = (int)Mathf.Round((numberList.transform.position.y - startY) / offset);

            if (number < 0)
                number = 0;
            if (number > amountOfNumbers)
                number = amountOfNumbers;
        }
        if (!isBeingTouched)
        {
            float multiplier = 1;

            //check if over maxnumber + 1 or under -1, if yes then slow down like hell
            if (Mathf.Round((numberList.transform.position.y - startY) / offset) < -1 || Mathf.Round((numberList.transform.position.y - startY) / offset) > amountOfNumbers + 1)
            {
                numberList.GetComponent<Rigidbody2D>().velocity = new Vector2(0, numberList.GetComponent<Rigidbody2D>().velocity.y / 30);
                  multiplier = 2;
            }

            // adds force according to how close it is to a number to align with the number.
            if (numberList.GetComponent<Rigidbody2D>().velocity.y < maxYVel && numberList.GetComponent<Rigidbody2D>().velocity.y > -maxYVel)
            {
                    numberList.GetComponent<Rigidbody2D>().AddForce(transform.up * force * Time.deltaTime * (number - (numberList.transform.position.y - startY) / offset) * multiplier + new Vector3(0, -numberList.GetComponent<Rigidbody2D>().velocity.y, 0));
               
                
            }
        }
        
        if(previousY != numberList.transform.position.y)
        {
           // Debug.Log("did it");
            int[] minAndMax = {number - 6, number + 6 };
            if (minAndMax[0] < 0)
                minAndMax[0] = 0;
            if (minAndMax[1] > amountOfNumbers)
                minAndMax[1] = amountOfNumbers;
            for (int i = minAndMax[0]; i <= minAndMax[1]; i++)
            {
                if((numbers[i].transform.position.y - startY) / offset > 0)
                {
                    // Debug.Log((i * offset) + " , " + ((numbers[i].transform.position.y - startY) / offset));
                    float y = (4 - ((numbers[i].transform.position.y - startY) / offset)) / 4;
                    numbers[i].transform.localScale = new Vector3(numbers[i].transform.localScale.x, y, numbers[i].transform.localScale.z);
                    numbers[i].transform.localPosition = new Vector3(numbers[i].transform.localPosition.x, numbersStartY[i] + ( ((numbers[i].transform.position.y - startY) / offset * multiplier)), numbers[i].transform.localPosition.z);
               
                }
                else 
                {
                    float y = (4 + ((numbers[i].transform.position.y - startY) / offset)) / 4;
                    numbers[i].transform.localScale = new Vector3(numbers[i].transform.localScale.x, y, numbers[i].transform.localScale.y);
                    numbers[i].transform.localPosition = new Vector3(numbers[i].transform.localPosition.x, numbersStartY[i] + (((numbers[i].transform.position.y - startY) / offset * multiplier)), numbers[i].transform.localPosition.z);
                }
            }

            previousY = numberList.transform.position.y;
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

            numbers.Add(g);
            numbersStartY.Add(g.transform.localPosition.y);
            numberList.transform.position += new Vector3(0, .2f, 0);
        }
    }
}

