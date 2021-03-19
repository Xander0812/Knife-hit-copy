using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife_Behaviour : MonoBehaviour
{
    //Скрипт на поведение ножа.

    [SerializeField] private Vector2 throwForce;
    private bool isActive = true;
    private BoxCollider2D knifeCollider;
    private Rigidbody2D knifeRigidbody;

    public bool attachedToWood;

    private void Awake()
    {
        Vibration.Init();
        knifeRigidbody = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive)
        {
            knifeRigidbody.AddForce(throwForce, ForceMode2D.Impulse);
            knifeRigidbody.gravityScale = 1;
            GameController.Instance.GameUI.DecrementDisplayedKnifeCount();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive)
            return;

        isActive = false;

        if(collision.collider.tag == "Target")
        {
            GetComponent<ParticleSystem>().Play();

            knifeRigidbody.velocity = new Vector2(0, 0);
            knifeRigidbody.bodyType = RigidbodyType2D.Kinematic;
            this.transform.SetParent(collision.collider.transform);
            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.4f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 1.2f);

            Vibration.VibratePop();

            GameController.Instance.OnSuccessfullKnifeHit();

            attachedToWood = true;
        }
        else if(collision.collider.tag == "Knife"){
            knifeRigidbody.velocity = new Vector2(knifeRigidbody.velocity.x, -2);

            knifeCollider.enabled = false;

            Vibration.Vibrate(200);

            GameController.Instance.StartGameOverSequence(false);
        }
        else if (collision.collider.tag == "Orange")
        {
            GetComponent<ParticleSystem>().Play();
        }
    }

}