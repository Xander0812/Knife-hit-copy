using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameExplosion : MonoBehaviour
{
    //Этот скрипт отвечает за "Взрыв" объектов при удачном завершении игры.

    [SerializeField] private GameObject Target;
    [SerializeField] private Rigidbody2D thisGameObjectRigidbody;
    [SerializeField] private int explosionForce;
    private bool exploded = false;

    private void Awake()
    {
        explosionForce = 10;
        enabled = false;
        Target = GameObject.FindGameObjectWithTag("Target");
        thisGameObjectRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (this.transform.parent != Target.transform)
        {
            if (exploded == false)
            {
                Explosion();
                exploded = true;
            }
        }
    }

    public void Explosion()
    {
        thisGameObjectRigidbody.bodyType = RigidbodyType2D.Dynamic;
        thisGameObjectRigidbody.mass = 2f;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        thisGameObjectRigidbody.AddForce(-(Target.transform.position - transform.position).normalized * explosionForce, ForceMode2D.Impulse);
    }
}
