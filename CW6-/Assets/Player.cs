using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;
    public float rad;
    public float jumpForce;

    public bool isGrounded;

    public LayerMask ground;

    public Transform groundCheck;

   
    bool facingRight = true;

    SpriteRenderer sprite;

    Animator anim;

    string currentAnim;

    const string IDLE_ANIM = "IDLE ANIM";
    const string WALK_ANIM = "WALK ANIM";
    const string JUMP_ANIM = "JUMP ANIM";



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();
        PlayerJump();
        Flip();
      
    }

    void PlayerMove()
    {
        float xPos = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2(xPos, rb.velocity.y);

        if (isGrounded && xPos == 0 && rb.velocity.y == 0)
        {
            PlayAnim(IDLE_ANIM);
        }

        else if (isGrounded && xPos != 0 && rb.velocity.y == 0)

        {
            PlayAnim(WALK_ANIM);
        }


    }

    void PlayerJump()

    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, rad, ground);
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            PlayAnim(JUMP_ANIM);
        }
    }

    void Flip()
    {
        if (Input.GetKey(KeyCode.D) && !facingRight)
        {
            sprite.flipX = false;
            facingRight = true;

        }

        else if (Input.GetKey(KeyCode.A) && facingRight)

        {
            sprite.flipX = true;
            facingRight = false;
        }
    }
    void PlayAnim(string toPlay)
    {
        if (currentAnim == toPlay)
        {
            return;

        }
        currentAnim = toPlay;
        anim.Play(toPlay);
        
    }
}