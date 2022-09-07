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
      </ul>
     </li>
     <li>
      <a href="#documentation-for-programmers">Documentation for programmers</a>
     </li>
     <li>
      <a href="#documentation-for-designers">Documentation for designers</a>
       <ul>
       <li><a href="#world">World</a></li>
        <li><a href="#rooms">Rooms</a></li>
      </ul>
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

<!-- PROGRAMMER INFO -->

## Documentation for programmers

<!-- DESIGNINFO -->

## Documentation for designers

*This section might change in the future, take the instructions with a pint of salt*

Let's get down to business, we've come here to design a videogame.

### World

First things first, here we have a World which include all the elements present in the game. Inside the World there are the next elements:

*  **Rooms**: Each Room in the game, inside each room there are items.
* **Keys**: The Item who open Doors or Chests.
* **Notes**: Items which can't be dropped.
* **Chests**: Items which contains more items.
* **Doors**: Doors who may or not block the path to exit the Room.

We are going to visit every one of them in the next points.

### Rooms

Each Room in the game must be represented in an object. A Room has the following attributes:

* **id**: Id who identifies the room. Must be unique and can't be repeated. 
* **name**: The name of the room.
* * **description**: Description of the room, this would appear each time the player enters.
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
    <description>"Es una habitacion normal"</description>
    <story>
      <page>"El diseñador entro, sin sabe muy bien que hacía ahí."</page>
      <page>"Finalmente, abrió su editor de texto y comenzó a crear unos cuantos xmls."</page>
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
    </items>
    <doors>
      <door>0</door>
      <door>-1</door>
      <door>-1</door>
      <door>-1</door>
    </doors>
    <endgame>false</endgame>
    <visited>false</visited>
  </room>
```

