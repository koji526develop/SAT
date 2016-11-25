using UnityEngine;
using System.Collections;

public class RotateBy : MonoBehaviour {

	public enum Axis
	{
		X,
		Y,
		Z
	}

	bool isRotateBy = false;
	Vector3 m_origin;
	float m_value = 0.0f;
	float m_rotateValue = 360.0f;
	float m_speed = 2.0f;
	Axis m_axis;

	public void SetRotateBy(Axis _axis, float _rotateValue, float _speed)
	{
		isRotateBy = true;
		m_value = 0.0f;
		m_axis = _axis;
		m_origin = transform.rotation.eulerAngles;
		m_rotateValue = _rotateValue;
		m_speed = _speed;
	}

	// Update is called once per frame
	void Update () {

		if (!isRotateBy)
			return;

		m_value += m_speed;

		if (m_value >= m_rotateValue)
		{
			isRotateBy = false;

			switch (m_axis)
			{
			case Axis.X:
				transform.eulerAngles = new Vector3(m_origin.x + m_rotateValue, m_origin.y, m_origin.z);
				break;
			case Axis.Y:
				transform.eulerAngles = new Vector3(m_origin.x, m_origin.y + m_rotateValue, m_origin.z);
				break;
			case Axis.Z:
				transform.eulerAngles = new Vector3(m_origin.x, m_origin.y, m_origin.z + m_rotateValue);
				break;
			default:
				break;
			}
		} 
		else
		{
			switch (m_axis)
			{
			case Axis.X:
				gameObject.transform.Rotate (new Vector3 (m_speed, 0, 0));
				break;
			case Axis.Y:
				gameObject.transform.Rotate (new Vector3 (0, m_speed, 0));
				break;
			case Axis.Z:
				gameObject.transform.Rotate (new Vector3 (0, 0, m_speed));
				break;
			default:
				break;
			}
		}
	}
}
