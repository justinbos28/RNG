using UnityEngine;

public class RotationRarity : MonoBehaviour
{
    public float movespeed = 25;

    void Update()
    {
        transform.Rotate(Vector3.forward * movespeed * Time.deltaTime);
    }
}
