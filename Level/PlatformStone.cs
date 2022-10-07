using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStone : Interactable
{
    [SerializeField] GameObject platform;
    protected override void Interact()
    {
        platform.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
    }
}
