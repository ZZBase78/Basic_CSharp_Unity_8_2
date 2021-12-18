using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public static class GameObjectFactory
    {
        public static void Init()
        {

        }
        public static GameObject Instantiate(GameObject gameObject, Vector3 position, Quaternion rotation)
        {
            return GameObject.Instantiate(gameObject, position, rotation);
        }
        public static GameObject Instantiate(GameObject gameObject, Vector3 position)
        {
            return GameObject.Instantiate(gameObject, position, gameObject.transform.rotation);
        }
        public static GameObject Instantiate(GameObject gameObject)
        {
            return GameObject.Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
        }
        public static GameObject InstantiateEmpty(string name)
        {
            return new GameObject(name);
        }
        public static void Destroy(GameObject gameObject)
        {
            GameObject.Destroy(gameObject);
            gameObject = null;
        }

    }
}
