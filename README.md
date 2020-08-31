# ProGenGE1RepeatAssignment
ProGenGE1RepeatAssignment


    A detailed description of what your assignment does and how it works
    Which parts of the assignmemnt you developed yourself vs parts that come from the examples we made on the course or that come from tutorials
    What you are most proud of about the assignment
    Instructions for building and running (if necessary)
    Include an embedded youtube video of the assignment in the readme.md and also submit the Youtube video link in the submission form. If you are on Windows 10 you can press     
    Windows Key, Alt and R to take a video. This is how you can capture videos if you are on a Mac. Also you can use OBS.


Hi Bryan, Regarding Instructions for building,  I tried to build it and it threw an error; cannot build player while editor is importing assets or compiling scripts - I'm not sure what this is about?. The game runs fine in editor but I've tried to build it several times and still get the saame error.


For this assignment I have attempted to develop a procedurally generated city grid, taking inspiration from this video; https://www.youtube.com/watch?v=zBDrH3lg4YY
I developed the entire system myself and figured out what the workings of it would be between my head and a few scraps of paper, however all of the code is wrangled from all across the internet and mangled together in my own special way. I will say that I figured out how to make this work and built the framework for the system in my own head, I understood what I had to do and I knew theoretically how to go about doing it,  I just didn't exactly know how to translate that to code but I am proud of the outcome even though it's one monolithic class and it is unfinished work.
Basically my system records the mouse position at the first two click points and stores them in two lists, one for permanent reference and another temp list which removes its range after every pair of clicks. On the second click my system uses line renderer to draw a line between the first and second click, it took me a while to figure out how to prevent the line from continuously being drawn from the last point to the next point without any break until I came up with using a temp list. So then once a player is happy with x amount of lines they have clicked into existence they can use a generate button which calls a method which takes in all of the vectors that make up these lines and then generates random points on those lines and then generates points, one to the left one to the right of those points and then renders a new line from that point to a distance based on the length of the original line. It took me ages to figure this out and get it to work. It uses a calculation that I got from the internet that basically uses Quaternion.angle axis to calculate a point at whatever specified angle from another point. As you will see from my awful code I just carried on repeating this process again and again passing lists from one method to another method that does exactly the same thing only with the new set of coordinates for new lines and then I use a bunch of bools as switches to get something approximating my desired result. Once a player is happy with all the lines they have procedurally generated they can then populate the city grid with buildings which use exactly the same system, they are just lists of fatter squatter render lines with coordinates based on all the points on lines that haven't already been used in another list, these use a coroutine to "animate" into existence one after another. That's pretty much it, I use randomised colours too, well, a random selection from a pallette of chosen colours.


I'm most proud of having figured out how to emulate something I was inspired by, albeit pretty poorly. I'm least proud of not being capable enough to refactor my code.



[![YouTube](http://img.youtube.com/vi/kKyPNQu9SCg/0.jpg)](https://www.youtube.com/watch?v=kKyPNQu9SCg)
