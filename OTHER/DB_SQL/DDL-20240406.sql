-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- Riskvalve.dbo.CurrentConditionLimitState definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.CurrentConditionLimitState;

CREATE TABLE Riskvalve.dbo.CurrentConditionLimitState (
	ID int IDENTITY(1,1) NOT NULL,
	CurrentConditionLimitState nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_CurrentConditionLimitState PRIMARY KEY (ID)
);


-- Riskvalve.dbo.FluidPhase definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.FluidPhase;

CREATE TABLE Riskvalve.dbo.FluidPhase (
	ID int IDENTITY(1,1) NOT NULL,
	FluidPhase nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_FluidPhase PRIMARY KEY (ID)
);


-- Riskvalve.dbo.HSSEDefinision definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.HSSEDefinision;

CREATE TABLE Riskvalve.dbo.HSSEDefinision (
	ID int IDENTITY(1,1) NOT NULL,
	HSSEDefinision nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_HSSEDefinision PRIMARY KEY (ID)
);


-- Riskvalve.dbo.ImpactEffect definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.ImpactEffect;

CREATE TABLE Riskvalve.dbo.ImpactEffect (
	ID int IDENTITY(1,1) NOT NULL,
	ImpactEffect nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_ImpactEffect PRIMARY KEY (ID)
);


-- Riskvalve.dbo.InspectionEffectiveness definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.InspectionEffectiveness;

CREATE TABLE Riskvalve.dbo.InspectionEffectiveness (
	ID int IDENTITY(1,1) NOT NULL,
	Effectiveness nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_InspectionEffectiveness PRIMARY KEY (ID)
);


-- Riskvalve.dbo.InspectionMethod definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.InspectionMethod;

CREATE TABLE Riskvalve.dbo.InspectionMethod (
	ID int IDENTITY(1,1) NOT NULL,
	InspectionMethod nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_InspectionMethod PRIMARY KEY (ID)
);


-- Riskvalve.dbo.IsValveRepaired definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.IsValveRepaired;

CREATE TABLE Riskvalve.dbo.IsValveRepaired (
	ID int IDENTITY(1,1) NOT NULL,
	IsValveRepaired nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_IsValveRepaired PRIMARY KEY (ID)
);


-- Riskvalve.dbo.ManualOverride definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.ManualOverride;

CREATE TABLE Riskvalve.dbo.ManualOverride (
	ID int IDENTITY(1,1) NOT NULL,
	ManualOverride nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_ManualOverride PRIMARY KEY (ID)
);


-- Riskvalve.dbo.RecomendedAction definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.RecomendedAction;

CREATE TABLE Riskvalve.dbo.RecomendedAction (
	ID int IDENTITY(1,1) NOT NULL,
	RecomendedAction nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_RecomendedAction PRIMARY KEY (ID)
);


-- Riskvalve.dbo.Repaired definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.Repaired;

CREATE TABLE Riskvalve.dbo.Repaired (
	ID int IDENTITY(1,1) NOT NULL,
	Repaired nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Repaired PRIMARY KEY (ID)
);


-- Riskvalve.dbo.TimeToLimitState definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.TimeToLimitState;

CREATE TABLE Riskvalve.dbo.TimeToLimitState (
	ID int IDENTITY(1,1) NOT NULL,
	TimeToLimitState nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_TimeToLimitState PRIMARY KEY (ID)
);


-- Riskvalve.dbo.ToxicOrFlamableFluid definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.ToxicOrFlamableFluid;

CREATE TABLE Riskvalve.dbo.ToxicOrFlamableFluid (
	ID int IDENTITY(1,1) NOT NULL,
	ToxicOrFlamableFluid nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_ToxicOrFlamableFluid PRIMARY KEY (ID)
);


-- Riskvalve.dbo.UsedWithinOEMSpecification definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.UsedWithinOEMSpecification;

CREATE TABLE Riskvalve.dbo.UsedWithinOEMSpecification (
	ID int IDENTITY(1,1) NOT NULL,
	UsedWithinOEMSpecification nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_UsedWithinOEMSpecification PRIMARY KEY (ID)
);


-- Riskvalve.dbo.ValveType definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.ValveType;

CREATE TABLE Riskvalve.dbo.ValveType (
	ID int IDENTITY(1,1) NOT NULL,
	ValveType nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_ValveType PRIMARY KEY (ID)
);


-- Riskvalve.dbo.[User] definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.[User];

CREATE TABLE Riskvalve.dbo.[User] (
	ID int IDENTITY(1,1) NOT NULL,
	Username nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Password nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Role] nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsAdmin bit DEFAULT 0 NOT NULL,
	IsEngineer bit DEFAULT 0 NOT NULL,
	IsViewer bit DEFAULT 0 NOT NULL,
	IsDeleted bit DEFAULT 0 NOT NULL,
	CreatedBy int NULL,
	CreatedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DeletedBy int NULL,
	DeletedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_User PRIMARY KEY (ID),
	CONSTRAINT FK_User_User FOREIGN KEY (CreatedBy) REFERENCES Riskvalve.dbo.[User](ID),
	CONSTRAINT FK_User_User_1 FOREIGN KEY (DeletedBy) REFERENCES Riskvalve.dbo.[User](ID)
);
 CREATE  UNIQUE NONCLUSTERED INDEX Index_User_1 ON dbo.User (  Username ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- Riskvalve.dbo.Area definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.Area;

CREATE TABLE Riskvalve.dbo.Area (
	ID int IDENTITY(1,1) NOT NULL,
	BusinessArea nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsDeleted bit DEFAULT 0 NOT NULL,
	CreatedBy int NULL,
	CreatedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DeletedBy int NULL,
	DeletedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Area PRIMARY KEY (ID),
	CONSTRAINT FK_Area_User FOREIGN KEY (CreatedBy) REFERENCES Riskvalve.dbo.[User](ID),
	CONSTRAINT FK_Area_User2 FOREIGN KEY (DeletedBy) REFERENCES Riskvalve.dbo.[User](ID)
);


-- Riskvalve.dbo.Platform definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.Platform;

CREATE TABLE Riskvalve.dbo.Platform (
	ID int IDENTITY(1,1) NOT NULL,
	AreaID int NULL,
	Platform nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Code nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsDeleted bit DEFAULT 0 NOT NULL,
	CreatedBy int NULL,
	CreatedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DeletedBy int NULL,
	DeletedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Platform PRIMARY KEY (ID),
	CONSTRAINT FK_Platform_Area FOREIGN KEY (AreaID) REFERENCES Riskvalve.dbo.Area(ID),
	CONSTRAINT FK_Platform_User FOREIGN KEY (CreatedBy) REFERENCES Riskvalve.dbo.[User](ID),
	CONSTRAINT FK_Platform_User2 FOREIGN KEY (DeletedBy) REFERENCES Riskvalve.dbo.[User](ID)
);


-- Riskvalve.dbo.Asset definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.Asset;

CREATE TABLE Riskvalve.dbo.Asset (
	ID int IDENTITY(1,1) NOT NULL,
	TagNo nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PlatformID int NULL,
	ValveTypeID int NULL,
	[Size] nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ClassRating nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ParentEquipmentNo nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ParentEquipmentDescription nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	InstallationDate nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PIDNo nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Manufacturer nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	BodyModel nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	BodyMaterial nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	EndConnection nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SerialNo nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ManualOverrideID int NULL,
	ActuatorMfg nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ActuatorSerialNo nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ActuatorTypeModel nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ActuatorPower nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	OperatingTemperature nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	OperatingPressure nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FlowRate nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ServiceFluid nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FluidPhaseID int NULL,
	ToxicOrFlamableFluidID int NULL,
	IsDeleted bit DEFAULT 0 NOT NULL,
	CreatedBy int NULL,
	CreatedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DeletedBy int NULL,
	DeletedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Asset PRIMARY KEY (ID),
	CONSTRAINT FK_Asset_FluidPhase_1 FOREIGN KEY (FluidPhaseID) REFERENCES Riskvalve.dbo.FluidPhase(ID),
	CONSTRAINT FK_Asset_ManualOverride FOREIGN KEY (ManualOverrideID) REFERENCES Riskvalve.dbo.ManualOverride(ID),
	CONSTRAINT FK_Asset_Platform FOREIGN KEY (PlatformID) REFERENCES Riskvalve.dbo.Platform(ID),
	CONSTRAINT FK_Asset_ToxicOrFlamableFluid_2 FOREIGN KEY (ToxicOrFlamableFluidID) REFERENCES Riskvalve.dbo.ToxicOrFlamableFluid(ID),
	CONSTRAINT FK_Asset_ValveType FOREIGN KEY (ValveTypeID) REFERENCES Riskvalve.dbo.ValveType(ID)
);


-- Riskvalve.dbo.Inspection definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.Inspection;

CREATE TABLE Riskvalve.dbo.Inspection (
	ID int IDENTITY(1,1) NOT NULL,
	AssetID int NULL,
	InspectionDate nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	InspectionMethodID int NULL,
	InspectionEffectivenessID int NULL,
	InspectionDescription nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CurrentConditionLeakeageToAtmosphereID int NULL,
	CurrentConditionFailureOfFunctionID int NULL,
	CurrentConditionPassingAcrossValveID int NULL,
	FunctionCondition nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	TestPressureIfAny nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsDeleted bit DEFAULT 0 NOT NULL,
	CreatedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedBy int NULL,
	DeletedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DeletedBy int NULL,
	CONSTRAINT PK_Inspection PRIMARY KEY (ID),
	CONSTRAINT FK_Inspection_Asset FOREIGN KEY (AssetID) REFERENCES Riskvalve.dbo.Asset(ID),
	CONSTRAINT FK_Inspection_CurrentConditionLimitStateA FOREIGN KEY (CurrentConditionFailureOfFunctionID) REFERENCES Riskvalve.dbo.CurrentConditionLimitState(ID),
	CONSTRAINT FK_Inspection_CurrentConditionLimitStateB FOREIGN KEY (CurrentConditionLeakeageToAtmosphereID) REFERENCES Riskvalve.dbo.CurrentConditionLimitState(ID),
	CONSTRAINT FK_Inspection_CurrentConditionLimitStateC FOREIGN KEY (CurrentConditionPassingAcrossValveID) REFERENCES Riskvalve.dbo.CurrentConditionLimitState(ID),
	CONSTRAINT FK_Inspection_InspectionEffectiveness FOREIGN KEY (InspectionEffectivenessID) REFERENCES Riskvalve.dbo.InspectionEffectiveness(ID),
	CONSTRAINT FK_Inspection_InspectionMethod FOREIGN KEY (InspectionMethodID) REFERENCES Riskvalve.dbo.InspectionMethod(ID),
	CONSTRAINT FK_Inspection_User FOREIGN KEY (CreatedBy) REFERENCES Riskvalve.dbo.[User](ID),
	CONSTRAINT FK_Inspection_User_1 FOREIGN KEY (DeletedBy) REFERENCES Riskvalve.dbo.[User](ID)
);


-- Riskvalve.dbo.Maintenance definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.Maintenance;

CREATE TABLE Riskvalve.dbo.Maintenance (
	Id int IDENTITY(1,1) NOT NULL,
	AssetID int NULL,
	IsValveRepairedID int NULL,
	MaintenanceDate nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	MaintenanceDescription nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsDeleted bit DEFAULT 0 NOT NULL,
	CreatedBy int NULL,
	CreatedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DeletedBy int NULL,
	DeletedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Maintenance PRIMARY KEY (Id),
	CONSTRAINT FK_Maintenance_Asset FOREIGN KEY (AssetID) REFERENCES Riskvalve.dbo.Asset(ID),
	CONSTRAINT FK_Maintenance_IsValveRepaired FOREIGN KEY (IsValveRepairedID) REFERENCES Riskvalve.dbo.IsValveRepaired(ID),
	CONSTRAINT FK_Maintenance_User FOREIGN KEY (CreatedBy) REFERENCES Riskvalve.dbo.[User](ID),
	CONSTRAINT FK_Maintenance_User2 FOREIGN KEY (DeletedBy) REFERENCES Riskvalve.dbo.[User](ID)
);


-- Riskvalve.dbo.Assessment definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.Assessment;

CREATE TABLE Riskvalve.dbo.Assessment (
	ID int IDENTITY(1,1) NOT NULL,
	AssetID int NOT NULL,
	AssessmentNo nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	TimePeriode nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	TimeToLimitStateLeakageToAtmosphere nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	TimeToLimitStateFailureOfFunction nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	TimeToLimitStatePassingAccrosValve nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	LeakageToAtmosphereID int NULL,
	FailureOfFunctionID int NULL,
	PassingAccrosValveID int NULL,
	LeakageToAtmosphereTP1ID int NULL,
	LeakageToAtmosphereTP2ID int NULL,
	LeakageToAtmosphereTP3ID int NULL,
	FailureOfFunctionTP1ID int NULL,
	FailureOfFunctionTP2ID int NULL,
	FailureOfFunctionTP3ID int NULL,
	PassingAccrosValveTP1ID int NULL,
	PassingAccrosValveTP2ID int NULL,
	PassingAccrosValveTP3ID int NULL,
	InspectionEffectivenessID int NULL,
	ImpactOfInternalFluidImpuritiesID int NULL,
	ImpactOfOperatingEnvelopesID int NULL,
	UsedWithinOEMSpecificationID int NULL,
	RepairedID int NULL,
	ProductLossDefinition nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	HSSEDefinisionID int NULL,
	Summary nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	RecommendationActionID int NULL,
	DetailedRecommendation nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsDeleted bit DEFAULT 0 NOT NULL,
	CreatedBy int NULL,
	CreatedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DeletedBy int NULL,
	DeletedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Assesment PRIMARY KEY (ID),
	CONSTRAINT FK_Assesment_Asset FOREIGN KEY (AssetID) REFERENCES Riskvalve.dbo.Asset(ID),
	CONSTRAINT FK_Assesment_CurrentConditionLimitState FOREIGN KEY (LeakageToAtmosphereID) REFERENCES Riskvalve.dbo.CurrentConditionLimitState(ID),
	CONSTRAINT FK_Assesment_CurrentConditionLimitState2 FOREIGN KEY (FailureOfFunctionID) REFERENCES Riskvalve.dbo.CurrentConditionLimitState(ID),
	CONSTRAINT FK_Assesment_CurrentConditionLimitState3 FOREIGN KEY (PassingAccrosValveID) REFERENCES Riskvalve.dbo.CurrentConditionLimitState(ID),
	CONSTRAINT FK_Assesment_HSSEDefinision FOREIGN KEY (HSSEDefinisionID) REFERENCES Riskvalve.dbo.HSSEDefinision(ID),
	CONSTRAINT FK_Assesment_ImpactEffect FOREIGN KEY (ImpactOfInternalFluidImpuritiesID) REFERENCES Riskvalve.dbo.ImpactEffect(ID),
	CONSTRAINT FK_Assesment_ImpactEffect2 FOREIGN KEY (ImpactOfOperatingEnvelopesID) REFERENCES Riskvalve.dbo.ImpactEffect(ID),
	CONSTRAINT FK_Assesment_InspectionEffectiveness FOREIGN KEY (InspectionEffectivenessID) REFERENCES Riskvalve.dbo.InspectionEffectiveness(ID),
	CONSTRAINT FK_Assesment_RecomendedAction FOREIGN KEY (RecommendationActionID) REFERENCES Riskvalve.dbo.RecomendedAction(ID),
	CONSTRAINT FK_Assesment_Repaired FOREIGN KEY (RepairedID) REFERENCES Riskvalve.dbo.Repaired(ID),
	CONSTRAINT FK_Assesment_TimeToLimitState FOREIGN KEY (LeakageToAtmosphereTP1ID) REFERENCES Riskvalve.dbo.TimeToLimitState(ID),
	CONSTRAINT FK_Assesment_TimeToLimitState2 FOREIGN KEY (LeakageToAtmosphereTP2ID) REFERENCES Riskvalve.dbo.TimeToLimitState(ID),
	CONSTRAINT FK_Assesment_TimeToLimitState3 FOREIGN KEY (LeakageToAtmosphereTP3ID) REFERENCES Riskvalve.dbo.TimeToLimitState(ID),
	CONSTRAINT FK_Assesment_TimeToLimitState4 FOREIGN KEY (FailureOfFunctionTP1ID) REFERENCES Riskvalve.dbo.TimeToLimitState(ID),
	CONSTRAINT FK_Assesment_TimeToLimitState5 FOREIGN KEY (FailureOfFunctionTP2ID) REFERENCES Riskvalve.dbo.TimeToLimitState(ID),
	CONSTRAINT FK_Assesment_TimeToLimitState6 FOREIGN KEY (FailureOfFunctionTP3ID) REFERENCES Riskvalve.dbo.TimeToLimitState(ID),
	CONSTRAINT FK_Assesment_TimeToLimitState7 FOREIGN KEY (PassingAccrosValveTP1ID) REFERENCES Riskvalve.dbo.TimeToLimitState(ID),
	CONSTRAINT FK_Assesment_TimeToLimitState8 FOREIGN KEY (PassingAccrosValveTP2ID) REFERENCES Riskvalve.dbo.TimeToLimitState(ID),
	CONSTRAINT FK_Assesment_TimeToLimitState9 FOREIGN KEY (PassingAccrosValveTP3ID) REFERENCES Riskvalve.dbo.TimeToLimitState(ID),
	CONSTRAINT FK_Assesment_UsedWithinOEMSpecification FOREIGN KEY (UsedWithinOEMSpecificationID) REFERENCES Riskvalve.dbo.UsedWithinOEMSpecification(ID)
);


-- Riskvalve.dbo.InspectionFile definition

-- Drop table

-- DROP TABLE Riskvalve.dbo.InspectionFile;

CREATE TABLE Riskvalve.dbo.InspectionFile (
	ID int IDENTITY(1,1) NOT NULL,
	FileName nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FileSize bigint NULL,
	FileType nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FilePath nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	InspectionID int NULL,
	MaintenanceID int NULL,
	IsDeleted bit DEFAULT 0 NOT NULL,
	CreatedBy int NULL,
	CreatedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DeletedBy int NULL,
	DeletedAt nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_InspectionFile PRIMARY KEY (ID),
	CONSTRAINT FK_InspectionFile_Inspection FOREIGN KEY (InspectionID) REFERENCES Riskvalve.dbo.Inspection(ID),
	CONSTRAINT FK_InspectionFile_Maintenance FOREIGN KEY (MaintenanceID) REFERENCES Riskvalve.dbo.Maintenance(Id),
	CONSTRAINT FK_InspectionFile_User FOREIGN KEY (CreatedBy) REFERENCES Riskvalve.dbo.[User](ID),
	CONSTRAINT FK_InspectionFile_User_1 FOREIGN KEY (DeletedBy) REFERENCES Riskvalve.dbo.[User](ID)
);


