namespace AmyMathLib.Vector
{
    using UnityEngine;

    /// <summary>
    /// An AVector2 class to replace Unity's Vector2 class
    /// </summary>
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
        /// <summary>
        /// Takes the vectors and adds them together
        /// </summary>
        /// <param name="a">The vector you want to add</param>
        /// <param name="b">The other vector you want to add</param>
        /// <returns>A new added AVector2</returns>
        public static AVector2 AddVector2(AVector2 a, AVector2 b)
        {
            AVector2 rv = new AVector2(a.x + b.x, a.y + b.y);

            return rv;
        }

        /// <summary>
        /// Takes the vectors and subtracts them from each other
        /// </summary>
        /// <param name="a">The vector you want to subtract</param>
        /// <param name="b">The other vector you want to subtract</param>
        /// <returns>A new subtracted AVector2</returns>
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
        public static AVector2 ScaleVector(AVector2 a, float scalar)
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
        public static AVector2 DivideVector(AVector2 a, float divisor)
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
        /// Converts to a Unity Vector2
        /// </summary>
        /// <returns>A Unity-compatible Vector2</returns>
        public Vector2 ToUnityVector2()
        {
            Vector2 rv = new Vector2(x, y);

            return rv;
        }
    }
}
