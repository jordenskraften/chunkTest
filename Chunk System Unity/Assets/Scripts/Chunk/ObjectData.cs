using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    dog = 1,
    spider = 2
}

public class ObjectData  
{ 
    public int xPos;
    public int yPos;
    public int zPos;
    public ObjectType id;
}
