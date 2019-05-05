using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;

public class PlayerController : Subject
{
	public float speed;
	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private new Camera camera;
	public float rotationSpeed = 7f;
	public WeaponObject currentWeapon;
	public GameObject Handle;
	public Interactable focus;
	public bool ActionMode = false;

	void Awake()
	{
		speed = 6f;
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
		camera = Camera.main;
	}

	private void Start()
	{
		Equip(EquipmentManager.instance.BareHands);
	}


	// Update is called once per frame
	void Update()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		
		WeaponSwitch();
		Focus();
		ModeSwitch();
	}

	private void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");


		Move(h, v);
		Turning();
		Animating();
	}

	void Move(float h, float v)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
        
        
		playerRigidbody.MovePosition (transform.position + movement);
		
		
	}

	void Turning()
	{
		Plane playerPlane = new Plane(Vector3.up,  transform.position);
		Ray camRay = camera.ScreenPointToRay(Input.mousePosition);
		float hitDistance = 0.0f;


		if (playerPlane.Raycast(camRay, out hitDistance))
		{
			Vector3 targetPoint = camRay.GetPoint(hitDistance);
			Quaternion TargetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			TargetRotation.x = 0;
			TargetRotation.z = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, rotationSpeed * Time.deltaTime);
		}
	}

	//	Changing animations depending on relative player rotation and movement
	void Animating()
	{
		Vector3 input = GetMovementAxis * speed;
		movement = camera.transform.TransformDirection(input);
		Vector3 relative = transform.InverseTransformDirection(movement);

		anim.SetFloat("horizontal", relative.x);
		anim.SetFloat("vertical", relative.z);
		anim.SetFloat("weaponID", currentWeapon.animationType);
	}

	void WeaponSwitch()
     	{
     		if (Input.GetKeyDown(KeyCode.Mouse2) && ActionMode)
     		{
	            if (currentWeapon == EquipmentManager.instance.RangedWeaapon)
	            {
		            Equip(EquipmentManager.instance.MeleeWeapon);
	            }
	            else
	            {
		            Equip(EquipmentManager.instance.RangedWeaapon);
	            }
     		}
     	}
	
	public void ModeSwitch(int forceSwitch = 0)
	{
		if (Input.GetKeyDown(KeyCode.Tab) || forceSwitch == 1)
		{
			if (forceSwitch == 0 && EquipmentManager.instance.RangedWeaapon != null)
			{
				ActionMode = !ActionMode;
			}
			else
			{
				ActionMode = false;
			}

			if (ActionMode && EquipmentManager.instance.RangedWeaapon != null)
			{
				Equip(EquipmentManager.instance.RangedWeaapon);
			}
			else
			{
				Equip(EquipmentManager.instance.BareHands);
			}
		}
	}
//	Save current position
	private Vector3 GetMovementAxis =>
		new Vector3(
			Input.GetAxis("Horizontal"),
			0,
			Input.GetAxis("Vertical"));

	private void Equip(WeaponObject weapon)
	{
		if (currentWeapon != null)
		{
			Destroy(currentWeapon.gameObject);
		}

		if (weapon != null)
		{
			currentWeapon = Instantiate(weapon,
				new Vector3(Handle.transform.position.x, Handle.transform.position.y, Handle.transform.position.z),
				Quaternion.identity);
			currentWeapon.transform.parent = Handle.transform;
			currentWeapon.transform.rotation = Handle.transform.rotation;
		}
	}
	
	

	private void Focus()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100))
			{
				RemoveFocus();
			}
		} 
		
		if (Input.GetMouseButtonDown(1))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100))
			{
				Interactable interactable = hit.collider.GetComponent<Interactable>();
				if (interactable != null)
				{
					SetFocus(interactable);
				}
			}
		}
	}

	void SetFocus(Interactable newFocus)
	{
		if (newFocus != focus)
		{
			if (focus != null)
			{
				focus.OnDefocus();
			}

			focus = newFocus;
		}
		focus.OnFocused(transform);
		focus.Interact();
	}

	void RemoveFocus()
	{
		if (focus != null)
		{
			focus.OnDefocus();
		}

		focus = null;
	}
}
