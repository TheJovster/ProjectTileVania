using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //message displayed to the player when intersecting the interactable
    public string promptMessage;

    public void BaseInteract() 
    {
        Interact();
    }

    protected virtual void Interact() 
    {
        //template function - overriden by child/sub-classes
    }
}
