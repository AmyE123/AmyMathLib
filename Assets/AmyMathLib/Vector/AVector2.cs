namespace AmyMathLib.Vector
{
    using UnityEngine;

    public class AVector2
    {
        public float x, y;

        public AVector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        #region Operator Overloads
        public static AVector2 operator +(AVector2 a, AVector2 b)
        {
            return AddVector2(a, b);
        }

        public static AVector2 operator -(AVector2 a, AVector2 b)
        {
            return SubtractVector2(a, b);
        }

        public static AVector2 operator *(AVector2 a, float b)
        {
            return ScaleVector(a, b);
        }

        public static AVector2 operator /(AVector2 a, float b)
        {
            return DivideVector(a, b);
        }
        #endregion

        #region Operator Functions
        public static AVector2 AddVector2(AVector2 a, AVector2 b)
        {
            AVector2 rv = new AVector2(a.x + b.x, a.y + b.y);

            return rv;
        }

        public static AVector2 SubtractVector2(AVector2 a, AVector2 b)
        {
            AVector2 rv = new AVector2(a.x - b.x, a.y - b.y);

            return rv;
        }

        /// <summary>
        /// Takes the AVector2 and scales it by the scalar
        /// </summary>
        /// <param name="a">The vector which you want to scale</param>
        /// <param name="scalar">The amount you want the vector to scale by</param>
        /// <returns>A new scaled AVector2</returns>
        static AVector2 ScaleVector(AVector2 a, float scalar)
        {
            AVector2 rv = new AVector2(a.x * scalar, a.y * scalar);

            return rv;
        }

        /// <summary>
        /// Takes the AVector2 and divides it by the divisor
        /// </summary>
        /// <param name="a">The vector which you want to divide</param>
        /// <param name="divisor">The amount you want the vector to divide by</param>
        /// <returns>A new divided AVector2</returns>
        static AVector2 DivideVector(AVector2 a, float divisor)
        {
            AVector2 rv = new AVector2(a.x / divisor, a.y / divisor);

            return rv;
        }
        #endregion


        /// <summary>
        /// Uses pythagorean theorem to calculate the length of the vector
        /// </summary>
        /// <returns>The length of AVector2 as a float</returns>
        public float GetLength()
        {
            // Length = Square Root of X^2 + Y^2
            float rv = Mathf.Sqrt(x * x + y * y);

            return rv;
        }

        /// <summary>
        /// Calculates the length squared of the vector
        /// </summary>
        /// <returns>The length squared</returns>
        public float GetMagnitudeSquared()
        {
            float rv = x * x + y * y;

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
        /// Normalizes the vector to give it a length of 1
        /// </summary>
        /// <returns>A new AVector2 with a length of 1</returns>
        public AVector2 NormalizeVector()
        {
            AVector2 rv = new AVector2(0, 0);
            rv.x = x;
            rv.y = y;

            rv = rv / rv.GetLength();

            return rv;
        }

        /// <summary>
        /// Converts from AVector2 to a Unity Vector2
        /// </summary>
        /// <returns>A Unity-compatible Vector2</returns>
        public Vector2 ToUnityVector2()
        {
            Vector2 rv = new Vector2(x, y);

            return rv;
        }

        public static Vector2 ToUnityVector2(AVector2 a)
        {
            Vector2 rv = new Vector2(a.x, a.y);
            return rv;
        }

        /// <summary>
        /// Converts from a Unity Vector2 to AVector2
        /// </summary>
        /// <returns>AVector2-compatible Unity Vector</returns>
        public static AVector2 ToAVector2(Vector2 a)
        {
            AVector2 rv = new AVector2(a.x, a.y);

            return rv;
        }
    }
}
