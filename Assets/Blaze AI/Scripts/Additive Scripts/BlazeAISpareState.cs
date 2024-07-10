using BlazeAISpace;
using UnityEngine;

[AddComponentMenu("Blaze AI/Additive Scripts/Blaze AI Spare State")]
public class BlazeAISpareState : MonoBehaviour
{
    public SpareState[] spareStates;
    [HideInInspector] public BlazeAI blaze;
    public SpareState chosenState { private set; get; }

    #region UNITY METHODS

    private void OnValidate()
    {
        ValidateStateNames();
    }

    #endregion


    #region SYSTEM VARIABLES

    private bool isExitOnTimer;
    private float stateTimer;
    private BlazeAI.State previousBlazeState;

    #endregion

    #region SYSTEM METHODS

    // set the state
    public void SetState(string stateName, int animIndex = -1, int audioIndex = -1)
    {
        var max = spareStates.Length;
        for (var i = 0; i < max; i++)
        {
            var state = spareStates[i];

            if (state.stateName == stateName)
            {
                TriggerState(state, animIndex, audioIndex);
                break;
            }
        }
    }

    public virtual void TriggerState(SpareState state, int passedAnimIndex = -1, int passedAudioIndex = -1)
    {
        // set the previous state of Blaze to exit to
        if (blaze.state != BlazeAI.State.spareState)
        {
            if (blaze.state == BlazeAI.State.distracted)
            {
                previousBlazeState = blaze.previousState;
            }
            else if (blaze.state == BlazeAI.State.hit)
            {
                if (blaze.enemyToAttack) previousBlazeState = BlazeAI.State.attack;
                else previousBlazeState = BlazeAI.State.alert;
            }
            else
            {
                previousBlazeState = blaze.state;
            }
        }

        blaze.SetState(BlazeAI.State.spareState);
        state.enterEvent.Invoke();

        // choose & play animation
        PlayAnimation(state, passedAnimIndex);

        // choose & play audio
        if (state.playAudio) PlayAudio(state, passedAudioIndex);

        stateTimer = 0;
        chosenState = state;

        if (state.exitMethod == SpareState.ExitMethod.ExitAfterTime)
        {
            isExitOnTimer = true;
            return;
        }

        isExitOnTimer = false;
    }

    public virtual void ExitState()
    {
        isExitOnTimer = false;
        stateTimer = 0;
        blaze.SetState(previousBlazeState);
        chosenState.exitEvent.Invoke();
    }

    public void StateTimer()
    {
        if (!isExitOnTimer) return;

        stateTimer += Time.deltaTime;
        if (stateTimer >= chosenState.exitTimer) ExitState();
    }

    private void ValidateStateNames()
    {
        if (spareStates == null) return;

        var max = spareStates.Length;
        for (var i = 0; i < max; i++)
        {
            // remove start and end spaces from state names
            var name = spareStates[i].stateName;
            spareStates[i].stateName = name.Trim();
        }
    }

    private void PlayAnimation(SpareState state, int passedAnimIndex)
    {
        if (state.animsToPlay == null) return;
        if (state.animsToPlay.Length == 0) return;

        var animName = "";

        if (passedAnimIndex < 0)
        {
            var randAnimIndex = Random.Range(0, state.animsToPlay.Length);
            animName = state.animsToPlay[randAnimIndex].Trim();
        }
        else
        {
            if (passedAnimIndex <= state.animsToPlay.Length - 1) animName = state.animsToPlay[passedAnimIndex].Trim();
        }

        if (animName.Length > 0)
        {
            blaze.animManager.Play(animName, state.animT);
        }
    }

    private void PlayAudio(SpareState state, int passedAudioIndex)
    {
        if (state.audiosToPlay == null) return;
        if (state.audiosToPlay.Length == 0) return;

        AudioClip chosenAudio = null;

        if (passedAudioIndex < 0)
        {
            var audioIndex = Random.Range(0, state.audiosToPlay.Length);
            chosenAudio = state.audiosToPlay[audioIndex];
        }
        else
        {
            if (passedAudioIndex <= state.audiosToPlay.Length - 1) chosenAudio = state.audiosToPlay[passedAudioIndex];
        }

        if (chosenAudio != null)
        {
            blaze.agentAudio.clip = chosenAudio;
            blaze.agentAudio.Play();
        }
    }

    #endregion
}