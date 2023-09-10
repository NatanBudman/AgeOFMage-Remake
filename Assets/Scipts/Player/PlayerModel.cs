using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public float Speed;
    public float MouseRotationSpeed;
    public Rigidbody2D _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 dir) 
    {
        if (dir == Vector2.zero) 
        {
            _rb.velocity = Vector2.zero;
            return;
        }
        Vector2 velocidadMovimiento = dir;
        velocidadMovimiento.Normalize();
        _rb.velocity = velocidadMovimiento * Speed;
    }

    public void MouseRotation() 
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, MouseRotationSpeed * Time.deltaTime);
    }
}

