using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFxController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem parent;
	[SerializeField]
	private ParticleSystem swordStatic;
	[SerializeField]
	private ParticleSystem Fx, FxInvisible;
	[SerializeField]
	private Texture2D Lv1, Lv3, Lv5;
	[SerializeField]
	private Gradient startColLv1, startColLv3, startColLv5;

	//private void Start()
	//{
	//	_mainSwordStatic = swordStaticSystem.main;
	//}

	public void Swing()
    {
        parent.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        parent.Play(true);
	}

    public void UpdateRotation(float angle)
    {
        bool right = Mathf.Abs(angle) <= 90;
		parent.transform.rotation = Quaternion.Euler(0, 0, angle);
		if (right)
        {
			swordStatic.transform.localRotation = Quaternion.identity;
			swordStatic.transform.localScale = Vector3.one;
			//swordFx.transform.localPosition = new Vector3(1.34f, -1.09f);
		}
        else
        {
			swordStatic.transform.localRotation = Quaternion.Euler(0, 0, 180);
			swordStatic.transform.localScale = new Vector3(-1, 1, 1);
			//swordFx.transform.localPosition = new Vector3(2.68f, -1.09f);
		}
	}

	public void LevelUp(int level)
	{
		ParticleSystem.VelocityOverLifetimeModule vOverTime = Fx.velocityOverLifetime;
		ParticleSystem.ShapeModule shape = Fx.shape;
		ParticleSystem.MainModule main = Fx.main;
		ParticleSystem.TrailModule trail = Fx.trails;
		Texture2D now = level >= 5 ? Lv5 : level >= 3 ? Lv3 : Lv1;
		Fx.GetComponent<ParticleSystemRenderer>().material.SetTexture("_MainTex", now);
		swordStatic.GetComponent<ParticleSystemRenderer>().material.SetTexture("_MainTex", now);
		_ChangeParticleSetting(level, ref vOverTime, ref shape, ref main, ref trail);
		vOverTime = FxInvisible.velocityOverLifetime;
		shape = FxInvisible.shape;
		main = FxInvisible.main;
		_ChangeParticleSetting(level, ref vOverTime, ref shape, ref main, ref trail);
	}
	void _ChangeParticleSetting(int level,
		ref ParticleSystem.VelocityOverLifetimeModule vOverTime,
		ref ParticleSystem.ShapeModule shape,
		ref ParticleSystem.MainModule main,
		ref ParticleSystem.TrailModule trail)
	{
		switch (level)
		{
			case 1:
				vOverTime.orbitalZ = 3f;
				main.startSizeY = 2.5f;
				main.startRotationZ = -45f * Mathf.Deg2Rad;
				shape.position = new Vector3(2, -1, 0);
				trail.colorOverTrail = startColLv1;
				break;
			case 2:
				vOverTime.orbitalZ = 3.1f;
				main.startSizeX = 0.75f;
				main.startSizeY = 3f;
				main.startRotationZ = -38.1f * Mathf.Deg2Rad;
				shape.position = new Vector3(2.15f, -1.28f, 0);
				trail.colorOverTrail = startColLv1;
				break;
			case 3:
				vOverTime.orbitalZ = 3.2f;
				main.startSizeX = 0.85f;
				main.startSizeY = 4f;
				main.startRotationZ = -38.4f * Mathf.Deg2Rad;
				shape.position = new Vector3(2.3f, -1.46f, 0);
				trail.colorOverTrail = startColLv3;
				break;
			case 4:
				vOverTime.orbitalZ = 3.6f;
				main.startSizeX = 1f;
				main.startSizeY = 5f;
				main.startRotationZ = -46.1f * Mathf.Deg2Rad;
				shape.position = new Vector3(2.65f, -1.82f, 0);
				trail.colorOverTrail = startColLv3;
				break;
			case 5:
				vOverTime.orbitalZ = 4.5f;
				main.startSizeX = 1.25f;
				main.startSizeY = 6f;
				main.startRotationZ = -55.62f * Mathf.Deg2Rad;
				shape.position = new Vector3(3.32f, -2.13f, 0);
				trail.colorOverTrail = startColLv5;
				break;
		}
	}
}
