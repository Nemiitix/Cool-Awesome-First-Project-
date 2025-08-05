using System.Collections.Generic;
using System;

public class Status
{
    /*Before the player's action, loop through each status in their list, then:
        Check the damage type, then deal the damage.
        If it doesn't deal damage, check if it's binding.
        If it's binding, forfeit their turn lmaoo.
      During an attack, loop through their statuses again, and check for weakening/strengthening effects.
      Apply those multipliers to the damage before dealing it.
    */

    /*Each status should have an ID. when applying the status, loop through the statuses and if it shares an ID
    with any of the statuses on the list, add to the status' duration as opposed to making a completely new one.
    */

    /*List of statuses
        0 - Poison - Deals 5% of the enemy's current health every turn.
        1 - Venom - 10% of the enemy's current health per turn
        2 - Burn - Deals 5% of the enemy's max health per turn.
        3 - SCORCH - Deals 10% of the enemy's max health per turn. (Note that schorching should only last for 1 or two turns each time it is applied.)
        4 - Defense - Reduces damage taken by 50%.
        5 - Weak - Affected deals 80% damage.
        6 - Incapacitated - Affected deals 60% damage.
        7 - Luck - Improves Card Draws
        8 - Fortune - Drastically improves card draws
        9 - Bad Luck - Worse Card Draws
        10 - Misfortune REALLY bad card draws.
        11 - Bleeding - 10% More damage taken.
        12 - Laceration - 20% More damage taken.
        13 - Binding - This turn is skipped.
        14 - Motivated - Deal 1% extra damage next turn!
        15 - Slighty Intimidated - Deal 1% less damage next turn...
        16 - Mana Needle - Take 3% of your health in damage over three turns.
        */
    public string name;
    // The name of the status.
    public string description;
    public int damageType;
    /* The kind of damage (flat, percentile, or none) that the status does every turn.
    1 - None
    2 - Flat
    3 - Percentile
    */
    public int duration;
    //How many turns left before the status is removed.
    public double damage;
    // How much damage is dealt every turn.
    public bool isBinding;
    // Wether or not the affected's turn is skipped.
    public int statusID;
    //The status' ID. Used to determine wether or not the player already has said status.
    public int luckChange;
    //How luck is changed, if at all.
    public bool changesDamageDealt;
    //Wether or not the status changes the damage you deal
    public double damageDealtChange;
    //The change in damage dealt, if any.
    public bool changesDamageTaken;
    //Wether or not it changes the damage you take.
    public double damageTakenChange;
    //The change in damage taken, if any.

    public bool isPositive;

    public bool isActive;
    
    public int Duration { get; set; }

    public Status(int id, int Duration)
    {
        statusID = id;
        duration = Duration;
        isActive = true;
        switch (id)
        {
            case 0:
                isPositive = false;
                name = "Poison";
                description = "Take 5% of your current health in damage every turn";
                damageType = 3;
                damage = .05;
                isBinding = false;
                luckChange = 0;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 1:
                isPositive = false;
                name = "Venom";
                description = "Take 10% of your current health in damage every turn";
                damageType = 3;
                damage = .10;
                isBinding = false;
                luckChange = 0;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 2:
                isPositive = false;
                name = "Burning";
                description = "Take 5% of your max health in damage every turn";
                damageType = 2;
                damage = .05;
                isBinding = false;
                luckChange = 0;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 3:
                isPositive = false;
                name = "Scorching";
                description = "Take 10% of your max health in damage every turn";
                damageType = 2;
                damage = .10;
                isBinding = false;
                luckChange = 0;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 4:
                name = "Defense";
                description = "Halve all damage from attacks (Note that this doesn't reduce damage from statuses)";
                damageType = 1;
                damage = 0;
                isBinding = false;
                luckChange = 0;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = true;
                damageTakenChange = .50;
                break;
            case 5:
                isPositive = false;
                name = "Weakened";
                description = "Weaken: Damage you deal is reduced by 20%";
                damageType = 1;
                damage = 0;
                isBinding = false;
                luckChange = 0;
                changesDamageDealt = true;
                damageDealtChange = .80;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 6:
                isPositive = false;
                name = "Incapacitated";
                description = "Stronger Weaken. Damage you deal is reduced by 40%";
                damageType = 1;
                damage = 0;
                isBinding = false;
                changesDamageDealt = true;
                damageDealtChange = .60;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 7:
                isPositive = true;
                name = "Luck";
                description = " Higher quality card draws. Only decays when drawing cards, as opposed to turns";
                damageType = 1;
                damage = 0;
                isBinding = false;
                luckChange = 1;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 8:
                isPositive = true;
                name = "Fortune";
                description = "Stronger luck. Dramatically improved card draws.";
                damageType = 1;
                damage = 0;
                isBinding = false;
                luckChange = 2;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 9:
                isPositive = false;
                name = "Bad Luck";
                description = "Worse Card Draws";
                damageType = 1;
                damage = 0;
                isBinding = false;
                luckChange = -1;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 10:
                isPositive = false;
                name = "Misfortune";
                description = "Stronger Bad Luck. Dramatically worse card draws.";
                damageType = 1;
                damage = 0;
                isBinding = false;
                luckChange = -2;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 11:
                isPositive = false;
                name = "Bleeding";
                description = "Take 10% more damage from attacks";
                damageType = 1;
                damage = 0;
                isBinding = false;
                luckChange = 0;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = true;
                damageTakenChange = 1.10;
                break;
            case 12:
                isPositive = false;
                name = "Laceration";
                description = "Stronger Bleeding. Take 20% more damage from attacks.";
                damageType = 1;
                damage = 0;
                isBinding = false;
                luckChange = 0;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = true;
                damageTakenChange = 1.20;
                break;
            case 13:
                isPositive = false;
                name = "Binding";
                description = "Your turn is skipped.";
                damageType = 1;
                damage = 0;
                isBinding = true;
                luckChange = 0;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 14:
                isPositive = true;
                name = "Motivated";
                description = "Deal 1% extra damage";
                damageType = 1;
                damage = 0;
                isBinding = false;
                luckChange = 0;
                changesDamageDealt = true;
                damageDealtChange = 1.01;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 15:
                isPositive = false;
                name = "Intimidated";
                description = "Deal 1% less damage";
                damageType = 1;
                damage = 0;
                isBinding = false;
                luckChange = 0;
                changesDamageDealt = true;
                damageDealtChange = 0.99;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 16:
                isPositive = false;
                name = "Mana Needle";
                description = "Take 3% of your max health in damage over the next three turns.";
                damageType = 2;
                damage = 0.01;
                isBinding = false;
                luckChange = 0;
                changesDamageDealt = false;
                damageDealtChange = 0;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;
            case 17:
                isPositive = true;
                name = "Death's Door";
                description = "Deal double damage! Don't want ot go out without a fight, do we?";
                damageType = 1;
                damage = 0;
                isBinding = false;
                luckChange = 0;
                changesDamageDealt = true;
                damageDealtChange = 2;
                changesDamageTaken = false;
                damageTakenChange = 0;
                break;






        }
    }


    
    



}
