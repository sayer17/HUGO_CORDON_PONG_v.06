using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float velocity = 10f;
    public game_manager gameManager;

    Rigidbody2D _rb;
    float _lastHorizontalDirection = 1f;
    AudioSource _audio;
    public AudioClip _aClip;
    public AudioClip _bClip;
    public float timeAlive = 0f;
    public float velocityMulti = 1f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        LaunchBall(0);
    }
    private void Update()
    {   
        timeAlive += Time.deltaTime;
        if (timeAlive > 2f && velocityMulti < 1.75f)
        {
            velocityMulti += 0.25f;
            velocity *= velocityMulti;
            timeAlive = 0f;
        }
        else
        {
            timeAlive += Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if (_rb.velocity.magnitude > velocity)
        {
            _rb.velocity = _rb.velocity.normalized * velocity;
        }

        if (Mathf.Abs(_rb.velocity.x) >= velocity * 0.3f)
        {
            _lastHorizontalDirection = Mathf.Sign(_rb.velocity.x);
        }
        else
        {
            float yDirection = Mathf.Sign(_rb.velocity.y);

            if (yDirection == 0f)
            {
                yDirection = 1f;
            }

            float minX = 0.3f;
            float y = Mathf.Sqrt(1f - minX * minX);

            _rb.velocity = new Vector2(
                _lastHorizontalDirection * minX,
                yDirection * y
            ) * velocity;
        }
    }

    void LaunchBall(int dir)
    {
        velocityMulti = 1f;
        timeAlive = 0f;
        velocity = 10f;
        this.transform.position = new Vector3(0,0,0);
        if(dir == 0)
        {
            _lastHorizontalDirection = Random.value > 0.5f ? 1f : -1f;
        }
        else
        {
            _lastHorizontalDirection = dir;
        }

        float yVelocity = Random.Range(-0.7f, 0.7f);

        _rb.velocity = new Vector2(
            _lastHorizontalDirection,
            yVelocity
        ).normalized * velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
        
    {
        if (collision.gameObject.CompareTag("Wall_R"))
        {
            gameManager.PuntosP1++;
            LaunchBall(-1);
            _audio.clip = _bClip;
            _audio.Play();
        }
        if (collision.gameObject.CompareTag("Wall_L"))
        {
            gameManager.PuntosP2++;
            LaunchBall(1);
            _audio.clip = _bClip;
            _audio.Play();
        }
        if (collision.gameObject.CompareTag("pala"))
        {
            _audio.clip = _aClip;
            _audio.Play();
        }
            gameManager.UpdatePointsText();
    }
}


