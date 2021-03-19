using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeSpawn : MonoBehaviour
{
    //Как говорит название, это скрипт на спаун апельсинок, однако тут вскоре появился и скрипт на появление ножей в цели.
    //Не очень хорошо было лепить обе вещи в один скрипт (хотя бы с таким названием), но и иметь 2 скипта, которые делают одно и то же с разными объектами тоже не очень.

    public OrangeSpawnChance orangeSpawnChance;
    public GameObject Orange;
    private int numberOfRolls = 1;

    public GameObject Knife;
    [SerializeField] private int maxNumberOfKnifesToSpawn;

    private int currentRoll;

    void Start()
    {
        Vector3 center = transform.position;
        currentRoll = Random.Range(1, 100);
        for (int i = 0; i < numberOfRolls; i++)
        {
            if (currentRoll <= orangeSpawnChance.spawnChance)
            {
                int ObjectPosition = Random.Range(0, 360);
                Vector3 orangePosition = RandomCircle(center, 2.0f, ObjectPosition);
                Instantiate(Orange, orangePosition, Quaternion.Euler(0, 0, -ObjectPosition), transform);
                currentRoll = Random.Range(1, 100);
                i--;
            }
        }

        for(int i = 0; i < Random.Range(1, maxNumberOfKnifesToSpawn); i++)
        {
            int ObjectPosition = Random.Range(0, 360);
            Vector3 knifePosition = RandomCircle(center, 1.5f, ObjectPosition);
            Instantiate(Knife, knifePosition, Quaternion.Euler(0, 0, -ObjectPosition+180), transform);
        }
    }
    private void Update()
    {
        currentRoll = Random.Range(1, 100);
    }
    Vector3 RandomCircle(Vector3 center, float radius, int rotation)
    {
        float angle = rotation;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

}
