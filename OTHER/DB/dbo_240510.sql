/*
 Navicat SQL Server Data Transfer

 Source Server         : LOCAL
 Source Server Type    : SQL Server
 Source Server Version : 16004105 (16.00.4105)
 Source Host           : 127.0.0.1:1433
 Source Catalog        : Riskvalve
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 16004105 (16.00.4105)
 File Encoding         : 65001

 Date: 10/05/2024 14:59:33
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

INSERT INTO [dbo].[Area] ([ID], [BusinessArea], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'1', N'NBU', N'0', N'1', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Area] ([ID], [BusinessArea], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'2', N'CBU', N'0', N'1', NULL, NULL, NULL)
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

INSERT INTO [dbo].[Assessment] ([ID], [AssetID], [AssessmentDate], [AssessmentNo], [TimePeriode], [TimeToLimitStateLeakageToAtmosphere], [TimeToLimitStateFailureOfFunction], [TimeToLimitStatePassingAccrosValve], [LeakageToAtmosphereID], [FailureOfFunctionID], [PassingAccrosValveID], [LeakageToAtmosphereTP1ID], [LeakageToAtmosphereTP2ID], [LeakageToAtmosphereTP3ID], [FailureOfFunctionTP1ID], [FailureOfFunctionTP2ID], [FailureOfFunctionTP3ID], [PassingAccrosValveTP1ID], [PassingAccrosValveTP2ID], [PassingAccrosValveTP3ID], [InspectionEffectivenessID], [ImpactOfInternalFluidImpuritiesID], [ImpactOfOperatingEnvelopesID], [UsedWithinOEMSpecificationID], [RepairedID], [ProductLossDefinition], [HSSEDefinisionID], [Summary], [RecommendationActionID], [DetailedRecommendation], [ConsequenceOfFailure ], [TP1A], [TP2A], [TP3A], [TP1B], [TP2B], [TP3B], [TP1C], [TP2C], [TP3C], [TPTimeToActionA], [TPTimeToActionB], [TPTimeToActionC], [TP1Risk], [TP2Risk], [TP3Risk], [TPTimeToActionRisk], [LoFScoreLeakageToAtmophereTP1], [LoFScoreLeakageToAtmophereTP2], [LoFScoreLeakageToAtmophereTP3], [LoFScoreFailureOfFunctionTP1], [LoFScoreFailureOfFunctionTP2], [LoFScoreFailureOfFunctionTP3], [LoFScorePassingAccrosValveTP1], [LoFScorePassingAccrosValveTP2], [LoFScorePassingAccrosValveTP3], [CoFScore], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt], [Discriminator]) VALUES (N'84', N'52', N'30-04-2024', N'ASSESSMENT84', N'18', N'40', N'40', N'60', N'3', N'1', N'1', N'1', N'2', N'3', N'1', N'2', N'3', N'1', N'2', N'2', N'4', N'1', N'2', N'2', N'1', N'250', N'3', N'', NULL, N'', N'C', N'2C', N'4C', N'5C', N'2C', N'3C', N'5C', N'2C', N'3C', N'3C', N'11-01-2025', N'18-05-2025', N'25-10-2026', N'2C', N'4C', N'5C', N'11-01-2025', N'107.51', N'1075.1', N'10751', N'65.01', N'650.1', N'6501', N'65.01', N'650.1', N'650.1', N'9', N'0', N'1', N'09-05-2024 14:19:36', NULL, NULL, N'AssessmentDB')
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

INSERT INTO [dbo].[AssessmentInspection] ([ID], [AssessmentID], [InspectionID]) VALUES (N'6', N'84', N'81')
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

INSERT INTO [dbo].[AssessmentMaintenance] ([ID], [AssessmentID], [MaintenanceID]) VALUES (N'6', N'84', N'30')
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
  [DeletedAt] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
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

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'1', N'WIB-PSV-V05A-4', N'5', N'1', N'12', N'155', N'P123', N'Just Desc', N'14/03/2024', N'PID 2', N'AL', N'NA', N'MAT', N'RJ', N'2342N4324823J', N'1', N'N/A', N'345656782', N'N/A', N'N/A', N'1', N'2', N'3', N'UNKNOWN', N'1', N'1', NULL, NULL, NULL, NULL, NULL, N'0', N'1', N'21-04-2024 12:53:24', NULL, NULL)
GO

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'49', N'WIB-PSV-V05A-41', N'11', N'1', N'10', N'150', N'PL-518-A-12', N'COTP Line', N'39888', N'CIC-11012', N'GLT', N'Trunnion', N'A105', N'RF', N'Missing Name Plate', N'1', N'N/A', N'N/A', N'N/A', N'N/A', N'60', N'50', N'100', N'Waste Gas', N'2', N'1', N'Pressure Safety Valve V05A', N'PSV/PRV', N'15000', N'N/A', N'In-service', N'0', N'1', N'22-04-2024 10:13:32', NULL, NULL)
GO

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'50', N'WIB-PSV-V05A-43', N'5', N'1', N'10', N'150', N'PL-518-A-12', N'COTP Line', N'39888', N'CIC-11012', N'GLT', N'Trunnion', N'A105', N'RF', N'Missing Name Plate', N'1', N'N/A', N'N/A', N'N/A', N'N/A', N'60', N'50', N'100', N'Waste Gas', N'2', N'1', N'Pressure Safety Valve V05A', N'PSV/PRV', N'15000', N'N/A', N'In-service', N'0', N'1', N'22-04-2024 10:13:32', NULL, NULL)
GO

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'51', N'WIB-PSV-V05A-44', N'5', N'1', N'10', N'150', N'PL-518-A-12', N'COTP Line', N'39888', N'CIC-11012', N'GLT', N'Trunnion', N'A105', N'RF', N'Missing Name Plate', N'1', N'N/A', N'N/A', N'N/A', N'N/A', N'60', N'50', N'100', N'Waste Gas', N'2', N'1', N'Pressure Safety Valve V05A', N'PSV/PRV', N'15000', N'N/A', N'In-service', N'0', N'1', N'22-04-2024 10:13:32', NULL, NULL)
GO

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'52', N'WIE-LCV-1', N'11', N'5', N'2', N'600', N'PL-102-D-4', N'Oil Level Control Valve outlet Test Separator V-01 (LCV-1)', N'1/1/2007 12:00:00AM', N'WIE-11001 P2', N'FISHER', N'Globe', N'WCB', N'RF', N'N/A', N'2', N'FISHER', N'N/A', N'667', N'N/A', N'100', N'1440', N'-', N'Crude Oil', N'1', N'1', N'Level Control Valve outlet Test Separator', N'Control Valve', N'-', N'Pneumatic', N'In-service', N'0', N'1', N'29-04-2024 15:05:39', NULL, NULL)
GO

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'53', N'WIE-LCV-3', N'11', N'5', N'2', N'600', N'PL-102-D-4', N'Oil Level Control Valve outlet Test Separator V-01 (LCV-1)', N'1/1/2007 12:00:00AM', N'WIE-11001 P2', N'FISHER', N'Globe', N'WCB', N'RF', N'N/A', N'2', N'FISHER', N'N/A', N'667', N'N/A', N'100', N'1440', N'-', N'Crude Oil', N'1', N'1', N'Level Control Valve outlet Test Separator', N'Control Valve', N'-', N'Pneumatic', N'In-service', N'1', N'1', N'29-04-2024 16:12:42', N'1', N'30-04-2024 14:15:57')
GO

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'54', N'xxxx', N'5', N'5', N'2', N'600', N'PL-102-D-4', N'Oil Level Control Valve outlet Test Separator V-01 (LCV-1)', N'1/1/2007 12:00:00AM', N'WIE-11001 P2', N'FISHER', N'Globe', N'WCB', N'RF', N'N/A', N'2', N'FISHER', N'N/A', N'667', N'N/A', N'100', N'1440', N'-', N'Crude Oil', N'1', N'1', N'Level Control Valve outlet Test Separator', N'Control Valve', N'-123', N'Pneumatic', N'In-service', N'0', N'1', N'30-04-2024 10:53:31', NULL, NULL)
GO

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'55', N'123', N'10', N'2', N'213', N'123', N'3213', N'123', N'08-05-2024', N'123', N'44', N'44', N'44', N'44', N'44', N'1', N'44', N'44', N'44', N'44', N'44', N'44', N'44', N'44', N'1', N'1', N'213', N'22', NULL, N'44', N'33', N'0', N'1', N'08-05-2024 11:53:38', NULL, NULL)
GO

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'56', N'34', N'5', N'1', N'45', N'345', N'3453', N'345', N'08-05-2024', N'345', N'453', N'453', N'453', N'453', N'453', N'1', N'453', N'453', N'453', N'543', N'453', N'45345', N'345', N'453', N'1', N'1', N'345', N'345', N'345', N'3', N'345', N'0', N'1', N'08-05-2024 12:20:30', NULL, NULL)
GO

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'57', N'567', N'10', N'1', N'567', N'567', N'567', N'567', N'08-05-2024', N'57', N'567', N'567', N'567', N'567', N'567', N'1', N'567', N'567', N'567', N'567', N'567', N'675', N'675', N'5675', N'1', N'1', N'567', N'567', N'567', N'567', N'567', N'0', N'1', N'08-05-2024 12:21:58', NULL, NULL)
GO

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'58', N'675', N'5', N'1', N'675', N'5', N'675', N'675', N'08-05-2024', N'675', N'675', N'567', N'567', N'567', N'576', N'1', N'567', N'567', N'567', N'57', N'65', N'567', N'567', N'567', N'1', N'1', N'675', N'675', N'675', N'675', N'675', N'0', N'1', N'08-05-2024 12:24:04', NULL, NULL)
GO

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'59', N'12', N'10', N'1', N'21', N'12', N'12', N'12', N'16-05-2024', N'21', N'21', N'21', N'21', N'12', N'12', N'1', N'21', N'2', N'12', N'12', N'12', N'1', N'21', N'12', N'1', N'1', N'21', N'21', N'21', N'21', N'21', N'0', N'1', N'08-05-2024 12:25:14', NULL, NULL)
GO

INSERT INTO [dbo].[Asset] ([ID], [TagNo], [PlatformID], [ValveTypeID], [Size], [ClassRating], [ParentEquipmentNo], [ParentEquipmentDescription], [InstallationDate], [PIDNo], [Manufacturer], [BodyModel], [BodyMaterial], [EndConnection], [SerialNo], [ManualOverrideID], [ActuatorMfg], [ActuatorSerialNo], [ActuatorTypeModel], [ActuatorPower], [OperatingTemperature], [OperatingPressure], [FlowRate], [ServiceFluid], [FluidPhaseID], [ToxicOrFlamableFluidID], [AssetName], [UsageType], [CostOfReplacementAndRepair], [Actuation], [Status], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'60', N'tes', N'10', N'1', N'23', N'tes', N'tes', N'4', N'08-05-2024', N'123', N'213', N'213', N'123', N'123', N'123', N'1', N'213', N'213', N'123', N'321', N'213', N'213', N'123', N'213', N'1', N'1', N'123', N'32', N'123--', N'213', N'33', N'0', N'1', N'08-05-2024 12:30:51', NULL, NULL)
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

INSERT INTO [dbo].[ImpactEffect] ([ID], [ImpactEffect], [ImpactEffectValue], [Weighting], [Weighting2]) VALUES (N'1', N'No Expected Effect', N'1', N'1.5', N'3')
GO

INSERT INTO [dbo].[ImpactEffect] ([ID], [ImpactEffect], [ImpactEffectValue], [Weighting], [Weighting2]) VALUES (N'2', N'Moderate Effect', N'3.5', N'1.5', N'3')
GO

INSERT INTO [dbo].[ImpactEffect] ([ID], [ImpactEffect], [ImpactEffectValue], [Weighting], [Weighting2]) VALUES (N'3', N'Significant Effect', N'6', N'1.5', N'3')
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

INSERT INTO [dbo].[Inspection] ([ID], [AssetID], [InspectionDate], [InspectionMethodID], [InspectionEffectivenessID], [InspectionDescription], [CurrentConditionLeakeageToAtmosphereID], [CurrentConditionFailureOfFunctionID], [CurrentConditionPassingAcrossValveID], [FunctionCondition], [TestPressureIfAny], [IsDeleted], [CreatedAt], [CreatedBy], [DeletedAt], [DeletedBy]) VALUES (N'74', N'1', N'11-06-2022', N'1', N'1', N'Packing, Valve Trim: Worn
Bonnet Gasket, Bonnet : N/A
Valve Body, Bolts & Nuts: Heavy Corroded 
Gear Box : Corroded', N'1', N'2', N'3', N'Packing Leak', N'', N'0', N'21-04-2024 22:12:38', N'1', NULL, NULL)
GO

INSERT INTO [dbo].[Inspection] ([ID], [AssetID], [InspectionDate], [InspectionMethodID], [InspectionEffectivenessID], [InspectionDescription], [CurrentConditionLeakeageToAtmosphereID], [CurrentConditionFailureOfFunctionID], [CurrentConditionPassingAcrossValveID], [FunctionCondition], [TestPressureIfAny], [IsDeleted], [CreatedAt], [CreatedBy], [DeletedAt], [DeletedBy]) VALUES (N'78', N'1', N'12-06-2022', N'1', N'1', N'Packing, Valve Trim: Worn
Bonnet Gasket, Bonnet : N/A
Valve Body, Bolts & Nuts: Heavy Corroded 
Gear Box : Corroded', N'2', N'1', N'3', N'Packing Leak', NULL, N'0', N'22-04-2024 10:15:27', N'1', NULL, NULL)
GO

INSERT INTO [dbo].[Inspection] ([ID], [AssetID], [InspectionDate], [InspectionMethodID], [InspectionEffectivenessID], [InspectionDescription], [CurrentConditionLeakeageToAtmosphereID], [CurrentConditionFailureOfFunctionID], [CurrentConditionPassingAcrossValveID], [FunctionCondition], [TestPressureIfAny], [IsDeleted], [CreatedAt], [CreatedBy], [DeletedAt], [DeletedBy]) VALUES (N'79', N'1', N'14-06-2022', N'1', N'1', N'Packing, Valve Trim: Worn
Bonnet Gasket, Bonnet : N/A
Valve Body, Bolts & Nuts: Heavy Corroded 
Gear Box : Corroded', N'2', N'2', N'3', N'Packing Leak', NULL, N'0', N'22-04-2024 10:15:27', N'1', NULL, NULL)
GO

INSERT INTO [dbo].[Inspection] ([ID], [AssetID], [InspectionDate], [InspectionMethodID], [InspectionEffectivenessID], [InspectionDescription], [CurrentConditionLeakeageToAtmosphereID], [CurrentConditionFailureOfFunctionID], [CurrentConditionPassingAcrossValveID], [FunctionCondition], [TestPressureIfAny], [IsDeleted], [CreatedAt], [CreatedBy], [DeletedAt], [DeletedBy]) VALUES (N'80', N'49', N'29-04-2024', N'1', N'1', N'ok', N'1', N'2', N'3', N'ok', N'ok', N'0', N'29-04-2024 11:45:26', N'1', NULL, NULL)
GO

INSERT INTO [dbo].[Inspection] ([ID], [AssetID], [InspectionDate], [InspectionMethodID], [InspectionEffectivenessID], [InspectionDescription], [CurrentConditionLeakeageToAtmosphereID], [CurrentConditionFailureOfFunctionID], [CurrentConditionPassingAcrossValveID], [FunctionCondition], [TestPressureIfAny], [IsDeleted], [CreatedAt], [CreatedBy], [DeletedAt], [DeletedBy]) VALUES (N'81', N'52', N'26-04-2022', N'1', N'4', N'Packing: Leak
Bonnet: Corroded
Bonnet Gasket: Worn
Valve Body: Corroded 
Valve Trim: Worn
Body Bolts & Nuts: Corroded
External Condition: Corroded
Seals: Worn', N'3', N'1', N'1', N'Packing Leak', N'', N'0', N'29-04-2024 15:11:27', N'1', NULL, NULL)
GO

INSERT INTO [dbo].[Inspection] ([ID], [AssetID], [InspectionDate], [InspectionMethodID], [InspectionEffectivenessID], [InspectionDescription], [CurrentConditionLeakeageToAtmosphereID], [CurrentConditionFailureOfFunctionID], [CurrentConditionPassingAcrossValveID], [FunctionCondition], [TestPressureIfAny], [IsDeleted], [CreatedAt], [CreatedBy], [DeletedAt], [DeletedBy]) VALUES (N'83', N'53', N'26-04-2022', N'1', N'4', N'Packing: Leak
Bonnet: Corroded
Bonnet Gasket: Worn
Valve Body: Corroded 
Valve Trim: Worn
Body Bolts & Nuts: Corroded
External Condition: Corroded
Seals: Worn', N'3', N'1', N'1', N'Packing Leak', NULL, N'0', N'29-04-2024 16:12:52', N'1', NULL, NULL)
GO

INSERT INTO [dbo].[Inspection] ([ID], [AssetID], [InspectionDate], [InspectionMethodID], [InspectionEffectivenessID], [InspectionDescription], [CurrentConditionLeakeageToAtmosphereID], [CurrentConditionFailureOfFunctionID], [CurrentConditionPassingAcrossValveID], [FunctionCondition], [TestPressureIfAny], [IsDeleted], [CreatedAt], [CreatedBy], [DeletedAt], [DeletedBy]) VALUES (N'84', N'1', N'23-12-2022', N'1', N'1', N'Packing, Valve Trim: Worn
Bonnet Gasket, Bonnet : N/A
Valve Body, Bolts & Nuts: Heavy Corroded 
Gear Box : Corroded', N'1', N'2', N'3', N'Packing Leak', NULL, N'0', N'30-04-2024 15:24:10', N'1', NULL, NULL)
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

INSERT INTO [dbo].[InspectionEffectiveness] ([ID], [Effectiveness], [EffectivenessValue], [Weighting]) VALUES (N'3', N'Fairly Effective', N'5', N'5')
GO

INSERT INTO [dbo].[InspectionEffectiveness] ([ID], [Effectiveness], [EffectivenessValue], [Weighting]) VALUES (N'4', N'Ineffective', N'7', N'5')
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

INSERT INTO [dbo].[InspectionFile] ([ID], [FileName], [FileSize], [FileType], [FilePath], [InspectionID], [MaintenanceID], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'1', N'5e826510-3e75-48d6-9900-9f9988b6b6fd.png', N'890803', N'image/png', N'Uploads/Maintenance/29/5e826510-3e75-48d6-9900-9f9988b6b6fd.png', NULL, N'29', N'0', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[InspectionFile] ([ID], [FileName], [FileSize], [FileType], [FilePath], [InspectionID], [MaintenanceID], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'2', N'9e81473d-63a0-4525-9f66-70d20b59e4f9.png', N'305539', N'image/png', N'Uploads/Inspection/74/9e81473d-63a0-4525-9f66-70d20b59e4f9.png', N'74', NULL, N'0', N'1', N'03-05-2024 10:01:22', NULL, NULL)
GO

INSERT INTO [dbo].[InspectionFile] ([ID], [FileName], [FileSize], [FileType], [FilePath], [InspectionID], [MaintenanceID], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'3', N'7c16753a-eb95-435b-a950-e73039e1b59d.png', N'890803', N'image/png', N'Uploads/Maintenance/23/7c16753a-eb95-435b-a950-e73039e1b59d.png', NULL, N'23', N'0', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[InspectionFile] ([ID], [FileName], [FileSize], [FileType], [FilePath], [InspectionID], [MaintenanceID], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'4', N'5ab2cf7b-7601-4818-b2ac-28bb6a3f5a07.png', N'890803', N'image/png', N'Uploads/Inspection/81/5ab2cf7b-7601-4818-b2ac-28bb6a3f5a07.png', N'81', NULL, N'0', N'1', N'10-05-2024 14:47:03', NULL, NULL)
GO

INSERT INTO [dbo].[InspectionFile] ([ID], [FileName], [FileSize], [FileType], [FilePath], [InspectionID], [MaintenanceID], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'5', N'979343f1-4371-4be8-9573-e11c6a403154.png', N'890803', N'image/png', N'Uploads/Maintenance/30/979343f1-4371-4be8-9573-e11c6a403154.png', NULL, N'30', N'0', NULL, NULL, NULL, NULL)
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

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'1', N'ImportAssetRegister', N'1', N'21-04-2024 22:12:14', N'An asset with the tag number WIB-PSV-V05A-4 already exists.', N'{"Id":0,"TagNo":"WIB-PSV-V05A-4","AssetName":"Pressure Safety Valve V05A","PlatformID":5,"ValveTypeID":1,"Size":"10","ClassRating":"150","ParentEquipmentNo":"PL-518-A-12","ParentEquipmentDescription":"COTP Line","InstallationDate":"39888","PIDNo":"CIC-11012","Manufacturer":"GLT","BodyModel":"Trunnion","BodyMaterial":"A105","EndConnection":"RF","SerialNo":"Missing Name Plate","ManualOverrideID":1,"ActuatorMfg":"N/A","ActuatorSerialNo":"N/A","ActuatorTypeModel":"N/A","ActuatorPower":"N/A","OperatingTemperature":"60","OperatingPressure":"50","FlowRate":"100","ServiceFluid":"Waste Gas","FluidPhaseID":2,"ToxicOrFlamableFluidID":1,"UsageType":"PSV/PRV","CostOfReplacementAndRepair":"15000","Actuation":"N/A","Status":"In-service","IsDeleted":false,"CreatedBy":1,"CreatedAt":"21-04-2024 22:12:14","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'2', N'ImportAssetRegister', N'1', N'22-04-2024 09:47:17', N'An asset with the tag number WIB-PSV-V05A-4 already exists.', N'{"Id":0,"TagNo":"WIB-PSV-V05A-4","AssetName":"Pressure Safety Valve V05A","PlatformID":5,"ValveTypeID":1,"Size":"10","ClassRating":"150","ParentEquipmentNo":"PL-518-A-12","ParentEquipmentDescription":"COTP Line","InstallationDate":"39888","PIDNo":"CIC-11012","Manufacturer":"GLT","BodyModel":"Trunnion","BodyMaterial":"A105","EndConnection":"RF","SerialNo":"Missing Name Plate","ManualOverrideID":1,"ActuatorMfg":"N/A","ActuatorSerialNo":"N/A","ActuatorTypeModel":"N/A","ActuatorPower":"N/A","OperatingTemperature":"60","OperatingPressure":"50","FlowRate":"100","ServiceFluid":"Waste Gas","FluidPhaseID":2,"ToxicOrFlamableFluidID":1,"UsageType":"PSV/PRV","CostOfReplacementAndRepair":"15000","Actuation":"N/A","Status":"In-service","IsDeleted":false,"CreatedBy":1,"CreatedAt":"22-04-2024 09:47:17","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'3', N'ImportInspection', N'1', N'22-04-2024 09:51:57', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"11-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":1,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":3,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"22-04-2024 09:51:57","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'4', N'ImportMaintenance', N'1', N'22-04-2024 09:53:10', N'Maintenance with the same asset and date already exist', N'{"Id":0,"AssetID":1,"IsValveRepairedID":1,"MaintenanceDate":"01-06-2023","MaintenanceDescription":"Refurbish valve body, gear box and replace bolts and nuts.","IsDeleted":false,"CreatedBy":1,"CreatedAt":"22-04-2024 09:53:10","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'5', N'ImportAssetRegister', N'1', N'22-04-2024 10:13:32', N'An asset with the tag number WIB-PSV-V05A-4 already exists.', N'{"Id":0,"TagNo":"WIB-PSV-V05A-4","AssetName":"Pressure Safety Valve V05A","PlatformID":5,"ValveTypeID":1,"Size":"10","ClassRating":"150","ParentEquipmentNo":"PL-518-A-12","ParentEquipmentDescription":"COTP Line","InstallationDate":"39888","PIDNo":"CIC-11012","Manufacturer":"GLT","BodyModel":"Trunnion","BodyMaterial":"A105","EndConnection":"RF","SerialNo":"Missing Name Plate","ManualOverrideID":1,"ActuatorMfg":"N/A","ActuatorSerialNo":"N/A","ActuatorTypeModel":"N/A","ActuatorPower":"N/A","OperatingTemperature":"60","OperatingPressure":"50","FlowRate":"100","ServiceFluid":"Waste Gas","FluidPhaseID":2,"ToxicOrFlamableFluidID":1,"UsageType":"PSV/PRV","CostOfReplacementAndRepair":"15000","Actuation":"N/A","Status":"In-service","IsDeleted":false,"CreatedBy":1,"CreatedAt":"22-04-2024 10:13:32","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'6', N'ImportAssetRegister', N'1', N'22-04-2024 10:13:32', N'An asset with the tag number WIB-PSV-V05A-41 already exists.', N'{"Id":0,"TagNo":"WIB-PSV-V05A-41","AssetName":"Pressure Safety Valve V05A","PlatformID":5,"ValveTypeID":1,"Size":"10","ClassRating":"150","ParentEquipmentNo":"PL-518-A-12","ParentEquipmentDescription":"COTP Line","InstallationDate":"39888","PIDNo":"CIC-11012","Manufacturer":"GLT","BodyModel":"Trunnion","BodyMaterial":"A105","EndConnection":"RF","SerialNo":"Missing Name Plate","ManualOverrideID":1,"ActuatorMfg":"N/A","ActuatorSerialNo":"N/A","ActuatorTypeModel":"N/A","ActuatorPower":"N/A","OperatingTemperature":"60","OperatingPressure":"50","FlowRate":"100","ServiceFluid":"Waste Gas","FluidPhaseID":2,"ToxicOrFlamableFluidID":1,"UsageType":"PSV/PRV","CostOfReplacementAndRepair":"15000","Actuation":"N/A","Status":"In-service","IsDeleted":false,"CreatedBy":1,"CreatedAt":"22-04-2024 10:13:32","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'7', N'ImportInspection', N'1', N'22-04-2024 10:15:27', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"11-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":1,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":3,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"22-04-2024 10:15:27","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'8', N'ImportInspection', N'1', N'22-04-2024 10:15:27', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"12-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":1,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":2,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"22-04-2024 10:15:27","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'9', N'ImportMaintenance', N'1', N'22-04-2024 10:18:47', N'Maintenance with the same asset and date already exist', N'{"Id":0,"AssetID":1,"IsValveRepairedID":1,"MaintenanceDate":"01-06-2023","MaintenanceDescription":"Refurbish valve body, gear box and replace bolts and nuts.","IsDeleted":false,"CreatedBy":1,"CreatedAt":"22-04-2024 10:18:47","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'10', N'ImportMaintenance', N'1', N'22-04-2024 10:18:47', N'Maintenance with the same asset and date already exist', N'{"Id":0,"AssetID":1,"IsValveRepairedID":1,"MaintenanceDate":"02-06-2023","MaintenanceDescription":"Refurbish valve body, gear box and replace bolts and nuts.","IsDeleted":false,"CreatedBy":1,"CreatedAt":"22-04-2024 10:18:47","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'11', N'ImportAssessment', N'1', N'22-04-2024 10:20:41', N'An assessment with the same asset and date already exists', N'{"Id":0,"AssetID":1,"AssessmentNo":"IMPORT","AssessmentDate":"01-07-2022","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"60","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"40","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":1,"LeakageToAtmosphereTP2ID":2,"LeakageToAtmosphereTP3ID":2,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":3,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":1,"UsedWithinOEMSpecificationID":1,"RepairedID":1,"ProductLossDefinition":"1","HSSEDefinisionID":null,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"IsDeleted":false,"CreatedAt":"22-04-2024 10:20:41","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'12', N'ImportAssessment', N'1', N'22-04-2024 10:20:41', N'An assessment with the same asset and date already exists', N'{"Id":0,"AssetID":1,"AssessmentNo":"IMPORT","AssessmentDate":"03-07-2022","TimePeriode":"24","TimeToLimitStateLeakageToAtmosphere":"60","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"40","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":1,"LeakageToAtmosphereTP2ID":2,"LeakageToAtmosphereTP3ID":2,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":3,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":1,"UsedWithinOEMSpecificationID":1,"RepairedID":1,"ProductLossDefinition":"1","HSSEDefinisionID":null,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"IsDeleted":false,"CreatedAt":"22-04-2024 10:20:41","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'13', N'ImportInspection', N'1', N'29-04-2024 15:53:09', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":52,"InspectionDate":"26-04-2022","InspectionMethodID":1,"InspectionEffectivenessID":4,"InspectionDescription":"Packing: Leak\nBonnet: Corroded\nBonnet Gasket: Worn\nValve Body: Corroded \nValve Trim: Worn\nBody Bolts & Nuts: Corroded\nExternal Condition: Corroded\nSeals: Worn","CurrentConditionLeakeageToAtmosphereID":3,"CurrentConditionFailureOfFunctionID":1,"CurrentConditionPassingAcrossValveID":1,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"29-04-2024 15:53:09","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'14', N'ImportInspection', N'1', N'29-04-2024 15:54:48', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":52,"InspectionDate":"26-04-2022","InspectionMethodID":1,"InspectionEffectivenessID":4,"InspectionDescription":"Packing: Leak\nBonnet: Corroded\nBonnet Gasket: Worn\nValve Body: Corroded \nValve Trim: Worn\nBody Bolts & Nuts: Corroded\nExternal Condition: Corroded\nSeals: Worn","CurrentConditionLeakeageToAtmosphereID":3,"CurrentConditionFailureOfFunctionID":1,"CurrentConditionPassingAcrossValveID":1,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"29-04-2024 15:54:48","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'15', N'ImportInspection', N'1', N'29-04-2024 16:01:09', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":52,"InspectionDate":"26-04-2022","InspectionMethodID":1,"InspectionEffectivenessID":4,"InspectionDescription":"Packing: Leak\nBonnet: Corroded\nBonnet Gasket: Worn\nValve Body: Corroded \nValve Trim: Worn\nBody Bolts & Nuts: Corroded\nExternal Condition: Corroded\nSeals: Worn","CurrentConditionLeakeageToAtmosphereID":3,"CurrentConditionFailureOfFunctionID":1,"CurrentConditionPassingAcrossValveID":1,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"29-04-2024 16:01:09","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'16', N'ImportInspection', N'1', N'29-04-2024 21:12:28', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":53,"InspectionDate":"26-04-2022","InspectionMethodID":1,"InspectionEffectivenessID":4,"InspectionDescription":"Packing: Leak\nBonnet: Corroded\nBonnet Gasket: Worn\nValve Body: Corroded \nValve Trim: Worn\nBody Bolts & Nuts: Corroded\nExternal Condition: Corroded\nSeals: Worn","CurrentConditionLeakeageToAtmosphereID":3,"CurrentConditionFailureOfFunctionID":1,"CurrentConditionPassingAcrossValveID":1,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"29-04-2024 21:12:28","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'17', N'ImportAssetRegister', N'1', N'30-04-2024 14:14:14', N'An asset with the tag number WIE-LCV-3 already exists.', N'{"Id":0,"TagNo":"WIE-LCV-3","AssetName":"Level Control Valve outlet Test Separator","PlatformID":11,"ValveTypeID":5,"Size":"2","ClassRating":"600","ParentEquipmentNo":"PL-102-D-4","ParentEquipmentDescription":"Oil Level Control Valve outlet Test Separator V-01 (LCV-1)","InstallationDate":"1/1/2007 12:00:00AM","PIDNo":"WIE-11001 P2","Manufacturer":"FISHER","BodyModel":"Globe","BodyMaterial":"WCB","EndConnection":"RF","SerialNo":"N/A","ManualOverrideID":2,"ActuatorMfg":"FISHER","ActuatorSerialNo":"N/A","ActuatorTypeModel":"667","ActuatorPower":"N/A","OperatingTemperature":"100","OperatingPressure":"1440","FlowRate":"-","ServiceFluid":"Crude Oil","FluidPhaseID":1,"ToxicOrFlamableFluidID":1,"UsageType":"Control Valve","CostOfReplacementAndRepair":"-","Actuation":"Pneumatic","Status":"In-service","IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 14:14:14","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'18', N'ImportAssetRegister', N'1', N'30-04-2024 14:16:04', N'An asset with the tag number WIE-LCV-3 already exists.', N'{"Id":0,"TagNo":"WIE-LCV-3","AssetName":"Level Control Valve outlet Test Separator","PlatformID":11,"ValveTypeID":5,"Size":"2","ClassRating":"600","ParentEquipmentNo":"PL-102-D-4","ParentEquipmentDescription":"Oil Level Control Valve outlet Test Separator V-01 (LCV-1)","InstallationDate":"1/1/2007 12:00:00AM","PIDNo":"WIE-11001 P2","Manufacturer":"FISHER","BodyModel":"Globe","BodyMaterial":"WCB","EndConnection":"RF","SerialNo":"N/A","ManualOverrideID":2,"ActuatorMfg":"FISHER","ActuatorSerialNo":"N/A","ActuatorTypeModel":"667","ActuatorPower":"N/A","OperatingTemperature":"100","OperatingPressure":"1440","FlowRate":"-","ServiceFluid":"Crude Oil","FluidPhaseID":1,"ToxicOrFlamableFluidID":1,"UsageType":"Control Valve","CostOfReplacementAndRepair":"-","Actuation":"Pneumatic","Status":"In-service","IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 14:16:04","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'19', N'ImportAssetRegister', N'1', N'30-04-2024 14:19:16', N'An asset with the tag number WIE-LCV-3 already exists.', N'{"Id":0,"TagNo":"WIE-LCV-3","AssetName":"Level Control Valve outlet Test Separator","PlatformID":11,"ValveTypeID":5,"Size":"2","ClassRating":"600","ParentEquipmentNo":"PL-102-D-4","ParentEquipmentDescription":"Oil Level Control Valve outlet Test Separator V-01 (LCV-1)","InstallationDate":"1/1/2007 12:00:00AM","PIDNo":"WIE-11001 P2","Manufacturer":"FISHER","BodyModel":"Globe","BodyMaterial":"WCB","EndConnection":"RF","SerialNo":"N/A","ManualOverrideID":2,"ActuatorMfg":"FISHER","ActuatorSerialNo":"N/A","ActuatorTypeModel":"667","ActuatorPower":"N/A","OperatingTemperature":"100","OperatingPressure":"1440","FlowRate":"-","ServiceFluid":"Crude Oil","FluidPhaseID":1,"ToxicOrFlamableFluidID":1,"UsageType":"Control Valve","CostOfReplacementAndRepair":"-","Actuation":"Pneumatic","Status":"In-service","IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 14:19:16","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'20', N'ImportAssetRegister', N'1', N'30-04-2024 14:20:01', N'An asset with the tag number WIE-LCV-3 already exists.', N'{"Id":0,"TagNo":"WIE-LCV-3","AssetName":"Level Control Valve outlet Test Separator","PlatformID":11,"ValveTypeID":5,"Size":"2","ClassRating":"600","ParentEquipmentNo":"PL-102-D-4","ParentEquipmentDescription":"Oil Level Control Valve outlet Test Separator V-01 (LCV-1)","InstallationDate":"1/1/2007 12:00:00AM","PIDNo":"WIE-11001 P2","Manufacturer":"FISHER","BodyModel":"Globe","BodyMaterial":"WCB","EndConnection":"RF","SerialNo":"N/A","ManualOverrideID":2,"ActuatorMfg":"FISHER","ActuatorSerialNo":"N/A","ActuatorTypeModel":"667","ActuatorPower":"N/A","OperatingTemperature":"100","OperatingPressure":"1440","FlowRate":"-","ServiceFluid":"Crude Oil","FluidPhaseID":1,"ToxicOrFlamableFluidID":1,"UsageType":"Control Valve","CostOfReplacementAndRepair":"-","Actuation":"Pneumatic","Status":"In-service","IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 14:20:01","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'21', N'ImportAssetRegister', N'1', N'30-04-2024 14:20:16', N'An asset with the tag number WIE-LCV-3 already exists.', N'{"Id":0,"TagNo":"WIE-LCV-3","AssetName":"Level Control Valve outlet Test Separator","PlatformID":11,"ValveTypeID":5,"Size":"2","ClassRating":"600","ParentEquipmentNo":"PL-102-D-4","ParentEquipmentDescription":"Oil Level Control Valve outlet Test Separator V-01 (LCV-1)","InstallationDate":"1/1/2007 12:00:00AM","PIDNo":"WIE-11001 P2","Manufacturer":"FISHER","BodyModel":"Globe","BodyMaterial":"WCB","EndConnection":"RF","SerialNo":"N/A","ManualOverrideID":2,"ActuatorMfg":"FISHER","ActuatorSerialNo":"N/A","ActuatorTypeModel":"667","ActuatorPower":"N/A","OperatingTemperature":"100","OperatingPressure":"1440","FlowRate":"-","ServiceFluid":"Crude Oil","FluidPhaseID":1,"ToxicOrFlamableFluidID":1,"UsageType":"Control Valve","CostOfReplacementAndRepair":"-","Actuation":"Pneumatic","Status":"In-service","IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 14:20:16","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'22', N'ImportInspection', N'1', N'30-04-2024 14:22:20', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":53,"InspectionDate":"26-04-2022","InspectionMethodID":1,"InspectionEffectivenessID":4,"InspectionDescription":"Packing: Leak\nBonnet: Corroded\nBonnet Gasket: Worn\nValve Body: Corroded \nValve Trim: Worn\nBody Bolts & Nuts: Corroded\nExternal Condition: Corroded\nSeals: Worn","CurrentConditionLeakeageToAtmosphereID":3,"CurrentConditionFailureOfFunctionID":1,"CurrentConditionPassingAcrossValveID":1,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 14:22:20","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'23', N'ImportAssetRegister', N'1', N'30-04-2024 14:41:06', N'Could not convert string to integer: Level Control Valve. Path ''ValveTypeID'', line 1, position 317.', N'{"Id":0,"TagNo":null,"AssetName":null,"PlatformID":null,"ValveTypeID":null,"Size":null,"ClassRating":null,"ParentEquipmentNo":null,"ParentEquipmentDescription":null,"InstallationDate":null,"PIDNo":null,"Manufacturer":null,"BodyModel":null,"BodyMaterial":null,"EndConnection":null,"SerialNo":null,"ManualOverrideID":null,"ActuatorMfg":null,"ActuatorSerialNo":null,"ActuatorTypeModel":null,"ActuatorPower":null,"OperatingTemperature":null,"OperatingPressure":null,"FlowRate":null,"ServiceFluid":null,"FluidPhaseID":null,"ToxicOrFlamableFluidID":null,"UsageType":null,"CostOfReplacementAndRepair":null,"Actuation":null,"Status":null,"IsDeleted":null,"CreatedBy":null,"CreatedAt":null,"DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'24', N'ImportAssetRegister', N'1', N'30-04-2024 14:42:03', N'Could not convert string to integer: Level Control Valve. Path ''ValveTypeID'', line 1, position 317.', N'{"Id":0,"TagNo":null,"AssetName":null,"PlatformID":null,"ValveTypeID":null,"Size":null,"ClassRating":null,"ParentEquipmentNo":null,"ParentEquipmentDescription":null,"InstallationDate":null,"PIDNo":null,"Manufacturer":null,"BodyModel":null,"BodyMaterial":null,"EndConnection":null,"SerialNo":null,"ManualOverrideID":null,"ActuatorMfg":null,"ActuatorSerialNo":null,"ActuatorTypeModel":null,"ActuatorPower":null,"OperatingTemperature":null,"OperatingPressure":null,"FlowRate":null,"ServiceFluid":null,"FluidPhaseID":null,"ToxicOrFlamableFluidID":null,"UsageType":null,"CostOfReplacementAndRepair":null,"Actuation":null,"Status":null,"IsDeleted":null,"CreatedBy":null,"CreatedAt":null,"DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'25', N'ImportAssessment', N'1', N'30-04-2024 15:09:36', N'An assessment with the same asset and date already exists', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"29-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"30","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"40","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":2,"LeakageToAtmosphereTP2ID":3,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":3,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":null,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"IsDeleted":false,"CreatedAt":"30-04-2024 15:09:36","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'26', N'ImportAssessment', N'1', N'30-04-2024 15:11:34', N'An assessment with the same asset and date already exists', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"29-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"30","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"40","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":2,"LeakageToAtmosphereTP2ID":3,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":3,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":null,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"IsDeleted":false,"CreatedAt":"30-04-2024 15:11:34","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'27', N'ImportAssessment', N'1', N'30-04-2024 15:13:52', N'An assessment with the same asset and date already exists', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"29-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"30","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"40","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":2,"LeakageToAtmosphereTP2ID":3,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":3,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":null,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"IsDeleted":false,"CreatedAt":"30-04-2024 15:13:52","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'28', N'ImportAssessment', N'1', N'30-04-2024 15:14:34', N'An assessment with the same asset and date already exists', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"29-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"30","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"40","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":2,"LeakageToAtmosphereTP2ID":3,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":3,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":null,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"IsDeleted":false,"CreatedAt":"30-04-2024 15:14:34","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'29', N'ImportAssessment', N'1', N'30-04-2024 15:15:14', N'An assessment with the same asset and date already exists', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"29-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"30","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"40","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":2,"LeakageToAtmosphereTP2ID":3,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":3,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":null,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"IsDeleted":false,"CreatedAt":"30-04-2024 15:15:14","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'30', N'ImportAssessment', N'1', N'30-04-2024 15:15:26', N'An assessment with the same asset and date already exists', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"29-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"30","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"40","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":2,"LeakageToAtmosphereTP2ID":3,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":3,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":null,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"IsDeleted":false,"CreatedAt":"30-04-2024 15:15:26","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'31', N'ImportMaintenance', N'1', N'30-04-2024 15:19:12', N'Maintenance with the same asset and date already exist', N'{"Id":0,"AssetID":1,"IsValveRepairedID":1,"MaintenanceDate":"01-06-2023","MaintenanceDescription":"Refurbish valve body, gear box and replace bolts and nuts.","IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:19:12","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'32', N'ImportMaintenance', N'1', N'30-04-2024 15:19:12', N'Maintenance with the same asset and date already exist', N'{"Id":0,"AssetID":1,"IsValveRepairedID":1,"MaintenanceDate":"02-06-2023","MaintenanceDescription":"Refurbish valve body, gear box and replace bolts and nuts.","IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:19:12","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'33', N'ImportMaintenance', N'1', N'30-04-2024 15:19:12', N'Maintenance with the same asset and date already exist', N'{"Id":0,"AssetID":1,"IsValveRepairedID":1,"MaintenanceDate":"02-06-2023","MaintenanceDescription":"Refurbish valve body, gear box and replace bolts and nuts.","IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:19:12","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'34', N'ImportMaintenance', N'1', N'30-04-2024 15:19:12', N'Maintenance with the same asset and date already exist', N'{"Id":0,"AssetID":1,"IsValveRepairedID":1,"MaintenanceDate":"03-06-2023","MaintenanceDescription":"Refurbish valve body, gear box and replace bolts and nuts.","IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:19:12","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'35', N'ImportMaintenance', N'1', N'30-04-2024 15:19:45', N'Could not convert string to integer: Guard. Path ''IsValveRepairedID'', line 1, position 159.', N'{"Id":0,"AssetID":0,"IsValveRepairedID":null,"MaintenanceDate":null,"MaintenanceDescription":null,"IsDeleted":null,"CreatedBy":null,"CreatedAt":null,"DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'36', N'ImportMaintenance', N'1', N'30-04-2024 15:19:45', N'Maintenance with the same asset and date already exist', N'{"Id":0,"AssetID":1,"IsValveRepairedID":1,"MaintenanceDate":"02-06-2023","MaintenanceDescription":"Refurbish valve body, gear box and replace bolts and nuts.","IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:19:45","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'37', N'ImportMaintenance', N'1', N'30-04-2024 15:19:45', N'Maintenance with the same asset and date already exist', N'{"Id":0,"AssetID":1,"IsValveRepairedID":1,"MaintenanceDate":"02-06-2023","MaintenanceDescription":"Refurbish valve body, gear box and replace bolts and nuts.","IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:19:45","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'38', N'ImportMaintenance', N'1', N'30-04-2024 15:19:45', N'Maintenance with the same asset and date already exist', N'{"Id":0,"AssetID":1,"IsValveRepairedID":1,"MaintenanceDate":"03-06-2023","MaintenanceDescription":"Refurbish valve body, gear box and replace bolts and nuts.","IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:19:45","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'39', N'ImportInspection', N'1', N'30-04-2024 15:23:34', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"11-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":1,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":3,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:23:34","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'40', N'ImportInspection', N'1', N'30-04-2024 15:23:34', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"12-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":2,"CurrentConditionFailureOfFunctionID":1,"CurrentConditionPassingAcrossValveID":3,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:23:34","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'41', N'ImportInspection', N'1', N'30-04-2024 15:23:34', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"12-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":1,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":2,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:23:34","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'42', N'ImportInspection', N'1', N'30-04-2024 15:23:34', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"14-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":2,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":3,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:23:34","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'43', N'ImportInspection', N'1', N'30-04-2024 15:23:47', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"11-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":1,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":3,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:23:47","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'44', N'ImportInspection', N'1', N'30-04-2024 15:23:47', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"12-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":2,"CurrentConditionFailureOfFunctionID":1,"CurrentConditionPassingAcrossValveID":3,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:23:47","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'45', N'ImportInspection', N'1', N'30-04-2024 15:23:47', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"12-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":1,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":2,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:23:47","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'46', N'ImportInspection', N'1', N'30-04-2024 15:23:47', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"14-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":2,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":3,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:23:47","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'47', N'ImportInspection', N'1', N'30-04-2024 15:24:10', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"12-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":2,"CurrentConditionFailureOfFunctionID":1,"CurrentConditionPassingAcrossValveID":3,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:24:10","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'48', N'ImportInspection', N'1', N'30-04-2024 15:24:10', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"12-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":1,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":2,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:24:10","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'49', N'ImportInspection', N'1', N'30-04-2024 15:24:10', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"14-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":2,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":3,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:24:10","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'50', N'ImportInspection', N'1', N'30-04-2024 15:24:39', N'Could not convert string to integer: Tes haha. Path ''CurrentConditionPassingAcrossValveID'', line 1, position 383.', N'{"Id":0,"AssetID":0,"InspectionDate":null,"InspectionMethodID":0,"InspectionEffectivenessID":0,"InspectionDescription":null,"CurrentConditionLeakeageToAtmosphereID":0,"CurrentConditionFailureOfFunctionID":0,"CurrentConditionPassingAcrossValveID":0,"FunctionCondition":null,"TestPressureIfAny":null,"IsDeleted":null,"CreatedBy":null,"CreatedAt":null,"DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'51', N'ImportInspection', N'1', N'30-04-2024 15:24:39', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"12-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":2,"CurrentConditionFailureOfFunctionID":1,"CurrentConditionPassingAcrossValveID":3,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:24:39","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'52', N'ImportInspection', N'1', N'30-04-2024 15:24:39', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"12-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":1,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":2,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:24:39","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'53', N'ImportInspection', N'1', N'30-04-2024 15:24:39', N'Inspection already exists for this asset on this date', N'{"Id":0,"AssetID":1,"InspectionDate":"14-06-2022","InspectionMethodID":1,"InspectionEffectivenessID":1,"InspectionDescription":"Packing, Valve Trim: Worn\nBonnet Gasket, Bonnet : N/A\nValve Body, Bolts & Nuts: Heavy Corroded \nGear Box : Corroded","CurrentConditionLeakeageToAtmosphereID":2,"CurrentConditionFailureOfFunctionID":2,"CurrentConditionPassingAcrossValveID":3,"FunctionCondition":"Packing Leak","TestPressureIfAny":null,"IsDeleted":false,"CreatedBy":1,"CreatedAt":"30-04-2024 15:24:39","DeletedBy":null,"DeletedAt":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'54', N'ImportAssessment', N'1', N'07-05-2024 17:35:47', N'Could not convert string to integer: Moderate or significant injury or health effect. Path ''HSSEDefinisionID'', line 1, position 416.', N'{"Id":0,"AssetID":0,"AssessmentNo":null,"AssessmentDate":null,"TimePeriode":null,"TimeToLimitStateLeakageToAtmosphere":null,"TimeToLimitStateFailureOfFunction":null,"TimeToLimitStatePassingAccrosValve":null,"LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":null,"LeakageToAtmosphereTP2ID":null,"LeakageToAtmosphereTP3ID":null,"FailureOfFunctionTP1ID":null,"FailureOfFunctionTP2ID":null,"FailureOfFunctionTP3ID":null,"PassingAccrosValveTP1ID":null,"PassingAccrosValveTP2ID":null,"PassingAccrosValveTP3ID":null,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":null,"ImpactOfOperatingEnvelopesID":null,"UsedWithinOEMSpecificationID":null,"RepairedID":null,"ProductLossDefinition":null,"HSSEDefinisionID":null,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"IsDeleted":null,"CreatedAt":null,"CreatedBy":null,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'55', N'ImportAssessment', N'1', N'07-05-2024 17:37:21', N'An assessment with the same asset and date already exists', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"29-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"30","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"40","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":2,"LeakageToAtmosphereTP2ID":3,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":3,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":3,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"IsDeleted":false,"CreatedAt":"07-05-2024 17:37:21","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'56', N'ImportAssessment', N'1', N'09-05-2024 13:20:33', N'An error occurred while saving the entity changes. See the inner exception for details.', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"30-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"40","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"60","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":1,"LeakageToAtmosphereTP2ID":2,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":2,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":3,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"LoFScoreLeakageToAtmophereTP1":null,"LoFScoreLeakageToAtmophereTP2":null,"LoFScoreLeakageToAtmophereTP3":null,"LoFScoreFailureOfFunctionTP1":null,"LoFScoreFailureOfFunctionTP2":null,"LoFScoreFailureOfFunctionTP3":null,"LoFScorePassingAccrosValveTP1":null,"LoFScorePassingAccrosValveTP2":null,"LoFScorePassingAccrosValveTP3":null,"CoFScore":null,"IsDeleted":false,"CreatedAt":"09-05-2024 13:20:33","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'57', N'ImportAssessment', N'1', N'09-05-2024 14:01:05', N'An error occurred while saving the entity changes. See the inner exception for details.', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"30-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"40","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"60","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":1,"LeakageToAtmosphereTP2ID":2,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":2,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":3,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"LoFScoreLeakageToAtmophereTP1":null,"LoFScoreLeakageToAtmophereTP2":null,"LoFScoreLeakageToAtmophereTP3":null,"LoFScoreFailureOfFunctionTP1":null,"LoFScoreFailureOfFunctionTP2":null,"LoFScoreFailureOfFunctionTP3":null,"LoFScorePassingAccrosValveTP1":null,"LoFScorePassingAccrosValveTP2":null,"LoFScorePassingAccrosValveTP3":null,"CoFScore":null,"IsDeleted":false,"CreatedAt":"09-05-2024 14:01:05","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'58', N'ImportAssessment', N'1', N'09-05-2024 14:01:20', N'An error occurred while saving the entity changes. See the inner exception for details.', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"30-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"40","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"60","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":1,"LeakageToAtmosphereTP2ID":2,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":2,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":3,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"LoFScoreLeakageToAtmophereTP1":null,"LoFScoreLeakageToAtmophereTP2":null,"LoFScoreLeakageToAtmophereTP3":null,"LoFScoreFailureOfFunctionTP1":null,"LoFScoreFailureOfFunctionTP2":null,"LoFScoreFailureOfFunctionTP3":null,"LoFScorePassingAccrosValveTP1":null,"LoFScorePassingAccrosValveTP2":null,"LoFScorePassingAccrosValveTP3":null,"CoFScore":null,"IsDeleted":false,"CreatedAt":"09-05-2024 14:01:20","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'59', N'ImportAssessment', N'1', N'09-05-2024 14:03:12', N'An error occurred while saving the entity changes. See the inner exception for details.', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"30-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"40","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"60","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":1,"LeakageToAtmosphereTP2ID":2,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":2,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":3,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"LoFScoreLeakageToAtmophereTP1":null,"LoFScoreLeakageToAtmophereTP2":null,"LoFScoreLeakageToAtmophereTP3":null,"LoFScoreFailureOfFunctionTP1":null,"LoFScoreFailureOfFunctionTP2":null,"LoFScoreFailureOfFunctionTP3":null,"LoFScorePassingAccrosValveTP1":null,"LoFScorePassingAccrosValveTP2":null,"LoFScorePassingAccrosValveTP3":null,"CoFScore":null,"IsDeleted":false,"CreatedAt":"09-05-2024 14:03:12","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'60', N'ImportAssessment', N'1', N'09-05-2024 14:08:14', N'An error occurred while saving the entity changes. See the inner exception for details.', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"30-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"40","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"60","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":1,"LeakageToAtmosphereTP2ID":2,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":2,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":3,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"LoFScoreLeakageToAtmophereTP1":null,"LoFScoreLeakageToAtmophereTP2":null,"LoFScoreLeakageToAtmophereTP3":null,"LoFScoreFailureOfFunctionTP1":null,"LoFScoreFailureOfFunctionTP2":null,"LoFScoreFailureOfFunctionTP3":null,"LoFScorePassingAccrosValveTP1":null,"LoFScorePassingAccrosValveTP2":null,"LoFScorePassingAccrosValveTP3":null,"CoFScore":null,"IsDeleted":false,"CreatedAt":"09-05-2024 14:08:14","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'61', N'ImportAssessment', N'1', N'09-05-2024 14:08:56', N'An error occurred while saving the entity changes. See the inner exception for details.', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"30-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"40","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"60","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":1,"LeakageToAtmosphereTP2ID":2,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":2,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":3,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"LoFScoreLeakageToAtmophereTP1":null,"LoFScoreLeakageToAtmophereTP2":null,"LoFScoreLeakageToAtmophereTP3":null,"LoFScoreFailureOfFunctionTP1":null,"LoFScoreFailureOfFunctionTP2":null,"LoFScoreFailureOfFunctionTP3":null,"LoFScorePassingAccrosValveTP1":null,"LoFScorePassingAccrosValveTP2":null,"LoFScorePassingAccrosValveTP3":null,"CoFScore":null,"IsDeleted":false,"CreatedAt":"09-05-2024 14:08:56","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'62', N'ImportAssessment', N'1', N'09-05-2024 14:11:13', N'An error occurred while saving the entity changes. See the inner exception for details.', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"30-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"40","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"60","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":1,"LeakageToAtmosphereTP2ID":2,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":2,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":3,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"LoFScoreLeakageToAtmophereTP1":null,"LoFScoreLeakageToAtmophereTP2":null,"LoFScoreLeakageToAtmophereTP3":null,"LoFScoreFailureOfFunctionTP1":null,"LoFScoreFailureOfFunctionTP2":null,"LoFScoreFailureOfFunctionTP3":null,"LoFScorePassingAccrosValveTP1":null,"LoFScorePassingAccrosValveTP2":null,"LoFScorePassingAccrosValveTP3":null,"CoFScore":null,"IsDeleted":false,"CreatedAt":"09-05-2024 14:11:12","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'63', N'ImportAssessment', N'1', N'09-05-2024 14:14:53', N'The instance of entity type ''AssessmentDB'' cannot be tracked because another instance with the same key value for {''Id''} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using ''DbContextOptionsBuilder.EnableSensitiveDataLogging'' to see the conflicting key values.', N'{"Id":83,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"30-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"40","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"60","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":1,"LeakageToAtmosphereTP2ID":2,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":2,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":3,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"LoFScoreLeakageToAtmophereTP1":null,"LoFScoreLeakageToAtmophereTP2":null,"LoFScoreLeakageToAtmophereTP3":null,"LoFScoreFailureOfFunctionTP1":null,"LoFScoreFailureOfFunctionTP2":null,"LoFScoreFailureOfFunctionTP3":null,"LoFScorePassingAccrosValveTP1":null,"LoFScorePassingAccrosValveTP2":null,"LoFScorePassingAccrosValveTP3":null,"CoFScore":null,"IsDeleted":false,"CreatedAt":"09-05-2024 14:14:53","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
GO

INSERT INTO [dbo].[Log] ([Id], [Module], [CreatedBy], [CreatedAt], [Message], [Data]) VALUES (N'64', N'ImportAssessment', N'1', N'09-05-2024 14:19:19', N'An assessment with the same asset and date already exists', N'{"Id":0,"AssetID":52,"AssessmentNo":"IMPORT","AssessmentDate":"30-04-2024","TimePeriode":"18","TimeToLimitStateLeakageToAtmosphere":"40","TimeToLimitStateFailureOfFunction":"40","TimeToLimitStatePassingAccrosValve":"60","LeakageToAtmosphereID":null,"FailureOfFunctionID":null,"PassingAccrosValveID":null,"LeakageToAtmosphereTP1ID":1,"LeakageToAtmosphereTP2ID":2,"LeakageToAtmosphereTP3ID":3,"FailureOfFunctionTP1ID":1,"FailureOfFunctionTP2ID":2,"FailureOfFunctionTP3ID":3,"PassingAccrosValveTP1ID":1,"PassingAccrosValveTP2ID":2,"PassingAccrosValveTP3ID":2,"InspectionEffectivenessID":null,"ImpactOfInternalFluidImpuritiesID":1,"ImpactOfOperatingEnvelopesID":2,"UsedWithinOEMSpecificationID":2,"RepairedID":1,"ProductLossDefinition":"250","HSSEDefinisionID":3,"Summary":null,"RecommendationActionID":null,"DetailedRecommendation":null,"ConsequenceOfFailure":null,"TP1A":null,"TP2A":null,"TP3A":null,"TP1B":null,"TP2B":null,"TP3B":null,"TP1C":null,"TP2C":null,"TP3C":null,"TPTimeToActionA":null,"TPTimeToActionB":null,"TPTimeToActionC":null,"TP1Risk":null,"TP2Risk":null,"TP3Risk":null,"TPTimeToActionRisk":null,"LoFScoreLeakageToAtmophereTP1":null,"LoFScoreLeakageToAtmophereTP2":null,"LoFScoreLeakageToAtmophereTP3":null,"LoFScoreFailureOfFunctionTP1":null,"LoFScoreFailureOfFunctionTP2":null,"LoFScoreFailureOfFunctionTP3":null,"LoFScorePassingAccrosValveTP1":null,"LoFScorePassingAccrosValveTP2":null,"LoFScorePassingAccrosValveTP3":null,"CoFScore":null,"IsDeleted":false,"CreatedAt":"09-05-2024 14:19:19","CreatedBy":1,"DeletedAt":null,"DeletedBy":null}')
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

INSERT INTO [dbo].[Maintenance] ([Id], [AssetID], [IsValveRepairedID], [MaintenanceDate], [MaintenanceDescription], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'23', N'1', N'1', N'01-06-2023', N'Refurbish valve body, gear box and replace bolts and nuts.', N'0', N'1', N'21-04-2024 22:13:19', NULL, NULL)
GO

INSERT INTO [dbo].[Maintenance] ([Id], [AssetID], [IsValveRepairedID], [MaintenanceDate], [MaintenanceDescription], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'26', N'1', N'1', N'02-06-2023', N'Refurbish valve body, gear box and replace bolts and nuts.', N'0', N'1', N'22-04-2024 10:18:47', NULL, NULL)
GO

INSERT INTO [dbo].[Maintenance] ([Id], [AssetID], [IsValveRepairedID], [MaintenanceDate], [MaintenanceDescription], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'27', N'1', N'1', N'03-06-2023', N'Refurbish valve body, gear box and replace bolts and nuts.', N'0', N'1', N'22-04-2024 10:18:47', NULL, NULL)
GO

INSERT INTO [dbo].[Maintenance] ([Id], [AssetID], [IsValveRepairedID], [MaintenanceDate], [MaintenanceDescription], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'28', N'49', N'1', N'29-04-2024', N'tes', N'0', N'1', N'29-04-2024 11:46:05', NULL, NULL)
GO

INSERT INTO [dbo].[Maintenance] ([Id], [AssetID], [IsValveRepairedID], [MaintenanceDate], [MaintenanceDescription], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'29', N'1', N'1', N'02-05-2024', N'sada', N'0', N'1', N'02-05-2024 15:02:48', NULL, NULL)
GO

INSERT INTO [dbo].[Maintenance] ([Id], [AssetID], [IsValveRepairedID], [MaintenanceDate], [MaintenanceDescription], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'30', N'52', N'1', N'08-05-2024', N'sdfghjk', N'0', N'1', N'09-05-2024 14:22:44', NULL, NULL)
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

INSERT INTO [dbo].[Platform] ([ID], [AreaID], [Platform], [Code], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'5', N'1', N'AIDA-A', N'', N'0', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Platform] ([ID], [AreaID], [Platform], [Code], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'10', N'1', N'AIDA-B', N'', N'0', NULL, N'31-03-2024 19:26:23', NULL, NULL)
GO

INSERT INTO [dbo].[Platform] ([ID], [AreaID], [Platform], [Code], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'11', N'2', N'WIDURI-E', N'WDE', N'0', N'1', N'29-04-2024 14:41:36', NULL, NULL)
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

INSERT INTO [dbo].[UsedWithinOEMSpecification] ([ID], [UsedWithinOEMSpecification], [UsedWithinOEMSpecificationValue], [Weighting]) VALUES (N'1', N'Significantly Below Specification', N'1', N'3')
GO

INSERT INTO [dbo].[UsedWithinOEMSpecification] ([ID], [UsedWithinOEMSpecification], [UsedWithinOEMSpecificationValue], [Weighting]) VALUES (N'2', N'Within Specification Range', N'2.67', N'3')
GO

INSERT INTO [dbo].[UsedWithinOEMSpecification] ([ID], [UsedWithinOEMSpecification], [UsedWithinOEMSpecificationValue], [Weighting]) VALUES (N'3', N'Above Specification Limit', N'6', N'3')
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

INSERT INTO [dbo].[User] ([ID], [Username], [Password], [Role], [IsAdmin], [IsEngineer], [IsViewer], [IsDeleted], [CreatedBy], [CreatedAt], [DeletedBy], [DeletedAt]) VALUES (N'1', N'Aldo', N'$2a$11$O5bjbyUQrpEfRBROnnCAW.VNgr.NDxWXc.9U1VlZNyGg7qoZuPvZa', N'Surface Facility Admin', N'1', N'1', N'1', N'0', NULL, NULL, NULL, NULL)
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
DBCC CHECKIDENT ('[dbo].[Area]', RESEED, 18)
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
DBCC CHECKIDENT ('[dbo].[Assessment]', RESEED, 84)
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
DBCC CHECKIDENT ('[dbo].[AssessmentInspection]', RESEED, 6)
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
DBCC CHECKIDENT ('[dbo].[AssessmentMaintenance]', RESEED, 6)
GO


-- ----------------------------
-- Auto increment value for Asset
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Asset]', RESEED, 60)
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
DBCC CHECKIDENT ('[dbo].[Inspection]', RESEED, 84)
GO


-- ----------------------------
-- Uniques structure for table Inspection
-- ----------------------------
ALTER TABLE [dbo].[Inspection] ADD CONSTRAINT [UC_Inspection] UNIQUE NONCLUSTERED ([InspectionDate] ASC, [AssetID] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


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
DBCC CHECKIDENT ('[dbo].[InspectionFile]', RESEED, 5)
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
DBCC CHECKIDENT ('[dbo].[Log]', RESEED, 64)
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
DBCC CHECKIDENT ('[dbo].[Maintenance]', RESEED, 30)
GO


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
DBCC CHECKIDENT ('[dbo].[Platform]', RESEED, 11)
GO


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
ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_ImpactEffect2] FOREIGN KEY ([ImpactOfOperatingEnvelopesID]) REFERENCES [dbo].[ImpactEffect] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState4] FOREIGN KEY ([FailureOfFunctionTP1ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_Asset] FOREIGN KEY ([AssetID]) REFERENCES [dbo].[Asset] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState9] FOREIGN KEY ([PassingAccrosValveTP3ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assessment_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState5] FOREIGN KEY ([FailureOfFunctionTP2ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_HSSEDefinision] FOREIGN KEY ([HSSEDefinisionID]) REFERENCES [dbo].[HSSEDefinision] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_RecomendedAction] FOREIGN KEY ([RecommendationActionID]) REFERENCES [dbo].[RecommendationAction] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState6] FOREIGN KEY ([FailureOfFunctionTP3ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_ImpactEffect] FOREIGN KEY ([ImpactOfInternalFluidImpuritiesID]) REFERENCES [dbo].[ImpactEffect] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_CurrentConditionLimitState2] FOREIGN KEY ([FailureOfFunctionID]) REFERENCES [dbo].[CurrentConditionLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState] FOREIGN KEY ([LeakageToAtmosphereTP1ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState7] FOREIGN KEY ([PassingAccrosValveTP1ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState2] FOREIGN KEY ([LeakageToAtmosphereTP2ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState8] FOREIGN KEY ([PassingAccrosValveTP2ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_Repaired] FOREIGN KEY ([RepairedID]) REFERENCES [dbo].[Repaired] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_CurrentConditionLimitState3] FOREIGN KEY ([PassingAccrosValveID]) REFERENCES [dbo].[CurrentConditionLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_TimeToLimitState3] FOREIGN KEY ([LeakageToAtmosphereTP3ID]) REFERENCES [dbo].[TimeToLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_UsedWithinOEMSpecification] FOREIGN KEY ([UsedWithinOEMSpecificationID]) REFERENCES [dbo].[UsedWithinOEMSpecification] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_InspectionEffectiveness] FOREIGN KEY ([InspectionEffectivenessID]) REFERENCES [dbo].[InspectionEffectiveness] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assesment_CurrentConditionLimitState] FOREIGN KEY ([LeakageToAtmosphereID]) REFERENCES [dbo].[CurrentConditionLimitState] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Assessment] ADD CONSTRAINT [FK_Assessment_User2] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AssessmentInspection
-- ----------------------------
ALTER TABLE [dbo].[AssessmentInspection] ADD CONSTRAINT [FK_AssessmentInspection_Assessment] FOREIGN KEY ([AssessmentID]) REFERENCES [dbo].[Assessment] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[AssessmentInspection] ADD CONSTRAINT [FK_AssessmentInspection_Inspection] FOREIGN KEY ([InspectionID]) REFERENCES [dbo].[Inspection] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AssessmentMaintenance
-- ----------------------------
ALTER TABLE [dbo].[AssessmentMaintenance] ADD CONSTRAINT [FK_AssessmentMaintenance_Assessment] FOREIGN KEY ([AssessmentID]) REFERENCES [dbo].[Assessment] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[AssessmentMaintenance] ADD CONSTRAINT [FK_AssessmentMaintenance_Maintenance] FOREIGN KEY ([MaintenanceID]) REFERENCES [dbo].[Maintenance] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
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

