using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Use this for initialization
    private Rigidbody _body;
    private string _owner;
    private float _speed = 0.0f;
    private Vector3 _forward = Vector3.zero;

	void Start () {
        Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
        for (int n = 0; n < renderers.Length; n++)
        {
            Material material = renderers[n].material;
            if (material != null)
            {
                material.SetColor("_OutlineColor", Color.green);
            }
        }
 
        Vector3 forward = transform.forward;
        forward.y = 0.0f;
        _body = GetComponent<Rigidbody>();
        _body.AddForce(  forward * ( _speed + Variables.BULLET_SPEED ), ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.right, 30.0f);
	}

    public void spawn( string owner, float speed)
    {
        _owner = owner;
        _speed = speed;
    }

}
