using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float rayDistance = 1.0f; // Distance below the car to check for ground

    void Update()
    {
        if (Car.instance == null) return;

        RaycastHit hit;
        Vector3 origin = Car.instance.transform.position;
        Vector3 direction = Vector3.down;

        // Raycast downward from the car
        if (Physics.Raycast(origin, direction, out hit, rayDistance))
        {
            // Check if the object hit is tagged as "Ground"
            if (hit.collider.CompareTag("Ground"))
            {
                // Ground detected – do nothing
                return;
            }
        }

        // No valid "Ground" object detected under the car – call Die
        Car.instance.Die();
        UIManager.instance.Fail();
    }
}