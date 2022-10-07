using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //components
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myBoxCollider;
    PlayerAbiltiesHandler abilitiesHandler;
    //basic movement variables
    Vector2 moveInput;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 3f;
    private float gravityScaleAtStart;
    //layers
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask climbingLayer;
    //Audio
    AudioSource myAudioSource;
    [SerializeField] AudioClip jumpEffect;
    [SerializeField] AudioClip[] jumpGrunts;
    [SerializeField] AudioClip[] footsteps;
    [SerializeField] AudioClip swoosh;

    //movement abilities
    public bool canDoubleJump = false;
    public bool canDash = false;
    private bool isDashing = false;

    //dash variables
    [SerializeField] private float dashTime = .2f;
    [SerializeField] private float dashCooldown = 3f;
    [SerializeField] private float timeSinceLastDash = 0f;
    [SerializeField] private float dashSpeed = 25f;

    //VFX
    [SerializeField] private GameObject dashVFX;
    [SerializeField] private GameObject jumpVFX;

    //dash after image FX variables
    [SerializeField] private SpriteRenderer mySpriteRenderer, afterImage;
    [SerializeField] private float afterImageLifetime, timeBetweenAfterImages;
    private float afterImageCounter;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        abilitiesHandler = GetComponent<PlayerAbiltiesHandler>(); //assigns the script that handles player abilities
        timeSinceLastDash = dashCooldown;
    }

    private void Update()
    {
        timeSinceLastDash += Time.deltaTime;
        if (timeSinceLastDash >= dashCooldown)
        {
            canDash = true;
        }

        if(isDashing) 
        {
            ShowAfterImage();
        }

        HandleAnimations();
        if (!GetComponent<Health>().isDead && !isDashing) //I made it impossible to change direction when dashing
        {
            Run();
            FlipSprite();
            ClimbLadder();

        }

    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnDash(InputValue value)
    {
        if (value.isPressed && canDash && abilitiesHandler.hasDash)
        {
            
            Dash();
        }
    }

    private void Dash()
    {
        Debug.Log("Dashed");
        isDashing = true;
        myAnimator.SetBool("Dash", isDashing);
        myAudioSource.PlayOneShot(swoosh); //TODO: Make an array that allows the game to dynamically switch between the sounds
        myRigidbody.velocity = new Vector2(dashSpeed * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), myRigidbody.velocity.y);
        afterImageCounter -= Time.deltaTime;
        if (afterImageCounter <= 0)
        {
            ShowAfterImage();
        }

        GameObject dashVFXInstance = Instantiate(dashVFX, new Vector2(transform.position.x, transform.position.y - .45f), transform.rotation);
        Destroy(dashVFXInstance, 1f);

        timeSinceLastDash = 0;
        canDash = false;
        StartCoroutine(WaitForTime(dashTime));
    }

    public void ShowAfterImage() 
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = mySpriteRenderer.sprite;
        image.transform.localScale = transform.localScale;

        Destroy(image.gameObject, afterImageLifetime);

        afterImageCounter = timeBetweenAfterImages;
    }


    private IEnumerator WaitForTime(float waitTime) 
    {
        yield return new WaitForSeconds(waitTime);
        isDashing = false;
        myAnimator.SetBool("Dash", isDashing);
    }

    private void OnJump(InputValue value) 
    {
        //if (!myBoxCollider.IsTouchingLayers(groundLayer)) return;

        if(value.isPressed && (myBoxCollider.IsTouchingLayers(groundLayer) || canDoubleJump)) //made changes to this script I need to revert it to default if it fails
        {
            if(myBoxCollider.IsTouchingLayers(groundLayer) && abilitiesHandler.hasDoubleJump) //so far so good - it's working. I can double jump
            {
                canDoubleJump = true; 
            }
            else 
            {
                canDoubleJump = false;
            }
            Jump();
        }
    }

    private void Jump() //the Jump method - extracted it for better readability
    {
        myAudioSource.PlayOneShot(jumpEffect);
        myAudioSource.PlayOneShot(jumpGrunts[Random.Range(0, jumpGrunts.Length)]);
        GameObject jumpVFXInstance = Instantiate(jumpVFX, new Vector2(transform.position.x, transform.position.y - .45f), transform.rotation);
        Destroy(jumpVFXInstance, .75f);
        myRigidbody.velocity += new Vector2(0f, jumpSpeed); //the jump functionality
        myAnimator.SetTrigger("Jumped");
        myAnimator.ResetTrigger("Jumped");

        //TODO: I need to integrate jump capability with the dash ability, specifically double jumping
    }

    private void Run() 
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myAnimator.SetBool("IsRunning", Mathf.Abs(playerVelocity.x) > Mathf.Epsilon);
        myRigidbody.velocity = playerVelocity;
    }

    private void FlipSprite() 
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed) 
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x) * 5f, transform.localScale.y);
        }
    }

    private void ClimbLadder() 
    {
        if (!myCapsuleCollider.IsTouchingLayers(climbingLayer))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("IsClimbing", false);
            return; 
        }    
            
        
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myAnimator.SetBool("IsClimbing", true);
        myAnimator.SetFloat("VerticalInput", Mathf.Abs(moveInput.y));
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;
        
        
    }

    private void HandleAnimations() 
    {
        myAnimator.SetBool("IsGrounded", CheckIsGrounded());
        myAnimator.SetBool("IsFalling", !CheckIsGrounded());
    }


    private void Footstep() 
    {
        AudioClip footStepSound = footsteps[Random.Range(0, footsteps.Length)];
        myAudioSource.PlayOneShot(footStepSound);
    }

    private void Hurt() //animation event
    {
        Debug.Log("Hurt animation event currently empty");
    } 

    public bool CheckIsGrounded() 
    {
        return myBoxCollider.IsTouchingLayers(groundLayer);
    }


    public void SetIsFalling() 
    {
        myAnimator.SetBool("IsFalling", true);
    }
}
