using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Labyrinth
{
	public partial class MainForm : Form
	{
		private int N;
		private int[] startCell, finishCell;
		private byte[,] labyrinth, labyrinthPass;
		private Random rnd;
		private DrawingClass draw;

		public MainForm()
		{
			InitializeComponent();
		}

		private void OnLoadMainForm(object sender, EventArgs e)
		{
			rnd = new Random(DateTime.Now.Millisecond);
			N = (int)numUpDown_sizeLabyrinth.Value;
		}

		private void OnClickButtonGeneratedLabyrinth(object sender, EventArgs e)
		{
			N = (int)numUpDown_sizeLabyrinth.Value;
			Size pbSize = pictureBox_labyrinth.Size;
			if (N > 125)
				pictureBox_labyrinth.Size = new Size(N * 3, N * 3);
			else pictureBox_labyrinth.Size = pbSize;

			Drawing();
			button_generatedLabyrinth.Enabled = false;
			button_passLabyrinth.Enabled = false;

			// Генерация лабиринта.
			Task taskGenLab = Task.Factory.StartNew(() =>
			{
				labyrinth = Labyrinth.GeneratedLabyrinth(N, rnd);
				startCell = Labyrinth.GetStartCell(N, rnd);
				finishCell = Labyrinth.GetStartCell(N, rnd);

				draw.DrawLabyrinth(labyrinth);

				Application.DoEvents();
			});

			while (!taskGenLab.IsCompleted)
				Application.DoEvents();

			if (taskGenLab.IsCompleted)
			{
				button_generatedLabyrinth.Enabled = true;
				button_passLabyrinth.Enabled = true;
				button_saveImage.Enabled = true;
				pictureBox_labyrinth.Refresh();
			}
		}

		private void OnClickButtonPassLabyrinth(object sender, EventArgs e)
		{
			Drawing();
			button_generatedLabyrinth.Enabled = false;
			button_passLabyrinth.Enabled = false;

			// Генерация лабиринта.
			var taskGenLab = Task.Factory.StartNew(() =>
			{
				// Поиск пути лабиринта.
				if (checkBox_fixedDots.Checked)
					labyrinthPass = Labyrinth.PassLabyrinth(labyrinth, rnd, ref startCell, ref finishCell, out _, false);
				else labyrinthPass = Labyrinth.PassLabyrinth(labyrinth, rnd, ref startCell, ref finishCell, out _);
				draw.DrawLabyrinth(labyrinthPass);

				Application.DoEvents();
			});

			while (!taskGenLab.IsCompleted)
				Application.DoEvents();

			if (taskGenLab.IsCompleted)
			{
				button_passLabyrinth.Enabled = true;
				button_generatedLabyrinth.Enabled = true;
				button_saveImage.Enabled = true;
				pictureBox_labyrinth.Refresh();
			}
		}

		private void OnClickButtonSaveImage(object sender, EventArgs e)
		{
			if (pictureBox_labyrinth.Image != null)
			{
				SaveFileDialog saveDialog = new SaveFileDialog
				{
					Title = "Сохранить картинку как...",
					OverwritePrompt = true,
					CheckPathExists = true,
					ShowHelp = true,
					Filter = "Image Files(*.PNG)|*.PNG|All files (*.*)|*.*"
				};
				if (saveDialog.ShowDialog() == DialogResult.OK)
					try
					{
						Bitmap bmp = new Bitmap(pictureBox_labyrinth.Image);
						bmp.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Ошибка",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
			}
		}

		private void Drawing()
		{
			draw = new DrawingClass(pictureBox_labyrinth, 0, 0, N + 2, N + 2);
			draw.SetGrades(N + 2, N + 2);
			draw.Clear();
		}
	}
}
