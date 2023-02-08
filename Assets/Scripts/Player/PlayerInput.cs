using BreadcrumbAi;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public int attackPower = 30;
    public float speed = 10;
    public float jumpHeight = 15;


    public AudioSource hitAudioSource;
    public AudioSource missAudioSource;


    private PhysicalCC physicalCC;

    public Transform bodyRender;

    private Animator animator; 

    public ObjectsUnderCollider objectsUnderHitColliderScript;

    public HealthStatus healthStatus;


    IEnumerator sitCort;
    public bool isSitting;


    private void Start()
    {
        animator = GetComponent<Animator>();
        physicalCC = GetComponent<PhysicalCC>();
        healthStatus = GetComponent<HealthStatus>();
    }

    void Update()
    {
        if (physicalCC.isGround)
        {
            physicalCC.moveInput = Vector3.ClampMagnitude(transform.forward
                            * Input.GetAxis("Vertical")
                            + transform.right
                            * Input.GetAxis("Horizontal"), 1f) * speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                physicalCC.inertiaVelocity.y = 0f;
                physicalCC.inertiaVelocity.y += jumpHeight;
            }

            if (Input.GetKeyDown(KeyCode.C) && sitCort == null)
            {
                sitCort = sitDown();
                StartCoroutine(sitCort);
            }

           
        }

        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Punch", true);
        }
        else
        {
            animator.SetBool("Punch", false);
        }

        animator.SetFloat("MoveSpeed", physicalCC.GetVelocity());

        if (healthStatus.GetHealth() <= 0) { 
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


    IEnumerator sitDown()
    {
        if (isSitting && Physics.Raycast(transform.position, Vector3.up, physicalCC.cc.height * 1.5f))
        {
            sitCort = null;
            yield break;
        }
        isSitting = !isSitting;

        float t = 0;
        float startSize = physicalCC.cc.height;
        float finalSize = isSitting ? physicalCC.cc.height / 2 : physicalCC.cc.height * 2;

        Vector3 startBodySize = bodyRender.localScale;
        Vector3 finalBodySize = isSitting ? bodyRender.localScale - Vector3.up * bodyRender.localScale.y / 2f : bodyRender.localScale + Vector3.up * bodyRender.localScale.y;



        speed = isSitting ? speed / 2 : speed * 2;
        jumpHeight = isSitting ? jumpHeight * 3 : jumpHeight / 3;

        while (t < 0.2f)
        {
            t += Time.deltaTime;
            physicalCC.cc.height = Mathf.Lerp(startSize, finalSize, t / 0.2f);
            bodyRender.localScale = Vector3.Lerp(startBodySize, finalBodySize, t / 0.2f);
            yield return null;
        }

        sitCort = null;
        yield break;
    }
}
