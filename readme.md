\# Project Overview



\## 1. Project Structure



The project is divided into numbered folders which are then divided into folders based on relevant topics (for example all player scripts are under the same folder).



\---



\## 2. How Systems Work



There are 3 main systems in play:



\### Player



This system regards the player as it appears in the scene.



\- There's an Input manager, which communicates with the movement manager that moves the model and the animator that applies animations.

\- The player stats handler holds the information about the player stats.



\---



\### Equipment



The inventory/Equipment systems are divided into current equipment handling, and the Inventory menu.



\- The communication happens mostly through the InventoryManager and the PlayerEquipmentManager, and through global events.

\- The inventory is made of a list of InventoryItem prefab that tracks whether it is equipped and where.

\- All Equipment information (the item's name, stats, icon) is held in ScriptableObjects called Equipment\_Def.

\- The EquipmentManager stores the equipment slot of each item.



\---



\### Store



The store creates item cards in the inventory based on the list of existing Equipment\_Defs.



\- It uses the PlayerEquipmentManager to learn if items are already unlocked.

\- Each item checks if there are enough funds or if the item was already bought whenever a transaction is made.



\---



\### Additional Systems



\- There is a Store Dialogue system that uses ShopkeepDialogue\_Def ScriptableObjects to navigate shown text and viable player lines.

\- The store interaction system is very self-contained (handled through one script).

&#x20; - \*(Note: This should ideally be divided further, but this was implemented late in development.)\*

\- The stat view updates from global events when stats change and displays the relevant information.



\---



\### Scripts to Look At



\- GameEventManager  

\- PlayerEquipmentManager  

\- StoreManager  

\- InventoryManager  

\- Equipment\_Def  

\- ShopkeepDialogueManager  



These scripts handle most of the core system interactions.



\---



\## 3. Known Limitations



\- The scene itself is very small

\- Most stats don't do anything

\- The UX isn't very intuitive

\- Limited testing has been done, so additional issues may exist

