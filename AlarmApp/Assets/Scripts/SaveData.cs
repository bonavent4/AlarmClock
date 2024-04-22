using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{

    public SaveObject saveObject = new SaveObject { };
    private void Awake()
    {
        //Debug.Log(Application.persistentDataPath + "/Saves/SaveData.txt");

       /* if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");*/

        if (!File.Exists(Application.persistentDataPath + "/SaveData.txt"))
            File.WriteAllText(Application.persistentDataPath + "/SaveData.txt", JsonUtility.ToJson(saveObject));
        else
            saveObject = JsonUtility.FromJson<SaveObject>(File.ReadAllText(Application.persistentDataPath + "/SaveData.txt"));

        for (int i = 0; i < saveObject.timers.Count; i++)
        {
            string[] numbersText = saveObject.timers[i].Split(':');
            int[] numbers = new int[numbersText.Length];
            for (int k = 0; k < numbersText.Length; k++)
            {
                numbers[k] = int.Parse(numbersText[k]);
            }

            gameObject.GetComponent<TimeManager>().AddTheTimer(numbers, saveObject.isOn[i], true);
        }
        
    }

    public class SaveObject
    {
        public List<string> timers = new List<string>();
        public List<bool> isOn = new List<bool>();
        //public List<int[]> settings = new List<int[]>();
        public List<int> difficulty = new List<int>();
        public List<int> sound = new List<int>();
        public List<int> snoozeAmount = new List<int>();
    }
    public void saveTheData()
    {
        
        File.WriteAllText(Application.persistentDataPath + "/SaveData.txt", JsonUtility.ToJson(saveObject));
        Debug.Log("savedData");
    }

    public void RemoveTimer(string timer)
    {
        int index = 0;
        for (int i = 0; i < saveObject.timers.Count; i++)
        {
            if(saveObject.timers[i] == timer)
            {
                index = i;
                break;
            }
        }
        saveObject.timers.Remove(saveObject.timers[index]);
        saveObject.isOn.Remove(saveObject.isOn[index]);
        //saveObject.settings.Remove(saveObject.settings[index]);
        saveObject.difficulty.Remove(saveObject.difficulty[index]);
        saveObject.sound.Remove(saveObject.sound[index]);
        saveObject.snoozeAmount.Remove(saveObject.snoozeAmount[index]);
        saveTheData();
    }
    public void AddTimer(int index, string timer, bool on, int[] settingsData)
    {
            saveObject.timers.Insert(index, timer);
            saveObject.isOn.Insert(index, on);
        saveObject.difficulty.Insert(index, settingsData[0]);
        saveObject.sound.Insert(index, settingsData[1]);
        saveObject.snoozeAmount.Insert(index, settingsData[2]);
    }
}
