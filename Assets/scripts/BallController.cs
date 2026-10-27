using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float velocity = 10f;
    public game_manager gameManager;

    Rigidbody2D _rb;
    float _lastHorizontalDirection = 1f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        LaunchBall(0);
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
        }
        if (collision.gameObject.CompareTag("Wall_L"))
        {
            gameManager.PuntosP2++;
            LaunchBall(1);
        }
        
        gameManager.UpdatePointsText();
    }
}


