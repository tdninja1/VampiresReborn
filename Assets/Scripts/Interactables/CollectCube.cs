using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCube : Interactable
{
    public GameObject cube;
    public GameObject particle;
    // Start is called before the first frame update
    protected override void Interact()
    {
        Debug.Log("CollectCube Test" + cube);
        Destroy(cube);
        Instantiate(particle, transform.position, Quaternion.identity);
        
      
        
    }


}
