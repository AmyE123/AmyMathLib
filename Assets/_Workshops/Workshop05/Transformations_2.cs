namespace Workshop
{
    using UnityEngine;
    using AmyMathLib.Vector;
    using AmyMathLib.Maths;
    using AmyMathLib.Matrix;

    /// <summary>
    /// A demo showcasing how to further use matrix manipulation with AmyMathLib
    /// </summary>
    public class Transformations_2 : MonoBehaviour
    {
        #region MESH MANIPULATION TASK
        [Header("Mesh Manipulation")]
        [SerializeField]
        private GameObject _meshGO;

        [SerializeField]
        private float _translation;

        [SerializeField]
        private float _angle;

        [SerializeField]
        private float _scale;

        Vector3[] ModelSpaceVerticies;
        #endregion //MESH MANIPULATION TASK

        private void Start()
        {
            #region MESH MANIPULATION TASK
            //Mesh filter is a component that stores info about the current mesh
            MeshFilter MF = _meshGO.GetComponent<MeshFilter>();

            //We get a copy of all the verticies
            ModelSpaceVerticies = MF.sharedMesh.vertices;

            #endregion //MESH MANIPULATION TASK
        }

        private void Update()
        {
            #region MESH MANIPULATION TASK

            //Define a new array with the correct size
            Vector3[] TransformedVerticies = new Vector3[ModelSpaceVerticies.Length];

            //Create our scaling matrix (2x, y, z)
            AMatrix4x4 S = new AMatrix4x4(new AVector3(1, 0, 0) * _scale, new AVector3(0, 1, 0), new AVector3(0, 0, 1), new AVector3(0, 0, 0));

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

            //Create our translation matrix
            AMatrix4x4 T = new AMatrix4x4(new AVector3(1, 0, 0), new AVector3(0, 1, 0), new AVector3(0, 0, 1), new AVector3(0, 0, _translation));

            AMatrix4x4 R = yawMatrix * (pitchMatrix * rollMatrix);
            AMatrix4x4 M = T * (R * S);

            //Transform each individual vertex
            for (int i = 0; i < TransformedVerticies.Length; i++)
            {
                var TRS = M * AMaths.ToAVector(ModelSpaceVerticies[i]);
                TransformedVerticies[i] = AMaths.ToUnityVector(TRS);
            }

            //Mesh filter is a component that stores info about the current mesh
            MeshFilter MF = _meshGO.GetComponent<MeshFilter>();

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
        }
    }
}
