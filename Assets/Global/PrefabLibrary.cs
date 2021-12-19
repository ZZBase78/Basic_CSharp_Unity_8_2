using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public static class PrefabLibrary
    {
        private static GameObject[] systemPrefabs;
        private static GameObject[] mazePrefabs;
        private static GameObject[] BonusPrefabs;

        public static void Init(World world)
        {
            systemPrefabs = world.GetSystemPrefabs();
            mazePrefabs = world.GetMazePrefabs();
            BonusPrefabs = world.GetBonusPrefabs();
        }

        public static GameObject GetSystemPrefab(int index)
        {
            return systemPrefabs[index];
        }
        public static GameObject GetMazePrefab(int index)
        {
            return mazePrefabs[index];
        }
        public static GameObject GetBonusPrefab(int index)
        {
            return BonusPrefabs[index];
        }
    }
}
