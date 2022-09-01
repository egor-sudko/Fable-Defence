using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int damage = 15;

    private void Update()
    {
        transform.Translate(Vector3.back * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IDamagable damagable))
        {
            damagable.ApplyDamage(damage);

            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("BulletDeadZone"))
        {
            gameObject.SetActive(false);
        }
    }
}
