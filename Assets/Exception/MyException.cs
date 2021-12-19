using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public class MyException : Exception
    {
        public MyException(string text) : base(text)
        {
            Debug.LogError(text);
        }
    }
}
