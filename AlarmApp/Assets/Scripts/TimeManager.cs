using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    MiniGameManager mGManager;

    [SerializeField] List<GameObject> Timers;
    [SerializeField] GameObject timerPrefab;
    [SerializeField] GameObject timerMenu;
    [SerializeField] GameObject sliderMenu;

    [SerializeField] float offset;

    [SerializeField] Numbers[] numbers;
    [SerializeField] string[] time;
    [SerializeField] string[] divided;
    string pMinute;

    [SerializeField] GameObject alreadyExistsText;
    float AETTime;

    int cancelHours;
    int cancelMinutes;
    bool openedViaTimerButton;
    private void Awake()
    {
        mGManager = FindObjectOfType<MiniGameManager>();
    }
    private void Update()
    {
        time = System.DateTime.Now.ToString().Split(' ');
        divided = time[1].Split(':');
        if (!mGManager.isInGame && pMinute != divided[1])
        {
            for (int i = 0; i < Timers.Count; i++)
            {
                string[] timersTime = Timers[i].GetComponentInChildren<TextMeshProUGUI>().text.Split(':');
                if (Timers[i].GetComponent<TimerButton>().snoozeText.activeSelf)
                    timersTime[1] = (int.Parse(timersTime[1]) + Timers[i].GetComponent<TimerButton>().snoozeTime).ToString();
               
                if(int.Parse(timersTime[1]) > 59)
                {
                    timersTime[1] = (int.Parse(timersTime[1]) - 59).ToString();
                    timersTime[0] = (int.Parse(timersTime[0]) + 1).ToString();
                }
                char[] d = timersTime[1].ToCharArray();
                if(d.Length == 1)
                {
                    timersTime[1] = "0" + timersTime[1];
                }

                Debug.Log(timersTime[1]);

                if (timersTime[0] == divided[0] && timersTime[1] == divided[1] && Timers[i].GetComponent<TimerButton>().isOn)
                {
                    mGManager.StartMiniGame(timerMenu, sliderMenu, Timers[i]);
                }
            }
            pMinute = divided[1];
        }
        if (alreadyExistsText.activeSelf && Time.time >= AETTime)
        {
            alreadyExistsText.SetActive(false);
        }
    }
    public void addTimer()
    {
        bool AlreadyExist = false;
        for (int i = 0; i < Timers.Count; i++)
        {
            if(numbers[0].number == Timers[i].GetComponent<TimerButton>().Hours && numbers[1].number == Timers[i].GetComponent<TimerButton>().Minutes)
            {
                AlreadyExist = true;
            }
        }
        if (!AlreadyExist)
        {
            AddTheTimer(new int[] { numbers[0].number, numbers[1].number});
            openedViaTimerButton = false;
        }
        else
        {
            alreadyExistsText.SetActive(true);
            AETTime = Time.time + 1.5f;
        }
    }
    void AddTheTimer(int[] theNumbers)
    {
        Debug.Log(theNumbers[0] + " , " + theNumbers[1]);
        GameObject g = Instantiate(timerPrefab, timerMenu.transform);
        bool haveInserted = false;
        for (int i = 0; i < Timers.Count; i++)
        {
            if (theNumbers[0] < Timers[i].GetComponent<TimerButton>().Hours || (theNumbers[0] == Timers[i].GetComponent<TimerButton>().Hours && theNumbers[1] < Timers[i].GetComponent<TimerButton>().Minutes))
            {
                Timers.Insert(i, g);
                haveInserted = true;
                break;
            }
        }
        if (!haveInserted)
        {
            Timers.Add(g);
        }
        g.GetComponent<TimerButton>().Hours = theNumbers[0];
        g.GetComponent<TimerButton>().Minutes = theNumbers[1];

        string[] extras = new string[theNumbers.Length];
        for (int i = 0; i < theNumbers.Length; i++)
        {
            if (theNumbers[i] < 10)
            {
                extras[i] = "0";
            }
        }
        string timeText = extras[0] + theNumbers[0].ToString() + ":" + extras[1] + theNumbers[1].ToString();

        g.GetComponentInChildren<TextMeshProUGUI>().text = timeText;

        sliderMenu.SetActive(false);
        ActivateTimerMenu();

        ResetYPositions();
        // g.transform.position += new Vector3(0, -offset * (Timers.Count -1), 0);
    }
    void ResetYPositions()
    {
        
        for (int i = 0; i < Timers.Count; i++)
        {
            Timers[i].transform.position = timerMenu.transform.position + new Vector3(0, -offset * i, 0);
        }
    }
    public void removeTimer(GameObject g, int pH, int pM)
    {
        openedViaTimerButton = true;

        cancelHours = pH;
        cancelMinutes = pM;

        numbers[0].numberList.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        numbers[1].numberList.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        numbers[0].numberList.transform.position = new Vector3(numbers[0].numberList.transform.position.x, numbers[0].startY + (numbers[0].offset * g.GetComponent<TimerButton>().Hours), numbers[0].numberList.transform.position.z);
        numbers[1].numberList.transform.position = new Vector3(numbers[1].numberList.transform.position.x, numbers[1].startY + (numbers[1].offset * g.GetComponent<TimerButton>().Minutes), numbers[1].numberList.transform.position.z);
        
        Timers.Remove(g);
        Destroy(g);



        sliderMenu.SetActive(true);
        timerMenu.SetActive(false);
    }
    public void OpenSliderMenu()
    {
        timerMenu.SetActive(false);
        sliderMenu.SetActive(true);
    }
    public void ActivateTimerMenu()
    {
        timerMenu.SetActive(true);
        for (int i = 0; i < Timers.Count; i++)
        {
            Timers[i].GetComponent<TimerButton>().ControllToggle();
        }
    }
    public void CancelButton()
    {
        if (openedViaTimerButton)
        {
            AddTheTimer(new int[] { cancelHours, cancelMinutes });
            openedViaTimerButton = false;
        }
        else
        {
            sliderMenu.SetActive(false);
            ActivateTimerMenu();
        }
    }
}
