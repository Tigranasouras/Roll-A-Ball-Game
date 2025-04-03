using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLight : MonoBehaviour
{
    public string enemyTag = "Enemy";
    private Transform enemyTransform;
    public Vector3 offset = new Vector3(0, 3, 0);

    void Update()
    {

        if (enemyTransform == null)
        {
            GameObject enemy = GameObject.FindGameObjectWithTag(enemyTag);
            //Debug.Log("No enemy!");
            if (enemy != null)
            {
                enemyTransform = enemy.transform;
                //Debug.Log("Enemy found!");
            }
        }

        if (enemyTransform != null)
        {
            transform.position = enemyTransform.position + offset;
          

        }

    }
}
