# TextGameProyect
 An engine to make text-based adventures with only a change in the xml of the data
 
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
        <li><a href="#images-in-game">Images in Game</a></li>
      </ul>
     </li>
     <li>
      <a href="#documentation-for-designers">Documentation for designers</a>
       <ul>
       <li><a href="#world">World</a></li>
        <li><a href="#rooms">Rooms</a></li>
        <li><a href="#doors">Doors</a></li>
        <li><a href="#no-interactable-items">No Interactable Items</a></li>
        <li><a href="#keys">Keys</a></li>
        <li><a href="#chests">Chests</a></li>
        <li><a href="#usable-furniture">Usable Furniture</a></li>
        <li><a href="#endgame-tale">EndGame Tale</a></li>
      </ul>
      <li>
      <a href="#about-the-project">Future Updates</a>
     </li>
     </li>
  </ol>  
    
</details>


<!-- ABOUT THE PROJECT -->
## About the Project

This project is born with love to old games like [Zork](https://es.wikipedia.org/wiki/Zork) and an idea in mind: being able to desing a text-based adventure changing only a few elements in the project. That way, game designers can implement an adventure without modify the code. 
This project follows, or try to, the next points:
* Adventure without worry about the size. The designer must be able to enter all the rooms they want without worry.
* Clean code. The code must be readable.
* Fun! What's the point of designing a videogame if you aren't having fun?

<!-- BUILT WITH -->
### Built With
* [Visual Studio](https://visualstudio.microsoft.com/es/)
* [C#](https://es.wikipedia.org/wiki/C_Sharp)

### Images in Game

![ScreenCapture](GitAssets/screenCapture.png "Screencapture")

<!-- DESIGNINFO -->

## Documentation for designers

*This section might change in the future, take the instructions with a pint of salt*

Let's get down to business, we've come here to design a videogame.

### World

First things first, here we have a World which include all the elements present in the game. Inside the World there are the next elements:

*  **Rooms**: Each Room in the game, inside each room there are items.
* **Doors**: Doors who may or not block the path to exit the Room.
* **Non Interactable Items**: Items which have no use except a message if the player use them.
* **Keys**: The Item which open Doors or Chests if the player use them with the Door or Chest.
* **Chests**: Items which contains more items.
* **Interactable Items**: Items which open Doors or Chests if the player use them alone.
* **Endgame Tale**: Text which will be displayed if the player completes the game.

This elements are inside a XML document, each one inside their own list. You can put the elements inside their respective list. For example:

```xml
<EndGameStoryList>
  <string>Tras mucho esfuerzo, llegas al final.</string>
  <string>Tus esfuerzos han sido recompensados.</string>
</EndGameStoryList>
```

**Important**: Each element inside of the world must have their own unique id. There are two groups which their members can repeat ids: Rooms and Items. This means, one item can have the same id that a room but one door can't have the same id that a chest.

We are going to visit every one of them in the next points.

### Rooms

Each Room in the game must be represented in an object. A Room has the following attributes:

* **id**: Id who identifies the room. Must be unique and can't be repeated. 
* **name**: The name of the room.
* **description**: Description of the room, this would appear each time the player enters.
* **story**: The story of the game. What happens when the player enters the room for the first time. It's separated in pages.
* **directions**: The directions which the player can follow to exit the room. There are 10 possible directions: north, norteast, east, southeast, south, southwest, west, northwest, up and down. By default is -1. You must put the id of the room in that direction so the player can go there.
* **doors**: Marks the path who have a door. By default is -1. You must put the id of the door in that direction.
* **items**: Items present in that room. A room is empty by default, if you want to put an item in there you must put the id in the list.
* **doors**: Doors in the room.  A room doens't have any door by default, if you want to put an door there you must put the id in the list.
* **endgame**: This marks if that room is the final room so when the player gets there the game ends. False by default.
* **visited**: This marks if the room has been visited. False by default.

This is an example of a XML of a Room:

```xml
<room>
    <id>0</id>
    <name>habitacion principal</name>
    <description>Es una habitacion normal</description>
    <story>
      <page>Despiertas en una habitación, al fondo de la sala, ves una puerta.</page>
      <page>En el suelo, ves un cofre</page>
    </story>
    <directions>
      <direction>0</direction>
      <direction>-1</direction>
      <direction>-1</direction>
      <direction>-1</direction>
      <direction>-1</direction>
      <direction>-1</direction>
      <direction>-1</direction>
      <direction>-1</direction>
      <direction>-1</direction>
      <direction>-1</direction>
    </directions>
    <items>
      <itemId>0</itemId>
      <itemId>1</itemId>
      <itemId>2</itemId>
      <itemId>4</itemId>
    </items>
    <doors>
      <door>0</door>
      <door>-1</door>
      <door>-1</door>
      <door>-1</door>
      <door>-1</door>
      <door>-1</door>
      <door>-1</door>
      <door>-1</door>
      <door>-1</door>
      <door>-1</door>
    </doors>
    <endgameTrigger>false</endgameTrigger>
    <visited>false</visited>
  </room>
```

### Doors

Each Door in the game must be represented in an object. A Door has the following attributes. A Door Id can't be the same as an item id:

* **id**: Id who identifies the door. Must be unique and can't be repeated. 
* **name**: The name of the door.
* **description**: Description of the door.
* **message**: Message which will show up if the player uses the door without a Key.
* **open**: This marks if the door is open. False by default.
* **blocked**: This marks if the door is blocked and you need a Key to open it. False by default.
* **keyId**: The Id of the Key which opens the door.
* **type**: Type of the container. DOOR by default.
* **endgame**: This marks if opening the door triggers the endgame.
* **visited**: This marks if the room has been visited. False by default.

This is an example of a XML of a Door:

```xml
<Door>
    <id>0</id>
    <name>puerta de la habitación</name>
    <description>Puerta cerrada, parece que necesitas una llave para abrirla</description>
    <useMessage>Necesitas una llave para abrirla</useMessage>
    <open>false</open>
    <blocked>true</blocked>
    <keyId>0</keyId>
    <type>DOOR</type>
    <endgameTrigger>false</endgameTrigger>
  </Door>
```
### No Interactable Items

Items who have no use in game beside a message. An Item has the following attributes:

* **id**: Id who identifies the item. Must be unique and can't be repeated. 
* **name**: The name of the item.
* **description**: Description of the item.
* **message**: Message which will show up if the player uses the item.
* **type**: Type of the item. It can be any of the following: DEFAULT, NOTE, FURNITURE. DEFAULT by default.
* **endgame**: This marks if using the item triggers the endgame.

This is an example of a XML of a NoInteractableItems:
```xml
<Item>
    <id>3</id>
    <name>nota</name>
    <description>Esta es una nota y al leerla te cuenta cosas</description>
    <useMessage>Informacion importante para el jugador</useMessage>
    <itemType>NOTE</itemType>
    <endgameTrigger>false</endgameTrigger>
  </Item>
  ```
  
### Keys

A Key can open either a Door or a Chest. It has the following attributes:

* **id**: Id who identifies the Key. Must be unique and can't be repeated. 
* **name**: The name of the Key.
* **description**: Description of the Key.
* **message**: Message which will show up if the player uses the Key without a Door or a Chest.  
* **type**: Type of the item. It's a Key so it must be a KEY. KEY by default.
* **containerID**: The id of the Door or the Chest which is opened by this Key.
* **endGameTrigger**: A Key can't launch an EndgameTrigger, but it must be setted to false.

This is an example of a XML of a Key:

```xml
<Key>
    <id>0</id>
    <name>Llave de la puerta de la habitación</name>
    <description>Llave que abre la puerta de la habitación</description>
    <useMessage>No ves que sentido tiene usar una llave en el aire</useMessage>
    <itemType>KEY</itemType>
    <containerId>0</containerId>
    <endgameTrigger>true</endgameTrigger>
  </Key>
  ```

### Chests

A Chest is an Item which have Items inside. It has the following attributes:

* **id**: Id who identifies the chest. Must be unique and can't be repeated. 
* **name**: The name of the chest.
* **description**: Description of the chest.
* **message**: Message which will show up if the player uses the chest without a Key.
* **open**: This marks if the chest is open. False by default.
* **blocked**: This marks if the chest is blocked and you need a Key to open it. False by default.
* **keyId**: The Id of the Key which opens the chest.
* **itemType**: Type of the item. CHEST by default.
* **containerType**: Type of the container. CHEST by default.
* **itemList**: List with all the ids of the items which are inside of the chest.
* **endgame**: This marks if opening the chest triggers the endgame.

This is an example of a XML of a Chest:

```xml
<Chest>
    <id>2</id>
    <name>Cofre</name>
    <description>Es un cofre de prueba</description>
    <useMessage>Un cofre cerrado con llave, no puedes usarlo sin una.</useMessage>
    <open>false</open>
    <blocked>true</blocked>
    <keyId>1</keyId>
    <itemType>CHEST</itemType>
    <containerType>CHEST</containerType>
    <itemsInside>
      <item>3</item>
      <item>7</item>
    </itemsInside>
    <endgameTrigger>false</endgameTrigger>
  </Chest>
  ```

### Usable Furniture

Items which are usable alone. They can't be grabbed and they can be only used alone to open Doors or Chests. An UsableFurniture has the following attributes:

* **id**: Id who identifies the item. Must be unique and can't be repeated. 
* **name**: The name of the item.
* **description**: Description of the item.
* **message**: Message which will show up if the player uses the item.
* **type**: Type of the item. Since it's an UsableFurniture, it must be USABLE_FURNITURE.
* **containerId**: The if of the Container (Door or Chest) which is opened if this Item is used.
* **used**: This marks if the Item has been used. False by default.
* **endgame**: This marks if using the item triggers the endgame.

This is an example of a XML of a Usable Furniture:

```xml
<UsableFurniture>
    <id>4</id>
    <name>Botón</name>
    <description>Es un boton, parece que se puede pulsar</description>
    <useMessage>Has pulsado el boton, se oye un ruido a lo lejos</useMessage>
    <itemType>USABLE_FURNITURE</itemType>
    <containerId>2</containerId>
    <used>false</used>
    <endgameTrigger>false</endgameTrigger>
  </UsableFurniture>
  ```

### EndGame Tale

This is the text which will be appearing in the screen if the player completes the game and triggers and EndGameEvent. By default is a congratulations message, but you can customize. 

This is an example of a XML of a EndGame Tale:

```xml
<string>Tras mucho esfuerzo, llegas al final.</string>
<string>Tus esfuerzos han sido recompensados.</string>
  ```
## Future Updates

This is a list of the things I want to implement in the future, they may or not may be end implemented:

- [ ] Multilenguage (Currently only working in spanish)
- [ ] NPCs
- [ ] Combat system
