using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifetime = 2f;
    [SerializeField] public float damage = 50f;
    public static UnityEvent turnchange = new UnityEvent();
    // Start is called before the first frame update

    private void Awake()
    {
        Destroy(this.gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        turnchange?.Invoke();
    }

}
