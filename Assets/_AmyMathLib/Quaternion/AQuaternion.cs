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

        public AQuaternion(float w, float x, float y, float z)
        {
            this.w = w;
            v = new AVector3(x, y, z);
        }

        public void SetAxis(AVector3 vector)
        {
            v.x = vector.x;
            v.y = vector.y;
            v.z = vector.z;
        }

        // I had some issues with this function, which were solved
        // through rubber ducking with my peers
        public AVector3 GetAxis()
        {
            // -- Incorrect implementation --
            //return new AVector3(v.x, v.y, v.z);

            // -- Correct implementation --
            return v / Mathf.Sin(GetAngle() / 2);
        }

        public float GetAngle()
        {
            return GetAxisAngle().w;
        }

        public AVector4 GetAxisAngle()
        {
            AVector4 rv = new AVector4(0, 0, 0, 0);

            // Inverse cosine to get our half angle back
            float halfAngle = Mathf.Acos(w);
            rv.w = halfAngle * 2;

            // Simple calculations to get our normal axis back using the half-angle
            rv.x = v.x / Mathf.Sin(halfAngle);
            rv.y = v.y / Mathf.Sin(halfAngle);
            rv.z = v.z / Mathf.Sin(halfAngle);

            return rv;
        }

        // I had some issues with this function, which were solved
        // through rubber ducking with my peers
        public AQuaternion Inverse()
        {
            // -- Incorrect implementation --
            //AQuaternion rv = new AQuaternion();
            //rv.w = w;
            //rv.SetAxis(-GetAxis());
            //return rv;

            // -- Correct implementation --
            return new(w, -v.x, -v.y, -v.z);
        }

        // I had some issues with this function, which were solved
        // through rubber ducking with my peers
        public static AQuaternion Slerp(AQuaternion q, AQuaternion r, float t)
        {
            // -- Incorrect implementation --
            //t = Mathf.Clamp(t, 0.0f, 1.0f);
            //AQuaternion d = r * q.Inverse();
            //AVector4 AxisAngle = d.GetAxisAngle();
            //AQuaternion dT = new AQuaternion(AxisAngle.w * t, new AVector3(AxisAngle.x, AxisAngle.y, AxisAngle.z));
            //return dT * q;

            // -- Correct implementation --
            AQuaternion d = r * q.Inverse();
            d = new(t * d.GetAxisAngle().w, d.GetAxis());
            Debug.Log($"{d.w}, {d.v.x}, {d.v.y}, {d.v.z}");
            return d * q;
        }

        public static AQuaternion ToAQuaternion(Quaternion a)
        {
            AQuaternion rv = new AQuaternion();

            rv.w = a.w;
            rv.v.x = a.x;
            rv.v.y = a.y;
            rv.v.z = a.z;

            return rv;
        }

        public static Quaternion ToUnityQuaternion(AQuaternion a)
        {
            Quaternion rv = new Quaternion();

            rv.w = a.w;
            rv.x = a.v.x;
            rv.y = a.v.y;
            rv.z = a.v.z;

            return rv;
        }

        public static Quaternion ToUnityQuaternion(AVector3 a)
        {
            Quaternion rv = new Quaternion();

            rv.w = 0.0f;
            rv.x = a.x;
            rv.y = a.y;
            rv.z = a.z;

            return rv;
        }

        public static AQuaternion operator *(AQuaternion a, AQuaternion b)
        {
            var w = a.w * b.w - a.v.x * b.v.x - a.v.y * b.v.y - a.v.z * b.v.z;
            var x = a.w * b.v.x + a.v.x * b.w + a.v.y * b.v.z - a.v.z * b.v.y;
            var y = a.w * b.v.y - a.v.x * b.v.z + a.v.y * b.w + a.v.z * b.v.x;
            var z = a.w * b.v.z + a.v.x * b.v.y - a.v.y * b.v.x + a.v.z * b.w;

            AQuaternion rv = new(w, x, y, z);

            return rv;
        }
    }
}