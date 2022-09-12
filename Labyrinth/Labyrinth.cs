using System;
using System.Collections.Generic;

namespace Labyrinth
{
    class Labyrinth
    {
        /// <summary>
        /// Метод случайного выбора направления (1 - вправо, 2 - влево, 3 - вниз, 4 - вверх, 0 - нет направления).
        /// </summary>
        /// <param name="cells">Матрица ячеек</param>
        /// <param name="row">Номер строки ячейки, от которой выбирается направление.</param>
        /// <param name="column">Номер столбца ячейки, от которой выбирается направление.</param>
        /// <param name="rnd"></param>
        /// <param name="building"></param>
        /// <returns></returns>
        public static int GetDirection(byte[,] cells, int row, int column, Random rnd, bool building = true)
        {
            int n = cells.GetLength(0);
            List<int> result = new List<int>();
            if (building)
            {
                if (column != (n - 1) && (cells[row, column + 1] & 0x80) != 0x80)     // Вправо
                    result.Add(1);
                if (column != 0 && (cells[row, column - 1] & 0x80) != 0x80)           // Влево
                    result.Add(2);
                if (row != (n - 1) && (cells[row + 1, column] & 0x80) != 0x80)        // Вниз
                    result.Add(3);
                if (row != 0 && (cells[row - 1, column] & 0x80) != 0x80)              // Вверх
                    result.Add(4);
            }
            else
            {
                if (column != (n - 1) && (cells[row, column] & 0x01) != 0x01 && (cells[row, column + 1] & 0x80) != 0x80)  // Вправо
                    result.Add(1);
                if (column != 0 && (cells[row, column] & 0x02) != 0x02 && (cells[row, column - 1] & 0x80) != 0x80)        // Влево
                    result.Add(2);
                if (row != (n - 1) && (cells[row, column] & 0x04) != 0x04 && (cells[row + 1, column] & 0x80) != 0x80)     // Вниз
                    result.Add(3);
                if (row != 0 && (cells[row, column] & 0x08) != 0x08 && (cells[row - 1, column] & 0x80) != 0x80)           // Вверх
                    result.Add(4);
            }

            int[] res = result.ToArray();
            if (res.Length == 0)
                return 0;

            return res[rnd.Next(0, res.Length)];
        }
        /// <summary>
        /// Метод случайного выбора стартовой ячейки.
        /// </summary>
        /// <param name="n">Размер матрицы ячеек.</param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static int[] GetStartCell(int n, Random rnd)
        {
            int[] startCell = new int[3];
            int rand = (rnd.Next(0, n));
            int side = (rnd.Next(1, 5));
            switch (side)
            {
                case 1:
                    startCell[0] = 0;
                    startCell[1] = rand;
                    break;
                case 2:
                    startCell[0] = n - 1;
                    startCell[1] = rand;
                    break;
                case 3:
                    startCell[0] = rand;
                    startCell[1] = 0;
                    break;
                case 4:
                    startCell[0] = rand;
                    startCell[1] = n - 1;
                    break;
            }
            startCell[2] = side;
            return startCell;
        }
        /// <summary>
        /// Метод генерации лабиринта.
        /// </summary>
        /// <remarks> Число 0x0F(15) - 00001111, где:
        /// ххххххх1 - наличие стенки справо;
        /// xxxxxx1x - наличие стенки слева;
        /// xxxxx1xx - наличие стенки снизу;
        /// xxxx1xxx - наличие стенки сверху.
        /// </remarks>
        /// <param name="size"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static byte[,] GeneratedLabyrinth(int size, Random rnd)
        {
            var cells = new byte[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    cells[i, j] = 0x0F;


            var startCell = GetStartCell(size, rnd);
            var row = startCell[0];
            var column = startCell[1];
            var cellsStack = new Stack<int>();

            int newDirection;
            while (true)
            {
                cells[row, column] |= 0x80;   // 0xxxxxxx -> 1xxxxxxx - старший разряд определяет посещённость клетки.
                newDirection = GetDirection(cells, row, column, rnd);
                switch (newDirection)
                {
                    case 1:
                        cellsStack.Push(1);
                        cells[row, column] ^= 0x01;
                        column++;
                        cells[row, column] ^= 0x02;
                        break;
                    case 2:
                        cellsStack.Push(2);
                        cells[row, column] ^= 0x02;
                        column--;
                        cells[row, column] ^= 0x01;
                        break;
                    case 3:
                        cellsStack.Push(3);
                        cells[row, column] ^= 0x04;
                        row++;
                        cells[row, column] ^= 0x08;
                        break;
                    case 4:
                        cellsStack.Push(4);
                        cells[row, column] ^= 0x08;
                        row--;
                        cells[row, column] ^= 0x04;
                        break;
                    default:
                        while (newDirection == 0)
                        {
                            int backward = cellsStack.Pop();
                            switch (backward)
                            {
                                case 1: column--; break;
                                case 2: column++; break;
                                case 3: row--; break;
                                case 4: row++; break;
                            }
                            if (row == startCell[0] && column == startCell[1])
                                return cells;
                            newDirection = GetDirection(cells, row, column, rnd);
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// Метод прохождения лабиринта.
        /// </summary>
        /// <remarks>
        /// xx00xxxx - белый цвет клетки (нетронутая клетка);
        /// xx01xxxx - жёлтый цвет клетки(старт/финиш);
        /// xx10xxxx - зелёный цвет клетки(верный путь);
        /// xx11xxxx - красный цвет клетки(неверный путь).
        /// </remarks>
        /// <param name="labyrinth">Матрица готового лабиринта</param>
        /// <param name="rnd"></param>
        /// <param name="startCell">Стартовая ячека</param>
        /// <param name="finishCell">Финишная ячейка</param>
        /// <param name="unFixedDots">Фиксированный старт</param>
        public static byte[,] PassLabyrinth(byte[,] labyrinth, Random rnd, ref int[] startCell, ref int[] finishCell, out int[,] directions, bool unFixedDots = true)
        {
            var size = labyrinth.GetLength(0);
            var cells = new byte[size, size];
            var cellsStack = new Stack<int>();
            directions = new int[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    cells[i, j] = labyrinth[i, j];
                    cells[i, j] ^= 0x80;
                }

            if (unFixedDots)
            {
                startCell = GetStartCell(size, rnd);
                finishCell = GetStartCell(size, rnd);
            }

            int row = startCell[0];
            int column = startCell[1];

            cells[startCell[0], startCell[1]] ^= 0x10;

            int newDirection;
            while (true)
            {
                if (row == finishCell[0] && column == finishCell[1])
                {
                    cells[row, column] |= 0x30;
                    cells[row, column] ^= 0x20;
                    cells[startCell[0], startCell[1]] |= 0x30;
                    cells[startCell[0], startCell[1]] ^= 0x20;
                    return cells;
                }

                cells[row, column] |= 0x80;
                newDirection = GetDirection(cells, row, column, rnd, false);
                directions[row, column] = newDirection;
                switch (newDirection)
                {
                    case 1:
                        cellsStack.Push(1);
                        column++;
                        break;
                    case 2:
                        cellsStack.Push(2);
                        column--;
                        break;
                    case 3:
                        cellsStack.Push(3);
                        row++;
                        break;
                    case 4:
                        cellsStack.Push(4);
                        row--;
                        break;
                    default:
                        while (newDirection == 0)
                        {
                            cells[row, column] |= 0x30;
                            int backward = cellsStack.Pop();
                            directions[row, column] = backward;
                            switch (backward)
                            {
                                case 1: column--; break;
                                case 2: column++; break;
                                case 3: row--; break;
                                case 4: row++; break;
                            }
                            newDirection = GetDirection(cells, row, column, rnd, false);
                        }
                        break;
                }
                cells[row, column] |= 0x30;
                cells[row, column] ^= 0x10;
            }
        }
    }
}