using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class MazeWall : MazePoint
    {
        private bool isVertical;
        private bool isOpen;

        public MazeWall(GameObject _parent, int new_x, int new_y, bool new_vertical) : base(_parent, new_x, new_y)
        {
            isVertical = new_vertical;
            isOpen = false;
        }
        public bool GetOpen()
        {
            return isOpen;
        }
        public void SetOpen(bool value)
        {
            isOpen = value;
        }

        public override void Show()
        {
            if (!isOpen)
            {
                Vector3 position = new Vector3(Maze.GetWorldXFromMazeX(x), 0, Maze.GetWorldYFromMazeY(y));
                gameObject = GameObject.Instantiate(PrefabLibrary.GetMazePrefab(1), position, Quaternion.identity);
                if (isVertical)
                {
                    gameObject.transform.localScale = new Vector3(Settings.wallThickness, 1f, Settings.cellHeight);
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(Settings.cellWidth, 1f, Settings.wallThickness);
                }
                gameObject.transform.parent = parent.transform;
            }
        }
    }

}
