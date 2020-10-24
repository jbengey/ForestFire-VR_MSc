using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    //Variables creation
    GameObject myTree;
    public int treeHealth=3;
    private bool fallen = false;
    public AudioSource TreeFallenAudio;


    // Start is called before the first frame update
    void Start()
    {
        myTree = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (treeHealth <=0 & fallen==false)
        {
            TreeFallenAudio.Play();
            Rigidbody treeRB = myTree.AddComponent<Rigidbody>();
            treeRB.isKinematic = false;
            treeRB.useGravity = true;

            StartCoroutine(removeTree(treeRB));
            fallen = true;

        }
    }

    //Create corutine
    private IEnumerator removeTree(Rigidbody treeRB)
    {
        yield return new WaitForSeconds(2);
        treeRB.AddForce(Vector3.forward, ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        Destroy(myTree);
    }




}
