namespace AmyMathLib.Maths
{
    using UnityEngine;
    using AmyMathLib.Vector;

    public class AMaths
    {
        #region Vector Functions
        public static float VectorToRadians(AVector2 V)
        {
            float rv = 0.0f;

            rv = Mathf.Atan(V.y / V.x);

            return rv;
        }

        public static AVector2 RadiansToVector(float angle)
        {
            AVector2 rv = new AVector2(Mathf.Cos(angle), Mathf.Sin(angle));

            return rv;
        }

        public static AVector3 EulerAnglesToDirection(AVector3 EulerAngles)
        {
            AVector3 rv = new AVector3(0, 0, 0);

            // TODO: Fix this, wrong coordinate system
            rv.x = Mathf.Cos(EulerAngles.y) * Mathf.Cos(EulerAngles.x);
            rv.y = Mathf.Sin(EulerAngles.x);
            rv.z = Mathf.Cos(EulerAngles.x) * Mathf.Sin(EulerAngles.y);

            return rv;
        }

        public static AVector3 VectorCrossProduct(AVector3 a, AVector3 b)
        {
            AVector3 c = new AVector3(0, 0, 0);

            c.x = a.y * b.z - a.z * b.y;
            c.y = a.z * b.x - a.x * b.z;
            c.z = a.x * b.y - a.y * b.x;

            return c;
        }
        #endregion
    }
}