using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float moveHorizontal;
    private Animator anim;
    private Rigidbody2D rb;
    
    public bool facingRight;
    public bool isGrounded;
    public bool canDoubleJump;
    PlayerCombat playerCombat;

    public bool characterAttack;
    public float characterTimer;
    

    void Start()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
        characterTimer = 0.7f;

    }

   
    void Update()
    {
        CharacterMove();
        CharacterAnimation();
        CharacterAttack();
        CharacterRunAttack();
        CharacterJump();
        CharacterAttackSpacing();
    }

    void CharacterMove()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (moveHorizontal * moveSpeed, rb.velocity.y);
    }

    void CharacterAnimation()
    {
        if (moveHorizontal > 0)
        {
            anim.SetBool("isRunning", true);
        }
        if (moveHorizontal == 0) 
        {
            anim.SetBool("isRunning", false);
        }
        if (moveHorizontal < 0)
        {
            anim.SetBool("isRunning", true);
        }
        if(facingRight == false && moveHorizontal > 0)
        {
            CharacterFlip();
        }
        if (facingRight == true && moveHorizontal < 0)
        {
            CharacterFlip();
        }
    }

    void CharacterFlip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void CharacterAttack()
    {
        if (Input.GetKeyDown(KeyCode.E) && moveHorizontal == 0)
        {
            

            if (characterAttack) 
            {
                anim.SetTrigger("isAttack");
                playerCombat.DamageEnemy();
                characterAttack = false;
            }

            
            FindObjectOfType<AudioManager>().Play("swordsound1");
        }
    }
    void CharacterRunAttack()
    {
        if (Input.GetKeyDown(KeyCode.E) && moveHorizontal > 0 || Input.GetKeyDown(KeyCode.E)  && moveHorizontal < 0)
        {
            

            if (characterAttack)
            {
                anim.SetTrigger("isRunAttack");
                playerCombat.DamageEnemy();
                characterAttack = false;
            }

            
            FindObjectOfType<AudioManager>().Play("swordsound1");
        }
    }

    void CharacterJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);

            if (isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            canDoubleJump = true;
        }
            else if (canDoubleJump)
            {
                jumpForce = jumpForce / 1.5f;
                rb.velocity = Vector2.up * jumpForce;

                canDoubleJump = false;
                jumpForce = jumpForce * 1.5f;
            }
        }
        
    }

    void CharacterAttackSpacing()
    {
        if (characterAttack == false)
        {
            characterTimer -= Time.deltaTime;
        }
        if (characterTimer < 0)
        {
            characterTimer = 0f;
        }
        if (characterTimer == 0f)
        {
            characterAttack = true;
            characterTimer = 0.7f;
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetBool("isJumping", false);

        if (collision.gameObject.tag == "Grounded")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        anim.SetBool("isJumping", false) ;

        if (collision.gameObject.tag == "Grounded")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetBool("isJumping", true) ;

        if(collision.gameObject.tag == "Grounded")
        {
            isGrounded = false;
        }
    }
}
