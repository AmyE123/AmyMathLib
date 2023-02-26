using UnityEngine;
using AmyMathLib.Vector;
using AmyMathLib.Maths;
using AmyMathLib.Matrix;
using UnityEngine.UIElements;

public class Transformations_1 : MonoBehaviour
{
    #region MESH MANIPULATION TASK
    [SerializeField]
    private GameObject _player;

    Vector3[] ModelSpaceVerticies;
    #endregion //MESH MANIPULATION TASK

    #region LERP TASK
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
    #endregion //LERP TASK

    private void Start()
    {
        #region MESH MANIPULATION TASK
        //Mesh filter is a component that stores info about the current mesh
        MeshFilter MF = GetComponent<MeshFilter>();

        //We get a copy of all the verticies
        ModelSpaceVerticies = MF.sharedMesh.vertices;
        #endregion //MESH MANIPULATION TASK
        #region LERP TASK
        _pursuerPos = _pursuer.transform.position;
        _evaderPos= _evader.transform.position;
        #endregion //LERP TASK
    }


    private void Update()
    {
        #region MESH MANIPULATION TASK
        //Define a new array with the correct size
        Vector3[] TransformedVerticies = new Vector3[ModelSpaceVerticies.Length];

        //Create our rotation matrix
        AMatrix4x4 rotationMatrix = new AMatrix4x4(new AVector3(1, 0, 0), new AVector3(0, 1, 0), new AVector3(0, 0, 1), new AVector3(0, 0, 0));

        //Create our scaling matrix (2x, y, z)
        AMatrix4x4 scaleMatrix = new AMatrix4x4(new AVector3(1, 0, 0) * 2.0f, new AVector3(0, 1, 0), new AVector3(0, 0, 1), new AVector3(0, 0, 0));

        //Create our translation matrix
        AMatrix4x4 translationMatrix = new AMatrix4x4(new AVector3(1, 0, 0), new AVector3(0, 1, 0), new AVector3(0, 0, 1), new AVector3(transform.position.x, transform.position.y, transform.position.z));

        //Transform each individual vertex
        for (int i = 0; i < TransformedVerticies.Length; i++)
        {
            var AModelSpaceVetex = AMaths.ToAVector(ModelSpaceVerticies[i]);
            TransformedVerticies[i] = AMaths.ToUnityVector(rotationMatrix * AModelSpaceVetex);
        }

        //Mesh filter is a component that stores info about the current mesh
        MeshFilter MF = GetComponent<MeshFilter>();

        //Assign our new verticies
        MF.mesh.vertices = TransformedVerticies;

        //These final steps are cometimes necessary to make the mesh look correct
        MF.mesh.RecalculateNormals();
        MF.mesh.RecalculateBounds();

        //Vector3 F = transform.position - _player.transform.position;
        //AVector3 R = AMaths.VectorCrossProduct(AMaths.ToAVector(Vector3.up), AMaths.ToAVector(F));
        //AVector3 U = AMaths.VectorCrossProduct(AMaths.ToAVector(F), R);

        // TODO: Use R and U to recalculate position of verticies or anything for that matter

        #endregion  //MESH MANIPULATION TASK

        #region LERP TASK
        if (Input.GetKeyDown(KeyCode.L))
        {
            EvaderMovement();
            PursuerMovement();
        }
        #endregion //LERP TASK
    }

    #region LERP TASK
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
#endregion //LERP TASK
}
