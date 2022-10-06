using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public Vector3 PlayerPosition;
    public List<Vector3> Points;
    public int Ammo = 0;

    public SaveData(Vector3 pos, List<Vector3> points, int ammo)
    {
        PlayerPosition = pos;
        Points = points;
        Ammo = ammo;
    }

    public override string ToString()
    {
        return "Player Ammo: " + Ammo.ToString();
    }
}