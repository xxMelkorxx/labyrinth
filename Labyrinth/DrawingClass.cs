using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Labyrinth
{
	class DrawingClass
	{
		private readonly PictureBox pictureBox;
		private readonly Graphics graphics;
		private readonly Bitmap bitmap;

		private double wnd_Xmin, wnd_Xmax, wnd_Ymin, wnd_Ymax;
		private int nx, ny;
		private double alpha, beta;
		private double dx, dy;

		public DrawingClass(PictureBox pB, double x1, double y1, double x2, double y2)
		{
			pictureBox = pB;
			bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
			graphics = Graphics.FromImage(bitmap);

			SetGrades(20, 20);
			wnd_Xmin = x1; wnd_Xmax = x2;
			wnd_Ymin = y1; wnd_Ymax = y2;
			dx = (wnd_Xmax - wnd_Xmin) / nx;
			dy = (wnd_Ymax - wnd_Ymin) / ny;
			alpha = pictureBox.Width / (wnd_Xmax - wnd_Xmin);
			beta = pictureBox.Height / (wnd_Ymax - wnd_Ymin);

			pictureBox.Image = bitmap;
		}

		/// <summary>
		/// Очищает область.
		/// </summary>
		public void Clear()
		{
			graphics.Clear(Color.White);
		}

		/// <summary>
		/// Настраивает выравние сетки.
		/// </summary>
		/// <param name="inx"></param>
		/// <param name="iny"></param>
		public void SetGrades(int inx, int iny)
		{
			nx = inx; ny = iny;
			dx = (wnd_Xmax - wnd_Xmin) / nx;
			dy = (wnd_Ymax - wnd_Ymin) / ny;
		}

		#region COORDINATE_TRANSFORMATIONS
		/// <summary>
		/// Преобразует мировые координаты в координаты окна (пиксели).
		/// </summary>
		/// <param name="x">X в мировых координатах.</param>
		/// <returns>Координата, преобразованная в координаты окна (пиксели).</returns>
		public int OutX(double x)
		{
			return (int)Math.Floor((x - wnd_Xmin) * alpha);
		}

		/// <summary>
		/// Преобразует мировые координаты в координаты окна (пиксели).
		/// </summary>
		/// <param name="y">Y мировых координатах.s</param>
		/// <returns>Координата, преобразованная в координаты окна (пиксели).</returns>
		public int OutY(double y, bool size = false)
		{
			if (size)
				return (int)Math.Floor((y - wnd_Ymin) * beta);
			else
				return (int)Math.Floor(pictureBox.Height - (y - wnd_Ymin) * beta);
		}

		/// <summary>
		/// Преобразует координаты окна (пиксели) в мировые координаты.
		/// </summary>
		/// <param name="x">X-координата в пикселях.</param>
		/// <returns>Координата, преобразованная в мировые координаты.</returns>
		public double InX(int x)
		{
			return (x / alpha) + wnd_Xmin;
		}

		/// <summary>
		/// Преобразует координаты окна (пиксели) в мировые координаты.
		/// </summary>
		/// <param name="y">Y-координата в пикселях.</param>
		/// <returns>Координата, преобразованная в мировые координаты.</returns>
		public double InY(int y)
		{
			return -(y / beta) + wnd_Ymax;
		}
		#endregion

		/// <summary>
		/// Рисует прямую линию между двумя заданными точками.
		/// </summary>
		/// <param name="color">Цвет линии.</param>
		/// <param name="x1">X-положение начальной точки в мировых координатах.</param>
		/// <param name="y1">Y-положение начальной точки в мировых координатах.</param>
		/// <param name="x2">X-положение конечной точки в мировых координатах.</param>
		/// <param name="y2">Y-положение конечной точки в мировых координатах.</param>
		public void DrawLine(Color color, double x1, double y1, double x2, double y2)
		{
			Pen pen = new Pen(color)
			{
				Width = 1f,
				DashStyle = DashStyle.Solid
			};
			graphics.DrawLine(pen, OutX(x1), OutY(y1), OutX(x2), OutY(y2));
		}

		/// <summary>
		/// Рисует прямоугольник.
		/// </summary>
		/// <param name="color">Цвет заливки.</param>
		/// <param name="x">X-положение начальной точки в мировых координатах.</param>
		/// <param name="y">Y-положение начальной точки в мировых координатах.</param>
		/// <param name="width">Ширина прямоугольника в мировых координатах.</param>
		/// <param name="height">Высота прямоугольника в мировых координатах.</param>
		public void DrawColoredRectangle(Color color, double x, double y, double width, double height)
		{
			SolidBrush brush = new SolidBrush(color);
			graphics.FillRectangle(brush, OutX(x) - OutX(width) / 2, OutY(y) - OutY(height, true) / 2, OutX(width) + 1, OutY(height, true) + 1);
		}

		/// <summary>
		/// Рисует ячейку лабиринта.
		/// </summary>
		/// <param name="cell">Ячейка.</param>
		/// <param name="row">Номер ряда.</param>
		/// <param name="column">Номер столбца.</param>
		public void DrawCell(byte cell, int row, int column)
		{
			switch (cell & 0x30)
			{
				case 0x00:
					DrawColoredRectangle(Color.White, 1 + (column + 0.5) * dx, 1 + (row + 0.5) * dy, dx, dy);
					break;
				case 0x10:
					DrawColoredRectangle(Color.Gold, 1 + (column + 0.5) * dx, 1 + (row + 0.5) * dy, dx, dy);
					break;
				case 0x20:
					DrawColoredRectangle(Color.Green, 1 + (column + 0.5) * dx, 1 + (row + 0.5) * dy, dx, dy);
					break;
				case 0x30:
					DrawColoredRectangle(Color.Red, 1 + (column + 0.5) * dx, 1 + (row + 0.5) * dy, dx, dy);
					break;
			}

			// Отрисовка стенок.
			if ((cell & 0x01) == 0x01)     // Правая стенка.
				DrawLine(Color.Black, 1 + (column + 1) * dx, 1 + row * dy, 1 + (column + 1) * dx, 1 + (row + 1) * dy);
			if ((cell & 0x02) == 0x02)     // Левая стенка.
				DrawLine(Color.Black, 1 + column * dx, 1 + row * dy, 1 + column * dx, 1 + (row + 1) * dy);
			if ((cell & 0x04) == 0x04)     // Нижняя стенка.
				DrawLine(Color.Black, 1 + column * dx, 1 + row * dy, 1 + (column + 1) * dx, 1 + row * dy);
			if ((cell & 0x08) == 0x08)     // Верхняя стенка.
				DrawLine(Color.Black, 1 + column * dx, 1 + (row + 1) * dy, 1 + (column + 1) * dx, 1 + (row + 1) * dy);
		}
		/// <summary>
		/// Рисует ячейку лабиринта (использует стрелку для указания направления).
		/// </summary>
		/// <param name="cell">Ячейка.</param>
		/// <param name="direction">Направление.</param>
		/// <param name="row">Номер ряда.</param>
		/// <param name="column">Номер столбца.</param>
		public void DrawCell(byte cell, int direction, int row, int column)
		{
			switch (cell &= 0x30)
			{
				case 0x00:
					DrawColoredRectangle(Color.White, 1 + (column + 0.5) * dx, 1 + (row + 0.5) * dy, dx, dy);
					break;
				case 0x10:
					DrawColoredRectangle(Color.Green, 1 + (column + 0.5) * dx, 1 + (row + 0.5) * dy, dx, dy);
					break;
				case 0x20:
					DrawColoredRectangle(Color.Gold, 1 + (column + 0.5) * dx, 1 + (row + 0.5) * dy, dx, dy);
					break;
				case 0x30:
					DrawColoredRectangle(Color.Red, 1 + (column + 0.5) * dx, 1 + (row + 0.5) * dy, dx, dy);
					break;
			}

			// Отрисовка стенок.
			if ((cell &= 0x01) == 0x01)     // Правая стенка.
				DrawLine(Color.Black,
					1 + (column + 1) * dx,
					1 + row * dy,
					1 + (column + 1) * dx,
					1 + (row + 1) * dy
					);
			if ((cell &= 0x02) == 0x02)     // Левая стенка.
				DrawLine(Color.Black,
					1 + column * dx,
					1 + row * dy,
					1 + column * dx,
					1 + (row + 1) * dy
					);
			if ((cell &= 0x04) == 0x04)     // Нижняя стенка.
				DrawLine(Color.Black,
					1 + column * dx,
					1 + row * dy,
					1 + (column + 1) * dx,
					1 + row * dy
					);
			if ((cell &= 0x08) == 0x08)     // Верхняя стенка.
				DrawLine(Color.Black,
					1 + column * dx,
					1 + (row + 1) * dy,
					1 + (column + 1) * dx,
					1 + (row + 1) * dy
					);
		}

		/// <summary>
		/// Рисует лабиринт.
		/// </summary>
		/// <param name="Labyrinth">Массив с информацией о лабиринте.</param>
		/// <param name="directions">Массив с информацией об направлениях.</param>
		public void DrawLabyrinth(byte[,] Labyrinth, byte[,] directions = null)
		{
			for (int i = 0; i < (nx - 2); i++)
				for (int j = 0; j < (ny - 2); j++)
				{
					if (directions == null)
						DrawCell(Labyrinth[i, j], nx - 3 - i, j);
					else
						DrawCell(Labyrinth[i, j], directions[i, j], nx - 3 - i, j);
				}
		}

	}
}