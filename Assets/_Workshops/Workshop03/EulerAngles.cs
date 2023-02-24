using UnityEngine;
using AmyMathLib.Vector;
using AmyMathLib.Maths;

public class EulerAngles : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    private AVector3 _eulerAngle = new AVector3(0, 0, 0);
    private AVector3 _direction = new AVector3(0, 0, 0);
    private AVector3 _crossProduct = new AVector3(0, 0, 0);
    private AVector3 _constantUpward = new AVector3(0, 1, 0);

    public const float SPEED = 0.02f;

    private void Update()
    {
        //EulerRotation = AMaths.
        TargetRotation();
        TargetMovement();
    }

    private void TargetRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // TODO: Move target yaw (left/right)
            _eulerAngle.y += 0.01f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // TODO: Move target yaw (left/right)
            _eulerAngle.y -= 0.01f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // TODO: Move target pitch (up/down)
            _eulerAngle.x += 0.01f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // TODO: Move target pitch (up/down)
            _eulerAngle.x -= 0.01f;
        }

        // convert euler to direction, gets forward vector
        _direction = AMaths.EulerAngleToDirection(_eulerAngle);

        // cross product gets right vector
        _crossProduct = AMaths.VectorCrossProduct(_constantUpward, _direction);

        var _other = transform.position + (_direction.ToUnityVector3() * 10);

        //draw ray 
        //Debug.DrawLine(_target.transform.position, _crossProduct.ToUnityVector3(), Color.blue);
        Debug.DrawLine(_target.transform.position, _other, Color.blue);
    }

    private void TargetMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // TODO: Move target forward in the direction it is rotated.
            // Add backwards movement too
            // transform.position to forward
            transform.position += new Vector3(_direction.x, _direction.y, _direction.z) * SPEED;           
        }
        if (Input.GetKey(KeyCode.S))
        {
            // TODO: Move target forward in the direction it is rotated.
            // Add backwards movement too
            // transform.position to forward
            transform.position -= new Vector3(_direction.x, _direction.y, _direction.z) * SPEED;
        }
        if (Input.GetKey(KeyCode.D))
        {
            // TODO: Move target to the right relative to the direction it is rotated.
            // Add leftwards movement too
            // transform.position to right
            transform.position += new Vector3(_crossProduct.x, _crossProduct.y, _crossProduct.z) * SPEED;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // TODO: Move target to the right relative to the direction it is rotated.
            // Add leftwards movement too
            // transform.position to right
            transform.position -= new Vector3(_crossProduct.x, _crossProduct.y, _crossProduct.z) * SPEED;
        }
    }
}
