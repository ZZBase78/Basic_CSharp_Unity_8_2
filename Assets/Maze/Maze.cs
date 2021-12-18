using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class Maze
    {
        private int sizeX;
        private int sizeY;
        private GameObject parent;
        private MazePoint[,] map;

        public Maze(GameObject _parent, int _sizeX, int _sizeY)
        {
            parent = _parent;
            sizeX = _sizeX * 2 + 1;
            sizeY = _sizeY * 2 + 1;
            map = new MazePoint[sizeX, sizeY];
        }
        ///////////////////////////////////////////////
        public static float GetWorldXFromMazeX(int x)
        {
            return Settings.cellWidth / 2f * (float)x;
        }
        public static float GetWorldYFromMazeY(int y)
        {
            return Settings.cellHeight / 2f * (float)y;
        }
        private bool IsEven(int value)
        {
            return ((value % 2) == 0);
        }
        private void NewMaze()
        {
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (IsEven(x))
                    {
                        if (IsEven(y))
                        {
                            //MazeWallCross
                            map[x, y] = new MazeWallCross(parent, x, y);
                        }
                        else
                        {
                            //MazeWall, vertical
                            map[x, y] = new MazeWall(parent, x, y, true);
                        }
                    }
                    else
                    {
                        if (IsEven(y))
                        {
                            //MazeWall, horizontal
                            map[x, y] = new MazeWall(parent, x, y, false);
                        }
                        else
                        {
                            //MazeCell
                            map[x, y] = new MazeCell(parent, x, y);
                        }
                    }
                }
            }
        }

        private MazeCell GetMazeCell(int x, int y)
        {
            if ((x < 1) || (x > sizeX - 2)) return null;
            if ((y < 1) || (y > sizeY - 2)) return null;
            return map[x, y] as MazeCell;
        }

        private void AddReachableCell(List<MazeCell> reachableCells, int x, int y)
        {
            MazeCell Cell = GetMazeCell(x, y);
            if (Cell != null && Cell.isReachable)
            {
                reachableCells.Add(Cell);
            }
        }

        public void Generate()
        {
            NewMaze();

            //«аполним список клеток лабиринта которые на данном этапе €вл€ютс€ недостижимыми
            List<MazeCell> nonReachableCells = new List<MazeCell>();

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (map[x, y] is MazeCell)
                    {
                        nonReachableCells.Add(map[x, y] as MazeCell);
                    }
                }
            }

            //Ћевую нижнюю клетку будет достижимой, т.к. это точка старта
            MazeCell startCell = GetMazeCell(1, 1);
            startCell.isReachable = true;
            nonReachableCells.Remove(startCell);

            //ѕеребираем недостижимые €чейки, чтобы сделать проходы в лабиринтах
            while (nonReachableCells.Count > 0)
            {
                //ѕолучаем следующую недостижимую €чейку лабиринта
                MazeCell nextCell = nonReachableCells[Random.Range(0, nonReachableCells.Count)];

                //—писок соседних достижимых €чеек лабиринта
                List<MazeCell> reachableCells = new List<MazeCell>();
                AddReachableCell(reachableCells, nextCell.x - 2, nextCell.y);
                AddReachableCell(reachableCells, nextCell.x + 2, nextCell.y);
                AddReachableCell(reachableCells, nextCell.x, nextCell.y - 2);
                AddReachableCell(reachableCells, nextCell.x, nextCell.y + 2);

                if (reachableCells.Count > 0)
                {
                    //≈сли есть соседние достижимые точки, выберем случайную, дл€ генерации прохода
                    MazeCell nearCell = reachableCells[Random.Range(0, reachableCells.Count)];

                    //√енерируем проход, помечаем стену открытой
                    int wallX = (nextCell.x + nearCell.x) / 2;
                    int wallY = (nextCell.y + nearCell.y) / 2;
                    (map[wallX, wallY] as MazeWall).SetOpen(true);

                    //ѕомечаем €чейку достижимой
                    nextCell.isReachable = true;

                    //убераем €чейку из списка недостижимых
                    nonReachableCells.Remove(nextCell);

                }
            }
            //Debug.Log("Generate comlete");
        }

        public void Show()
        {
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    map[x, y].Show();
                }
            }
        }
    }
}
