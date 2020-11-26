using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float wallAware = 0.5F;
    public LayerMask groundLayer;
 
    public float aimingTime = 0.2f;
    public float shootingTime = 1f;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Weapon _weapon;

    private bool _facingRight;
    private bool _isAttacking;



    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    // Start is called before the first frame update
    void Start()
    {

        //Direccion del enemigo
        if (transform.localScale.x < 0f)
        {
            _facingRight = false;
        }else if (transform.localScale.x > 0f)
        {
            _facingRight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.right;

        if (_facingRight == false)
        {
            direction = Vector2.left;
        }

        if(_isAttacking == false)
        {
            //Si el enemigo se encuentra a una distancia de media unidad de unity de un muro entonces el enemigo gira.
            if(Physics2D.Raycast(transform.position, direction, wallAware, groundLayer))
            {
                Flip();
            }
        }
        
    }
    private void FixedUpdate()
    {
        float horizontalVelocity = speed;

        if (_facingRight == false)
        {
            horizontalVelocity = horizontalVelocity * -1f;
        }

        if (_isAttacking)
        {
            horizontalVelocity = 0f;
        }
        _rigidbody2D.velocity = new Vector2(horizontalVelocity, _rigidbody2D.velocity.y);
    }

    public void LateUpdate()
    {
        _animator.SetBool("Idle", _rigidbody2D.velocity == Vector2.zero);
    }

    //Hacer que el enemigo no se mueva y dispare 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isAttacking == false && collision.CompareTag("Player"))
        {
            StartCoroutine(AimAndShoot());
        }
    }

    private IEnumerator AimAndShoot()
    {
        float speedBackup = speed;
        speed = 0;

        _isAttacking = true;

        yield return new WaitForSeconds(aimingTime);

        _animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(shootingTime);

        _isAttacking = false;
        speed = speedBackup;
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }

    void CanShoot()
    {
        if(_weapon != null)
        {
            _weapon.Shoot();
        }
    }

    private void OnEnable()
    {
        _isAttacking = false;
    }

    private void OnDisable()
    {
        StopCoroutine("AimAndShoot");
        _isAttacking = false;
    }
}
