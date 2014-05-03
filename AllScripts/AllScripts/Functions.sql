USE Maps;
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetWayByName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetWayByName]
GO
CREATE FUNCTION GetWayByName 
(	
	@WayName nvarchar(32)
)
-- select * from GetWayByName('วแสอัํั')
RETURNS TABLE 
AS
RETURN 
(
	SELECT w.wayid as WayID,w.geog4326,wt.tagvalue as WayName,geog4326.STLength() as WayLength from Ways w inner join Waytags wt on w.wayid=wt.wayid
	and wt.tagname='name' and wt.tagvalue like '%'+@WayName +'%'
)
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetWayByCords]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetWayByCords]
GO
CREATE FUNCTION GetWayByCords 
(	
	@Longitude float,
	@Latitude float
)
-- select * from GetWayByCords(31.3181264,30.0818767)
RETURNS TABLE 
AS
RETURN 
(
	SELECT w.wayid as WayID,w.geog4326,wt.tagvalue as WayName,w.geog4326.STLength() as WayLength from Ways w inner join WayNodes wn on wn.WayID = W.WayID
	inner join Nodes n on n.NodeID = wn.NodeID and n.longitude = @Longitude and n.latitude = @Latitude
	inner join Waytags wt on w.wayid=wt.wayid and wt.tagname='name'
)
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DrawWayByName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[DrawWayByName]
GO
CREATE FUNCTION [DrawWayByName] 
(	
	@WayName nvarchar(32)
)
-- select * from DrawWayByName('วแสอัํั')
RETURNS TABLE 
AS
RETURN 
(
	SELECT w.wayid as WayID,w.geog4326,wt.tagvalue as WayName,geog4326.STLength() as WayLength from Ways w inner join Waytags wt on w.wayid=wt.wayid
	and wt.tagname='name' 
	where wt.tagvalue like '%'+@WayName +'%' 
	or w.WayID in (select distinct WayID from WayNodes where nodeid in (select nodeid from WayNodes inner join ways on ways.wayid=waynodes.wayid 
																		inner join waytags on ways.wayid=waytags.wayid and waytags.tagname='name' and waytags.tagvalue like '%'+@WayName +'%' ))
)
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DrawWayByID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[DrawWayByID]
GO
CREATE FUNCTION [DrawWayByID] 
(	
	@WayID int
)
-- select * from DrawWayByID(4987193);
RETURNS TABLE 
AS
RETURN 
(
	SELECT w.wayid as WayID,w.geog4326,wt.tagvalue as WayName,geog4326.STLength() as WayLength from Ways w inner join Waytags wt on w.wayid=wt.wayid
	and wt.tagname='name' 
	where w.wayid = @WayID 
	or w.WayID in (select distinct WayID from WayNodes where nodeid in (select nodeid from WayNodes inner join ways on ways.wayid=waynodes.wayid 
																		and ways.wayid=@wayID))
)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetWayByTag]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetWayByTag]
GO
CREATE FUNCTION GetWayByTag 
(	
	@TagName varchar(32) = NULL,
	@TagValue nvarchar(32) = NULL
)
-- select * from GetWayByTag(NULL,'zoo')
RETURNS TABLE 
AS
RETURN 
(
	select Ways.WayID,geog4326,isnull(tagvalue,'') as Name 
	from Ways left outer join WayTags on Ways.WayID=WayTags.WayID
	and WayTags.TagName='name' 
	where Ways.WayID in (
						select DISTINCT w.WayID
						from Ways w inner join WayTags WT on WT.WayID = w.WayID
						where (@TagName is NULL or TagName like '%' + @TagName +'%') and 
								(@TagValue is NULL or TagValue like N'%' + @TagValue + N'%')
					)
)
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRandomNodeInWay]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetRandomNodeInWay]
GO
CREATE FUNCTION GetRandomNodeInWay 
(	
	@WayName nvarchar(32)
)
-- select dbo.GetRandomNodeInWay('วแสอัํั')
RETURNS NodeID
AS
BEGIN
DECLARE @NodeID NodeID
SELECT top 1 @NodeID = wn.nodeid from Ways w inner join Waytags wt on w.wayid=wt.wayid
	inner join waynodes wn on wn.wayid=w.wayid
	and wt.tagname='name' and wt.tagvalue like '%'+@WayName +'%'
RETURN @NodeID
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DrawEdge]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].DrawEdge
GO
CREATE FUNCTION DrawEdge
(
	@EdgeID int
)
RETURNS geography
-- select dbo.DrawEdge(5957)
AS
BEGIN
	Declare @EdgeString varchar(max);
	set @EdgeString = 'LINESTRING( ';
	Declare @EdgeGeography geography;
	set @EdgeGeography = NULL;
	Declare @Found bit;
	set @Found = 0;
	Declare @Longitude decimal(18,9),@Latitude decimal(18,9)
	Declare @Node1OrderID int , @Node2OrderID int
	select @Node1OrderID = ID from WayNodes wn inner join Edges e on e.edgeid=@EdgeID and e.WayID=wn.WayID and e.Node1ID = wn.NodeID order by ID desc
	select @Node2OrderID = ID from WayNodes wn inner join Edges e on e.edgeid=@EdgeID and e.WayID=wn.WayID and e.Node2ID = wn.NodeID order by ID desc
	declare WayNodes_Cur cursor for select CAST(n.Longitude AS decimal(18,9)),CAST(n.Latitude AS decimal(18,9)) from WayNodes wn inner join Edges e 
			on e.edgeid=@EdgeID and e.WayID=wn.WayID and ID between @Node1OrderID and @Node2OrderID 
			inner join Nodes n on n.NodeID = wn.NodeID
	Open WayNodes_Cur;
	fetch next from WayNodes_Cur into @Longitude,@Latitude
	if @Longitude is not null
		set @Found = 1
	while @@FETCH_STATUS = 0
	Begin
		set @EdgeString = @EdgeString + CAST(@Longitude as varchar(32)) + ' ' + CAST(@Latitude as varchar(32)) + ' ,';
		fetch next from WayNodes_Cur into @Longitude,@Latitude	
	END
	Close WayNodes_Cur;
	DEALLOCATE WayNodes_Cur;
	
	set @EdgeString = substring(@EdgeString,0,len(@EdgeString)) + ' )';
	if(@Found = 1)
		SET @EdgeGeography = geography::Parse(@EdgeString);
	 
	RETURN @EdgeGeography;
END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DrawEdges]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].DrawEdges
GO
if exists (select * from sys.types where name='TblEdges')
BEGIN
	drop type TblEdges
END
GO
CREATE TYPE dbo.TblEdges AS TABLE(EdgeID int);
GO

CREATE FUNCTION DrawEdges 
(	
	@EdgeIDTbl dbo.TblEdges Readonly
)
RETURNS TABLE 
AS
RETURN 
(
	select top 5000 waytags.tagvalue as WayName,edgetags.tagvalue as Distance, dbo.DrawEdge(edges.edgeid) as Geography
	from edges inner join Ways on edges.edgeid in (select edgeid from @EdgeIDTbl) and Ways.WayID=edges.WayID 
	left outer join waytags on waytags.wayid=ways.wayid and waytags.tagname='name'
	left outer join edgetags on edges.edgeid=edgetags.edgeid and edgetags.tagname='Distance'
)
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNodeByCords]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetNodeByCords]
GO
CREATE FUNCTION GetNodeByCords 
(	
	@Longitude float,
	@Latitude float
)
-- select * from GetNodeByCords(31.3181264,30.0818767)
RETURNS TABLE 
AS
RETURN 
(
	select Nodes.NodeID,geog4326,UID,User_N,IsVisible,Version,isnull(tagvalue,'') as Name from Nodes 
	left outer join  NodeTags nt on Nodes.NodeID=nt.NodeID and tagname='name'
	where Longitude = @Longitude and Latitude=@Latitude
)
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNodeByTag]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetNodeByTag]
GO
CREATE FUNCTION GetNodeByTag 
(	
	@TagName varchar(32) = NULL,
	@TagValue nvarchar(32) = NULL
)
-- select * from GetNodeByTag(NULL,'cinema')
RETURNS TABLE 
AS
RETURN 
(
	select Nodes.NodeID,Longitude,Latitude,geog4326,isnull(tagvalue,'') as Name 
	from Nodes left outer join NodeTags on Nodes.NodeID=NodeTags.NodeID
	and NodeTags.TagName='name' 
	where Nodes.NodeID in (
						select DISTINCT n.NodeID
						from Nodes n inner join NodeTags NT on NT.NodeID = n.NodeID
						where (@TagName is NULL or TagName like '%' + @TagName +'%') and 
								(@TagValue is NULL or TagValue like N'%' + @TagValue + N'%')
					)
)
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IfNodeIsInCircle]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[IfNodeIsInCircle]
GO
CREATE FUNCTION IfNodeIsInCircle 
(	
	@NodeID NodeID,
	@CenterNodeID	NodeID,
	@Radius decimal(21,6)
)
-- select dbo.IfNodeIsInCircle(670117016,670117058,0.01) 
RETURNS bit 
AS
BEGIN
declare @Distance decimal(21,6),@bInCircle bit
select @Distance = sqrt((centerNode.Longitude - Node.Longitude)*(centerNode.Longitude - Node.Longitude) + (centerNode.Latitude - Node.Latitude)*(centerNode.Latitude - Node.Latitude)) 
from Nodes centerNode,Nodes Node where centerNode.NodeID=@CenterNodeID and Node.NodeID=@NodeID
if(@Distance <= @Radius)
	SET @bInCircle = 1;
else 
	SET @bInCircle = 0;
	
Return @bInCircle;
END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDistance]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetDistance]
GO
Create Function [dbo].[GetDistance] 
( 
      @Node1ID NodeID, 
      @Node2ID NodeID, 
      @ReturnType VarChar(10) 
)
-- select dbo.GetDistance(316663107,908599479,'Meters') 
Returns Float(18)
AS
Begin
      Declare @R Float(8); 
      Declare @dLat Float(18); 
      Declare @dLon Float(18); 
      Declare @a Float(18); 
      Declare @c Float(18); 
      Declare @d Float(18);
      Declare @Node1Long float , @Node1Lat float,@Node2Long float , @Node2Lat float
      select @Node1Long = Node1.Longitude, @Node1Lat = Node1.Latitude, @Node2Long = Node2.Longitude, @Node2Lat = Node2.Latitude
      from Nodes Node1,Nodes Node2 where Node1.NodeID=@Node1ID and Node2.NodeID=@Node2ID
      Set @R =  
            Case @ReturnType  
            When 'Miles' Then 3956.55  
            When 'Kilometers' Then 6367.45 
            When 'Feet' Then 20890584 
            When 'Meters' Then 6367450 
            Else 20890584 -- Default feet (Garmin rel elev) 
            End
      Set @dLat = Radians(@Node2Lat - @Node1Lat);
      Set @dLon = Radians(@Node2Long - @Node1Long);
      Set @a = Sin(@dLat / 2)  
                 * Sin(@dLat / 2)  
                 + Cos(Radians(@Node1Lat)) 
                 * Cos(Radians(@Node2Lat))  
                 * Sin(@dLon / 2)  
                 * Sin(@dLon / 2); 
      Set @c = 2 * Asin(Min(Sqrt(@a))); 

      Set @d = @R * @c; 
      Return @d; 
End
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNearestFromNode]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetNearestFromNode]
GO
CREATE FUNCTION GetNearestFromNode 
(	
	@NodeID NodeID,
	@TagName nvarchar(32) = NULL,
	@TagValue nvarchar(32) = NULL,
	@Radius float = 0.0001,
	@IncreaseRadiusBy float = 0.0005	,
	@MaxRadius float = 1
)
-- select * from GetNearestFromNode(612579430,NULL,'hospital',0.0001,0.001,1)
Returns @Node Table
(
				NodeID NodeID,
				Name	nvarchar(32),
				Longitude float,
				Latitude float,
				geog4326 geography)
As
Begin
	insert into @Node select n.NodeID,NodeTags.TagValue,n.Longitude,n.Latitude,geog4326 
	from NodeTags nt inner join Nodes n on n.NodeID=nt.NodeID 
			and (@TagName IS NULL or @TagName = nt.TagName ) 
			and (@TagValue IS NULL or @TagValue = nt.TagValue ) 
			and dbo.IfNodeIsInCircle(n.NodeID,@NodeID,@Radius) = 1 
			left outer join NodeTags on n.NodeID = NodeTags.NodeID and NodeTags.TagName = 'name'
	while (@@ROWCOUNT =0 and @Radius < @MaxRadius)
	Begin
		SET @Radius = @Radius + @IncreaseRadiusBy;
		
		insert into @Node select n.NodeID,NodeTags.TagValue,n.Longitude,n.Latitude,geog4326 
			from NodeTags nt inner join Nodes n on n.NodeID=nt.NodeID 
			and (@TagName IS NULL or @TagName = nt.TagName ) 
			and (@TagValue IS NULL or @TagValue = nt.TagValue ) 
			and dbo.IfNodeIsInCircle(n.NodeID,@NodeID,@Radius) = 1 
			left outer join NodeTags on n.NodeID = NodeTags.NodeID and NodeTags.TagName = 'name'
	end
	Return 
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllWays]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetAllWays]
GO
CREATE FUNCTION GetAllWays 
(	
)
-- select * from GetAllWays()
RETURNS TABLE 
AS
RETURN 
(
	SELECT w.wayid as WayID,w.geog4326,wt.tagvalue as WayName,geog4326.STLength() as WayLength from Ways w 
	left outer join Waytags wt on w.wayid=wt.wayid
	and wt.tagname='name'
)
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllNodes]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetAllNodes]
GO
CREATE FUNCTION GetAllNodes 
(	
)
-- select * from GetAllNodes()
RETURNS TABLE 
AS
RETURN 
(
	SELECT n.NodeID,n.longitude,n.latitude,n.geog4326,nt.tagvalue as Name from Nodes n 
	inner join Nodetags nt on n.NodeID=nt.NodeID
	and nt.tagname='name'
)
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNodeMaxMin]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetNodeMaxMin]
GO
CREATE FUNCTION GetNodeMaxMin 
(	
)
-- select * from GetNodeMaxMin()
RETURNS TABLE 
AS
RETURN 
(
	select MIN(longitude) xMin,MAX(longitude) xMax,MIN(Latitude) yMin,MAX(Latitude) yMax 
	from waynodes inner join Nodes on Nodes.nodeid=WayNodes.NodeID
)
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEdgeByID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetEdgeByID]
GO
CREATE FUNCTION GetEdgeByID 
(	
	@EdgeID int
)
-- select * from GetEdgeByID(5957)
RETURNS TABLE 
AS
RETURN 
(
	SELECT w.wayid as WayID,dbo.DrawEdge(@EdgeID) as geog4326,wt.tagvalue as WayName,dbo.DrawEdge(@EdgeID).STLength() as WayLength 
	from Edges e inner join Ways w on e.EdgeID = @EdgeID and w.WayID=e.WayID
	left outer join Waytags wt on w.wayid=wt.wayid
	and wt.tagname='name'
)
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNodeByID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetNodeByID]
GO
CREATE FUNCTION GetNodeByID 
(	
	@NodeID NodeID
)
-- select * from GetNodeByID(607250058)
RETURNS TABLE 
AS
RETURN 
(
	SELECT n.Longitude,n.Latitude, n.geog4326
	from Nodes n where n.NodeID = @NodeID
)
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNearstWay]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetNearstWay]
GO
CREATE FUNCTION GetNearstWay 
(	
	@Longitude float,
	@Latitude float,
	@BufferSpace int = 30
)
-- select * from GetNearstWay(31.40132,30.15994,30)
RETURNS TABLE 
AS
RETURN 
(
	SELECT TOP 10 w.wayid as WayID,w.geog4326,wt.tagvalue as WayName,geog4326.STLength() as WayLength from Ways w 
	left outer join Waytags wt on w.wayid=wt.wayid
	and wt.tagname='name'
	where  geog4326.STDistance(geography::Point(@Latitude,@Longitude,4326))<=@BufferSpace
	order by wt.tagvalue desc
)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsNodeInEdge]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[IsNodeInEdge]
GO
CREATE FUNCTION IsNodeInEdge 
(	
	@NodeID NodeID,
	@EdgeID int
)
-- select dbo.IsNodeInEdge(1773768712,1)
RETURNS bit 
AS
BEGIN
Declare @Node1OrderID int , @Node2OrderID int
DECLARE @Found bit;
select @Node1OrderID = ID from WayNodes wn inner join Edges e on e.edgeid=@EdgeID and e.WayID=wn.WayID and e.Node1ID = wn.NodeID order by ID desc
select @Node2OrderID = ID from WayNodes wn inner join Edges e on e.edgeid=@EdgeID and e.WayID=wn.WayID and e.Node2ID = wn.NodeID order by ID desc
IF EXISTS (select 1 from Edges e inner join Ways w on e.WayID = w.WayID
	inner join WayNodes wn on wn.WayID=w.WayID and wn.NodeID=@NodeID and e.edgeid=@EdgeID and ID between @Node1OrderID and @Node2OrderID )
	SET @Found = 1;
else 
	SET @Found = 0;
RETURN @Found
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEdgeByCords]') AND type in (N'P', N'PC'))
DROP proc [dbo].[GetEdgeByCords]
GO
CREATE proc GetEdgeByCords 
(	
	@Longitude float,
	@Latitude float,
	@Radius decimal(21,6) = 0.1
)
-- exec GetEdgeByCords 31.40132,30.15994,0.1
AS
BEGIN
DECLARE @NodeID NodeID
if(@Radius=0)
	select @NodeID = NodeID from Nodes where Longitude=@Longitude and Latitude=@Latitude
else select top 1 @NodeID=NodeID from dbo.GetNearestNodes(@Longitude,@Latitude,@Radius) 

select e.EdgeID, e.wayid as WayID,dbo.DrawEdge(e.edgeID) as geog4326,wt.tagvalue as WayName,dbo.DrawEdge(e.edgeID).STLength() as EdgeLength 
from edges e inner join Waytags wt on e.wayid=wt.wayid and wt.tagname='name' 
where e.EdgeID in (
			SELECT e.EdgeID
			from Edges e inner join Ways w on e.WayID = w.WayID 
			inner join WayNodes on WayNodes.WayID=w.WayID and WayNodes.NodeID=@NodeID
			)
and dbo.IsNodeInEdge(@NodeID,e.EdgeID) = 1
END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNearestNodes]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetNearestNodes]
GO
CREATE FUNCTION GetNearestNodes 
(	
	@Longitude float,
	@Latitude float,
	@Radius decimal(21,6)
)
-- select * from dbo.GetNearestNodes(31.40132,30.15994,0.01) 
RETURNS Table 
AS
RETURN (
select Node.NodeID,geog4326,UID,User_N,IsVisible,Version,isnull(tagvalue,'') as Name  ,Longitude,Latitude
from Nodes Node inner join NodeTags nt on nt.NodeID=Node.NodeID and TagName='name'
where sqrt((@Longitude - Node.Longitude)*(@Longitude - Node.Longitude) + (@Latitude - Node.Latitude)*(@Latitude - Node.Latitude))  <=@Radius
)
GO