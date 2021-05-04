using UnityEngine;

namespace Es.InkPainter.Sample
{
	public class MousePainter : MonoBehaviour
	{
		/// <summary>
		/// Types of methods used to paint.
		/// </summary>
		[System.Serializable]
		private enum UseMethodType
		{
			RaycastHitInfo,
			WorldPoint,
			NearestSurfacePoint,
			DirectUV,
		}

		[SerializeField] 
		AudioSource sprayAudio;

		[SerializeField]
		LayerMask layer;

		[SerializeField] 
		GameObject nextButton;

		[SerializeField]
		GameObject tutorialIcon;

		[SerializeField]
		private Brush brush;

		[SerializeField]
		private UseMethodType useMethodType = UseMethodType.RaycastHitInfo;

		[SerializeField]
		bool erase = false;

		public bool mouseCanPaint = false;
		public void CHANGEBrushColor(Color color)
        {
			brush.brushColor = color;
        }
        private void Update()
		{
			if (Input.GetMouseButton(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitInfo;
				bool success = true;
				if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layer))
				{
					var paintObject = hitInfo.transform.GetComponent<InkCanvas>();
                    var paintableObject = hitInfo.transform.GetComponent<Paintable>();

                    if (paintObject != null && paintableObject != null && paintableObject.CanPaint && mouseCanPaint)
                        switch (useMethodType)
						{
							case UseMethodType.RaycastHitInfo:
								success = erase ? paintObject.Erase(brush, hitInfo) : paintObject.Paint(brush, hitInfo);
                                if (success)
                                {
                                    sprayAudio.Play();
                                    paintableObject.Painted = true;

                                    if (!nextButton.activeInHierarchy)
                                        nextButton.SetActive(true);

                                    if (tutorialIcon.activeInHierarchy)
                                        tutorialIcon.SetActive(false);

									//if (!ColorUI.scoreIsCalculated)
									{
                                        ColorUI.CalculateScore();
									}
								}
                                break;

							case UseMethodType.WorldPoint:
								success = erase ? paintObject.Erase(brush, hitInfo.point) : paintObject.Paint(brush, hitInfo.point);
								break;

							case UseMethodType.NearestSurfacePoint:
								success = erase ? paintObject.EraseNearestTriangleSurface(brush, hitInfo.point) : paintObject.PaintNearestTriangleSurface(brush, hitInfo.point);
								break;

							case UseMethodType.DirectUV:
								if (!(hitInfo.collider is MeshCollider))
									Debug.LogWarning("Raycast may be unexpected if you do not use MeshCollider.");
								success = erase ? paintObject.EraseUVDirect(brush, hitInfo.textureCoord) : paintObject.PaintUVDirect(brush, hitInfo.textureCoord);
								break;
						}
					if (!success)
						Debug.LogError("Failed to paint.");
				}
			}
			else
            {
				sprayAudio.Pause();
            }
		}
		public void DisablePainting()
        {
			mouseCanPaint = false;
		}
		public void EnablePainting()
		{
			mouseCanPaint = true;
		}
		public void ResetPaint()
        {
			foreach (var canvas in FindObjectsOfType<InkCanvas>())
				canvas.ResetPaint();
		}
	}
}