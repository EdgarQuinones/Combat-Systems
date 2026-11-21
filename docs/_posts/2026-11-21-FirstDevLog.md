---
layout: post
title: "Devlog #1 - First DevLog"
date: 2026-11-21
excerpt: "My first DevLog and everything I've worked on so far"
---

## My First DevLog

This is my first official devlog discussing my thoughts, process, and things I've learned while working on combat systems. I wanted to start this so I could track my progress and reflect more on my programming practices.

## Everything So Far

This is my first time diving into the intricacies of combat systems. I am a big fan of **hack-and-slash** and **beat 'em up** games, so this will be an exciting learning experience.

So far, I have a player who can move and attack enemies, and an enemy that can be pushed, hurt, and defeated by the player.

The player has one attack ability that contains several modifiers, including damage, knockback strength, and timers. One of the aspects of combat I wanted to learn was state changes, so the attack has an initial setup state, the actual hit state with *hitstop* included, and a recovery state. During all these states, the player is slowed. My goal is not only to make an attack the player sees, but one they can really feel.

The enemy currently is just a test dummy with a **Rigidbody2D** and a `Health.cs` script that the player can interact with. Its knockback is based on its mass, and it will deactivate when its **health** reaches 0. In the future, I want the enemy to have its own AI and combat system, with its own attack states and complexities.

## What's Next?

Next steps involve tuning the player so that hitstop does not interfere with the attack state timings. Currently, when a player hits an enemy, both the player's and enemy's velocities are saved and set to zero for 0.08 seconds. The problem is that the full attack timer is currently about 0.5 seconds, so technically, the player is waiting longer than 0.5 seconds (plus the 1-second cooldown) before their next attack.

Thank you, and I look forward to sharing more updates!