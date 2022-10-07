using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Health : MonoBehaviour
{
    [HideInInspector] public int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int scoreValue;
    
    public bool isDead = false;

    private Animator myAnimator;
    private AudioSource myAudioSource;
    [SerializeField] private AudioClip[] hurtGrunts;
    [SerializeField] private AudioClip deathGrunt;

    private void Start()
    {
        StopAllCoroutines();
        currentHealth = maxHealth;
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
    }

    public void Heal(int healAmount) 
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth) 
        {
            currentHealth = maxHealth;
        }

    }

    public void TakeDamage(int damageToTake) //the name is self explanatory - damages the target and checks if the target's health is less or equal to zero
    {
        currentHealth -= damageToTake;
        AudioClip soundToPlay = hurtGrunts[Random.Range(0, hurtGrunts.Length)]; //returns a random int from the hurtGrunts array
        myAudioSource.PlayOneShot(soundToPlay);//plays the hurt sound;
        myAnimator.SetTrigger("Hurt");
        if (currentHealth <= 0) 
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die() 
    {
        if(this.gameObject.tag == "Enemy") //adds score if the object is enemy - handles enemy death
        {
            isDead = true; //sets isDead bool to true - used by enemy controller to know when to stop moving
            myAudioSource.PlayOneShot(deathGrunt); //plays the death sound
            myAnimator.SetTrigger("Die"); //triggers the death animation
            FindObjectOfType<GameManager>().UpdateScore(scoreValue);
            GetComponent<Rigidbody2D>().isKinematic = true; //disables the rigidbody's interaction with physics
            foreach (Collider2D collider in gameObject.GetComponents<Collider2D>()) //searches for all of the colliders on the game object and disables them
            {
                collider.enabled = false;
            }
            Destroy(this.gameObject, 3f); //removes the game object from the hierarchy. TODO: Add more functinality for specifically the player.
        }
        else if(this.gameObject.tag == "Player") 
        {
            isDead = true; //sets isDead bool to true - used by enemy controller to know when to stop moving
            myAudioSource.PlayOneShot(deathGrunt); //plays the death sound
            myAnimator.SetBool("Die", isDead); //triggers the death animation
            GetComponent<Rigidbody2D>().isKinematic = true; //sets rigidbody to kinematic
            StartCoroutine(LoadCurrentScene()); //loads the current scene
        }
       
    }

    public int GetMaxHealth() //getter for maxHealth
    {
        return maxHealth;
    }

    public int GetCurrentHealth() //getter for currentHealth
    {
        return currentHealth;
    }

    private IEnumerator LoadCurrentScene() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(currentSceneIndex);
    }
}
