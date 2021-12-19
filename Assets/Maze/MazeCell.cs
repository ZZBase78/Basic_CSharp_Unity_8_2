using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class MazeCell : MazePoint
    {
        public bool isReachable;
        public MazeCell(GameObject _parent, int new_x, int new_y) : base(_parent, new_x, new_y)
        {
            isReachable = false;
        }
        public override void Show()
        {
            Vector3 position = new Vector3(Maze.GetWorldXFromMazeX(x), 0, Maze.GetWorldYFromMazeY(y));
            gameObject = GameObject.Instantiate(PrefabLibrary.GetMazePrefab(0), position, Quaternion.identity);
            gameObject.transform.localScale = new Vector3(Settings.cellWidth, 1f, Settings.cellHeight);
            gameObject.transform.parent = parent.transform;
        }
    }

}
