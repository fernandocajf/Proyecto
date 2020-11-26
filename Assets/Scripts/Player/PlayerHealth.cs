using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Transform _startPoint;
    public int totalHealth = 5;
    public RectTransform heartUI;

    //Game Over
    public RectTransform gameOverMenu;
    public GameObject hordes;


    private int health;
    private float heartSize = 16f;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private PlayerScript _controller;


    public void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<PlayerScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
    }
    
    public void AddDamage(int amount)
    {
        health = health - amount;

        StartCoroutine("VisualFeedBack");

        if(health <= 0)
        {
            health = 0;
            gameObject.SetActive(false);
        }

        heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);

        Debug.Log("El player ha sido dañado, su vida actual es " + health);
    }

    public void AddHealth(int amount)
    {
        health = health + amount;

        if (health > totalHealth)
        {
            health = totalHealth;
        }

        heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);

        Debug.Log("El player ha capturado una vida, su vida actual es " + health);
    }

    private IEnumerator VisualFeedBack()
    {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;
    }
    private void OnEnable()
    {
        health = totalHealth;
    }

    private void OnDisable()
    {
        gameOverMenu.gameObject.SetActive(true);

        hordes.SetActive(false);
        _animator.enabled = false;
        _controller.enabled = false;


        _renderer.color = Color.white;
        health = 5;
        heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);

        _controller.transform.position = new Vector2(_startPoint.transform.position.x, _startPoint.transform.position.y);

    }
}
