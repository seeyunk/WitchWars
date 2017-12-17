using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    // Use this for initialization
    private GameObject _player;
    private Vector3 _offset;

    private void spawn(string resourceName)
    {
        _player = Instantiate<GameObject>(Resources.Load<GameObject>(resourceName));
    }

    void Start () {
        this.spawn("Characters/BlueIdol");
        _offset = transform.position - _player.transform.position;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        if (_player != null)
        {
            transform.position = _player.transform.position + _offset;
            //transform.rotation = _player.transform.rotation;
        }
    }
}
