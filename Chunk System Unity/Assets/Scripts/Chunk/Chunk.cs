using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public ChunkData chunkData;
    public int x;
    public int y;
    public bool isExist; 
    public Vector3 chunkPos;
    public List<ObjectData> objects;

    //тут крч надо загружать из файла дату чанка по его иксу и игреку 
}
