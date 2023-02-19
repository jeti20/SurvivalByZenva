using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask;


    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot; //sledzenie aktualnego x
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    //components
    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //znikniecie kursora przy stacie
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (canLook == true)
        {
            CameraLook();
        }
        
    }

    private void FixedUpdate()
    {
        Move();

    }

    void Move()
    {
        //liczy kierunek w odniesieniu do tego w która strone paczymy
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rig.velocity.y;

        rig.velocity = dir;
    }
   
    void CameraLook() //pozwala na sprawienie, ¿e kamera porusza siê gora i dol w okreslonym max i min zasiegu
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);  // usun "-" jesli chcemy zrobiv zeby poruszanie bylo revert w osi y 

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // os x
    }

    // called when we move our mouse - managed by the Input System
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // called when we press WASD - managed by the Input System
    public void onMoveInput(InputAction.CallbackContext context)
    {
        // are we holding down a movement button?
        if (context.phase == InputActionPhase.Performed) //trzymamy klawisz/binding
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        // have we let go of a movement button?
        else if (context.phase == InputActionPhase.Canceled) //puszczmay klawisz/banding
        {
            curMovementInput = Vector2.zero;
        }
        
    }

    // called when we press down on the spacebar - managed by the Input System
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        // is this the first frame we're pressing the button?
        if (context.phase == InputActionPhase.Started) //isActiveAndEnabled this the first frame we are pressing the button?
        {
            // are we standing on the ground?
            if (IsGrounded())
            {
                rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    bool IsGrounded ()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
        };

        for (int  i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    //wlaczenie kursora myszy przy otwarciu eq
    public void Togglecursor (bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
