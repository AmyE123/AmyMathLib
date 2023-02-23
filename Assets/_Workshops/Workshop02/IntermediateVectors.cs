using UnityEngine;
using AmyMathLib.Vector;
using AmyMathLib.Maths;

public class IntermediateVectors : MonoBehaviour
{
    [SerializeField]
    private GameObject _evader;

    [SerializeField]
    private GameObject _pursuer;


    [SerializeField]
    private float _evaderSpeed = 10;

    [SerializeField]
    private float _pursuerSpeed = 5;

    private AVector3 _previousEvaderPosition;
    private AVector3 _evaderPosition;

    private void Start()
    {
        _previousEvaderPosition = AMaths.ToAVector(_evader.transform.position);       
    }

    void Update()
    {
        _evaderPosition = AMaths.ToAVector(transform.position);

        EvaderMovement();
        PursuerMovement();
        
        _previousEvaderPosition = AMaths.ToAVector(_evader.transform.position);
    }

    void PursuerMovement()
    {
        AVector3 pursuerPosition = AMaths.ToAVector(_pursuer.transform.position);
        AVector3 pursuerDirection = AVector3.SubtractVector3(AMaths.ToAVector(_evader.transform.position), pursuerPosition);
        AVector3 pursuerDirectionNormalised = pursuerDirection.NormalizeVector();

        _pursuer.transform.position += AMaths.ToUnityVector(pursuerDirectionNormalised) * _pursuerSpeed * Time.deltaTime;        
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
