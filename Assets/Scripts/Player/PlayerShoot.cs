using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private LayerMask _enemiesMask;

    Vector2 _screenMiddle;
    private void Start() 
    {
        _screenMiddle = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    private void Update() 
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Transform pointingTransform = GetFromRay();
        if (pointingTransform == null) return;
        
        if (pointingTransform.TryGetComponent(out Target target))
            target.Shooted();
    }

    private Transform GetFromRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(_screenMiddle);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _enemiesMask)) 
        {
            if (hit.transform != null)
            {
                if (hit.transform != null)
                    return hit.transform;
            }
        }

        return null;
    }
}
