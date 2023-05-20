namespace Profiling
{
    using AmyMathLib.Vector;
    using UnityEngine;

    /// <summary>
    /// A profiling test of Unity's Transformation vs AmyMathLib's AVectors
    /// </summary>
    public class ProfilingTest_VECTORS : MonoBehaviour
    {
        public GameObject Unity;
        public GameObject AmyMathsLib;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Unity.transform.position = new Vector3(-1, 0, 1);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                var AMathsVec = new AVector3(1, 0, 1);
                AmyMathsLib.transform.position = AMathsVec.ToUnityVector3();
            }
        }
    }
}