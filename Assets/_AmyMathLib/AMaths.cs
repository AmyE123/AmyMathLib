namespace AmyMathLib.Maths
{
    using UnityEngine;
    using AmyMathLib.Vector;

    /// <summary>
    /// An AMaths class to replace Unity's Maths class
    /// </summary>
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

        /// <summary>
        /// Gets the direction from the euler angle
        /// </summary>
        /// <param name="EulerAngle">The euler angle to get the direction from</param>
        /// <returns>A direction vector</returns>
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

        /// <summary>
        /// The cross product of two vectors
        /// </summary>
        /// <param name="a">The first vector3</param>
        /// <param name="b">The second vector3</param>
        /// <returns>A vector3 of the cross product</returns>
        public static AVector3 VectorCrossProduct(AVector3 a, AVector3 b)
        {
            AVector3 c = new AVector3(0, 0, 0);

            c.x = a.y * b.z - a.z * b.y;
            c.y = a.z * b.x - a.x * b.z;
            c.z = a.x * b.y - a.y * b.x;

            return c;
        }

        /// <summary>
        /// Converts a AVector2 to radians
        /// </summary>
        /// <param name="V">The AVector2 you want to convert</param>
        /// <returns>The radian returned from the vector</returns>
        public static float VectorToRadians(AVector2 V)
        {
            float rv = 0.0f;

            rv = Mathf.Atan(V.y / V.x);

            return rv;
        }

        /// <summary>
        /// Converts a radian to a AVector2
        /// </summary>
        /// <param name="angle">The angle which you want to convert</param>
        /// <returns>The AVector2 from the radian</returns>
        public static AVector2 RadiansToVector(float angle)
        {
            AVector2 rv = new AVector2(Mathf.Cos(angle), Mathf.Sin(angle));

            return rv;
        }
        #endregion

        #region Other Functions
        /// <summary>
        /// Rounds a float number to a whole number by using casting
        /// </summary>
        /// <param name="a">The number to round</param>
        /// <returns>A rounded integer number</returns>
        public static int Round(float a)
        {
            int rv = (int)a;
            return rv;
        }
        #endregion

        /// <summary>
        /// Implementation of Linear Interpolation
        /// </summary>
        /// <param name="a">'From' AVector3</param>
        /// <param name="b">'To' AVector3</param>
        /// <param name="t">The time it takes for the lerp</param>
        /// <returns>AVector3 value for the lerp</returns>
        public static AVector3 Lerp(AVector3 a, AVector3 b, float t)
        {
            return a * (1.0f - t) + b * t;
        }

        /// <summary>
        /// Implementation of Linear Interpolation
        /// </summary>
        /// <param name="a">'From' AVector2</param>
        /// <param name="b">'To' AVector2</param>
        /// <param name="t">The time it takes for the lerp</param>
        /// <returns>AVector2 value for the lerp</returns>
        public static AVector2 Lerp(AVector2 a, AVector2 b, float t)
        {
            return a * (1.0f - t) + b * t;
        }

        /// <summary>
        /// Implementation of Linear Interpolation
        /// </summary>
        /// <param name="a">'From' float</param>
        /// <param name="b">'To' float</param>
        /// <param name="t">The time it takes for the lerp</param>
        /// <returns>float value for the lerp</returns>
        public static float Lerp(float a, float b, float t)
        {
            return a * (1.0f - t) + b * t;
        }

        // TODO: Fix this
        //public static AVector3 LerpEaser(AVector3 a, AVector3 b, float t)
        //{
        //    AVector3 l = a * (1.0f - t) + b * t;

        //    float x = EaseInOutCubic(l.x);
        //    float y = EaseInOutCubic(l.y);
        //    float z = EaseInOutCubic(l.z);

        //    return new AVector3(x, y, z);
        //}

        public static AVector3 RotateVertexAroundAxis(float angle, AVector3 axis, AVector3 vertex)
        {
            AVector3 rv = (vertex * Mathf.Cos(angle)) +
                GetDotProduct(vertex, axis) * axis * (1.0f - Mathf.Cos(angle)) +
                VectorCrossProduct(axis, vertex) * Mathf.Sin(angle);

            return rv;
        }

        //TODO: Fix this
        public static float EaseInOutCubic(float x)
        {
            return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
        }
    }
}