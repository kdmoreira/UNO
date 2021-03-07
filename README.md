# Campinas Tech Talents

I love UNO since I've first played this simple, yet addictive, card game, and in order to improve my object-oriented programming skills, I've decided to refactor a console-based UNO game I had previously coded in Python.

This is a "Player vs PC" game and I've been coding it without any tutorials or other people's code as reference. As soon as I finish this version, I plan to see if other people have also coded this game in order to see how differently they've done it and how mine could be improved.

In the Python version of the game I used an imperative approach and way too many anti-patterns, which made me run into a lot of problems... Now using C# and the knowledge I've recently acquired regarding OOP, the main advantage of this paradigm was clear from the start: it makes the program much more organized since a UNO game can be easily seen as something made of different objects, e.g.: a Deck, a hand of cards, the cards themselves, the game itself.

Despite the aforementioned advantages, the task still has its challenges. One of the main ones I faced was to reduce tight coupling in the game mechanics. I noticed some methods were becoming very dependent on each other and this was resulting in a circular dependency (the previous Python version suffered a lot from this). I've tried to split each mthod into smaller ones with one single task. Instead of including verifications inside of them, I externalized a control system by making use of Game properties such as PlayerTurn and Active game. I'm still working on the game mechanics.

I suspect the code isn't free of flaws and the design could have been better. If you spot something that could be improved, please let me know!
