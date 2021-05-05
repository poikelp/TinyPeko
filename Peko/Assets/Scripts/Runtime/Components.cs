using Unity.Entities;
using Unity.Mathematics;

namespace Peko
{
    public enum GameStateEnum
    {
        Start,
        InGame,
        EndGame
    }
    public enum AnimationType
    {
        Wait,
        Run,
        Up,
        Down,
        End
    }

    public struct CarrotSpawner : IComponentData
    {
        public Entity Prefab;
        public float TimeLimit;
        public float ElapsedTime;
        public Entity Gold;
        public Entity Nomal;
    }
    public struct GoldTag : IComponentData {}

    public struct AnimSpritesBuffer : IBufferElementData
    {
        public Entity Sprite;
    }
    public struct PlayerRunAnimTag : IComponentData {}
    public struct PlayerWaitAnimTag : IComponentData {}
    public struct PlayerEndAnimTag : IComponentData {}
    
    public struct SpriteAnimationData : IComponentData
    {
        public float ElapsedTime;
        public AnimationType AnimType;
    }

    public struct Player : IComponentData {}

    public struct JumpData : IComponentData 
    {
        public float Velocity;
    }

    public struct BGTag : IComponentData {}
    public struct GroundTag : IComponentData {}
    public struct CarrotTag : IComponentData {}

    
    public struct GameState : IComponentData 
    {
        public GameStateEnum Value;
    }

    public struct Counter : IComponentData
    {
        public int Value;
    }

    public struct NumberSpriteBuffer : IBufferElementData
    {
        public Entity Sprite;
    }
    public struct NumberTag : IComponentData {}
    public struct NumberPlace : IBufferElementData
    {
        public Entity Value;
    }

    public struct Timer : IComponentData
    {
        public float Value;
    }

    public struct StartMessage : IComponentData
    {
        public Entity Value;
    }
    public struct ContinueMessage : IComponentData
    {
        public Entity Value;
    }
    public struct DefaultMessage : IComponentData
    {
        public Entity Value;
    }
    public struct MessageTag : IComponentData {}
    
    public struct RunSpeed : IComponentData
    {
        public float Value;
    }

    public struct TwiButtonTag : IComponentData {}
    public struct TwiIcon : IComponentData {
        public Entity Value;
    }
}