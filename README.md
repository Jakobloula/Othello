# Othello

# The project 
This is my first project where i used C# for the backend and XAML for the frontend. In this project i learned a lot and some of the things like using threads, databinding, Events and etc. 

# How to play 
Othello is a two player game where one player has black disks and the other white disks. The disks are black on one side and white on the other. The game is played on an 8x8 grid, where the initial state consists of two black and two white disks arranged diagonally on the four middle tiles. The black player alwyas move first. 

A move in the game involves a player a disk on the board in a way that captures at least one of the opponent's disks. A disk is considerd captured if it lies directly between the newly placed disk and another disk of the current player, with no empty spaces between them. The captured disks can be in a horizontal, vertical, or diagonal line. 

For example, during the black player's first move, there are typically four possible valid moves that can capture white disks, based on the initial game setup. After the move, all of the opponent's disks that were captured are flipped to the current player's color. This flipping process affects entire rows, columns, and diagonals of the opponent's disks if they are caught between two of the current player's disks without any gaps.

Once the move is completed, it becomes the next player's turn. If a player cannot make a valid move, the turn is passed to the opponent. The game continues until the board is completely filled or no player can make a move. The player with the most disks of their color on the board at the end of the game wins. If both players have the same number of disks, the game ends in a draw.

# Game modes
The game supports three different gameplay modes: Human vs Human, Human vs Computer and Computer vs Computer. The computer is just a simple randomizer which adds a disk on a valid move. 