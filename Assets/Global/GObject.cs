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
        protected GameObject gameObject;
        protected GameObjectFactory gameObjectFactory;

        
        public GameObject GetGameObject()
        {
            return gameObject;
        }
        public virtual void Dispose()
        {
            gameObjectFactory.Destroy(gameObject);
        }
    }
}
