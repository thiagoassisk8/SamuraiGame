using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    private float moveSpeedOrigin;

    public float speedMutiplier;
    public float speedIncreaseMilestone;
    private float speedIncreaseMilestoneOrigin;
    private float speedMilestoneCounts;
    private float speedMilestoneCountsOrigin;

    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;

    // Ground checking properties
    public bool isOnGround;
    public bool isAttacking;

    public LayerMask groundLayer;

    public Transform groundCheckPoint;
    public float groundCheckRadius;
    private bool stopJumpping;
    private bool canDoubleJumping;
    

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;

    public GameManager gameManager;

    public AudioSource jumpSound, deathSound, attackSound;

    // Use this for initialization
    void Start() {
        

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        speedMilestoneCounts = speedIncreaseMilestone;
        myAnimator.SetBool("Attack1", false);
        
        // hurtEnemyOnContact = GetComponent<HurtEnemyOnContact>();

        moveSpeedOrigin = moveSpeed;
        speedMilestoneCountsOrigin = speedMilestoneCounts;
        speedIncreaseMilestoneOrigin = speedIncreaseMilestone;
    }

    // Update is called once per frame
    void Update() {
        // Detect the ground
        isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        // Speed up if pass the speed milestone
        if (transform.position.x > speedMilestoneCounts) {
            speedMilestoneCounts += speedIncreaseMilestone;
            moveSpeed = moveSpeed * speedMutiplier;

            speedIncreaseMilestone += speedIncreaseMilestone * speedMutiplier;
        }

        // Moving right
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

                


        // Jump (Space and left key of mouse)
        if (isActiveAndEnabled &&
            (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))) {
            jumpSound.Play();
            if (isOnGround) {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stopJumpping = false;
            } else if (canDoubleJumping) {
                canDoubleJumping = false;
                jumpTimeCounter = jumpTime;
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stopJumpping = false;
            }
        }
        
        //função de ataque
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            attackSound.Play();
            // OnClick();
            isAttacking = true;
            // StartCoroutine("OnClickFire1Co");
            // GetComponent<AudioSource>().Play();

        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !stopJumpping) {
            if (jumpTimeCounter > 0) {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
            jumpTimeCounter = 0; // Lock user keeping jumping
            stopJumpping = true;
        }

        if (isOnGround) {
            jumpTimeCounter = jumpTime;
            canDoubleJumping = true;
            
            
            
        }

        // Setup animators
        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("IsOnGround", isOnGround);
        myAnimator.SetBool("isAttacking", isAttacking);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("KillBox")) {
            deathSound.Play();
            gameManager.RestartGame();
            moveSpeed = moveSpeedOrigin;
            speedMilestoneCounts = speedMilestoneCountsOrigin;
            speedIncreaseMilestone = speedIncreaseMilestoneOrigin;
        }
    }
}