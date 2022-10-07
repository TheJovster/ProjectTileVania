using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaThreshold : MonoBehaviour
{
    [SerializeField] string areaName = "Tower of the Red One";
    HUDManager hudManager;
    // Start is called before the first frame update
    void Start()
    {
        hudManager = FindObjectOfType<HUDManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") 
        {
            hudManager.UpdateAreaText(areaName);
        }
    }
}
