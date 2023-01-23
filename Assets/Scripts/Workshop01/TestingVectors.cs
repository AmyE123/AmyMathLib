using UnityEngine;
using AmyMathLib.Vector;
using System.Collections.Generic;

public class TestingVectors : MonoBehaviour
{
    [SerializeField]
    GameObject[] GameObjects;

    [SerializeField]
    List<AVector3> GameObjectPositions = new List<AVector3>();

    [SerializeField]
    List<float> Magnitudes = new List<float>();

    void Start()
    {
        GameObjectPositions.Add(AVector3.ToMyVector(GameObjects[0].transform.position));
        GameObjectPositions.Add(AVector3.ToMyVector(GameObjects[1].transform.position));

        Magnitudes.Add(GameObjectPositions[0].GetMagnitude());
        Magnitudes.Add(GameObjectPositions[1].GetMagnitude());

        Debug.Log($"AE: GO1_MAG - {Magnitudes[0]} / GO2_MAG - {Magnitudes[1]}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObjects[1].transform.position = GameObjectPositions[0].ToUnityVector();
        }
    }
}
