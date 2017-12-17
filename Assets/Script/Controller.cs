using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDragHandler
{
    private Vector2 _oldPos;
    private Vector2 _center;
    private RectTransform _pivotTransform;
    private GameObject _target;

	// Use this for initialization
	void Start () {
        _target = GameObject.FindGameObjectWithTag("Player");
        _pivotTransform = GameObject.Find("Controller.Pivot").GetComponent<RectTransform>();

        RectTransform rt = GetComponent<RectTransform>();
        _center = Camera.main.WorldToScreenPoint(rt.transform.position);
    }

    public void OnPointerDown( PointerEventData eventData )
    {
        _oldPos = eventData.pointerCurrentRaycast.screenPosition - _center;
        _pivotTransform.localPosition = _oldPos;
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        this.EndDrag();
    }

    public void OnPointerExit( PointerEventData eventData )
    {
        this.EndDrag();
    }

    private void EndDrag()
    {
        _pivotTransform.localPosition = Vector2.MoveTowards(_oldPos, Vector2.zero, Vector2.Distance(_oldPos, Vector2.zero));
        ExecuteEvents.Execute<IPlayerActionHandler>(_target, null, (x, y) => x.TurnNeutral());
    }

    public void OnDrag( PointerEventData eventData )
    {
        Vector2 position = eventData.pointerCurrentRaycast.screenPosition - _center;
        _pivotTransform.localPosition = position;
        float angle = Vector2.SignedAngle(position, _oldPos);        
        if ( angle > 0.0f )
        {
            ExecuteEvents.Execute<IPlayerActionHandler>(_target , null, (x, y) => x.TurnRight());
        }
        else if ( angle < 0.0f )
        {
            ExecuteEvents.Execute<IPlayerActionHandler>(_target, null, (x, y) => x.TurnLeft());
        }
      
        _oldPos = position;
    }
    
}
