using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SqGame : MonoBehaviour
{
    RaycastHit2D hit;

    [SerializeField]bool CanTouch = false;
    [SerializeField] GameObject[] Buttons;
    [SerializeField] float timeBetween;
    int index;
    [SerializeField] List<GameObject> newButtonList = new List<GameObject>();
    int pButton;
    int buttonsPressed;

    [SerializeField] TextMeshProUGUI RoundText;

    [SerializeField] int[] difficultyRounds;
    MiniGameManager mgManager;

    private void Awake()
    {
        mgManager = FindObjectOfType<MiniGameManager>();
    }
    private void Start()
    {
        StartRound();
    }
    private void Update()
    {
        if(CanTouch && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            hit = Physics2D.Raycast(pos, Camera.main.transform.forward);
            if (hit.collider != null && hit.collider.gameObject.GetComponent<SqButton>())
            {
                // StartRound();
                hit.collider.GetComponent<SqButton>().Pressed();
                if (hit.collider.gameObject == newButtonList[buttonsPressed])
                {

                    mgManager.resetTimer();
                    buttonsPressed++;
                    if (buttonsPressed == newButtonList.Count)
                        StartRound();
                }
                else
                {
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        Buttons[i].GetComponent<SqButton>().turnRed();
                    }
                    buttonsPressed = 0;
                    CanTouch = false;
                    index = 0;
                    mgManager.stopTimer();
                    for (int i = 0; i < newButtonList.Count; i++)
                    {
                        Invoke("ShowButton", timeBetween * (i + 1));
                    }
                }
            }
        }
    }
    void StartRound()
    {
        if(newButtonList.Count == difficultyRounds[mgManager.difficulty])
        {
            Debug.Log("Finished");
            mgManager.DoneWithGame();
            Destroy(gameObject);
        }
        else
        {
            buttonsPressed = 0;
            CanTouch = false;
            mgManager.stopTimer();
            AddButtonToNewList();
            RoundText.text = "Round " + newButtonList.Count + "/" + difficultyRounds[mgManager.difficulty];

            index = 0;
            for (int i = 0; i < newButtonList.Count; i++)
            {
                Invoke("ShowButton", timeBetween * (i + 1));
            }
        }
    }
    void AddButtonToNewList()
    {
        int random = Random.Range(0, Buttons.Length);
        if (newButtonList.Count == 0 || random != pButton)
        {
            newButtonList.Add(Buttons[random]);
            pButton = random;
        }
        else
        {
            AddButtonToNewList();
        }
    }
    void ShowButton()
    {
        newButtonList[index].GetComponent<SqButton>().turnWhite();
        //newButtonList[index].GetComponent<SqButton>().turnGray();
        index++;
        if(index == newButtonList.Count)
        {
            CanTouch = true;
            mgManager.startTimer();
        }
    }
}
