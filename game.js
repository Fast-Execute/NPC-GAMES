const npcs = [
  {id:1,name:"Mara",role:"Archivist",trait:"curious",age:34,life:"adult",family:"Mara household",children:[],alive:true,trust:72,knowledge:82,memory:[],links:[2,4]},
  {id:2,name:"Tebo",role:"Fisher",trait:"skeptical",age:46,life:"adult",family:"Tebo household",children:[],alive:true,trust:54,knowledge:41,memory:[],links:[1,3,5]},
  {id:3,name:"Nia",role:"Teacher",trait:"empathetic",age:28,life:"adult",family:"Nia household",children:[],alive:true,trust:88,knowledge:79,memory:[],links:[2,4,6]},
  {id:4,name:"Kade",role:"Mechanic",trait:"pragmatic",age:52,life:"adult",family:"Kade household",children:[],alive:true,trust:61,knowledge:66,memory:[],links:[1,3,5,7]},
  {id:5,name:"Luma",role:"Merchant",trait:"opportunistic",age:23,life:"adult",family:"Luma household",children:[],alive:true,trust:48,knowledge:58,memory:[],links:[2,4,8]},
  {id:6,name:"Sefu",role:"Astronomer",trait:"wondering",age:67,life:"elder",family:"Sefu household",children:[],alive:true,trust:76,knowledge:91,memory:[],links:[3,7,9]},
  {id:7,name:"Rian",role:"Scout",trait:"suspicious",age:17,life:"youth",family:"Rian household",children:[],alive:true,trust:39,knowledge:63,memory:[],links:[4,6,8,10]},
  {id:8,name:"Ayo",role:"Storyteller",trait:"imaginative",age:39,life:"adult",family:"Ayo household",children:[],alive:true,trust:69,knowledge:55,memory:[],links:[5,7,9,11]},
  {id:9,name:"Zuri",role:"Doctor",trait:"analytical",age:31,life:"adult",family:"Zuri household",children:[],alive:true,trust:84,knowledge:87,memory:[],links:[6,8,10]},
  {id:10,name:"Biko",role:"Guard",trait:"protective",age:58,life:"adult",family:"Biko household",children:[],alive:true,trust:57,knowledge:44,memory:[],links:[7,9,11,12]},
  {id:11,name:"Ira",role:"Builder",trait:"practical",age:12,life:"child",family:"Ira household",children:[],alive:true,trust:63,knowledge:51,memory:[],links:[8,10,12]},
  {id:12,name:"Vale",role:"Drifter",trait:"unpredictable",age:74,life:"elder",family:"Vale household",children:[],alive:true,trust:31,knowledge:73,memory:[],links:[10,11]}
];

const world = {
  day:1, hour:8, year:1, nextNpcId:13, knowledge:new Set(), rumours:[], connections:0,
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
      <div class="npc-role">${n.role} · Age ${n.age} · ${n.life} · ${n.memory.length} memories</div>
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


// LIFE ENGINE + MEETING ICONS
const icons = [
  {id:"crowley",name:"Aleister Crowley",era:"1875–1947",style:"provocative, mystical, theatrical",idea:"individual will, occult philosophy, disciplined practice"},
  {id:"curie",name:"Marie Curie",era:"1867–1934",style:"precise, calm, scientific",idea:"evidence, persistence, discovery"},
  {id:"einstein",name:"Albert Einstein",era:"1879–1955",style:"playful, reflective, conceptual",idea:"thought experiments, relativity, curiosity"},
  {id:"socrates",name:"Socrates",era:"c. 470–399 BC",style:"questioning, ironic, patient",idea:"examining assumptions through dialogue"},
  {id:"tesla",name:"Nikola Tesla",era:"1856–1943",style:"visionary, intense, inventive",idea:"invention, electricity, imagination"}
];

const player = {
  name:"Sello", age:18, generation:1, alive:true, legacy:[], pastMemories:[],
  skills:["curiosity"], beliefs:[], meetingCount:0
};

function renderLife() {
  const state=document.querySelector("#lifeState");
  if(!state) return;
  state.textContent=player.alive?"ALIVE":"LIFE COMPLETE";
  document.querySelector("#lifeMeta").textContent=`Age ${player.age} · Generation ${player.generation}`;
  document.querySelector("#lifeMeter").style.width=`${Math.min(100,(player.age/100)*100)}%`;
  document.querySelector("#legacyCount").textContent=player.legacy.length;
  document.querySelector("#pastMemoryCount").textContent=player.pastMemories.length;
  document.querySelector("#playerName").textContent=player.name;
}

function renderIcons() {
  const el=document.querySelector("#iconList");
  if(!el) return;
  el.innerHTML=icons.map(icon=>`<button class="icon-card" data-icon="${icon.id}">
    <strong>${icon.name}</strong><small>${icon.era}</small><span>${icon.style}</span>
  </button>`).join("");
  document.querySelectorAll(".icon-card").forEach(b=>b.onclick=()=>meetIcon(b.dataset.icon));
}

function meetIcon(id) {
  const icon=icons.find(x=>x.id===id);
  if(!icon || !player.alive) return;
  player.meetingCount++;
  const openings=[
    `“You have come with a question. Good. Questions are more useful than comfortable answers.”`,
    `“Observe carefully. The world changes when the observer changes.”`,
    `“Do not borrow a conclusion merely because it sounds beautiful.”`,
    `“Your life is brief. Your experiments need not be.”`
  ];
  const voice=openings[(player.age+player.meetingCount+icon.name.length)%openings.length];
  player.pastMemories.push(`Met ${icon.name}: ${icon.idea}.`);
  player.legacy.push(`A lesson associated with ${icon.name}.`);
  player.beliefs.push(icon.idea);
  addFeed(`<b>MEETING ICON — ${icon.name}:</b> ${voice}`);
  addFeed(`<b>ICON MEMORY:</b> You encountered an historical simulation of ${icon.name}. Your interpretation becomes part of your life history.`);
  renderLife();
}

function agePlayer() {
  if(!player.alive) return;
  player.age++;
  if(player.age%5===0) player.skills.push(["patience","craft","memory","leadership"][Math.floor(Math.random()*4)]);
  if(player.age>=100) diePlayer("Your body reached the end of its natural simulated lifespan.");
  else addFeed(`<b>LIFE:</b> ${player.name} is now ${player.age}. The world remembers another year.`);
  renderLife();
}

function diePlayer(reason) {
  if(!player.alive) return;
  player.alive=false;
  player.legacy.push(`Generation ${player.generation} ended at age ${player.age}.`);
  npcs.forEach(n=>n.memory.push({text:`${player.name} died at age ${player.age}. Their legacy survives.`,source:"LIFE"}));
  addFeed(`<b>DEATH:</b> ${reason}`);
  addFeed(`<b>LEGACY:</b> The NPC network now carries memories of ${player.name}'s life.`);
  renderLife();
  setTimeout(reincarnate,500);
}

function reincarnate() {
  player.generation++;
  player.age=0;
  player.alive=true;
  const names=["Sello","Mara","Tebo","Nia","Kade","Luma"];
  player.name=names[Math.floor(Math.random()*names.length)];
  player.pastMemories=player.pastMemories.slice(-3);
  player.skills=player.skills.slice(-3);
  addFeed(`<b>REINCARNATION:</b> A new life has begun — Generation ${player.generation}. Some memories survived the transition.`);
  addFeed(`<b>NEW NODE:</b> ${player.name} begins life with fragments of a previous existence, not a complete memory.`);
  renderLife();
}

document.querySelector("#ageBtn").onclick=agePlayer;
document.querySelector("#deathBtn").onclick=()=>diePlayer("The player chose to end this life.");
renderIcons();
renderLife();


// GENERATIONAL WORLD ENGINE — prototype simulation
function lifeStage(age) {
  if(age<3) return "infant";
  if(age<13) return "child";
  if(age<18) return "youth";
  if(age<60) return "adult";
  if(age<80) return "elder";
  return "very old";
}

function createDescendant(parent) {
  const names=["Amari","Tariro","Imani","Kito","Lerato","Neo","Palesa","Thando","Zola","Mosi"];
  const child={
    id:world.nextNpcId++,
    name:names[Math.floor(Math.random()*names.length)],
    role:"Apprentice",
    trait:parent.trait,
    age:0,
    life:"infant",
    family:parent.family,
    children:[],
    alive:true,
    trust:Math.max(20,Math.min(95,parent.trust+Math.floor(Math.random()*15-7))),
    knowledge:Math.max(5,Math.floor(parent.knowledge*.25)),
    memory:[{text:"You were born into the " + parent.family + ".",source:"FAMILY"}],
    links:[parent.id]
  };
  parent.children.push(child.id);
  npcs.push(child);
  parent.links.push(child.id);
  addFeed(`<b>BIRTH:</b> ${child.name} was born into the ${parent.family}. ${parent.name} becomes a parent.`);
}

function ageNpc(n) {
  if(!n.alive) return;
  n.age++;
  n.life=lifeStage(n.age);
  if(n.age===18 && n.role==="Apprentice") {
    n.role=["Craftsperson","Teacher","Trader","Explorer"][Math.floor(Math.random()*4)];
    n.knowledge=Math.min(100,n.knowledge+12);
    n.memory.push({text:"I entered adult life and began my own work.",source:"LIFE"});
    addFeed(`<b>COMING OF AGE:</b> ${n.name} is now an adult and works as a ${n.role}.`);
  }
  if(n.age>=82 && Math.random()<.18) {
    n.alive=false;
    n.life="deceased";
    const remembrance=`${n.name}, who lived to age ${n.age}, has died. Their stories remain.`;
    npcs.filter(x=>x.alive).forEach(other=>other.memory.push({text:remembrance,source:"COMMUNITY"}));
    addFeed(`<b>DEATH:</b> ${n.name} has died at age ${n.age}. Their family and community remember them.`);
    return;
  }
  if(n.age>=20 && n.age<=45 && n.children.length===0 && Math.random()<.035) createDescendant(n);
}

function advanceGenerationYear() {
  world.year++;
  const living=[...npcs].filter(n=>n.alive);
  living.forEach(ageNpc);
  addFeed(`<b>YEAR ${world.year}:</b> A year passed. ${npcs.filter(n=>n.alive).length} people remain alive; ${npcs.filter(n=>!n.alive).length} lives have ended. Families and memories continue.`);
  render();
}

const yearButton=document.querySelector("#yearBtn");
if(yearButton) yearButton.onclick=advanceGenerationYear;
