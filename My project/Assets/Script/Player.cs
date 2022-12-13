using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject scanObject;

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private float applySpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private GameObject manager;

    private bool isRun = false;
    private bool isGround = true;
    private bool isTalk;

    [SerializeField]
    private float lookSensitvity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationx;

    //필요한 컴포넌트
    [SerializeField]
    private Camera theCamrea;
    private Rigidbody2D myRigid;
    private CapsuleCollider2D capsuleCollider;


    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        myRigid = GetComponent<Rigidbody2D>();

        applySpeed = walkSpeed;
    }

    void Update()
    {
        IsGround();             //지면에 닿아잇는지아닌지 유무
        TryJump();              //점프시도
        TryRun();               //달리기시도
        Move();                 //키보드 입력에따라 이동
        CameraRotation();       //마우스 위아래 y 움직임에따라 카메라 x축회전
                //마우스 좌우 x 움직임에따라 캐릭터 y축회전
        TryTalk();
    }

    private void IsGround() //지면착지유무
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }

    private void TryTalk()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //    manager.Action(scanObject);
        
        //isTalk(true);

        //else if()

        //isTalk(false);

    }

    private void TryJump() //점프시도
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump() // 점프힘
    {
        myRigid.velocity = transform.up * jumpForce;
    }

    private void TryRun() //달리기시도
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }

    private void RunningCancel() //달리기멈춤
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirz = Input.GetAxisRaw("Vertical");
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirz;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CameraRotation() // 좌우캐릭터 회전
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitvity;

        currentCameraRotationx -= _cameraRotationX;
        currentCameraRotationx = Mathf.Clamp(currentCameraRotationx, -cameraRotationLimit, cameraRotationLimit);

        theCamrea.transform.localEulerAngles = new Vector3(currentCameraRotationx, 0f, 0f);
    }


}

