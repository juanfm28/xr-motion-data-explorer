# Architecture — XR Motion / Data Explorer

## Overview

The application separates data loading, playback and visualization
so that the core systems can later be reused by the XR interface.

## High-Level Architecture

CSV
 ↓
Data Parser
 ↓
Data Model
 ↓
Playback System
 ↓
Visualization
 ↓
UI
 ↓
XR Interaction

## Responsibilities

### Data Parser

Reads CSV data and converts it into C# data structures.

### Data Model

Represents tracked objects and their temporal samples.

### Playback System

Maintains the current playback time and resolves dataset state.

### Visualization

Represents tracked objects and trajectories in the Unity scene.

### UI

Controls playback and displays information.

### XR Interaction

Adds XR interaction on top of the existing desktop systems.

## Principles

- Keep data parsing separate from visualization.
- Keep the data model independent from Unity GameObjects where practical.
- Keep playback logic separate from UI.
- XR builds on the desktop systems rather than duplicating them.
- Prefer simple solutions over premature abstractions.