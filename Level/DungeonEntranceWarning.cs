using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonEntranceWarning : MonoBehaviour
{
    [SerializeField] private AudioSource SFXPlayer;
    [SerializeField] private AudioClip warningClip;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D leftEye;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D rightEye;
    [SerializeField] private float waitTime = 21f;
    [SerializeField] private float lightFadeTime = 1f; //currently almost useless aside from making the Mathf.Lerp work therefore it's declared
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") 
        {
            SFXPlayer.PlayOneShot(warningClip);
            this.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine("EyeGlow");
        }
    }

    private IEnumerator EyeGlow() 
    {
        leftEye.intensity = Mathf.Lerp(leftEye.intensity, 7, lightFadeTime);
        rightEye.intensity = Mathf.Lerp(rightEye.intensity, 7, lightFadeTime);
        yield return new WaitForSeconds(waitTime);
        leftEye.intensity = Mathf.Lerp(leftEye.intensity, 0, lightFadeTime / Time.deltaTime);
        rightEye.intensity = Mathf.Lerp(rightEye.intensity, 0, lightFadeTime / Time.deltaTime);

    }

}
