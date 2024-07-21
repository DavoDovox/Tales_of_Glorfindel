using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    
    public Enums enums;
    byte[] stats = {0, 0, 0, 0, 0};
    public float health;
    public Rigidbody2D rb;
    Vector2 velocity;
    public const float speedMultiplier = 4.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + velocity * speedMultiplier * Time.fixedDeltaTime);
    }
}