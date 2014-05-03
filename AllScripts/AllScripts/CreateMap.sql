USE Maps
GO
-----------------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------   Declarations   ------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'NodeID' AND ss.name = N'dbo')
DROP TYPE [dbo].[NodeID]
GO
CREATE TYPE [dbo].[NodeID] FROM [decimal](21, 6) NOT NULL
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Nodes]') AND type in (N'U'))
DROP TABLE [dbo].[Nodes]
GO
CREATE TABLE [dbo].[Nodes](
	[NodeID] [dbo].[NodeID] NOT NULL,
	[Longitude] [float] NULL,
	[Latitude] [float] NULL,
	[geog4326] [geography] NULL,
	[UID] [int] NULL,
	[User_N] [nvarchar](50) NULL,
	[IsVisible] [bit] NULL,
	[Version] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[NodeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NodeTags]') AND type in (N'U'))
DROP TABLE [dbo].[NodeTags]
GO
CREATE TABLE [dbo].[NodeTags](
	[NodeID] [dbo].[NodeID] NOT NULL,
	[TagName] [varchar](32) NOT NULL,
	[TagValue] [nvarchar](32) NULL,
PRIMARY KEY CLUSTERED 
(
	[NodeID] ASC,
	[TagName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NodeTags]  WITH CHECK ADD  CONSTRAINT [FK_NodeTags_Nodes] FOREIGN KEY([NodeID])
REFERENCES [dbo].[Nodes] ([NodeID])
GO
ALTER TABLE [dbo].[NodeTags] CHECK CONSTRAINT [FK_NodeTags_Nodes]
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ways]') AND type in (N'U'))
DROP TABLE [dbo].[Ways]
GO
CREATE TABLE [dbo].[Ways](
	[WayID] [int] NOT NULL,
	[geog4326] [geography] NULL,
PRIMARY KEY CLUSTERED 
(
	[WayID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WayTags]') AND type in (N'U'))
DROP TABLE [dbo].[WayTags]
GO
CREATE TABLE [dbo].[WayTags](
	[WayID] [int] NOT NULL,
	[TagName] [varchar](32) NOT NULL,
	[TagValue] [nvarchar](32) NULL,
PRIMARY KEY CLUSTERED 
(
	[WayID] ASC,
	[TagName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WayTags]  WITH CHECK ADD  CONSTRAINT [FK_WayTags_Ways] FOREIGN KEY([WayID])
REFERENCES [dbo].[Ways] ([WayID])
GO
ALTER TABLE [dbo].[WayTags] CHECK CONSTRAINT [FK_WayTags_Ways]
GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WayNodes]') AND type in (N'U'))
DROP TABLE [dbo].[WayNodes]
GO
CREATE TABLE [dbo].[WayNodes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WayID] [int] NULL,
	[NodeID] [dbo].[NodeID] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WayNodes]  WITH CHECK ADD  CONSTRAINT [FK_WayNodes_Nodes] FOREIGN KEY([NodeID])
REFERENCES [dbo].[Nodes] ([NodeID])
GO
ALTER TABLE [dbo].[WayNodes] CHECK CONSTRAINT [FK_WayNodes_Nodes]
GO
ALTER TABLE [dbo].[WayNodes]  WITH CHECK ADD  CONSTRAINT [FK_WayNodes_Ways] FOREIGN KEY([WayID])
REFERENCES [dbo].[Ways] ([WayID])
GO
ALTER TABLE [dbo].[WayNodes] CHECK CONSTRAINT [FK_WayNodes_Ways]
GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Edges]') AND type in (N'U'))
DROP TABLE [dbo].[Edges]
GO
CREATE TABLE [dbo].[Edges](
	[EdgeID] [int] IDENTITY(1,1) NOT NULL,
	[WayID] [int] NULL,
	[Node1ID] [dbo].[NodeID] NOT NULL,
	[Node2ID] [dbo].[NodeID] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EdgeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Edges]  WITH CHECK ADD  CONSTRAINT [FK_Edges_Nodes] FOREIGN KEY([Node1ID])
REFERENCES [dbo].[Nodes] ([NodeID])
GO
ALTER TABLE [dbo].[Edges] CHECK CONSTRAINT [FK_Edges_Nodes]
GO
ALTER TABLE [dbo].[Edges]  WITH CHECK ADD  CONSTRAINT [FK_Edges_Nodes1] FOREIGN KEY([Node2ID])
REFERENCES [dbo].[Nodes] ([NodeID])
GO
ALTER TABLE [dbo].[Edges] CHECK CONSTRAINT [FK_Edges_Nodes1]
GO
ALTER TABLE [dbo].[Edges]  WITH CHECK ADD  CONSTRAINT [FK_Edges_Ways] FOREIGN KEY([WayID])
REFERENCES [dbo].[Ways] ([WayID])
GO
ALTER TABLE [dbo].[Edges] CHECK CONSTRAINT [FK_Edges_Ways]
GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EdgeTags]') AND type in (N'U'))
DROP TABLE [dbo].[EdgeTags]
GO
CREATE TABLE [dbo].[EdgeTags](
	[EdgeID] [int] NOT NULL,
	[TagName] [varchar](32) NOT NULL,
	[TagValue] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[EdgeID] ASC,
	[TagName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[EdgeTags]  WITH CHECK ADD  CONSTRAINT [FK_EdgeTags_Edges] FOREIGN KEY([EdgeID])
REFERENCES [dbo].[Edges] ([EdgeID])
GO
ALTER TABLE [dbo].[EdgeTags] CHECK CONSTRAINT [FK_EdgeTags_Edges]
GO

-----------------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------   Loading XML Map File   ----------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @x xml;
SET @x = (SELECT * FROM OPENROWSET(
BULK 'C:\Users\hatimalsum\Dropbox\Master\AAST CS 712 - Algorithmic Graph Theory\Project\Database\AllScripts\AllScripts\Cairo.osm',
SINGLE_BLOB) AS x);

-------------------------------------------------------------------
-- Nodes
INSERT INTO Nodes
SELECT
OSMnode.value('@id', 'decimal') AS nodeid,
OSMnode.value('@lon', 'float') AS longitude,
OSMnode.value('@lat', 'float') AS latitude,
geography::Point(OSMnode.value('@lat', 'float'), OSMnode.value('@lon', 'float'), 4326) AS geog4326,
OSMnode.value('@uid', 'int') AS uid,
OSMnode.value('@user', 'nvarchar(50)') AS user_n,
OSMnode.value('@visible', 'bit') AS visible,
OSMnode.value('@version', 'int') AS version
FROM
@x.nodes('/osm/node') AS OSM(OSMnode);

-------------------------------------------------------------------
-- NodeTags
INSERT INTO NodeTags
SELECT
OSMNode.e.value('(@id)[1]', 'decimal') AS 'nodeID',
OSMNodeTag.e.value('@k', 'nvarchar(32)') AS 'TagName',
OSMNodeTag.e.value('@v', 'nvarchar(32)') AS 'TagValue'
FROM
@x.nodes('/osm/node') AS OSMNode(e)
CROSS APPLY
OSMNode.e.nodes('tag') AS OSMNodeTag(e);

-------------------------------------------------------------------
-- Ways
INSERT INTO Ways(wayid)
SELECT
OSMWay.e.value('(@id)[1]', 'int') AS 'WayID'
FROM
@x.nodes('/osm/way') AS OSMWay(e);

-------------------------------------------------------------------
-- WayTags
INSERT INTO WayTags
SELECT
OSMWay.e.value('(@id)[1]', 'int') AS 'WayID',
OSMWayTag.e.value('@k', 'nvarchar(32)') AS 'TagName',
OSMWayTag.e.value('@v', 'nvarchar(32)') AS 'TagValue'
FROM
@x.nodes('/osm/way') AS OSMWay(e)
CROSS APPLY
OSMWay.e.nodes('tag') AS OSMWayTag(e);

SELECT
w.wayid,
wtn.TagValue AS wayname,
wt.TagValue AS highwaytype
FROM
ways w
INNER JOIN waytags wt ON w.wayid = wt.wayid AND wt.TagName = 'Highway'
LEFT JOIN waytags wtn ON w.wayid = wtn.wayid AND wtn.TagName = 'Name'
WHERE
--wt.TagValue IN ('motorway', 'motorway_Link', 'trunk', 'trunk_Link', 'primary', 'primary_Link', 'secondary', 'tertiary', 'residential')
wt.TagValue IN ('motorway', 'motorway_Link','trunk', 'trunk_Link', 'primary', 'primary_Link')
-------------------------------------------------------------------
-- WayNodes
INSERT INTO WayNodes (wayid, nodeid)
SELECT
OSMWay.e.value('(@id)[1]', 'int') AS 'WayID',
OSMWayNode.e.value('(@ref)[1]', 'decimal') AS 'NodeID'
FROM
@x.nodes('/osm/way') AS OSMWay(e)
CROSS APPLY
OSMWay.e.nodes('nd') AS OSMWayNode(e)
----------------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------   Updating Geometry Data   -------------------------------------------------------------------
DECLARE @count int
DECLARE @wayid int
DECLARE @g geography
DECLARE @gm GEOMETRY
DECLARE @c varchar(max)
SELECT @count=count(*) FROM ways WHERE geog4326 IS NULL
SELECT TOP 1 @wayid = wayid from ways where geog4326 is null
SELECT @count, @wayid
WHILE (@count > 0)
BEGIN
SELECT TOP 1 @wayid = wayid from ways where geog4326 is null
SET @c = 'LINESTRING(' + STUFF((
SELECT ',' + CAST(CAST(n.Longitude AS decimal(18,9)) AS varchar(32)) + ' ' + CAST(CAST(n.Latitude AS decimal(18,9)) AS varchar(32)) AS [text()]
FROM
ways w JOIN waynodes wn ON w.wayid = wn.wayid
JOIN nodes n ON wn.nodeid = n.nodeid
WHERE wn.wayid = @wayid
ORDER BY w.wayid, ID
FOR XML PATH(''), TYPE
).value('/', 'NVARCHAR(MAX)'),1,1,'') +')'
SET @gm = GEOMETRY::STLineFromText(@c, 4326);
IF @gm.STIsValid() = 0
BEGIN
SET @gm = @gm.MakeValid()
END
SELECT @g = geography::STGeomFromText(@gm.STAsText(), 4326);
UPDATE ways
SET geog4326 = @g
WHERE wayid = @wayid
SELECT @count = @count - 1
END

----------------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------   Building Graph Objects (V,E)   -------------------------------------------------------------
declare waynodes_cur cursor for select wayid,nodeid,ID from waynodes
declare @way_id int , @nodeid NodeID,@ID int ,@StartNode NodeID
open waynodes_cur
fetch next from waynodes_cur into @way_id,@nodeid,@ID
set @StartNode = -1;
while (@@FETCH_STATUS =0 )
Begin
	if @StartNode=-1
		set @StartNode = @nodeid;
		
	if(@nodeid<> @StartNode and exists(select 1 from WayNodes where WayID<>@way_id and NodeID=@nodeid /*Way Intersection*/) )
	BEGIN
		insert into Edges(node1id,node2id,wayid) select @StartNode,@nodeid,@way_id;
		if @ID = (select MAX(ID) from WayNodes where WayID = @way_id /*Last Node in Way*/)
			set @StartNode = -1;
		else
			set @StartNode = @nodeid
	END
	else if (@nodeid<> @StartNode and @ID = (select MAX(ID) from WayNodes where WayID = @way_id /*Last Node in Way*/))
	BEGIN
		insert into Edges(node1id,node2id,wayid) select @StartNode,@nodeid,@way_id;
		set @StartNode = -1;		
	END
	fetch next from waynodes_cur into @way_id,@nodeid,@ID
END
close waynodes_cur
deallocate waynodes_cur
insert into EdgeTags select edgeid,'Distance',dbo.GetDistance(n1,n2,'Meters') from edges inner join nodes n1 on n1.nodeid = edges.node1id
inner join nodes n2 on n2.nodeid=edges.node2id
inner join ways on ways.wayid=edges.wayid
----------------------------------------------------------------------------------------------------------------------------------------------------------
--SetIntialWeight Tags :hatim alsum :S
delete EdgeTags where TagName='Weight'
go
insert into EdgeTags
select EdgeID,'Weight',0 from EdgeTags 
go
--SetIntialWeight Tags :hatim alsum :F

SELECT top 5000
ways.wayid,waytags.tagvalue,geog4326.STLength() as WayLength,
geog4326
FROM ways inner join waytags on ways.wayid=waytags.wayid
and waytags.tagname='name'

