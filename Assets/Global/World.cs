using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class World : RootMonoBehaviour
    {
        private Core core;
        private EventManager eventManager;

        private void Update()
        {
            eventManager.Update();
        }
        private void LateUpdate()
        {
            eventManager.LateUpdate();
        }
        private void FixedUpdate()
        {
            eventManager.FixedUpdate();
        }
        private void Awake()
        {
            core = new Core();
            eventManager = core.GetEventManager();
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
