namespace Marathon.Formats.Save
{
    public enum SonicNextFlags
    {
        // Episode unlocks.
        Unlock_Episode_Silver = 50,
        Unlock_Episode_Shadow = 51,

        // Multiplayer unlocks.
        Unlock_Battle = 54,

        // Unknown flags.
        Unknown_TestPlayerMission = 914,

        // Sonic's cleared town missions.
        Clear_Town_Sonic_Mission1 = 1001,
        Clear_Town_Sonic_Mission2 = 1003,
        Clear_Town_Sonic_Mission3 = 1004,
        Clear_Town_Sonic_Mission4 = 1005,
        Clear_Town_Sonic_Mission13 = 1008,
        Clear_Town_Sonic_Mission17 = 1010,
        Clear_Town_Sonic_Mission5 = 1011,
        Clear_Town_Sonic_Mission9 = 1012,
        Clear_Town_Sonic_Mission14 = 1013,
        Clear_Town_Sonic_Mission15 = 1014,
        Clear_Town_Sonic_Mission6 = 1018,
        Clear_Town_Sonic_Mission10 = 1019,
        Clear_Town_Sonic_Mission7 = 1024,
        Clear_Town_Sonic_Mission11 = 1025,
        Clear_Town_Sonic_Mission12 = 1027,
        Clear_Town_Sonic_Mission16 = 1029,
        Clear_Town_Sonic_Mission18 = 1030,
        Clear_Town_Sonic_Mission20 = 1031,
        Clear_Town_Sonic_Mission19 = 1032,
        Clear_Town_Sonic_Mission8 = 1033,

        // Shadow's cleared town missions.
        Clear_Town_Shadow_Mission4 = 1103,
        Clear_Town_Shadow_Mission1 = 1104,
        Clear_Town_Shadow_Mission7 = 1107,
        Clear_Town_Shadow_Mission8 = 1108,
        Clear_Town_Shadow_Mission9 = 1109,
        Clear_Town_Shadow_Mission10 = 1112,
        Clear_Town_Shadow_Mission12 = 1114,
        Clear_Town_Shadow_Mission11 = 1117,
        Clear_Town_Shadow_Mission2 = 1118,
        Clear_Town_Shadow_Mission13 = 1119,
        Clear_Town_Shadow_Mission5 = 1126,
        Clear_Town_Shadow_Mission6 = 1128,
        Clear_Town_Shadow_Mission3 = 1130,
        Clear_Town_Shadow_Mission15 = 1131,
        Clear_Town_Shadow_Mission14 = 1132,

        // Silver's cleared town missions.
        Clear_Town_Silver_Mission1 = 1201,
        Clear_Town_Silver_Mission2 = 1203,
        Clear_Town_Silver_Mission14 = 1208,
        Clear_Town_Silver_Mission3 = 1211,
        Clear_Town_Silver_Mission4 = 1212,
        Clear_Town_Silver_Mission5 = 1214,
        Clear_Town_Silver_Mission15 = 1215,
        Clear_Town_Silver_Mission6 = 1216,
        Clear_Town_Silver_Mission7 = 1218,
        Clear_Town_Silver_Mission10 = 1219,
        Clear_Town_Silver_Mission12 = 1220,
        Clear_Town_Silver_Mission8 = 1221,
        Clear_Town_Silver_Mission13 = 1226,
        Clear_Town_Silver_Mission17 = 1232,
        Clear_Town_Silver_Mission16 = 1237,
        Clear_Town_Silver_Mission18 = 1238,
        Clear_Town_Silver_Mission9 = 1239,
        Clear_Town_Silver_Mission11 = 1240,

        // Sonic's cleared stages.
        Clear_Stage_Sonic_WaveOcean = 4000,
        Clear_Stage_Sonic_WaveOcean_Hard = 4001,
        Clear_Stage_Sonic_DustyDesert = 4005,
        Clear_Stage_Sonic_DustyDesert_Hard = 4006,
        Clear_Stage_Sonic_WhiteAcropolis = 4010,
        Clear_Stage_Sonic_WhiteAcropolis_Hard = 4011,
        Clear_Stage_Sonic_CrisisCity = 4015,
        Clear_Stage_Sonic_CrisisCity_Hard = 4016,
        Clear_Stage_Sonic_FlameCore = 4020,
        Clear_Stage_Sonic_FlameCore_Hard = 4021,
        Clear_Stage_Sonic_RadicalTrain = 4025,
        Clear_Stage_Sonic_RadicalTrain_Hard = 4026,
        Clear_Stage_Sonic_TropicalJungle = 4030,
        Clear_Stage_Sonic_TropicalJungle_Hard = 4031,
        Clear_Stage_Sonic_KingdomValley = 4035,
        Clear_Stage_Sonic_KingdomValley_Hard = 4036,
        Clear_Stage_Sonic_AquaticBase = 4040,
        Clear_Stage_Sonic_AquaticBase_Hard = 4041,
        Clear_Stage_Tails_WaveOcean = 4045,

        // Sonic's cleared bosses.
        Clear_Boss_Sonic_EggCerberus = 4046,
        Clear_Boss_Sonic_Silver = 4047,
        Clear_Boss_Sonic_Iblis2 = 4048,
        Clear_Boss_Sonic_EggGenesis = 4049,
        Clear_Boss_Sonic_EggWyvern = 4050,

        // Shadow's cleared stages.
        Clear_Stage_Shadow_WhiteAcropolis = 4051,
        Clear_Stage_Shadow_WhiteAcropolis_Hard = 4052,
        Clear_Stage_Shadow_KingdomValley = 4056,
        Clear_Stage_Shadow_KingdomValley_Hard = 4057,
        Clear_Stage_Shadow_CrisisCity = 4061,
        Clear_Stage_Shadow_CrisisCity_Hard = 4062,
        Clear_Stage_Shadow_FlameCore = 4066,
        Clear_Stage_Shadow_FlameCore_Hard = 4067,
        Clear_Stage_Shadow_RadicalTrain = 4071,
        Clear_Stage_Shadow_RadicalTrain_Hard = 4072,
        Clear_Stage_Shadow_AquaticBase = 4076,
        Clear_Stage_Shadow_AquaticBase_Hard = 4077,
        Clear_Stage_Shadow_WaveOcean = 4081,
        Clear_Stage_Shadow_WaveOcean_Hard = 4082,
        Clear_Stage_Shadow_DustyDesert = 4086,
        Clear_Stage_Shadow_DustyDesert_Hard = 4087,
        Clear_Stage_Rouge_TropicalJungle = 4091,

        // Shadow's cleared bosses.
        Clear_Boss_Shadow_EggCerberus = 4092,
        Clear_Boss_Shadow_Iblis2 = 4093,
        Clear_Boss_Shadow_Mephiles1 = 4094,
        Clear_Boss_Shadow_Silver = 4095,
        Clear_Boss_Shadow_Mephiles2 = 4096,

        // Silver's cleared stages.
        Clear_Stage_Silver_CrisisCity = 4097,
        Clear_Stage_Silver_CrisisCity_Hard = 4098,
        Clear_Stage_Silver_TropicalJungle = 4102,
        Clear_Stage_Silver_TropicalJungle_Hard = 4103,
        Clear_Stage_Silver_DustyDesert = 4107,
        Clear_Stage_Silver_DustyDesert_Hard = 4108,
        Clear_Stage_Silver_WhiteAcropolis = 4112,
        Clear_Stage_Silver_WhiteAcropolis_Hard = 4113,
        Clear_Stage_Silver_RadicalTrain = 4117,
        Clear_Stage_Silver_RadicalTrain_Hard = 4118,
        Clear_Stage_Silver_AquaticBase = 4122,
        Clear_Stage_Silver_AquaticBase_Hard = 4123,
        Clear_Stage_Silver_KingdomValley = 4127,
        Clear_Stage_Silver_KingdomValley_Hard = 4128,
        Clear_Stage_Silver_FlameCore = 4132,
        Clear_Stage_Silver_FlameCore_Hard = 4133,
        Clear_Stage_Blaze_WaveOcean = 4137,

        // Silver's cleared bosses.
        Clear_Boss_Silver_Iblis1 = 4138,
        Clear_Boss_Silver_Sonic = 4139,
        Clear_Boss_Silver_EggGenesis = 4140,
        Clear_Boss_Silver_Shadow = 4141,
        Clear_Boss_Silver_Iblis3 = 4142,

        // Last cleared stage.
        Clear_Stage_Last_EndOfTheWorld = 4143,
        
        // Last cleared boss.
        Clear_Boss_Last_Solaris = 4144,

        // Audio and theater room unlocks.
        Unlock_Multimedia_Sonic = 4145,
        Unlock_Multimedia_Shadow = 4146,
        Unlock_Multimedia_Silver = 4147,
        Unlock_Multimedia_Last = 4148,
        Unlock_AudioRoom = 4155,

        // Gold medal results.
        GoldMedal_Sonic_ClearAllActMissionsWithRankS = 4152,
        GoldMedal_Shadow_ClearAllActMissionsWithRankS = 4153,
        GoldMedal_Silver_ClearAllActMissionsWithRankS = 4154,
        GoldMedal_Sonic_ClearAllTownMissionsWithRankS = 4159,
        GoldMedal_Shadow_ClearAllTownMissionsWithRankS = 4160,
        GoldMedal_Silver_ClearAllTownMissionsWithRankS = 4161,

        /* Silver medal results.
           TODO: possibly unused? */
        SilverMedal_CollectAll1 = 5409,
        SilverMedal_CollectAll2 = 5410,
        SilverMedal_CollectAll3 = 5411,

        // Sonic's upgrades.
        Upgrade_Sonic_LightDash = 6000,
        Upgrade_Sonic_Sliding = 6001,
        Upgrade_Sonic_BoundJump = 6002,
        Upgrade_Sonic_HomingSmash = 6003,
        Upgrade_Sonic_GreenGem = 6004,
        Upgrade_Sonic_RedGem = 6005,
        Upgrade_Sonic_BlueGem = 6006,
        Upgrade_Sonic_WhiteGem = 6007,
        Upgrade_Sonic_SkyGem = 6008,
        Upgrade_Sonic_YellowGem = 6009,
        Upgrade_Sonic_PurpleGem = 6010,
        Upgrade_Sonic_RainbowGem = 6011,

        // Shadow's upgrades.
        Upgrade_Shadow_LightDash = 6012,
        Upgrade_Shadow_BoostLV1 = 6013,
        Upgrade_Shadow_BoostLV2 = 6014,
        Upgrade_Shadow_BoostLV3 = 6015,

        // Silver's upgrades.
        Upgrade_Silver_HoldSmash = 6016,
        Upgrade_Silver_CatchAll = 6017,
        Upgrade_Silver_Teleport = 6018,
        Upgrade_Silver_Psychoshock = 6019,
        Upgrade_Silver_SpeedUp = 6020
    }
}
