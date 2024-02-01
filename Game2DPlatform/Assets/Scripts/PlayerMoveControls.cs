using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private GatherInput gatherInput;
    private Rigidbody2D rb;
    private Animator anim;

    private int direction = 1;
    public int additionalJumps = 2;
    private int resetJumpsNumber;

    private bool doubleJump = true;

    public float rayLength;

    public LayerMask groundLayer;
    public Transform leftPoint;
    public Transform rightPoint;

    private bool grounded = true;

    public bool knockBack = false;
    public bool hasControl = true;

    public bool onLadders;
    public float climbSpeed;
    public float climbHorizontalSpeed;

    private float startGravity;

    // Start is called before the first frame update
    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startGravity = rb.gravityScale;

        resetJumpsNumber = additionalJumps;
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorValeus();
    }

    private void FixedUpdate()
    {
        CheckStatus();
        if (knockBack || hasControl == false)
            return;
        Move();
        JumpPlayer();
    }

    private void Move()
    {
        Flip();
        rb.velocity = new Vector2(speed * gatherInput.valueX, rb.velocity.y);
        if (onLadders)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(climbHorizontalSpeed * gatherInput.valueX, climbSpeed * gatherInput.valueY);
            if (rb.velocity.y == 0)
                anim.enabled = false;
            else
                anim.enabled = true;
        }
    }

    public void ExitLadders()
    {
        rb.gravityScale = startGravity;
        onLadders = false;
        anim.enabled = true;
    }

    private void JumpPlayer()
    {
        if (gatherInput.jumpInput)
        {
            if(grounded || onLadders)
            {
                ExitLadders();
                rb.velocity = new Vector2(gatherInput.valueX * speed, jumpForce);
                doubleJump = true;
            }
            else if (additionalJumps > 0)
            {
                ExitLadders();
                rb.velocity = new Vector2(gatherInput.valueX * speed, jumpForce);
                doubleJump = false;
                additionalJumps -= 1;
            }
        }

        gatherInput.jumpInput = false;
    }

    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightCheckHit = Physics2D.Raycast(rightPoint.position, Vector2.down, rayLength, groundLayer);
        if (leftCheckHit || rightCheckHit)
        {
            grounded = true;
            doubleJump = false;
            additionalJumps = resetJumpsNumber;
        }
        else
        {
            grounded = false;
        }
        SeeRays(leftCheckHit, rightCheckHit);
    }

    private void SeeRays(RaycastHit2D leftCheckHit, RaycastHit2D rightCheckHit)
    {
        Color color1 = leftCheckHit ? Color.red : Color.green;
        Color color2 = rightCheckHit ? Color.red : Color.green;

        Debug.DrawRay(leftPoint.position, Vector2.down * rayLength, color1);
        Debug.DrawRay(rightPoint.position, Vector2.down * rayLength, color2);
    }

    private void Flip()
    {
        if(gatherInput.valueX * direction < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }

    private void SetAnimatorValeus()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("vSpeed", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Climb", onLadders);
    }

    public IEnumerator KnockBack(float forceX, float forceY, float duration, Transform otherObject)
    {
        int knockBackDirection;
        if (transform.position.x < otherObject.position.x)
            knockBackDirection = -1;
        else
            knockBackDirection = 1;

        knockBack = true;
        rb.velocity = Vector2.zero;
        Vector2 theForce = new Vector2(knockBackDirection * forceX, forceY);
        rb.AddForce(theForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        knockBack = false;
        rb.velocity = Vector2.zero;
    }
}
