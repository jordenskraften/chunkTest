using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPosition
{
    public int x;
    public int y;
    public ChunkPosition(int _x, int _y)
    {
        x = _x;
        y = _y;
    } 
}
public class NeighboringChunkPositions
{ 
    public ChunkPosition[] positions = new ChunkPosition[9];
    public NeighboringChunkPositions(int x, int y)
    {
        positions[0] = new ChunkPosition(x,y);
        
        positions[1] = new ChunkPosition(x,y+1);
        positions[2] = new ChunkPosition(x+1,y+1);
        positions[3] = new ChunkPosition(x+1,y);
        positions[4] = new ChunkPosition(x+1,y-1);
        positions[5] = new ChunkPosition(x,y-1);
        positions[6] = new ChunkPosition(x-1,y-1);
        positions[7] = new ChunkPosition(x-1,y);
        positions[8] = new ChunkPosition(x-1,y+1);
    }

}
public class PlayerData : MonoBehaviour
{
    public int x;
    public int y; 
    public testChunkSpawner chunkSpawner;

    // Update is called once per frame
    void FixedUpdate()
    {
        //получаем текущий чанк на котором стоим
        float xPos = transform.position.x;
        float yPos = transform.position.z;
        x = (int)xPos / chunkSpawner.width;
        y = (int)yPos / chunkSpawner.height;
        //спавним соседние чанки
        //деспавним остальные чанки
        NeighboringChunkPositions chunkNeighbors = new NeighboringChunkPositions(x,y);  
        chunkSpawner.RefreshPlayerArea(chunkNeighbors);

    }
}
