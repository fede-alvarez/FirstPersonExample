using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private string _fileName = "/gamesave.save";
    private SaveData _loadedData;

    private void Awake() 
    {
        EventManager.SaveGame += SaveGame;
        EventManager.LoadGame += LoadGame;
    }

    private void Start() 
    {
        CreateFile();    
    }

    public void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + _fileName, FileMode.Create);
        bf.Serialize(file, json);
        file.Close();
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + _fileName, FileMode.Open);
        string save = (string)bf.Deserialize(file);
        file.Close();

        _loadedData = JsonUtility.FromJson<SaveData>(save);
        EventManager.OnGameLoaded(_loadedData);
    }

    private void CreateFile()
    {
        if (!File.Exists(Application.persistentDataPath + _fileName))
        {
            FileStream file = File.Create(Application.persistentDataPath + _fileName);
            file.Close();
            return;
        }
    }

    private SaveData GetLoadedData()
    {
        return _loadedData;
    }

    private void OnDestroy() 
    {
        EventManager.SaveGame -= SaveGame;
        EventManager.LoadGame -= LoadGame;
    }
}
