
USE Maps
GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON
SET NUMERIC_ROUNDABORT OFF

IF object_id(N'WayWeights', 'V') IS NOT NULL
	DROP VIEW dbo.WayWeights
GO

CREATE VIEW dbo.WayWeights
WITH SCHEMABINDING AS
SELECT EW.WayID,
SUM(CONVERT(decimal,EW.Weight)) as TotalWeight ,
SUM(1) as EdgesCount ,
SUM(CONVERT(decimal,EW.Weight))/SUM(1) as AverageWeight
FROM dbo.EdgeWeights EW
group by EW.wayID

GO



