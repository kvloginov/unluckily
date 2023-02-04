using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyScript;

public class Spawner : MonoBehaviour
{
    public LootDrop Loot;
    public int RandomDropCount = 1;
    public float DropRange = .5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Loot.SpawnDrop(this.transform, RandomDropCount, DropRange);
        }
    }
}
