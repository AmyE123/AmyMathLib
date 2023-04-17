namespace AmyMathLib.Vector
{
    using UnityEngine;

    public class AVector3
    {
        public float x, y, z;

        public AVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        #region Operator Overloads
        public static AVector3 operator +(AVector3 a, AVector3 b)
        {
            return AddVector3(a, b);
        }

        public static AVector3 operator -(AVector3 a, AVector3 b)
        {
            return SubtractVector3(a, b);
        }

        public static AVector3 operator -(AVector3 a)
        {
            return new AVector3(-a.x, -a.y, -a.z);
        }

        public static AVector3 operator *(AVector3 a, float b)
        {
            return ScaleVector(a, b);
        }

        public static AVector3 operator *(float a, AVector3 b)
        {
            return ScaleVector(b, a);
        }

        public static AVector3 operator /(AVector3 a, float b)
        {
            return DivideVector(a, b);
        }
        #endregion

        #region Operator Functions
        public static AVector3 AddVector3(AVector3 a, AVector3 b)
        {
            AVector3 rv = new AVector3(a.x + b.x, a.y + b.y, a.z + b.z);

            return rv;
        }

        public static AVector3 SubtractVector3(AVector3 a, AVector3 b)
        {
            AVector3 rv = new AVector3(a.x - b.x, a.y - b.y, a.z - b.z);

            return rv;
        }

        /// <summary>
        /// Takes the AVector3 and scales it by the scalar
        /// </summary>
        /// <param name="a">The vector which you want to scale</param>
        /// <param name="scalar">The amount you want the vector to scale by</param>
        /// <returns>A new scaled AVector3</returns>
        public static AVector3 ScaleVector(AVector3 a, float scalar)
        {
            AVector3 rv = new AVector3(a.x * scalar, a.y * scalar, a.z * scalar);

            return rv;
        }

        /// <summary>
        /// Takes the AVector3 and divides it by the divisor
        /// </summary>
        /// <param name="a">The vector which you want to divide</param>
        /// <param name="divisor">The amount you want the vector to divide by</param>
        /// <returns>A new divided AVector3</returns>
        public static AVector3 DivideVector(AVector3 a, float divisor)
        {
            AVector3 rv = new AVector3(a.x / divisor, a.y / divisor, a.z / divisor);

            return rv;
        }
        #endregion


        /// <summary>
        /// Uses pythagorean theorem to calculate the length of the vector
        /// </summary>
        /// <returns>The length of AVector3</returns>
        public float GetLength()
        {
            // Length = Square Root of X^2 + Y^2 + Z^2
            float rv = Mathf.Sqrt(x * x + y * y + z * z);

            return rv;
        }

        /// <summary>
        /// Calculates the length squared of the vector
        /// </summary>
        /// <returns>The length squared</returns>
        public float GetMagnitudeSquared()
        {
            float rv = x * x + y * y + z * z;

            return rv;
        }

        /// <summary>
        /// Normalizes the vector to give it a length of 1
        /// </summary>
        /// <returns>A new AVector3 with a length of 1</returns>
        public AVector3 NormalizeVector()
        {
            AVector3 rv = new AVector3(0, 0, 0);
            rv.x = x;
            rv.y = y;
            rv.z = z;

            rv = rv / rv.GetLength();

            return rv;
        }

        /// <summary>
        /// Converts to a Unity Vector3
        /// </summary>
        /// <returns>A Unity-compatible Vector3</returns>
        public Vector3 ToUnityVector3()
        {
            Vector3 rv = new Vector3(x, y, z);

            return rv;
        }
    }
}
