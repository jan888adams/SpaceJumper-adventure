using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) // to kill instantly
    { // onExit -> to make damage over time
    if(collision.tag == "Player")
    {
     PlayerController controller = collision.GetComponent<PlayerController>();
     controller.Die();
    }

    }
}
