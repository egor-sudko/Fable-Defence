using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private int poolCount = 5;
    [SerializeField] private bool autoExpand = true;
    [SerializeField] private Bullet bulletPrefab;
    [Space]
    [SerializeField] private Transform startBulletPosition;

    private PoolMono<Bullet> pool;

    [SerializeField]private float timeToNextShot = 0.75f;
    private bool nextShot = true;

    private void Start()
    {
        pool = new PoolMono<Bullet>(bulletPrefab, poolCount, transform);
        pool.autoExpand = autoExpand;
    }

    private void Update()
    {
        if (nextShot)
        {
            StartCoroutine(Fire());
            nextShot = false;
        }
    }

    IEnumerator Fire()
    {
        var bullet = pool.GetFreeElement();
        bullet.transform.position = startBulletPosition.position;
        yield return new WaitForSeconds(timeToNextShot);
        nextShot = true;
    }
}
