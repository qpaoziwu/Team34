using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public enum TileType { LSideWall, RSideWall, Platform, Enemy, Collectible }

    [Serializable]
    public struct TypeTagPair {
        public TileType type;
        public string tag;
    }

    [Serializable]
    public class ObjectPool {
        public string tag;
        public GameObject[] prefabs;
        public List<GameObject> objects;

        public int Populate (string _tag) {
            this.tag = _tag;
            this.objects = new List<GameObject> ();
            GameObject[] _temp = GameObject.FindGameObjectsWithTag (this.tag);
            for (int i = 0; i < _temp.Length; i++) {
                this.objects.Add (_temp[i]);
            }
            return this.objects.Count;
        }

        public GameObject Pull (Transform _position) {
            GameObject _temp=null;
            if (this.objects.Count > 0) {
                _temp = this.objects[0];
                _temp.SetActive (true);
                this.objects.Remove (_temp);
            } else {
                if (prefabs.Length>0)
                {
                                    GameObject _tempPrefab = prefabs[UnityEngine.Random.Range(0,prefabs.Length)];
                _temp = Instantiate (_tempPrefab, _position);
                }
            }
            return _temp;
        }

        public void Drown (GameObject _object) {
            _object.SetActive (false);
            this.objects.Add (_object);
        }
    }

    public ObjectPool[] pools;
    public TypeTagPair[] typeTagPairs;
    void Start () {
        InitializePools ();
    }
    private void InitializePools () {
        int numOfTypes = typeTagPairs.Length;
        pools = new ObjectPool[numOfTypes];
        Debug.Log ("Initialized " + numOfTypes + " pools. Commencing Populate().");
        for (int i = 0; i < numOfTypes; i++) {
            pools[i] = new ObjectPool ();
            Debug.Log ("Pool" + i + " populated with " + pools[i].Populate (typeTagPairs[i].tag) + " GameObjects");
        }
        Debug.Log (pools.Length + " pools were initialized succesfully.");
    }

    // Object controls

    public GameObject Pull (TileType _type, Transform _position) {
        return pools[(int) _type].Pull (_position);
    }
    public GameObject Pull (int _type, Transform _position) {
        return pools[_type].Pull (_position);
    }
    public void Drown (GameObject _object) {
        foreach (var _pair in typeTagPairs) {
            if (!_object.transform.CompareTag (_pair.tag)) {
                continue;
            }
            pools[(int) _pair.type].Drown (_object);
        }
    }

}