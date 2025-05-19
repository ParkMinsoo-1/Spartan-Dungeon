using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector2 mousePos;
    private Vector2 movInput;
    private float cameraRotation;
    public Transform cameraContainer;
    
    
    [Header("Movement")]
    public float speed;
    public float jumpForce;
    public LayerMask layerMask;
    
    [Header("CameraRotation")]
    public float maxRot;
    public float minRot;
    public float sensitibity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            movInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            movInput = Vector2.zero;
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        Vector3 direction = transform.forward*movInput.y + transform.right*movInput.x;
        direction *= speed;
        direction.y = _rigidbody.velocity.y;
        _rigidbody.velocity = direction;
    }

    void CameraLook()
    {
        cameraRotation += mousePos.y * sensitibity;
        cameraRotation = Mathf.Clamp(cameraRotation, minRot, maxRot);
        cameraContainer.localEulerAngles = new Vector3(-cameraRotation,0, 0);
        transform.eulerAngles += new Vector3(0, mousePos.x * sensitibity, 0);
    }

    bool IsGrounded()
    {
        Ray ray = new Ray (transform.position + transform.up * 0.01f, Vector3.down );

        if (Physics.Raycast(ray,0.5f, layerMask))
        {
            return true;
        }

        return false;
    }
}
