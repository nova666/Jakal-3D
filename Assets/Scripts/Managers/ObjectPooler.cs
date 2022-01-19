﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ArmyOfOne
{
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Poll
        {
            public string tag;
            public GameObject prefab;
            public int size;

        }
        public Dictionary<string, Queue<GameObject>> poolDictionary;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

