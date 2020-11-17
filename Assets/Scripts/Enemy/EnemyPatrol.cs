using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float minX;
    public float maxX;
    public float waitingTime = 2f;

    private GameObject _target;
    private Animator _animator;
    private Weapon _weapon;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateTarget();
        StartCoroutine("PatrolToTarget");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTarget()
    {
        //Puntos de dirección del enemigo
        // Al inicio, crea un target a la izquierda
        if (_target == null)
        {
            _target = new GameObject("Target");
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1,1,1);
            return;
        }
        // If we are in the left, change target to the rght
        if (_target.transform.position.x == minX)
        {
            _target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1,1,1);
        }

        // If we are in the right, change target to the left
        else if(_target.transform.position.x == maxX)
        {
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    private IEnumerator PatrolToTarget()
    {
        // Corrutina para mover al enemigo
        while (Vector2.Distance(_target.transform.position, transform.position) > 0.05f)
        {
            _animator.SetBool("Idle",false);

            Vector2 direction = _target.transform.position - transform.position;
            float xDirection = direction.x;

            transform.Translate(direction.normalized * speed * Time.deltaTime);

            // IMPORTANTE
            yield return null;
        }

        // At this point, I've reached the target, let's set our position the target's one
        transform.position = new Vector2(_target.transform.position.x, transform.position.y);

        UpdateTarget();

        _animator.SetBool("Idle", true);

        // Shot
        _animator.SetTrigger("Shoot");



        // And let's wait for a moment
        yield return new WaitForSeconds(waitingTime);

        // Once waited, let's restore the patrol behaviour

        StartCoroutine("PatrolToTarget");
    }

    //void CanShoot()
    //{
    //    if (_weapon != null)
    //    {
    //        _weapon.Shoot();
    //    }
    //}
}
