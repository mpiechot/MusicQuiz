#nullable enable

using UnityEngine;
using UnityEngine.Rendering;

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
        ///     Subscribes to camera rendering callbacks.
        /// </summary>
        private void OnEnable()
        {
            RenderPipelineManager.endCameraRendering += ApplyBlur;
        }

        /// <summary>
        ///     Removes the camera rendering callbacks.
        /// </summary>
        private void OnDisable()
        {
            RenderPipelineManager.endCameraRendering -= ApplyBlur;
        }

        /// <summary>
        ///     Applies the blur effect after the camera finishes rendering.
        /// </summary>
        /// <param name="context">Current render context.</param>
        /// <param name="camera">Camera being rendered.</param>
        private void ApplyBlur(ScriptableRenderContext context, Camera camera)
        {
            if (blurMaterial == null || camera.cameraType != CameraType.Game)
            {
                return;
            }

            blurMaterial.SetFloat("_BlurAmount", blurAmount);
            CommandBuffer cmd = CommandBufferPool.Get(nameof(CameraBlurController));
            cmd.Blit(BuiltinRenderTextureType.CurrentActive, BuiltinRenderTextureType.CameraTarget, blurMaterial);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }
}
