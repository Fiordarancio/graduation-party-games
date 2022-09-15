# Passalessandra 
 Just a nice, customizable Passaparola.
 
![Example of game loop.](https://github.com/Fiordarancio/graduation-party-games/blob/main/Passalessandra/docs/example_game_loop.png "Example of game loop")

# Goal
 Players are asked questions whose answer begins with a given letter of the alphabet. It's like solving a clue in a crossword when you already know the first letter of the answer. Example: 

```
"You must have one to log in" -> "Account"
"The Indian Hollywood" -> "Bollywood"
...
```

Answers can be single or multiple words, names or whatever you'd like. As a rule of thumb, take the very first letter of the expression as the one that matters.

Players have limited time to answer all or most questions. 
During each question, each player can answer or pass. If they answer correctly they can go on to the next question; if they fail or pass, their game pauses and the opponent starts playing. When a player wants to pass, they say: "Passalessandra!" (or any magic word of your choice).

The player who answers all or most questions first wins.

# Controls
 By default, the control activates the left player's UI.

Use mouse click to select the letter to activate and start playing. 
The green button sets that letter as correct, while the yellow one as idle and the red one as wrong. In the last two cases, the control is passed to the other player.
The small blue button on the right resets the status of the letter to default.
The blue button with the clock icon restores some seconds for the active player.

Each of the aformentioned controls can be issued by keyboard. Press:
* Z,C to select the previous/next unanswered question
* F to mark it correct
* G to mark it idle
* H to mark it wrong
* N to reset the letter
* M to restore time

# Prepare your questions
 The application would automatically save custom question-answer pairs into JSON files, that resede in the [persistent data directiory](https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html), under the folder `savings`. Once started, the application will search for a JSON file via the following paths:

```
Application.persistentDataPath + /savings/custom_qas_playerA.json 
Application.persistentDataPath + /savings/custom_qas_playerB.json
```

You can find a template for questions in this repo. The application will search for these files via [Application.persistentDataPath](https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html), so put them into the folder that your OS requires. If there is no such file, predefined lists will be loaded

# Coming soon
* Add menus
* Load custom images for players' profile

# Disclaimer
 The main loop music has been sampled from the original show on YouTube. All rights belong to the respective owners.
 
 Other sound fx are under free license.
