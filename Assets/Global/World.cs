using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class World : RootMonoBehaviour
    {
        [SerializeField] private GameObject[] systemPrefabs;
        [SerializeField] private GameObject[] mazePrefabs;
        [SerializeField] private GameObject[] bonusPrefabs;
        private Core core;

        public GameObject[] GetSystemPrefabs()
        {
            return systemPrefabs;
        }
        public GameObject[] GetMazePrefabs()
        {
            return mazePrefabs;
        }
        public GameObject[] GetBonusPrefabs()
        {
            return bonusPrefabs;
        }

        private void Update()
        {
            EventManager.Update();
        }
        private void LateUpdate()
        {
            EventManager.LateUpdate();
        }
        private void FixedUpdate()
        {
            EventManager.FixedUpdate();
        }
        private void Awake()
        {
            core = new Core(this);
        }
        private void Start()
        {
            core.Start();
        }
        private void OnDestroy()
        {
            core.OnDestroy();
        }
    }
}
