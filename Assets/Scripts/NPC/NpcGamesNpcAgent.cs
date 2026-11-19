using UnityEngine;
namespace NPCGames {
 public class NpcGamesNpcAgent : MonoBehaviour {
  public string characterName="Unknown",occupation="Citizen",personality="thoughtful";
  public float moveSpeed=1.25f,trust=50f,knowledge=40f;
  private Vector3 home,work,publicSquare,destination;private float nextDecision,energy=80f,mood=60f;private NpcGamesWorldClock clock;private int conversations;private string lastMemory="I am part of this community.";
  public void SetRoutine(Vector3 homePosition,Vector3 workPosition,Vector3 meetingPoint,NpcGamesWorldClock worldClock){home=homePosition;work=workPosition;publicSquare=meetingPoint;clock=worldClock;ChooseDestination();}
  void Update(){
   if(clock!=null&&Time.time>=nextDecision)ChooseDestination();
   Vector3 d=destination-transform.position;d.y=0;
   if(d.sqrMagnitude>.08f){transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(d),Time.deltaTime*3f);transform.position+=transform.forward*moveSpeed*Time.deltaTime;energy=Mathf.Max(10,energy-Time.deltaTime*.5f);}
   else energy=Mathf.Min(100,energy+Time.deltaTime*.8f);
  }
  void ChooseDestination(){
   float hour=clock!=null?clock.Hour:12f;
   if(hour>=6f&&hour<8f)destination=home;
   else if(hour>=8f&&hour<16f)destination=work;
   else if(hour>=16f&&hour<19f)destination=publicSquare;
   else destination=home;
   Vector2 variation=Random.insideUnitCircle*1.6f;destination+=new Vector3(variation.x,0,variation.y);
   nextDecision=Time.time+Random.Range(4f,8f);
  }
  public string Interact(){
   conversations++;trust=Mathf.Min(100,trust+1.5f);mood=Mathf.Min(100,mood+2f);
   string greeting=hourGreeting();
   string thought=personality=="curious"?"I keep wondering what lies beyond what we know.":personality=="skeptical"?"Stories travel quickly. I prefer to check what I can.":personality=="empathetic"?"People here carry more than they show.":personality=="pragmatic"?"A problem is easier when we understand its parts.":personality=="ambitious"?"I want to help shape what this settlement becomes.":personality=="wondering"?"The sky changes, but it never stops telling stories.":personality=="cautious"?"I watch carefully before I decide whom to trust.":personality=="imaginative"?"Every ordinary place has a story waiting inside it.":personality=="analytical"?"Details matter. Small observations can change a conclusion.":"I look out for the people who depend on one another.";
   lastMemory="I spoke with the player "+conversations+" time(s).";
   return characterName+" — "+occupation+"\n"+greeting+" "+thought+" "+lastMemory;
  }
  string hourGreeting(){float h=clock!=null?clock.Hour:12f;return h<12?"Good morning.":h<18?"Good afternoon.":"Good evening.";}
 }
}