using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform _camera;
    public float moverateX, moverateY;
    private float startpointX, startpointY;

    private void Start()
    {
        startpointX = transform.position.x;
        startpointY = transform.position.y;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(startpointX + _camera.position.x * moverateX, startpointY + _camera.position.y * moverateY, transform.position.z);
    }

}
