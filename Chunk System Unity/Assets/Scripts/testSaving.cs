using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TestObjectData
{
    public int id;
    public float x;
    public float y;
    public float z;

    public TestObjectData(int _id, int _x, int _y, int _z)
    {
        id = _id;
        x = _x;
        y = _y;
        z = _z;
    }
}

[System.Serializable]
public class TestChunkData
{
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

[System.Serializable]
public class TestSubsceneData
{
    public List<TestChunkData> chunkList = new List<TestChunkData>();

    public void AddChunkToList(TestChunkData chunk)
    {
        this.chunkList.Add(chunk);
    }
    public void RemoveChunkFromList(TestChunkData chunk)
    {
        this.chunkList.Remove(chunk);
    }
}

[System.Serializable]
public class TestRealmData
{
    public List<TestSubsceneData> subsceneList = new List<TestSubsceneData>();

    public void AddSubsceneToList(TestSubsceneData subscene)
    {
        this.subsceneList.Add(subscene);
    }
    public void RemoveSubsceneFromList(TestSubsceneData subscene)
    {
        this.subsceneList.Remove(subscene);
    } 
}

[System.Serializable]
public class TestGameSaveData
{
    public TestRealmData earthRealm = new TestRealmData();
    public TestRealmData downEarthRealm = new TestRealmData();
}

[System.Serializable]
public class testSaving : MonoBehaviour
{ 
    public GameObject testPrefab;
    void Start()
    {
        
        TestGameSaveData save1 = new TestGameSaveData();

        //создали объекты
        TestObjectData object1 = new TestObjectData(0, 0, 0, 0);
        TestObjectData object2 = new TestObjectData(1, 1, 0, 0);
        TestObjectData object3 = new TestObjectData(2, 2, 0, 0);

        //создали чанк и добавили в него объекты
        TestChunkData chunk1 = new TestChunkData();
        chunk1.AddObjectToList(object1);
        chunk1.AddObjectToList(object2);
        chunk1.AddObjectToList(object3);
        
        //добавили в сабсцену чанк
        TestSubsceneData subscene1 = new TestSubsceneData();
        subscene1.AddChunkToList(chunk1);

        save1.earthRealm.AddSubsceneToList(subscene1);
        Debug.Log("World created");

        //теперь это говно надо записать в файл json к примеру 
        string jsonSavedFile = JsonUtility.ToJson(save1);
        Debug.Log("World saved to json");


        //для проверки конвертируем json в с# код
        TestGameSaveData loadedObject = JsonUtility.FromJson<TestGameSaveData>(jsonSavedFile);
        Debug.Log("World loaded from json");
 
        //теперь из загруженого объекта трайнем заспавнить объекты
        foreach ( TestSubsceneData subscene in loadedObject.earthRealm.subsceneList)
        {
            foreach (TestChunkData chunk in subscene.chunkList)
            {
                foreach (TestObjectData obje in chunk.objectsList)
                {
                    Debug.Log(obje.id);
                    Instantiate(testPrefab, new Vector3(obje.x, obje.y, obje.z), Quaternion.identity);
                }
            }
        } 

        Debug.Log("Spawn is over");

    } 
}
