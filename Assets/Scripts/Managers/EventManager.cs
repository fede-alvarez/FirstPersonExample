using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction PlayerNoAim;
    public static event UnityAction PlayerAim;
    public static event UnityAction HitDetected;
    public static event UnityAction<int> PlayerShooted;
    public static event UnityAction LavaRised;

    public static event UnityAction GameSaved;
    public static event UnityAction GameLoaded;

    public static void OnPlayerAim() => PlayerAim?.Invoke();
    public static void OnPlayerNoAim() => PlayerNoAim?.Invoke();
    public static void OnHitDetected() => HitDetected?.Invoke();
    public static void OnPlayerShooted(int ammo) => PlayerShooted?.Invoke(ammo);
    public static void OnLavaRised() => LavaRised?.Invoke();

    public static void SaveGame() => GameSaved?.Invoke();
    public static void LoadGame() => GameLoaded?.Invoke();

    //public static event UnityAction<Vector3> CoinCollectedPosition;
    //public static void OnCoinCollected(Vector3 pos) => CoinCollectedPosition?.Invoke(pos);
}
