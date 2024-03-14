using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public bool isInGame;

    [SerializeField] GameObject GameMenu;
    [SerializeField] GameObject[] miniGamePrefabs;
    [SerializeField] GameObject snoozeButton;

    [SerializeField] AudioSource alarmSound;

    public int difficulty;

    GameObject timerM;
    GameObject button;
    GameObject theTimer;

    TimeManager tM;

    private void Awake()
    {
        tM = FindObjectOfType<TimeManager>();
    }

    public void StartMiniGame(GameObject timerMenu, GameObject sliderMenu, GameObject timer)
    {
        if(button != null)
        button.SetActive(true);

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
    }
    public void PlayGameButton(GameObject Button)
    {
        button = Button;
        Button.SetActive(false);
        GameObject g = Instantiate(miniGamePrefabs[Random.Range(0, miniGamePrefabs.Length)], GameMenu.transform);
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
}
