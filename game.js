const npcs = [
  {id:1,name:"Mara",role:"Archivist",trait:"curious",trust:72,knowledge:82,memory:[],links:[2,4]},
  {id:2,name:"Tebo",role:"Fisher",trait:"skeptical",trust:54,knowledge:41,memory:[],links:[1,3,5]},
  {id:3,name:"Nia",role:"Teacher",trait:"empathetic",trust:88,knowledge:79,memory:[],links:[2,4,6]},
  {id:4,name:"Kade",role:"Mechanic",trait:"pragmatic",trust:61,knowledge:66,memory:[],links:[1,3,5,7]},
  {id:5,name:"Luma",role:"Merchant",trait:"opportunistic",trust:48,knowledge:58,memory:[],links:[2,4,8]},
  {id:6,name:"Sefu",role:"Astronomer",trait:"wondering",trust:76,knowledge:91,memory:[],links:[3,7,9]},
  {id:7,name:"Rian",role:"Scout",trait:"suspicious",trust:39,knowledge:63,memory:[],links:[4,6,8,10]},
  {id:8,name:"Ayo",role:"Storyteller",trait:"imaginative",trust:69,knowledge:55,memory:[],links:[5,7,9,11]},
  {id:9,name:"Zuri",role:"Doctor",trait:"analytical",trust:84,knowledge:87,memory:[],links:[6,8,10]},
  {id:10,name:"Biko",role:"Guard",trait:"protective",trust:57,knowledge:44,memory:[],links:[7,9,11,12]},
  {id:11,name:"Ira",role:"Builder",trait:"practical",trust:63,knowledge:51,memory:[],links:[8,10,12]},
  {id:12,name:"Vale",role:"Drifter",trait:"unpredictable",trust:31,knowledge:73,memory:[],links:[10,11]}
];

const world = {
  day:1, hour:8, knowledge:new Set(), rumours:[], connections:0,
  feed: ["<b>NETWORK:</b> Collective consciousness initialized.","<b>WORLD:</b> Twelve independent minds have connected.","<b>SYSTEM:</b> Memories begin empty."]
};

function addFeed(html) {
  world.feed.unshift(html);
  world.feed = world.feed.slice(0,40);
  document.querySelector("#feed").innerHTML = world.feed.map(x=>`<div class="feed-item">${x}</div>`).join("");
}

function seedKnowledge() {
  ["water","weather","trade","history","stars","machines","medicine","roads"].forEach(x=>world.knowledge.add(x));
  npcs.forEach(n => {
    n.memory.push({text:`The world contains knowledge about ${n.role.toLowerCase()} work.`, source:"collective"});
  });
}
seedKnowledge();

function render() {
  document.querySelector("#nodeCount").textContent = npcs.length + " NPCs";
  document.querySelector("#knowledgeCount").textContent = world.knowledge.size;
  document.querySelector("#memoryCount").textContent = npcs.reduce((a,n)=>a+n.memory.length,0);
  document.querySelector("#rumourCount").textContent = world.rumours.length;
  document.querySelector("#connectionCount").textContent = npcs.reduce((a,n)=>a+n.links.length,0)/2;
  document.querySelector("#worldTime").textContent = `DAY ${world.day} · ${String(world.hour).padStart(2,"0")}:00`;
  document.querySelector("#npcList").innerHTML = npcs.map(n => `
    <article class="npc" data-id="${n.id}">
      <div class="npc-top"><span class="npc-name">${n.name}</span><span class="npc-state">${n.trait}</span></div>
      <div class="npc-role">${n.role} · ${n.memory.length} memories</div>
      <div class="meter"><i style="width:${Math.min(100,n.knowledge)}%"></i></div>
    </article>`).join("");
  document.querySelectorAll(".npc").forEach(el => el.onclick=()=>inspect(Number(el.dataset.id)));
}

function inspect(id) {
  const n=npcs.find(x=>x.id===id);
  addFeed(`<b>${n.name}:</b> ${n.role}, ${n.trait}. Trust ${n.trust}%. Recent memory: ${n.memory.at(-1)?.text || "none"}`);
}

function mutate(text, npc) {
  if (npc.trait==="skeptical" && Math.random()<.35) return "I am not sure, but " + text;
  if (npc.trait==="imaginative" && Math.random()<.45) return text.replace(/\.$/,"... maybe there is more to it.");
  if (npc.trait==="opportunistic" && Math.random()<.3) return text + " This could be valuable.";
  return text;
}

function propagate(origin, text) {
  let current=origin;
  const hops=1+Math.floor(Math.random()*3);
  for(let i=0;i<hops;i++){
    const targets=current.links.map(id=>npcs.find(n=>n.id===id)).filter(Boolean);
    if(!targets.length) break;
    const target=targets[Math.floor(Math.random()*targets.length)];
    const belief=mutate(text,target);
    target.memory.push({text:belief,source:current.name});
    target.knowledge=Math.min(100,target.knowledge+2);
    current=target;
    if(Math.random()<.28) world.rumours.push(belief);
  }
}

function playerSpeak() {
  const input=document.querySelector("#playerInput");
  const text=input.value.trim();
  if(!text) return;
  const origin=npcs[Math.floor(Math.random()*npcs.length)];
  origin.memory.push({text,source:"PLAYER"});
  world.knowledge.add(...text.toLowerCase().split(/\s+/).filter(w=>w.length>4).slice(0,5));
  addFeed(`<b>PLAYER → ${origin.name}:</b> “${escapeHtml(text)}”`);
  propagate(origin,text);
  addFeed(`<b>NETWORK:</b> Information is propagating through ${origin.name}'s social links.`);
  input.value="";
  render();
}

function worldEvent() {
  const events=[
    "A strange signal has appeared beyond the northern ridge.",
    "A traveller arrived carrying an unfamiliar machine.",
    "The river changed course during the night.",
    "Someone discovered an encrypted message beneath the old station."
  ];
  const text=events[Math.floor(Math.random()*events.length)];
  const origin=npcs[Math.floor(Math.random()*npcs.length)];
  origin.memory.push({text,source:"WORLD"});
  addFeed(`<b>WORLD EVENT → ${origin.name}:</b> ${text}`);
  propagate(origin,text);
  render();
}

function tick() {
  world.hour++;
  if(world.hour>=24){world.hour=0;world.day++;}
  npcs.forEach(n=>{
    if(Math.random()<.25 && n.memory.length>1){
      const m=n.memory[Math.floor(Math.random()*n.memory.length)];
      propagate(n,m.text);
    }
    if(Math.random()<.15) n.trust=Math.max(0,Math.min(100,n.trust+(Math.random()>.5?1:-1)));
  });
  addFeed(`<b>TIME:</b> The network advanced to Day ${world.day}, ${String(world.hour).padStart(2,"0")}:00. NPCs exchanged information.`);
  render();
}

function escapeHtml(s){return s.replace(/[&<>"']/g,c=>({"&":"&amp;","<":"&lt;",">":"&gt;",'"':"&quot;","'":"&#039;"}[c]));}
document.querySelector("#speakBtn").onclick=playerSpeak;
document.querySelector("#eventBtn").onclick=worldEvent;
document.querySelector("#tickBtn").onclick=tick;
render();
