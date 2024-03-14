using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public bool isInGame;

    [SerializeField] GameObject GameMenu;
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

    [SerializeField]bool muteAlarmWhilePlaying;

    private void Awake()
    {
        tM = FindObjectOfType<TimeManager>();
    }

    public void StartMiniGame(GameObject timerMenu, GameObject sliderMenu, GameObject timer)
    {
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
        GameMenu.SetActive(true);

        
            alarmSound.Play();


        minigameIndex = Random.Range(0, miniGamePrefabs.Length);
    }
    public void PlayGameButton(GameObject Button)
    {
        outOfGameMenu = Button;
        outOfGameMenu.SetActive(false);
        inGameMenu.SetActive(true);
        miniGame = Instantiate(miniGamePrefabs[minigameIndex], GameMenu.transform);
        if (muteAlarmWhilePlaying)
            alarmSound.Stop();
    }
    public void DoneWithGame()
    {
        GameMenu.SetActive(false);
        tM.ActivateTimerMenu();

        alarmSound.Stop();

        isInGame = false;
    }
    public void Snooze()
    {
        GameMenu.SetActive(false);
        tM.ActivateTimerMenu();

        alarmSound.Stop();

        isInGame = false;


        theTimer.GetComponent<TimerButton>().ActivateSnoozeText();
    }
    public void GoBackToOutOfGameMenu()
    {
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
            alarmSound.Play();
        }
        else
        {
            muteAlarmWhilePlaying = true;
            alarmSound.Stop();
        }
            
    }
}
