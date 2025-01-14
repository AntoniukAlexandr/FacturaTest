using UnityEngine;

public class HealthBarRotation : MonoBehaviour
{
    public Camera Camera;

    private void Update()
    {
        if (Camera)
        {
            transform.LookAt(Camera.transform, Vector3.up);
        }        
    }
}

