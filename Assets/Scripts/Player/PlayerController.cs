using System;
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
    public Transform cameraRoot; // 회전 중심 (플레이어 뒷부분)
    public Transform cameraTransform; // 실제 카메라
    public float cameraDistance = 5f;
    public float cameraSmoothSpeed = 10f;
    public GameObject curInteractGameObject;
    public Interactable interactable;

    [Header("Movement")]
    [SerializeField] private float speed ;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layerMask;
    public float Speed {get {return speed;} set {speed = value;}}
    
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

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            
            if (curInteractGameObject != null)
            {
                interactable.OnInteraction();
                curInteractGameObject = null;
                interactable = null;
            }
                 
            else
            {
                Debug.Log("상호작용할 수 있는 아이템이 없습니다.");
            }
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
        // 마우스 입력으로 플레이어 회전
        float mouseX = mousePos.x * sensitibity;
        float mouseY = mousePos.y * sensitibity;
    
        transform.Rotate(0f, mouseX, 0f); // 플레이어 좌우 회전

        cameraRotation -= mouseY;
        cameraRotation = Mathf.Clamp(cameraRotation, minRot, maxRot);

        cameraRoot.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);

        // 원하는 위치 계산
        Vector3 desiredPosition = cameraRoot.position - cameraRoot.forward * cameraDistance;
    
        // 부드럽게 이동
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, Time.deltaTime * cameraSmoothSpeed);

        cameraTransform.LookAt(cameraRoot);
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

    private void OnTriggerEnter(Collider other)
    {
        curInteractGameObject = other.gameObject;
        if (curInteractGameObject != null && curInteractGameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            interactable = curInteractGameObject.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        curInteractGameObject = null;
    }
}
