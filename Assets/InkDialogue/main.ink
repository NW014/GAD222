EXTERNAL AddCoin(coinCount)
EXTERNAL AddApple(appleCount)
EXTERNAL StartCombat()
EXTERNAL EnterBuilding(value)

VAR playerName = ""
VAR interactedWithGoblin = false
VAR coinCount = 0
VAR bagCollected = false
VAR itemsCollected = false
VAR itemCount = 0
VAR firstMeetStatue = true

INCLUDE npc1.ink
INCLUDE statue.ink
INCLUDE Intro.ink
INCLUDE Diary.ink
INCLUDE Carrot.ink
INCLUDE DoorStart.ink
INCLUDE Bag.ink
INCLUDE Pot.ink
INCLUDE Sword.ink
INCLUDE Map.ink
INCLUDE Sign.ink