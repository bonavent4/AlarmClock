using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchInput : MonoBehaviour
{
    MiniGameManager mGManager;
    [SerializeField] GameObject Box;
    int number = 0;
    RaycastHit2D hit;

    GameObject theObject;
    float pY;
    [SerializeField] float force;
    private void Awake()
    {
        mGManager = FindObjectOfType<MiniGameManager>();
    }
    private void Update()
    {
        if(!mGManager.isInGame && Input.touchCount == 1)
        {
            
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                hit = Physics2D.Raycast(pos, Camera.main.transform.forward);
                if (hit.collider != null)
                {
                    theObject = hit.collider.gameObject;
                    theObject.GetComponent<Numbers>().numberList.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    pY = Input.touches[0].position.y;

                    theObject.GetComponent<Numbers>().isBeingTouched = true;
                }
            }
            if(theObject!= null)
            {
                
                if (pY != Input.touches[0].position.y)
                {
                    theObject.GetComponent<Numbers>().numberList.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    Numbers ns = theObject.GetComponent<Numbers>();
                    //ns.numberList.transform.position += new Vector3(0, Input.touches[0].position.y - pY, 0);
                     
                  
                    ns.numberList.GetComponent<Rigidbody2D>().AddForce(transform.up * (Input.touches[0].position.y - pY) * force );
                    pY = Input.touches[0].position.y;
                }
            }
        }
        else
        {
             if(theObject != null)
            {
                theObject.GetComponent<Numbers>().isBeingTouched = false;
                theObject = null;
            }
                
        }
    }
    public void ClickedButton(TextMeshProUGUI textUi)
    {
        textUi.text = number.ToString();
        number++;
    }
}
