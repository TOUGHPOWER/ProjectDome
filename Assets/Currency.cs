using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] Currencies currencyType;
    [SerializeField] Sprite currencyObjectSprite;
    [SerializeField] int value;

    // Start is called before the first frame update
    void Start()
    {
       playerInfo = FindAnyObjectByType<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddCurrency(int amount) 
    {
        if (currencyType == Currencies.Energy) 
        {
            playerInfo.currentNumEnergy += amount;
        }
        else if (currencyType == Currencies.Crystal)
        {
            playerInfo.currentNumCrystals += amount;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AddCurrency(value);
            Destroy(gameObject);
        }
    }
}
