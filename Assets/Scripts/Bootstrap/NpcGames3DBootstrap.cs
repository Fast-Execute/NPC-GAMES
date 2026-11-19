using UnityEngine;
namespace NPCGames {
 public class NpcGames3DBootstrap : MonoBehaviour {
  private NpcGamesNpcAgent nearby; private string message="Welcome to the living world."; private Transform player; private NpcGamesWorldClock clock;
  void Start(){ BuildWorld(); }
  void Update(){ nearby=FindNearby(); if(nearby!=null && Input.GetKeyDown(KeyCode.E)) message=nearby.Interact(); }
  void BuildWorld(){
   RenderSettings.ambientLight=new Color(.55f,.58f,.65f); RenderSettings.fog=true; RenderSettings.fogColor=new Color(.58f,.66f,.72f); RenderSettings.fogDensity=.008f;
   clock=gameObject.AddComponent<NpcGamesWorldClock>();
   var sun=new GameObject("World Sun").AddComponent<Light>(); sun.type=LightType.Directional; sun.intensity=1.15f; sun.transform.rotation=Quaternion.Euler(48,-32,0); clock.sun=sun;
   Block("Ground",new Vector3(0,-.55f,0),new Vector3(100,1,100),new Color(.25f,.38f,.25f));
   Block("Village Square",new Vector3(0,-.02f,0),new Vector3(13,.12f,13),new Color(.48f,.43f,.34f));
   Block("North Road",new Vector3(0,.005f,14),new Vector3(3,.08f,25),new Color(.38f,.34f,.27f));
   Block("East Road",new Vector3(15,.005f,0),new Vector3(25,.08f,3),new Color(.38f,.34f,.27f));
   Block("West Road",new Vector3(-15,.005f,0),new Vector3(25,.08f,3),new Color(.38f,.34f,.27f));
   for(int i=0;i<8;i++){float a=i*Mathf.PI*.25f; Building("Settlement House "+(i+1),new Vector3(Mathf.Cos(a)*13,0,Mathf.Sin(a)*13));}
   Building("Memory Archive",new Vector3(0,0,22),new Vector3(7,5,6),new Color(.28f,.34f,.48f));
   Building("Community School",new Vector3(-20,0,5),new Vector3(8,4,7),new Color(.55f,.43f,.25f));
   Building("Open Market",new Vector3(20,0,-5),new Vector3(8,3,7),new Color(.48f,.3f,.22f));
   BuildTrees();
   BuildLamps();
   var p=GameObject.CreatePrimitive(PrimitiveType.Capsule); p.name="Player"; p.transform.position=new Vector3(0,1.1f,-5); p.GetComponent<Renderer>().material=Mat(new Color(.18f,.65f,.9f)); Destroy(p.GetComponent<Collider>());
   var cc=p.AddComponent<CharacterController>(); cc.height=1.8f; cc.radius=.38f; var movement=p.AddComponent<NpcGamesPlayerController>(); player=p.transform;
   var cam=new GameObject("Third Person Camera"); cam.AddComponent<Camera>().fieldOfView=70; cam.tag="MainCamera"; cam.AddComponent<AudioListener>(); var rig=cam.AddComponent<NpcGamesCameraRig>(); rig.target=player; movement.viewCamera=cam.transform; rig.SnapBehindTarget();
   string[] names={"Mara","Tebo","Nia","Kade","Luma","Sefu","Rian","Ayo","Zuri","Biko"};
   string[] roles={"Archivist","Fisher","Teacher","Mechanic","Merchant","Astronomer","Scout","Storyteller","Doctor","Guard"};
   string[] traits={"curious","skeptical","empathetic","pragmatic","ambitious","wondering","cautious","imaginative","analytical","protective"};
   Vector3[] work={new Vector3(0,0,22),new Vector3(20,0,-5),new Vector3(-20,0,5),new Vector3(4,0,-4),new Vector3(20,0,-5),new Vector3(0,0,22),new Vector3(-8,0,12),new Vector3(2,0,3),new Vector3(-4,0,2),new Vector3(0,0,-8)};
   for(int i=0;i<names.Length;i++){float a=i*Mathf.PI*2/names.Length; Vector3 home=new Vector3(Mathf.Cos(a)*7,0,Mathf.Sin(a)*7); var n=GameObject.CreatePrimitive(PrimitiveType.Capsule); n.name=names[i]; n.transform.position=home+Vector3.up; n.transform.localScale=new Vector3(.78f,1,.78f); n.GetComponent<Renderer>().material=Mat(Color.HSVToRGB(i/10f,.48f,.85f)); var agent=n.AddComponent<NpcGamesNpcAgent>(); agent.characterName=names[i]; agent.occupation=roles[i]; agent.personality=traits[i]; agent.SetRoutine(home,work[i],new Vector3(0,0,0),clock);}
  }
  void BuildTrees(){Vector3[] spots={new Vector3(-30,0,-20),new Vector3(-25,0,25),new Vector3(30,0,20),new Vector3(29,0,-25),new Vector3(-35,0,0),new Vector3(0,0,35),new Vector3(35,0,0)};foreach(var p in spots){var trunk=GameObject.CreatePrimitive(PrimitiveType.Cylinder);trunk.name="Tree Trunk";trunk.transform.position=p+Vector3.up*1.5f;trunk.transform.localScale=new Vector3(.45f,1.5f,.45f);trunk.GetComponent<Renderer>().material=Mat(new Color(.28f,.18f,.1f));var crown=GameObject.CreatePrimitive(PrimitiveType.Sphere);crown.name="Tree Canopy";crown.transform.position=p+Vector3.up*3.3f;crown.transform.localScale=Vector3.one*3.2f;crown.GetComponent<Renderer>().material=Mat(new Color(.16f,.34f,.18f));}}
  void BuildLamps(){Vector3[] spots={new Vector3(-7,0,-7),new Vector3(7,0,-7),new Vector3(-7,0,7),new Vector3(7,0,7),new Vector3(0,0,15)};foreach(var p in spots){var pole=GameObject.CreatePrimitive(PrimitiveType.Cylinder);pole.name="Street Lamp";pole.transform.position=p+Vector3.up*2;pole.transform.localScale=new Vector3(.12f,2,.12f);pole.GetComponent<Renderer>().material=Mat(new Color(.12f,.13f,.15f));var light=new GameObject("Warm Street Light").AddComponent<Light>();light.type=LightType.Point;light.color=new Color(1f,.72f,.42f);light.range=10;light.intensity=0;light.transform.position=p+Vector3.up*4;}}
  void Building(string name,Vector3 pos){Building(name,pos,new Vector3(5,3.5f,5),new Color(.52f,.39f,.27f));}
  void Building(string name,Vector3 pos,Vector3 size,Color color){var b=GameObject.CreatePrimitive(PrimitiveType.Cube);b.name=name;b.transform.position=pos+Vector3.up*(size.y*.5f);b.transform.localScale=size;b.GetComponent<Renderer>().material=Mat(color);var roof=GameObject.CreatePrimitive(PrimitiveType.Cube);roof.name=name+" Roof";roof.transform.position=pos+Vector3.up*(size.y+.25f);roof.transform.localScale=new Vector3(size.x*1.12f,.5f,size.z*1.12f);roof.GetComponent<Renderer>().material=Mat(new Color(.22f,.18f,.16f));}
  void Block(string name,Vector3 pos,Vector3 size,Color color){var b=GameObject.CreatePrimitive(PrimitiveType.Cube);b.name=name;b.transform.position=pos;b.transform.localScale=size;b.GetComponent<Renderer>().material=Mat(color);}
  Material Mat(Color c){Shader s=Shader.Find("Universal Render Pipeline/Lit");if(s==null)s=Shader.Find("Standard");var m=new Material(s);m.color=c;return m;}
  NpcGamesNpcAgent FindNearby(){if(player==null)return null;NpcGamesNpcAgent best=null;float d=3.2f;foreach(var n in FindObjectsByType<NpcGamesNpcAgent>(FindObjectsSortMode.None)){float x=Vector3.Distance(player.position,n.transform.position);if(x<d){d=x;best=n;}}return best;}
  void OnGUI(){GUI.color=Color.white;GUI.Box(new Rect(14,14,360,120),"NPC-GAMES | THE LIVING WORLD\nWASD move  SHIFT run  SPACE jump\nMouse look  E interact  ESC release cursor\n"+(clock!=null?clock.DisplayTime:""));GUI.Box(new Rect(14,Screen.height-76,520,60),message);if(nearby!=null)GUI.Box(new Rect(Screen.width*.5f-120,Screen.height-125,240,35),"Press E to speak with "+nearby.characterName);}
 }
}