=== DoorStart ===
{bagCollected:
    {itemsCollected:
        ~ EnterBuilding(1)
        -> END
    
        - else:
        {itemCount < 4:
            Feels like I'm still forgetting something...
            Better check the house again to be sure.
            -> END
            
            - else:
            I think that's everything. Time to start my journey!
            ~itemsCollected = true
    
            -> END
        }
    }
}
I should find my bag first.

-> END