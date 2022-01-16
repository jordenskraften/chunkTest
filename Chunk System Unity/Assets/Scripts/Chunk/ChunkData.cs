using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkData 
{
    public int x;
    public int y;
    public bool isSpawned;
    public float xPos;
    public float yPos;
    public float zPos;
    public List<TestObjectData> objectsList = new List<TestObjectData>();

    public void AddObjectToList(TestObjectData obj)
    {
        this.objectsList.Add(obj);
    }
    public void RemoveObjectFromList(TestObjectData obj)
    {
        this.objectsList.Remove(obj);
    }
}
