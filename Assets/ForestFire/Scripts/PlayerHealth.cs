using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerHealth : MonoBehaviour
{

    public int MaxHealth=100, CurrentHealth; //Create health varaibles for the player, default values
    public AudioSource DamageSound, DeathSound; //Create reference to audiosource
    bool CanTakeDamage = true; //Variable to enable damage control


    //Function to reduce a players health by a given value, and run any effects that relate to that
    void TakeDamage(int damagevalue)
    {
        if (CurrentHealth!=0)
        {
            CurrentHealth = CurrentHealth - damagevalue; //Reduce player health
            DamageSound.Play(); //Play damage sound, alerting the player
        }
    }

    //Function to check if a players health is zero - player has therefore died (call each frame)
    void CheckDeath(int health)
    {
        if (health==0)
        {
            DeathSound.Play(); //Play death sound
            //Move player to the end grave scene
        }
    }

    //Function to check the distance of a player to a fire, and return true or false
    bool IsPlayerNearFire()
    {
        GameObject player = this.gameObject; //Find the player by XR RIG component
        Vector3 playerPosition = player.transform.position; //Get players current position

        Collider[] hitColliders = Physics.OverlapSphere(playerPosition, 4); //Create a physics sphere around the player, to check which objects are nearby

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "tree") //validate if tree is found in any state, before attempting to reach forest fire script
            {
                GameObject parentCell = hitCollider.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject; //Create reference to the ForestFireCell prefab this instance is childed to - so that I can access the current state of the tree (alight, alive etc..)
                ForestFireCell activeCell = parentCell.GetComponent<ForestFireCell>();
                if (activeCell.cellState == ForestFireCell.State.Alight) //Check status of the tree - look for fire
                {
                    return true;
                }
            }
        }
        return false;
    }


    //coroutine Function to deal damage to player if next to the a fire for longer than specified time
    private IEnumerator FireDamage()
    {
        CanTakeDamage = false; //Stop the update function from being called, whilst evaluating player status
        yield return new WaitForSeconds(2);
    
        if (IsPlayerNearFire() == true) //Check if still next to fire, if so take damage
        {
            TakeDamage(5);
        }
        CanTakeDamage = true; //Allow update function to continue checking each frame
    }



    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth; //Set current player health to full - start of game
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath(CurrentHealth);

        if ((CanTakeDamage == true) & (IsPlayerNearFire() == true))
        {
            StartCoroutine(FireDamage());
        }

    }
}
