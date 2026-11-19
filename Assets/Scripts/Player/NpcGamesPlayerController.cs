using UnityEngine;
namespace NPCGames {
 [RequireComponent(typeof(CharacterController))]
 public class NpcGamesPlayerController : MonoBehaviour {
  public float walkSpeed=5f,runSpeed=8f,jumpHeight=1.35f,gravity=-22f; public Transform viewCamera; CharacterController motor; float verticalSpeed;
  void Awake(){motor=GetComponent<CharacterController>();}
  void Update(){
   float x=Input.GetAxisRaw("Horizontal"),z=Input.GetAxisRaw("Vertical");
   Vector3 forward=viewCamera?Vector3.ProjectOnPlane(viewCamera.forward,Vector3.up).normalized:transform.forward;
   Vector3 right=viewCamera?Vector3.ProjectOnPlane(viewCamera.right,Vector3.up).normalized:transform.right;
   Vector3 dir=(forward*z+right*x).normalized;
   if(dir.sqrMagnitude>.01f)transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(dir),Time.deltaTime*12f);
   float speed=Input.GetKey(KeyCode.LeftShift)?runSpeed:walkSpeed;
   if(motor.isGrounded&&verticalSpeed<0)verticalSpeed=-2f;
   if(motor.isGrounded&&Input.GetKeyDown(KeyCode.Space))verticalSpeed=Mathf.Sqrt(jumpHeight*-2f*gravity);
   verticalSpeed+=gravity*Time.deltaTime;Vector3 v=dir*speed;v.y=verticalSpeed;motor.Move(v*Time.deltaTime);
  }
 }
}