using UnityEngine;
using AmyMathLib.Vector;
using AmyMathLib.Maths;
using AmyMathLib.Matrix;

public class Transformations_1 : MonoBehaviour
{
    [SerializeField]
    private GameObject _evader;

    [SerializeField]
    private GameObject _pursuer;

    [SerializeField]
    private float _evaderSpeed = 10;

    [SerializeField]
    private float _pursuerSpeed = 5;

    [SerializeField]
    private Transform _from;

    [SerializeField]
    private Transform _to;

    [SerializeField]
    private AMatrix4x4 _matrix;

    private Vector3 _pursuerPos;
    private Vector3 _evaderPos;

    private void Start()
    {       
        _pursuerPos = _pursuer.transform.position;
        _evaderPos= _evader.transform.position;
    }

    private void Update()
    {
        EvaderMovement();
        PursuerMovement();
    }

    void PursuerMovement()
    {
        var from = AMaths.ToAVector(_pursuerPos);
        var to = AMaths.ToAVector(_evaderPos);

        var lerp = AMaths.Lerp(from, to, Time.deltaTime * _pursuerSpeed);

        _pursuer.transform.position = lerp.ToUnityVector3();
    }

    void EvaderMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _evader.transform.Translate(Vector3.forward * Time.deltaTime * _evaderSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _evader.transform.Translate(-1 * Vector3.forward * Time.deltaTime * _evaderSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _evader.transform.Rotate(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _evader.transform.Rotate(0, 1, 0);
        }
    }
}
