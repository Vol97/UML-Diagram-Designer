using System.Collections.Generic;
using System.Drawing;
using UML_Diagram_Designer.Interfaces;

namespace UML_Diagram_Designer
{
    public abstract class AbstractDiagramElement : IDrawable, ISelectable, IMoveable
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Color ObjectPenColor { get; set; }
        public float ObjectPenWidth { get; set; }
        public abstract bool CheckIfTheObjectIsClicked(Point point);
        public abstract void Draw(Canvas painter);
        public abstract void Move(int deltaX, int deltaY);
    }
}
