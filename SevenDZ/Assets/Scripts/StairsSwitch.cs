using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSwitch : MonoBehaviour
{
    [SerializeField] private Collider coll;
    [SerializeField] private StairsGroup stairsGroup;
    private float direction;
    private float fixedOffset=4.2f;
    
    private void OnTriggerExit(Collider other)
    {
        direction = other.transform.position.z - transform.position.z;//minus - upstairs, plus downstairs
        //float offset;
        if (other == stairsGroup.Character.CharacterController && coll.transform.parent == stairsGroup.CurrentMiddleStair)
        {
            Transform[] offsetArray = new Transform[stairsGroup.Stairs.Length];
            int offsetNumbers = 1;
            for (int i = 0; i < stairsGroup.Stairs.Length; i++)
            {
                offsetArray[i] = stairsGroup.Stairs[(i + offsetNumbers) % stairsGroup.Stairs.Length];

                if (coll.transform.parent == stairsGroup.Stairs[i])
                {
                    stairsGroup.CurrentMiddleStair = stairsGroup.Stairs[i + 1];
                    float offset = fixedOffset + stairsGroup.Stairs[i + 1].transform.position.y;
                    stairsGroup.Stairs[i - 1].transform.position = new Vector3(0.0f, offset, 0.0f);

                    Debug.Log(stairsGroup.CurrentMiddleStair);
                }
            }
            for (int i = 0; i < stairsGroup.Stairs.Length; i++)
            {
                stairsGroup.Stairs[i] = offsetArray[i];
            }
            stairsGroup.FloorRoof.transform.position= new Vector3(stairsGroup.FloorRoof.transform.position.x, stairsGroup.FloorRoof.transform.position.y + 4.2f, stairsGroup.FloorRoof.transform.position.z);

        }
    }
}
