# ThatDefaultUnityProject

A bunch of scripts and systems that are commonly used across projects.
Open to suggestions and bug reporting through issues and contributions in the form of pull requests.

## Content

Some basic (WIP) documentation can be found on the [wiki](https://github.com/AtlantiaKing/ThatDefaultUnityProject/wiki)

- AI decision making
    - BehaviorTree
        - Scaleable, reusable decision making
        - Flexible and easy to reconfigure
    - Finite State Machine (FSM)
- Audio & Particle patch systems (Easy SFX and RFX implementation system)
    - Automatic pitch variation (configurable)
    - Automatic volume variation (configurable)
    - `ScriptableObject` based
    - Event based
    - Audio cooldown and play limit
    - Trailing audio sources, perfect when objects get destroyed
- MonoSingleton (Monobehavior Singleton baseclass)
- Random Utils (Weighted random)
- Common transform manipulation
    - Transform mimic (Copy position and/or rotation and/or scale)
    - LookAtCamera
