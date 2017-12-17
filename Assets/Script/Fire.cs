using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private volatile bool _pushed = false;
    private GameObject _target;

    // Use this for initialization
    void Start () {
        _target = GameObject.FindGameObjectWithTag("Player");
          
    }

    void Update()
    {
        if ( _pushed )
        {
            ExecuteEvents.Execute<IPlayerActionHandler>(_target, null, (x, y) => x.FireOn());
        }
        else
        {
            ExecuteEvents.Execute<IPlayerActionHandler>(_target, null, (x, y) => x.FireOff());
        }
    }

    public void OnPointerDown( PointerEventData eventData )
    {
        _pushed = true;
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        _pushed = false;
    }

    public void OnPointerExit( PointerEventData eventData )
    {
        _pushed = false;
    }
}
