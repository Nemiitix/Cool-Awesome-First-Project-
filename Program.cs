using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
List<Player> Players = new List<Player>();
int livingPlayers = 0;
Spell ChooseSpell(Player Caster)
{
    int count = 1;
    bool move = false;
    int chosenID = 0;
    foreach (Spell s in Caster.Spells)
    {
        Console.WriteLine($"{count}: {s.name}");
        count += 1;
    }
    while (!move)
    {
        string target = Console.ReadLine();
        if (Int32.TryParse(target, null, out count))
        {
            move = true;
            chosenID = Int32.Parse(target);
        }
        else
        {
            Console.WriteLine("Remember to type ONLY the digit corresponding to the spell you want to choose.");
        }
    }
    try
    {
        return Caster.Spells[chosenID - 1];
    }
    catch
    {
        Console.WriteLine("Make sure you're only entering one of the listed numbers!");
        return ChooseSpell(Caster);
    }
}

Player ChoosePlayers(Player Caster)
{

    // This is the best way I could do this, please be patient with me D:
    bool move = false;
    int count = 1;
    int chosenID = 0;
    List<Player> viableTargets = new List<Player>();
    Console.WriteLine("Choose a Player to target");
    foreach (Player player in Players)
    {
        if (player != Caster && player.isAlive)
        {
            viableTargets.Add(player);
            Console.WriteLine($"{count}: {player.name}");
            count += 1;
        }
    }
    while (!move)
    {

        string target = Console.ReadLine();
        if (Int32.TryParse(target, null, out count))
        {
            chosenID = Int32.Parse(target);
            move = true;
        }
        else
        {
            Console.WriteLine("Make sure you're inputting the digit corresponding to the player you want to target!");
        }
    }
    try
    {
        return viableTargets[chosenID - 1];
    }
    catch
    {
        Console.WriteLine("Make sure you're only entering one of the listed numbers!");
        return ChoosePlayers(Caster);
    }
        
}

Status ChooseStatus(Player Caster)
{
    List<Status> AvailableStatuses = new List<Status>();
    bool move = false;
    int count = 1;
    int ChosenID = 0;
    foreach (Status s in Caster.Statuses)
    {
        
        Console.WriteLine($"{count}: {s.name}");
        AvailableStatuses.Add(s);
        count += 1;
    }
    string Target = Console.ReadLine();
    while (!move)
    {
        if (Int32.TryParse(Target, null, out count))
        {
            move = true;
            ChosenID = Int32.Parse(Target);
        }
        else
        {
            Console.WriteLine("Make sure to type only the digit that corresponds to the action you want to take!");
        }
    }
    try
    {
        return AvailableStatuses[ChosenID - 1];
    }
    catch
    {
        Console.WriteLine("Make sure you're only entering one of the listed numbers!");
        return ChooseStatus(Caster);
    }
}
bool keepgoing = false;
while (keepgoing == false)
{
    Console.WriteLine("Welcome to the game! Type \"1\" to start the game. Press \"2\" to read about the game!");
    string choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            bool moveon = false;
            while (!moveon)
            {
                Console.WriteLine("Enter your name, then pass the computer to the next person. Leave the name blank when you're finished.");
                string newPlayer = Console.ReadLine();
                if (Players.Count < 3)
                {
                    Players.Add(new Player(newPlayer));
                    Console.WriteLine($"Player {Players.Count}: {newPlayer}");
                }
                else if (string.IsNullOrEmpty(newPlayer))
                {
                    Console.WriteLine("Moving on!");
                    moveon = true;
                }
                else
                {
                    Players.Add(new Player(newPlayer));
                    Console.WriteLine($"Player {Players.Count}: {newPlayer}");
                    Console.WriteLine("Moving on!");
                    moveon = true;
                }
            }
            Console.WriteLine("The game is starting!");
            livingPlayers = Players.Count;
            Player winningPlayer = null ;
            while (livingPlayers > 1)
            {
                foreach (Player CurrentPlayer in Players)
                {
                    //Handle statuses at the beginning of turn (Does it skip the turn? Does it deal damage? reduce the duration if it isn't a luck changing status, etc etc.)
                    if (CurrentPlayer.isAlive)
                    {
                        moveon = false;
                        Console.WriteLine($"{CurrentPlayer.name}'s turn!");
                        double statusDamage = 0;
                        foreach (Status s in CurrentPlayer.Statuses)
                        {
                            switch (s.damageType)
                            {
                                case 1:
                                    {
                                        break;
                                    }
                                case 2:
                                    {
                                        statusDamage += CurrentPlayer.Health * s.damage;
                                        break;
                                    }
                                case 3:
                                    {
                                        statusDamage += 1000 * s.damage;
                                        break;
                                    }
                            }
                            if (s.isBinding)
                            {
                                moveon = true;
                                Console.WriteLine("Your turn was skipped!");
                            }


                            if (s.luckChange == 0)
                            {
                                s.duration--;
                            }
                        }
                        if (statusDamage > 0)
                        {
                            Console.WriteLine($"{CurrentPlayer.name} took {statusDamage} damage from various causes!");
                            CurrentPlayer.Health -= statusDamage;
                        }
                        CurrentPlayer.Statuses.RemoveAll(status => status.duration < 0);
                        Math.Round(CurrentPlayer.Health);
                        //Player Takes their turn, choosing which action they want to take.
                        if (CurrentPlayer.Health <= 0)
                        {
                            CurrentPlayer.DeathsDoor = true;
                            Console.WriteLine($"This is your last action, {CurrentPlayer.name}. Make it count. (You will be eliminated after this turn. You deal increased damage on all attacks, but you only get one.)");
                            CurrentPlayer.Statuses.Add(new Status(17, 1));
                        }
                        while (!moveon)
                        {
                            Console.WriteLine("Choose what you want to do.\n Type \"S\" to slash.\n Type \"D\" to draw a spell.\n Type \"C\" to cast a spell.\n Type \"N\" to defend.\n Press \"M\" for more options.");
                            choice = Console.ReadLine();

                            switch (choice.ToUpper())
                            {
                                //Slash, dealing damage and applying the imbuement
                                case "S":
                                    CurrentPlayer.Slash(ChoosePlayers(CurrentPlayer));
                                    moveon = true;
                                    break;
                                case "D":
                                    //Draw a spell.
                                    if (CurrentPlayer.Spells.Count < 5)
                                    {
                                        foreach (Status status in CurrentPlayer.Statuses)
                                        {
                                            CurrentPlayer.luck = status.luckChange;
                                        }
                                        CurrentPlayer.Draw(CurrentPlayer.luck);
                                        CurrentPlayer.luck = 0;
                                        moveon = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("You have the maximum amount of spells! (5) Cast some of them or try a different action before drawing new cards!");
                                    }
                                    break;
                                case "C":
                                    {
                                        //Cast a spell (Doesn't do anything if they don't have any left.)
                                        if (CurrentPlayer.Spells.Count > 0)
                                        {
                                            Spell selectedSpell = ChooseSpell(CurrentPlayer);
                                            if (selectedSpell.hasTarget)
                                            {
                                                selectedSpell.method(CurrentPlayer, ChoosePlayers(CurrentPlayer));
                                            }
                                            else
                                            {
                                                selectedSpell.method(CurrentPlayer);
                                            }
                                            CurrentPlayer.Spells.Remove(selectedSpell);
                                            moveon = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("You don't have any spells! Try drawing some before you cast one!");
                                        }
                                        break;
                                    }
                                case "N":
                                    // Defend, halving all damage at the cost of their turn
                                    CurrentPlayer.Defend();
                                    moveon = true;
                                    break;
                                case "M":
                                    //More stuff! Mainly info on what spells they have, or how they're doing.
                                    Console.WriteLine("More Options (These won't take up your turns!)\n \"S\": View your statuses.\n \"C\": Get info on one of your spell cards.\n \"I\": View your general info.");
                                    {
                                        choice = Console.ReadLine();
                                        switch (choice.ToUpper())
                                        {
                                            //View Statuses
                                            case "S":
                                                {
                                                    if (CurrentPlayer.Statuses.Count > 0)
                                                    {
                                                        Console.WriteLine("Get information on one of your statuses!");
                                                        Status chosenStatus = ChooseStatus(CurrentPlayer);
                                                        Console.WriteLine($"{chosenStatus.name}:\n Effect: {chosenStatus.description}\n Turns Left: {chosenStatus.duration}");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("You don't have any status effects! Make sure to count your blessings while you still have them...");
                                                    }
                                                    break;
                                                }
                                            //View Spells
                                            case "C":
                                                if (CurrentPlayer.Spells.Count > 0)
                                                {
                                                    Console.WriteLine("Get Information on one of your spells!");
                                                    Spell chosenSpell = ChooseSpell(CurrentPlayer);
                                                    Console.WriteLine($"{chosenSpell.name}\n {chosenSpell.description}");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("You don't have any spells yet! Try drawing some before you check them.");
                                                }
                                                break;
                                            //General Information, Imbuement, Etc.
                                            case "I":
                                                Console.WriteLine($"{CurrentPlayer.name}: \n Health: {CurrentPlayer.Health} / 1000. \n Spells: ");
                                                foreach (Spell spell in CurrentPlayer.Spells)
                                                {
                                                    Console.Write($"{spell.name}, ");
                                                }
                                                Console.WriteLine("\n Statuses: ");
                                                foreach (Status status in CurrentPlayer.Statuses)
                                                {
                                                    Console.WriteLine($"{status.name}, ");
                                                }
                                                Console.WriteLine($"Weapon Quality: {CurrentPlayer.weaponQuality}");
                                                if (CurrentPlayer.imbuement != null)
                                                {
                                                    Console.WriteLine($"Imbuement {CurrentPlayer.imbuement.name} ({CurrentPlayer.imbuement.description})");
                                                }
                                                break;
                                        }
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Make sure to type in the exact letter corresponding to the action you want to take!");
                                    break;
                            }
                        }
                        if (CurrentPlayer.DeathsDoor)
                        {
                            CurrentPlayer.isAlive = false;
                            Console.WriteLine($"{CurrentPlayer.name} has perished!");
                            livingPlayers -= 1;
                        }
                        Console.WriteLine("Your turn is over. Pass the computer to the next person, or press enter if it is your turn next!");
                        Console.ReadLine();

                    }
                    else
                    {
                        continue;
                    }
                }
            }
            foreach (Player player in Players)
            {
                if (player.isAlive)
                {
                    winningPlayer = player;
                }
            }
            //Who won? (Who's next? You decide!)
            if (winningPlayer != null)
            {
                Console.WriteLine($"{winningPlayer.name} has won the game with {winningPlayer.health} health remaining!");
            }
            else
            {
                Console.WriteLine("At the edge of nowhere, two corpses lie motionless... Somehow, you both died. Nobody won.");
            }
            break;
        case "2":
            Console.WriteLine("Welcome to the game! This is my first \"Project\" so to speak, so bear with me if it's a bit clunky.");
            bool move = false;
            while (!move)
            {
                Console.WriteLine("To Learn About... \n The Game Itself: Type 1. \n Status Effects: Type 2. \n Drawing Spells: Type 3. \n Imbuements: Type 4. \nTo move on, press 5.");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("The goal of the game is to defeat your foes with your blade and your spells. There are four actions you can take: \n Slash: Deal damage based on your weapon quality. \n Defend: Halve all damage you take at the cost of your turn. \n Draw: Draw a spell. Spells differ in quality, with higher quality cards being rarer. \n Cast: Cast a spell. Casting spells can turn the tides of battle quickly, giving you an advantage, or the enemies a disadvantage. \nUsing these options Strategically and effectively will allow you to dominate the battlefield.");
                        break;
                    case "2":
                        Console.WriteLine("Status Effects can dramatically change the course of a battle. They range from doing small amounts of damage to skipping turns entirely!");
                        Console.WriteLine("The following is a list of status effects. Don't worry if you can't memorize them. You ask for a description when you view your statuses.\n Poison: Take 5% of your current health in damage every turn.\n Venom: Take 10% of your current health in damage every turn.\n Burn: Take 5% of your max health in damage every turn.\n Scorch: Take 10% of your max health in damage every turn.\n Defense: Halve the amount of damage you take.\n Weakened: Deal 20% Less damage.\n Incapacitated: Deal 40% Less damage.\n Bleeding: Take 10% Extra damage.\n Laceration: Take 20% Extra Damage.\n Luck: Improves Card Draws. Does not decay on turn start, instead decaying when you draw a card. \n Fortune: Drastically Improves Card Draws.\n Bad Luck: Worse Card Draws.\n Misfortune: REALLY Bad Card Draws");
                        break;
                    case "3":
                        Console.WriteLine("Spells can have a quality of 0 to 4. Higher quality spells are rarer, but also more powerful. When drawing a spell, luck plays an important part in what cards you get. Statuses that affect luck will affect the probability of drawing a good spell.");
                        break;
                    case "4":
                        Console.WriteLine("Imbuements are accessed through relatively rare spells. These Imbuements power up your slashes, making them inflict a statys effect. However, you can only have one Imbuement at a time, and they can only apply the status once before it is consumed.");
                        break;
                    case "5":
                        move = true;
                        break;
                    default:
                        Console.WriteLine("Make sure you type exactly what is prompted. Otherwise, you'll have to input it again.");
                        break;
                }
            }
            break;
    }
}