BazuziTetris
------

For some reason, I have always felt stuck around writing games. I decided to get unstuck, and this is the result.

Tetris is a good game to get unstuck on because it's quite simple, yet still includes the core game loop that you find in just about every game. You have to track both a time and asynchronous user inputs, and update accordingly.

If you're thinking about studying it to learn something: I don't make any claim about this being wonderful code. 
However, many of the other Tetris implementations I've seen were rather ugly to my eye, and I think I did better than that. 

One thing I think I got right is decoupling the game engine from the UI. In fact, if you look in 
the history, you'll see an earlier console UI implementation.

I mostly approached this with TDD, although there are some non-TDD bits here and there.

The game is basically functional, but is missing many of the things you'd expect in a complete Tetris clone. For example:

- Block color variation.
- Z, S, and T-shaped blocks.
- Losing when you hit the top
- Speeding up over time
- A next piece preview.
- Random sequence of pieces (using a fixed seed for predictability)
- Scoring points
- Sound
- Animation when clearing rows

I don't intend to carry it further. I have accomplished my goal.
