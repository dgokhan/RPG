using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    [Header("Metrics")]
    public float Damp;
    [Range(1,20)]
    public float RotationSpeed;
    [Range(1,20)]
    public float StrafeTurnSpeed;


    private float normalFov;
    public float SpritFov;

    private float inputX;
    private float inputY;
    private float maxSpeed;

    public Transform Model;

    private Animator Animator;
    private Vector3 StickDirection;
    private Camera mainCam;
 
    public KeyCode SprintButton = KeyCode.LeftShift;
    public KeyCode WalkButton = KeyCode.C;
    public enum MovementType
    {
        Directional,
        Strafe
    }

    public MovementType walkType; 

    void Start()
    {
        Animator = GetComponent<Animator>();
        mainCam = Camera.main;
        normalFov = mainCam.fieldOfView;
    }

    private void LateUpdate()
    {
        Movement();
    }

    private void Movement()
    {

       if (walkType == MovementType.Strafe)
       {
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");

            Animator.SetFloat("iX", inputX, Damp, Time.deltaTime * 10);
            Animator.SetFloat("iY", inputY, Damp, Time.deltaTime * 10);

            var isWalking = inputX != 0 || inputY != 0;
            if (isWalking)
            {
                float yawCamera = mainCam.transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), StrafeTurnSpeed * Time.fixedDeltaTime);
                Animator.SetBool("strafeMoving", true);
            } 
            else
            {
                Animator.SetBool("strafeMoving", false);
            }
       }
       if (walkType == MovementType.Directional)
       {
            InputMove();
            InputRotation();

            StickDirection = new Vector3(inputX, 0, inputY);

            if(Input.GetKeyDown(KeyCode.Space))
            {

            }

            if (Input.GetKey(SprintButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, SpritFov, Time.deltaTime * 2);

                maxSpeed = 2;
                inputX = 2 * Input.GetAxis("Horizontal");
                inputY = 2 * Input.GetAxis("Vertical");
            }   
            else if (Input.GetKey(WalkButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);

                maxSpeed = 0.2f;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }  
            else
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);
            
                maxSpeed = 1;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            } 
       } 
    }

    private void InputMove()
    {
        Animator.SetFloat("Speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, Damp , Time.deltaTime * 10);
    }

    private void InputRotation()
    {
        Vector3 rotOffset = mainCam.transform.TransformDirection(StickDirection);
        rotOffset.y = 0;

        Model.forward = Vector3.Slerp(Model.forward, rotOffset, Time.deltaTime * RotationSpeed);
    }
}
