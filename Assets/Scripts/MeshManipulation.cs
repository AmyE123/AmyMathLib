namespace BlockyRoad
{
    using AmyMathLib.Maths;
    using AmyMathLib.Matrix;
    using AmyMathLib.Vector;
    using UnityEngine;

    /// <summary>
    /// A class responsible for manipulating meshes
    /// using the maths, matrix and vector classes
    /// under AmysMathsLib library
    /// </summary>
    public class MeshManipulation : MonoBehaviour
    {
        private GameObject _gameObject;

        private Vector3[] ModelSpaceVertices;

        private float X, Y, Z;
        private float go_X, go_Y, go_Z;

        /// <summary>
        /// A manual bool that can be changed
        /// to stop a mesh from scaling
        /// </summary>
        public bool StopScaling;

        private void Start()
        {
            _gameObject = gameObject;

            go_X = _gameObject.transform.localScale.x;
            go_Y = _gameObject.transform.localScale.y;
            go_Z = _gameObject.transform.localScale.z;
        }

        private bool InitializeMesh(GameObject gameObject)
        {
            MeshFilter mf = gameObject.GetComponent<MeshFilter>();

            if (mf == null)
            {
                Debug.Log("[ERROR-NULLREF]: GameObject doesn't contain MeshFilter");
                return false;
            }

            ModelSpaceVertices = mf.sharedMesh.vertices;

            if (ModelSpaceVertices.Length <= 0)
            {
                Debug.Log("[ERROR-MESHFILTER]: MeshFilter had an issue setting the vertices");
                return false;
            }

            go_X = gameObject.transform.localScale.x;
            go_Y = gameObject.transform.localScale.y;
            go_Z = gameObject.transform.localScale.z;

            return true;
        }

        /// <summary>
        /// An estimated value for the time it takes
        /// for the scalar lerp to be complete.
        /// </summary>
        /// <param name="lerpSpeed">The desired speed</param>
        /// <returns>A correct timer value</returns>
        public float GetMeshTimerValue(float lerpSpeed)
        {
            //TODO: Find another solution as this isn't right.
            return lerpSpeed / 3;
        }

        private float Lerp(float a, float b, float speed)
        {
            var lerp = AMaths.Lerp(a, b, Time.deltaTime * speed);

            return lerp;
        }

        /// <summary>
        /// Scaling an object using mesh manipulation and apply linear interpolation to the scaling
        /// </summary>
        /// <param name="scaleX">The desired scale of X</param>
        /// <param name="scaleY">The desired scale of Y</param>
        /// <param name="scaleZ">The desired scale of Z</param>
        /// <param name="speed">The speed you want the lerp to go</param>
        /// <returns>A value indicating whether the scaling could be completed</returns>
        public bool LerpScaleObject(float scaleX, float scaleY, float scaleZ, float speed)
        {
            if (InitializeMesh(_gameObject) && !StopScaling)
            {
                //Initialize lerping values
                var newSpeed = speed * 10;

                X = Lerp(go_X, scaleX, Time.deltaTime * newSpeed);
                Y = Lerp(go_Y, scaleY, Time.deltaTime * newSpeed);
                Z = Lerp(go_Z, scaleZ, Time.deltaTime * newSpeed);

                //Define a new array with the correct size
                Vector3[] TransformedVerticiesScale = new Vector3[ModelSpaceVertices.Length];

                //Create our scaling matrix (2x, y, z)
                AMatrix4x4 scaleMatrix = new AMatrix4x4(new AVector3(1, 0, 0) * X, new AVector3(0, 1, 0) * Y, new AVector3(0, 0, 1) * Z, new AVector3(0, 0, 0));

                //Transform each individual vertex
                for (int i = 0; i < TransformedVerticiesScale.Length; i++)
                {
                    var AModelSpaceVetex = AMaths.ToAVector(ModelSpaceVertices[i]);
                    TransformedVerticiesScale[i] = AMaths.ToUnityVector(scaleMatrix * AModelSpaceVetex);
                }

                //Mesh filter is a component that stores info about the current mesh
                MeshFilter MF_scale = _gameObject.GetComponent<MeshFilter>();

                //Assign our new verticies
                MF_scale.mesh.vertices = TransformedVerticiesScale;

                //These final steps are cometimes necessary to make the mesh look correct
                MF_scale.mesh.RecalculateNormals();
                MF_scale.mesh.RecalculateBounds();

                return true;
            }
            else if (StopScaling)
            {
                Debug.Log("[LOG]: Lerp scale completed");
                return false;
            }
            else
            {
                Debug.Log("[ERROR]: Mesh couldn't be initialized");
                return false;
            }
        }

        /// <summary>
        /// Scaling an object using mesh manipulation
        /// </summary>
        /// <param name="scaleX">The desired scale of X</param>
        /// <param name="scaleY">The desired scale of Y</param>
        /// <param name="scaleZ">The desired scale of Z</param>
        /// <returns>A value indicating whether the scaling could be completed</returns>
        public void ScaleObject(float scaleX, float scaleY, float scaleZ)
        {
            if (InitializeMesh(_gameObject) && !StopScaling)
            {
                //Define a new array with the correct size
                Vector3[] TransformedVerticiesScale = new Vector3[ModelSpaceVertices.Length];

                //Create our scaling matrix (2x, y, z)
                AMatrix4x4 scaleMatrix = new AMatrix4x4(new AVector3(1, 0, 0) * scaleX, new AVector3(0, 1, 0) * scaleY, new AVector3(0, 0, 1) * scaleZ, new AVector3(0, 0, 0));

                //Transform each individual vertex
                for (int i = 0; i < TransformedVerticiesScale.Length; i++)
                {
                    var AModelSpaceVetex = AMaths.ToAVector(ModelSpaceVertices[i]);
                    TransformedVerticiesScale[i] = AMaths.ToUnityVector(scaleMatrix * AModelSpaceVetex);
                }

                //Mesh filter is a component that stores info about the current mesh
                MeshFilter MF_scale = _gameObject.GetComponent<MeshFilter>();

                //Assign our new verticies
                MF_scale.mesh.vertices = TransformedVerticiesScale;

                //These final steps are cometimes necessary to make the mesh look correct
                MF_scale.mesh.RecalculateNormals();
                MF_scale.mesh.RecalculateBounds();
            }
        }
    }
}

