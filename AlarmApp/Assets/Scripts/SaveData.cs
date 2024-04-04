using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{

    public SaveObject saveObject = new SaveObject { };
    private void Awake()
    {
        
        if (!Directory.Exists(Application.persistentDataPath + "/Saves/SaveData.txt"))
             File.WriteAllText(Application.persistentDataPath + "/Saves/SaveData.txt", JsonUtility.ToJson(saveObject));
        else
            saveObject = JsonUtility.FromJson<SaveObject>(File.ReadAllText(Application.persistentDataPath + "/Saves/SaveData.txt"));

        for (int i = 0; i < saveObject.timers.Count; i++)
        {
            string[] numbersText = saveObject.timers[i].Split(':');
            int[] numbers = new int[numbersText.Length];
            for (int k = 0; k < numbersText.Length; k++)
            {
                numbers[i] = int.Parse(numbersText[i]);
            }
            gameObject.GetComponent<TimeManager>().AddTheTimer(numbers, saveObject.isOn[i]);
        }
        
    }

    public class SaveObject
    {
        public List<string> timers = new List<string>();
        public List<bool> isOn = new List<bool>();
    }
    public void saveTheData()
    {
        File.WriteAllText(Application.persistentDataPath + "/Saves/SaveData.txt", JsonUtility.ToJson(saveObject));
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
    }
    public void AddTimer(int index, string timer, bool on)
    {
        saveObject.timers.Insert(index, timer);
        saveObject.isOn.Insert(index, on);
    }
}
