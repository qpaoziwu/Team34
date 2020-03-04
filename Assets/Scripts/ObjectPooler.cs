using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public enum ElementType { Platform, Enemy, Collectible }

    [Serializable]
    public struct TypeTagPair
    {
        public ElementType type;
        public string tag;
    }

    [Serializable]
    public struct PrefabPool
    {
        public GameObject[] prefabs;
    }

    [Serializable]
    public class ObjectPool
    {
        public string tag;
        public List<GameObject> objects;

        public int Populate(string _tag)
        {
            this.tag = _tag;
            this.objects = new List<GameObject>();
            GameObject[] _temp = GameObject.FindGameObjectsWithTag(this.tag);
            for (int i = 0; i < _temp.Length; i++)
            {
                if (!_temp[i].activeSelf)
                {
                    this.objects.Add(_temp[i]);
                }   
            }
            return this.objects.Count;
        }

        public GameObject Pull(GameObject[] _prefabs)
        {
            GameObject _temp = null;
            int _tempCount = this.objects.Count;
            if (_tempCount > 0)
            {
                _temp = this.objects[UnityEngine.Random.Range(0, _tempCount)];
                _temp.SetActive(true);
                this.objects.Remove(_temp);
            }
            else
            {
                    GameObject _tempPrefab = _prefabs[UnityEngine.Random.Range(0, _prefabs.Length)];
                    _temp = Instantiate(_tempPrefab, new Vector2(-100,-100), Quaternion.identity);
            }
            return _temp;
        }

        public void Drown(GameObject _object)
        {
            _object.SetActive(false);
            if (!this.objects.Contains(_object))
            {
                this.objects.Add(_object);
            }  
        }
    }

    public ObjectPool[] pools;
    public PrefabPool[] prefabs;
    public TypeTagPair[] typeTagPairs;
    private LevelController controller;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("LevelScroller").GetComponent<LevelController>();
        InitializePools();
    }
    private void InitializePools()
    {
        int numOfTypes = typeTagPairs.Length;
        pools = new ObjectPool[numOfTypes];
        Debug.Log("Initialized " + numOfTypes + " pools. Commencing Populate().");
        for (int i = 0; i < numOfTypes; i++)
        {
            pools[i] = new ObjectPool();
            Debug.Log("Pool" + i + " populated with " + pools[i].Populate(typeTagPairs[i].tag) + " GameObjects");
        }
        Debug.Log(pools.Length + " pools were initialized succesfully.");
    }

    // Object controls

    private GameObject Pull(ElementType _type)
    {
        return pools[(int)_type].Pull(prefabs[(int)_type].prefabs);
    }
    public GameObject Pull(int _type)
    {
        return pools[_type].Pull(prefabs[_type].prefabs);
    }
    public void Drown(GameObject _object)
    {
        foreach (var _pair in typeTagPairs)
        {
            if (!_object.transform.CompareTag(_pair.tag))
            {
                continue;
            }
            pools[(int)_pair.type].Drown(_object);
        }
    }
}