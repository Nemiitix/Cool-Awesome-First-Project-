using System.Security.Cryptography.X509Certificates;

class Methods
{
    double DamageMultiplier(Player Caster, Player Target = null)
    {
        double multiplier = 1;
        foreach (Status s in Caster.Statuses)
        {
            if (s.changesDamageDealt)
            {
                multiplier *= s.damageDealtChange;
            }
        }
        foreach (Status s in Caster.Statuses)
        {
            if (s.changesDamageTaken)
            {
                multiplier *= s.damageTakenChange;
            }
        }
        return multiplier;
    }


    bool AlreadyAfflicted(List<Status> Statuses, int statusID, int duration)
    {
        bool alreadyAfflicted = false;
        foreach (Status s in Statuses)
        {
            if (s.statusID == statusID)
            {
                s.Duration += duration;
                alreadyAfflicted = true;
            }
        }
        return alreadyAfflicted;
    }

    // SPITE - Literally fucking DIE. Halve your targets health.
    public void SPITE(Player Caster, Player Target = null)
    {
        Caster.Health = 0;
        double damage = Target.Health / 50;
        Target.Health -= damage;
        Console.WriteLine($"You pour every ounce of hate into a spell. You half {Target.name}'s life force! Unforunately, your body could not handle the strain you put on it. You perish for the rest of the game.");
    }
    // Positive Affirmation - You do 1% extra damage next turn
    public void PositiveAffirmation(Player Caster, Player Target = null)
    {
        Caster.Statuses.Add(new Status(14, 1));
        Console.WriteLine("You tell yourself you're going to do great... You deal 1% more damage next turn!");
    }
    //Mana Needle - Target takes 3% of their max health in damage over three turns.
    public void ManaNeedle(Player Caster, Player Target = null)
    {
        Target.Statuses.Add(new Status(16, 1));
    }

    public void SlightlyIntimidatingStare(Player Caster, Player Target = null)
    {
        Target.Statuses.Add(new Status(15, 1));
        Console.WriteLine($"You look at {Target.name} really hard. They do 1% less damage next turn!");
    }

    public void SlashButWorse(Player Caster, Player Target = null)
    {
        Random RandomNumberGenerator = new Random();
        double damage = 0;

        switch (Caster.WeaponQuality)
        {
            case 0:
                damage = RandomNumberGenerator.Next(10, 20);
                break;
            case 1:
                damage = RandomNumberGenerator.Next(25, 30);
                break;
            case 2:
                damage = RandomNumberGenerator.Next(30, 45);
                break;
            case 3:
                damage = RandomNumberGenerator.Next(45, 50);
                break;
            case 4:
                damage = RandomNumberGenerator.Next(65, 80);
                break;
            default:
                break;
        }
        double multiplier = DamageMultiplier(Caster, Target);
        damage *= multiplier;
        Target.Health -= damage;
    }

    public void Glare(Player Caster, Player Target = null)
    {
        bool alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 5, 2);
        if (alreadyAfflicted == false)
        {
            Target.Statuses.Add(new Status(5, 1));
        }
        Console.WriteLine($"You pierce {Target.name}'s soul with a gaze. They're weakened next turn!");
    }
    // If you're reading this I'm sorry if this gives you an aneurysm I'm just not sure what the "better way" of doing this is-
    public void MinorMagicDie(Player Caster, Player Target = null)
    {
        Random die = new Random();
        int rolled = die.Next(1, 6);
        switch (rolled)
        {
            case 1:
                bool alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 0, 2);

                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(0, 2));
                }
                Console.WriteLine($"You rolled a one! {Target.name} was poisoned!");
                break;
            case 2:
                alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 2, 2);
                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(2, 2));
                }
                Console.WriteLine($"You rolled a two! {Target.name} was burned!");
                break;
            case 3:
                alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 5, 1);


                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(5, 2));
                }
                Console.WriteLine($"You rolled a three! {Target.name} was weakened!");
                break;
            case 4:
                alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 9, 1);
                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(9, 1));
                }
                Console.WriteLine($"You rolled a four! {Target.name} felt unlucky...");
                break;
            case 5:
                alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 11, 1);
                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(11, 2));
                }
                Console.WriteLine($"You rolled a five! A wound opened up on{Target.name}'s body!");
                break;
            case 6:
                alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 13, 1);
                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(13, 1));
                }
                Console.WriteLine($"You rolled a six! {Target.name} was bound in place!");
                break;
            default:
                break;
        }
    }

    public void BeginnersFlame(Player Caster, Player Target = null)
    {
        double damage = 20;
        damage *= DamageMultiplier(Caster, Target);
        Target.Health -= damage;
        Console.WriteLine($"{Target.name} took {damage} damage! However, you didn't adequately control your flame. You also burnt yourself...");
        bool alreadyAfflicted = AlreadyAfflicted(Caster.Statuses, 2, 2);
        if (alreadyAfflicted == false)
        {
            Caster.Statuses.Add(new Status(2, 2));
        }
        if (AlreadyAfflicted(Caster.Statuses, 2, 2))
        {
            Caster.Statuses.Add(new Status(2, 2));
        }
    }

    public void IceNeedles(Player Caster, Player Target = null)
    {
        double damage = 15;
        damage *= DamageMultiplier(Caster, Target);
        Target.Health -= damage * 3;
        Console.WriteLine($"{Target.name} took {damage} damage! However, your frost ran wild! Before you could contain it, you both were weakened by the overwhelming cold...");
        bool alreadyAfflicted = AlreadyAfflicted(Caster.Statuses, 5, 2);
        if (alreadyAfflicted == false)
        {
            Caster.Statuses.Add(new Status(5, 2));
        }
        alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 5, 1);
        if (alreadyAfflicted == false)
        {
            Target.Statuses.Add(new Status(5, 2));
        }
        Target.Health -= damage;
    }

    public void Shank(Player Caster, Player Target = null)
    {
        Random RandomNumberGenerator = new Random();
        double damage = RandomNumberGenerator.Next(20, 50);
        damage *= DamageMultiplier(Caster, Target);
        Target.Health -= damage;
        Console.WriteLine($"{Target.name} took {damage} damage! A wound opened up where they were stabbed...");
        bool alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 11, 2);
        if (alreadyAfflicted == false)
        {
            Target.Statuses.Add(new Status(11, 2));
        }
    }

    public void OverGrowth(Player Caster, Player Target = null)
    {
        Random RandomNumberGenerator = new Random();
        double damage = RandomNumberGenerator.Next(20, 50);
        damage *= DamageMultiplier(Caster, Target);
        Target.Health -= damage;
        Console.WriteLine($"Vines envelop {Target.name}... {Target.name} took {damage} damage!");
        int coin = RandomNumberGenerator.Next(1, 2);
        if (coin == 2)
        {
            bool alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 13, 1);
            if (alreadyAfflicted == false)
            {
                Target.Statuses.Add(new Status(13, 1));
            }
        }

    }

    public void FirePalm(Player Caster, Player Target = null)
    {
        bool alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 3, 2);
        if (alreadyAfflicted == false)
        {
            Target.Statuses.Add(new Status(3, 2));
        }
        alreadyAfflicted = AlreadyAfflicted(Caster.Statuses, 3, 2);
        if (alreadyAfflicted == false)
        {
            Caster.Statuses.Add(new Status(3, 2));
        }
    }



    public void SnakeFangs(Player Caster, Player Target = null)
    {
        Random RandomNumberGenerator = new Random();
        double damage = RandomNumberGenerator.Next(10, 20);
        damage *= DamageMultiplier(Caster, Target);
        Target.Health -= damage;
        bool alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 0, 3);
        if (alreadyAfflicted == false)
        {
            Target.Statuses.Add(new Status(0, 3));
        }
        Console.WriteLine($"You grow fangs and sink them into {Target.name}'s neck. {Target.name} was poisoned!");
    }

    public void SuckerPunch(Player Caster, Player Target = null)
    {
        Random RandomNumberGenerator = new Random();
        double damage = RandomNumberGenerator.Next(30, 60);
        damage *= DamageMultiplier(Caster, Target);
        Target.Health -= damage;
        bool alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 5, 2);
        if (alreadyAfflicted == false)
        {
            Target.Statuses.Add(new Status(5, 2));
        }
        Console.WriteLine($"You take {Target.name} off guard, punching them in the neck & dealing {damage} damage! They are weakened as they are still reeling from the attack.");
    }

    public void Curse(Player Caster, Player Target = null)
    {
        bool alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 9, 1);
        if (alreadyAfflicted == false)
        {
            Target.Statuses.Add(new Status(9, 1));
        }
        Console.WriteLine($"{Target.name} begins to feel unlucky...");
    }

    public void Bless(Player Caster, Player Target = null)
    {
        bool alreadyAfflicted = AlreadyAfflicted(Caster.Statuses, 7, 1);
        if (alreadyAfflicted == false)
        {
            Caster.Statuses.Add(new Status(7, 1));
        }
        Console.WriteLine($"You begin to feel lucky!");
    }

    public void Forge(Player Caster, Player Target = null)
    {
        if (Caster.WeaponQuality < 4)
        {
            Caster.WeaponQuality += 1;
            Console.WriteLine($"You focus on your blade, increasing it's quality. It is now quality {Caster.WeaponQuality}!");
        }
        else
        {
            Console.WriteLine("You try to imrpove your blade, but it is already the best it can be! It is currently quality 4");
        }
    }

    public void DeForge(Player Caster, Player Target = null)
    {
        if (Target.WeaponQuality > 0)
        {
            Target.WeaponQuality -= 1;
            Console.WriteLine($"You focus on {Target}'s blade, decreasing it's quality. It is now Quality {Target.WeaponQuality}!");
        }
        else
        {
            Console.WriteLine($"You try to ruin {Target}'s blade, but it is already the worst it can be! It is currently quality 0");
        }
    }

    public void Flashstep(Player Caster, Player Target = null)
    {
        Random RandomNumberGenerator = new Random();
        double damage = 0;

        switch (Caster.weaponQuality)
        {
            case 0:
                damage = RandomNumberGenerator.Next(10, 20);
                break;
            case 1:
                damage = RandomNumberGenerator.Next(25, 30);
                break;
            case 2:
                damage = RandomNumberGenerator.Next(30, 45);
                break;
            case 3:
                damage = RandomNumberGenerator.Next(45, 50);
                break;
            case 4:
                damage = RandomNumberGenerator.Next(65, 80);
                break;
            default:
                break;
        }
        damage *= DamageMultiplier(Caster, Target);
        Target.Health -= damage * 1.25;
        Console.WriteLine($"{Target.name} took {damage} damage!");
    }

    public void HolyLight(Player Caster, Player Target = null)
    {
        Random RandomNumberGenerator = new Random();
        double damage = RandomNumberGenerator.Next(50, 70);
        damage *= DamageMultiplier(Caster, Target);
        Target.Health -= damage;
        Console.WriteLine($"A beam of unthinkable power is summoned form the sky... {Target.name} took {damage} damage!");
        int killStatuses = RandomNumberGenerator.Next(1, 4);
        if (killStatuses == 4)
        {
            Target.Statuses.RemoveAll(status => status.isPositive);
            Console.WriteLine($"Coming out of the beam, {Target.name} feels burdened... All their positive status effects were removed!");
        }

    }

    public void CursedCoin(Player Caster, Player Target = null)
    {
        Random RandomNumberGenerator = new Random();
        int coin = RandomNumberGenerator.Next(1, 2);
        switch (coin)
        {
            case 1:
                bool alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 13, 2);
                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(13, 2));
                }
                Console.WriteLine($"You landed heads.{Target.name} was bound!");
                break;
            case 2:
                Caster.Health -= 100;
                Console.WriteLine("You landed tails. A horrible pain wracks your body! You took 100 damage.");
                break;
            default:
                break;

        }
    }

    public void MajorMagicDie(Player Caster, Player Target = null)
    {
        Random die = new Random();
        int rolled = die.Next(1, 6);
        switch (rolled)
        {
            case 1:
                bool alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 1, 2);
                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(1, 2));
                }
                Console.WriteLine($"You rolled a one! {Target.name} was envenomed!");
                break;
            case 2:
                alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 3, 2);
                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(3, 2));
                }
                Console.WriteLine($"You rolled a two! {Target.name} was Scorched!");
                break;
            case 3:
                alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 6, 2);
                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(6, 2));
                }
                Console.WriteLine($"You rolled a three! {Target.name} was Incapacitated!");
                break;
            case 4:
                alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 10, 2);
                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(9, 2));
                }
                Console.WriteLine($"You rolled a four! {Target.name} felt a great sense Misfortune looming over them...");
                break;
            case 5:
                alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 12, 2);
                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(12, 2));
                }
                Console.WriteLine($"You rolled a five! A Laceration opened up on{Target.name}'s body!");
                break;
            case 6:
                alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 13, 3);
                if (alreadyAfflicted == false)
                {
                    Target.Statuses.Add(new Status(6, 3));
                }
                Console.WriteLine($"You rolled a six! {Target.name} was bound in place!");
                break;
            default:
                break;
        }
    }

    public void Hex(Player Caster, Player Target = null)
    {
        bool alreadyAfflicted = AlreadyAfflicted(Target.Statuses, 10, 1);
        if (alreadyAfflicted == false)
        {
            Target.Statuses.Add(new Status(10, 1));
        }
        Console.WriteLine($"{Target.name} begins to feel a great misfortune looming upon them...");
    }

    public void Anointment(Player Caster, Player Target = null)
    {
        bool alreadyAfflicted = AlreadyAfflicted(Caster.Statuses, 8, 1);
        if (alreadyAfflicted == false)
        {
            Caster.Statuses.Add(new Status(8, 1));
        }
        Console.WriteLine($"You begin to feel like the luckiest person alive! Nothing can stand in your way.");
    }

    public void ImbuementFlame(Player Caster, Player Target = null)
    {
        Caster.imbuement = new Status(2, 3);
        Console.WriteLine("You channel a flame into your blade. Your next slash will afflict burning!");
    }

    public void ImbuementIntoxication(Player Caster, Player Target = null)
    {
        Caster.imbuement = new Status(0, 3);
        Console.WriteLine("You channel a potent venom into your blade. Your next slash will afflict posion!");
    }

    public void ImbuementStrength(Player Caster, Player Target = null)
    {
        Caster.imbuement = new Status(5, 3);
        Console.WriteLine("You pour every ounce of strength into your blade. Your next slash will leave the enemy weakened!");
    }

    public void ImbuementWounds(Player Caster, Player Target = null)
    {
        Caster.imbuement = new Status(11, 3);
        Console.WriteLine("You pour every ounce of your focus into the precision of your next strike. Your next strike will afflict bleeding!");
    }

    public void ImbuementMalediction(Player Caster, Player Target = null)
    {
        Caster.imbuement = new Status(9, 2);
        Console.WriteLine("You utter a curse to your blade. It seems to hum, as if in understanding. Your next slash will afflict Bad Luck!");
    }

    public void ImbuementRagingFlame(Player Caster, Player Target = null)
    {
        Caster.imbuement = new Status(3, 2);
        Console.WriteLine("A flame runs wild within your blade. Your next slash will inflict scorching!");
    }

    public void ImbuementBane(Player Caster, Player Target = null)
    {
        Caster.imbuement = new Status(1, 2);
        Console.WriteLine("A potent toxin courses through your veins, and by extension, your blade. Your next slash will inflict Venom!");
    }

    public void ImbuementVigor(Player Caster, Player Target = null)
    {
        Caster.imbuement = new Status(6, 2);
        Console.WriteLine("You pour boundless amounts of strength into the tip of your sword. Nothing stands in your way. Your nextr slash will inflict Incapacitation!");
    }

    public void ImbuementDeepWounds(Player Caster, Player Target = null)
    {
        Caster.imbuement = new Status(12, 2);
        Console.WriteLine("You hone your focus. The tip of your blade feels sharper than ever. Your next slash will inflict laceration!");
    }

    public void ImbuementOmen(Player Caster, Player Target = null)
    {
        Caster.imbuement = new Status(10, 2);
        Console.WriteLine("Your blade feels heavier, almost as if it resists the magic you send coursing through it. It whispers omens, secrets of the future. Your next attack will inflict misfortune!");
    }

    public void RampantFlame(Player Caster, Player Target = null)
    {
        double damage = 0;
        int attackPotency = 0;
        bool isBurnt = AlreadyAfflicted(Target.Statuses, 2, 0);
        bool isScorched = AlreadyAfflicted(Target.Statuses, 3, 0);

        if (isBurnt == false && isScorched == false)
        {
            damage = 30;
            Target.Statuses.Add(new Status(2, 2));
            attackPotency = 1;
        }
        else if (isBurnt && !isScorched)
        {
            damage = 60;
            attackPotency = 2;
            Target.Statuses.Add(new Status(3, 2));
        }
        else if (!isBurnt && isScorched)
        {
            damage = 120;
            attackPotency = 3;
        }
        else if (isBurnt && isScorched)
        {
            damage = 240;
            attackPotency = 4;
        }

        double multiplier = DamageMultiplier(Caster, Target);
        damage *= multiplier;
        Target.Health -= damage;

        switch (attackPotency)
        {
            case 1:
                Console.WriteLine($"The essence of flame runs through your palm and into your target. {Target.name} wasn't burned or scorched, so they take {damage} damage.");
                break;

            case 2:
                Console.WriteLine($"The essence of flame runs through your palm and into your target. {Target.name} was already burnt, so they take {damage} damage and are scorched!");
                break;
            case 3:
                Console.WriteLine($"The essence of flame runs through your palm and into your target. {Target.name} was already scorched! They take {damage} damage!");
                break;
            case 4:
                Console.WriteLine($"The essence of flame runs through your palm and into your target. {Target.name} was already burnd AND scorched! They erupt into a large plume of smoke, dealing {damage} damage!");
                break;
            default:
                break;

        }
    }

    public void Bloodweaving(Player Caster, Player Target = null)
    {
        double damage = 0;
        int attackPotency = 0;

        if (AlreadyAfflicted(Target.Statuses, 11, 0))
        {
            damage = 100;
            attackPotency = 1;
            Target.Statuses.RemoveAll(status => status.statusID == 11);
        }
        else
        {
            damage = 50;
            Target.Statuses.Add(new Status(11, 2));
        }
        damage *= DamageMultiplier(Caster, Target);
        Target.Health -= damage;
        Caster.Statuses.Add(new Status(5, 3));
        switch (attackPotency)
        {
            case 0:
                Console.WriteLine($"You concentrate on the blood of your enemy. They didn't have any open wounds, so you open one for next time. {Target.name} took {damage} damage, and was inflicted with bleeding! However, the strain on your body inflicts weaken on yourself.");
                break;

            case 1:
                Console.WriteLine($"You concentrate on the blood of your enemy. You wring the blood from {Target.name}'s body, dealing {damage} damage! However, their wounds close up, and the strain on your body inflicts weaken on yourself.");
                break;
            default:
                break;
        }
    }

    public void SMITE(Player Caster, Player Target = null)
    {
        Random randomNumber = new Random();
        int statusThing = 0;
        bool wasScorched = false;
        bool wasIncapacitated = false;
        double damage = 150 * DamageMultiplier(Caster, Target);
        statusThing = randomNumber.Next(1, 2);

        switch (statusThing)
        {
            case 1:
                Target.Statuses.Add(new Status(3, 2));
                wasScorched = true;
                break;
            case 2:
                wasScorched = false;
                break;
        }
        statusThing = randomNumber.Next(1, 2);

        switch (statusThing)
        {
            case 1:
                Target.Statuses.Add(new Status(6, 20));
                wasIncapacitated = true;
                break;
            case 2:
                wasIncapacitated = false;
                break;
        }
        Console.WriteLine($"You summon a bright light from within your soul.. You release it in a powerful flash, dealing {damage} damage to {Target.name}!");
        if (wasScorched)
        {
            Console.WriteLine($"As the light clears, {Target.name} stumbles out of it with severe burns all over their skin. {Target.name} was scorched!");
        }
        if (wasIncapacitated)
        {
            Console.WriteLine($"As the light clears, {Target.name} struggles to walk as they stumble out of it. {Target.name} was incapacitated!");
        }
    }

    public void DoubleShot(Player Caster, Player Target1, Player Target2)
    {
        int peopleShot = 0;
        double damage = 0;
        Random random = new Random();
        if (Target1 == Target2)
        {
            damage = random.Next(25, 50);
            peopleShot = 1;
        }
        else
        {
            damage = random.Next(50, 75);
            peopleShot = 2;
        }
        switch (peopleShot)
        {
            case 1:
                damage *= DamageMultiplier(Caster, Target1);
                Console.WriteLine($"You conjure two magical bullets and fire them at {Target1.name}. You deal {damage} damage twice!");
                Target1.Health -= damage * 2;
                break;
            case 2:
                double damage1 = damage;
                double damage2 = damage;
                damage1 *= DamageMultiplier(Caster, Target1);
                damage2 *= DamageMultiplier(Caster, Target2);
                Console.WriteLine($"You conjure two magical bullets. You fire them at {Target1.name} and {Target2.name!} You deal {damage1} and {damage2} respectively");
                Target1.Health -= damage1;
                Target2.Health -= damage2;
                break;
        }
    }
}