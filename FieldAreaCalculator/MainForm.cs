using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using faclib;

namespace FieldAreaCalculator
{
	public partial class MainForm : Form
	{
		ucFieldViewer FieldViewer;

		static string poly_draw_info = "Клики раставляют точки. Двойной клик замыкает фигуру.";
		static string circle_draw_info = "Первый клик устанавливает центр, движение мыши указывает радиус, следующий клик завершиет рисование окружности.";
		public MainForm()
		{
			InitializeComponent();

			labelResult.Text = "";
			labelInfo.Text = poly_draw_info;

			FieldViewer = new ucFieldViewer(OnBeginDraw, OnEndDraw);
			FieldViewer.BorderStyle = BorderStyle.FixedSingle;
			FieldViewer.Location = new Point(5, 50);
			FieldViewer.Size = new Size(640, 480);

			Controls.Add(FieldViewer);

			FieldViewer.Image = new Bitmap(FieldViewer.Width, FieldViewer.Height);
		}

		private void cbIsPolygon_CheckStateChanged(object sender, EventArgs e)
		{
			if(cbIsPolygon.Checked)
			{
				cbIsPolygon.Text = "Polygon";
				labelInfo.Text = poly_draw_info;
			}
			else
			{
				cbIsPolygon.Text = "Circule";
				labelInfo.Text = circle_draw_info;
			}

			labelResult.Text = "";

			FieldViewer.SetDrawingMode(cbIsPolygon.Checked);
		}

		private void OnBeginDraw(bool IsPolygon)
		{
			labelResult.Text = "";
		}

		private void OnEndDraw(bool IsPolygon)
		{
			if(IsPolygon)
			{
				List<point> vertices = FieldViewer.GetPolygon();
								
				double area = CFac.SquarePoligon(FieldViewer.GetPolygon().ToArray());

				//список в 1 элемент не вызывает OnEndDraw, проверять нет необходимости.
				if(vertices.Count > 2)
				{
					//работаем с многоугольником
					if(vertices.Count == 3)
					{
						//сей полигон есть треугольник

						double a = vertices[0].dist(vertices[1]);
						double b = vertices[1].dist(vertices[2]);
						double c = vertices[2].dist(vertices[0]);

						if((a * a + b * b == c * c) || (a * a + c * c == b * b) || (c * c + b * b == a * a))
						{
							//сей треугольник прямоуголен
							labelResult.Text = "Площядь прямоугольного треугольника:" + area.ToString() + " глаз алмаз!";
						}
						else
						{
							labelResult.Text = "Площядь треугольника:" + area.ToString();
						}
					}
					else
					{
						labelResult.Text = "Площядь многоугольника:" + area.ToString();
					}
				}
				else
				{
					//всегда ровна нулю однако) но расчёты будут выполнены честно!
					labelResult.Text = "Площядь линии:" + area.ToString();
				}
			}
			else
			{
				labelResult.Text = "Площадь окружности:" + CFac.SquareCircle(FieldViewer.GetRadius()).ToString();
			}
		}
	}
}
