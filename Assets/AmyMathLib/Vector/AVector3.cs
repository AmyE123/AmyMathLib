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
        /// Uses pythagorean theorem to calculate the magnitude of the vector from (0,0,0). 
        /// </summary>
        /// <returns>The magnitude/length of AVector3 from (0,0,0)</returns>
        public float GetMagnitude()
        {
            // Length = Square Root of X^2 + Y^2 + Z^2
            float rv = Mathf.Sqrt(x * x + y * y + z * z);

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
