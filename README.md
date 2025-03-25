# 🚀 Node-Based Pathfinding System for 2D Platformers

This project was developed during my **second group project at Futuregames**, where I designed a **node-based 2D platforming pathfinding system**. 

## 🏗️ Core Components

The system consists of three main classes:

1. **🟢 Node Class** - Stores information about its position and connected nodes.
2. **🟦 Platform Class** - Represents a platform where AI can move, has a unique ID, and assigns nodes at game start.
3. **🔗 NodeConnection Class** - Represents a connection to another node in the game.

Connections can be **one-directional** or **two-directional** (both nodes need to be connected in the latter case). At game start, nodes assign themselves to platforms via **line tracing** and snap to predefined positions. Additionally, the system allows snapping the leftmost and rightmost nodes to the platform edges.

One-directional paths can be used to create **passages where AI can jump down but must find another way to return**, adding more strategic complexity to level design. 🎯

**Example of Nodes Snapping to Platforms:**  
![Nodes Snapping](Assets/Gify/Snapping%20Nodes.gif)

## 🔍 Pathfinding Algorithm

The system uses a **custom 2D adaptation of the A* algorithm**, designed specifically for platforming movement, taking in three parameters:

- **📍 Starting Position**
- **🎯 Ending Position** (only required for the shortest path)
- **🆔 Ending Platform ID**

Since the AI does not pathfind to the player's exact position but to the platform instead, it switches from **pathfinding mode** to **walking mode** once it reaches the correct platform.

**Example of Pathfinding in Action:**  
![Pathfinding Demo](Assets/Gify/Pathfiding%20demo.gif)

## 🤖 AI Behavior

The AI operates in three distinct states:

1. **🛤️ Pathfinding State** – AI is on a different platform than the player and actively finds a path.
2. **🚶 Walking State** – AI is on the same platform as the player and moves toward them.
3. **⚔️ Attacking State** – AI stops moving for **one second** when close enough to the player.

The AI recalculates its path or changes state when it **collides with a platform** or when the **player lands on a new platform**.

**Example of AI Chasing Player:**  
![AI Chasing Player](Assets/Gify/Pathfinding%20Enemy%20demo.gif)

## 🔧 Flexibility and Scalability

- 🏗️ The system is **node-based** and can be **easily expanded** or modified.
- ⚡ AI **efficiently navigates** using the A* algorithm.
- 📌 Nodes **automatically snap** to platforms at game start.
- 🔄 AI dynamically updates paths based on **player movement**.
- 🎮 Developers can **easily add connections** for AI path planning.
- 🔀 One-directional paths allow for **strategic jump-down points**, forcing AI to find alternative return routes.

**Example of Adding Node Connections:**  
![Adding Node Connection](Assets/Gify/Adding%20new%20nodes.gif)
