# Console-based UNO game

I love UNO since I've first played this simple, yet addictive, card game, and in order to improve my object-oriented programming skills, I've decided to refactor a console-based UNO game I had previously coded in Python.

This is a "Player vs PC" game and I've been coding it without any tutorials or other people's code as reference as a way of forcing myself to think deeply about the possible solutions. As soon as I finish this version, I plan to see if other people have also coded this game in order to see how differently they've done it and how mine could be improved.

In the Python version of the game I used an imperative approach and way too many anti-patterns, which made me run into a lot of problems... Now using C# and the knowledge I've recently acquired regarding OOP, the main advantage of this paradigm was clear from the start: it makes the program much more organized since a UNO game can be easily seen as something made of different objects, e.g.: a deck, a hand of cards, the cards themselves, the game itself.

Despite the aforementioned advantages, the task still has its challenges. One of the main ones I faced was to reduce circular references in the game mechanics, in this case, circular method calls, something the previous Python version suffered a lot from. I've tried to split each method into smaller ones with one single task and make calls in a more linear fashion, externalizing a control system by making use of Game properties such as PlayerTurn and Active game to control loops. I'm still working on the game mechanics and lately I've been reflecting if everything is as it should be or if certain methods actually belong to other classes.

I'm sure the code isn't free of flaws and the design could have been better. I look forward to making this game work to check other people's versions, but I'm open to suggestions, if you spot something that could be improved, please let me know.
