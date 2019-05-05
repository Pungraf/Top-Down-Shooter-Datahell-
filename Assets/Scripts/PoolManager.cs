using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Singleton
    public static PoolManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

    public void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();
        
        GameObject poolHolder = new GameObject(prefab.name + " pool");
        poolHolder.transform.parent = transform;

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<ObjectInstance>());

            for (int i = 0; i < poolSize; i++)
            {
                ObjectInstance newObject = new ObjectInstance(Instantiate(prefab, poolHolder.transform));
                poolDictionary[poolKey].Enqueue(newObject);
            }
        }
    }

    public void RespawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            ObjectInstance objectToRespawn = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToRespawn);
            
            objectToRespawn.Reuse(position, rotation);
        }
    }

    public class ObjectInstance
    {
        GameObject gameObject;
        Transform transform;

        bool hasBulletScriptComponent;
        Bullet bulletSrcipt;

        public ObjectInstance(GameObject objectInstance)
        {
            gameObject = objectInstance;
            transform = gameObject.transform;
            gameObject.SetActive(false);

            if (gameObject.GetComponent<Bullet>())
            {
                hasBulletScriptComponent = true;
                bulletSrcipt = gameObject.GetComponent<Bullet>();
            }
        }

        public void Reuse(Vector3 position, Quaternion rotation)
        {
            if (hasBulletScriptComponent)
            {
                bulletSrcipt.OnObjectReuse();
            }
            gameObject.SetActive(true);
            transform.position = position;
            transform.rotation = rotation;
        }
    }
    
    

}
