using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float baseSpeed;
    [SerializeField] float speedMultiplier;
    [SerializeField] Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        //Assign Player Rigidbody
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer() 
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        float speed = baseSpeed * speedMultiplier;
        Vector2 moveDirection = new Vector2((horizontalInput * baseSpeed) * Time.deltaTime, (verticalInput * baseSpeed) * Time.deltaTime);

        playerRb.velocity = moveDirection;
    }


}
