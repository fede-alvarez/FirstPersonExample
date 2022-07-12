using UnityEngine;

public class Cube : MonoBehaviour, IDamagable
{
    private int shoots = 0;

    public void Damage(Vector3 direction)
    {
        shoots++;

        if (shoots >= 3)
            gameObject.SetActive(false);
    }
}
