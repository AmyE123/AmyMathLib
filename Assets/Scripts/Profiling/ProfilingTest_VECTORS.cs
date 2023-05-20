namespace Profiling
{
    using AmyMathLib.Vector;
    using UnityEngine;

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