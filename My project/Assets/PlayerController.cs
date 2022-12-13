using UnityEngine;


    public class PlayerController : MonoBehaviour
    {

        public float walkSpeed = 5f;
        public float jumpForce = 20f;

        private bool isGround = true;
        bool isJumping = false;
        

        //필요한 컴포넌트
        
        private Animator anim;
        private CapsuleCollider2D capsuleCollider;
        [SerializeField]
        private Rigidbody2D myRigid;

    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetBool("isJump", false);
    }

    void Start()
        {
            //컴포넌트 할당
            capsuleCollider = GetComponent<CapsuleCollider2D>();
            myRigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            //IsGround();
            Move();
            TryJump();

        Debug.DrawRay(myRigid.position, Vector3.down, new Color(0, 1, 0));
    }

        //지면체크
        //private void IsGround()
        //{
        //    isGround = Physics2D.Raycast(myRigid.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        
        //}

        private void TryJump()
        {
            if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
            && !anim.GetBool("isJump"))
            {
                isJumping = true;
                anim.SetBool("isJump", true);
            }
            if (!isJumping)
            {
                return;
            }

        myRigid.velocity = transform.up * jumpForce;

        isJumping = false;
        }


    private void Move()
        {
            float _moveDirX = Input.GetAxisRaw("Horizontal");
            //float _moveDirz = Input.GetAxisRaw("Vertical");
            Vector3 _moveHorizontal = transform.right * _moveDirX;
            //Vector3 _moveVertical = transform.forward * _moveDirz;

            Vector3 _velocity = (_moveHorizontal).normalized * walkSpeed;
          
            transform.position += _velocity * walkSpeed * Time.deltaTime;
    }
}
