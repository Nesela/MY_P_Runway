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

    //�ʿ��� ������Ʈ
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
        IsGround();             //���鿡 ����մ����ƴ��� ����
        TryJump();              //�����õ�
        TryRun();               //�޸���õ�
        Move();                 //Ű���� �Է¿����� �̵�
        CameraRotation();       //���콺 ���Ʒ� y �����ӿ����� ī�޶� x��ȸ��
                //���콺 �¿� x �����ӿ����� ĳ���� y��ȸ��
        TryTalk();
    }

    private void IsGround() //������������
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

    private void TryJump() //�����õ�
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump() // ������
    {
        myRigid.velocity = transform.up * jumpForce;
    }

    private void TryRun() //�޸���õ�
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

    private void RunningCancel() //�޸������
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

    private void CameraRotation() // �¿�ĳ���� ȸ��
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitvity;

        currentCameraRotationx -= _cameraRotationX;
        currentCameraRotationx = Mathf.Clamp(currentCameraRotationx, -cameraRotationLimit, cameraRotationLimit);

        theCamrea.transform.localEulerAngles = new Vector3(currentCameraRotationx, 0f, 0f);
    }


}

