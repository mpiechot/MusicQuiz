#nullable enable

using UnityEngine;

namespace Musicmania.CameraControllers
{
    /// <summary>
    ///     Applies a configurable full-screen blur effect while leaving UI Toolkit elements unaffected.
    /// </summary>
    [ExecuteInEditMode]
    public class CameraBlurController : MonoBehaviour
    {
        /// <summary>
        ///     Amount of blur to apply. 0 for no blur and 1 for maximum blur.
        /// </summary>
        [Range(0f, 1f)]
        [SerializeField]
        private float blurAmount;

        /// <summary>
        ///     Material that performs the blur pass.
        /// </summary>
        [SerializeField]
        private Material? blurMaterial;

        /// <summary>
        ///     Gets or sets the amount of blur to apply.
        /// </summary>
        public float BlurAmount
        {
            get => blurAmount;
            set => blurAmount = Mathf.Clamp01(value);
        }

        /// <summary>
        ///     Renders the scene with the blur effect.
        /// </summary>
        /// <param name="src">Source render texture.</param>
        /// <param name="dest">Destination render texture.</param>
        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (blurMaterial != null)
            {
                blurMaterial.SetFloat("_BlurAmount", blurAmount);
                Graphics.Blit(src, dest, blurMaterial);
            }
            else
            {
                Graphics.Blit(src, dest);
            }
        }
    }
}
