namespace AmyMathLib.Quaternion
{
    using AmyMathLib.Vector;
    using UnityEngine;

    public class AQuaternion
    {
        public float w;
        public AVector3 v;

        public AQuaternion()
        {
            w = 0.0f;
            v = new AVector3(0, 0, 0);
        }

        public AQuaternion(float angle, AVector3 axis)
        {
            float halfAngle = angle / 2;
            w = Mathf.Cos(halfAngle);
            v = axis * Mathf.Sin(halfAngle);
        }

        public AQuaternion(AVector3 position)
        {
            w = 0.0f;
            v = new AVector3(position.x, position.y, position.z);
        }

        public void SetAxis(AVector3 vector)
        {
            v.x = vector.x;
            v.y = vector.y;
            v.z = vector.z;
        }

        public AVector3 GetAxis()
        {
            return new AVector3(v.x, v.y, v.z);
        }

        public AQuaternion Inverse()
        {
            AQuaternion rv = new AQuaternion();
            rv.w = w;

            rv.SetAxis(-GetAxis());

            return rv;
        }

        public static AQuaternion operator *(AQuaternion a, AQuaternion b)
        {
            var w = a.w * b.w - a.v.x * b.v.x - a.v.y * b.v.y - a.v.z * b.v.z;
            var x = a.w * b.v.x + a.v.x * b.w + a.v.y * b.v.z - a.v.z * b.v.y;
            var y = a.w * b.v.y - a.v.x * b.v.z + a.v.y * b.w + a.v.z * b.v.x;
            var z = a.w * b.v.z + a.v.x * b.v.y - a.v.y * b.v.x + a.v.z * b.w;

            AQuaternion rv = new AQuaternion(new AVector3(x, y, z));

            return rv;
        }
    }
}