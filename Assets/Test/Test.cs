using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public class Test
    {
        public void Start()
        {
            Temp temp = new Temp();
        }
    }

    public class Temp
    {
        public Temp()
        {
            //Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            //Global.world.actionUpdate += DoAction;
        }
        public void DoAction()
        {
            Debug.Log("Temp.DoAction()");
        }
    }
}
