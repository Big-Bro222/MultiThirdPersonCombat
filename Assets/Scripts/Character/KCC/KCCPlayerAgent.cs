using System.Collections;
using System.Collections.Generic;
using Example;
using Fusion;
using Fusion.KCC;
using UnityEngine;

namespace BigBro
{
	/// <summary>
	/// Simple variant of controlling player.
	/// </summary>
	[OrderBefore(typeof(KCC))]
	[OrderAfter(typeof(Player))]
	public sealed class KCCPlayerAgent : Example.Player
	{
		private KCCPlayerInputHandler _playerInput;
		// NetworkBehaviour INTERFACE
		public override void Spawned()
		{
			base.Spawned();
			_playerInput = GetComponent<KCCPlayerInputHandler>();
		}

		public override sealed void FixedUpdateNetwork()
		{
			base.FixedUpdateNetwork();
			// For following lines, we should use Input.FixedInput only. This property holds input for fixed updates.

			// Clamp input look rotation delta
			Vector2 lookRotation      = KCC.FixedData.GetLookRotation(true, true);
			Vector2 lookRotationDelta = KCCUtility.GetClampedLookRotationDelta(lookRotation, _playerInput.FixedInput.LookRotationDelta, -MaxCameraAngle, MaxCameraAngle);

			// Apply clamped look rotation delta
			KCC.AddLookRotation(lookRotationDelta);

			// Calculate input direction based on recently updated look rotation (the change propagates internally also to KCCData.TransformRotation)
			Vector3 inputDirection = KCC.FixedData.TransformRotation * new Vector3(_playerInput.FixedInput.MoveDirection.x, 0.0f, _playerInput.FixedInput.MoveDirection.y);

			KCC.SetInputDirection(inputDirection);

			if (_playerInput.WasActivated(EGameplayInputAction.Jump) == true)
			{
				// By default the character jumps forward in facing direction
				Quaternion jumpRotation = KCC.FixedData.TransformRotation;

				// If we are moving, jump in that direction instead
				if (inputDirection.IsAlmostZero() == false)
				{
					jumpRotation = Quaternion.LookRotation(inputDirection);
				}

				// Applying jump impulse
				KCC.Jump(jumpRotation * JumpImpulse);
			}

			if (KCC.FixedData.IsGrounded == true)
			{
				// Sprint is updated only when grounded
				KCC.SetSprint(_playerInput.FixedInput.Sprint);
			}

			if (_playerInput.WasActivated(EGameplayInputAction.Dash) == true)
			{
				// We only care about registering processor to the KCC, responsibility for cleanup is on dash processor.
				KCC.AddModifier(DashProcessor);
			}

			if (_playerInput.WasActivated(EGameplayInputAction.Fire) == true)
			{
				//Debug.LogError("Fire!");
			}

			if (_playerInput.WasActivated(EGameplayInputAction.Aim) == true)
			{
				//Debug.LogError("Aim!");
			}

			if (_playerInput.WasActivated(EGameplayInputAction.MMB) == true)
			{
				// Middle mouse button action
			}

			// Additional input processing goes here
		}

		public override sealed void Render()
		{
			base.Render();

			// Only input authority can predict in render.
			if (HasInputAuthority == false)
				return;

			// For following lines, we should use Input.RenderInput and Input.CachedInput only. These properties hold input for render updates.
			// Input.RenderInput holds input for current render frame.
			// Input.CachedInput holds combined input for all render frames from last fixed update. This property will be used to set input for next fixed update (polled by Fusion).

			// Look rotation have to be updated to get smooth camera rotation

			// Get look rotation from last fixed update (not last render!)
			Vector2 lookRotation = KCC.FixedData.GetLookRotation(true, true);

			// For correct look rotation, we have to apply deltas from all render frames since last fixed update => stored in Input.CachedInput
			Vector2 lookRotationDelta = KCCUtility.GetClampedLookRotationDelta(lookRotation, _playerInput.CachedInput.LookRotationDelta, -MaxCameraAngle, MaxCameraAngle);

			KCC.SetLookRotation(lookRotation + lookRotationDelta);

			// At his point, KCC haven't been updated yet (except look rotation, which propagates immediately).
			// Camera have to be synced later (LateUpdate in this case) or we have to update KCC manually (this approach is used in AdvancedPlayer).
		}

		// MonoBehaviour INTERFACE

		private void LateUpdate()
		{
			if (Runner == null)
				return;

			// Updating camera pivot.
			Vector2 pitchRotation = KCC.Data.GetLookRotation(true, false);
			CameraPivot.localRotation = Quaternion.Euler(pitchRotation);

			// Refreshing camera position and rotation.
			RefreshCamera(true);
		}
	}
}
