using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Slime : MonoBehaviour
{
    Vector2 velocity;
    public float speedMultiplier = 5.0f;
    public Rigidbody2D rb;
    private Animator animator;
    private bool knock_back;
    // Start is called before the first frame update
    float sqrt(float N)
    {
        float x = N / 2;
        float y = 0;
        while(x-y>0.01)
        {
            y = N / x;
            x = (x + y) / 2;
        }
        return x;
    }
    void Start()
    {
        knock_back = false;
        animator = GetComponent<Animator>();
        velocity.x = 0;
        velocity.y = 0;
        animator.Play("Slime_movement");
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.9f);
        while (true)
        {
            velocity.x = transform.position.x - Player.player_pos[0];
            velocity.y = transform.position.y - Player.player_pos[1];
            float v = -sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
            velocity.x /= v;
            velocity.y /= v;
            Vector2 vel = velocity*1.3f;
            vel.y += 2;
            for (int i = 0; i < 32; i++)
            { 
                rb.MovePosition(rb.position + vel * speedMultiplier * Time.fixedDeltaTime);
                vel.y -= 0.125f;
                yield return new WaitForSeconds(0.01f);
                if (knock_back)
                {
                    vel.x = -1.3f*vel.x;
                    vel.y -= 1.3f*velocity.y;
                    knock_back = false;
                }
            }
            yield return new WaitForSeconds(0.59f);

        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        knock_back = true;
        // Decrease player's health
        Player.health -= 2;

        if (Player.health > 0)
        {
            // Calculate direction from slime to player
            Vector2 direction = (Player.rb.position - (Vector2)transform.position).normalized;

            // Push the player away from the slime
            float pushStrength = 5f;
            Vector2 push = direction * pushStrength;

            // Apply force to the player
            Player.rb.AddForce(push, ForceMode2D.Impulse);
        }
    }


    // Update is called once per frame
    void Update()
    {
    }

    
}
