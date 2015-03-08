using UnityEngine;
using System.Collections;

public class FrameBasedAnimation : StateMachineBehaviour {

	[SerializeField]
	private int frameCount = 5;

	private float frameDeltaTime;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		frameCount = 0;
		frameDeltaTime = 1f / ((float)frameCount);
		animator.SetInteger ("frame", frameCount);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log (frameCount);
		animator.Play (stateInfo.shortNameHash, layerIndex, (float)frameCount * frameDeltaTime);
		frameCount++;
		animator.SetInteger ("frame", frameCount);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
