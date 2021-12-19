using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public abstract class MazePoint : GObject
    {
        protected int _x;
        protected int _y;
        protected GameObject parent;
        public int x { get { return _x; } }
        public int y { get { return _y; } }

        public MazePoint(GameObject _parent, int _x, int _y)
        {
            parent = _parent;
            this._x = _x;
            this._y = _y;
        }

        public abstract void Show();
    }
}
