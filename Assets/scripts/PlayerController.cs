using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float topHeigh;// lmite de las palas
    public float botHeigh;
    public bool useArrowKeys = false;// if true, use arrow key; if false, use W S
    public bool IA;
    public GameObject ball;
    public float IAfollowTolerance = 0.5f;

    void Update()
    {
        if (IA == false)
        {
            MovePaddle();
        }
        else
        {
            MovePaddleIA();
        }
       
    }
    void MovePaddle()
    {
        this.gameObject.transform.position = new Vector3(this.transform.position.x, ConstrainPaddle(), this.transform.position.z);
    }
    void MovePaddleIA()
    {
       
        int direccion = 0;
        float diferencia = ball.transform.position.y - transform.position.y;


        if (diferencia > IAfollowTolerance)
        {
            direccion = 1;
        }
        else if (diferencia < -IAfollowTolerance)
        {
            direccion = -1;
        }
        float nuevaY = transform.position.y + (direccion * moveSpeed / 2 * Time.deltaTime);
        if (nuevaY > topHeigh)
        {
            nuevaY = topHeigh;
        }
        if (nuevaY < botHeigh)
        {
            nuevaY = botHeigh;
        }
        this.gameObject.transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
    }
    float ConstrainPaddle()
    {
        float nuevaY = this.gameObject.transform.position.y + (PaddleDirection() * moveSpeed * Time.deltaTime);
        if (nuevaY > topHeigh)
        {
            nuevaY = topHeigh;
        }
        if (nuevaY < botHeigh)
        {
            nuevaY = botHeigh;
        }
        return nuevaY;
    }
        //nuevaY = Math,Clamp(nuevaY, limiteAbajo, limiteArriba);
    
    float PaddleDirection()
    {
        float direccion = 0f;
        if (useArrowKeys == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                direccion = 1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direccion = -1f;
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                direccion = 1f;

            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                direccion = -1f;
            }

        }
        return direccion;
    }
}