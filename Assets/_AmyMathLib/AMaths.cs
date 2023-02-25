namespace AmyMathLib.Maths
{
    using UnityEngine;
    using AmyMathLib.Vector;

    public class AMaths
    {
        #region Vector Functions
        /// <summary>
        /// Calculates the dot product of two AVector3's
        /// </summary>
        /// <param name="ShouldNormalize">Whether we should normalize the vectors before calculating the dot product</param>
        /// <returns>A measure of how closely the two vectors align, in terms of the directons they point, a scalar number.</returns>
        public static float GetDotProduct(AVector3 a, AVector3 b, bool ShouldNormalize = true)
        {
            float rv = 0.0f;

            if (ShouldNormalize)
            {
                AVector3 normalizedA = a.NormalizeVector();
                AVector3 normalizedB = b.NormalizeVector();

                rv = normalizedA.x * normalizedB.x + normalizedA.y * normalizedB.y + normalizedA.z * normalizedB.z;
            }
            else
            {
                rv = a.x * b.x + a.y * b.y + a.z * b.z;
            }

            return rv;
        }

        /// <summary>
        /// Calculates the dot product of two AVector2's
        /// </summary>
        /// <param name="ShouldNormalize">Whether we should normalize the vectors before calculating the dot product</param>
        /// <returns>A measure of how closely the two vectors align, in terms of the directons they point, a scalar number.</returns>
        public static float GetDotProduct(AVector2 a, AVector2 b, bool ShouldNormalize = true)
        {
            float rv = 0.0f;

            if (ShouldNormalize)
            {
                AVector2 normalizedA = a.NormalizeVector();
                AVector2 normalizedB = b.NormalizeVector();

                rv = normalizedA.x * normalizedB.x + normalizedA.y * normalizedB.y;
            }
            else
            {
                rv = a.x * b.x + a.y * b.y;
            }

            return rv;
        }

        /// <summary>
        /// Converts from AVector3 to a Unity Vector3
        /// </summary>
        /// <returns>A Unity-compatible Vector3</returns>
        public static Vector3 ToUnityVector(AVector3 a)
        {
            Vector3 rv = new Vector3(a.x, a.y, a.z);
            return rv;
        }

        /// <summary>
        /// Converts from AVector2 to a Unity Vector2
        /// </summary>
        /// <returns>A Unity-compatible Vector2</returns>
        public static Vector2 ToUnityVector(AVector2 a)
        {
            Vector2 rv = new Vector2(a.x, a.y);
            return rv;
        }

        /// <summary>
        /// Converts from a Unity Vector3 to AVector3
        /// </summary>
        /// <returns>AVector3-compatible Unity Vector</returns>
        public static AVector3 ToAVector(Vector3 a)
        {
            AVector3 rv = new AVector3(a.x, a.y, a.z);

            return rv;
        }

        /// <summary>
        /// Converts from a Unity Vector2 to AVector2
        /// </summary>
        /// <returns>AVector2-compatible Unity Vector</returns>
        public static AVector2 ToAVector(Vector2 a)
        {
            AVector2 rv = new AVector2(a.x, a.y);

            return rv;
        }

        public static AVector3 EulerAngleToDirection(AVector3 EulerAngle)
        {
            AVector3 rv = new AVector3(0, 0, 0);

            // TODO: Fix this, wrong coordinate system
            rv.x = Mathf.Cos(EulerAngle.y) * Mathf.Cos(EulerAngle.x);
            rv.y = Mathf.Sin(EulerAngle.y) * Mathf.Cos(EulerAngle.x);
            rv.z = Mathf.Sin(EulerAngle.x);

            //rv.x = Mathf.Cos(EulerAngle.y) * Mathf.Cos(EulerAngle.x);
            //rv.y = Mathf.Sin(EulerAngle.x);
            //rv.z = Mathf.Cos(EulerAngle.x) * Mathf.Sin(EulerAngle.y);

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
        #endregion

        // TODO: Implement LERP V3
        public static AVector3 Lerp(AVector3 a, AVector3 b, float t)
        {
            return a * (1.0f - t) + b * t;
        }

        // TODO: Implement LERP V2
        public static AVector2 Lerp(AVector2 a, AVector2 b, float t)
        {
            return a * (1.0f - t) + b * t;
        }
    }
}