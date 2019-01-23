using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isOnGround;
    public float force = 50f;
    private float movementDirection = 1;
    public GameObject player;
    public float health = 100;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("player");
    }

    void Update(){
        if (health <= 0){
            health = 0;
            movementDirection = 0;
            rb.freezeRotation = false;
        }

        Debug.Log(player.transform.position.x);
        Debug.Log(transform.position.x);

        if (movementDirection != 0){
            if (player.transform.position.x > transform.position.x){
                movementDirection = -1;
            }else{
                movementDirection = 1;
            }
        }

        if (isOnGround){
            rb.AddForce(new Vector2(-5f * movementDirection, 0f));
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        Debug.Log(col.gameObject.tag);
    }

    public void OnHit(int direction, float hForce, float vForce, float hpCount)
    {
        rb.AddForce(new Vector2(force * direction * hForce, force * vForce));

        health -= hpCount;

        if (health <= 0){
            rb.AddTorque(force * Random.value * 2f * -movementDirection);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "ground")
        {
            isOnGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "ground")
        {
            isOnGround = false;
        }
    }
}
