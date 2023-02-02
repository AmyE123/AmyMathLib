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

        static AVector3 AddVector3(AVector3 a, AVector3 b)
        {
            AVector3 rv = new AVector3(a.x + b.x, a.y + b.y, a.z + b.z);

            return rv;
        }

        static AVector3 SubtractVector(AVector3 a, AVector3 b)
        {
            AVector3 rv = new AVector3(a.x - b.x, a.y - b.y, a.z - b.z);

            return rv;
        }

        /// <summary>
        /// Uses pythagorean theorem to calculate the length of the vector
        /// </summary>
        /// <returns>The length of AVector3</returns>
        public static float GetMagnitude()
        {
            // Length = Square Root of X^2 + Y^2 + Z^2
            float rv = Mathf.Sqrt(x * x + y * y + z * z);

            return rv;
        }

        /// <summary>
        /// Calculates the length squared of the vector
        /// </summary>
        /// <returns>The length squared</returns>
        public static float GetMagnitudeSquared()
        {
            float rv = x * x + y * y + z * z;

            return rv;
        }

        /// <summary>
        /// Calculates the dot product of two AVector3's
        /// </summary>
        /// <param name="ShouldNormalize">Whether we should normalize the vectors before calculating the dot product</param>
        /// <returns>A measure of how closely the two vectors align, in terms of the directons they point, a scalar number.</returns>
        public float GetDotProduct(AVector3 a, AVector3 b, bool ShouldNormalize = true)
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

        /// <summary>
        /// Normalizes the vector to give it a length of 1
        /// </summary>
        /// <param name="a">The vector to normalize</param>
        /// <returns>A new AVector3 with a length of 1</returns>
        public AVector3 NormalizeVector(AVector3 a)
        {
            AVector3 rv = DivideVector(this, GetMagnitude());

            return rv;
        }

        /// <summary>
        /// Converts from AVector3 to a Unity Vector3
        /// </summary>
        /// <returns>A Unity-compatible Vector3</returns>
        public Vector3 ToUnityVector3()
        {
            Vector3 rv = new Vector3(x, y, z);

            return rv;
        }

        /// <summary>
        /// Converts from a Unity Vector3 to AVector3
        /// </summary>
        /// <returns>AVector3-compatible Unity Vector</returns>
        public static AVector3 ToAVector3(Vector3 a)
        {
            AVector3 rv = new AVector3(a.x, a.y, a.z);

            return rv;
        }
    }
}
