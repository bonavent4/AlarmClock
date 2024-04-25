using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    MiniGameManager mGManager;

    public List<GameObject> Timers;
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
    bool timerIsOn;

    //top timer
    [SerializeField] TextMeshProUGUI topTimerText;

    [SerializeField] GameObject deleteButton;

    SaveData saveData;
    SettingsManager settingsM;

    GameObject currentTimer;
    private void Awake()
    {
        saveData = gameObject.GetComponent<SaveData>();
        mGManager = FindObjectOfType<MiniGameManager>();
        settingsM = gameObject.GetComponent<SettingsManager>();
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
                    timersTime[1] = (int.Parse(timersTime[1]) - 60).ToString();
                    timersTime[0] = (int.Parse(timersTime[0]) + 1).ToString();
                }
                char[] d = timersTime[1].ToCharArray();
                if(d.Length == 1)
                {
                    timersTime[1] = "0" + timersTime[1];
                }

                //Debug.Log(timersTime[1]);

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

        TopTimer();
    }
    void TopTimer()
    {
        if(topTimerText.text != divided[0] + ":" + divided[1])
        topTimerText.text = divided[0] + ":" + divided[1];
    }
    public void addTimer()
    {
        bool AlreadyExist = false;
        if(currentTimer != null)
        {
            saveData.RemoveTimer(currentTimer.GetComponent<TimerButton>().Hours + ":" + currentTimer.GetComponent<TimerButton>().Minutes);
            Timers.Remove(currentTimer);
            Destroy(currentTimer);
            AlreadyExist = false;
            Debug.Log("Aleady ...");
        }
        else
        {
            for (int i = 0; i < Timers.Count; i++)
            {
                if (numbers[0].number == Timers[i].GetComponent<TimerButton>().Hours && numbers[1].number == Timers[i].GetComponent<TimerButton>().Minutes)
                {

                    AlreadyExist = true;
                    /*if (i == Timers.IndexOf(currentTimer))
                    {
                        
                    }*/

                }
            }
        }
        
        if (!AlreadyExist)
        {
            AddTheTimer(new int[] { numbers[0].number, numbers[1].number}, true, false);
            openedViaTimerButton = false;
        }
        else
        {
            alreadyExistsText.SetActive(true);
            AETTime = Time.time + 1.5f;
        }
    }
    public void AddTheTimer(int[] theNumbers, bool on, bool isAlreadySavedData)
    {
       // Debug.Log(theNumbers[0] + " , " + theNumbers[1]);
        GameObject g = Instantiate(timerPrefab, timerMenu.transform);

        g.GetComponent<TimerButton>().isOn = on;
        g.GetComponent<TimerButton>().ControllToggle();

        bool haveInserted = false;
        for (int i = 0; i < Timers.Count; i++)
        {
            if (theNumbers[0] < Timers[i].GetComponent<TimerButton>().Hours || (theNumbers[0] == Timers[i].
                GetComponent<TimerButton>().Hours && theNumbers[1] < Timers[i].GetComponent<TimerButton>().Minutes))
            {
                Timers.Insert(i, g);
                if (!isAlreadySavedData)
                    saveData.AddTimer(i, theNumbers[0] + ":" + theNumbers[1], on, settingsM.settingsIndex);

                haveInserted = true;
                break;
            }
        }
        if (!haveInserted)
        {
            Timers.Add(g);
            if(!isAlreadySavedData)
                 saveData.AddTimer(Timers.IndexOf(g), theNumbers[0] + ":" + theNumbers[1], on, settingsM.settingsIndex);
        }
        if (!isAlreadySavedData)
            saveData.saveTheData();

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
    public void removeTimer(GameObject g, int pH, int pM, bool on)
    {
        currentTimer = g;

        deleteButton.SetActive(true);
        sliderMenu.SetActive(true);
        timerMenu.SetActive(false);

        


        timerIsOn = on;
        openedViaTimerButton = true;

        cancelHours = pH;
        cancelMinutes = pM;

        numbers[0].numberList.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        numbers[1].numberList.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        numbers[0].numberList.transform.position = new Vector3(numbers[0].numberList.transform.position.x, numbers[0].startY + (numbers[0].offset * g.GetComponent<TimerButton>().Hours), numbers[0].numberList.transform.position.z);
        numbers[1].numberList.transform.position = new Vector3(numbers[1].numberList.transform.position.x, numbers[1].startY + (numbers[1].offset * g.GetComponent<TimerButton>().Minutes), numbers[1].numberList.transform.position.z);

        //saveData.RemoveTimer(g.GetComponent<TimerButton>().Hours + ":" + g.GetComponent<TimerButton>().Minutes);
        int[] sI = { saveData.saveObject.difficulty[Timers.IndexOf(g)], saveData.saveObject.sound[Timers.IndexOf(g)] , saveData.saveObject.snoozeAmount[Timers.IndexOf(g)] };
        settingsM.settingsIndexes(sI);
        settingsM.SetSettingsText();

        foreach (Numbers number in numbers)
        {
            number.numberList.transform.position += new Vector3(0, .2f, 0);
        }


        
    }
    public void OpenSliderMenu()
    {
        deleteButton.SetActive(false);
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
       /* if (openedViaTimerButton)
        {
            //saveData.RemoveTimer(currentTimer.GetComponent<TimerButton>().Hours + ":" + currentTimer.GetComponent<TimerButton>().Minutes);

            //Timers.Remove(currentTimer);
            //Destroy(currentTimer);

            //AddTheTimer(new int[] { cancelHours, cancelMinutes }, timerIsOn, false);
            openedViaTimerButton = false;

        }
        else
        {*/
            sliderMenu.SetActive(false);
            ActivateTimerMenu();
       // }
    }
    public void DeleteButton()
    {
        saveData.RemoveTimer(currentTimer.GetComponent<TimerButton>().Hours + ":" + currentTimer.GetComponent<TimerButton>().Minutes);
        Timers.Remove(currentTimer);
        Destroy(currentTimer);
        sliderMenu.SetActive(false);
        ActivateTimerMenu();

        ResetYPositions();

    }
}
