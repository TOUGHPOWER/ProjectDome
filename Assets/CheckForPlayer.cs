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
            parentBuilding.isInBuilding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parentBuilding.isInBuilding = false;
        }
    }
}
