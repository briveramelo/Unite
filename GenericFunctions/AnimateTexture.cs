
#region Declarations
using UnityEngine;
using System.Collections;

public class AnimateTexture : MonoBehaviour {
#endregion


	#region Initialize Variable
	public int _uvTieX = 1;
	public int _uvTieY = 1;
	public int _fps = 30;
	public bool toggleScale;
	public Vector2 offset;

	private Vector2 _size;
	private MeshRenderer _myRenderer;
	private int _lastIndex = -1;
	
	void Awake () 
	{
		toggleScale = false;
		_myRenderer = GetComponent<MeshRenderer>();
		if(_myRenderer == null)
			enabled = false;
	}
	#endregion


	#region Set Texture Scales / Offsets
	void Update()
	{
		_size = new Vector2 (1.0f / _uvTieX , 1.0f / _uvTieY);
		// Calculate index
		int index = (int)(Time.timeSinceLevelLoad * _fps) % (_uvTieX * _uvTieY);
		if(index != _lastIndex)
		{
			// split into horizontal and vertical index
			int uIndex = index % _uvTieX;
			int vIndex = index / _uvTieY;
			
			// build offset
			// v coordinate is the bottom of the image in opengl so we need to invert.
			if (toggleScale){
				_myRenderer.material.SetTextureScale ("_MainTex", _size);
			}
			offset = new Vector2 (uIndex * _size.x, 1.0f - _size.y - vIndex * _size.y);
			_myRenderer.material.SetTextureOffset ("_MainTex", offset);

			_lastIndex = index;
		}
	}
	#endregion
}
