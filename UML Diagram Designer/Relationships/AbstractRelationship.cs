using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using UML_Diagram_Designer.ArrowParts;

namespace UML_Diagram_Designer.Relationships
{
    public abstract class AbstractRelationship : AbstractDiagramElement
    {
        private SelectedPoint selectPoint;
        public AbstractCap cap;
        public DashStyle lineStyle;

        public enum SelectedPoint
        {
            StartPoint = 0,
            EndPoint
        }

        public override void Draw(Canvas painter)
        {
            painter.pen.CustomEndCap = cap.GetCap();
            painter.pen.DashStyle = lineStyle;
            painter.graphics.DrawLine(painter.pen, StartPoint, EndPoint);
        }

        public override bool CheckIfTheObjectIsClicked(Point point)
        {
            const int delta = 20;
            if (point.X < StartPoint.X + delta && point.X > StartPoint.X - delta &&
                point.Y < StartPoint.Y + delta && point.Y > StartPoint.Y - delta)
            {
                selectPoint = SelectedPoint.StartPoint;
                return true;
            }
            if (point.X < EndPoint.X + delta && point.X > EndPoint.X - delta &&
                point.Y < EndPoint.Y + delta && point.Y > EndPoint.Y - delta)
            {
                selectPoint = SelectedPoint.EndPoint;
                return true;
            }
            return false;
        }

        public override void Move(int deltaX, int deltaY)
        {
            if (selectPoint == SelectedPoint.StartPoint)
            {
                StartPoint = new Point(StartPoint.X + deltaX, StartPoint.Y + deltaY);
            }
            else
            {
                EndPoint = new Point(EndPoint.X + deltaX, EndPoint.Y + deltaY);
            }
        }
    }
}
