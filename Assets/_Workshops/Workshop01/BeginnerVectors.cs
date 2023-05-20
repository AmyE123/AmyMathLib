namespace Workshop
{
    using UnityEngine;
    using AmyMathLib.Vector;
    using AmyMathLib.Maths;
    using System.Collections.Generic;

    /// <summary>
    /// A demo showcasing how to use vectors with AmyMathLib
    /// </summary>
    public class BeginnerVectors : MonoBehaviour
    {
        [SerializeField]
        GameObject[] GameObjects;

        [SerializeField]
        List<AVector3> GameObjectPositions = new List<AVector3>();

        [SerializeField]
        List<float> Magnitudes = new List<float>();

        private void Start()
        {
            GameObjectPositions.Add(AMaths.ToAVector(GameObjects[0].transform.position));
            GameObjectPositions.Add(AMaths.ToAVector(GameObjects[1].transform.position));

            Magnitudes.Add(GameObjectPositions[0].GetLength());
            Magnitudes.Add(GameObjectPositions[1].GetLength());

            Debug.Log($"AE: Game Object 1 Magnitude - {Magnitudes[0]} / Game Object 2 Magnitude - {Magnitudes[1]}");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObjects[1].transform.position = GameObjectPositions[0].ToUnityVector3();
            }
        }
    }
}
