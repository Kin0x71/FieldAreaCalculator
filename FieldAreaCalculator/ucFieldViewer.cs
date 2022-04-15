using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using faclib;

namespace FieldAreaCalculator
{
	public class ucFieldViewer : PictureBox
	{
		public delegate void OnBeginEndDraw(bool IsPolygon);
		private OnBeginEndDraw BeginDrawCallback = null;
		private OnBeginEndDraw EndDrawCallback = null;

		private List<point> DrawingPoints = new List<point>();
		private point LastPoint = new point();
		private point CurrentPoint = new point();
		private bool BeginDrawing = false;

		private Pen BlackPen = new Pen(Color.Black, 3);
		private Brush WhiteBrush = new SolidBrush(Color.White);

		public bool IsPolygon = true;

		public ucFieldViewer(OnBeginEndDraw begin_draw_callback, OnBeginEndDraw end_draw_callback)
		{
			BeginDrawCallback = begin_draw_callback;
			EndDrawCallback = end_draw_callback;

			Click += new EventHandler(onClick);
			DoubleClick += new EventHandler(onDoubleClick);
			MouseMove += new MouseEventHandler(onMouseMove);
		}

		public double GetRadius()
		{
			return LastPoint.dist(CurrentPoint);
		}

		public List<point> GetPolygon()
		{
			return DrawingPoints;
		}

		public void SetDrawingMode(bool is_polygon)
		{
			IsPolygon = is_polygon;

			ClearView();
			BeginDrawing = false;
			DrawingPoints.Clear();
		}

		private void ClearView()
		{
			using(Graphics graphics = Graphics.FromImage(Image))
			{
				graphics.FillRectangle(WhiteBrush, new Rectangle(0, 0, Image.Width, Image.Height));
			}

			Refresh();
		}

		private void onClick(object sender, EventArgs e)
		{
			MouseEventArgs mouse_event = (MouseEventArgs)e;

			if(!BeginDrawing)
			{
				ClearView();
				BeginDrawing = true;
				DrawingPoints.Clear();
				LastPoint = new point(mouse_event.X, mouse_event.Y);

				BeginDrawCallback(IsPolygon);

				return;
			}

			if(IsPolygon)
			{
				using(Graphics graphics = Graphics.FromImage(Image))
				{
					graphics.DrawLine(BlackPen, (float)LastPoint.x, (float)LastPoint.y, (float)CurrentPoint.x, (float)CurrentPoint.y);
				}

				CurrentPoint = LastPoint;
				LastPoint = new point(mouse_event.X, mouse_event.Y);

				DrawingPoints.Add(CurrentPoint);
			}
			else
			{
				BeginDrawing = false;

				using(Graphics graphics = Graphics.FromImage(Image))
				{
					DrawCircule(graphics);
				}

				EndDrawCallback(false);
			}

		}

		private void onDoubleClick(object sender, EventArgs e)
		{
			if(!BeginDrawing || !IsPolygon) return;

			if(DrawingPoints.Count > 0)
			{
				DrawingPoints.Add(CurrentPoint);

				BeginDrawing = false;

				//если вершин меньше 2х или найдено хотябы одно пересечение считаем это ошибкой и удаляем линии
				if(DrawingPoints.Count < 2 || FindFirstIntersections())
				{
					ClearView();
					DrawingPoints.Clear();
				}
				else
				{
					using(Graphics graphics = Graphics.FromImage(Image))
					{
						graphics.DrawLine(BlackPen, (float)CurrentPoint.x, (float)CurrentPoint.y, (float)DrawingPoints[0].x, (float)DrawingPoints[0].y);
					}

					EndDrawCallback(true);
				}
			}

			Refresh();
		}

		private void onMouseMove(object sender, MouseEventArgs e)
		{
			if(!BeginDrawing) return;

			CurrentPoint = new point(e.X, e.Y);

			Refresh();
		}

		private void DrawCircule(Graphics g)
		{
			double radius = LastPoint.dist(CurrentPoint);

			g.DrawEllipse(
				BlackPen,
				new RectangleF(
					(float)(LastPoint.x - radius), (float)(LastPoint.y - radius), (float)(radius + radius), (float)(radius + radius)
				)
			);
		}

		private bool FindFirstIntersections()
		{
			for(int ia = 0; ia < DrawingPoints.Count; ++ia)
			{
				point pa0 = DrawingPoints[ia];
				point pb0 = DrawingPoints[ia == DrawingPoints.Count - 1 ? 0 : ia + 1];

				for(int ib = 0; ib < DrawingPoints.Count; ++ib)
				{
					if(ib == ia) continue;

					point pa1 = DrawingPoints[ib];
					point pb1 = DrawingPoints[ib == DrawingPoints.Count - 1 ? 0 : ib + 1];

					if(pb0 == pa1 || pa0 == pb1) continue;

					if((pb1.y - pa1.y) * (pb0.x - pa0.x) - (pb1.x - pa1.x) * (pb0.y - pa0.y) != 0)
					{
						double u1 = ((pb1.x - pa1.x) * (pa0.y - pa1.y) - (pa0.x - pa1.x) * (pb1.y - pa1.y)) / ((pb1.y - pa1.y) * (pb0.x - pa0.x) - (pb1.x - pa1.x) * (pb0.y - pa0.y));
						double u2 = ((pb0.x - pa0.x) * (pa0.y - pa1.y) - (pb0.y - pa0.y) * (pa0.x - pa1.x)) / ((pb1.y - pa1.y) * (pb0.x - pa0.x) - (pb1.x - pa1.x) * (pb0.y - pa0.y));
						
						if((u1 >= 0) && (u1 <= 1) && (u2 >= 0) && (u2 <= 1)){
							//CFac.point intersect = new CFac.point(pa0.x + u1 * (pb0.x - pa0.x), pa0.y + u1 * (pb0.y - pa0.y));
							return true;
						}
					}
				}
			}

			return false;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.DrawImage(
				Image,
				e.ClipRectangle,
				e.ClipRectangle.X,
				e.ClipRectangle.Y,
				e.ClipRectangle.Width,
				e.ClipRectangle.Height,
				GraphicsUnit.Pixel
			);

			if(!BeginDrawing) return;

			if(IsPolygon)
			{
				e.Graphics.DrawLine(BlackPen, (float)LastPoint.x, (float)LastPoint.y, (float)CurrentPoint.x, (float)CurrentPoint.y);
			}
			else
			{
				DrawCircule(e.Graphics);
			}
		}
	}
}
