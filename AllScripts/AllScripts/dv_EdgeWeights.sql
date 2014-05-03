
USE Maps
GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON
SET NUMERIC_ROUNDABORT OFF

IF object_id(N'EdgeWeights', 'V') IS NOT NULL
	DROP VIEW dbo.EdgeWeights
GO

CREATE VIEW dbo.EdgeWeights
WITH SCHEMABINDING AS
Select
	E.EdgeID,
	ET.TagValue as [Weight],
	E.WayID,
	E.Node1ID,
	E.Node2ID
from dbo.Edges E
inner join dbo.EdgeTags ET on E.EdgeId=ET.EdgeId and ET.TagName ='Weight'
GO
CREATE UNIQUE CLUSTERED INDEX EdgeWeights_IndexedView
ON dbo.EdgeWeights([EdgeID])
