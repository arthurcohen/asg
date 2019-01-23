using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isOnGround;
    public float force = 50f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D col){
        Debug.Log(col.gameObject.tag);
    }

    public void OnHit(int direction, float hForce, float vForce)
    {
        rb.AddForce(new Vector2(force * direction * hForce, force * vForce));
    }
}
