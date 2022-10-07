using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float searchRadius = .5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private TextMeshProUGUI interactablePrompt;
    [SerializeField] private Graphic interactablePromptImage;
    private Collider2D[] interactables;
    private bool canInteract;
    HUDManager hudManager;
    private void Start()
    {
        interactablePromptImage.enabled = false;
        interactablePrompt.alpha = 0;
        hudManager = FindObjectOfType<HUDManager>();
    }

    void Update()
    {
        interactables = Physics2D.OverlapCircleAll(transform.position, searchRadius, interactableLayer); //this returns an array
        if (interactables.Length > 0) //be careful with this loop, if there are multiple interactable objects stacked on each other, the code will cause unintended behaviors;
        {
            Interactable interactable = interactables[0].gameObject.GetComponent<Interactable>();
            hudManager.UpdateInteractableObjectPrompt(interactable.promptMessage);
            interactablePromptImage.enabled = true;
            interactablePrompt.alpha = 1;
            canInteract = true;
        }
        else
        {
            interactablePromptImage.enabled = false;
            interactablePrompt.alpha = 0;
            canInteract = false;
        }
    }

    private void OnInteract() 
    {
        if(canInteract) 
        {
            Interactable interactable = interactables[0].gameObject.GetComponent<Interactable>();
            if (interactable != null) 
            {
                
                interactable.BaseInteract();
                Debug.Log("Interacted with " + interactable.name);
            }
                
            else 
            {
                Debug.Log("No Interaction Available");
                return;
            } 
                
            Debug.Log("You can interact with something");
        }
        else 
        {
            Debug.Log("Nothing to interact with");
            return;
        }
    }
}
