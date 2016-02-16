using DG.Tweening;
using UnityEngine;

namespace UFZ.Interaction
{
	/// <summary>
	/// Creates a menu navigatable camera viewpoint. It is important that the GameObject where
	/// this script is attached to is a child of a GameObject named "Viewpoints"!
	/// </summary>
	public class Viewpoint : MonoBehaviour
	{
		public string NodeToMove = "Player";
		public bool StartHere = false;
		public string Name = "Viewpoint";

		private GameObject _nodeToMove;

		public void Awake()
		{
			Name = name;
		}

		public void Start()
		{
			_nodeToMove = GameObject.Find("Player");

#if MVR
			_moveCommand = new vrCommand("", MoveHandler);
			_jumpCommand = new vrCommand("", JumpHandler);
#endif

			if (StartHere)
				Jump();

			// Workaround to null exceptions when there is no subscriber to the event
			OnFinish += delegate { return; };
			OnStart += delegate (float duration) { return; };
			OnFinish += delegate { return; };
		}

#if MVR
		private void OnDestroy()
		{
			MiddleVR.DisposeObject(ref _moveCommand);
			MiddleVR.DisposeObject(ref _jumpCommand);
		}

		private vrCommand _moveCommand;
		private vrCommand _jumpCommand;

		private vrValue MoveHandler(vrValue value)
		{
			MoveInternal();
			return true;
		}

		private vrValue JumpHandler(vrValue value)
		{
			JumpInternal();
			return true;
		}
#endif

		/// <summary>
		/// Moves a GameObject smoothly to this viewpoint.
		/// </summary>
		public void Move()
		{
#if MVR
			if (_moveCommand != null)
				_moveCommand.Do(true);
#else
			MoveInternal();
#endif
		}
		private void MoveInternal()
		{
			const float speed = 1.5f; // units per seconds
			var vec = transform.position - _nodeToMove.transform.position;
			var length = vec.magnitude;
			var duration = length/speed;

			_nodeToMove.transform.DOMove(transform.position, duration)
				.OnStart(() => OnStart(duration)).OnComplete(() => OnFinish());
			_nodeToMove.transform.DORotate(transform.rotation.eulerAngles, duration);
		}

		/// <summary>
		/// Translates a GameObject to a viewpoint instantly.
		/// </summary>
		public void Jump()
		{
#if MVR
			if (_jumpCommand != null)
				_jumpCommand.Do(true);
#else
			JumpInternal();
#endif
		}
		private void JumpInternal()
		{
			_nodeToMove.transform.position = transform.position;
			_nodeToMove.transform.rotation = transform.rotation;
			if (OnSet != null) OnSet();
		}

		public delegate void OnSetEvent();
		public event OnSetEvent OnSet;

		public delegate void OnFinishEvent();
		public event OnFinishEvent OnFinish;

		public delegate void OnStartEvent(float duration);
		public event OnStartEvent OnStart;
	}
}
