/// Michael Thomas
/// 16.02.19

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolScript : MonoBehaviour
{

    //Object that has been selected to spawn
    private GameObject SelectedObject;
    private Vector3 StartPosition;

    //Pools of objects
    public List<GameObject> poolObjects;

    //Called by another script to set the SelectedObject, this will call another function to start the checks to spawn the object
    public void SetObject(GameObject _Object, Vector3 startPosition)
    {
        SelectedObject = _Object;
        StartPosition = startPosition;

        CheckObjectPool();
    }

    public void EmptyArray()
    {
        poolObjects.Clear();
    }

    //Function that checks to see if there are any objects in the pool that can be used, if not it will create a new one
    private void CheckObjectPool()
    {
        int x = 0;
        bool foundObject = false;

        foreach (GameObject _object in poolObjects)
        {
            //if we found the right object and it's inactive, then we can use it!
            if (_object != null && _object.tag == SelectedObject.tag && _object.activeInHierarchy == false)
            {
                ActivateObject(x);
                foundObject = true;
                break;
            }
            x++;
        }

        //If there are no avaliable objects in the pool then we create a new one and activate it
        if (!foundObject)
        {
            CreateNewObject();
        }
    }

    //Activates the object, int _x being the index to use to find the object in the poolObjects list
    private void ActivateObject(int _x)
    {
        //find the object and set it true
        poolObjects[_x].SetActive(true);
        SetPositionObject(_x);
    }

    //Sets the position of the desired object, int x being the index used to find it
    private void SetPositionObject(int x)
    {
        poolObjects[x].transform.position = new Vector3(StartPosition.x, poolObjects[x].transform.position.y, StartPosition.z);
    }

    private void CreateNewObject()
    {
        //Add new object to the inactivepoolobjects
        poolObjects.Add(Instantiate(SelectedObject, transform));
        SetPositionObject(poolObjects.Count - 1);
    }
}
