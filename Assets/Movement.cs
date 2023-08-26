using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        collectScript= FindObjectOfType<collection>();
        Cursor.visible =false;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(playerBody.velocity.y<=0.1&&playerBody.velocity.y>=-0.1){
                jump=true;
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            run=true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            run=false;
        }
    }
    
    void FixedUpdate()
    {
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
        
    }

    void MovePlayer(){
        Vector3 MoveVector = transform.TransformDirection(playerMovementInput)*speed;
        if(run){
            MoveVector=MoveVector*((int)(1+0.2*collectScript.score));
        }
        playerBody.velocity = new Vector3(MoveVector.x,playerBody.velocity.y,MoveVector.z);

        if(jump){
            playerBody.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
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
