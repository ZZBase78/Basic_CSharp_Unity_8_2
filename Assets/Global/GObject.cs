using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZZBase.Maze
{
    /// <summary>
    /// �������� ����� ��� ���� �������� �����
    /// </summary>
    public class GObject : IDisposable
    {
        public GameObject gameObject;
        protected GameObjectFactory gameObjectFactory;

        
        public GameObject GetGameObject()
        {
            return gameObject;
        }
        public virtual void Dispose()
        {
            gameObjectFactory.Destroy(gameObject);
            //GameObject.Destroy(gameObject);
            gameObject = null;
        }
    }
}
