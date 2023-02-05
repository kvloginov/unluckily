using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsUnderCollider : MonoBehaviour
{

    public string[] tagsToStore;


    private HashSet<string> tagsToStoreAsSet = new();
    private HashSet<Collider> colliders; 
    
    // Start is called before the first frame update
    void Start()
    {
        colliders = new HashSet<Collider>();
        
        foreach (string tag in tagsToStore )
        {
            tagsToStoreAsSet.Add( tag );    
        }

    }

    public HashSet<Collider> Get()
    {
        return colliders;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tagsToStoreAsSet.Contains(other.gameObject.tag))
        {
            Debug.Log("collider add " + other.gameObject.name);
            colliders.Add(other);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (tagsToStoreAsSet.Contains(other.gameObject.tag))
        {
            Debug.Log("collider remove " + other.gameObject.name);
            colliders.Remove(other);
        }
    }
}
