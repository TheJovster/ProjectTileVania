using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public int score = 0;

    public int GetCurrentScore() 
    {
        return score;
    }

    public void UpdateScore(int scoreToAdd) 
    {
        score += scoreToAdd;
    } 

    public void SavePlayer() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SaveSystem.SavePlayer(
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbiltiesHandler>(),
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(),
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(),
            currentSceneIndex
            ); 
        //TOOD Consider implementing interfaces
        //save the current scene index.
        

        Debug.Log("Game Saved");
    }

    public void LoadPlayer() 
    {

        PlayerData data = SaveSystem.LoadPlayer(
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbiltiesHandler>(),
        GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(),
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>()); //TOOD Consider implementing interfaces

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbiltiesHandler>().hasDoubleJump = data.hasAbilityDoubleJump;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbiltiesHandler>().hasDash = data.hasAbilityDash;
            

        GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().currentHealth = data.currentHealth;

        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(data.position[0], data.position[1], data.position[2]);

/*        if (SceneManager.GetActiveScene().buildIndex != data.levelIndex) //possible issue here
        {
            SceneManager.LoadSceneAsync(data.levelIndex);

            SaveSystem.LoadPlayer(
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbiltiesHandler>(),
                GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(),
                GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>()); //TODO: Interfaces are VERY necessary here. This is completely nad utterly unamanagable.

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbiltiesHandler>().hasDoubleJump = data.hasAbilityDoubleJump;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbiltiesHandler>().hasDash = data.hasAbilityDash;


                GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().currentHealth = data.currentHealth;

                GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }*/
//The above code does not do what I want it to do - I might need to delete it and start from scratch.

        Debug.Log("Game Loaded");
    }
}
