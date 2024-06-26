using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public bool isInGame;

    [SerializeField] GameObject GameMenu;
    [SerializeField] GameObject SlothMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject[] miniGamePrefabs;

    GameObject miniGame;
    int minigameIndex;

    [SerializeField] GameObject snoozeButton;

    [SerializeField] AudioSource alarmSound;

    public int difficulty;

    GameObject timerM;

    GameObject outOfGameMenu;
    [SerializeField] GameObject inGameMenu;
    GameObject theTimer;

    TimeManager tM;
    SaveData saveD;

    [SerializeField]bool muteAlarmWhilePlaying;
    [SerializeField] GameObject soundOnIcon;
    [SerializeField] GameObject soundOffIcon;

    [SerializeField] GameObject timeSlider;
    [SerializeField] float timeSpeed;
    bool moveSlider;

    //sloth
    SlothManager slothM;
    private void Awake()
    {
        tM = FindObjectOfType<TimeManager>();
        saveD = gameObject.GetComponent<SaveData>();
        slothM = gameObject.GetComponent<SlothManager>();
    }
    private void Update()
    {
        if (moveSlider)
        {
            Debug.Log("Moving");
            timeSlider.transform.localPosition -= new Vector3(timeSpeed * Time.deltaTime, 0, 0);
            if(timeSlider.transform.localPosition.x <= -100)
            {
                GoBackToOutOfGameMenu();
            }
        }
    }
    public void StartMiniGame(GameObject timerMenu, GameObject sliderMenu, GameObject timer)
    {
        //set difficulty
        difficulty = saveD.saveObject.difficulty[tM.Timers.IndexOf(timer)];

        if(outOfGameMenu != null)
        outOfGameMenu.SetActive(true);

        if (timer.GetComponent<TimerButton>().snoozeText.activeSelf)
        {
            snoozeButton.SetActive(false);
            timer.GetComponent<TimerButton>().snoozeText.SetActive(false);
        }
        else
            snoozeButton.SetActive(true);


        timerM = timerMenu;
        theTimer = timer;
        isInGame = true;
        timerMenu.SetActive(false);
        sliderMenu.SetActive(false);
        SlothMenu.SetActive(false);
        settingsMenu.SetActive(false);
        GameMenu.SetActive(true);

        
            alarmSound.Play();


        minigameIndex = Random.Range(0, miniGamePrefabs.Length);


        //sloth
        slothM.PlaceSloth(1);
    }
    public void PlayGameButton(GameObject Button)
    {
        outOfGameMenu = Button;
        outOfGameMenu.SetActive(false);
        inGameMenu.SetActive(true);
        miniGame = Instantiate(miniGamePrefabs[minigameIndex], GameMenu.transform);
        if (muteAlarmWhilePlaying)
            alarmSound.Stop();

        startTimer();
    }
    public void DoneWithGame()
    {
        stopTimer();
        doneWithGame();

        slothM.AddOrTakePoints(5);
    }
    public void Snooze()
    {
        doneWithGame();

        theTimer.GetComponent<TimerButton>().ActivateSnoozeText();
    }
    void doneWithGame()
    {
        GameMenu.SetActive(false);
        tM.ActivateTimerMenu();

        alarmSound.Stop();

        isInGame = false;

        slothM.SlothInactive();
    }
    public void GoBackToOutOfGameMenu()
    {
        stopTimer();
        inGameMenu.SetActive(false);
        outOfGameMenu.SetActive(true);
        Destroy(miniGame);
        if (!alarmSound.isPlaying)
        {
            alarmSound.Play();
        }
    }
    public void MuteMusic()
    {
        if (muteAlarmWhilePlaying)
        {
            muteAlarmWhilePlaying = false;
            soundOffIcon.SetActive(false);
            soundOnIcon.SetActive(true);
            alarmSound.Play();
        }
        else
        {
            muteAlarmWhilePlaying = true;
            soundOffIcon.SetActive(true);
            soundOnIcon.SetActive(false);
            alarmSound.Stop();
        }
            
    }

    public void startTimer()
    {
        moveSlider = true;
        resetTimer();   
    }
    public void stopTimer()
    {
        moveSlider = false;
        resetTimer();
    }
    public void resetTimer()
    {
        timeSlider.transform.localPosition = new Vector2(0, timeSlider.transform.localPosition.y);
    }
}
