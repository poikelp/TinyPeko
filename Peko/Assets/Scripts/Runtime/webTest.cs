using Unity.Entities;
using Unity.Tiny;
using Unity.Tiny.Input;
using urlopener;
using Unity.Mathematics;

namespace Peko
{
    [UpdateBefore(typeof(GameResetSystem))]
    public class webTest : SystemBase
    {
        private bool touch;
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<GameState>();
        }
        protected override void OnUpdate()
        {
            var state = GetSingleton<GameState>();
            if(state.Value == GameStateEnum.EndGame)
            {
                bool push = false;
                float2 position = new float2(0,0);

                var Input = World.GetExistingSystem<InputSystem>();
                if(Input.IsTouchSupported() && touch && /*Input.TouchCount() > 0*/ Input.GetTouch(0).phase == TouchState.Ended)
                {
                    touch = false;
                    push = true;
                    position.x = Input.GetTouch(0).x;
                    position.y = Input.GetTouch(0).y;
                }
                if(Input.GetMouseButtonUp(0))
                {
                    push = true;
                    position = Input.GetInputPosition();
                }

                if(Input.IsTouchSupported() && Input.GetTouch(0).phase == TouchState.Began)
                {
                    touch = true;
                }

                if(push)
                {
                    float2 s2wPos = MyS2W(position);
                    if(235f <= s2wPos.x && s2wPos.x <= 287f
                        && 62f <= s2wPos.y && s2wPos.y <= 114f)
                    {
                        string score = GetSingleton<Counter>().Value.ToString();
                        // string url = "https://twitter.com/intent/tweet?text=" + score + "個のニンジンを集めました\n&via=poi_third&url=https://poikelp.github.io/TinyPeko";
                        // if(Input.IsTouchSupported())
                        // {
                        //     url = "twitter://post?message=" + score + "個のニンジンを集めました\nhttps://poikelp.github.io/TinyPeko @poi_third より";
                        // }
                        URLOpener.OpenURL(score);
                        
                    }
                    

                    
                }
            }
        }
        private float2 MyS2W (float2 pos)
        {
            var displayInfo = GetSingleton<DisplayInfo>();
            float screenWidth = displayInfo.width;
            float screenHeight = displayInfo.height;

            float targetWidth = 350.0f;
            float targetHeight = 350.0f;

            float targetRatio = 350.0f / 350.0f;
            float currentRatio = displayInfo.width / displayInfo.height;

            float ratioDifference = targetRatio / currentRatio;
            

            
            if(screenHeight > screenWidth)
            {
                float offset = (screenHeight - screenWidth) / 2;
                pos.y -= offset;
            }
            else
            {
                float offset = (screenWidth - screenHeight) /2;
                pos.x -= offset;
            }
            
            pos = pos / math.min(screenHeight, screenWidth);
            
            pos.x *= targetWidth;
            pos.y *= targetHeight;


            return pos;

        }
    }
}
