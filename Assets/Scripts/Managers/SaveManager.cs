using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

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
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
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
        if (!File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            Debug.LogWarning("No game file saved!");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        SaveData save = (SaveData) bf.Deserialize(file);
        file.Close();

        print(save.ammo);
        print(save.GetPlayerPosition());

        // Load de actual data
        GameManager.GetInstance.GetPlayerShoot.Ammo = save.ammo;
        
        Debug.Log("Game Loaded");
    }
}
