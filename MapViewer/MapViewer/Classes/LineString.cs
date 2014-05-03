using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapViewer.Classes
{
    public class LineStringParser
    {
        public static PointF[] Parse(string lineString)
        {
            if (lineString == string.Empty)
                return null;
            PointF[] Result;
            string[] p1 = lineString.Split(new char[] { '(', ')' });
            if (p1.Length < 2)
                return null;
            string[] points = p1[1].Split(',');
            Result = new PointF[points.Length];
            string[] tmp ;
            for (int i = 0; i < points.Length; i++)
            {
                tmp = points[i].Trim().Split(' ');
                if (tmp.Length < 2)
                    return null;
                Result[i] = new PointF(float.Parse(tmp[0]),-1*float.Parse(tmp[1]));
            }
            return Result;
        }
    }
}
