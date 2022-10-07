using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public int levelIndex;
    public int currentHealth;
    public float[] position;
    public bool hasAbilityDash;
    public bool hasAbilityDoubleJump;
    //TODO: Add more abiltiies here as they are implemented

    public PlayerData(PlayerAbiltiesHandler abilities, Health playerHealth, Transform playerTransform, int level) //TODO: Add components for currency
    {
        position = new float[3];
        position[0] = playerTransform.position.x;
        position[1] = playerTransform.position.y;
        position[2] = playerTransform.position.z;

        currentHealth = playerHealth.GetCurrentHealth();
        hasAbilityDash = abilities.hasDash;
        hasAbilityDoubleJump = abilities.hasDoubleJump;

        levelIndex = level;
    }
}
