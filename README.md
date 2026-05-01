# ARPoster
An interactive Augmented Reality (AR) experience built in Unity using AR Foundation. This project allows users to scan a poster and view 3D fruit models that appear in real space, animate, and display information when tapped.
| Username | Name |
|--------- | ------- |
| https-haley | Haley |
| AfroHat | Andre |
| timothypham045 | Tim |
| kaijafrierson | Kaija |

# Requirements #
- Mobile device with AR support
  - Android (ARCore)
- Unity 2022.3 LTS

# How to Use #
1. Scan the poster with your device
2. Watch fruits appear in 3D
3. Tap a fruit to display its name and price
4. Observe animations (rotation, floating, pop-up effects)

# Key Scripts #
- FruitSpawner
  - Scans the flyer and spawns the fruit in the correct position
- Rotate Fruit
  - Applied the rotation animation to the fruit
- FruitInfo
  - Detects the users touch and triggers the UI updates
      - Update() detects touch and calls SelectFruit()
      - SelectFruit() changes the size of the fruit
- FruitInfoDisplay
  - Controls the UI popup display
- PopUp
  - Fruit "popups" from the flyer animation

# Demo #
