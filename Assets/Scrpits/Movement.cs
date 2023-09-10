using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    private float deltaX;
    private float deltaZ;
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float xRot;
    private bool jump=false;
    private bool run=false;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private Rigidbody playerBody;

    [SerializeField] private float speed;
    [SerializeField] private Vector2 sensitivity;
    [SerializeField] private float jumpForce;
    [SerializeField] private collection collectScript;

    InputChannel inputChannel;

    [SerializeField] Button replay;
    [SerializeField] Button exit;
    [SerializeField] Text pauseText;
    private bool paused=false;
    // Start is called before the first frame update
    void Start()
    {
        var beacon = FindObjectOfType<BeaconSO>();
        inputChannel = beacon.inputChannel;
        inputChannel.MoveEvent += HandleMovement;
        inputChannel.RunEvent += HandleRun;
        inputChannel.RunCancelledEvent += HandleRunCancel;
        inputChannel.JumpEvent += HandleJump;
        inputChannel.MouseEvent += HandleMouse;
        inputChannel.OtherPauseEvent += HandlePause;

        collectScript= FindObjectOfType<collection>();
        playerBody = GetComponent<Rigidbody>();
        Cursor.visible =false;
    }

    void HandleRun(){
        run=true;
    }

    void HandleRunCancel(){
        run=false;
    }

    void HandleMovement(Vector2 dir){
        deltaX=dir.x;
        deltaZ=dir.y;
    }

    void HandleMouse(Vector2 dir){
        playerMouseInput=dir;
    }

    void HandleJump(){
        jump=true;
    }

    void HandlePause(){
        paused=!paused;
    }

    void Update(){
        CheakPause();
    }

    void CheakPause(){
        if(paused){
            replay.gameObject.SetActive(true);
            pauseText.gameObject.SetActive(true);
            exit.gameObject.SetActive(true); 
            Time.timeScale = 0f;
            Cursor.visible =true;
        }else{
            replay.gameObject.SetActive(false);
            pauseText.gameObject.SetActive(false);
            exit.gameObject.SetActive(false); 
            Time.timeScale = 1f;
            Cursor.visible =false;
        }
    }
    
    void FixedUpdate()
    {
        playerMovementInput = new Vector3(deltaX,0f,deltaZ);

        MovePlayer();
        MovePlayerCamera();
        
    }

    void MovePlayer(){
        Vector3 MoveVector = transform.TransformDirection(playerMovementInput)*speed;
        if(run){
            MoveVector=MoveVector*((int)(1+collectScript.runSpeed));
        }
        playerBody.velocity = new Vector3(MoveVector.x,playerBody.velocity.y,MoveVector.z);

        if(jump){
            if(playerBody.velocity.y<=0.1&&playerBody.velocity.y>=-0.1){
                playerBody.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            }
            jump=false;
        }
    }

    void MovePlayerCamera(){
        xRot -= playerMouseInput.y * sensitivity.y;
        if(xRot>20){
            xRot=20;
        }else if (xRot<-20){
            xRot=-20;
        }
        transform.Rotate(0f,playerMouseInput.x*sensitivity.x,0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot,0f,0f); 
    }
}
