using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = .5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask interactibleLayer;

    private Animator myAnimator;
    private AudioSource myAudioSource;
    private PlayerAbiltiesHandler abilitiesHandler;

    

    [SerializeField] private AudioClip[] swordSwings;
    [SerializeField] private AudioClip effortSound;


    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
        abilitiesHandler = GetComponent<PlayerAbiltiesHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnFire(InputValue value) 
    {
        if(value.isPressed && GetComponent<PlayerMovement>().CheckIsGrounded()) 
        {
            GroundAttack();
        }
        else if(value.isPressed && !GetComponent<PlayerMovement>().CheckIsGrounded() && abilitiesHandler.hasAirAttack) 
        {
            AirAttack();
        }
    }

    void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            //I need to implement an interactions interface
            //TODO: Create interactions interface
            //call interactions interface
            //when called do a Physics2D.OverlapCircleAll
            //make the interactible object do the thing it's supposed to do
        }
    }

    private void AirAttack()
    {
        print("Attacked in air");
        //TODO play an attack animation
        //detect enemies that are in range of attack
        //damage enemies
        //reset trigger
    }

    private void GroundAttack()
    {
        //play an attack animation
        myAnimator.SetTrigger("Attack");
        //reset trigger - optional
    }

    private void Hit() //animation event
    {
        //detect enemies that are in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name); 
            enemy.GetComponent<Health>().TakeDamage(damage);
        }
        myAnimator.ResetTrigger("Attack");
    }


    private void StartAttack() 
    {
        AudioClip attackClip = swordSwings[Random.Range(0, swordSwings.Length)];
        myAudioSource.PlayOneShot(attackClip);
        myAudioSource.PlayOneShot(effortSound);
    }

    // for testing purposes
    private void OnDamageSelf(InputValue value)
    {
        if (value.isPressed)
        {
            DamageHealth();
        }
    }
    private void DamageHealth() 
    {
        this.gameObject.GetComponent<Health>().TakeDamage(5);
    }



}
