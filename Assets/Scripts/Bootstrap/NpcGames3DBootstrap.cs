using UnityEngine;
namespace NPCGames {
 public class NpcGames3DBootstrap : MonoBehaviour {
  private NpcGamesNpcAgent nearby; private string message="Welcome to the living world."; private Transform player;
  void Start(){ BuildWorld(); }
  void Update(){ nearby=FindNearby(); if(nearby!=null && Input.GetKeyDown(KeyCode.E)) message=nearby.Interact(); }
  void BuildWorld(){
   RenderSettings.ambientLight=new Color(.55f,.58f,.65f); RenderSettings.fog=true; RenderSettings.fogColor=new Color(.58f,.66f,.72f); RenderSettings.fogDensity=.008f;
   var sun=new GameObject("World Sun").AddComponent<Light>(); sun.type=LightType.Directional; sun.intensity=1.15f; sun.transform.rotation=Quaternion.Euler(48,-32,0);
   Block("Ground",new Vector3(0,-.55f,0),new Vector3(100,1,100),new Color(.25f,.38f,.25f));
   Block("Village Square",new Vector3(0,-.02f,0),new Vector3(13,.12f,13),new Color(.48f,.43f,.34f));
   for(int i=0;i<8;i++){float a=i*Mathf.PI*.25f; Building("Settlement House "+(i+1),new Vector3(Mathf.Cos(a)*13,0,Mathf.Sin(a)*13));}
   Building("Memory Archive",new Vector3(0,0,22),new Vector3(7,5,6),new Color(.28f,.34f,.48f));
   Building("Community School",new Vector3(-20,0,5),new Vector3(8,4,7),new Color(.55f,.43f,.25f));
   Building("Open Market",new Vector3(20,0,-5),new Vector3(8,3,7),new Color(.48f,.3f,.22f));
   var p=GameObject.CreatePrimitive(PrimitiveType.Capsule); p.name="Player"; p.transform.position=new Vector3(0,1.1f,-5); p.GetComponent<Renderer>().material=Mat(new Color(.18f,.65f,.9f)); Destroy(p.GetComponent<Collider>());
   var cc=p.AddComponent<CharacterController>(); cc.height=1.8f; cc.radius=.38f; p.AddComponent<NpcGamesPlayerController>(); player=p.transform;
   var cam=new GameObject("Third Person Camera"); cam.AddComponent<Camera>().fieldOfView=70; cam.tag="MainCamera"; cam.AddComponent<AudioListener>(); var rig=cam.AddComponent<NpcGamesCameraRig>(); rig.target=player; rig.SnapBehindTarget();
   string[] names={"Mara","Tebo","Nia","Kade","Luma","Sefu","Rian","Ayo","Zuri","Biko"};
   string[] roles={"Archivist","Fisher","Teacher","Mechanic","Merchant","Astronomer","Scout","Storyteller","Doctor","Guard"};
   for(int i=0;i<names.Length;i++){float a=i*Mathf.PI*2/names.Length; Vector3 home=new Vector3(Mathf.Cos(a)*7,0,Mathf.Sin(a)*7); var n=GameObject.CreatePrimitive(PrimitiveType.Capsule); n.name=names[i]; n.transform.position=home+Vector3.up; n.transform.localScale=new Vector3(.78f,1,.78f); n.GetComponent<Renderer>().material=Mat(Color.HSVToRGB(i/10f,.48f,.85f)); var agent=n.AddComponent<NpcGamesNpcAgent>(); agent.characterName=names[i]; agent.occupation=roles[i]; agent.SetHome(home);}
  }
  void Building(string name,Vector3 pos){Building(name,pos,new Vector3(5,3.5f,5),new Color(.52f,.39f,.27f));}
  void Building(string name,Vector3 pos,Vector3 size,Color color){var b=GameObject.CreatePrimitive(PrimitiveType.Cube); b.name=name; b.transform.position=pos+Vector3.up*(size.y*.5f); b.transform.localScale=size; b.GetComponent<Renderer>().material=Mat(color); var roof=GameObject.CreatePrimitive(PrimitiveType.Cube); roof.name=name+" Roof"; roof.transform.position=pos+Vector3.up*(size.y+.25f); roof.transform.localScale=new Vector3(size.x*1.12f,.5f,size.z*1.12f); roof.GetComponent<Renderer>().material=Mat(new Color(.22f,.18f,.16f));}
  void Block(string name,Vector3 pos,Vector3 size,Color color){var b=GameObject.CreatePrimitive(PrimitiveType.Cube);b.name=name;b.transform.position=pos;b.transform.localScale=size;b.GetComponent<Renderer>().material=Mat(color);}
  Material Mat(Color c){Shader s=Shader.Find("Universal Render Pipeline/Lit");if(s==null)s=Shader.Find("Standard");var m=new Material(s);m.color=c;return m;}
  NpcGamesNpcAgent FindNearby(){if(player==null)return null;NpcGamesNpcAgent best=null;float d=3.2f;foreach(var n in FindObjectsByType<NpcGamesNpcAgent>(FindObjectsSortMode.None)){float x=Vector3.Distance(player.position,n.transform.position);if(x<d){d=x;best=n;}}return best;}
  void OnGUI(){GUI.color=Color.white;GUI.Box(new Rect(14,14,340,100),"NPC-GAMES | THE LIVING WORLD\nWASD move  SHIFT run  SPACE jump\nMouse look  E interact  ESC release cursor");GUI.Box(new Rect(14,Screen.height-76,520,60),message);if(nearby!=null)GUI.Box(new Rect(Screen.width*.5f-105,Screen.height-125,210,35),"Press E to speak with "+nearby.characterName);}
 }
}