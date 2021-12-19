using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class PrefabLibrary
    {
        private GameObject[] systemPrefabs;
        private GameObject[] mazePrefabs;
        private GameObject[] BonusPrefabs;

        public PrefabLibrary(World world)
        {
            systemPrefabs = world.GetSystemPrefabs();
            mazePrefabs = world.GetMazePrefabs();
            BonusPrefabs = world.GetBonusPrefabs();
        }

        public GameObject GetSystemPrefab(int index)
        {
            return systemPrefabs[index];
        }
        public GameObject GetMazePrefab(int index)
        {
            return mazePrefabs[index];
        }
        public GameObject GetBonusPrefab(int index)
        {
            return BonusPrefabs[index];
        }
    }
}
