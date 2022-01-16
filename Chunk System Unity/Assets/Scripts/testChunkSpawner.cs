using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testChunkSpawner : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject chunkPrefab;
    public int width = 10, height = 10;
    public List<ChunkData> chunks = new List<ChunkData>();
    public List<Chunk> spawnedChunks = new List<Chunk>();
    public List<Chunk> outAreaChunks = new List<Chunk>();
    
    void Awake()
    {
        playerData.chunkSpawner = this; 
    }
    public void RefreshPlayerArea(NeighboringChunkPositions nP)
    { 
        ChunkPosition[] neighborsPositions = nP.positions;
        foreach (ChunkPosition pos in neighborsPositions)
        {
            EnableChunk(pos.x, pos.y);
        }
        foreach (Chunk ch in spawnedChunks)
        {
            bool inArea = false; 
            foreach (ChunkPosition ps in neighborsPositions)
            {
                if ((ch.x == ps.x) && (ch.y == ps.y))
                {
                    inArea = true;
                }
            }
            if (!inArea)
                outAreaChunks.Add(ch);
        }
        //чистим списки 
        foreach (Chunk chu in outAreaChunks)
        {
            spawnedChunks.Remove(chu);
            DespawnChunk(chu);
        } 
        outAreaChunks.Clear();

    }
    public void EnableChunk(int x, int y)
    { 
        bool isExist = false;
        ChunkData bufferChunk = null; 
        if (chunks.Count >= 1)
        {
            foreach (ChunkData chunk in chunks)
            {
                if ((chunk.x == x) && (chunk.y == y))
                {
                    isExist = true; 
                    bufferChunk = chunk;
                }
                if (isExist) break;
            }
        }
        if (isExist == true)
        {
            SpawnChunk(bufferChunk);
        }
        else
        {
            SpawnChunk(CreateChunk(x, y));
        }
    } 
    ChunkData CreateChunk(int x, int y)
    { 
        ChunkData newChunkData = new ChunkData();
        newChunkData.x = x;
        newChunkData.y = y; 
        newChunkData.isSpawned = false;
        newChunkData.xPos = x * width;
        newChunkData.zPos = y * height;
        newChunkData.yPos = 0f;
        chunks.Add(newChunkData);
        return newChunkData;
    }
    
    void SpawnChunk(ChunkData chunkData)
    {
        if (!chunkData.isSpawned)
        {
            GameObject spawnedChunk = Instantiate(chunkPrefab, 
                        new Vector3(chunkData.xPos, chunkData.yPos, chunkData.zPos),
                        Quaternion.identity);
            Chunk chunkComponent = spawnedChunk.GetComponent<Chunk>();
            if (chunkComponent != null)
            {
                chunkComponent.x = chunkData.x;
                chunkComponent.y = chunkData.y;
                chunkComponent.chunkData = chunkData;
                spawnedChunks.Add(chunkComponent);
            } 
            chunkData.isSpawned = true;
        }
    }
    void DespawnChunk(Chunk bufferChunk)
    {  
        if (bufferChunk != null)
        { 
            bufferChunk.chunkData.isSpawned = false;  
            Destroy(bufferChunk.gameObject, 0f);  
        }  
    }

}
