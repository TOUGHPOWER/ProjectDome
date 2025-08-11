using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    private BaseEnemy enemyScript;

    [SerializeField] int damage;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<BaseEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            enemyScript.SubtractCurrentHP(collision.gameObject.GetComponent<CircleAttack>().damageValue);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInfo>().SubtractCurrentHP(damage);
            
            print($"Touched:{collision.gameObject.name}");

            enemyScript.Die();

        }
        else if (collision.gameObject.CompareTag("Reactor") || collision.gameObject.CompareTag("Building"))
        {
            Building buildingTouched = collision.gameObject.GetComponent<Building>();
            if (buildingTouched.IsBuilt)
            {
                buildingTouched.SubtractCurrentHP(damage);
                enemyScript.Die();
            }

        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            collision.gameObject.GetComponent<Shield>().SubtractCurrentHP(damage);
            print($"Touched:{collision.gameObject.name}");
            enemyScript.Die();
        }
        
    }
}
