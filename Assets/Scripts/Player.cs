using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour{
    
    public Enums enums;
    public string PG;
    byte[] stats = {0, 0, 0, 0, 0};
    public float health;
    public Rigidbody2D rb;
    Vector2 velocity;
    public float speedMultiplier = 10.0f;
    private Animator animator;
    private string a_name;
    public static float[] player_pos= { 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PG = "W_";
        health = 100;
    }

    

    // Update is called once per frame
    void Update()
    {
        player_pos[0] = transform.position.x;
        player_pos[1] = transform.position.y;
        player_pos[2] = transform.position.z;
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PG == "W_")
                PG = "T_";
            else
                PG = "W_";
        }
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speedMultiplier = 15.0f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
                speedMultiplier = 10.0f;
        if (velocity.x == 0 && velocity.y == 0){
            if (a_name == PG+"walking_N")
                Animation_update(PG + "idle_N");
            else if (a_name == PG + "walking_S")
                Animation_update(PG + "idle_S");
            else if (a_name == PG + "walking_E")
                Animation_update(PG + "idle_E");
            else if (a_name == PG + "walking_W")
                Animation_update(PG + "idle_W");
        }
        else{
            if (velocity.x > 0)
                Animation_update(PG + "walking_E");
            else if (velocity.x < 0)
                Animation_update(PG + "walking_W");
            else if (velocity.y > 0)
                Animation_update(PG + "walking_N");
            else if (velocity.y < 0)
                Animation_update(PG + "walking_S");
        }
    }

    void Animation_update(string name)
    {
        if (name != a_name){
            a_name = name;
            animator.Play(a_name);
        }
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + velocity * speedMultiplier * Time.fixedDeltaTime);
    }

    
}