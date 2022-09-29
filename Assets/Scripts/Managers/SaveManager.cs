using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string _fullFilePath;
    private string _fileName = "gamesave.save";

    private void Start() 
    {
        _fullFilePath = Application.persistentDataPath + "/" + _fileName;
    }

    private SaveData CreateSaveGameObject()
    {
        SaveData save = new SaveData();

        // Save the values in the class
        save.ammo = GameManager.GetInstance.GetPlayerShoot.Ammo;
        save.SavePlayerPosition(GameManager.GetInstance.GetPlayer.position);

        return save;
    }

    public void SaveGame()
    {
        SaveData save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(_fullFilePath);
        bf.Serialize(file, save);
        file.Close();

        // Reset States if needed

        Debug.Log("Game Saved");
    }

    public void SaveAsJSON()
    {
        SaveData save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
        
        //SaveData save = JsonUtility.FromJson<SaveData>(json);
    }

    public void LoadGame()
    { 
        if (!File.Exists(_fullFilePath))
        {
            Debug.LogWarning("No game file saved!");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(_fullFilePath, FileMode.Open);
        SaveData save = (SaveData) bf.Deserialize(file);
        file.Close();

        // Load de actual data
        GameManager.GetInstance.GetPlayerShoot.Ammo = save.ammo;
        print(save.GetPlayerPosition());
        //save.SavePlayerPosition(GameManager.GetInstance.GetPlayer.position);
        
        Debug.Log("Game Loaded");
    }
}
