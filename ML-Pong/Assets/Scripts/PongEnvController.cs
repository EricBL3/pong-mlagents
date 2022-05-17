using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Event
{
    LeftPaddleGoal = 0,
    RightPaddleGoal = 1
}

public class PongEnvController : MonoBehaviour
{
    [SerializeField] private PaddleAgent leftPaddleAgent;
    [SerializeField] private PaddleAgent rightPaddleAgent;
    [SerializeField] private Ball ball;
    [SerializeField] private SpriteRenderer middleLineRenderer;
    [SerializeField] private int maxEnvironmentSteps = 2000;
    private int resetTimer;

    public void ResolveEvent(Event triggerEvent)
    {
        switch (triggerEvent)
        {
            //Rewards for self-competitive play
            case Event.LeftPaddleGoal:
                leftPaddleAgent.AddReward(1f);
                rightPaddleAgent.AddReward(-1f);
                StartCoroutine(SwapMiddleLineColor(Color.blue, 0.5f));
                leftPaddleAgent.EndEpisode();
                rightPaddleAgent.EndEpisode();
                ResetScene();
                break;
            case Event.RightPaddleGoal:
                leftPaddleAgent.AddReward(-1f);
                rightPaddleAgent.AddReward(1f);
                StartCoroutine(SwapMiddleLineColor(Color.red, 0.5f));
                leftPaddleAgent.EndEpisode();
                rightPaddleAgent.EndEpisode();
                ResetScene();
                break;
        }
    }

    private IEnumerator SwapMiddleLineColor(Color color, float time)
    {
        middleLineRenderer.color = color;
        yield return new WaitForSeconds(time);
        middleLineRenderer.color = Color.white;
    }

    private void ResetScene()
    {
        resetTimer = 0;
        ball.Launch();
    }

    private void FixedUpdate()
    {
        resetTimer += 1;
        if (resetTimer >= maxEnvironmentSteps && maxEnvironmentSteps > 0)
        {
            leftPaddleAgent.EpisodeInterrupted();
            rightPaddleAgent.EpisodeInterrupted();
            ResetScene();
        }
    }
}
