using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CharCardsPrinter
{
    public interface IView
    {
        PointF Location { get; set; }
        SizeF Size { get; set; }
        void Draw(Graphics graphics);
    }
    public abstract class View : IView
    {

        public virtual PointF Location { get; set; }
        public virtual SizeF Size { get; set; }
        public abstract void Draw(Graphics graphics);
    }
    public interface IViewContainer : IView
    {
        List<IView> Views { get; }
        void Add(IView view);
        void Add(IView view, int index);
        void Remove(IView view);
        void Remove(int index);
        void Layout(float left, float top, float width, float height);
    }

    public class ViewContainer :View, IViewContainer
    {
        public ViewContainer()
        {
        }

        public List<IView> Views { get; } = new List<IView>();

        public void Add(IView view)
        {
            Views.Add(view);
        }

        public void Add(IView view, int index)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Graphics graphics)
        {
            foreach(IView view in Views)
            {
                view.Draw(graphics);
            }
        }

        public void Layout(float left, float top, float width, float height)
        {            
        }

        public void Remove(IView view)
        {
            Views.Remove(view);
        }

        public void Remove(int index)
        {
            throw new NotImplementedException();
        }
    }
    
    public class GridLayout : ViewContainer
    {
        public float MarginLeft { get; set; } 
        public float MarginTop { get; set; }
        public float MarginRight { get; set; }
        public float MarginBottom { get; set; }

        public int ColumnCount { get; set; }
        public int RowCount { get; set; }

        public override void Draw(Graphics graphics)
        {

        }
    }
}
