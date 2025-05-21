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
    public Camera camera;
    public GameObject curInteractGameObject;
    private Interactable interactable;
    public float checkRate = 0.05f;
    private float lastCheckTime;
    
    
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
        camera = Camera.main;
        
    }

    private void Update()
    {
        LookInfo();
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

    public void LookInfo() //마우스포인트에서 아이템의 정보를 확인함
    {
        if (Time.time - lastCheckTime > checkRate) //업데이트 되는 시간을 조절하기 위함.
        {
            lastCheckTime = Time.time;
            
            //마우스 포인트에서 Ray를 발사함.
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != null)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    interactable = curInteractGameObject.GetComponent<Interactable>();
                    Debug.Log($"{curInteractGameObject.name} has interacted with {interactable}");
                }
            }
            else
            {
                curInteractGameObject = null;
                interactable = null;
                Debug.Log("물건이 없습니다.");
            }
        }
        
        
    }
}
