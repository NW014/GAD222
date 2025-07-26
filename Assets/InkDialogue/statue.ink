=== statue ===

{firstMeetStatue:
    -> statue0
    -> END
}
-> statue1
-> END

= statue0
Oh? Hello there! Are you an adventurer?
I haven't seen anyone in a long while.
People used to come here all the time, leaving coins as an offering.
However, I have no use for these coins. Maybe they'll be more useful on your hands.
Would you like to take some coins for yourself?
    + [Yes]
        ~ AddCoin(5)
        ~ coinCount = coinCount + 5
        Alright, here you go! Now you have {coinCount} coins!
        Come back if you want more coins!
        ~ firstMeetStatue = false
    -> END
    + [No]
        That's okay, come back anytime if you want some!
        ~ firstMeetStatue = false
-> END

= statue1
Hello there, brave adventurer!
Do you want some coins?
+ [Yes]
    ~ AddCoin(5)
    ~ coinCount = coinCount + 5
    Alright, here you go! Now you have {coinCount} coins!
    Come back if you want more coins!
    
+ [No]
    That's okay, come back anytime if you want some!
- -> END
