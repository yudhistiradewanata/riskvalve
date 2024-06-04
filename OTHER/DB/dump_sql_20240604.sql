/*
 Navicat Premium Data Transfer

 Source Server         : Localhost
 Source Server Type    : SQL Server
 Source Server Version : 16004105 (16.00.4105)
 Source Host           : 127.0.0.1:1433
 Source Catalog        : Riskvalve_RAW
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 16004105 (16.00.4105)
 File Encoding         : 65001

 Date: 04/06/2024 01:31:36
*/


-- ----------------------------
-- Table structure for Area
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Area]') AND type IN ('U'))
	DROP TABLE [dbo].[Area]
GO

CREATE TABLE [dbo].[Area] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [BusinessArea] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsDeleted] bit DEFAULT 0 NOT NULL,
  [CreatedBy] int  NULL,
  [CreatedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [DeletedBy] int  NULL,
  [DeletedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Area] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Area
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[Area] ON
GO

INSERT INTO [dbo].[Area] ([ID], [BusinessArea], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'1', N'SBU', N'0', N'1', N'04-06-2024 01:26:04', NULL, NULL)
GO

INSERT INTO [dbo].[Area] ([ID], [BusinessArea], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'2', N'CBU', N'0', N'1', N'04-06-2024 01:26:08', NULL, NULL)
GO

INSERT INTO [dbo].[Area] ([ID], [BusinessArea], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'3', N'NBU', N'0', N'1', N'04-06-2024 01:26:25', NULL, NULL)
GO

INSERT INTO [dbo].[Area] ([ID], [BusinessArea], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'4', N'PABELOKAN', N'0', N'1', N'04-06-2024 01:26:31', NULL, NULL)
GO

SET IDENTITY_INSERT [dbo].[Area] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Assessment
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Assessment]') AND type IN ('U'))
	DROP TABLE [dbo].[Assessment]
GO

CREATE TABLE [dbo].[Assessment] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [AssetID] int  NOT NULL,
  [AssessmentDate] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [AssessmentNo] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TimePeriode] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TimeToLimitStateLeakageToAtmosphere] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TimeToLimitStateFailureOfFunction] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TimeToLimitStatePassingAccrosValve] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [LeakageToAtmosphereID] int  NULL,
  [FailureOfFunctionID] int  NULL,
  [PassingAccrosValveID] int  NULL,
  [LeakageToAtmosphereTP1ID] int  NULL,
  [LeakageToAtmosphereTP2ID] int  NULL,
  [LeakageToAtmosphereTP3ID] int  NULL,
  [FailureOfFunctionTP1ID] int  NULL,
  [FailureOfFunctionTP2ID] int  NULL,
  [FailureOfFunctionTP3ID] int  NULL,
  [PassingAccrosValveTP1ID] int  NULL,
  [PassingAccrosValveTP2ID] int  NULL,
  [PassingAccrosValveTP3ID] int  NULL,
  [InspectionEffectivenessID] int  NULL,
  [ImpactOfInternalFluidImpuritiesID] int  NULL,
  [ImpactOfOperatingEnvelopesID] int  NULL,
  [UsedWithinOEMSpecificationID] int  NULL,
  [RepairedID] int  NULL,
  [ProductLossDefinition] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [HSSEDefinisionID] int  NULL,
  [Summary] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [RecommendationActionID] int  NULL,
  [DetailedRecommendation] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ConsequenceOfFailure ] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TP1A] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TP2A] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TP3A] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TP1B] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TP2B] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TP3B] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TP1C] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TP2C] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TP3C] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TPTimeToActionA] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TPTimeToActionB] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TPTimeToActionC] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TP1Risk] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TP2Risk] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TP3Risk] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TPTimeToActionRisk] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [LoFScoreLeakageToAtmophereTP1] float(53)  NULL,
  [LoFScoreLeakageToAtmophereTP2] float(53)  NULL,
  [LoFScoreLeakageToAtmophereTP3] float(53)  NULL,
  [LoFScoreFailureOfFunctionTP1] float(53)  NULL,
  [LoFScoreFailureOfFunctionTP2] float(53)  NULL,
  [LoFScoreFailureOfFunctionTP3] float(53)  NULL,
  [LoFScorePassingAccrosValveTP1] float(53)  NULL,
  [LoFScorePassingAccrosValveTP2] float(53)  NULL,
  [LoFScorePassingAccrosValveTP3] float(53)  NULL,
  [CoFScore] float(53)  NULL,
  [IntegrityStatus] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsDeleted] bit DEFAULT 0 NOT NULL,
  [CreatedBy] int  NULL,
  [CreatedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [DeletedBy] int  NULL,
  [DeletedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Discriminator] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Assessment] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Assessment
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[Assessment] ON
GO

SET IDENTITY_INSERT [dbo].[Assessment] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for AssessmentInspection
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AssessmentInspection]') AND type IN ('U'))
	DROP TABLE [dbo].[AssessmentInspection]
GO

CREATE TABLE [dbo].[AssessmentInspection] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [AssessmentID] int  NULL,
  [InspectionID] int  NULL
)
GO

ALTER TABLE [dbo].[AssessmentInspection] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of AssessmentInspection
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[AssessmentInspection] ON
GO

SET IDENTITY_INSERT [dbo].[AssessmentInspection] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for AssessmentMaintenance
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AssessmentMaintenance]') AND type IN ('U'))
	DROP TABLE [dbo].[AssessmentMaintenance]
GO

CREATE TABLE [dbo].[AssessmentMaintenance] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [AssessmentID] int  NULL,
  [MaintenanceID] int  NULL
)
GO

ALTER TABLE [dbo].[AssessmentMaintenance] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of AssessmentMaintenance
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[AssessmentMaintenance] ON
GO

SET IDENTITY_INSERT [dbo].[AssessmentMaintenance] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Asset
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Asset]') AND type IN ('U'))
	DROP TABLE [dbo].[Asset]
GO

CREATE TABLE [dbo].[Asset] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [TagNo] nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [PlatformID] int  NULL,
  [ValveTypeID] int  NULL,
  [Size] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ClassRating] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ParentEquipmentNo] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ParentEquipmentDescription] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [InstallationDate] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [PIDNo] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Manufacturer] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [BodyModel] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [BodyMaterial] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [EndConnection] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [SerialNo] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ManualOverrideID] int  NULL,
  [ActuatorMfg] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ActuatorSerialNo] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ActuatorTypeModel] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ActuatorPower] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [OperatingTemperature] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [OperatingPressure] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [FlowRate] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ServiceFluid] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [FluidPhaseID] int  NULL,
  [ToxicOrFlamableFluidID] int  NULL,
  [AssetName] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [UsageType] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [CostOfReplacementAndRepair] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Actuation] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Status] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsDeleted] bit DEFAULT 0 NOT NULL,
  [CreatedBy] int  NULL,
  [CreatedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [DeletedBy] int  NULL,
  [DeletedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Discriminator] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Asset] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Asset
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[Asset] ON
GO

SET IDENTITY_INSERT [dbo].[Asset] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for CurrentConditionLimitState
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrentConditionLimitState]') AND type IN ('U'))
	DROP TABLE [dbo].[CurrentConditionLimitState]
GO

CREATE TABLE [dbo].[CurrentConditionLimitState] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [CurrentConditionLimitState] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [LimitStateValue] float(53)  NULL,
  [Weighting] float(53)  NULL
)
GO

ALTER TABLE [dbo].[CurrentConditionLimitState] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of CurrentConditionLimitState
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[CurrentConditionLimitState] ON
GO

INSERT INTO [dbo].[CurrentConditionLimitState] ([ID], [CurrentConditionLimitState], [LimitStateValue], [Weighting]) VALUES (N'1', N'Good', N'0.5', N'5')
GO

INSERT INTO [dbo].[CurrentConditionLimitState] ([ID], [CurrentConditionLimitState], [LimitStateValue], [Weighting]) VALUES (N'2', N'Fair', N'5', N'5')
GO

INSERT INTO [dbo].[CurrentConditionLimitState] ([ID], [CurrentConditionLimitState], [LimitStateValue], [Weighting]) VALUES (N'3', N'Poor', N'9', N'5')
GO

SET IDENTITY_INSERT [dbo].[CurrentConditionLimitState] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for FluidPhase
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[FluidPhase]') AND type IN ('U'))
	DROP TABLE [dbo].[FluidPhase]
GO

CREATE TABLE [dbo].[FluidPhase] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [FluidPhase] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[FluidPhase] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of FluidPhase
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[FluidPhase] ON
GO

INSERT INTO [dbo].[FluidPhase] ([ID], [FluidPhase]) VALUES (N'1', N'Liquid')
GO

INSERT INTO [dbo].[FluidPhase] ([ID], [FluidPhase]) VALUES (N'2', N'Gas')
GO

SET IDENTITY_INSERT [dbo].[FluidPhase] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for HSSEDefinision
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[HSSEDefinision]') AND type IN ('U'))
	DROP TABLE [dbo].[HSSEDefinision]
GO

CREATE TABLE [dbo].[HSSEDefinision] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [HSSEDefinision] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [MinBBSValue] float(53)  NULL,
  [CoFCategory] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Score] float(53)  NULL
)
GO

ALTER TABLE [dbo].[HSSEDefinision] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of HSSEDefinision
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[HSSEDefinision] ON
GO

INSERT INTO [dbo].[HSSEDefinision] ([ID], [HSSEDefinision], [MinBBSValue], [CoFCategory], [Score]) VALUES (N'1', N'Multiple Fatalities', N'601', N'E', N'243')
GO

INSERT INTO [dbo].[HSSEDefinision] ([ID], [HSSEDefinision], [MinBBSValue], [CoFCategory], [Score]) VALUES (N'2', N'Single Fatality or Total Permanent', N'401', N'D', N'81')
GO

INSERT INTO [dbo].[HSSEDefinision] ([ID], [HSSEDefinision], [MinBBSValue], [CoFCategory], [Score]) VALUES (N'3', N'Moderate or Significant Injury or Health Effect', N'201', N'C', N'9')
GO

INSERT INTO [dbo].[HSSEDefinision] ([ID], [HSSEDefinision], [MinBBSValue], [CoFCategory], [Score]) VALUES (N'4', N'Minor Injury or Health Effect', N'1', N'B', N'3')
GO

INSERT INTO [dbo].[HSSEDefinision] ([ID], [HSSEDefinision], [MinBBSValue], [CoFCategory], [Score]) VALUES (N'5', N'Slight Injury or Health Effect', N'0', N'A', N'1')
GO

SET IDENTITY_INSERT [dbo].[HSSEDefinision] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for ImpactEffect
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ImpactEffect]') AND type IN ('U'))
	DROP TABLE [dbo].[ImpactEffect]
GO

CREATE TABLE [dbo].[ImpactEffect] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [ImpactEffect] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ImpactEffectValue] float(53)  NULL,
  [Weighting] float(53)  NULL,
  [Weighting2] float(53)  NULL
)
GO

ALTER TABLE [dbo].[ImpactEffect] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of ImpactEffect
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[ImpactEffect] ON
GO

INSERT INTO [dbo].[ImpactEffect] ([ID], [ImpactEffect], [ImpactEffectValue], [Weighting], [Weighting2]) VALUES (N'1', N'No Expected Effect', N'1', N'6', N'3')
GO

INSERT INTO [dbo].[ImpactEffect] ([ID], [ImpactEffect], [ImpactEffectValue], [Weighting], [Weighting2]) VALUES (N'2', N'Moderate Effect', N'3.5', N'6', N'3')
GO

INSERT INTO [dbo].[ImpactEffect] ([ID], [ImpactEffect], [ImpactEffectValue], [Weighting], [Weighting2]) VALUES (N'3', N'Significant Effect', N'6', N'6', N'3')
GO

SET IDENTITY_INSERT [dbo].[ImpactEffect] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Inspection
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Inspection]') AND type IN ('U'))
	DROP TABLE [dbo].[Inspection]
GO

CREATE TABLE [dbo].[Inspection] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [AssetID] int  NULL,
  [InspectionDate] nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [InspectionMethodID] int  NULL,
  [InspectionEffectivenessID] int  NULL,
  [InspectionDescription] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [CurrentConditionLeakeageToAtmosphereID] int  NULL,
  [CurrentConditionFailureOfFunctionID] int  NULL,
  [CurrentConditionPassingAcrossValveID] int  NULL,
  [FunctionCondition] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [TestPressureIfAny] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsDeleted] bit DEFAULT 0 NOT NULL,
  [CreatedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [CreatedBy] int  NULL,
  [DeletedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [DeletedBy] int  NULL
)
GO

ALTER TABLE [dbo].[Inspection] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Inspection
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[Inspection] ON
GO

SET IDENTITY_INSERT [dbo].[Inspection] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for InspectionEffectiveness
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[InspectionEffectiveness]') AND type IN ('U'))
	DROP TABLE [dbo].[InspectionEffectiveness]
GO

CREATE TABLE [dbo].[InspectionEffectiveness] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [Effectiveness] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [EffectivenessValue] float(53)  NULL,
  [Weighting] float(53)  NULL
)
GO

ALTER TABLE [dbo].[InspectionEffectiveness] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of InspectionEffectiveness
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[InspectionEffectiveness] ON
GO

INSERT INTO [dbo].[InspectionEffectiveness] ([ID], [Effectiveness], [EffectivenessValue], [Weighting]) VALUES (N'1', N'Highly Effective', N'0.5', N'5')
GO

INSERT INTO [dbo].[InspectionEffectiveness] ([ID], [Effectiveness], [EffectivenessValue], [Weighting]) VALUES (N'2', N'Ussually Effective', N'3', N'5')
GO

INSERT INTO [dbo].[InspectionEffectiveness] ([ID], [Effectiveness], [EffectivenessValue], [Weighting]) VALUES (N'3', N'Fairly Effective', N'7', N'5')
GO

INSERT INTO [dbo].[InspectionEffectiveness] ([ID], [Effectiveness], [EffectivenessValue], [Weighting]) VALUES (N'4', N'Ineffective', N'9', N'5')
GO

SET IDENTITY_INSERT [dbo].[InspectionEffectiveness] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for InspectionFile
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[InspectionFile]') AND type IN ('U'))
	DROP TABLE [dbo].[InspectionFile]
GO

CREATE TABLE [dbo].[InspectionFile] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [FileName] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [FileSize] bigint  NULL,
  [FileType] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [FilePath] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [InspectionID] int  NULL,
  [MaintenanceID] int  NULL,
  [IsDeleted] bit DEFAULT 0 NOT NULL,
  [CreatedBy] int  NULL,
  [CreatedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [DeletedBy] int  NULL,
  [DeletedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[InspectionFile] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of InspectionFile
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[InspectionFile] ON
GO

SET IDENTITY_INSERT [dbo].[InspectionFile] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for InspectionMethod
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[InspectionMethod]') AND type IN ('U'))
	DROP TABLE [dbo].[InspectionMethod]
GO

CREATE TABLE [dbo].[InspectionMethod] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [InspectionMethod] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[InspectionMethod] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of InspectionMethod
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[InspectionMethod] ON
GO

INSERT INTO [dbo].[InspectionMethod] ([ID], [InspectionMethod]) VALUES (N'1', N'Visual Inspection')
GO

INSERT INTO [dbo].[InspectionMethod] ([ID], [InspectionMethod]) VALUES (N'2', N'Ultrasonic Testing Inspection')
GO

INSERT INTO [dbo].[InspectionMethod] ([ID], [InspectionMethod]) VALUES (N'3', N'Radiography Test')
GO

SET IDENTITY_INSERT [dbo].[InspectionMethod] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for IsValveRepaired
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IsValveRepaired]') AND type IN ('U'))
	DROP TABLE [dbo].[IsValveRepaired]
GO

CREATE TABLE [dbo].[IsValveRepaired] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [IsValveRepaired] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[IsValveRepaired] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of IsValveRepaired
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[IsValveRepaired] ON
GO

INSERT INTO [dbo].[IsValveRepaired] ([ID], [IsValveRepaired]) VALUES (N'1', N'Yes')
GO

INSERT INTO [dbo].[IsValveRepaired] ([ID], [IsValveRepaired]) VALUES (N'2', N'No')
GO

SET IDENTITY_INSERT [dbo].[IsValveRepaired] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Log
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Log]') AND type IN ('U'))
	DROP TABLE [dbo].[Log]
GO

CREATE TABLE [dbo].[Log] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Module] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [CreatedBy] int  NULL,
  [CreatedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Message] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Data] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Log] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Log
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[Log] ON
GO

SET IDENTITY_INSERT [dbo].[Log] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Maintenance
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Maintenance]') AND type IN ('U'))
	DROP TABLE [dbo].[Maintenance]
GO

CREATE TABLE [dbo].[Maintenance] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [AssetID] int  NULL,
  [IsValveRepairedID] int  NULL,
  [MaintenanceDate] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [MaintenanceDescription] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsDeleted] bit DEFAULT 0 NOT NULL,
  [CreatedBy] int  NULL,
  [CreatedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [DeletedBy] int  NULL,
  [DeletedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Maintenance] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Maintenance
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[Maintenance] ON
GO

SET IDENTITY_INSERT [dbo].[Maintenance] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for ManualOverride
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ManualOverride]') AND type IN ('U'))
	DROP TABLE [dbo].[ManualOverride]
GO

CREATE TABLE [dbo].[ManualOverride] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [ManualOverride] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[ManualOverride] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of ManualOverride
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[ManualOverride] ON
GO

INSERT INTO [dbo].[ManualOverride] ([ID], [ManualOverride]) VALUES (N'1', N'Yes')
GO

INSERT INTO [dbo].[ManualOverride] ([ID], [ManualOverride]) VALUES (N'2', N'No')
GO

INSERT INTO [dbo].[ManualOverride] ([ID], [ManualOverride]) VALUES (N'3', N'Normally Open or Close')
GO

SET IDENTITY_INSERT [dbo].[ManualOverride] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Platform
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Platform]') AND type IN ('U'))
	DROP TABLE [dbo].[Platform]
GO

CREATE TABLE [dbo].[Platform] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [AreaID] int  NULL,
  [Platform] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Code] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsDeleted] bit DEFAULT 0 NOT NULL,
  [CreatedBy] int  NULL,
  [CreatedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [DeletedBy] int  NULL,
  [DeletedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Platform] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Platform
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[Platform] ON
GO

SET IDENTITY_INSERT [dbo].[Platform] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for RecommendationAction
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[RecommendationAction]') AND type IN ('U'))
	DROP TABLE [dbo].[RecommendationAction]
GO

CREATE TABLE [dbo].[RecommendationAction] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [RecommendationAction] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[RecommendationAction] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of RecommendationAction
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[RecommendationAction] ON
GO

INSERT INTO [dbo].[RecommendationAction] ([ID], [RecommendationAction]) VALUES (N'1', N'Inspection')
GO

INSERT INTO [dbo].[RecommendationAction] ([ID], [RecommendationAction]) VALUES (N'2', N'Repair')
GO

INSERT INTO [dbo].[RecommendationAction] ([ID], [RecommendationAction]) VALUES (N'3', N'Replace')
GO

SET IDENTITY_INSERT [dbo].[RecommendationAction] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for Repaired
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Repaired]') AND type IN ('U'))
	DROP TABLE [dbo].[Repaired]
GO

CREATE TABLE [dbo].[Repaired] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [Repaired] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [RepairedValue] float(53)  NULL,
  [Weighting] float(53)  NULL
)
GO

ALTER TABLE [dbo].[Repaired] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Repaired
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[Repaired] ON
GO

INSERT INTO [dbo].[Repaired] ([ID], [Repaired], [RepairedValue], [Weighting]) VALUES (N'1', N'Yes', N'7.5', N'1')
GO

INSERT INTO [dbo].[Repaired] ([ID], [Repaired], [RepairedValue], [Weighting]) VALUES (N'2', N'No', N'1', N'1')
GO

SET IDENTITY_INSERT [dbo].[Repaired] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for TimeToLimitState
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeToLimitState]') AND type IN ('U'))
	DROP TABLE [dbo].[TimeToLimitState]
GO

CREATE TABLE [dbo].[TimeToLimitState] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [TimeToLimitState] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [LimitStateValue] float(53)  NULL,
  [Weighting] float(53)  NULL
)
GO

ALTER TABLE [dbo].[TimeToLimitState] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of TimeToLimitState
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[TimeToLimitState] ON
GO

INSERT INTO [dbo].[TimeToLimitState] ([ID], [TimeToLimitState], [LimitStateValue], [Weighting]) VALUES (N'1', N'Improbable', N'1', N'1')
GO

INSERT INTO [dbo].[TimeToLimitState] ([ID], [TimeToLimitState], [LimitStateValue], [Weighting]) VALUES (N'2', N'Doubtful', N'10', N'1')
GO

INSERT INTO [dbo].[TimeToLimitState] ([ID], [TimeToLimitState], [LimitStateValue], [Weighting]) VALUES (N'3', N'Expected', N'100', N'1')
GO

SET IDENTITY_INSERT [dbo].[TimeToLimitState] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for ToxicOrFlamableFluid
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ToxicOrFlamableFluid]') AND type IN ('U'))
	DROP TABLE [dbo].[ToxicOrFlamableFluid]
GO

CREATE TABLE [dbo].[ToxicOrFlamableFluid] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [ToxicOrFlamableFluid] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[ToxicOrFlamableFluid] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of ToxicOrFlamableFluid
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[ToxicOrFlamableFluid] ON
GO

INSERT INTO [dbo].[ToxicOrFlamableFluid] ([ID], [ToxicOrFlamableFluid]) VALUES (N'1', N'Yes')
GO

INSERT INTO [dbo].[ToxicOrFlamableFluid] ([ID], [ToxicOrFlamableFluid]) VALUES (N'2', N'No')
GO

SET IDENTITY_INSERT [dbo].[ToxicOrFlamableFluid] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for UsedWithinOEMSpecification
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[UsedWithinOEMSpecification]') AND type IN ('U'))
	DROP TABLE [dbo].[UsedWithinOEMSpecification]
GO

CREATE TABLE [dbo].[UsedWithinOEMSpecification] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [UsedWithinOEMSpecification] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [UsedWithinOEMSpecificationValue] float(53)  NULL,
  [Weighting] float(53)  NULL
)
GO

ALTER TABLE [dbo].[UsedWithinOEMSpecification] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of UsedWithinOEMSpecification
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[UsedWithinOEMSpecification] ON
GO

INSERT INTO [dbo].[UsedWithinOEMSpecification] ([ID], [UsedWithinOEMSpecification], [UsedWithinOEMSpecificationValue], [Weighting]) VALUES (N'1', N'Significantly Below Specification', N'1', N'1.5')
GO

INSERT INTO [dbo].[UsedWithinOEMSpecification] ([ID], [UsedWithinOEMSpecification], [UsedWithinOEMSpecificationValue], [Weighting]) VALUES (N'2', N'Within Specification Range', N'2.67', N'1.5')
GO

INSERT INTO [dbo].[UsedWithinOEMSpecification] ([ID], [UsedWithinOEMSpecification], [UsedWithinOEMSpecificationValue], [Weighting]) VALUES (N'3', N'Above Specification Limit', N'6', N'1.5')
GO

SET IDENTITY_INSERT [dbo].[UsedWithinOEMSpecification] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for User
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type IN ('U'))
	DROP TABLE [dbo].[User]
GO

CREATE TABLE [dbo].[User] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [Username] nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Password] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Role] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [IsAdmin] bit DEFAULT 0 NOT NULL,
  [IsEngineer] bit DEFAULT 0 NOT NULL,
  [IsViewer] bit DEFAULT 0 NOT NULL,
  [IsDeleted] bit DEFAULT 0 NOT NULL,
  [CreatedBy] int  NULL,
  [CreatedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [DeletedBy] int  NULL,
  [DeletedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[User] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of User
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[User] ON
GO

INSERT INTO [dbo].[User] ([ID], [Username], [Password], [Role], [IsAdmin], [IsEngineer], [IsViewer], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'1', N'aldo@febrian.com', N'$2a$11$O5bjbyUQrpEfRBROnnCAW.VNgr.NDxWXc.9U1VlZNyGg7qoZuPvZa', N'Surface Facility Admin', N'1', N'1', N'1', N'0', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[User] ([ID], [Username], [Password], [Role], [IsAdmin], [IsEngineer], [IsViewer], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'3', N'Engineer', N'$2a$11$S19V9zqjs5jGRmRtG9rm3eA8BxPdUu5usvn6hO4EYPalbb5YjscOi', N'Engineer', N'0', N'1', N'0', N'0', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[User] ([ID], [Username], [Password], [Role], [IsAdmin], [IsEngineer], [IsViewer], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'7', N'tes', N'$2a$11$LWaIyyDNi8ePvX6dT2P1OOuOaR7awGVoGtvYFYmuBlo/gGj0co7qe', N'Tester', N'1', N'1', N'0', N'0', NULL, NULL, NULL, NULL)
GO

SET IDENTITY_INSERT [dbo].[User] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for ValveType
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ValveType]') AND type IN ('U'))
	DROP TABLE [dbo].[ValveType]
GO

CREATE TABLE [dbo].[ValveType] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [ValveType] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[ValveType] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of ValveType
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[ValveType] ON
GO

INSERT INTO [dbo].[ValveType] ([ID], [ValveType]) VALUES (N'1', N'Air Release Valve')
GO

INSERT INTO [dbo].[ValveType] ([ID], [ValveType]) VALUES (N'2', N'Blowdown Valve')
GO

INSERT INTO [dbo].[ValveType] ([ID], [ValveType]) VALUES (N'3', N'Check Valve')
GO

INSERT INTO [dbo].[ValveType] ([ID], [ValveType]) VALUES (N'4', N'Deluge Valve')
GO

INSERT INTO [dbo].[ValveType] ([ID], [ValveType]) VALUES (N'5', N'Level Control Valve')
GO

SET IDENTITY_INSERT [dbo].[ValveType] OFF
GO

COMMIT
GO


-- ----------------------------
-- Auto increment value for Area
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Area]', RESEED, 4)
GO


-- ----------------------------
-- Primary Key structure for table Area
-- ----------------------------
ALTER TABLE [dbo].[Area] ADD CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Assessment
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Assessment]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table Assessment
-- ----------------------------
ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [PK_Assesment] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for AssessmentInspection
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[AssessmentInspection]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table AssessmentInspection
-- ----------------------------
ALTER TABLE [dbo].[AssessmentInspection] ADD CONSTRAINT [PK__Assessme__3214EC272FE6346C] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for AssessmentMaintenance
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[AssessmentMaintenance]', RESEED, 1)
GO


-- ----------------------------
-- Auto increment value for Asset
-- ----------------------------

-- ----------------------------
-- Indexes structure for table Asset
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_ID]
ON [dbo].[Asset] (
  [ID] ASC
)
GO


-- ----------------------------
-- Uniques structure for table Asset
-- ----------------------------
ALTER TABLE [dbo].[Asset] ADD CONSTRAINT [UC_TagNo] UNIQUE NONCLUSTERED ([TagNo] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Asset
-- ----------------------------
ALTER TABLE [dbo].[Asset] ADD CONSTRAINT [PK_Asset] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for CurrentConditionLimitState
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[CurrentConditionLimitState]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table CurrentConditionLimitState
-- ----------------------------
ALTER TABLE [dbo].[CurrentConditionLimitState] ADD CONSTRAINT [PK_CurrentConditionLimitState] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for FluidPhase
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[FluidPhase]', RESEED, 2)
GO


-- ----------------------------
-- Primary Key structure for table FluidPhase
-- ----------------------------
ALTER TABLE [dbo].[FluidPhase] ADD CONSTRAINT [PK_FluidPhase] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for HSSEDefinision
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[HSSEDefinision]', RESEED, 5)
GO


-- ----------------------------
-- Primary Key structure for table HSSEDefinision
-- ----------------------------
ALTER TABLE [dbo].[HSSEDefinision] ADD CONSTRAINT [PK_HSSEDefinision] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for ImpactEffect
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[ImpactEffect]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table ImpactEffect
-- ----------------------------
ALTER TABLE [dbo].[ImpactEffect] ADD CONSTRAINT [PK_ImpactEffect] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Inspection
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Inspection
-- ----------------------------
ALTER TABLE [dbo].[Inspection] ADD CONSTRAINT [PK_Inspection] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for InspectionEffectiveness
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[InspectionEffectiveness]', RESEED, 4)
GO


-- ----------------------------
-- Primary Key structure for table InspectionEffectiveness
-- ----------------------------
ALTER TABLE [dbo].[InspectionEffectiveness] ADD CONSTRAINT [PK_InspectionEffectiveness] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for InspectionFile
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[InspectionFile]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table InspectionFile
-- ----------------------------
ALTER TABLE [dbo].[InspectionFile] ADD CONSTRAINT [PK_InspectionFile] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for InspectionMethod
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[InspectionMethod]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table InspectionMethod
-- ----------------------------
ALTER TABLE [dbo].[InspectionMethod] ADD CONSTRAINT [PK_InspectionMethod] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for IsValveRepaired
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[IsValveRepaired]', RESEED, 2)
GO


-- ----------------------------
-- Primary Key structure for table IsValveRepaired
-- ----------------------------
ALTER TABLE [dbo].[IsValveRepaired] ADD CONSTRAINT [PK_IsValveRepaired] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Log
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Log]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table Log
-- ----------------------------
ALTER TABLE [dbo].[Log] ADD CONSTRAINT [PK__LogDB__3214EC07DE25BC35] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Maintenance
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Maintenance
-- ----------------------------
ALTER TABLE [dbo].[Maintenance] ADD CONSTRAINT [PK_Maintenance] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for ManualOverride
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[ManualOverride]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table ManualOverride
-- ----------------------------
ALTER TABLE [dbo].[ManualOverride] ADD CONSTRAINT [PK_ManualOverride] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Platform
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Platform
-- ----------------------------
ALTER TABLE [dbo].[Platform] ADD CONSTRAINT [PK_Platform] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for RecommendationAction
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[RecommendationAction]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table RecommendationAction
-- ----------------------------
ALTER TABLE [dbo].[RecommendationAction] ADD CONSTRAINT [PK_RecomendedAction] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Repaired
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Repaired]', RESEED, 2)
GO


-- ----------------------------
-- Primary Key structure for table Repaired
-- ----------------------------
ALTER TABLE [dbo].[Repaired] ADD CONSTRAINT [PK_Repaired] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for TimeToLimitState
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[TimeToLimitState]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table TimeToLimitState
-- ----------------------------
ALTER TABLE [dbo].[TimeToLimitState] ADD CONSTRAINT [PK_TimeToLimitState] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for ToxicOrFlamableFluid
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[ToxicOrFlamableFluid]', RESEED, 2)
GO


-- ----------------------------
-- Primary Key structure for table ToxicOrFlamableFluid
-- ----------------------------
ALTER TABLE [dbo].[ToxicOrFlamableFluid] ADD CONSTRAINT [PK_ToxicOrFlamableFluid] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for UsedWithinOEMSpecification
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[UsedWithinOEMSpecification]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table UsedWithinOEMSpecification
-- ----------------------------
ALTER TABLE [dbo].[UsedWithinOEMSpecification] ADD CONSTRAINT [PK_UsedWithinOEMSpecification] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for User
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[User]', RESEED, 7)
GO


-- ----------------------------
-- Indexes structure for table User
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [Index_User_1]
ON [dbo].[User] (
  [Username] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table User
-- ----------------------------
ALTER TABLE [dbo].[User] ADD CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for ValveType
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[ValveType]', RESEED, 5)
GO


-- ----------------------------
-- Primary Key structure for table ValveType
-- ----------------------------
ALTER TABLE [dbo].[ValveType] ADD CONSTRAINT [PK_ValveType] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table Area
-- ----------------------------
ALTER TABLE [dbo].[Area] ADD CONSTRAINT [FK_Area_User2] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Area] ADD CONSTRAINT [FK_Area_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Assessment
-- ----------------------------
ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState4] FOREIGN KEY ([FailureOfFunctionTP1ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assessment_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_CurrentConditionLimitState] FOREIGN KEY ([LeakageToAtmosphereID]) REFERENCES [dbo].[CurrentConditionLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState] FOREIGN KEY ([LeakageToAtmosphereTP1ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState2] FOREIGN KEY ([LeakageToAtmosphereTP2ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_ImpactEffect] FOREIGN KEY ([ImpactOfInternalFluidImpuritiesID]) REFERENCES [dbo].[ImpactEffect] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_Repaired] FOREIGN KEY ([RepairedID]) REFERENCES [dbo].[Repaired] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState3] FOREIGN KEY ([LeakageToAtmosphereTP3ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_Asset] FOREIGN KEY ([AssetID]) REFERENCES [dbo].[Asset] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_RecomendedAction] FOREIGN KEY ([RecommendationActionID]) REFERENCES [dbo].[RecommendationAction] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_InspectionEffectiveness] FOREIGN KEY ([InspectionEffectivenessID]) REFERENCES [dbo].[InspectionEffectiveness] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState5] FOREIGN KEY ([FailureOfFunctionTP2ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState8] FOREIGN KEY ([PassingAccrosValveTP2ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_CurrentConditionLimitState2] FOREIGN KEY ([FailureOfFunctionID]) REFERENCES [dbo].[CurrentConditionLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assessment_User2] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_ImpactEffect2] FOREIGN KEY ([ImpactOfOperatingEnvelopesID]) REFERENCES [dbo].[ImpactEffect] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState7] FOREIGN KEY ([PassingAccrosValveTP1ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_CurrentConditionLimitState3] FOREIGN KEY ([PassingAccrosValveID]) REFERENCES [dbo].[CurrentConditionLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState9] FOREIGN KEY ([PassingAccrosValveTP3ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState6] FOREIGN KEY ([FailureOfFunctionTP3ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_HSSEDefinision] FOREIGN KEY ([HSSEDefinisionID]) REFERENCES [dbo].[HSSEDefinision] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_UsedWithinOEMSpecification] FOREIGN KEY ([UsedWithinOEMSpecificationID]) REFERENCES [dbo].[UsedWithinOEMSpecification] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AssessmentInspection
-- ----------------------------
ALTER TABLE [dbo].[AssessmentInspection] ADD CONSTRAINT [FK_AssessmentInspection_Inspection] FOREIGN KEY ([InspectionID]) REFERENCES [dbo].[Inspection] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[AssessmentInspection] ADD CONSTRAINT [FK_AssessmentInspection_Assessment] FOREIGN KEY ([AssessmentID]) REFERENCES [dbo].[Assessment] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AssessmentMaintenance
-- ----------------------------
ALTER TABLE [dbo].[AssessmentMaintenance] ADD CONSTRAINT [FK_AssessmentMaintenance_Maintenance] FOREIGN KEY ([MaintenanceID]) REFERENCES [dbo].[Maintenance] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[AssessmentMaintenance] ADD CONSTRAINT [FK_AssessmentMaintenance_Assessment] FOREIGN KEY ([AssessmentID]) REFERENCES [dbo].[Assessment] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Asset
-- ----------------------------
ALTER TABLE [dbo].[Asset] ADD CONSTRAINT [FK_Asset_ValveType] FOREIGN KEY ([ValveTypeID]) REFERENCES [dbo].[ValveType] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Asset] ADD CONSTRAINT [FK_Asset_Platform] FOREIGN KEY ([PlatformID]) REFERENCES [dbo].[Platform] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Asset] ADD CONSTRAINT [FK_Asset_FluidPhase_1] FOREIGN KEY ([FluidPhaseID]) REFERENCES [dbo].[FluidPhase] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Asset] ADD CONSTRAINT [FK_Asset_ToxicOrFlamableFluid_2] FOREIGN KEY ([ToxicOrFlamableFluidID]) REFERENCES [dbo].[ToxicOrFlamableFluid] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Asset] ADD CONSTRAINT [FK_Asset_ManualOverride] FOREIGN KEY ([ManualOverrideID]) REFERENCES [dbo].[ManualOverride] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Inspection
-- ----------------------------
ALTER TABLE [dbo].[Inspection] ADD CONSTRAINT [FK_Inspection_InspectionEffectiveness] FOREIGN KEY ([InspectionEffectivenessID]) REFERENCES [dbo].[InspectionEffectiveness] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Inspection] ADD CONSTRAINT [FK_Inspection_CurrentConditionLimitStateB] FOREIGN KEY ([CurrentConditionLeakeageToAtmosphereID]) REFERENCES [dbo].[CurrentConditionLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Inspection] ADD CONSTRAINT [FK_Inspection_CurrentConditionLimitStateC] FOREIGN KEY ([CurrentConditionPassingAcrossValveID]) REFERENCES [dbo].[CurrentConditionLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Inspection] ADD CONSTRAINT [FK_Inspection_CurrentConditionLimitStateA] FOREIGN KEY ([CurrentConditionFailureOfFunctionID]) REFERENCES [dbo].[CurrentConditionLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Inspection] ADD CONSTRAINT [FK_Inspection_InspectionMethod] FOREIGN KEY ([InspectionMethodID]) REFERENCES [dbo].[InspectionMethod] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Inspection] ADD CONSTRAINT [FK_Inspection_Asset] FOREIGN KEY ([AssetID]) REFERENCES [dbo].[Asset] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Inspection] ADD CONSTRAINT [FK_Inspection_User_1] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Inspection] ADD CONSTRAINT [FK_Inspection_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table InspectionFile
-- ----------------------------
ALTER TABLE [dbo].[InspectionFile] ADD CONSTRAINT [FK_InspectionFile_Inspection] FOREIGN KEY ([InspectionID]) REFERENCES [dbo].[Inspection] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[InspectionFile] ADD CONSTRAINT [FK_InspectionFile_Maintenance] FOREIGN KEY ([MaintenanceID]) REFERENCES [dbo].[Maintenance] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[InspectionFile] ADD CONSTRAINT [FK_InspectionFile_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[InspectionFile] ADD CONSTRAINT [FK_InspectionFile_User_1] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Maintenance
-- ----------------------------
ALTER TABLE [dbo].[Maintenance] ADD CONSTRAINT [FK_Maintenance_Asset] FOREIGN KEY ([AssetID]) REFERENCES [dbo].[Asset] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Maintenance] ADD CONSTRAINT [FK_Maintenance_IsValveRepaired] FOREIGN KEY ([IsValveRepairedID]) REFERENCES [dbo].[IsValveRepaired] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Maintenance] ADD CONSTRAINT [FK_Maintenance_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Maintenance] ADD CONSTRAINT [FK_Maintenance_User2] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Platform
-- ----------------------------
ALTER TABLE [dbo].[Platform] ADD CONSTRAINT [FK_Platform_Area] FOREIGN KEY ([AreaID]) REFERENCES [dbo].[Area] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Platform] ADD CONSTRAINT [FK_Platform_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Platform] ADD CONSTRAINT [FK_Platform_User2] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table User
-- ----------------------------
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_User_1] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

