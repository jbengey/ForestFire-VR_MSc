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
            Rigidbody treeRB = myTree.AddComponent<Rigidbody>();
            treeRB.isKinematic = false;
            treeRB.useGravity = true;
            treeRB.AddForce(Vector3.forward, ForceMode.Impulse);
            StartCoroutine(removeTree());
            fallen = true;

        }
    }

    //Create corutine
    private IEnumerator removeTree()
    {
        yield return new WaitForSeconds(8);
        Destroy(myTree);
    }




}
