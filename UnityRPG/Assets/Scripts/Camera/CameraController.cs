using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _tilt;

    // Update is called once per frame
    private void Update()
    {
        float mouseRotation = Input.GetAxis("Mouse Y");
        _tilt = Mathf.Clamp(_tilt - mouseRotation, -35f, 35f);
        transform.localRotation = Quaternion.Euler(_tilt,0f,0f);
    }
}
