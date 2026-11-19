using UnityEngine;
namespace NPCGames {
 public class NpcGamesCameraRig : MonoBehaviour {
  public Transform target;public float distance=6f,height=2.1f,sensitivity=2.2f;float yaw,pitch=14f;
  void Start(){Cursor.lockState=CursorLockMode.Locked;Cursor.visible=false;}
  public void SnapBehindTarget(){if(target!=null)yaw=target.eulerAngles.y;}
  void LateUpdate(){
   if(target==null)return;
   if(Input.GetKeyDown(KeyCode.Escape)){Cursor.lockState=CursorLockMode.None;Cursor.visible=true;}
   if(Input.GetMouseButtonDown(0)){Cursor.lockState=CursorLockMode.Locked;Cursor.visible=false;}
   if(Cursor.lockState==CursorLockMode.Locked){yaw+=Input.GetAxis("Mouse X")*sensitivity*3f;pitch-=Input.GetAxis("Mouse Y")*sensitivity*2f;pitch=Mathf.Clamp(pitch,-12,55);}
   Quaternion r=Quaternion.Euler(pitch,yaw,0);Vector3 focus=target.position+Vector3.up*height;Vector3 desired=focus-r*Vector3.forward*distance;
   Vector3 ray=desired-focus; if(Physics.SphereCast(focus,.22f,ray.normalized,out RaycastHit hit,distance,~(1<<target.gameObject.layer)))desired=hit.point+hit.normal*.25f;
   transform.position=desired;transform.rotation=Quaternion.LookRotation(focus-transform.position,Vector3.up);
  }
 }
}