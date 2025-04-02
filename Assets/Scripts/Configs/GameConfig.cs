using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/Configs/GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private int _fieldHeight;
    public int FieldHeight => _fieldHeight;

    [SerializeField] private int _fieldWidth;
    public int FieldWidth => _fieldWidth;

    [SerializeField] private Vector3 _cellOffset;
    public Vector3 CellOffset => _cellOffset;

    [SerializeField] private Vector3 _fieldPosition;
    public Vector3 FieldPosition => _fieldPosition;
}
