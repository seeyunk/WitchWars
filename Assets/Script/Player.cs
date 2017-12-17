using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayerActionHandler {

    private Rigidbody _body;
    private volatile float _speed = Variables.WITCH_SPEED;
    private volatile float _boostTime = 0.0f;

    private volatile Variables.FireState _fire = Variables.FireState.OFF;
    private volatile Variables.BoostState _boost = Variables.BoostState.OFF;
    private volatile Variables.MoveState _move = Variables.MoveState.NEUTRAL;

    private GameObject _bullet;
    private Vector3 _forward;
    private string _userId = "seeyunk";


    // Use this for initialization
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        transform.position = new Vector3(0.0f, Variables.OBJECT_HEIGHT, 0.0f);
        transform.rotation = Quaternion.identity;
        StartCoroutine(TurnRoutine());
        StartCoroutine(BoostRoutine());
        StartCoroutine(FireRoutine());
        this.loadBulletResource("Bullets/carrot");

        Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
        for (int n = 0; n < renderers.Length; n++) {
            Material material = renderers[n].material;
            if ( material != null )
            {
                material.SetColor("_OutlineColor", Color.green);
            }
        }
        
        //Debug.Log(GetComponent<Renderer>().name);
        
        //renderer.material.setColor("Outline Color", Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //input();
    }

    public float GetBoostTime()
    {
        return _boostTime;
    }

    public void loadBulletResource(string resourceName)
    {
        _bullet = Resources.Load<GameObject>(resourceName);
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            if ( _fire.Equals( Variables.FireState.ON ) )
            {
                GameObject bullet = Instantiate<GameObject>(_bullet, transform.position, transform.rotation );
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.spawn(_userId, _speed);
                Destroy(bullet, 1.0f);
                _fire = Variables.FireState.OFF;
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator TurnRoutine()
    {
        while (true)
        {
            Vector3 to = transform.rotation.eulerAngles;
            if ( _move.Equals( Variables.MoveState.LEFT ) ) {
                to.y -= 3.0f;
                to.z = 30.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler( to ), 2.0f );
            }
            else if ( _move.Equals( Variables.MoveState.RIGHT ) )
            {
                to.y += 3.0f;
                to.z = -30.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(to), 2.0f);
            }
            else
            {
                to.z = 0.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(to), 2.0f);
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator BoostRoutine()
    {
        while (true)
        {
            Vector3 to = transform.rotation.eulerAngles;
            if ( !_boost.Equals( Variables.BoostState.OVERHEAT) 
                && _boost.Equals(Variables.BoostState.ON) )
            {
                if (_boostTime >= Variables.BOOST_TIME_LIMIT)
                {
                    _boost = Variables.BoostState.OVERHEAT;
                }
                else
                {
                    _boostTime += Time.deltaTime;
                    Speed(_speed, Variables.BOOST_SPEED, Variables.WITCH_UP_VELOCITY);

                    to.x = 30.0f;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(to), 2.0f);

                }
            }
            else 
            {
                _boostTime -= Time.deltaTime;
                if ( _boostTime <= 0.0f )
                {
                    _boostTime = 0.0f;
                }

                Speed(_speed, Variables.WITCH_SPEED, Variables.WITCH_DOWN_VELOCITY);


                to.x = 0.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(to), 2.0f);
            }
         
            yield return new WaitForSeconds(Time.deltaTime);

        }
    }

    private void Speed(  float from, float to, float velocity )
    {
        if ( to > from )
        {
            _speed += velocity;
            if ( _speed >= to )
            {
                _speed = to;
            }
        }
        else
        {
            _speed -= velocity;
            if ( _speed <= to )
            {
                _speed = to;
            }
        }
    }

    private void Move()
    {
        Vector3 pos = transform.position;
        float rotation = transform.rotation.eulerAngles.y;
        float rad = rotation * Mathf.Deg2Rad;
        pos.x = pos.x + Mathf.Sin(rad) * _speed;
        pos.z = pos.z + Mathf.Cos(rad) * _speed;
        transform.position = pos;
    }

    public void TurnLeft()
    {
        _move = Variables.MoveState.LEFT;
    }

    public void TurnRight()
    {
        _move = Variables.MoveState.RIGHT;
    }

    public void TurnNeutral()
    {
        _move = Variables.MoveState.NEUTRAL;
    }

    public void FireOn()
    {
        _fire = Variables.FireState.ON;
    }

    public void FireOff()
    {
        _fire = Variables.FireState.OFF;
    }

    public void BoostOn()
    {
        if (_boost != Variables.BoostState.OVERHEAT)
        {
            _boost = Variables.BoostState.ON;
        }
    }

    public void BoostOff()
    {
        _boost = Variables.BoostState.OFF;
    }


    //private void input()
    //{
    //    if ( Input.GetKey( KeyCode.LeftArrow ) )
    //    {
    //        _move = Variables.MoveState.LEFT;
    //    }
    //    else if ( Input.GetKey( KeyCode.RightArrow ) )
    //    {
    //        _move = Variables.MoveState.RIGHT;
    //    }
    //    else
    //    {
    //        //_move = Variables.MoveState.NEUTRAL;
    //    }

    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        if (_boost != Variables.BoostState.OVERHEAT) {
    //            _boost = Variables.BoostState.ON;
    //        }
    //    }
    //    else if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        _boost = Variables.BoostState.OFF;
    //    }

    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        _fire = Variables.FireState.ON;
    //    }
    //    else if (Input.GetKeyUp(KeyCode.A))
    //    {
    //        _fire = Variables.FireState.OFF;
    //    }
        
    //}
}
