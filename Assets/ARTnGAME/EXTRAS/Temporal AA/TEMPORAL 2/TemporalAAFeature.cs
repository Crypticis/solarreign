/*
MIT License

Copyright (c) 2022 Pascal Zwick

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.Mathematics;

public class TemporalAAFeature : ScriptableRendererFeature
{
    static readonly double2[] Halton2364Seq = { new(0, 0), new(0.5, 0.3333333333333333), new(0.25, 0.6666666666666666), new(0.75, 0.1111111111111111), new double2(0.125, 0.4444444444444444), new double2(0.625, 0.7777777777777777), new double2(0.375, 0.2222222222222222), new double2(0.875, 0.5555555555555556), new double2(0.0625, 0.8888888888888888), new double2(0.5625, 0.037037037037037035), new double2(0.3125, 0.37037037037037035), new double2(0.8125, 0.7037037037037037), new double2(0.1875, 0.14814814814814814), new double2(0.6875, 0.48148148148148145), new double2(0.4375, 0.8148148148148147), new double2(0.9375, 0.25925925925925924), new double2(0.03125, 0.5925925925925926), new double2(0.53125, 0.9259259259259258), new double2(0.28125, 0.07407407407407407), new double2(0.78125, 0.4074074074074074), new double2(0.15625, 0.7407407407407407), new double2(0.65625, 0.18518518518518517), new double2(0.40625, 0.5185185185185185), new double2(0.90625, 0.8518518518518517), new double2(0.09375, 0.2962962962962963), new double2(0.59375, 0.6296296296296297), new double2(0.34375, 0.9629629629629629), new double2(0.84375, 0.012345679012345678), new double2(0.21875, 0.345679012345679), new double2(0.71875, 0.6790123456790123), new double2(0.46875, 0.12345679012345678), new double2(0.96875, 0.4567901234567901), new double2(0.015625, 0.7901234567901234), new double2(0.515625, 0.2345679012345679), new double2(0.265625, 0.5679012345679013), new double2(0.765625, 0.9012345679012346), new double2(0.140625, 0.04938271604938271), new double2(0.640625, 0.38271604938271603), new double2(0.390625, 0.7160493827160495), new double2(0.890625, 0.16049382716049382), new double2(0.078125, 0.49382716049382713), new double2(0.578125, 0.8271604938271604), new double2(0.328125, 0.2716049382716049), new double2(0.828125, 0.6049382716049383), new double2(0.203125, 0.9382716049382716), new double2(0.703125, 0.08641975308641975), new double2(0.453125, 0.41975308641975306), new double2(0.953125, 0.7530864197530864), new double2(0.046875, 0.19753086419753085), new double2(0.546875, 0.5308641975308641), new double2(0.296875, 0.8641975308641974), new double2(0.796875, 0.30864197530864196), new double2(0.171875, 0.6419753086419753), new double2(0.671875, 0.9753086419753085), new double2(0.421875, 0.024691358024691357), new double2(0.921875, 0.35802469135802467), new double2(0.109375, 0.691358024691358), new double2(0.609375, 0.13580246913580246), new double2(0.359375, 0.4691358024691358), new double2(0.859375, 0.802469135802469), new double2(0.234375, 0.24691358024691357), new double2(0.734375, 0.5802469135802469), new double2(0.484375, 0.9135802469135802), new double2(0.984375, 0.06172839506172839), new double2(0.0078125, 0.3950617283950617), new double2(0.5078125, 0.7283950617283951), new double2(0.2578125, 0.1728395061728395), new double2(0.7578125, 0.5061728395061729), new double2(0.1328125, 0.839506172839506), new double2(0.6328125, 0.2839506172839506), new double2(0.3828125, 0.6172839506172839), new double2(0.8828125, 0.9506172839506172), new double2(0.0703125, 0.09876543209876543), new double2(0.5703125, 0.43209876543209874), new double2(0.3203125, 0.7654320987654321), new double2(0.8203125, 0.20987654320987653), new double2(0.1953125, 0.5432098765432098), new double2(0.6953125, 0.8765432098765431), new double2(0.4453125, 0.32098765432098764), new double2(0.9453125, 0.654320987654321), new double2(0.0390625, 0.9876543209876543), new double2(0.5390625, 0.004115226337448559), new double2(0.2890625, 0.33744855967078186), new double2(0.7890625, 0.6707818930041152), new double2(0.1640625, 0.11522633744855966), new double2(0.6640625, 0.44855967078189296), new double2(0.4140625, 0.7818930041152262), new double2(0.9140625, 0.22633744855967078), new double2(0.1015625, 0.5596707818930041), new double2(0.6015625, 0.8930041152263374), new double2(0.3515625, 0.0411522633744856), new double2(0.8515625, 0.3744855967078189), new double2(0.2265625, 0.7078189300411523), new double2(0.7265625, 0.1522633744855967), new double2(0.4765625, 0.48559670781893), new double2(0.9765625, 0.8189300411522632), new double2(0.0234375, 0.2633744855967078), new double2(0.5234375, 0.5967078189300411), new double2(0.2734375, 0.9300411522633744), new double2(0.7734375, 0.07818930041152262), new double2(0.1484375, 0.4115226337448559), new double2(0.6484375, 0.7448559670781892), new double2(0.3984375, 0.18930041152263374), new double2(0.8984375, 0.522633744855967), new double2(0.0859375, 0.8559670781893003), new double2(0.5859375, 0.3004115226337448), new double2(0.3359375, 0.6337448559670782), new double2(0.8359375, 0.9670781893004115), new double2(0.2109375, 0.016460905349794237), new double2(0.7109375, 0.34979423868312753), new double2(0.4609375, 0.6831275720164608), new double2(0.9609375, 0.12757201646090535), new double2(0.0546875, 0.46090534979423864), new double2(0.5546875, 0.794238683127572), new double2(0.3046875, 0.23868312757201646), new double2(0.8046875, 0.5720164609053499), new double2(0.1796875, 0.9053497942386831), new double2(0.6796875, 0.053497942386831275), new double2(0.4296875, 0.38683127572016457), new double2(0.9296875, 0.720164609053498), new double2(0.1171875, 0.1646090534979424), new double2(0.6171875, 0.4979423868312757), new double2(0.3671875, 0.8312757201646089), new double2(0.8671875, 0.27572016460905346), new double2(0.2421875, 0.6090534979423868), new double2(0.7421875, 0.9423868312757201), new double2(0.4921875, 0.0905349794238683), new double2(0.9921875, 0.4238683127572016), new double2(0.00390625, 0.757201646090535), new double2(0.50390625, 0.20164609053497942), new double2(0.25390625, 0.5349794238683127), new double2(0.75390625, 0.8683127572016459), new double2(0.12890625, 0.3127572016460905), new double2(0.62890625, 0.6460905349794238), new double2(0.37890625, 0.9794238683127571), new double2(0.87890625, 0.028806584362139915), new double2(0.06640625, 0.3621399176954732), new double2(0.56640625, 0.6954732510288065), new double2(0.31640625, 0.13991769547325103), new double2(0.81640625, 0.4732510288065843), new double2(0.19140625, 0.8065843621399176), new double2(0.69140625, 0.2510288065843621), new double2(0.44140625, 0.5843621399176955), new double2(0.94140625, 0.9176954732510287), new double2(0.03515625, 0.06584362139917695), new double2(0.53515625, 0.39917695473251025), new double2(0.28515625, 0.7325102880658436), new double2(0.78515625, 0.17695473251028807), new double2(0.16015625, 0.5102880658436214), new double2(0.66015625, 0.8436213991769546), new double2(0.41015625, 0.28806584362139914), new double2(0.91015625, 0.6213991769547325), new double2(0.09765625, 0.9547325102880657), new double2(0.59765625, 0.10288065843621398), new double2(0.34765625, 0.4362139917695473), new double2(0.84765625, 0.7695473251028806), new double2(0.22265625, 0.2139917695473251), new double2(0.72265625, 0.5473251028806584), new double2(0.47265625, 0.8806584362139916), new double2(0.97265625, 0.3251028806584362), new double2(0.01953125, 0.6584362139917695), new double2(0.51953125, 0.9917695473251028), new double2(0.26953125, 0.008230452674897118), new double2(0.76953125, 0.34156378600823045), new double2(0.14453125, 0.6748971193415637), new double2(0.64453125, 0.11934156378600823), new double2(0.39453125, 0.45267489711934156), new double2(0.89453125, 0.7860082304526748), new double2(0.08203125, 0.23045267489711932), new double2(0.58203125, 0.5637860082304527), new double2(0.33203125, 0.8971193415637859), new double2(0.83203125, 0.04526748971193415), new double2(0.20703125, 0.3786008230452675), new double2(0.70703125, 0.7119341563786008), new double2(0.45703125, 0.15637860082304525), new double2(0.95703125, 0.4897119341563786), new double2(0.05078125, 0.8230452674897117), new double2(0.55078125, 0.2674897119341564), new double2(0.30078125, 0.6008230452674896), new double2(0.80078125, 0.9341563786008229), new double2(0.17578125, 0.0823045267489712), new double2(0.67578125, 0.4156378600823045), new double2(0.42578125, 0.7489711934156378), new double2(0.92578125, 0.19341563786008228), new double2(0.11328125, 0.5267489711934156), new double2(0.61328125, 0.8600823045267488), new double2(0.36328125, 0.3045267489711934), new double2(0.86328125, 0.6378600823045267), new double2(0.23828125, 0.97119341563786), new double2(0.73828125, 0.020576131687242795), new double2(0.48828125, 0.35390946502057613), new double2(0.98828125, 0.6872427983539093), new double2(0.01171875, 0.1316872427983539), new double2(0.51171875, 0.46502057613168724), new double2(0.26171875, 0.7983539094650205), new double2(0.76171875, 0.242798353909465), new double2(0.13671875, 0.5761316872427984), new double2(0.63671875, 0.9094650205761317), new double2(0.38671875, 0.05761316872427983), new double2(0.88671875, 0.39094650205761317), new double2(0.07421875, 0.7242798353909465), new double2(0.57421875, 0.16872427983539093), new double2(0.32421875, 0.5020576131687242), new double2(0.82421875, 0.8353909465020575), new double2(0.19921875, 0.27983539094650206), new double2(0.69921875, 0.6131687242798354), new double2(0.44921875, 0.9465020576131686), new double2(0.94921875, 0.09465020576131687), new double2(0.04296875, 0.4279835390946502), new double2(0.54296875, 0.7613168724279835), new double2(0.29296875, 0.20576131687242796), new double2(0.79296875, 0.5390946502057612), new double2(0.16796875, 0.8724279835390945), new double2(0.66796875, 0.3168724279835391), new double2(0.41796875, 0.6502057613168724), new double2(0.91796875, 0.9835390946502056), new double2(0.10546875, 0.03292181069958847), new double2(0.60546875, 0.3662551440329218), new double2(0.35546875, 0.6995884773662551), new double2(0.85546875, 0.14403292181069957), new double2(0.23046875, 0.4773662551440329), new double2(0.73046875, 0.8106995884773661), new double2(0.48046875, 0.2551440329218107), new double2(0.98046875, 0.588477366255144), new double2(0.02734375, 0.9218106995884773), new double2(0.52734375, 0.06995884773662552), new double2(0.27734375, 0.40329218106995884), new double2(0.77734375, 0.7366255144032922), new double2(0.15234375, 0.1810699588477366), new double2(0.65234375, 0.51440329218107), new double2(0.40234375, 0.8477366255144031), new double2(0.90234375, 0.29218106995884774), new double2(0.08984375, 0.625514403292181), new double2(0.58984375, 0.9588477366255143), new double2(0.33984375, 0.10699588477366255), new double2(0.83984375, 0.4403292181069959), new double2(0.21484375, 0.7736625514403291), new double2(0.71484375, 0.21810699588477364), new double2(0.46484375, 0.5514403292181069), new double2(0.96484375, 0.8847736625514402), new double2(0.05859375, 0.3292181069958848), new double2(0.55859375, 0.6625514403292181), new double2(0.30859375, 0.9958847736625513), new double2(0.80859375, 0.0013717421124828531), new double2(0.18359375, 0.3347050754458162), new double2(0.68359375, 0.6680384087791494), new double2(0.43359375, 0.11248285322359396), new double2(0.93359375, 0.4458161865569273), new double2(0.12109375, 0.7791495198902605), new double2(0.62109375, 0.22359396433470508), new double2(0.37109375, 0.5569272976680384), new double2(0.87109375, 0.8902606310013716), new(0.24609375, 0.03840877914951989), new(0.74609375, 0.3717421124828532), new(0.49609375, 0.7050754458161865), new(0.99609375, 0.149519890260631) };
    
    [Range(0, 1)]
    public float TemporalFade = 0.95f;
    public float MovementBlending = 100;

    [Tooltip("If the resulting image appears upside down. Toggle this variable to unflip the image.")]
    public bool UseFlipUV = false;

    public Material TAAMaterial;
    
    public float JitterSpread = 1;
    [Range(1, 256)]
    public int HaltonLength = 8;

    class TemporalAAPass : ScriptableRenderPass
    {

        [Range(0, 1)]
        public float TemporalFade;
        public float MovementBlending;
        public bool UseFlipUV;

        public Material TAAMaterial;
        
        public float JitterSpread = 1;
        [Range(1, 256)]
        public int HaltonLength = 8;

        public static RenderTexture temp, temp1;

        private Matrix4x4 prevViewProjectionMatrix;
        
        private UnityEngine.Camera prevRenderCamera;

        private TemporalAAFeature feature;

        public TemporalAAPass(TemporalAAFeature inFeature) : base()
        {

            //v0.1
            if (Camera.main != null)
            {
                //Debug.Log("Reset camera");
                Camera.main.ResetWorldToCameraMatrix();
                Camera.main.ResetProjectionMatrix();
            }

            if (temp)
            {
                temp.Release();
                temp1.Release();
                temp = null;
            }

            feature = inFeature;
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            base.OnCameraSetup(cmd, ref renderingData);

            if (renderingData.cameraData.cameraType != CameraType.Game || Camera.main == null || renderingData.cameraData.camera != Camera.main)
                return;
            
            prevRenderCamera = renderingData.cameraData.camera;

            ConfigureInput(ScriptableRenderPassInput.Color);
            ConfigureInput(ScriptableRenderPassInput.Depth);

            RenderTextureDescriptor currentCameraDescriptor = renderingData.cameraData.cameraTargetDescriptor;

            if (temp && (currentCameraDescriptor.width != temp.width || currentCameraDescriptor.height != temp.height))
            {
                Debug.Log("Deleting Render Target: " + currentCameraDescriptor.width + " " + temp.width);

                temp.Release();
                temp1.Release();
                temp = null;
            }

            if (!temp)
            {
                temp = new RenderTexture(currentCameraDescriptor.width, currentCameraDescriptor.height, UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat, 0);
                temp1 = new RenderTexture(currentCameraDescriptor.width, currentCameraDescriptor.height, UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat, 0);

                Debug.Log("Allocating new Render Target");
            }
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            
            if (renderingData.cameraData.cameraType != CameraType.Game || Camera.main == null || renderingData.cameraData.camera != Camera.main)
                return;

            UpdateValuesFromFeature();
            CommandBuffer cmd = CommandBufferPool.Get("TemporalAAPass");


            TAAMaterial.SetTexture("_TemporalAATexture", temp);


            Matrix4x4 mt = renderingData.cameraData.camera.nonJitteredProjectionMatrix.inverse;
            TAAMaterial.SetMatrix("_invP", mt);

            mt = this.prevViewProjectionMatrix * renderingData.cameraData.camera.cameraToWorldMatrix;
            TAAMaterial.SetMatrix("_FrameMatrix", mt);

            TAAMaterial.SetFloat("_TemporalFade", TemporalFade);
            TAAMaterial.SetFloat("_MovementBlending", MovementBlending);


            //v0.1
            if (Camera.main != null){
                UniversalAdditionalCameraData UCD = Camera.main.GetComponent<UniversalAdditionalCameraData>();
                if (UCD != null && UCD.renderPostProcessing)
                {
                    TAAMaterial.SetInt("_UseFlipUV", 1);
                }
                else
                {
                    TAAMaterial.SetInt("_UseFlipUV", 0);
                }
            }
            else {
                TAAMaterial.SetInt("_UseFlipUV", UseFlipUV ? 1 : 0);
            }


            //TAAMaterial.SetInt("_UseFlipUV", UseFlipUV ? 1 : 0);

            Blit(cmd, BuiltinRenderTextureType.CurrentActive, temp1, TAAMaterial);

            Blit(cmd, temp1, renderingData.cameraData.renderer.cameraColorTarget);


            //Ping pong
            RenderTexture temp2 = temp;
            temp = temp1;
            temp1 = temp2;

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);


            this.prevViewProjectionMatrix = renderingData.cameraData.camera.nonJitteredProjectionMatrix * renderingData.cameraData.camera.worldToCameraMatrix;

            renderingData.cameraData.camera.ResetProjectionMatrix();
        }

        private void UpdateValuesFromFeature()
        {
            TemporalFade = feature.TemporalFade;
            MovementBlending = feature.MovementBlending;
            HaltonLength = feature.HaltonLength;
            JitterSpread = feature.JitterSpread;
        }

        /// Cleanup any allocated resources that were created during the execution of this render pass.
        public override void FrameCleanup(CommandBuffer cmd)
        {
            base.FrameCleanup(cmd);
            var cam = prevRenderCamera;
            if (!cam) {
                return;
            }
                
            cam.ResetWorldToCameraMatrix();
            cam.ResetProjectionMatrix();
                
            cam.nonJitteredProjectionMatrix = cam.projectionMatrix;
                
            Matrix4x4 p = cam.projectionMatrix;
            float2 jitter = (float2)(2 * Halton2364Seq[Time.frameCount % HaltonLength] - 1) * JitterSpread;
            p.m02 = jitter.x / (float)Screen.width;
            p.m12 = jitter.y / (float)Screen.height;
            cam.projectionMatrix = p;
        }
    }

    TemporalAAPass m_temporalPass;

    public override void Create()
    {
        m_temporalPass = new TemporalAAPass(this);
        m_temporalPass.TemporalFade = TemporalFade;
        m_temporalPass.MovementBlending = MovementBlending;
        m_temporalPass.TAAMaterial = TAAMaterial;
        m_temporalPass.HaltonLength = HaltonLength;
        m_temporalPass.JitterSpread = JitterSpread;
        m_temporalPass.UseFlipUV = this.UseFlipUV;

        // Configures where the render pass should be injected.
        m_temporalPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(m_temporalPass);
    }
}
