using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction HitDetected;
    public static void OnHitDetected() => HitDetected?.Invoke();

    //public static event UnityAction<Vector3> CoinCollectedPosition;
    //public static void OnCoinCollected(Vector3 pos) => CoinCollectedPosition?.Invoke(pos);
}
