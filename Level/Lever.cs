using UnityEngine;

public class Lever : Interactable
{
    private bool isActive = false;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject[] hallwayTorches;
    [SerializeField] private AudioSource sfxHandler;
    [SerializeField] private AudioClip openFX;
    

    protected override void Interact()
    {
        if (!isActive)
        {
            door.GetComponent<Animator>().SetBool("Activated", true);
            foreach (GameObject torch in hallwayTorches)
            {
                sfxHandler.PlayOneShot(openFX);
                torch.SetActive(true);
                isActive = true;
                GetComponent<CircleCollider2D>().enabled = false;
            }
        }
        
    }
}
