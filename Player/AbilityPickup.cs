using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    public bool enableDoubleJump;
    public bool enableDash;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && enableDoubleJump)
        {
            other.gameObject.GetComponent<PlayerAbiltiesHandler>().hasDoubleJump = enableDoubleJump;
            Destroy(this.gameObject);
            //TODO: Spawn FX
            //TODO: Spawn text which confirms what you have unlocked
        }
        else if (other.gameObject.tag == "Player" && enableDash) 
        {
            other.gameObject.GetComponent<PlayerAbiltiesHandler>().hasDash = enableDash;
            Destroy(this.gameObject);
        }
        
    }
}
