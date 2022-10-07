using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : Interactable
{
    [SerializeField] private int levelIndex;

    protected override void Interact()
    {
        SceneManager.LoadSceneAsync(levelIndex);
    }
}
