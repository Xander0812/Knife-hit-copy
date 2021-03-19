using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeConroller : MonoBehaviour
{
    [SerializeField] private ParticleSystem orangeParticle;

    private CircleCollider2D OrangeCollider;
    private SpriteRenderer OrangeSprite;

    public GameObject Knife;
    public Knife_Behaviour knifeInWood;

    private void Awake()
    {
        knifeInWood = Knife.GetComponent<Knife_Behaviour>();
        OrangeCollider = GetComponent<CircleCollider2D>();
        OrangeSprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Knife" && knifeInWood.attachedToWood != true)
        {
            GameManager.Instance.TotatlOranges += 1;
            OrangeCollider.enabled = false;
            OrangeSprite.enabled = false;
            GetComponent<ParticleSystem>().Play();
            orangeParticle.Play();
            Destroy(this.gameObject, 3.0f);
        }
    }
}
