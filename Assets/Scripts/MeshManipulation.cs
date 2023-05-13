namespace BlockyRoad
{
    using AmyMathLib.Maths;
    using AmyMathLib.Matrix;
    using AmyMathLib.Vector;
    using UnityEngine;

    public class MeshManipulation : MonoBehaviour
    {
        const float LERP_SPEED = 20;

        //TODO: Remove once implementation works
        [SerializeField]
        private GameObject _gameObject;

        [SerializeField]
        private float _scaleX, _scaleY, _scaleZ;

        Vector3[] ModelSpaceVertices;

        private bool _canLerp;
        public bool StopScaling;

        [SerializeField]
        private float _timer;

        private float X, Y, Z;
        private float go_X, go_Y, go_Z;

        private void Start()
        {
            _gameObject = gameObject;

            go_X = _gameObject.transform.localScale.x;
            go_Y = _gameObject.transform.localScale.y;
            go_Z = _gameObject.transform.localScale.z;

            // The time it takes for the scalar lerp to be complete
            _timer = LERP_SPEED / 3;
        }

        public float GetMeshTimerValue(float lerpSpeed)
        {
            // A estimated value for the time
            // it takes for the scalar lerp to be complete
            return lerpSpeed / 3;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _canLerp = true;                               
            }

            if (_canLerp)
            {
                ScaleObject(0.1f, 0.1f, 0.1f, LERP_SPEED);
                
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    StopScaling = true;
                    _canLerp= false;
                }
            }
        }

        public bool ScaleObject(float scaleX, float scaleY, float scaleZ, float speed)
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

        private float Lerp(float a, float b, float speed)
        {
            var lerp = AMaths.Lerp(a, b, Time.deltaTime * speed);

            return lerp;
        }
    }
}

