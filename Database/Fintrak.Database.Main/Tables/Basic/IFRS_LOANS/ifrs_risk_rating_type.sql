/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_risk_rating_type]
(	[RiskRatingTypeId] INT NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
   	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [AK_ifrs_risk_rating_type_Name] UNIQUE ([Name]), 
    CONSTRAINT [PK_ifrs_risk_rating_type] PRIMARY KEY ([RiskRatingTypeId])) 
