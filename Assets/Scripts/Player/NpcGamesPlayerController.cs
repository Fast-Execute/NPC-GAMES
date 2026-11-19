using UnityEngine;
namespace NPCGames {
 [RequireComponent(typeof(CharacterController))]
 public class NpcGamesPlayerController : MonoBehaviour {
  public float walkSpeed=5f,runSpeed=8f,jumpHeight=1.35f,gravity=-22f; CharacterController motor; float verticalSpeed;
  void Awake(){motor=GetComponent<CharacterController>();}
  void Update(){float x=Input.GetAxisRaw("Horizontal"),z=Input.GetAxisRaw("Vertical");Vector3 dir=(transform.right*x+transform.forward*z).normalized;float speed=Input.GetKey(KeyCode.LeftShift)?runSpeed:walkSpeed;if(motor.isGrounded&&verticalSpeed<0)verticalSpeed=-2;if(motor.isGrounded&&Input.GetKeyDown(KeyCode.Space))verticalSpeed=Mathf.Sqrt(jumpHeight*-2*gravity);verticalSpeed+=gravity*Time.deltaTime;Vector3 v=dir*speed;v.y=verticalSpeed;motor.Move(v*Time.deltaTime);}
 }
}