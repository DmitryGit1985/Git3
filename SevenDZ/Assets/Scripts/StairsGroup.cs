using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsGroup : MonoBehaviour
{
    [SerializeField] private Transform[] stairs;
    [SerializeField] private Transform floorRoof;
    private Transform currentMiddleStair;
    private int middleStairStartIndex=1;
    private Character character;
    public Transform[] Stairs { get => stairs; }
    public Transform CurrentMiddleStair { get => currentMiddleStair; set => currentMiddleStair = value; }
    public Transform FloorRoof { get => floorRoof; }
    public Character Character { get => character = character ?? FindObjectOfType<Character>(); }

    void Start()
    {
        currentMiddleStair = stairs[middleStairStartIndex];
    }
}