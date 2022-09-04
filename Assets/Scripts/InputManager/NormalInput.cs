using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class NormalInput : MonoBehaviour
{
    [HideInInspector] public Vector2 movement;
    [SerializeField] private UnityEvent attack1;
    [SerializeField] private UnityEvent attack2;

    [HideInInspector] public UnityEvent OnPressSpace = new UnityEvent();

    public void OnMove(InputValue input)
    {
        movement = input.Get<Vector2>();
    }
    public void OnAttack1(InputValue input)
    {
        attack1.Invoke();
    }
    public void OnAttack2(InputValue input)
    {
        attack2.Invoke();
    }
    public void OnPause()
    {
        GameManager.Instance.PauseGame(true);
    }
    public void OnSpace()
    {
        OnPressSpace.Invoke();
    }
}
