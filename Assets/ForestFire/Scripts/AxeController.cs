using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{

    public AudioSource TreeChopSound1;
    GameObject Axe;

    private void Start()
    {
        Axe = this.gameObject;
    }

    //Function to enable to gravity on the rigid body, but only once the first grab has occured - allows axe to stay stuck into tree at the start of the scene
    public void EnaleGravity()
    {
        Rigidbody axeRB = Axe.GetComponent<Rigidbody>();
        axeRB.useGravity = true;
    }

    //Function to disable gravity on the rigid body, useful if wanting the Axe to become stuck in a tree
    public void DisableGravity()
    {
        Rigidbody axeRB = Axe.GetComponent<Rigidbody>();
        axeRB.useGravity = false;
    }



    //Method to call on collision of Axe 
    private void OnCollisionEnter(Collision collision)
    {
        GameObject parentCell = collision.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject; //Create reference to the ForestFireCell prefab this instance is childed to - so that I can access the current state of the tree (alight, alive etc..)
        ForestFireCell activeCell = parentCell.GetComponent<ForestFireCell>();


        if ((collision.gameObject.tag == "tree") & (activeCell.cellState == ForestFireCell.State.Tree))
        {
            TreeChopSound1.Play(); //Play the sound provided

            Debug.Log("Collison with " + collision.gameObject.transform.parent.gameObject.name); //Debug the collision to determine whether the gameobect was colliding with the parent or child instance

            GameObject parentTree = collision.gameObject.transform.parent.gameObject; //Create a gameobject variable to store the parent of the gameobject it collided with - where treecontroller is located
            TreeController activeTree = parentTree.GetComponent<TreeController>(); //Find Tree controller component, to allow access to variables
            activeTree.treeHealth--; //Decrement tree health by 1

        }
    }
}
