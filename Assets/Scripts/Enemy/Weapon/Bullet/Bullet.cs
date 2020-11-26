using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;

    public float livingTime = 3f;

    public Color initialColor = Color.white;
    public Color finalColor;

    public int damage = 1;

    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody2D;

    private float _startingTime;
    private bool _returning;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startingTime = Time.time;
        Destroy(gameObject, livingTime);
    }

    // Update is called once per frame
    void Update()
    {

        float _timeSinceStarted = Time.time - _startingTime;
        float _percentageCompleted = _timeSinceStarted / livingTime;

        _renderer.color = Color.Lerp(initialColor,finalColor,_percentageCompleted);
    }

    private void FixedUpdate()
    {
        Vector2 movement = direction.normalized * speed;
        _rigidbody2D.velocity = movement;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_returning == false && collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("AddDamage", damage);
            Destroy(gameObject);
        }

        if (_returning == true && collision.CompareTag("Enemy")) 
        {
            collision.SendMessageUpwards("AddDamage");
            Destroy(gameObject);
        }
    }

    private void AddDamage()
    {
        _returning = true;
        direction = direction * -1f;
    }
}
