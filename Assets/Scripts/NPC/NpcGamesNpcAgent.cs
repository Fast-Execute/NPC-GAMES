using UnityEngine;
namespace NPCGames {
 public class NpcGamesNpcAgent : MonoBehaviour {
  public string characterName="Unknown",occupation="Citizen";public float wanderRadius=5f,moveSpeed=1.25f;Vector3 home,destination;float nextDecision;
  public void SetHome(Vector3 p){home=p;ChooseDestination();}
  void Update(){if(Time.time>=nextDecision||Vector3.Distance(transform.position,destination)<.35f)ChooseDestination();Vector3 d=destination-transform.position;d.y=0;if(d.sqrMagnitude>.08f){transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(d),Time.deltaTime*3);transform.position+=transform.forward*moveSpeed*Time.deltaTime;}}
  void ChooseDestination(){Vector2 o=Random.insideUnitCircle*wanderRadius;destination=home+new Vector3(o.x,0,o.y);nextDecision=Time.time+Random.Range(3,7);}
  public string Interact(){return characterName+" — "+occupation+": I have my own work, memories and place in this community. What would you like to know?";}
 }
}