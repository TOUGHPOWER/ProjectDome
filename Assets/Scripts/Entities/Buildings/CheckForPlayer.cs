using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour
{
    private Building parentBuilding;
    // Start is called before the first frame update
    void Start()
    {
       parentBuilding = gameObject.GetComponentInParent<Building>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parentBuilding.PlayerInBuilding = true;
            print("Hi Player");
        }

        if (collision.CompareTag("Shield"))
        {
            parentBuilding.isShielded = true;
            print("Hi building");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parentBuilding.PlayerInBuilding = false;
            parentBuilding.playerInfo.EnableDepositing(true);
        }
    }
}
