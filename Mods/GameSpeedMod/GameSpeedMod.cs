﻿/** 
 * GameSpeedMod.cs
 * Dylan Bailey
 * 11/14/16
 * License - Do whatever you want with this code as long as no puppies are set aflame
*/

using Game;
using Game.UI;
using Patchwork;
using UnityEngine;
//using SDK;

namespace TyrannyMods.pw
{
    /// <summary>
    /// Modifies the game speed toggles to add a new 6x speed.
    /// </summary>
    [ModifiesType]
    public class mod_TimeController : Game.TimeController
    {
        [NewType]
        enum GameSpeedState
        {
            SLOW,
            NORMAL,
            DOUBLE,
            SIX,
            TEN
        }

        [NewMember]
        GameSpeedState gameSpeed = GameSpeedState.NORMAL;

        [ModifiesMember("ToggleFast")]
        public void ToggleFastNew()
        {
            switch(gameSpeed) {
                case GameSpeedState.SLOW:
                    this.TimeScale = 1.0f;
                    gameSpeed = GameSpeedState.NORMAL;
                    break;
                case GameSpeedState.NORMAL:
                    this.TimeScale = 2.0f;
                    gameSpeed = GameSpeedState.DOUBLE;
                    break;
                case GameSpeedState.DOUBLE:
                    this.TimeScale = 6.0f;
                    gameSpeed = GameSpeedState.SIX;
                    break;
                default:
                    break;
            }
            
            this.UpdateTimeScale();
        }

        [ModifiesMember("ToggleSlow")]
        public void ToggleSlowNew()
        {
           switch(gameSpeed) {
                case GameSpeedState.TEN:
                    this.TimeScale = 6.0f;
                    gameSpeed = GameSpeedState.SIX;
                    UltraFastModeEngaged = false;
                    break;
                case GameSpeedState.SIX:
                    this.TimeScale = 2.0f;
                    gameSpeed = GameSpeedState.DOUBLE;
                    break;
                case GameSpeedState.DOUBLE:
                    this.TimeScale = 1.0f;
                    gameSpeed = GameSpeedState.NORMAL;
                    break;
                case GameSpeedState.NORMAL:
                    this.TimeScale = 0.2f;
                    gameSpeed = GameSpeedState.SLOW;
                    break;
                default:
                    break;
            }
            this.UpdateTimeScale();
        }

        [ModifiesMember("UpdateTimeScale")]
        public void UpdateTimeScaleNew()
        {
            if ((this.m_PlayerPaused || this.m_UiPaused) && !this.ProhibitPause) {
                Time.timeScale = 0f;
            }
            else if (Cutscene.CutsceneActive || Onyx.SingletonBehavior<ConversationManager>.Instance.IsConversationOrSIRunning()) {
                Time.timeScale = 1f;
            }
            //else if (GameState.InCombat) {
            //    Time.timeScale = GameState.Option.CombatSpeed;
            //}
            else {
                Time.timeScale = this.TimeScale;
            }
        }

        [NewMember]
        bool UltraFastModeEngaged = false;

        [ModifiesMember("OnyxUpdate")]
        public void OnyxUpdateNew()
        {
            this.RealtimeSinceStartupThisFrame = Time.realtimeSinceStartup;
            this.GameTimeSinceStartup += Time.deltaTime;
            float num = 0.2f;
            TimeController.m_smoothUnscaledDeltaTime = num * TimeController.UnscaledDeltaTime + (1f - num) * TimeController.m_previousSmoothUnscaledDeltaTime;
            TimeController.m_previousSmoothUnscaledDeltaTime = TimeController.m_smoothUnscaledDeltaTime;
            //if (!this.CanUseSlowMode && this.TimeScale == this.SlowTime) {
            //    this.TimeScale = 1f;
            //}
            //if (!this.CanUseFastMode && this.TimeScale == this.FastTime) {
            //    this.TimeScale = 1f;
            //}
            if (!GameState.IsLoading) {
                this.UpdateTimeScale();
            }
            if (UIWindowManager.KeyInputAvailable) {
                if (GameInput.GetControlDown(MappedControl.RESTORE_SPEED, true)) {
                    this.TimeScale = this.NormalTime;
                }
                else if (GameInput.GetControlDown(MappedControl.COMBAT_SPEED_DOWN, true)) {
                    this.ToggleSlow();
                     //Debug.LogError("TOGGLE SLOW");
                }
                else if (GameInput.GetControlDown(MappedControl.COMBAT_SPEED_UP, true)) {
                    this.ToggleFast();
                     //Debug.LogError("TOGGLE SLOW");
                }
                else if (GameInput.GetControlDown(MappedControl.FAST_TOGGLE, true)) {
                    if (!UltraFastModeEngaged) {
                        this.TimeScale = 10.0f;
                        gameSpeed = GameSpeedState.TEN;
                        UltraFastModeEngaged = true;
                    }
                    else {
                        this.TimeScale = 1.0f;
                        gameSpeed = GameSpeedState.NORMAL;
                        UltraFastModeEngaged = false;
                    }
                }
                //else if (GameInput.GetControlDown(MappedControl.GAME_SPEED_CYCLE, true)) {
                //    //Debug.LogError("GAME SPEED CYCLE");
                //    if (this.Fast) {
                //        this.Slow = true;
                //    }
                //    else if (this.Slow) {
                //        this.Slow = false;
                //    }
                //    else if (this.CanUseFastMode) {
                //        this.Fast = true;
                //    }
                //    else if (this.CanUseSlowMode) {
                //        this.Slow = true;
                //    }
                //}
            }
        }


    }
}













