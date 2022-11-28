#ifndef UNIVERSAL_PARTICLES_FORWARD_LIT_PASS_INCLUDED
#define UNIVERSAL_PARTICLES_FORWARD_LIT_PASS_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Particles.hlsl"

void InitializeInputData(VaryingsParticle input, half3 normalTS, out InputData inputData)
{
    inputData = (InputData)0;

    inputData.positionWS = input.positionWS.xyz;

#ifdef _NORMALMAP
    half3 viewDirWS = half3(input.normalWS.w, input.tangentWS.w, input.bitangentWS.w);
    inputData.tangentToWorld = half3x3(input.tangentWS.xyz, input.bitangentWS.xyz, input.normalWS.xyz);
    inputData.normalWS = TransformTangentToWorld(normalTS, inputData.tangentToWorld);
#else
    half3 viewDirWS = input.viewDirWS;
    inputData.normalWS = input.normalWS;
#endif

    inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);

#if SHADER_HINT_NICE_QUALITY
    viewDirWS = SafeNormalize(viewDirWS);
#endif

    inputData.viewDirectionWS = viewDirWS;

#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
    inputData.shadowCoord = input.shadowCoord;
#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
    inputData.shadowCoord = TransformWorldToShadowCoord(inputData.positionWS);
#else
    inputData.shadowCoord = float4(0, 0, 0, 0);
#endif

    inputData.fogCoord = InitializeInputDataFog(float4(input.positionWS.xyz, 1.0), input.positionWS.w);
    inputData.vertexLighting = half3(0.0h, 0.0h, 0.0h);
    inputData.bakedGI = SampleSHPixel(input.vertexSH, inputData.normalWS);
    inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.clipPos);
    inputData.shadowMask = half4(1, 1, 1, 1);

    #if defined(DEBUG_DISPLAY) && !defined(PARTICLES_EDITOR_META_PASS)
    inputData.vertexSH = input.vertexSH;
    #endif
}

///////////////////////////////////////////////////////////////////////////////
//                  Vertex and Fragment functions                            //
///////////////////////////////////////////////////////////////////////////////

VaryingsParticle ParticlesLitVertex(AttributesParticle input)
{
    VaryingsParticle output = (VaryingsParticle)0;

    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input, output);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

    VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
    VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);

    half3 viewDirWS = GetWorldSpaceNormalizeViewDir(vertexInput.positionWS);
    half3 vertexLight = VertexLighting(vertexInput.positionWS, half3(normalInput.normalWS));
    half fogFactor = 0.0;
    #if !defined(_FOG_FRAGMENT)
        fogFactor = ComputeFogFactor(vertexInput.positionCS.z);
    #endif

#ifdef _NORMALMAP
    output.normalWS = half4(normalInput.normalWS, viewDirWS.x);
    output.tangentWS = half4(normalInput.tangentWS, viewDirWS.y);
    output.bitangentWS = half4(normalInput.bitangentWS, viewDirWS.z);
#else
    output.normalWS = half3(normalInput.normalWS);
    output.viewDirWS = viewDirWS;
#endif

    OUTPUT_SH(output.normalWS.xyz, output.vertexSH);

    output.positionWS.xyz = vertexInput.positionWS;
    output.positionWS.w = fogFactor;
    output.clipPos = vertexInput.positionCS;
    output.color = GetParticleColor(input.color);

#if defined(_FLIPBOOKBLENDING_ON)
#if defined(UNITY_PARTICLE_INSTANCING_ENABLED)
    GetParticleTexcoords(output.texcoord, output.texcoord2AndBlend, input.texcoords.xyxy, 0.0);
#else
    GetParticleTexcoords(output.texcoord, output.texcoord2AndBlend, input.texcoords, input.texcoordBlend);
#endif
#else
    GetParticleTexcoords(output.texcoord, input.texcoords.xy);
#endif

#if defined(_SOFTPARTICLES_ON) || defined(_FADING_ON) || defined(_DISTORTION_ON)
    output.projectedPosition = vertexInput.positionNDC;
#endif

#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
    output.shadowCoord = GetShadowCoord(vertexInput);
#endif

    return output;
}

half4 ParticlesLitFragment(VaryingsParticle input) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

    ParticleParams particleParams;
    InitParticleParams(input, particleParams);

    SurfaceData surfaceData;
    InitializeParticleLitSurfaceData(particleParams, surfaceData);

    InputData inputData;
    InitializeInputData(input, surfaceData.normalTS, inputData);
    SETUP_DEBUG_TEXTURE_DATA(inputData, input.texcoord, _BaseMap);

    // half4 color = UniversalFragmentPBR(inputData, surfaceData);
    // color.rgb = MixFog(color.rgb, inputData.fogCoord);
    // color.a = OutputAlpha(color.a, _Surface);


    float time = _Time * 10;
    float offset = (time - floor(time))/time;
    float currentTime = (time)*(offset);
    
    float3 WaveParams = float3(10.0, 0.8, 0.1);
    float2 rippleCenter = float2(0.3, 0.25);
    
    float2 texCoord = input.texcoord;
    
    float d = distance(texCoord, rippleCenter);
    // half4 color = UniversalFragmentPBR(inputData, surfaceData);
    half4 color = UniversalFragmentPBR(inputData, surfaceData);
    color.rgb = MixFog(color.rgb, inputData.fogCoord);
    color.a = OutputAlpha(color.a, _Surface);

    // The pixel offset distance based on the input parameters
    float diff = (d - currentTime);
    
    // Only distort the pixels within the parameter distance from the center
    if (abs(d - currentTime) < WaveParams.z)
    {
        
        float scaleDiff = (1.0 - pow(abs(diff * WaveParams.x), WaveParams.y));
        float diffTime = (diff  * scaleDiff);
        
        // The direction of the distortion
        float2 diffTexCoord = normalize(texCoord - rippleCenter);
        
        // Perform the distortion and reduce the effect over time
        texCoord += ((diffTexCoord * diffTime) / (currentTime * d * 10.0));
        color = UniversalFragmentPBR(inputData, surfaceData);
        color.rgb = MixFog(color.rgb, inputData.fogCoord);
        color.a = OutputAlpha(color.a, _Surface);
        
        // Blow out the col and reduce the effect over time
        color += (color * scaleDiff) / (currentTime * d * 10.0);
    } 
    color.a = clamp(color.a, 0, 1);
    return color;
}

#endif // UNIVERSAL_PARTICLES_FORWARD_LIT_PASS_INCLUDED
