﻿using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class backgroundLoop : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    private Camera mainCamera;
    private Vector2 screenBounds;

    private void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in levels) {
            loadChildObjects(obj);
        }
    }

    void loadChildObjects(GameObject obj) {
        //Debug.Log(obj.name);
        //Transform[] children = obj.GetComponentsInChildren<Transform>();
        //float objectWidth = children[1].GetComponent<SpriteRenderer>().bounds.size.x;
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        //Debug.Log("objectWidth: " + objectWidth + " screenBounds.x: " + screenBounds.x + " screenBounds.y: " + screenBounds.y);
        //Debug.Log(childsNeeded);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++) {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void repositionChildObjects(GameObject obj) {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        //Debug.Log(children.Length);
        if (children.Length > 1) {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth) {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
            }
            else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth) {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }

    private void LateUpdate()
    {
        foreach (GameObject obj in levels) {
            repositionChildObjects(obj);
        }
    }
}