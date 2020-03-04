using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] int libeeLayer;

   // [SerializeField] SortSelectLibee LibeeSorter;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == libeeLayer)
        {
          //  LibeeSorter.GatherDeadLibees(other.gameObject.transform);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == libeeLayer)
        {
          //  LibeeSorter.GatherDeadLibees(other.gameObject.transform);
        }
    }
}
