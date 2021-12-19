using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class BonusBehaviour : RootMonoBehaviour
    {
        public event Action<Collider> onTriggerEnter = delegate (Collider value) { };
        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnter(other);
        }
    }
}
