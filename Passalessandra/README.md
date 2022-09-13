### Passalessandra (the brand new customizable Passaparola)

# Goal
Players are asked questions whose answer begins with a given letter of the alphabet. It's like solving a clue in a crossword when you already know the first letter of the answer. Example: 

```
"You must have one to log in" -> "Account"
"The Indian Hollywood" -> "Bollywood"
...
```

Answers can be single or multiple words, names or whatever you'd like. As a rule of thumb, take the very first letter of the expression the one that matters.

Players have limited time to answer all or most questions. 
During each question, each player can answer or pass. If they answer correctly they can go on to the next question; if they fail or pass, their game pauses and the opponent starts playing. When a player wants to pass, they say: "Passalessandra!" (or every magic word of your choice).

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
You can put your desired questions/clues for each player into a JSON file named `custom_qaA.json` and `custom_qaB.json`. You can a template in this repository. 

The application will search for these files via PersistentPath, so put them into (the folder that your OS requires)[https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html].

When the game starts, these lists will be loaded for the two players.

## Coming soon
* Add menus
* Load custom images for your player's profile.