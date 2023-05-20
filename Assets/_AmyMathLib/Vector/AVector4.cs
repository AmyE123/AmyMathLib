namespace AmyMathLib.Vector
{
    using UnityEngine;

    /// <summary>
    /// An AVector4 class to replace Unity's Vector4 class
    /// </summary>
    public class AVector4
    {
        public float x, y, z, w;

        public AVector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        #region Operator Overloads
        public static AVector4 operator +(AVector4 a, AVector4 b)
        {
            return AddVector4(a, b);
        }

        public static AVector4 operator -(AVector4 a, AVector4 b)
        {
            return SubtractVector4(a, b);
        }

        public static AVector4 operator *(AVector4 a, float b)
        {
            return ScaleVector(a, b);
        }

        public static AVector4 operator /(AVector4 a, float b)
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
        /// <returns>A new added AVector4</returns>
        public static AVector4 AddVector4(AVector4 a, AVector4 b)
        {
            AVector4 rv = new AVector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);

            return rv;
        }

        /// <summary>
        /// Takes the vectors and subtracts them from each other
        /// </summary>
        /// <param name="a">The vector you want to subtract</param>
        /// <param name="b">The other vector you want to subtract</param>
        /// <returns>A new subtracted AVector4</returns>
        public static AVector4 SubtractVector4(AVector4 a, AVector4 b)
        {
            AVector4 rv = new AVector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);

            return rv;
        }

        /// <summary>
        /// Takes the AVector4 and scales it by the scalar
        /// </summary>
        /// <param name="a">The vector which you want to scale</param>
        /// <param name="scalar">The amount you want the vector to scale by</param>
        /// <returns>A new scaled AVector4</returns>
        public static AVector4 ScaleVector(AVector4 a, float scalar)
        {
            AVector4 rv = new AVector4(a.x * scalar, a.y * scalar, a.z * scalar, a.w * scalar);

            return rv;
        }

        /// <summary>
        /// Takes the AVector4 and divides it by the divisor
        /// </summary>
        /// <param name="a">The vector which you want to divide</param>
        /// <param name="divisor">The amount you want the vector to divide by</param>
        /// <returns>A new divided AVector4</returns>
        public static AVector4 DivideVector(AVector4 a, float divisor)
        {
            AVector4 rv = new AVector4(a.x / divisor, a.y / divisor, a.z / divisor, a.w / divisor);

            return rv;
        }
        #endregion


        /// <summary>
        /// Uses pythagorean theorem to calculate the length of the vector
        /// </summary>
        /// <returns>The length of AVector4</returns>
        public float GetLength()
        {
            // Length = Square Root of X^2 + Y^2 + Z^2 + W^2
            float rv = Mathf.Sqrt(x * x + y * y + z * z + w * w);

            return rv;
        }

        /// <summary>
        /// Calculates the length squared of the vector
        /// </summary>
        /// <returns>The length squared</returns>
        public float GetMagnitudeSquared()
        {
            float rv = x * x + y * y + z * z + w * w;

            return rv;
        }

        /// <summary>
        /// Normalizes the vector to give it a length of 1
        /// </summary>
        /// <returns>A new AVector4 with a length of 1</returns>
        public AVector4 NormalizeVector()
        {
            AVector4 rv = new AVector4(0, 0, 0, 0);
            rv.x = x;
            rv.y = y;
            rv.z = z;
            rv.w = w;

            rv = rv / rv.GetLength();

            return rv;
        }

        /// <summary>
        /// Converts to a Unity Vector4
        /// </summary>
        /// <returns>A Unity-compatible Vector4</returns>
        public Vector4 ToUnityVector4()
        {
            Vector4 rv = new Vector4(x, y, z, w);

            return rv;
        }
    }
}
