using System.Collections.Generic;
using System.Runtime;
using System.Runtime.CompilerServices;
public class Player
{
    public string name;
    public int weaponQuality;
    public int luck;
    public double Health;
    public List<Spell>? spells;

    public Status? imbuement;

    public bool isAlive;

    public double health
    { get; set; }

    public int WeaponQuality
    { get; set; }

    public int Luck
    { get; set; }

    public List<Status>? Statuses
    { get; set; }

    public List<Spell>? Spells
    { get; set; }

    public Status? Imbuement
    { get; set; }

    public bool isDead;

    public bool DeathsDoor;
    public void Slash(Player Target)
    {
        Random damageGenerator = new Random();
        double damage;
        switch (weaponQuality)
        {
            case 0:
                damage = damageGenerator.Next(10, 20);
                break;
            case 1:
                damage = damageGenerator.Next(25, 30);
                break;
            case 2:
                damage = damageGenerator.Next(30, 45);
                break;
            case 3:
                damage = damageGenerator.Next(45, 50);
                break;
            case 4:
                damage = damageGenerator.Next(65, 80);
                break;
            default:
                damage = 0;
                break;
        }
        foreach (Status s in this.Statuses)
        {
            if (s.changesDamageDealt == true)
            {
                Math.Round(damage *= s.damageDealtChange);
            }
            else
            {
                continue;
            }
        }
        foreach (Status s in Target.Statuses)
        {
            if (s.changesDamageTaken == true)
            {
                Math.Round(damage *= s.damageTakenChange);
            }
            else
            {
                continue;
            }
        }
        Target.health -= damage;
        if (this.imbuement != null)
        {
            Target.Statuses.Add(imbuement);
            imbuement = null;
            Console.WriteLine($"You strike {Target.name} with your blade, dealing {damage} damage! You also applied your imbuement!");
        }
        else
        {
            Console.WriteLine($"You strike {Target.name}with your blade, doing {damage} damage!");
        }

    }

    public void Defend()
    {
        bool isDefending = false;
        foreach (Status s in Statuses)
        {
            if (s.statusID == 4)
            {
                s.Duration += 1;
                isDefending = true;
                break;
            }
            else
            {
                continue;
            }
        }

        if (isDefending == false)
        {
            Statuses.Add(new Status(4, 1));
        }
    }

    public void Draw(int luck)
    {
        int chosenList = 0;
        List<int> quality0 = new List<int>() { 1, 2, 3, 4, 5 };
        List<int> quality1 = new List<int>() { 6, 7, 8, 9 };
        List<int> quality2 = new List<int>() { 10, 11, 12, 13, 14, 15, 16, 17, 18 };
        List<int> quality3 = new List<int>() { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };
        List<int> quality4 = new List<int>() { 30, 31, 32, 33, 34, 35, 36, 37 };
        Random rng = new Random();
        int rolledNumber = rng.Next(1, 100);
        Console.WriteLine(rolledNumber);
        switch (luck)
        {
            case -2:
                if (rolledNumber <= 50)
                {
                    chosenList = 0;
                }
                else if (rolledNumber > 50 && rolledNumber < 80)
                {
                    chosenList = 1;
                }
                else
                {
                    chosenList = 3;
                }
                break;
            case -1:
                if (rolledNumber <= 30)
                {

                    chosenList = 0;
                }
                else if (rolledNumber > 30 && rolledNumber <= 75)
                {
                    chosenList = 1;
                }
                else if (rolledNumber > 75 && rolledNumber <= 95)
                {
                    chosenList = 2;
                }
                else
                {
                    chosenList = 3;
                }
                break;
            case 0:
                {
                    if (rolledNumber <= 15)
                    {
                        chosenList = 0;

                    }
                    else if (rolledNumber > 15 && rolledNumber <= 40)
                    {
                        chosenList = 1;

                    }
                    else if (rolledNumber > 40 && rolledNumber <= 70)
                    {
                        chosenList = 2;
                    }
                    else if (rolledNumber > 70 && rolledNumber <= 90)
                    {
                        chosenList = 3;
                    }
                    else if (rolledNumber > 90 && rolledNumber <= 100)
                    {
                        chosenList = 4;
                    }
                    break;
                }
            case 1:
                if (rolledNumber <= 5)
                {
                    chosenList = 1;
                }
                else if (rolledNumber > 5 && rolledNumber <= 25)
                {
                    chosenList = 2;
                }
                else if (rolledNumber > 25 && rolledNumber <= 70)
                {
                    chosenList = 3;
                }
                else
                {
                    chosenList = 4;
                }
                break;
            case 2:
                if (rolledNumber <= 20)
                {
                    chosenList = 2;
                }
                else if (rolledNumber > 20 && rolledNumber <= 60)
                {
                    chosenList = 3;
                }
                else
                {
                    chosenList = 4;
                }
                break;
        }

        switch (chosenList)
        {
            case 0:
                Spells.Add(new Spell(quality0[rng.Next(0, quality0.Count - 1)]));
                break;
            case 1:
                Spells.Add(new Spell(quality1[rng.Next(0, quality1.Count - 1)]));
                break;
            case 2:
                Spells.Add(new Spell(quality2[rng.Next(0, quality2.Count - 1)]));
                break;
            case 3:
                Spells.Add(new Spell(quality3[rng.Next(0, quality3.Count - 1)]));
                break;
            case 4:
                Spells.Add(new Spell(quality4[rng.Next(0, quality4.Count - 1)]));
                break;
        }
        foreach (Status s in Statuses)
        {
            if (s.luckChange != 0)
            {
                s.duration--;
            }
        }
    }
    public Player(string name, double health = 1000)
    {
        this.name = name;
        Health = health;
        WeaponQuality = 1;
        Luck = 0;
        isAlive = true;
        Spells = new List<Spell>();
        Statuses = new List<Status>();
    }
}