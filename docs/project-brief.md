# Project Brief — XR Motion / Data Explorer

## Overview
Interactive Unity application for loading, reproducing and
visualizing time-series spatial motion data.

## Goals
- Load motion data from CSV.
- Reproduce recorded movement in a 3D scene.
- Visualize trajectories.
- Inspect individual tracked objects.
- Extend the desktop visualization to XR.

## Dataset

The application will use time-series spatial data stored in CSV format.

Each sample is expected to represent the state of a tracked object
at a specific point in time.

Initial fields will likely include:

- Object ID
- Timestamp
- Position X
- Position Y
- Position Z

Additional fields may be added once a concrete dataset is selected.

The parser and data model should not depend unnecessarily on
visualization-specific Unity components.

## Desktop MVP

The first complete version will run as a standard desktop Unity
application before any XR functionality is added.

### Data Loading

- Load a spatial time-series dataset from CSV.
- Parse rows into C# data structures.
- Validate that samples and timestamps are read correctly.

### Playback

- Reproduce recorded movement over time.
- Play and pause playback.
- Restart playback.
- Scrub through the timeline.

### Visualization

- Represent tracked objects in a 3D Unity scene.
- Update their positions according to the current playback time.
- Display their trajectories.

### UI

- Provide Play, Pause and Restart controls.
- Provide a timeline / scrubber.
- Allow selection of a tracked object.
- Display basic information about the selected object.

## XR Phase
Explore the same dataset interactively using Meta Quest / OpenXR.

## Optional Extensions
To define after

## Non-Goals
- No backend.
- No cloud infrastructure.
- No multiplayer.

## Technology
- Unity 6.3
- C#
- OpenXR
- XR Interaction Toolkit

## Definition of Done
- CSV loads correctly.
- Spatial data plays back in Unity.
- Play / Pause / Restart / Scrub work.
- Trajectories are visible.
- Objects can be selected and basic information is displayed.
- Functional XR version exists.
- Repository contains example dataset, screenshots and video.