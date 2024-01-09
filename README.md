# Composite-quests-generator
Generation of composite quests from parts without repeating the result and without endless loops when choosing random parts.

![Image alt](https://github.com/Siuniaev/Composite-quests-generator/blob/main/promo.jpg)

## What problem does it solve?
There are situations (I see this in game development) when you need to create variants of game objects from component parts randomly. These can be quests, locations, robots, craft weapons and much more. A simple task turns out to be not simple when you have to create a large number of such objects without repetition: identical objects are created very often, and there is not enough memory and time to generate all possible options at once!

## How It Works?
We build a tree of available options and roll the random, that's it. To avoid repetitions, we delete completed variants-tree branches. In order not to waste a lot of memory, we build new branches only as the next new “growth point” is randomly selected.
![Image alt](https://github.com/Siuniaev/Composite-quests-generator/blob/main/tree_view.png)