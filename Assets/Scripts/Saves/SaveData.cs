using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;

    public int ammo = 0;

    public void SavePlayerPosition(Vector3 position)
    {
        playerPosX = position.x;
        playerPosY = position.y;
        playerPosZ = position.z;
    }

    public Vector3 GetPlayerPosition()
    {
        return new Vector3(playerPosX, playerPosY, playerPosZ);
    }
}