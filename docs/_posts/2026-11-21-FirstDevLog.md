---
layout: post
title: "Devlog #1 - First DevLog"
date: 2026-11-21
---

## My First DevLog

This is my first official devlog discussing my thoughts, process, and things I learned while working on combat systems. I wanted to start this so I can track my progress and reflect more on my programming practices. 

## Everything so far

This is my first time diving into the intriclcies of combat systems. I am a big fan of **Hack and slash** and **Beat 'Em Up** games, so this will be an exciting learning experience. 

So far, I have a player who can move, attack enemies, and an enemy that can be pushed, hurt, and defeated by the player. 

The player has one attack ability that contains some modifiers, some of those traits include damage, knockback strength, and some timers. One of the aspects of combat I wanted to learn was state changes, so the attack has a initial setup state, the actual hit state with a *hitstop* included, and a recover state. During all these states the player is slowed. My goal is not only to make a attack the player sees, but can really feel. 

The enemy currently is just a test dummy with a **RigidBody2D** and `Health.cs` script that the player can interact with. Their knockback is based on their mass and they will deactivate when their **health** reaches 0. At a future point I want the enemy to have an AI and combat of their own, which their own attack states and complexities.

## What's Next?

Nexts steps involves tuning the player so the hitstop does not interfere with the attack states timing. Currently when a player hits an enemy, both the player and enemies' velocity is saved and set to zero for 0.08 seconds. The problem with this is the full attack timer at this time is about 0.5s, so technically the player is waiting longer than 0.5s (Plus the 1s CD) for their next attack.

Thank you and I look forward to sharing more updates!