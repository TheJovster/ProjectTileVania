using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 20;
    [SerializeField] private GameObject healFX;
    [SerializeField] private AudioClip healAudio;    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Health>().GetCurrentHealth() < other.gameObject.GetComponent<Health>().GetMaxHealth())
            {
                other.GetComponent<Health>().Heal(healAmount);
                GameObject fxInstance = Instantiate(healFX, other.transform.position, other.transform.rotation);
                fxInstance.transform.parent = other.transform;
                SFXAudioHandler.instance.sfxPlayer.PlayOneShot(healAudio);
                StartCoroutine(WaitTime());
                Destroy(fxInstance, 1.2f);
                Destroy(this.gameObject);
            }
            else return;
        }
        else return;

    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2f);
    }
}
