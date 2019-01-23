using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isOnGround;
    private bool isAtacking;
    private bool isAtackingCombo;
    public BoxCollider2D shortAtk;
    public BoxCollider2D mediumAtk;
    public BoxCollider2D longAtk;
    private float lastPosX;
    private int direction = 1;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        shortAtk.enabled = false;
        mediumAtk.enabled = false;
        longAtk.enabled = false;

        lastPosX = transform.position.x;

    }

    void FixedUpdate()
    {
        if (lastPosX > transform.position.x){
            if (direction == 1){
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                direction = -1;
            }
        }else if(lastPosX < transform.position.x){
            if (direction == -1){
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                direction = 1;
            }    
        }

        if (TCKInput.GetAction("left", EActionEvent.Press)){
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, -2.0f, 0.5f), rb.velocity.y);
        }
        if (TCKInput.GetAction("right", EActionEvent.Press)){
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 2.0f, 0.5f), rb.velocity.y);
        }
        if (isOnGround && TCKInput.GetAction("action", EActionEvent.Press)){
            rb.velocity = new Vector2(rb.velocity.x, 5f);
        }

        if (TCKInput.GetAction("action2", EActionEvent.Down)){
            if (!isAtackingCombo && isAtacking){
                isAtackingCombo = true;
            }else if (!isAtacking){
                isAtacking = true;
                shortAtk.enabled = true;
            }
        }

        lastPosX = transform.position.x;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "ground")
        {
            isOnGround = true;
            Debug.Log("chao");
        }
    }

    void OnCollisionExit2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "ground")
        {
            isOnGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        Debug.Log("tg");
        Debug.Log(col.gameObject.tag);
        if(col.gameObject.tag == "enemy"){
            if (isAtacking){
                col.gameObject.GetComponent<enemy>().OnHit(1, 20, 0, 10);
                isAtacking = false;
                shortAtk.enabled = false;
            }
            if (isAtackingCombo){
                col.gameObject.GetComponent<enemy>().OnHit(1, 0, 100, 20);
                isAtackingCombo = false;
                shortAtk.enabled = false;
            }
            Debug.Log("enemy");
        }
    }
}
