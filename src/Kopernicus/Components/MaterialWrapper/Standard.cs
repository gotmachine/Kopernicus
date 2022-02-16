// Material wrapper generated by shader translator tool

using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Rendering;

namespace Kopernicus.Components.MaterialWrapper
{
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Standard : Material
    {
        // Internal property ID tracking object
        protected class Properties
        {
            // Return the shader for this wrapper
            private const String SHADER_NAME = "Standard";

            public static Shader Shader
            {
                get { return Shader.Find(SHADER_NAME); }
            }

            // Color, default = (1.000000,1.000000,1.000000,1.000000)
            public const String COLOR_KEY = "_Color";
            public Int32 ColorId { get; private set; }

            // Albedo, default = "white" { }
            public const String MAIN_TEX_KEY = "_MainTex";
            public Int32 MainTexId { get; private set; }

            // Alpha Cutoff, default = 0.500000
            public const String CUTOFF_KEY = "_Cutoff";
            public Int32 CutoffId { get; private set; }

            // Smoothness, default = 0.500000
            public const String GLOSSINESS_KEY = "_Glossiness";
            public Int32 GlossinessId { get; private set; }

            // Smoothness Scale, default = 1.000000
            public const String GLOSS_MAP_SCALE_KEY = "_GlossMapScale";
            public Int32 GlossMapScaleId { get; private set; }

            // Smoothness texture channel, default = 0.000000
            public const String SMOOTHNESS_TEXTURE_CHANNEL_KEY = "_SmoothnessTextureChannel";
            public Int32 SmoothnessTextureChannelId { get; private set; }

            // Metallic, default = 0.000000
            public const String METALLIC_KEY = "_Metallic";
            public Int32 MetallicId { get; private set; }

            // Metallic, default = "white" { }
            public const String METALLIC_GLOSS_MAP_KEY = "_MetallicGlossMap";
            public Int32 MetallicGlossMapId { get; private set; }

            // Specular Highlights, default = 1.000000
            public const String SPECULAR_HIGHLIGHTS_KEY = "_SpecularHighlights";
            public Int32 SpecularHighlightsId { get; private set; }

            // Glossy Reflections, default = 1.000000
            public const String GLOSSY_REFLECTIONS_KEY = "_GlossyReflections";
            public Int32 GlossyReflectionsId { get; private set; }

            // Scale, default = 1.000000
            public const String BUMP_SCALE_KEY = "_BumpScale";
            public Int32 BumpScaleId { get; private set; }

            // Normal Map, default = "bump" { }
            public const String BUMP_MAP_KEY = "_BumpMap";
            public Int32 BumpMapId { get; private set; }

            // Height Scale, default = 0.020000
            public const String PARALLAX_KEY = "_Parallax";
            public Int32 ParallaxId { get; private set; }

            // Height Map, default = "black" { }
            public const String PARALLAX_MAP_KEY = "_ParallaxMap";
            public Int32 ParallaxMapId { get; private set; }

            // Strength, default = 1.000000
            public const String OCCLUSION_STRENGTH_KEY = "_OcclusionStrength";
            public Int32 OcclusionStrengthId { get; private set; }

            // Occlusion, default = "white" { }
            public const String OCCLUSION_MAP_KEY = "_OcclusionMap";
            public Int32 OcclusionMapId { get; private set; }

            // Color, default = (0.000000,0.000000,0.000000,1.000000)
            public const String EMISSION_COLOR_KEY = "_EmissionColor";
            public Int32 EmissionColorId { get; private set; }

            // Emission, default = "white" { }
            public const String EMISSION_MAP_KEY = "_EmissionMap";
            public Int32 EmissionMapId { get; private set; }

            // Detail Mask, default = "white" { }
            public const String DETAIL_MASK_KEY = "_DetailMask";
            public Int32 DetailMaskId { get; private set; }

            // Detail Albedo x2, default = "grey" { }
            public const String DETAIL_ALBEDO_MAP_KEY = "_DetailAlbedoMap";
            public Int32 DetailAlbedoMapId { get; private set; }

            // Normal Map, default = "bump" { }
            public const String DETAIL_NORMAL_MAP_KEY = "_DetailNormalMap";
            public Int32 DetailNormalMapId { get; private set; }

            // UV Set for secondary textures, default = 0.000000
            public const String UV_SEC_KEY = "_UVSec";
            public Int32 UvSecId { get; private set; }

            // __mode, default = 0.000000
            public const String MODE_KEY = "_Mode";
            public Int32 ModeId { get; private set; }

            // __src, default = 1.000000
            public const String SRC_BLEND_KEY = "_SrcBlend";
            public Int32 SrcBlendId { get; private set; }

            // __dst, default = 0.000000
            public const String DST_BLEND_KEY = "_DstBlend";
            public Int32 DstBlendId { get; private set; }

            // __zw, default = 1.000000
            public const String Z_WRITE_KEY = "_ZWrite";
            public Int32 ZWriteId { get; private set; }

            // Singleton instance
            private static Properties _singleton;

            public static Properties Instance
            {
                get
                {
                    // Construct the singleton if it does not exist
                    return _singleton ?? (_singleton = new Properties());
                }
            }

            private Properties()
            {
                ColorId = Shader.PropertyToID(COLOR_KEY);
                MainTexId = Shader.PropertyToID(MAIN_TEX_KEY);
                CutoffId = Shader.PropertyToID(CUTOFF_KEY);
                GlossinessId = Shader.PropertyToID(GLOSSINESS_KEY);
                GlossMapScaleId = Shader.PropertyToID(GLOSS_MAP_SCALE_KEY);
                SmoothnessTextureChannelId = Shader.PropertyToID(SMOOTHNESS_TEXTURE_CHANNEL_KEY);
                MetallicId = Shader.PropertyToID(METALLIC_KEY);
                MetallicGlossMapId = Shader.PropertyToID(METALLIC_GLOSS_MAP_KEY);
                SpecularHighlightsId = Shader.PropertyToID(SPECULAR_HIGHLIGHTS_KEY);
                GlossyReflectionsId = Shader.PropertyToID(GLOSSY_REFLECTIONS_KEY);
                BumpScaleId = Shader.PropertyToID(BUMP_SCALE_KEY);
                BumpMapId = Shader.PropertyToID(BUMP_MAP_KEY);
                ParallaxId = Shader.PropertyToID(PARALLAX_KEY);
                ParallaxMapId = Shader.PropertyToID(PARALLAX_MAP_KEY);
                OcclusionStrengthId = Shader.PropertyToID(OCCLUSION_STRENGTH_KEY);
                OcclusionMapId = Shader.PropertyToID(OCCLUSION_MAP_KEY);
                EmissionColorId = Shader.PropertyToID(EMISSION_COLOR_KEY);
                EmissionMapId = Shader.PropertyToID(EMISSION_MAP_KEY);
                DetailMaskId = Shader.PropertyToID(DETAIL_MASK_KEY);
                DetailAlbedoMapId = Shader.PropertyToID(DETAIL_ALBEDO_MAP_KEY);
                DetailNormalMapId = Shader.PropertyToID(DETAIL_NORMAL_MAP_KEY);
                UvSecId = Shader.PropertyToID(UV_SEC_KEY);
                ModeId = Shader.PropertyToID(MODE_KEY);
                SrcBlendId = Shader.PropertyToID(SRC_BLEND_KEY);
                DstBlendId = Shader.PropertyToID(DST_BLEND_KEY);
                ZWriteId = Shader.PropertyToID(Z_WRITE_KEY);
            }
        }

        public enum TextureChannel
        {
            MetallicAlpha = 0,
            AlbedoAlpha = 1
        }

        public enum UvSet
        {
            Uv0 = 0,
            Uv1 = 1
        }

        public enum BlendMode
        {
            Opaque,
            Cutout,
            Fade,
            Transparent
        }

        // Is some random material this material
        public static Boolean UsesSameShader(Material m)
        {
            if (m == null)
            {
                return false;
            }

            return m.shader.name == Properties.Shader.name;
        }

        // Color, default = (1.000000,1.000000,1.000000,1.000000)
        public Color Color
        {
            get { return GetColor(Properties.Instance.ColorId); }
            set { SetColor(Properties.Instance.ColorId, value); }
        }

        // Albedo, default = "white" { }
        public Texture2D MainTex
        {
            get { return GetTexture(Properties.Instance.MainTexId) as Texture2D; }
            set { SetTexture(Properties.Instance.MainTexId, value); }
        }

        public Vector2 MainTexScale
        {
            get { return GetTextureScale(Properties.Instance.MainTexId); }
            set { SetTextureScale(Properties.Instance.MainTexId, value); }
        }

        public Vector2 MainTexOffset
        {
            get { return GetTextureOffset(Properties.Instance.MainTexId); }
            set { SetTextureOffset(Properties.Instance.MainTexId, value); }
        }

        // Alpha Cutoff, default = 0.500000
        public Single Cutoff
        {
            get { return GetFloat(Properties.Instance.CutoffId); }
            set { SetFloat(Properties.Instance.CutoffId, Mathf.Clamp(value, 0.000000f, 1.000000f)); }
        }

        // Smoothness, default = 0.500000
        public Single Glossiness
        {
            get { return GetFloat(Properties.Instance.GlossinessId); }
            set { SetFloat(Properties.Instance.GlossinessId, Mathf.Clamp(value, 0.000000f, 1.000000f)); }
        }

        // Smoothness Scale, default = 1.000000
        public Single GlossMapScale
        {
            get { return GetFloat(Properties.Instance.GlossMapScaleId); }
            set { SetFloat(Properties.Instance.GlossMapScaleId, Mathf.Clamp(value, 0.000000f, 1.000000f)); }
        }

        // Smoothness texture channel, default = 0.000000
        public TextureChannel SmoothnessTextureChannel
        {
            get { return (TextureChannel)GetFloat(Properties.Instance.SmoothnessTextureChannelId); }
            set { SetFloat(Properties.Instance.SmoothnessTextureChannelId, (Int32)value); }
        }

        // Metallic, default = 0.000000
        public Single Metallic
        {
            get { return GetFloat(Properties.Instance.MetallicId); }
            set { SetFloat(Properties.Instance.MetallicId, Mathf.Clamp(value, 0.000000f, 1.000000f)); }
        }

        // Metallic, default = "white" { }
        public Texture2D MetallicGlossMap
        {
            get { return GetTexture(Properties.Instance.MetallicGlossMapId) as Texture2D; }
            set
            {
                SetTexture(Properties.Instance.MetallicGlossMapId, value);
                SetupMaterial();
            }
        }

        public Vector2 MetallicGlossMapScale
        {
            get { return GetTextureScale(Properties.Instance.MetallicGlossMapId); }
            set { SetTextureScale(Properties.Instance.MetallicGlossMapId, value); }
        }

        public Vector2 MetallicGlossMapOffset
        {
            get { return GetTextureOffset(Properties.Instance.MetallicGlossMapId); }
            set { SetTextureOffset(Properties.Instance.MetallicGlossMapId, value); }
        }

        // Specular Highlights, default = 1.000000
        public Boolean SpecularHighlights
        {
            get { return GetFloat(Properties.Instance.SpecularHighlightsId) > 0f; }
            set { SetFloat(Properties.Instance.SpecularHighlightsId, value ? 1f : 0f); }
        }

        // Glossy Reflections, default = 1.000000
        public Boolean GlossyReflections
        {
            get { return GetFloat(Properties.Instance.GlossyReflectionsId) > 0f; }
            set { SetFloat(Properties.Instance.GlossyReflectionsId, value ? 1f : 0f); }
        }

        // Scale, default = 1.000000
        public Single BumpScale
        {
            get { return GetFloat(Properties.Instance.BumpScaleId); }
            set { SetFloat(Properties.Instance.BumpScaleId, value); }
        }

        // Normal Map, default = "bump" { }
        public Texture2D BumpMap
        {
            get { return GetTexture(Properties.Instance.BumpMapId) as Texture2D; }
            set
            {
                SetTexture(Properties.Instance.BumpMapId, value);
                SetupMaterial();
            }
        }

        public Vector2 BumpMapScale
        {
            get { return GetTextureScale(Properties.Instance.BumpMapId); }
            set { SetTextureScale(Properties.Instance.BumpMapId, value); }
        }

        public Vector2 BumpMapOffset
        {
            get { return GetTextureOffset(Properties.Instance.BumpMapId); }
            set { SetTextureOffset(Properties.Instance.BumpMapId, value); }
        }

        // Height Scale, default = 0.020000
        public Single Parallax
        {
            get { return GetFloat(Properties.Instance.ParallaxId); }
            set { SetFloat(Properties.Instance.ParallaxId, Mathf.Clamp(value, 0.005000f, 0.080000f)); }
        }

        // Height Map, default = "black" { }
        public Texture2D ParallaxMap
        {
            get { return GetTexture(Properties.Instance.ParallaxMapId) as Texture2D; }
            set
            {
                SetTexture(Properties.Instance.ParallaxMapId, value);
                SetupMaterial();
            }
        }

        public Vector2 ParallaxMapScale
        {
            get { return GetTextureScale(Properties.Instance.ParallaxMapId); }
            set { SetTextureScale(Properties.Instance.ParallaxMapId, value); }
        }

        public Vector2 ParallaxMapOffset
        {
            get { return GetTextureOffset(Properties.Instance.ParallaxMapId); }
            set { SetTextureOffset(Properties.Instance.ParallaxMapId, value); }
        }

        // Strength, default = 1.000000
        public Single OcclusionStrength
        {
            get { return GetFloat(Properties.Instance.OcclusionStrengthId); }
            set { SetFloat(Properties.Instance.OcclusionStrengthId, Mathf.Clamp(value, 0.000000f, 1.000000f)); }
        }

        // Occlusion, default = "white" { }
        public Texture2D OcclusionMap
        {
            get { return GetTexture(Properties.Instance.OcclusionMapId) as Texture2D; }
            set { SetTexture(Properties.Instance.OcclusionMapId, value); }
        }

        public Vector2 OcclusionMapScale
        {
            get { return GetTextureScale(Properties.Instance.OcclusionMapId); }
            set { SetTextureScale(Properties.Instance.OcclusionMapId, value); }
        }

        public Vector2 OcclusionMapOffset
        {
            get { return GetTextureOffset(Properties.Instance.OcclusionMapId); }
            set { SetTextureOffset(Properties.Instance.OcclusionMapId, value); }
        }

        // Color, default = (0.000000,0.000000,0.000000,1.000000)
        public Color EmissionColor
        {
            get { return GetColor(Properties.Instance.EmissionColorId); }
            set { SetColor(Properties.Instance.EmissionColorId, value); }
        }

        // Emission, default = "white" { }
        public Texture2D EmissionMap
        {
            get { return GetTexture(Properties.Instance.EmissionMapId) as Texture2D; }
            set
            {
                SetTexture(Properties.Instance.EmissionMapId, value);
                SetupMaterial();
            }
        }

        public Vector2 EmissionMapScale
        {
            get { return GetTextureScale(Properties.Instance.EmissionMapId); }
            set { SetTextureScale(Properties.Instance.EmissionMapId, value); }
        }

        public Vector2 EmissionMapOffset
        {
            get { return GetTextureOffset(Properties.Instance.EmissionMapId); }
            set { SetTextureOffset(Properties.Instance.EmissionMapId, value); }
        }

        // Detail Mask, default = "white" { }
        public Texture2D DetailMask
        {
            get { return GetTexture(Properties.Instance.DetailMaskId) as Texture2D; }
            set { SetTexture(Properties.Instance.DetailMaskId, value); }
        }

        public Vector2 DetailMaskScale
        {
            get { return GetTextureScale(Properties.Instance.DetailMaskId); }
            set { SetTextureScale(Properties.Instance.DetailMaskId, value); }
        }

        public Vector2 DetailMaskOffset
        {
            get { return GetTextureOffset(Properties.Instance.DetailMaskId); }
            set { SetTextureOffset(Properties.Instance.DetailMaskId, value); }
        }

        // Detail Albedo x2, default = "grey" { }
        public Texture2D DetailAlbedoMap
        {
            get { return GetTexture(Properties.Instance.DetailAlbedoMapId) as Texture2D; }
            set
            {
                SetTexture(Properties.Instance.DetailAlbedoMapId, value);
                SetupMaterial();
            }
        }

        public Vector2 DetailAlbedoMapScale
        {
            get { return GetTextureScale(Properties.Instance.DetailAlbedoMapId); }
            set { SetTextureScale(Properties.Instance.DetailAlbedoMapId, value); }
        }

        public Vector2 DetailAlbedoMapOffset
        {
            get { return GetTextureOffset(Properties.Instance.DetailAlbedoMapId); }
            set { SetTextureOffset(Properties.Instance.DetailAlbedoMapId, value); }
        }

        // Normal Map, default = "bump" { }
        public Texture2D DetailNormalMap
        {
            get { return GetTexture(Properties.Instance.DetailNormalMapId) as Texture2D; }
            set
            {
                SetTexture(Properties.Instance.DetailNormalMapId, value);
                SetupMaterial();
            }
        }

        // Scale, default = 1.000000
        public Vector2 DetailNormalMapScale
        {
            get { return GetTextureScale(Properties.Instance.DetailNormalMapId); }
            set { SetTextureScale(Properties.Instance.DetailNormalMapId, value); }
        }

        public Vector2 DetailNormalMapOffset
        {
            get { return GetTextureOffset(Properties.Instance.DetailNormalMapId); }
            set { SetTextureOffset(Properties.Instance.DetailNormalMapId, value); }
        }

        // UV Set for secondary textures, default = 0.000000
        public UvSet UvSec
        {
            get { return (UvSet)GetFloat(Properties.Instance.UvSecId); }
            set { SetFloat(Properties.Instance.UvSecId, (Int32)value); }
        }

        // __mode, default = 0.000000
        public BlendMode Mode
        {
            get { return (BlendMode)GetFloat(Properties.Instance.ModeId); }
            set
            {
                SetFloat(Properties.Instance.ModeId, (Int32)value);
                SetupMaterial();
            }
        }

        // __src, default = 1.000000
        public Single SrcBlend
        {
            get { return GetFloat(Properties.Instance.SrcBlendId); }
            set { SetFloat(Properties.Instance.SrcBlendId, value); }
        }

        // __dst, default = 0.000000
        public Single DstBlend
        {
            get { return GetFloat(Properties.Instance.DstBlendId); }
            set { SetFloat(Properties.Instance.DstBlendId, value); }
        }

        // __zw, default = 1.000000
        public Single ZWrite
        {
            get { return GetFloat(Properties.Instance.ZWriteId); }
            set { SetFloat(Properties.Instance.ZWriteId, value); }
        }

        private void SetupMaterial()
        {
            if (Mode == BlendMode.Opaque)
            {
                SetOverrideTag("RenderType", "");
                DisableKeyword("_ALPHATEST_ON");
                DisableKeyword("_ALPHABLEND_ON");
                DisableKeyword("_ALPHAPREMULTIPLY_ON");
                SrcBlend = (Int32)UnityEngine.Rendering.BlendMode.One;
                DstBlend = (Int32)UnityEngine.Rendering.BlendMode.Zero;
                ZWrite = 1;
                renderQueue = -1;
            }

            if (Mode == BlendMode.Cutout)
            {
                SetOverrideTag("RenderType", "TransparentCutout");
                EnableKeyword("_ALPHATEST_ON");
                DisableKeyword("_ALPHABLEND_ON");
                DisableKeyword("_ALPHAPREMULTIPLY_ON");
                SrcBlend = (Int32)UnityEngine.Rendering.BlendMode.One;
                DstBlend = (Int32)UnityEngine.Rendering.BlendMode.Zero;
                ZWrite = 1;
                renderQueue = (Int32)RenderQueue.AlphaTest;
            }

            if (Mode == BlendMode.Fade)
            {
                SetOverrideTag("RenderType", "Transparent");
                DisableKeyword("_ALPHATEST_ON");
                EnableKeyword("_ALPHABLEND_ON");
                DisableKeyword("_ALPHAPREMULTIPLY_ON");
                SrcBlend = (Int32)UnityEngine.Rendering.BlendMode.SrcAlpha;
                DstBlend = (Int32)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;
                ZWrite = 0;
                renderQueue = (Int32)RenderQueue.Transparent;
            }

            if (Mode == BlendMode.Transparent)
            {
                SetOverrideTag("RenderType", "Transparent");
                DisableKeyword("_ALPHATEST_ON");
                DisableKeyword("_ALPHABLEND_ON");
                EnableKeyword("_ALPHAPREMULTIPLY_ON");
                SrcBlend = (Int32)UnityEngine.Rendering.BlendMode.One;
                DstBlend = (Int32)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;
                ZWrite = 0;
                renderQueue = (Int32)RenderQueue.Transparent;
            }

            SetKeyword("_NORMALMAP", BumpMap || DetailNormalMap);
            if (MetallicGlossMap != null)
            {
                SetKeyword("_METALLICGLOSSMAP", MetallicGlossMap);
            }

            SetKeyword("_PARALLAXMAP", ParallaxMap);
            SetKeyword("_DETAIL_MULX2", DetailAlbedoMap || DetailNormalMap);
            SetKeyword("_EMISSION", EmissionMap);

            if (HasProperty("_SmoothnessTextureChannel"))
            {
                SetKeyword("_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A",
                    SmoothnessTextureChannel == TextureChannel.AlbedoAlpha);
            }
        }

        private void SetKeyword(String keyword, Boolean state)
        {
            if (state)
            {
                EnableKeyword(keyword);
            }
            else
            {
                DisableKeyword(keyword);
            }
        }

        public Standard() : base(Properties.Shader)
        {
            SetupMaterial();
        }

        [Obsolete("Creating materials from shader source String is no longer supported. Use Shader assets instead.")]
        public Standard(String contents) : base(contents)
        {
            shader = Properties.Shader;
            SetupMaterial();
        }

        public Standard(Material material) : base(material)
        {
            // Throw exception if this material was not the proper material
            if (material.shader.name != Properties.Shader.name)
            {
                throw new InvalidOperationException("Type Mismatch: Standard shader required");
            }

            SetupMaterial();
        }

    }
}
