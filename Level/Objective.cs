using UnityEngine.InputSystem;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private bool interactedWith;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && interactedWith) 
        {
            Debug.Log("You have managed to escape the Grand Inquisitor's Dungeon Cathedral!");
        }
    }


}
