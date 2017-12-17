using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Boost : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler  {

    // Use this for initialization
    private bool _pushed = false;
    private GameObject _target;
    private Player _playerScript;
    private Image _image;

	void Start () {
        _target = GameObject.FindGameObjectWithTag("Player");
        _playerScript = _target.GetComponent<Player>();
        _image = GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {
        if (_pushed)
        {
            ExecuteEvents.Execute<IPlayerActionHandler>(_target, null, (x, y) => x.BoostOn());
        }
        else
        {
            ExecuteEvents.Execute<IPlayerActionHandler>(_target, null, (x, y) => x.BoostOff());
        }

        FillBoost();
    }

    private void FillBoost()
    {
        float amount = _playerScript.GetBoostTime() / Variables.BOOST_TIME_LIMIT;
        _image.fillAmount = amount;
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
