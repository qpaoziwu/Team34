using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTarget2D : MonoBehaviour
{
    //[Range(1, 2)]
    //public int AimMode;
    //public Vector3 BoxLocation;
    //public Vector3 BoxSize;
    //public GameObject AimTarget;

    public CircleCollider2D m_Box;
    public bool stopSorting;

    public List<int> TaggedLayers;
    public List<Transform> TargetsByRange;

    void Awake()
    {
        //Set Ref. to Box Collider
        if (m_Box != null)
        {
            m_Box = GetComponent<CircleCollider2D>();
        }
        else
        {
            print("BoxCollider not found, please check setup.");
        }

        //Set Aim Mode
        //SetBoxTransform();

        //Set Target Layers
        AddLayersToList();
    }

    //Read TargetsByRange and return first item
    //Return T if List is null
    public Transform ClosestTarget(Transform T)
    {
        if (TargetsByRange.Count > 0)
        {
            return TargetsByRange[0];
        }
        else return T;
    }

    //Set the layers that can be targeted
    private void AddLayersToList()
    {
        TaggedLayers.Add(8); //Libee Layer
        TaggedLayers.Add(10); //Libee Layer
        TaggedLayers.Add(11); //Enemy Layer
        //TaggedLayers.Add(LayerMask.GetMask("Collectibles")); //Libee Layer
        //TaggedLayers.Add(LayerMask.GetMask("Enemies")); //Enemy Layer
    }

    //Add all layered objects to TargetsInRange, then Sort by distance
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!stopSorting)
        {
            for (int i = TaggedLayers.Count - 1; i >= 0; i--)
            {
                if (other.gameObject.layer == (TaggedLayers[i]))
                {
                    if (!TargetsByRange.Contains(other.gameObject.transform))
                    {
                        TargetsByRange.Add(other.gameObject.transform);
                        print(other.gameObject + " in range");
                        SortListByRange();
                    }
                }
            }
        }
    }

    //Remove all objects from TargetsInRange, then Sort by distance
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!stopSorting)
        {
            if (TargetsByRange.Contains(other.gameObject.transform))
            {
                TargetsByRange.Remove(other.gameObject.transform);
                SortListByRange();
            }
        }
    }

    private void SortListByRange()
    {
        for (int i = TargetsByRange.Count - 1; i >= 0; i--)
        {
            if (TargetsByRange[i] == null)
            {
                TargetsByRange.Remove(TargetsByRange[i]);

            }
        }
            TargetsByRange.Sort((x, y) => { return (gameObject.transform.position - x.transform.position).sqrMagnitude.CompareTo((gameObject.transform.position - y.transform.position).sqrMagnitude); });   
    }

}
//Aim Mode 1:
//  Set BoxCollider to Scale and Center with Vector3
//Aim Mode 2:
//  Set BoxCollider to Scale and Center with TongueTarget
//private void SetBoxTransform()
//{
//    if (AimMode == 1)
//    {
//        m_Box.size = BoxSize;
//        m_Box.center = Vector3.forward * BoxSize.z / 2f;
//    }

//    if (AimMode == 2)
//    {
//        m_Box.size = new Vector3(BoxSize.x, BoxSize.y, AimTarget.transform.position.z);
//        m_Box.center = Vector3.forward * AimTarget.transform.position.z / 2f;
//    }
//}