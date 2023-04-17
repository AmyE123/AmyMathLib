using AmyMathLib.Maths;
using AmyMathLib.Quaternion;
using AmyMathLib.Vector;
using UnityEngine;

public class Quaternions : MonoBehaviour
{
    public float angle;


    // Update is called once per frame
    void Update()
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
}
