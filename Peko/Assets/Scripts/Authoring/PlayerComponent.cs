using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Peko;

namespace Peko.Authoring
{
    public class PlayerComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        // public Sprite[] runSprites;
        // public Sprite[] waitSprites;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<Player>(entity);
            dstManager.AddComponent<JumpData>(entity);
            dstManager.AddComponentData<RunSpeed>(entity, new RunSpeed
            {
                Value = 2.0f
            });
            
            // var runSpriteBuffer = dstManager.AddBuffer<RunSprite>(entity);
            // if(runSprites != null)
            // {
            //     foreach(var s in runSprites)
            //     {
            //         var spriteEntity = conversionSystem.GetPrimaryEntity(s);
            //         runSpriteBuffer.Add(new RunSprite
            //         {
            //             Sprite = spriteEntity
            //         });    
            //     }    
            // }
            // var waitSpriteBuffer = dstManager.AddBuffer<WaitSprite>(entity);
            // if(waitSprites != null)
            // {
            //     foreach(var s in waitSprites)
            //     {
            //         var spriteEntity = conversionSystem.GetPrimaryEntity(s);
            //         waitSpriteBuffer.Add(new WaitSprite
            //         {
            //             Sprite = spriteEntity
            //         });
            //     }
            // }

            dstManager.AddComponentData<SpriteAnimationData>(entity, new SpriteAnimationData
            {
                ElapsedTime = 0,
                AnimType = AnimationType.Wait
            });
            
        }

    }
    // [UpdateInGroup(typeof(GameObjectDeclareReferencedObjectsGroup))]
    // class DeclarePekoSpriteReference : GameObjectConversionSystem
    // {
    //     protected override void OnUpdate()
    //     {
    //         Entities.ForEach((PlayerComponent mgr) =>
    //         {
    //             if(mgr.runSprites != null)
    //             {
    //                 foreach(var s in mgr.runSprites)
    //                 {
    //                     DeclareReferencedAsset(s);
    //                 }
    //             }
    //             if(mgr.waitSprites != null)
    //             {
    //                 foreach(var s in mgr.waitSprites)
    //                 {
    //                     DeclareReferencedAsset(s);
    //                 }
    //             }
                
    //         });
    //     }
    // }
}
