=== goblin ===

{interactedWithGoblin:
    {coinCount >= 5:
        -> goblinline2
      - else:
        You again? What do you want?
    + [Say hello]
        Don't talk to me, I have more important things to do.
        -> END
    }
    - else:
    Who are you, stranger?
        + [Introduce yourself]
            Whatever, just don't steal my coins.
            ~ interactedWithGoblin = true
            -> END
}
+ [Stay silent]
    -> goblinCombatStart

= goblinline1
...
* [Initiate combat]
    (Unfortunately you're not ready for battle right now.)
    (Perhaps you will have the courage for it in the next update.)
    -> goblinline1
+ [Leave quietly]
    -> END
    
= goblinline2
You again? What do you-
Wait, are those coins I smell on you?
+ [Yes]
    I'll trade you an apple if you hand over 5 coins.
    ++ [Sure]
        ~ coinCount = coinCount - 5
        ~ AddApple(1)
        ~ AddCoin(-5)
        Yes! More coins! Here, take your apple.
        I'll be here if you have more coins for apples.
    ++ [No thanks]
        Fine, I'll be here if you change your mind.
+ [No]
    (The goblin glares at you.)
    Fine, keep your coins. Not like you need these apples I'm willing to trade for them anyway.
- -> END

= goblinCombatStart
Are you looking for a fight? Bring it on then!
+ [Start battle]
    ~ StartCombat()
+ [Leave quietly]
    Hmph. Don't come bother me again.
- -> END