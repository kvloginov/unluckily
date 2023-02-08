using BreadcrumbAi;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private HealthStatus healthStatus;

    private Animator animator;

    public int attackPower = 15;

    public ObjectsUnderCollider objectsUnderHitColliderScript;


    public AudioSource hitAudioSource;
    public AudioSource missAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        healthStatus = GetComponent<HealthStatus>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthStatus.GetHealth() <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }
    }

    // called by animation
    public void doHit()
    {
        bool touched = false;
        foreach (Collider other in objectsUnderHitColliderScript.Get())
        {
            if (other.IsDestroyed())
            {
                continue;
            }
            var healthStatus = other.GetComponent(typeof(HealthStatus)) as HealthStatus;
            if (healthStatus == null)
            {
                continue;
            }
            healthStatus.TakeDamage(attackPower);
            touched = true;
        }
        if (touched)
        {
            Debug.Log("PLAY TOUCHED");
            hitAudioSource.Play();
        }
    }


    public void doPunchStart()
    {
        missAudioSource.Play();
    }
}
