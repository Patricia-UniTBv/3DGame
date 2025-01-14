using UnityEngine;

public class CameraFollow: MonoBehaviour
{
    public Transform target;

    Vector3 offset;

    private void Start()
    {
        offset = target.position - transform.position;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = target.position - offset;
    }
}
