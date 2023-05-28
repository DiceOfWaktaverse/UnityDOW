using UnityEngine;

namespace DOW
{
    public abstract class DOWObject
    {
        public Vector3 Position { get; protected set; } = Vector3.zero;
      
        public DOWObject()
        {
            SetPosition(Vector3.zero);
        }
        public DOWObject(Vector3 position)
        {
            SetPosition(position);
        }

        public virtual void SetPosition(Vector3 position) {
            this.Position = position;
        }
    }

    public enum eCircleType
    {
        Circle,
        XEllipse,
        YEllipse
    }
    public class Circle : DOWObject
    {
        public eCircleType RadiusType { get; protected set; } = eCircleType.Circle;
        public float Radius { get; protected set; } = 0f;
        public float RadiusX { get; protected set; } = 0f;
        public float RadiusY { get; protected set; } = 0f;
        private Vector3 f1 = Vector3.zero;
        public Vector3 F1
        {
            get { return f1; }
            set { f1 = value; }
        }
        private Vector3 f2 = Vector3.zero;
        public Vector3 F2
        {
            get { return f2; }
            set { f2 = value; }
        }

        public Circle(Vector3 position, float radiusX, float radiusY)
        {
            SetPosition(position);
            SetEllipse(radiusX, radiusY);
        }

        public void SetEllipse(float radiusX, float radiusY) {
            this.RadiusX = radiusX;
            this.RadiusY = radiusY;

            f1 = new Vector3(Position.x, Position.y, Position.z);
            f2 = new Vector3(Position.x, Position.y, Position.z);
            if (radiusX == radiusY) {
                Radius = radiusX;
                RadiusType = eCircleType.Circle;
            } else if(radiusX > radiusY) {
                Radius = radiusX;
                RadiusType = eCircleType.XEllipse;
                var f = Mathf.Sqrt(radiusX * radiusX - radiusY * radiusY);
                f1.x -= f;
                f2.x += f;
            } else {
                Radius = radiusY;
                RadiusType = eCircleType.YEllipse;
                var f = Mathf.Sqrt(radiusY * radiusY - radiusX * radiusX);
                f1.y -= f;
                f2.y += f;
            }
        }

        public bool IsContain(Vector3 target)
        {
            switch (RadiusType)
            {
                case eCircleType.Circle:
                {
                    var distanceF1 = Vector2.Distance(Position, target);
                    if (distanceF1 < Radius)
                    {
                        return true;
                    }
                }
                break;
                case eCircleType.XEllipse:
                case eCircleType.YEllipse:
                {
                    var distanceF1 = Vector2.Distance(target, f1);
                    var distanceF2 = Vector2.Distance(target, f2);
                    if ((distanceF1 + distanceF2) <= (2 * Radius))
                    {
                        return true;
                    }
                }
                break;
            }

            return false;
        }
    }
}