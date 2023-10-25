using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    [SerializeField] private int damage;
    public float radius;
    private ParticleSystem particle;

    [HideInInspector] public Magic magic;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider c in enemies)
        {
            if (c.TryGetComponent(out EnemyCtrl enemy))
            {
                enemy.TakeDamage(damage);
            }
        }
        particle.Stop();
        magic.area.position = new Vector3(0f, -1f, 0f);
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        while (particle.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
