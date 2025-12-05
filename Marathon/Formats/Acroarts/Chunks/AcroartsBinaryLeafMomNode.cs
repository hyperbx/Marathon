using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Chunks
{
    public enum MomBase : uint
    {
        _acmTranslateNormal = 0x0,
        _acmTranslateAdd = 0x1,
        _acmTranslateAccel = 0x2,
        _acmTranslateRandomNormal = 0x3,
        _acmTranslateRandomAdd = 0x4,
        _acmTranslateRandomAccel = 0x5,
        _acmTranslateSin = 0x6,
        _acmTranslateRandomSin = 0x7,
        _acmTranslateGoal = 0x8,
        _acmTranslateSpline = 0x9,
        _acmTranslateAddGoal = 0xA,
        _acmTranslateRandomGoal = 0xB,
        _acmTranslateWrap = 0x5C11,
        _acmRotateNormal = 0x14,
        _acmRotateAdd = 0x15,
        _acmRotateAccel = 0x16,
        _acmRotateRandomNormal = 0x17,
        _acmRotateRandomAdd = 0x18,
        _acmRotateRandomAccel = 0x19,
        _acmRotateSin = 0x1A,
        _acmRotateRandomSin = 0x1B,
        _acmRotateGoal = 0x1C,
        _acmRotateRandomGoal = 0x1D,
        _acmRotateAddGoal = 0x1E,
        _acmRotateErase = 0x1F,
        _acmScaleAdd = 0x29,
        _acmScaleAccel = 0x2A,
        _acmScaleAddGoal = 0x32,
        _acmScaleGoal = 0x30,
        _acmScaleNormal = 0x28,
        _acmScaleRandomAccel = 0x2D,
        _acmScaleRandomAdd = 0x2C,
        _acmScaleRandomGoal = 0x31,
        _acmScaleRandomNormal = 0x2B,
        _acmScaleRandomSin = 0x2F,
        _acmScaleSin = 0x2E,
        _acmPlaceFanShaped = 0x3C,
        _acmPlaceLineShaped = 0x3D,
        _acmPlaceSelectLocation = 0x3E,
        _acmPlacePolygonShaped = 0x3F,
        _acmCameraPosition = 0x1C2,
        _acmCameraTarget = 0x1C3,
        _acmCameraRoll = 0x1C4,
        _acmTurnCamera = 0x96,
        _acmCameraContactLens = 0x264,
        _acmUvScrollByCameraDirection = 0x12C,
        _acmPointLight = 0x17C,
        _acmDirectionalLight = 0x17D,
        _acmAmbientLight = 0x17E,
        _acmDetachCoordinate = 0x25A,
        _acmUvScrollSin = 0x12E,
        _acmUvScrollNormal = 0x12D,
        _acmUvSet = 0x12F,
        _acmMaterialColorNormal = 0xC8,
        _acmMaterialColorSin = 0xC9,
        _acmMaterialColorRandomNormal = 0xCA,
        _acmMaterialColorRandomSin = 0xCB,
        _acmMaterialColorGoal = 0xCC,
        _acmMaterialColorRandomGoal = 0xCD,
        _acmMaterialColorSetByCamDist = 0xCE,
        _acmMaterialColorSetByCamDire = 0xCF,
        _acmMotionFrameSet = 0x1F6,
        _acmMotionSet = 0x1F4,
        _acmModelMotionSet = 0x1F5,
        _acmParticleBillboardEx = 0x65,
        _acmParticleBillboardPV = 0x67,
        _acmParticleBillboardND = 0x6A,
        _acmBlurBelt = 0x78,
        _acmSparklingTail = 0x69,
        _acmBillboardTail = 0x7A,
        _acmTexScreen = 0x263,
        _acmLineEx = 0x66,
        _acmTextureSurfaceAnimation = 0xFA,
        _acmTextureSurfaceAnimationEasy = 0xFD,
        _acmTextureSet = 0x100,
        _acmTexturePatternAnimation = 0xFB,
        _acmTexturePatternAnimationEasy = 0xFE,
        _acmTextureListAnimation = 0xFF,
        _acmScreenPerspective = 0x226,
        _acmTimeRate = 0x28A,
        _acmThunder = 0x258,
        _acmLensFlare = 0x265,
        _acmSendParamAccel = 0x2BE,
        _acmSendParamAdd = 0x2BD,
        _acmSendParamAddGoal = 0x2C5,
        _acmSendParamEqualSpace = 0x2C8,
        _acmSendParamGoal = 0x2C4,
        _acmSendParamNormal = 0x2BC,
        _acmSendParamRandomAccel = 0x2C2,
        _acmSendParamRandomAdd = 0x2C1,
        _acmSendParamRandomGoal = 0x2C7,
        _acmSendParamRandomNormal = 0x2C0,
        _acmSendParamRandomSin = 0x2C3,
        _acmSendParamSin = 0x2BF,
        _acmSendParamSpline = 0x2C6,
        _acmModelDrawMultitude = 0x269,
        _acmSoundPlay = 0x320, //+
        _acmSound3DPlay = 0x321,
        _acmParticlePlay = 0x32A,
        _acmParticleSet = 0x32B,
        _acmCellSpriteSceneSet = 0x334,
        _acmSubtitle = 0x33E,
        _acmClipPlane = 0x348,
        _acmModelJoin = 0xE7,
        _acmShadowOn = 0xE8,
        _acmFilterClassicBlur = 0x352,
        _acmFilterColorCorrection = 0x353,
        _acmFilterDepthOfField = 0x354,
        _acmSceneBloom = 0x366
    };

    public class AcroartsBinaryLeafMomNode : INode
    {
        public List<byte> Data { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MomBase Type { get; set; }
        public List<byte> Data2 { get; set; }
        public IACM ACM { get; set; }

        public AcroartsBinaryLeafMomNode() { }

        public AcroartsBinaryLeafMomNode(BINAReader in_reader)
        {
            Read(in_reader);
        }

        public void Read(BINAReader in_reader)
        {
            var Position = in_reader.Position;
            var DefaultOffset = 0x50; // Should Use Offset Of Binary Archive

            Data = in_reader.ReadArray<byte>(0x4).ToList();
            Type = in_reader.Read<MomBase>();
            Data2 = in_reader.ReadArray<byte>(0xC).ToList();
            var count = in_reader.Read<uint>(); // Count not always match, it usually depends on Type
            var offset = in_reader.Read<uint>();    
            in_reader.JumpTo(offset + DefaultOffset);
            switch (Type)
            {
                case MomBase._acmParticlePlay:
                    ACM = new _acmParticlePlay(in_reader, count);
                    break;
                case MomBase._acmBlurBelt:
                    ACM = new _acmBlurBelt(in_reader, count);
                    break;
                case MomBase._acmRotateNormal:
                    ACM = new _acmRotateNormal(in_reader, count);
                    break;
                case MomBase._acmPlaceFanShaped:
                    ACM = new _acmPlaceFanShaped(in_reader, count);
                    break;
                default:
                    Logger.Warning($"{Type.ToString()} [Count {count}] not implemented yet");
                    break;
            }
        }

        public void Write(BINAWriter in_writer)
        {
            // TODO
        }
    }
}
