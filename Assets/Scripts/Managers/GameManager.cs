using UnityEngine;

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
    }
    
    private void OnDestroy()
    {
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
}
