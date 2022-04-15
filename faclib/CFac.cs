using System;

namespace faclib
{
	public class point
	{
		public double x;
		public double y;

		public point()
		{
			x = 0.0;
			y = 0.0;
		}

		public point(double X, double Y)
		{
			x = X;
			y = Y;
		}

		public double dist(point p)
		{
			return Math.Sqrt((x - p.x) * (x - p.x) + (y - p.y) * (y - p.y));
		}
	};

	public static class CFac
    {
		//формула площади Гаусса
		//взято из https://cpp.mazurok.com/tag/площадь-многоугольника/
		public static double SquarePoligon(point[] points)
		{
			if(points.Length < 2) return 0.0;

			double sum = 0;

			for(int i = 0; i < points.Length - 1; ++i)
			{
				sum += (points[i].x + points[i + 1].x) * (points[i + 1].y - points[i].y);
			}

			sum += (points[points.Length - 1].x + points[0].x) * (points[0].y - points[points.Length - 1].y);

			return Math.Abs(sum) / 2;
		}

		public static double SquareCircle(double radius)
		{
			return Math.PI * Math.Pow(radius, 2.0);
		}
	}
}
