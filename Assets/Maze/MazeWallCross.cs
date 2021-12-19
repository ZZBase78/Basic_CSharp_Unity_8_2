using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class MazeWallCross : MazePoint
    {
        public MazeWallCross(GameObject _parent, int new_x, int new_y) : base(_parent, new_x, new_y)
        {

        }

        public override void Show()
        {
            Vector3 position = new Vector3(Maze.GetWorldXFromMazeX(x), 0, Maze.GetWorldYFromMazeY(y));
            gameObject = GameObject.Instantiate(PrefabLibrary.GetMazePrefab(2), position, Quaternion.identity);
            gameObject.transform.localScale = new Vector3(Settings.wallThickness * 2f, 1.1f, Settings.wallThickness * 2f);
            gameObject.transform.parent = parent.transform;
        }
    }

}
