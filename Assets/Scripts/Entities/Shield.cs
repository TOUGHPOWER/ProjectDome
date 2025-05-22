using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Entity
{
    [Header("Class Variables")]
    [SerializeField] float startingMaxHP;
    // Start is called before the first frame update
    void Start()
    {
        SetupHealthValues(startingMaxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
