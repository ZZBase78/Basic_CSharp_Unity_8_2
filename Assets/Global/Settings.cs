using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class Settings
    {
        public int mazeWidth { get; }
        public int mazeHeight { get; }
        public float cellWidth { get; }
        public float cellHeight { get; }
        public float wallThickness { get; }
        public int maxScore { get; }

        public Settings() 
        {
            mazeWidth = 10;
            mazeHeight = 10;
            cellWidth = 3;
            cellHeight = 3;
            wallThickness = 0.2f;
            maxScore = 200;
        }
    }
}
