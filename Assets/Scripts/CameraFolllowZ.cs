using UnityEngine;

public class CameraFollowZ : MonoBehaviour
{
    public float smoothSpeed = 5f;

    void FixedUpdate()
    {
        Debug.Log(Car.instance != null);
        if (Car.instance != null)
        {
            Vector3 currentPosition = transform.position;
            float targetZ = Car.instance.transform.position.z - 10f;

            // Smoothly interpolate towards the target Z
            float newZ = Mathf.Lerp(currentPosition.z, targetZ, smoothSpeed * Time.deltaTime);

            // Update only the Z position
            transform.position = new Vector3(currentPosition.x, currentPosition.y, newZ);
        }
    }
}