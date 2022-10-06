using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _portalCamera;
    [SerializeField] private Material _portalMaterial;
    private static GameManager instance;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }else{
            instance = this;
        }
    }

    private void Start() 
    {
        if (_portalCamera.targetTexture != null)
            _portalCamera.targetTexture.Release();
        
        _portalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        _portalMaterial.mainTexture = _portalCamera.targetTexture;

        EventManager.GameLoaded += OnGameLoaded;
        EventManager.OnLoadGame();
    }

    private void OnGameLoaded(SaveData data)
    {
        print("Game Loaded");
        print(data.ToString());
        _player.GetComponent<PlayerShoot>().Ammo = data.Ammo;
    }

    public void SaveGame()
    {
        print("Game Saved!");
        Vector3 pos = Vector3.zero;
        List<Vector3> list = new List<Vector3>() {pos, pos, pos};
        
        EventManager.OnSaveGame(new SaveData(pos, list, 14));
    }
    
    private void OnDestroy()
    {
        EventManager.GameLoaded -= OnGameLoaded;

        if (instance != null)
            instance = null;
    }
    
    public static GameManager GetInstance
    {
        get { return instance;}
    }

    public Transform GetPlayer {
        get { return _player; }
    }

    public PlayerShoot GetPlayerShoot {
        get { return _player.GetComponent<PlayerShoot>(); }
    }
}
