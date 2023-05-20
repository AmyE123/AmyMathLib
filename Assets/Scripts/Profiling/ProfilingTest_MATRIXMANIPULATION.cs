namespace Profiling
{
    using AmyMathLib.Maths;
    using AmyMathLib.Matrix;
    using AmyMathLib.Vector;
    using UnityEngine;

    /// <summary>
    /// A profiling test of Unity's Transformation vs AmyMathLib's Matrix Manipulation
    /// </summary>
    public class ProfilingTest_MATRIXMANIPULATION : MonoBehaviour
    {
        [Header("Unity Transformation")]
        public GameObject Unity;

        [Header("Mesh Manipulation")]
        public GameObject AmyMathsLib;
        private Vector3[] _modelSpaceVerticies;

        private void Start()
        {
            MeshFilter mf = AmyMathsLib.GetComponent<MeshFilter>();
            _modelSpaceVerticies = mf.sharedMesh.vertices;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Unity.transform.localScale = new Vector3(2, 2, 2);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                ScaleObject(2, 2, 2);
            }
        }

        private void ScaleObject(float x, float y, float z)
        {
            //Define a new array with the correct size
            Vector3[] TransformedVerticiesScale = new Vector3[_modelSpaceVerticies.Length];

            //Create our scaling matrix (2x, y, z)
            AMatrix4x4 scaleMatrix = new AMatrix4x4(new AVector3(1, 0, 0) * x, new AVector3(0, 1, 0) * y, new AVector3(0, 0, 1) * z, new AVector3(0, 0, 0));

            //Transform each individual vertex
            for (int i = 0; i < TransformedVerticiesScale.Length; i++)
            {
                var AModelSpaceVetex = AMaths.ToAVector(_modelSpaceVerticies[i]);
                TransformedVerticiesScale[i] = AMaths.ToUnityVector(scaleMatrix * AModelSpaceVetex);
            }

            //Mesh filter is a component that stores info about the current mesh
            MeshFilter MF_scale = AmyMathsLib.GetComponent<MeshFilter>();

            //Assign our new verticies
            MF_scale.mesh.vertices = TransformedVerticiesScale;

            //These final steps are cometimes necessary to make the mesh look correct
            MF_scale.mesh.RecalculateNormals();
            MF_scale.mesh.RecalculateBounds();
        }
    }
}
