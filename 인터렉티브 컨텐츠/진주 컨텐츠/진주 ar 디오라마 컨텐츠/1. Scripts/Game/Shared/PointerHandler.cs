using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] WalkingModeController _WalkingModeController;

    public enum Arrow { Left, Right }
    public Arrow _Arrow = Arrow.Left;
    bool IsPointerDown = false;
    Animator _ArrowAnimator;


    private void Awake()
    {
        _ArrowAnimator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        IsPointerDown = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPointerDown = true;
        _ArrowAnimator.SetBool("Press", IsPointerDown);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPointerDown = false;
        _ArrowAnimator.SetBool("Press", IsPointerDown);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPointerDown)
        {
            if (_Arrow.Equals(Arrow.Left))
            {
                _WalkingModeController.MoveCamera("L");
            }
            else
            {
                _WalkingModeController.MoveCamera("R");
            }
        }
    }
}
