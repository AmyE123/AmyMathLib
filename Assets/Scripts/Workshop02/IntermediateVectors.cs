using UnityEngine;
using AmyMathLib.Vector;

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
    private AVector3 _evaderVelocity = new AVector3(0, 0, 0);

    private void Start()
    {
        //previous pos = transform.pos
        _previousEvaderPosition = AVector3.ToAVector3(_evader.transform.position);

        

    }

    void Update()
    {
        //ev pos = transform.pos
        _evaderPosition = AVector3.ToAVector3(transform.position);

        EvaderMovement();
        PursuerMovement();

       
        //ev velo = subtract (ev pos - previous pos)
        _evaderVelocity = AVector3.SubtractVector(_evaderPosition, _previousEvaderPosition);
        
        // prev pos = transform.pos
        _previousEvaderPosition = AVector3.ToAVector3(_evader.transform.position);
    }

    void PursuerMovement()
    {
        AVector3 pursuerPosition = AVector3.ToAVector3(_pursuer.transform.position);
        AVector3 pursuerDirection = AVector3.SubtractVector(AVector3.ToAVector3(_evader.transform.position), pursuerPosition);
        AVector3 pursuerDirectionNormalised = pursuerDirection.NormalizeVector();

        //float dotProduct = AVector3.GetDotProduct(pursuerDirectionNormalised, _evaderVelocity.NormalizeVector(), false);

        _pursuer.transform.position += AVector3.ToUnityVector3(pursuerDirectionNormalised) * _pursuerSpeed * Time.deltaTime;        
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
