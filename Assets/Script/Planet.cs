using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    // Use this for initialization
    private BoxCollider _collider;

	void Start () {
        _collider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    void OnTriggerExit(Collider other)
    {
        //Destroy(other.gameObject);
    }
}
