using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakePaint
{
    public interface IFigure
    {
        Point[] Points { get; set; }
        Size Size { get; set; }
        Color Color { get; set; }
        bool IsFilled { get; set; }
        bool IsCustom { get; set; }
    }
    class Rectangle : IFigure
    {
        public Point[] Points { get; set; } = new Point[4];
        public Size Size { get; set; }
        public Color Color { get; set; }
        public bool IsFilled { get; set; }
        public bool IsCustom { get; set; }
    }
    class Circle : IFigure
    {
        public Point[] Points { get; set; } = new Point[1];
        public Size Size { get; set; }
        public Color Color { get; set; }
        public bool IsFilled { get; set; }

        public bool IsCustom { get; set; }

    }
    class Triangle : IFigure
    {
        public Point[] Points { get; set; } = new Point[4];
        public Size Size { get; set; }
        public Color Color { get; set; }
        public bool IsFilled { get; set; }
        public bool IsCustom { get; set; }
    }
    interface IFactory
    {
        IFigure GetFigure();
    }
    class RectangleFactory:IFactory
    {
        public IFigure GetFigure()
        {
            return new Rectangle();
        }
    }
    class CircleFactory : IFactory
    {
        public IFigure GetFigure()
        {
            return new Circle();
        }
    }
    class TriangleFactory : IFactory
    {
        public IFigure GetFigure()
        {
            return new Triangle();
        }
    }
}
