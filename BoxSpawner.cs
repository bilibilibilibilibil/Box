using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box_prefabs;

    public void SpawnBox()
    {
        GameObject obj_box = Instantiate(box_prefabs);
        Vector3 temp = transform.position;
        temp.z = 0f;
        obj_box.transform.position = temp;
    }


}
