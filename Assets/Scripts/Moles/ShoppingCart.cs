using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class ShoppingCart : MonoBehaviour
{ 
    [HideInInspector] [SerializeField] private Animator m_animator;

    private void OnValidate()
    {
        m_animator = GetComponent<Animator>();
    }

    public void GetMovingDirection(InputAction.CallbackContext _ctx)
    {
        if (_ctx.ReadValue<Vector2>().x > 0)
        {
            OnMovingRight(_ctx);
        }
        else if(_ctx.ReadValue<Vector2>().x < 0)
        {
            OnMovingLeft(_ctx);
        }
        else
        {
            m_animator.SetBool("MovingLeft", false);
            m_animator.SetBool("MovingRight", false);
        }
    }
    
    public void OnMovingLeft(InputAction.CallbackContext _ctx)
    {
        if (_ctx.performed)
        {
            m_animator.SetBool("MovingLeft", true);
            m_animator.SetBool("MovingRight", false);
        }
        else if(_ctx.canceled)
        {
            m_animator.SetBool("MovingLeft", false);
        }
    }
    
    public void OnMovingRight(InputAction.CallbackContext _ctx)
    {
        if (_ctx.performed)
        {
            m_animator.SetBool("MovingLeft", false);
            m_animator.SetBool("MovingRight", true);
        }
        else if(_ctx.canceled)
        {
            m_animator.SetBool("MovingRight", false);
        }
    }
    
    
}
