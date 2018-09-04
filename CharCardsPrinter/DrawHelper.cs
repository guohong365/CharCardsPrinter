using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace CharCardsPrinter
{
   public static class DrawHelper
    {
        public static float PixelToMillimeter(float dpi, int pixel)
        {
            return pixel / dpi * 25.3f;
        }
        public static float GetRatio(SizeF source, SizeF target)
        {
            return GetRatio(source.Width, source.Height, target.Width, target.Height);
        }
        public static float GetRatio(float sourceWidth,float sourceHeight, float targetWidth, float targetHeight)
        {
            float w = (float)sourceWidth / targetWidth;
            float h = (float)sourceHeight / targetHeight;

            return w > h ? h : w;

        }
    }
}
