using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using System.Collections;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;
		public bool lookFromTouch;

        [Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		[Header("Shooting settings")]
		public GameObject projectilePrefab;
		public Transform projectileSpawnPoint;
		public float projectileForce = 20;
		public float cooldown;
		public bool cooldownDone = true;
		public bool powerUp = false;
		public float powerUpTime = 20;
        public Image timerBar;
        public float tiempoTotal = 0f;
        public float tiempoRestante = 0;
		public AudioSource ProjectileSound;

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				lookFromTouch = false;
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnShoot(InputValue value)
		{
			Shoot();
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		private void Shoot()
		{
			if (cooldownDone)
			{
                GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
                newProjectile.GetComponent<Rigidbody>().AddForce(projectileSpawnPoint.up * projectileForce, ForceMode.Impulse);
				ProjectileSound.Play();
				cooldownDone = false;
                StartCoroutine(ShootCooldown());

                Destroy(newProjectile, 5f);
            }
		}

        IEnumerator ShootCooldown()
        {
            yield return new WaitForSeconds(cooldown);

			cooldownDone=true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Box"))
            {
                Destroy(other.gameObject);
				powerUp = true;
				tiempoRestante = 20;
				tiempoTotal = 20;
				StartCoroutine(ActivePowerUp());
            }
        }

		IEnumerator ActivePowerUp()
		{
			yield return new WaitForSeconds(powerUpTime);
			powerUp = false;
		}

        private void Update()
        {
            if (powerUp)
			{
				cooldown = 0.01f;
			}
			else
			{
				cooldown = 0.15f;
			}

            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                timerBar.fillAmount = tiempoRestante / tiempoTotal;
            }
            else
            {
                tiempoRestante = 0;
                timerBar.fillAmount = 0;
            }
        }
    }
	
}