using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls myControls;

    public float valueX;
    public bool jumpInput;

    public bool tryAttack;

    public float valueY;
    public bool tryToClimb;

    private void Awake()
    {
        myControls = new Controls();
    }

    private void OnEnable()
    {
        myControls.Player.Move.performed += StartMove;
        myControls.Player.Move.canceled += StopMove;

        myControls.Player.Jump.performed += JumpStart;
        myControls.Player.Jump.canceled += JumpStop;

        myControls.Player.Attack.performed += TryToAttack;
        myControls.Player.Attack.canceled += StopTryToAttack;

        myControls.Player.Climb.performed += ClimbStart;
        myControls.Player.Climb.canceled += ClimbStop;

        myControls.Player.Enable();
    }

    private void OnDisable()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled -= StopMove;

        myControls.Player.Jump.performed -= JumpStart;
        myControls.Player.Jump.canceled -= JumpStop;

        myControls.Player.Attack.performed -= TryToAttack;
        myControls.Player.Attack.canceled -= StopTryToAttack;

        myControls.Player.Climb.performed -= ClimbStart;
        myControls.Player.Climb.canceled -= ClimbStop;

        myControls.Player.Disable();
        //myControls.Disable();
    }

    public void DisableControls()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled -= StopMove;

        myControls.Player.Jump.performed -= JumpStart;
        myControls.Player.Jump.canceled -= JumpStop;

        myControls.Player.Attack.performed -= TryToAttack;
        myControls.Player.Attack.canceled -= StopTryToAttack;

        myControls.Player.Climb.performed -= ClimbStart;
        myControls.Player.Climb.canceled -= ClimbStop;

        myControls.Player.Disable();
        valueX = 0;
    }

    private void StartMove(InputAction.CallbackContext ctx)
    {
        valueX = Mathf.RoundToInt(ctx.ReadValue<float>());
        Debug.Log("valueX: " + valueX);
    }

    private void StopMove(InputAction.CallbackContext ctx)
    {
        valueX = 0;
    }

    private void JumpStart(InputAction.CallbackContext ctx)
    {
        jumpInput = true;
    }

    private void JumpStop(InputAction.CallbackContext ctx)
    {
        jumpInput = false;
    }

    private void TryToAttack(InputAction.CallbackContext ctx)
    {
        tryAttack = true;
    }

    private void StopTryToAttack(InputAction.CallbackContext ctx)
    {
        tryAttack = false;
    }


    private void ClimbStart(InputAction.CallbackContext ctx)
    {
        valueY = Mathf.RoundToInt(ctx.ReadValue<float>());

        if(Mathf.Abs(valueY) > 0)
        {
            tryToClimb = true;
        }
    }

    private void ClimbStop(InputAction.CallbackContext ctx)
    {
        tryToClimb = false;
        valueY = 0;
    }
}
