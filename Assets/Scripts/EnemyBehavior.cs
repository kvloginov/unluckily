using BreadcrumbAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private Ai ai;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<Ai>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ai.Health <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject); 
        }
    }
}
