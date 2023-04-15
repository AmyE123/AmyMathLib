using UnityEngine;
using AmyMathLib.Vector;
using AmyMathLib.Maths;
using AmyMathLib.Matrix;

public class Transformations_1 : MonoBehaviour
{
    #region MESH MANIPULATION TASK
    [Header("Mesh Manipulation")]
    [SerializeField]
    private GameObject _translateMeshGO;

    [SerializeField]
    private GameObject _rotateMeshGO;

    [SerializeField]
    private GameObject _scaleMeshGO;

    [SerializeField]
    private float _translation;

    [SerializeField]
    private float _angle;

    [SerializeField]
    private float _scale;

    Vector3[] ModelSpaceVerticiesTranslation;
    Vector3[] ModelSpaceVerticiesRotation;
    Vector3[] ModelSpaceVerticiesScale;
    #endregion //MESH MANIPULATION TASK

    #region LERP TASK
    [Header("Lerping")]
    [SerializeField]
    private GameObject _gameObject;

    [SerializeField]
    private float _speed = 5;

    [SerializeField]
    private Transform _pointA;

    [SerializeField]
    private Transform _pointB;

    private bool _canLerp;

    [SerializeField]
    private AMatrix4x4 _matrix;
    #endregion //LERP TASK

    private void Start()
    {
        #region MESH MANIPULATION TASK
        //Mesh filter is a component that stores info about the current mesh
        MeshFilter MF_trans = _translateMeshGO.GetComponent<MeshFilter>();
        MeshFilter MF_rot = _rotateMeshGO.GetComponent<MeshFilter>();
        MeshFilter MF_scale = _scaleMeshGO.GetComponent<MeshFilter>();

        //We get a copy of all the verticies
        ModelSpaceVerticiesTranslation = MF_trans.sharedMesh.vertices;
        ModelSpaceVerticiesRotation = MF_rot.sharedMesh.vertices;
        ModelSpaceVerticiesScale = MF_scale.sharedMesh.vertices;

        #endregion //MESH MANIPULATION TASK
    }

    private void Update()
    {
        #region MESH MANIPULATION TASK

        #region Translation
        //Define a new array with the correct size
        Vector3[] TransformedVerticiesTranslation = new Vector3[ModelSpaceVerticiesTranslation.Length];

        //Create our translation matrix
        AMatrix4x4 translationMatrix = new AMatrix4x4(new AVector3(1, 0, 0), new AVector3(0, 1, 0), new AVector3(0, 0, 1), new AVector3(0, 0, _translation));

        //Transform each individual vertex
        for (int i = 0; i < TransformedVerticiesTranslation.Length; i++)
        {
            var AModelSpaceVetex = AMaths.ToAVector(ModelSpaceVerticiesTranslation[i]);
            AVector4 AModelSpaceVertex4 = new AVector4(AModelSpaceVetex.x, AModelSpaceVetex.y, AModelSpaceVetex.z, 1);
            var Transformed4 = translationMatrix * AModelSpaceVertex4;
            AVector3 Transformed3 = new AVector3(Transformed4.x, Transformed4.y, Transformed4.z);
            TransformedVerticiesTranslation[i] = AMaths.ToUnityVector(Transformed3);
        }

        //Mesh filter is a component that stores info about the current mesh
        MeshFilter MF_trans = _translateMeshGO.GetComponent<MeshFilter>();

        //Assign our new verticies
        MF_trans.mesh.vertices = TransformedVerticiesTranslation;

        //These final steps are cometimes necessary to make the mesh look correct
        MF_trans.mesh.RecalculateNormals();
        MF_trans.mesh.RecalculateBounds();
        #endregion //Translation

        #region Rotation
        //Define a new array with the correct size
        Vector3[] TransformedVerticiesRotation = new Vector3[ModelSpaceVerticiesRotation.Length];

        //Create our rotation matrix, this time rotating around the Roll, Pitch and Yaw in that order
        AMatrix4x4 rollMatrix = new AMatrix4x4(
            new AVector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0),
            new AVector3(-Mathf.Sin(_angle), Mathf.Cos(_angle), 0),
            new AVector3(0, 0, 1),
            new AVector3(0, 0, 0));

        AMatrix4x4 pitchMatrix = new AMatrix4x4(
            new AVector3(1, 0, 0),
            new AVector3(0, Mathf.Cos(_angle), Mathf.Sin(_angle)),
            new AVector3(0, -Mathf.Sin(_angle), Mathf.Cos(_angle)),
            new AVector3(0, 0, 0));

        AMatrix4x4 yawMatrix = new AMatrix4x4(
            new AVector3(Mathf.Cos(_angle), 0, -Mathf.Sin(_angle)),
            new AVector3(0, 1, 0),
            new AVector3(Mathf.Sin(_angle), 0, Mathf.Cos(_angle)),
            new AVector3(0, 0, 0));

        //Transform each individual vertex
        for (int i = 0; i < TransformedVerticiesRotation.Length; i++)
        {
            AVector3 RolledVetrex = rollMatrix * AMaths.ToAVector(ModelSpaceVerticiesRotation[i]);
            AVector3 PitchedVertex = pitchMatrix * RolledVetrex;
            AVector3 YawedVertex = yawMatrix * PitchedVertex;

            TransformedVerticiesRotation[i] = AMaths.ToUnityVector(YawedVertex);
        }

        //Mesh filter is a component that stores info about the current mesh
        MeshFilter MF_rot = _rotateMeshGO.GetComponent<MeshFilter>();

        //Assign our new verticies
        MF_rot.mesh.vertices = TransformedVerticiesRotation;

        //These final steps are cometimes necessary to make the mesh look correct
        MF_rot.mesh.RecalculateNormals();
        MF_rot.mesh.RecalculateBounds();
        #endregion //Rotation

        #region Scale
        //Define a new array with the correct size
        Vector3[] TransformedVerticiesScale = new Vector3[ModelSpaceVerticiesScale.Length];

        //Create our scaling matrix (2x, y, z)
        AMatrix4x4 scaleMatrix = new AMatrix4x4(new AVector3(1, 0, 0) * _scale, new AVector3(0, 1, 0), new AVector3(0, 0, 1), new AVector3(0, 0, 0));

        //Transform each individual vertex
        for (int i = 0; i < TransformedVerticiesScale.Length; i++)
        {
            var AModelSpaceVetex = AMaths.ToAVector(ModelSpaceVerticiesScale[i]);
            TransformedVerticiesScale[i] = AMaths.ToUnityVector(scaleMatrix * AModelSpaceVetex);
        }

        //Mesh filter is a component that stores info about the current mesh
        MeshFilter MF_scale = _scaleMeshGO.GetComponent<MeshFilter>();

        //Assign our new verticies
        MF_scale.mesh.vertices = TransformedVerticiesScale;

        //These final steps are cometimes necessary to make the mesh look correct
        MF_scale.mesh.RecalculateNormals();
        MF_scale.mesh.RecalculateBounds();
        #endregion //Scale

        //Vector3 F = transform.position - _player.transform.position;
        //AVector3 R = AMaths.VectorCrossProduct(AMaths.ToAVector(Vector3.up), AMaths.ToAVector(F));
        //AVector3 U = AMaths.VectorCrossProduct(AMaths.ToAVector(F), R);

        // TODO: Use R and U to recalculate position of verticies or anything for that matter

        #endregion  //MESH MANIPULATION TASK

        #region LERP TASK      
        if (Input.GetKeyDown(KeyCode.L))
        {
            _canLerp = !_canLerp;
        }

        if (_canLerp)
        {
            Lerp(AMaths.ToAVector(_pointB.position));
        }
        if (!_canLerp)
        {
            Lerp(AMaths.ToAVector(_pointA.position));
        }
        #endregion //LERP TASK
    }

    #region LERP TASK
    void Lerp(AVector3 target)
    {
        var lerp = AMaths.Lerp(AMaths.ToAVector(_gameObject.transform.position), target, Time.deltaTime * _speed);

        _gameObject.transform.position = lerp.ToUnityVector3();
    }
    #endregion //LERP TASK
}
