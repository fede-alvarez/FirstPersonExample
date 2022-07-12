using UnityEngine;
using TMPro;

public class WeaponAmmoUI : MonoBehaviour
{
    private TMP_Text _ammoAmount;

    void Awake()
    {
        _ammoAmount = GetComponent<TMP_Text>();
    }

    private void Start() 
    {
        EventManager.PlayerShooted += OnPlayerShooted;
    }

    private void OnPlayerShooted(int bulletsAmount)
    {
        _ammoAmount.text = bulletsAmount.ToString();
    }

    private void OnDestroy() 
    {
        EventManager.PlayerShooted -= OnPlayerShooted;
    }
}
