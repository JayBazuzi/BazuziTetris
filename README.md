BazuziTetris
------

For some reason, I have always felt stuck around writing games. I decided to get unstuck, and this is the result.

Tetris is a good game to get unstuck on because it's quite simple, yet still includes the core game loop that you find in just about every game. You have to track both a time and asynchronous user inputs, and update accordingly.

I don't make any claim about this being wonderful code. However, many of the other Tetris implementations I've seen were rather ugly to my eye. One thing I think I got right is decoupling the game engine from the UI. In fact, if you look in the history, you'll see an earlier console implication.

I mostly approached this with TDD, although there are some non-TDD bits here and there.


