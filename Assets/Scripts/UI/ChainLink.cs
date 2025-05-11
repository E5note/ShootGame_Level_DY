using UnityEngine;
 public class ChainLink : MonoBehaviour
 {
     private LineRenderer line; 
     public Transform nextLink;
     private void Start()
     {
         line = GetComponent<LineRenderer>();
     }
     private void Update()
     {
         if (line != null)
         {
             
             line.SetPositions(new Vector3[] { transform.position, nextLink.position });

         }
     }

     private void OnJointBreak2D(Joint2D brokenJoint)
     {
         Destroy(line);
     }
 }