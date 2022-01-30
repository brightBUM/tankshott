using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TankControl : MonoBehaviour
{
    [SerializeField] public float Health = 100f;
    [SerializeField] Text healthtxt;
    [SerializeField] float movespeed = 2f;
    
    Rigidbody2D rb;
    public bool movable;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        healthtxt.text = Health.ToString();
        if(movable)
        {
            rb.position += new Vector2(Input.GetAxis("Horizontal"), 0) * Time.deltaTime * movespeed;
        }
    }
   
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Bullet>(out Bullet obj))
        {
            Health -= obj.damage;
        }
    }
}
