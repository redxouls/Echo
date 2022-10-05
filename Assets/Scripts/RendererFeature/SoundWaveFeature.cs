using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SoundWaveFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class SoundWaveSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        public Material material;
    }

    [SerializeField] private SoundWaveSettings settings;

    class SoundWavePass : ScriptableRenderPass
    {
        // This method is called before executing the render pass.
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in a performant manner.
        private SoundWaveFeature.SoundWaveSettings settings;
        
        private RenderTargetIdentifier colorBuffer, soundWaveBuffer;
        private int soundWaveBufferID = Shader.PropertyToID("_SoundWaveBuffer");

        private Material material;

        public SoundWavePass(SoundWaveFeature.SoundWaveSettings settings) 
        {
            this.settings = settings;
            this.renderPassEvent = settings.renderPassEvent;
            if (settings.material == null) material = CoreUtils.CreateEngineMaterial("Hidden/SoundWavePlane");
            else material = settings.material;
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;
            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
            
            material.SetMatrix("_InvProjectionMatrix", renderingData.cameraData.camera.projectionMatrix.inverse);
            material.SetMatrix("_ViewToWorld", renderingData.cameraData.camera.cameraToWorldMatrix);

            cmd.GetTemporaryRT(soundWaveBufferID, descriptor, FilterMode.Point);
            soundWaveBuffer = new RenderTargetIdentifier(soundWaveBufferID);
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, new ProfilingSampler("SoundWave Pass")))
            {
                // No-shader variant
                //Blit(cmd, colorBuffer, pointBuffer);
                //Blit(cmd, pointBuffer, pixelBuffer);
                //Blit(cmd, pixelBuffer, colorBuffer);

                Blit(cmd, colorBuffer, soundWaveBuffer, material);
                Blit(cmd, soundWaveBuffer, colorBuffer);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        // Cleanup any allocated resources that were created during the execution of this render pass.
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            if (cmd == null) throw new System.ArgumentNullException("cmd");
            cmd.ReleaseTemporaryRT(soundWaveBufferID);
            //cmd.ReleaseTemporaryRT(pointBufferID);
        }
    }

    private SoundWavePass m_ScriptablePass;

    /// <inheritdoc/>
    public override void Create()
    {
        m_ScriptablePass = new SoundWavePass(settings);

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // renderer.EnqueuePass(m_ScriptablePass);

#if UNITY_EDITOR
        if (renderingData.cameraData.isSceneViewCamera) return;
#endif
        renderer.EnqueuePass(m_ScriptablePass);
    }
}


