using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

public class Spell
{
    public string name;
    public delegate void SpellMethod(Player Caster, Player Target = null);

    public SpellMethod method;

    public string description;

    public int id;

    public int spellType;

    public bool hasTarget;
    /*
    "God, I wish there was an easier way to do this"
    - Yandere Dev
    (lmao)
    */
    public Spell(int id)
    {
        Methods doesThisWork = new Methods();
        switch (id)
        {
            case 1:
                method = doesThisWork.SPITE;
                this.name = "SPITE";
                id = 1;
                this.description = "Literally die, but halve the health of your target";
                this.hasTarget = true;
                Console.WriteLine("You drew SPITE! (Quality 0)");
                break;
            case 2:
                method = doesThisWork.PositiveAffirmation;
                this.name = "Positve Affirmation";
                this.id = 2;
                this.description = "Deal 1% extra damage next turn";
                this.hasTarget = false;
                Console.WriteLine("You drew Positive Affirmation! (Quality 0)");
                break;
            case 3:
                method = doesThisWork.ManaNeedle;
                this.name = "Mana Needle";
                this.id = 3;
                this.description = "Deal 3% of a target's max health over three turns";
                this.hasTarget = true;
                Console.WriteLine("You drew Mana Needle! (Quality 0)");
                break;
            case 4:
                method = doesThisWork.SlightlyIntimidatingStare;
                this.name = "Slightly Intimidating Stare";
                this.id = 4;
                this.description = "Your target will deal 1% less damage next turn";
                this.hasTarget = true;
                Console.WriteLine("You drew Slightly Intimidating Stare! (Quality 0)");
                break;
            case 5:
                method = doesThisWork.SlashButWorse;
                this.name = "slash but worse";
                this.id = 5;
                this.description = "A slash that deals 25% damage. It will not use up your imbuement";
                this.hasTarget = true;
                Console.WriteLine("You drew slash but worse! (Quality 0)");
                break;
            case 6:
                method = doesThisWork.Glare;
                this.name = "Glare";
                this.id = 6;
                this.description = "Apply weaken to a target for one turn";
                this.hasTarget = true;
                Console.WriteLine("You drew Glare! (Quality 1)");
                break;
            case 7:
                method = doesThisWork.MinorMagicDie;
                this.name = "Minor Magic Die";
                this.id = 7;
                this.description = "Roll a die. Depending on the number you roll, apply a status effect.";
                this.hasTarget = true;
                Console.WriteLine("You drew Minor Magic Die! (Quality 1)");
                break;
            case 8:
                method = doesThisWork.BeginnersFlame;
                this.name = "Beginner's Flame";
                this.id = 8;
                this.description = "Deal 20 damage to an enemy., burning them. However, you inflict burn on yourself as well.";
                this.hasTarget = true;
                Console.WriteLine("You drew Beginner's Flame! (Quality 1)");
                break;
            case 9:
                method = doesThisWork.IceNeedles;
                this.name = "Ice Needles";
                this.id = 9;
                this.description = "Deal 15 damage, but apply weak to both yourself and yoour target.";
                this.hasTarget = true;
                Console.WriteLine("You drew Ice Needles! (Quality 1");
                break;
            case 10:
                method = doesThisWork.Shank;
                this.name = "Shank";
                this.id = 10;
                this.description = "Deal 20-50 damage, apply bleeding";
                this.hasTarget = true;
                Console.WriteLine("You drew Shank! (Quality 2)");
                break;
            case 11:
                method = doesThisWork.OverGrowth;
                this.name = "Overgrowth";
                this.id = 11;
                this.description = "Deal 20-50 damage. 50% chance to inflict binding.";
                this.hasTarget = true;
                Console.WriteLine("You drew Overgrowth! (Quality 2)");
                break;
            case 12:
                method = doesThisWork.FirePalm;
                this.name = "Fire Palm";
                this.id = 12;
                this.description = "Apply scorching to both you and a target";
                this.hasTarget = true;
                Console.WriteLine("You drew fire palm! (Quality 2)");
                break;
            case 13:
                method = doesThisWork.SnakeFangs;
                this.name = "Snake Fangs";
                this.id = 13;
                this.description = "Deal 10-20 damage. Poison your target";
                this.hasTarget = true;
                Console.WriteLine("You drew Snake Fangs! (Quality 2)");
                break;
            case 14:
                method = doesThisWork.SuckerPunch;
                this.name = "Sucker Punch";
                this.id = 14;
                this.description = "Deal 30-60 damage. Inflict Weaken.";
                this.hasTarget = true;
                Console.WriteLine("You Drew Sucker Punch (Quality 2)");
                break;
            case 15:
                method = doesThisWork.Curse;
                this.name = "Curse";
                this.id = 15;
                this.description = "Inflict bad Luck on your enemy";
                this.hasTarget = true;
                Console.WriteLine("You drew Curse! (Quality 2)");
                break;
            case 16:
                method = doesThisWork.Bless;
                this.name = "Bless";
                this.id = 16;
                this.description = "Inflict luck on yourself";
                this.hasTarget = false;
                Console.WriteLine("You drew Bless! (Quality 2)");
                break;
            case 17:
                method = doesThisWork.Forge;
                this.name = "Forge";
                this.id = 17;
                this.description = "Improve the quality of your blade by one";
                this.hasTarget = false;
                Console.WriteLine("You drew Forge! (Quality 2)");
                break;
            case 18:
                method = doesThisWork.DeForge;
                this.name = "Deforge";
                this.id = 18;
                this.description = "Decrease the quality of your target's weapon by one.";
                this.hasTarget = true;
                Console.WriteLine("You drew Deforge! (Quality 2)");
                break;
            case 19:
                method = doesThisWork.Flashstep;
                this.name = "Flashstep";
                this.id = 19;
                this.description = "A better version of slash, contained in a spell! This will not use up your imbuement";
                this.hasTarget = true;
                Console.WriteLine("You drew Flashstep! (Quality 3)");
                break;
            case 20:
                method = doesThisWork.HolyLight;
                this.name = "Holy Light";
                this.id = 20;
                this.description = "Deal 50-70 damage. 50% chance to remove all of your enemy's positive status effects";
                this.hasTarget = true;
                Console.WriteLine("You drew Holy Light! (Quality 3)");
                break;
            case 21:
                method = doesThisWork.CursedCoin;
                this.name = "Cursed Coin";
                this.id = 21;
                this.description = "50% Chance to bind your target for two turns. 50% chance to deal 100 damage to you.";
                this.hasTarget = true;
                Console.WriteLine("You drew Cursed Coin! (Quality 3)");
                break;
            case 22:
                method = doesThisWork.MajorMagicDie;
                this.name = "Major Magic Die";
                this.id = 22;
                this.description = "Roll a die. Depending on the number, inflict a powerful status effect";
                this.hasTarget = true;  
                Console.WriteLine("You drew Major Magic Die! (Quality 3)");
                break;
            case 23:
                method = doesThisWork.Hex;
                this.name = "Hex";
                this.id = 23;
                this.description = "Inflict Misfortune on an enemy";
                this.hasTarget = true;
                Console.WriteLine("You drew Hex! (Quality 3)");
                break;
            case 24:
                method = doesThisWork.Anointment;
                this.name = "Anointment";
                this.id = 24;
                this.description = "Inflict Fortune on yourself";
                this.hasTarget = false;
                Console.WriteLine("You drew Anointment! (Quality 3)");
                break;
            case 25:
                method = doesThisWork.ImbuementFlame;
                this.name = "Imbuement: Flame";
                this.id = 25;
                this.description = "Imbue your weapon with Flame, making your next slash burn your enemies";
                this.hasTarget = false;
                Console.WriteLine("You drew Imbuement: Flame! (Quality 3)");
                break;
            case 26:
                method = doesThisWork.ImbuementIntoxication;
                this.name = "Imbuement: Intoxication";
                this.id = 26;
                this.description = "Imbue your weapon with Poison, making your next slash poison your enemies";
                this.hasTarget = false;
                Console.WriteLine("You drew Imbuement: Intoxication! (Quality 3)");
                break;
            case 27:
                method = doesThisWork.ImbuementStrength;
                this.name = "Imbuement: Strength";
                this.id = 27;
                this.description = "Imbue your weapon with strength, making your next slash weaken enemies.";
                this.hasTarget = false;
                Console.WriteLine("You drew Imbuement: Strength! (Quality 3)");
                break;
            case 28:
                method = doesThisWork.ImbuementWounds;
                this.name = "Imbuement: Wounds";
                this.id = 28;
                this.description = "Sharpen your weapon, causing your next slash to cause enemies to bleed";
                this.hasTarget = false;
                Console.WriteLine("You drew Imbuement: Wounds (Quality 3)");
                break;
            case 29:
                method = doesThisWork.ImbuementMalediction;
                this.name = "Imbuement: Malediction";
                this.id = 29;
                this.description = "Imbue your weapon with a powerful curse, causing your next slash to give enemies bad luck";
                this.hasTarget = false;
                Console.WriteLine("You drew Imbuement: Malediction! (Quality 3)");
                break;
            case 30:
                method = doesThisWork.ImbuementRagingFlame;
                this.name = "Imbuement: Raging Flame";
                this.id = 30;
                this.description = "Immbue your weapon with a powerful inferno, causing your next slash to scorch enemies";
                this.hasTarget = false;
                Console.WriteLine("You drew Imbuement: Raging Flame! (Quality 4)");
                break;
            case 31:
                method = doesThisWork.ImbuementBane;
                this.name = "Imbuement: Bane";
                this.id = 31;
                this.description = "Imbue your weapon with a potent venom, causing your next slash to inflict Venom";
                this.hasTarget = false;
                Console.WriteLine("You drew Imbuement: Bane! (Quality 4)");
                break;
            case 32:
                method = doesThisWork.ImbuementVigor;
                this.name = "Imbuement: Vigor";
                this.id = 32;
                this.description = "Imbue your weapon with Vigor, causing your next slash to inflict Incapacitation";
                this.hasTarget = false;
                Console.WriteLine("You drew Imbuement: Vigor! (Quality 4)");
                break;
            case 33:
                method = doesThisWork.ImbuementDeepWounds;
                this.name = "Imbuement: Deep Wounds";
                this.id = 33;
                this.description = "Sharpen your blade to a fine edge, causing your next slash to Lacerate your enemies";
                this.hasTarget = false;
                Console.WriteLine("You Drew Imbuement: Deep Wounds! (Quality 4)");
                break;
            case 34:
                method = doesThisWork.ImbuementOmen;
                this.name = "Imbuement: Omen";
                this.id = 34;
                this.description = "Imbue Your weapon with an omen from the past, causing your next slash to inflict Misfortune";
                this.hasTarget = false;
                Console.WriteLine("You drew Imbuement: Omen! (Quality 4)");
                break;
            case 35:
                method = doesThisWork.RampantFlame;
                this.name = "Rampant Flame";
                this.id = 35;
                this.description = "Spread a flame across your target. This deals extra damage if the target is burnt, scorched, or both";
                this.hasTarget = true;
                Console.WriteLine("You drew Rampant Flame! (Quality 4)");
                break;
            case 36:
                method = doesThisWork.Bloodweaving;
                this.name = "Bloodweaving";
                this.id = 36;
                this.description = "Manipulate the enemy's blood, dealing damage to them, and inflicting bleeding if they aren't already. This deals extra damage if the enemy is bleeding, but it removes the status. It will always weaken the caster";
                this.hasTarget = true;
                Console.WriteLine("You drew Bloodweaving! (Quality 4)");
                break;
            case 37:
                method = doesThisWork.SMITE;
                this.name = "SMITE";
                this.id = 37;
                this.description = "Summon a beam of holy light which strikes targets. Has a chance to scorch or incapacitate enemies";
                this.hasTarget = true;
                Console.WriteLine("You drew SMITE! (Quality 4)");
                break;
        }
    }
}

