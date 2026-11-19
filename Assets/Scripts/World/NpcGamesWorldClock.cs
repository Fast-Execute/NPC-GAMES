using UnityEngine;
namespace NPCGames {
 public class NpcGamesWorldClock : MonoBehaviour {
  [Range(0,24)]public float Hour=8f;public float gameMinutesPerRealSecond=2f;public Light sun;public string DisplayTime{get{return Mathf.FloorToInt(Hour).ToString("00")+":"+Mathf.FloorToInt((Hour%1)*60).ToString("00");}}
  void Update(){Hour=(Hour+gameMinutesPerRealSecond/60f*Time.deltaTime)%24f;if(sun!=null){float angle=(Hour/24f)*360f-90f;sun.transform.rotation=Quaternion.Euler(angle, -32f,0);float daylight=Mathf.Clamp01(Mathf.Sin((Hour-6f)/12f*Mathf.PI));sun.intensity=Mathf.Lerp(.08f,1.15f,daylight);RenderSettings.ambientLight=Color.Lerp(new Color(.055f,.07f,.13f),new Color(.55f,.58f,.65f),daylight);RenderSettings.fogColor=Color.Lerp(new Color(.07f,.09f,.16f),new Color(.58f,.66f,.72f),daylight);}}
 }
}