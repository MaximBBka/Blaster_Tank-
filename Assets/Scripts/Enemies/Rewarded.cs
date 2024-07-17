using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewarded : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private void Start()
    {
        rb.AddForce(new Vector2(Random.Range(-3, 3), Random.Range(1, 4)) * 3, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<TankBase>(out TankBase tankBase))
        {
            MainUi.Instance.TotalMoney += 1;
            Destroy(this.gameObject);
            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Coin);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PetDelivery>(out PetDelivery petDelivery))
        {
            MainUi.Instance.TotalMoney += 1;
            Destroy(this.gameObject);
            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Coin);
        }
    }
    public void DestroyRewarded()
    {
        Destroy(this.gameObject, 60f);
    }
    private void Update()
    {
        DestroyRewarded();
    }
}
