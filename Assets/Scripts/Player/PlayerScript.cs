using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 2.5f;
    public float jumpForce = 3.4f;
    public float longIdleTime = 5f;

    //Verificar si el player choca el suelo para saltar
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;


    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private Vector2 _movement;

    //Mirar a la derecha o izquierda
    private bool _facingRight = true;
    //Pisando el suelo
    private bool _isGrounded;
    //Atacar
    private bool _isAttacking;
    //Animación larga de Idle
    private float _longIdleTimer;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento
        if (_isAttacking == false) { 
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            _movement = new Vector2(horizontalInput, 0f);

            //Flip Player
            if (horizontalInput < 0f && _facingRight == true)
            {
                Flip();
            }else if (horizontalInput > 0f && _facingRight == false)
            {
                Flip();
            }
            
        }

        //Saltar
        //Comprobar si el player está en el suelo
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && _isGrounded == true && _isAttacking == false)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //Atacar
        if (Input.GetButtonDown("Fire1") && _isGrounded == true && _isAttacking == false)
        {
            _movement = Vector2.zero;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger("Attack");
        }
    }
    void FixedUpdate()
    {
        if (_isAttacking == false) { 
            float horizontalVelocity = _movement.normalized.x * speed;
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
        }
    }

    void LateUpdate()
    {
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("IsGrounded",_isGrounded);
        _animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);

        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _isAttacking = true;
        }else
        {
            _isAttacking = false;
        }
        //Animación larga de Idle
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            _longIdleTimer += Time.deltaTime;
            if (_longIdleTimer >= longIdleTime)
            {
                _animator.SetTrigger("LongIdle");
            }
        }
        else
        {
            _longIdleTimer = 0f;
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);

    }

}
