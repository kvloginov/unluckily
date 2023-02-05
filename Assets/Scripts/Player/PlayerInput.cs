using BreadcrumbAi;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public int attackPower = 30;
    public float speed = 5;
    public float jumpHeight = 15;
    public PhysicalCC physicalCC;

    public Transform bodyRender;

    public Animator handAnimator; // TODO: move to hand script?

    public ObjectsUnderCollider objectsUnderHitColliderScript;


    IEnumerator sitCort;
    public bool isSitting;


    private void Start()
    {

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

            if (Input.GetMouseButton(0))
            {
                handAnimator.SetBool("Punch", true);
            }
            else
            {
                handAnimator.SetBool("Punch", false);
            }
        }
    }

    // called by animation
    public void doHit()
    {
        foreach (Collider other in objectsUnderHitColliderScript.Get())
        {
            var ai = other.GetComponent(typeof(Ai)) as Ai;
            if (ai == null)
            {
                continue;
            }
            ai.Health -= attackPower;
            Debug.Log("now " + other.name + "hp is " + ai.Health);
        }
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
