using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private bool isActive = false;
    void Start()
    {
        Destroy(gameObject, 5f);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "sedan-sports" &&  !isActive)
        {
            isActive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.name == "sedan-sports" && isActive)
        {
            Car.instance.Boost(9f);
        }
    }
}
