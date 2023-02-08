using BreadcrumbAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStatus : MonoBehaviour
{

    public int health = 100;


    public void TakeDamage(int initialDamage)
    {
        health -= initialDamage;
        Debug.Log("now " + name + "hp is " + health);
    }

    public int GetHealth()
    {
        return health;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
