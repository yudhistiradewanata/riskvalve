INSERT INTO Riskvalve.dbo.Area (BusinessArea,IsDeleted,CreatedBy,CreatedAt,DeletedBy,DeletedAt) VALUES
	 (N'NBU',0,1,NULL,NULL,NULL),
	 (N'CBU',0,1,NULL,NULL,NULL),
	 (N'SBD',0,1,NULL,NULL,NULL),
	 (N'FEB',1,1,NULL,NULL,NULL),
	 (N'TAK',1,NULL,NULL,1,N'31-03-2024 14:58:51'),
	 (N'ALDO',0,1,NULL,NULL,NULL),
	 (N'NEW2',1,1,NULL,NULL,NULL),
	 (N'NEW3',1,1,NULL,NULL,NULL),
	 (N'CDS',1,1,N'3/31/2024 2:02:26PM',NULL,NULL),
	 (N'BST',1,1,N'3/31/2024 2:02:49PM',NULL,NULL);
INSERT INTO Riskvalve.dbo.Area (BusinessArea,IsDeleted,CreatedBy,CreatedAt,DeletedBy,DeletedAt) VALUES
	 (N'TSA',1,1,N'2024-03-31 14:52:52',NULL,NULL),
	 (N'SST',0,1,N'31-03-2024 14:53:17',NULL,NULL),
	 (N'121',1,1,N'31-03-2024 14:55:14',1,N'31-03-2024 15:03:35');
INSERT INTO Riskvalve.dbo.Assessment (AssetID,AssessmentNo,TimePeriode,TimeToLimitStateLeakageToAtmosphere,TimeToLimitStateFailureOfFunction,TimeToLimitStatePassingAccrosValve,LeakageToAtmosphereID,FailureOfFunctionID,PassingAccrosValveID,LeakageToAtmosphereTP1ID,LeakageToAtmosphereTP2ID,LeakageToAtmosphereTP3ID,FailureOfFunctionTP1ID,FailureOfFunctionTP2ID,FailureOfFunctionTP3ID,PassingAccrosValveTP1ID,PassingAccrosValveTP2ID,PassingAccrosValveTP3ID,InspectionEffectivenessID,ImpactOfInternalFluidImpuritiesID,ImpactOfOperatingEnvelopesID,UsedWithinOEMSpecificationID,RepairedID,ProductLossDefinition,HSSEDefinisionID,Summary,RecommendationActionID,DetailedRecommendation,IsDeleted,CreatedBy,CreatedAt,DeletedBy,DeletedAt) VALUES
	 (1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,N'Some summary text',NULL,NULL,0,NULL,N'2024-04-06',NULL,NULL),
	 (1,N'',N'',N'',N'',N'',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,N'',1,N'',1,N'',0,1,N'2024-04-06 18:42:05',NULL,NULL),
	 (1,N'',N'',N'',N'',N'',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,N'',1,N'',1,N'',0,1,N'2024-04-06 18:42:52',NULL,NULL),
	 (1,N'',N'',N'',N'',N'',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,N'',1,N'',1,N'',0,1,N'2024-04-06 18:44:06',NULL,NULL);
INSERT INTO Riskvalve.dbo.Asset (TagNo,PlatformID,ValveTypeID,[Size],ClassRating,ParentEquipmentNo,ParentEquipmentDescription,InstallationDate,PIDNo,Manufacturer,BodyModel,BodyMaterial,EndConnection,SerialNo,ManualOverrideID,ActuatorMfg,ActuatorSerialNo,ActuatorTypeModel,ActuatorPower,OperatingTemperature,OperatingPressure,FlowRate,ServiceFluid,FluidPhaseID,ToxicOrFlamableFluidID,IsDeleted,CreatedBy,CreatedAt,DeletedBy,DeletedAt) VALUES
	 (N'TAG-STK-PRO-001',5,1,N'12',N'155',N'P123',N'Just Desc',N'14/03/2024',N'PID 2',N'AL',N'NA',N'MAT',N'RJ',N'2342N4324823J',1,N'N/A',N'345656782',N'N/A',N'N/A',N'1',N'2',N'3',N'UNKNOWN',1,1,0,NULL,NULL,NULL,NULL),
	 (N'123',2,2,N'12 x 10',N'150',N'P1',N'PE DESC',N'14/03/2024',N'PID 2',N'NA',N'BONA',N'BMNA',N'ECNA',N'SNNA',1,N'ANA',N'ASNA',N'ATNA',N'APNA',N'',N'',N'',N'FLUID',1,1,0,NULL,NULL,NULL,NULL),
	 (N'3213',2,2,N'',N'21312',N'21312',N'12312',N'15/03/2024',N'123123',N'',N'',N'',N'',N'',3,N'',N'',N'',N'',N'',N'',N'',N'',1,1,0,NULL,NULL,NULL,NULL),
	 (N'TAG-123',8,2,N'12',N'',N'',N'',N'',N'',N'',N'',N'',N'',N'',2,N'',N'',N'',N'',N'',N'',N'',N'',1,1,0,NULL,NULL,NULL,NULL);
INSERT INTO Riskvalve.dbo.CurrentConditionLimitState (CurrentConditionLimitState) VALUES
	 (N'Good'),
	 (N'Fair'),
	 (N'Poor');
INSERT INTO Riskvalve.dbo.FluidPhase (FluidPhase) VALUES
	 (N'Liquid'),
	 (N'Gas');
INSERT INTO Riskvalve.dbo.HSSEDefinision (HSSEDefinision) VALUES
	 (N'Multiple Fatalities'),
	 (N'Single Fatality or Total Permanent'),
	 (N'Moderate or Significant Injury or Health Effect'),
	 (N'Minor Injury or Health Effect'),
	 (N'Slight Injury or Health Effect');
INSERT INTO Riskvalve.dbo.ImpactEffect (ImpactEffect) VALUES
	 (N'No Expected Effect'),
	 (N'Moderate Effect'),
	 (N'Significant Effect');
INSERT INTO Riskvalve.dbo.Inspection (AssetID,InspectionDate,InspectionMethodID,InspectionEffectivenessID,InspectionDescription,CurrentConditionLeakeageToAtmosphereID,CurrentConditionFailureOfFunctionID,CurrentConditionPassingAcrossValveID,FunctionCondition,TestPressureIfAny,IsDeleted,CreatedAt,CreatedBy,DeletedAt,DeletedBy) VALUES
	 (1,N'2024-03-29',1,1,N'1',1,1,1,N'3',N'123',1,NULL,NULL,N'4/1/2024 12:07:51AM',NULL),
	 (1,N'2024-03-28',2,1,N'Desc',1,1,1,N'3',N'123',1,NULL,NULL,N'01-04-2024 00:09:28',1),
	 (1,N'2024-03-29',1,1,N'1',1,1,1,N'3',N'123',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-29',1,1,N'1',1,1,1,N'3',N'123',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-29',1,1,N'1',1,1,1,N'3',N'123',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL);
INSERT INTO Riskvalve.dbo.Inspection (AssetID,InspectionDate,InspectionMethodID,InspectionEffectivenessID,InspectionDescription,CurrentConditionLeakeageToAtmosphereID,CurrentConditionFailureOfFunctionID,CurrentConditionPassingAcrossValveID,FunctionCondition,TestPressureIfAny,IsDeleted,CreatedAt,CreatedBy,DeletedAt,DeletedBy) VALUES
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-27',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL);
INSERT INTO Riskvalve.dbo.Inspection (AssetID,InspectionDate,InspectionMethodID,InspectionEffectivenessID,InspectionDescription,CurrentConditionLeakeageToAtmosphereID,CurrentConditionFailureOfFunctionID,CurrentConditionPassingAcrossValveID,FunctionCondition,TestPressureIfAny,IsDeleted,CreatedAt,CreatedBy,DeletedAt,DeletedBy) VALUES
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL);
INSERT INTO Riskvalve.dbo.Inspection (AssetID,InspectionDate,InspectionMethodID,InspectionEffectivenessID,InspectionDescription,CurrentConditionLeakeageToAtmosphereID,CurrentConditionFailureOfFunctionID,CurrentConditionPassingAcrossValveID,FunctionCondition,TestPressureIfAny,IsDeleted,CreatedAt,CreatedBy,DeletedAt,DeletedBy) VALUES
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,NULL,NULL,NULL,NULL),
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,N'01-04-2024 20:37:09',1,NULL,NULL),
	 (1,N'2024-03-31',1,1,N'Rand Desc Lorem',1,2,3,N'1',N'1',0,N'01-04-2024 22:10:20',1,NULL,NULL),
	 (1,N'02-04-2024',2,1,N'123',1,1,1,N'',N'',0,N'01-04-2024 22:20:57',1,NULL,NULL);
INSERT INTO Riskvalve.dbo.InspectionEffectiveness (Effectiveness) VALUES
	 (N'Highly Effective'),
	 (N'Ussually Effective'),
	 (N'Fairly Effective'),
	 (N'Ineffective');
INSERT INTO Riskvalve.dbo.InspectionFile (FileName,FileSize,FileType,FilePath,InspectionID,MaintenanceID,IsDeleted,CreatedBy,CreatedAt,DeletedBy,DeletedAt) VALUES
	 (N'7382a5c3-4d1f-469f-bc3a-85cc0d122686.png',119770,N'image/png',N'Uploads/Inspection/31/7382a5c3-4d1f-469f-bc3a-85cc0d122686.png',31,NULL,1,1,N'31-03-2024 23:53:00',NULL,N'31-03-2024 23:54:21'),
	 (N'b969da85-358e-41a6-bbb6-8a75f01ada17.png',4250,N'image/png',N'Uploads/Inspection/31/b969da85-358e-41a6-bbb6-8a75f01ada17.png',31,NULL,1,1,N'31-03-2024 23:53:00',NULL,N'31-03-2024 23:54:21'),
	 (N'1d68535d-43f8-4e7a-afb3-7df14fdd503e.png',119770,N'image/png',N'Uploads/Inspection/23/1d68535d-43f8-4e7a-afb3-7df14fdd503e.png',23,NULL,0,NULL,NULL,NULL,NULL),
	 (N'49a8bd9a-631f-4ad0-beb3-0ae4191af0be.png',4250,N'image/png',N'Uploads/Inspection/23/49a8bd9a-631f-4ad0-beb3-0ae4191af0be.png',23,NULL,0,NULL,NULL,NULL,NULL),
	 (N'7cab5e6d-324b-4a38-b5f2-1a4823b14179.png',1360,N'image/png',N'Uploads/Inspection/23/7cab5e6d-324b-4a38-b5f2-1a4823b14179.png',23,NULL,0,NULL,NULL,NULL,NULL),
	 (N'795cb591-2657-4905-a937-8f26eb73bace.png',119770,N'image/png',N'Uploads/Inspection/31/795cb591-2657-4905-a937-8f26eb73bace.png',31,NULL,1,NULL,NULL,1,N'31-03-2024 23:59:15'),
	 (N'7dc5511a-cd90-4a70-841b-57bbec80e84b.png',4250,N'image/png',N'Uploads/Inspection/31/7dc5511a-cd90-4a70-841b-57bbec80e84b.png',31,NULL,1,NULL,NULL,1,N'31-03-2024 23:59:16'),
	 (N'01a11843-7669-4029-8a6b-53ae58160f98.png',1360,N'image/png',N'Uploads/Inspection/31/01a11843-7669-4029-8a6b-53ae58160f98.png',31,NULL,1,NULL,NULL,1,N'31-03-2024 23:59:16'),
	 (N'ab822383-c2db-4b86-8e40-4f570d151b05.png',119770,N'image/png',N'Uploads/Inspection/31/ab822383-c2db-4b86-8e40-4f570d151b05.png',31,NULL,0,1,N'31-03-2024 23:59:15',NULL,NULL),
	 (N'd9070356-466d-415e-b884-1794d66c9e2f.png',4250,N'image/png',N'Uploads/Inspection/31/d9070356-466d-415e-b884-1794d66c9e2f.png',31,NULL,0,1,N'31-03-2024 23:59:15',NULL,NULL);
INSERT INTO Riskvalve.dbo.InspectionFile (FileName,FileSize,FileType,FilePath,InspectionID,MaintenanceID,IsDeleted,CreatedBy,CreatedAt,DeletedBy,DeletedAt) VALUES
	 (N'226099b5-7410-4bb0-8baf-dc3c2ccb30e5.png',1360,N'image/png',N'Uploads/Inspection/31/226099b5-7410-4bb0-8baf-dc3c2ccb30e5.png',31,NULL,0,1,N'31-03-2024 23:59:15',NULL,NULL),
	 (N'7b226849-d741-41ed-b970-b744e4d26d41.png',33300,N'image/png',N'Uploads/Inspection/31/7b226849-d741-41ed-b970-b744e4d26d41.png',31,NULL,0,1,N'31-03-2024 23:59:15',NULL,NULL),
	 (N'979a6c6c-070b-404d-ae48-b58dd6fd5946.png',119770,N'image/png',N'Uploads/Inspection/35/979a6c6c-070b-404d-ae48-b58dd6fd5946.png',35,NULL,0,1,N'01-04-2024 20:33:21',NULL,NULL),
	 (N'af8c3e05-845d-47e9-ad33-7bf41c239c7e.png',4250,N'image/png',N'Uploads/Inspection/35/af8c3e05-845d-47e9-ad33-7bf41c239c7e.png',35,NULL,0,1,N'01-04-2024 20:33:21',NULL,NULL),
	 (N'55521dbc-e2fe-4aa0-b92f-bf2abc86c368.png',1360,N'image/png',N'Uploads/Inspection/35/55521dbc-e2fe-4aa0-b92f-bf2abc86c368.png',35,NULL,0,1,N'01-04-2024 20:33:21',NULL,NULL),
	 (N'6394a688-f1f9-464a-84c2-b0060e4d5ee4.png',33300,N'image/png',N'Uploads/Inspection/35/6394a688-f1f9-464a-84c2-b0060e4d5ee4.png',35,NULL,0,1,N'01-04-2024 20:33:21',NULL,NULL),
	 (N'c56e61f9-f291-486e-81da-b903dde354fc.png',119770,N'image/png',N'Uploads/Inspection/36/c56e61f9-f291-486e-81da-b903dde354fc.png',36,NULL,0,1,N'01-04-2024 20:37:09',NULL,NULL),
	 (N'3aaa83a9-a0a1-48f2-9332-2fd23955753c.png',4250,N'image/png',N'Uploads/Inspection/36/3aaa83a9-a0a1-48f2-9332-2fd23955753c.png',36,NULL,0,1,N'01-04-2024 20:37:09',NULL,NULL),
	 (N'e5c7f310-7d2f-4d1d-a6e5-02969e76f281.png',1360,N'image/png',N'Uploads/Inspection/36/e5c7f310-7d2f-4d1d-a6e5-02969e76f281.png',36,NULL,0,1,N'01-04-2024 20:37:09',NULL,NULL),
	 (N'336368be-76ff-46e1-9305-5a1589575bb4.png',33300,N'image/png',N'Uploads/Inspection/36/336368be-76ff-46e1-9305-5a1589575bb4.png',36,NULL,0,1,N'01-04-2024 20:37:09',NULL,NULL);
INSERT INTO Riskvalve.dbo.InspectionFile (FileName,FileSize,FileType,FilePath,InspectionID,MaintenanceID,IsDeleted,CreatedBy,CreatedAt,DeletedBy,DeletedAt) VALUES
	 (N'03daf43c-a157-4844-9820-b0881bb19199.png',119770,N'image/png',N'Uploads/Inspection/38/03daf43c-a157-4844-9820-b0881bb19199.png',38,NULL,0,1,N'01-04-2024 22:10:20',NULL,NULL),
	 (N'236b1edf-c51e-4f3c-9da1-818e2fcf225f.png',4250,N'image/png',N'Uploads/Inspection/38/236b1edf-c51e-4f3c-9da1-818e2fcf225f.png',38,NULL,0,1,N'01-04-2024 22:10:20',NULL,NULL),
	 (N'502b2428-4cf7-44f8-be9b-209956d3277c.png',1360,N'image/png',N'Uploads/Inspection/38/502b2428-4cf7-44f8-be9b-209956d3277c.png',38,NULL,0,1,N'01-04-2024 22:10:20',NULL,NULL),
	 (N'b176e0e9-9e0e-467c-8713-c2c91eac8ccc.png',33300,N'image/png',N'Uploads/Inspection/38/b176e0e9-9e0e-467c-8713-c2c91eac8ccc.png',38,NULL,0,1,N'01-04-2024 22:10:20',NULL,NULL),
	 (N'083bde43-e2ee-4fe1-ad92-f82da874a383.png',4250,N'image/png',N'Uploads/Inspection/39/083bde43-e2ee-4fe1-ad92-f82da874a383.png',39,NULL,1,1,N'01-04-2024 22:21:19',1,N'01-04-2024 22:21:35'),
	 (N'890b3d75-4e07-42ad-8246-3354a6337695.png',4250,N'image/png',N'Uploads/Inspection/39/890b3d75-4e07-42ad-8246-3354a6337695.png',39,NULL,0,1,N'01-04-2024 22:21:35',NULL,NULL),
	 (N'fd706026-10ad-4ddd-b010-710044cca7c9.png',4250,N'image/png',N'Uploads/Inspection/3/fd706026-10ad-4ddd-b010-710044cca7c9.png',NULL,3,0,NULL,NULL,NULL,NULL),
	 (N'd2c7ef88-25d7-469e-9ed3-7f18d36e1198.png',33300,N'image/png',N'Uploads/Inspection/3/d2c7ef88-25d7-469e-9ed3-7f18d36e1198.png',NULL,3,0,NULL,NULL,NULL,NULL),
	 (N'4929aad6-d472-46be-91c2-d813d238f528.png',1360,N'image/png',N'Uploads/Inspection/3/4929aad6-d472-46be-91c2-d813d238f528.png',NULL,3,0,NULL,NULL,NULL,NULL),
	 (N'd0e6dc2e-b813-43ce-af83-b09b9251a168.png',4250,N'image/png',N'Uploads/Inspection/5/d0e6dc2e-b813-43ce-af83-b09b9251a168.png',NULL,5,0,NULL,NULL,NULL,NULL);
INSERT INTO Riskvalve.dbo.InspectionFile (FileName,FileSize,FileType,FilePath,InspectionID,MaintenanceID,IsDeleted,CreatedBy,CreatedAt,DeletedBy,DeletedAt) VALUES
	 (N'd0011730-d4f4-467e-9272-f2b03e94da19.png',33300,N'image/png',N'Uploads/Inspection/5/d0011730-d4f4-467e-9272-f2b03e94da19.png',NULL,5,0,NULL,NULL,NULL,NULL),
	 (N'7ed58843-2927-4e66-9f66-3b7c6369b7ab.png',1360,N'image/png',N'Uploads/Inspection/5/7ed58843-2927-4e66-9f66-3b7c6369b7ab.png',NULL,5,0,NULL,NULL,NULL,NULL),
	 (N'9043b487-a2d7-4f5f-8a92-e5cd1132d4e7.png',4250,N'image/png',N'Uploads/Maintenance/3/9043b487-a2d7-4f5f-8a92-e5cd1132d4e7.png',NULL,3,0,NULL,NULL,NULL,NULL),
	 (N'52e9aafd-f97d-4db4-b908-759d58a7ae1a.png',33300,N'image/png',N'Uploads/Maintenance/3/52e9aafd-f97d-4db4-b908-759d58a7ae1a.png',NULL,3,0,NULL,NULL,NULL,NULL),
	 (N'41900e3f-a591-40c0-93a5-16da55eaa87b.png',1360,N'image/png',N'Uploads/Maintenance/3/41900e3f-a591-40c0-93a5-16da55eaa87b.png',NULL,3,0,NULL,NULL,NULL,NULL),
	 (N'e8b70bc0-59ee-4f87-a908-74399d4fff92.png',4250,N'image/png',N'Uploads/Maintenance/3/e8b70bc0-59ee-4f87-a908-74399d4fff92.png',NULL,3,0,NULL,NULL,NULL,NULL),
	 (N'e78ce30a-567c-4565-8766-9ddcc109eeb2.png',33300,N'image/png',N'Uploads/Maintenance/3/e78ce30a-567c-4565-8766-9ddcc109eeb2.png',NULL,3,0,NULL,NULL,NULL,NULL),
	 (N'0cebf788-b18b-40af-9f20-c5c4725d961f.png',1360,N'image/png',N'Uploads/Maintenance/3/0cebf788-b18b-40af-9f20-c5c4725d961f.png',NULL,3,0,NULL,NULL,NULL,NULL),
	 (N'a1632c4a-7e8f-4749-8e1d-e986fc24349e.png',4250,N'image/png',N'Uploads/Inspection/6/a1632c4a-7e8f-4749-8e1d-e986fc24349e.png',NULL,6,0,NULL,NULL,NULL,NULL),
	 (N'8c14ce8d-7d34-44f1-81e7-019c1e742c0b.png',33300,N'image/png',N'Uploads/Inspection/6/8c14ce8d-7d34-44f1-81e7-019c1e742c0b.png',NULL,6,0,NULL,NULL,NULL,NULL);
INSERT INTO Riskvalve.dbo.InspectionFile (FileName,FileSize,FileType,FilePath,InspectionID,MaintenanceID,IsDeleted,CreatedBy,CreatedAt,DeletedBy,DeletedAt) VALUES
	 (N'503da086-e26c-4944-bd94-f230f7b5bc84.png',1360,N'image/png',N'Uploads/Inspection/6/503da086-e26c-4944-bd94-f230f7b5bc84.png',NULL,6,0,NULL,NULL,NULL,NULL),
	 (N'bde20273-204d-4b74-9d02-59d75ff913a4.png',4250,N'image/png',N'Uploads/Maintenance/6/bde20273-204d-4b74-9d02-59d75ff913a4.png',NULL,6,0,NULL,NULL,NULL,NULL),
	 (N'2a445047-5a49-48c9-b9d2-1c20a615bfe5.png',33300,N'image/png',N'Uploads/Maintenance/6/2a445047-5a49-48c9-b9d2-1c20a615bfe5.png',NULL,6,0,NULL,NULL,NULL,NULL),
	 (N'a6ee04e2-4f33-4212-9836-367efc8bd044.png',1360,N'image/png',N'Uploads/Maintenance/6/a6ee04e2-4f33-4212-9836-367efc8bd044.png',NULL,6,0,NULL,NULL,NULL,NULL);
INSERT INTO Riskvalve.dbo.InspectionMethod (InspectionMethod) VALUES
	 (N'Visual Inspection'),
	 (N'Ultrasonic Testing Inspection'),
	 (N'Radiography Test');
INSERT INTO Riskvalve.dbo.IsValveRepaired (IsValveRepaired) VALUES
	 (N'Yes'),
	 (N'No');
INSERT INTO Riskvalve.dbo.Maintenance (AssetID,IsValveRepairedID,MaintenanceDate,MaintenanceDescription,IsDeleted,CreatedBy,CreatedAt,DeletedBy,DeletedAt) VALUES
	 (1,1,N'02-04-2024',N'Tes Desc',0,NULL,NULL,NULL,NULL),
	 (1,1,N'02-04-2024',N'Tes Desc',0,1,N'02-04-2024 01:14:30',NULL,NULL),
	 (1,1,N'02-04-2024',N'Tes Desc2',1,NULL,NULL,1,N'02-04-2024 01:27:24'),
	 (1,1,N'03-04-2024',N'tes',1,1,N'02-04-2024 01:30:19',1,N'02-04-2024 01:30:53');
INSERT INTO Riskvalve.dbo.ManualOverride (ManualOverride) VALUES
	 (N'Yes'),
	 (N'No'),
	 (N'Normally Open or Close');
INSERT INTO Riskvalve.dbo.Platform (AreaID,Platform,Code,IsDeleted,CreatedBy,CreatedAt,DeletedBy,DeletedAt) VALUES
	 (1,N'NARU-TO',N'KNH-SHA-34B',0,NULL,NULL,NULL,NULL),
	 (9,N'DODO-B-3',N'ALD-FEB-123',0,NULL,NULL,NULL,NULL),
	 (7,N'ALD-BCD',N'ALDO-FEBRIAN-99',0,NULL,NULL,NULL,NULL),
	 (1,N'PLATFORM Z',N'NBU-CODE-ALDO',0,NULL,NULL,NULL,NULL),
	 (10,N'NAME',N'CODE-123',0,NULL,NULL,NULL,NULL),
	 (2,N'DUO',N'133-DUO',1,NULL,N'31-03-2024 19:26:23',NULL,NULL);
INSERT INTO Riskvalve.dbo.RecomendedAction (RecomendedAction) VALUES
	 (N'Inspection'),
	 (N'Repair'),
	 (N'Replace');
INSERT INTO Riskvalve.dbo.Repaired (Repaired) VALUES
	 (N'Yes'),
	 (N'No');
INSERT INTO Riskvalve.dbo.TimeToLimitState (TimeToLimitState) VALUES
	 (N'Improbable'),
	 (N'Doubtful'),
	 (N'Expected');
INSERT INTO Riskvalve.dbo.ToxicOrFlamableFluid (ToxicOrFlamableFluid) VALUES
	 (N'Yes'),
	 (N'No');
INSERT INTO Riskvalve.dbo.UsedWithinOEMSpecification (UsedWithinOEMSpecification) VALUES
	 (N'Significantly Below Specification'),
	 (N'Within Specification Range'),
	 (N'Above Specification Limit');
INSERT INTO Riskvalve.dbo.[User] (Username,Password,[Role],IsAdmin,IsEngineer,IsViewer,IsDeleted,CreatedBy,CreatedAt,DeletedBy,DeletedAt) VALUES
	 (N'Aldo',N'$2a$11$O5bjbyUQrpEfRBROnnCAW.VNgr.NDxWXc.9U1VlZNyGg7qoZuPvZa',N'Surface Facility Admin',1,1,1,0,NULL,NULL,NULL,NULL),
	 (N'Engineer',N'$2a$11$S19V9zqjs5jGRmRtG9rm3eA8BxPdUu5usvn6hO4EYPalbb5YjscOi',N'Engineer',0,1,0,0,NULL,NULL,NULL,NULL),
	 (N'Viewer',N'$2a$11$tlI6bO7ovWtKgLLrdlhzeOr3XwHgqpXr92q.2LmvF7Tf1IUhsKCgS',N'Viewer',1,0,1,0,NULL,NULL,NULL,NULL);
INSERT INTO Riskvalve.dbo.ValveType (ValveType) VALUES
	 (N'Air Release Valve'),
	 (N'Blowdown Valve'),
	 (N'Check Valve'),
	 (N'Deluge Valve');
