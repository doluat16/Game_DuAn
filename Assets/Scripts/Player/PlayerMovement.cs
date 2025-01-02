using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public float dashPower;
    public bool isDash;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontalInput;

    private int dir = 1;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (!isDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // Flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            dir = 1;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1); //khi player quay ngược lại, thì mặt cũng phải quay ngc lại theo trục x
            dir = -1;
        }

        //Set animator parameters
        anim.SetFloat("velocityX", Mathf.Abs(horizontalInput));
        anim.SetBool("grounded", isGrounded());
        //Wall jump logic
        if (wallJumpCoolDown > 0.2f && !isDash)
        {

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
                Jump();

        }
        else
            wallJumpCoolDown += Time.deltaTime;

        if (body.velocity.x >= 0 && body.velocity.x <= speed) isDash = false;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            wallJumpCoolDown = 0;
        }
    }

    private void Dash()
    {
        isDash = true;
        body.AddForce(new Vector2(dashPower * dir, 0), ForceMode2D.Impulse);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public void UpSpeed(float _speed)
    {
        speed = _speed;
    }
}
