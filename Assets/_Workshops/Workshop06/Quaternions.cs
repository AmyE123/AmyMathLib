namespace Workshop
{
    using AmyMathLib.Maths;
    using AmyMathLib.Quaternion;
    using AmyMathLib.Vector;
    using UnityEngine;

    /// <summary>
    /// A demo showcasing how to use Quaternions with AmyMathLib
    /// </summary>
    public class Quaternions : MonoBehaviour
    {
        public float angle;
        public float t;
        public float speed = 0.5f;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                QuaternionDemo();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SlerpDemo();
            }           
        }

        void QuaternionDemo()
        {
            angle += Time.deltaTime;

            // Define a quaternion that is the equivilent of rotating around the Yaw axis by "angle" amount
            AQuaternion q = new AQuaternion(angle, new AVector3(0, 1, 0));

            // Define a vector which we will rotate
            AVector3 p = new AVector3(1, 2, 3);

            // Store that vector in a quaternion (we're using an overloaded constructor here to store the raw position)
            AQuaternion k = new AQuaternion(p);

            // The variable newK will have our new position inside of it
            AQuaternion newK = q * k * q.Inverse();

            // Get the position as a vector
            AVector3 newP = newK.GetAxis();

            // Set the position so we can see if it's working
            transform.position = AMaths.ToUnityVector(newP);
        }

        void SlerpDemo()
        {
            t += Time.deltaTime * speed;

            // Defining two rotations
            AQuaternion q = new AQuaternion(Mathf.PI * 0.5f, new AVector3(0, 1, 0));
            AQuaternion r = new AQuaternion(Mathf.PI * 0.25f, new AVector3(1, 0, 0));

            // This is the slerped value
            AQuaternion slerped = AQuaternion.Slerp(q, r, t);

            // Define a vector which we will rotate
            AVector3 p = new AVector3(1, 2, 3);

            // Store that vector in a quaternion (We're using an overloaded constructor here to store the raw position)
            AQuaternion k = new AQuaternion(p);

            // The variable newK will have our new position inside of it
            AQuaternion newK = slerped * k * slerped.Inverse();

            // Get the position as a vector
            AVector3 newP = newK.GetAxis();

            // Set the position so we can see if it's working
            transform.rotation = AQuaternion.ToUnityQuaternion(newP);
        }
    }
}
