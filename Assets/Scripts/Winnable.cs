using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winnable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "sedan-sports")
        {
            Car.instance.Die();
            UIManager.instance.Win();
        }
    }
}
