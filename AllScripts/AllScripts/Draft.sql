USE Maps
GO
select * from WayNodes
select * from WayTags where TagValue like N'%„Ìœ«‰ «· Õ—Ì—%'
select * from WayTags where WayID=10873191
select * from WayNodes where WayID=10873186
select * from WayNodes where nodeid=96621058

select distinct TagName,TagValue from WayTags
select * from nodes
select	* from NodeTags where TagName ='amenity'
select * from Ways
select * from WayNodes
select nodeid,COUNT(*) as no_inter from WayNodes group by nodeid having COUNT(*) >1
select WayNodes.WayID,COUNT(*) as no_nodes from WayNodes inner join Ways on Ways.WayID=WayNodes.WayID group by WayNodes.WayID order by COUNT(*)
select distinct nodeid from WayNodes
select * from WayTags where WayID in (174647078,
174525899)

select * from WayNodes where WayID=10873186

select Ways.WayID,WayTags.TagValue,geog4326.STLength() as WayLength,
geog4326 from Ways 
left join WayTags on Ways.WayID=WayTags.WayID
and WayTags.TagName='name'
where Ways.WayID=10873186 or Ways.WayID in (select distinct WayID from WayNodes where nodeid in (select nodeid from WayNodes where WayID=10873186))

DECLARE @g geography; 
SET @g = geography::Parse('LINESTRING(-122.360 47.656, -122.343 47.656)');
SELECT @g.ToString();
select * from WayNodes where WayID=165849552
alter table edges drop column istwoway
select * from edges inner join nodes on nodes.nodeid=edges.Node1ID
select * from edges where WayID=10873186
select Node1ID,COUNT(*) from edges group by Node1ID order by COUNT(*)
select * from WayNodes inner join WayTags on WayNodes.WayID=WayTags.WayID where nodeid=366496927

select top 5000 geography::Parse('LINESTRING(' + CAST(CAST(n1.Longitude AS decimal(18,9)) AS varchar(32)) + ' ' + CAST(CAST(n1.Latitude AS decimal(18,9)) AS varchar(32)) + ', '
						+ CAST(CAST(n2.Longitude AS decimal(18,9)) AS varchar(32)) + ' ' + CAST(CAST(n2.Latitude AS decimal(18,9)) AS varchar(32))+')')
from edges inner join nodes n1 on n1.nodeid = edges.Node1ID
inner join nodes n2 on n2.nodeid=edges.Node2ID
inner join Ways on Ways.WayID=edges.WayID
order by Ways.WayID
where Ways.WayID=10873186 or Ways.WayID in (select distinct WayID from WayNodes where nodeid in (select nodeid from WayNodes where WayID=10873186))

select * from edges where node1id=1773768710

select Ways.WayID,WayTags.TagValue,geog4326.STLength() as WayLength,
geog4326 from Ways 
inner join WayTags on Ways.WayID=WayTags.WayID
and WayTags.TagName='name'
and waytags.tagvalue like '%„œÌ‰… «·”·«„%'
96621052.000000
34712118.000000
select * from waynodes where wayid=5091223----10873186
select * from edges
select * from edgetags order by TagValue
select * from nodes inner join nodetags on nodes.nodeid=nodetags.nodeid
select Ways.WayID,WayTags.TagValue,geog4326.STLength() as WayLength,
geog4326 from Ways 
left join WayTags on Ways.WayID=WayTags.WayID
and WayTags.TagName='name'
where Ways.WayID=10873186 or Ways.WayID in (select distinct WayID from WayNodes where nodeid in (select nodeid from WayNodes where WayID=10873186))


select * from edges inner join ways on ways.wayid=edges.wayid left outer join waytags on ways.wayid=waytags.wayid where node2id=315743355 and waytags.tagname='name'

select *from waynodes where nodeid=34712118
select * from waytags where wayid=5091223
select * from Ways--5550

select distinct Node1ID from Edges
select * from WayNodes where wayid=165849552
select * from ways where wayid=165849552
select * from nodes

select * from NodeTags where NodeID in (1770391007.000000)
select distinct tagname,tagvalue from NodeTags where TagName in (
'shop',
'sport',
'amenity')
select TagName,MIN(tagvalue) from wayTags where tagname not like 'name%' group by TagName 
select 'LINESTRING(' + CAST(CAST(n1.Longitude AS decimal(18,9)) AS varchar(32)) + ' ' + CAST(CAST(n1.Latitude AS decimal(18,9)) AS varchar(32)) + ', '
						+ CAST(CAST(n2.Longitude AS decimal(18,9)) AS varchar(32)) + ' ' + CAST(CAST(n2.Latitude AS decimal(18,9)) AS varchar(32))+')'
from edges inner join nodes n1 on n1.nodeid = edges.Node1ID
inner join nodes n2 on n2.nodeid=edges.Node2ID
inner join Ways on Ways.WayID=edges.WayID
order by Ways.WayID
declare @s varchar(100)
set @s = 'LINESTRING()'
--select SUBSTRING (@s,0,len(@S))
select geography::Parse(@s)


select * from GetWayByName('Âœ«— «·„‘«…')


select distinct tagname,tagvalue from NodeTags where TagName in (
'shop',
'sport',
'amenity')

select distinct tagname,tagvalue from WayTags where TagName in (
'tourism',
'shop',
'leisure',
'amenity')

select * from #nodes
select distinct tagname,tagvalue from WayTags where TagName in (
'width',
'oneway',
'lanes',
'highway',
'maxspeed',
'bridge',
'Distance')
select * from Edges where WayID in (30925785,
47518105,
78352977)
select * from WayTags where WayID=52069892
select * from ways inner join WayTags on WayTags.WayID = Ways.WayID and WayTags.TagName='name' and TagValue=N'«·‰Ì·'
select * from Nodes

SELECT distinct n.NodeID--,n.longitude,n.latitude,n.geog4326,nt.tagvalue as Name 
from Nodes n 
	inner join Nodetags nt on n.NodeID=nt.NodeID
	and nt.tagname='name'

select * from NodeTags where TagName='name'
select *,GEOMETRY::STLineFromText(geog4326.ToString(), 4326),geog4326.ToString() from ways
select WayID,GEOMETRY::STLineFromText(geog4326.ToString(), 4326) from ways
select *,geog4326.ToString() from Ways where WayID=4987197
SELECT GEOMETRY::STLineFromText('LINESTRING (30049.1336 31226.9638 ,30049.2161 31226.8142 ,30049.2933 31226.7363 ,30050.1801 31226.6234 ,30050.2774 31226.6136 )',4326)
, GEOMETRY::STLineFromText('LINESTRING (31226.9638 30049.1336,  31226.8142 30049.2161)',4326)
LINESTRING (12.269638 00.491336, 12.268142 00.492161, 12.267363 00.492933, 12.266234 00.501801, 12.266136 00.502774)
SELECT GEOMETRY::STLineFromText('LINESTRING (26.9638 49.1336, 26.8142 49.2161, 26.7363 49.2933, 26.6234 50.1801, 26.6136 50.2774)',4326)

select * from Nodes where 
select * from Edges where WayID=174647078

select MIN(longitude),MAX(longitude),MIN(Latitude),MAX(Latitude) from waynodes inner join Nodes on Nodes.nodeid=WayNodes.NodeID
select geog4326.ToString() from ways
select * from GetWayByName('')
select * from GetWayByTag(NULL,NULL)
select * from WayTags
select * from ways
select * from edgesnodes
select * from NodeTags
select * from WayNodes where WayID=165849552
select * from Edges where WayID=165849552 
select geog4326.ToString() from GetAllWays() where wayname like N'%«·ÿ—Ìﬁ «·œ%'
select * from WayNodes wn inner join Edges e on e.edgeid=1 and e.WayID=wn.WayID and e.Node2ID = wn.NodeID
select * from Nodes where exists (select 1 from NodeTags where NodeTags.NodeID=NodeID and TagName like 'name%' and TagName <>'name') 
and not exists (select 1 from NodeTags where NodeTags.NodeID=NodeID and TagName <>'name') 

select * from Edges where EdgeID=7920
select * from Ways where WayID=31590420

DECLARE @TblWightFactors dbo.TblWightFactors
insert into @TblWightFactors values('Distance',100,0);
insert into @TblWightFactors values('maxspeed',100,60);
insert into @TblWightFactors values('lanes',-10,2);
insert into @TblWightFactors values('Preferred',-1,1);
insert into @TblWightFactors values('Weight',1,0);
insert into @TblWightFactors values('Flow',-1,1);
DECLARE @FromNode NodeID,@ToNode NodeID
select @ToNode = dbo.GetRandomNodeInWay(N'’›ÿ «··»‰')
select @FromNode = dbo.GetRandomNodeInWay(N'«·ÿÌ—«‰')
select @FromNode,@ToNode
select dbo.GetEdgeWeight(13374,150,@TblWightFactors)
exec Dijkstra @FromNode,@ToNode,1,@TblWightFactors
exec Dijkstra 316663107,908599479

select * from Edges where EdgeID=7920
select * from Nodes inner join NodeTags on Nodes.NodeID=NodeTags.nodeid where Nodes.NodeID=677289991.000000
select * from edgetags where edgeid=7920 in (select EdgeID from edges where WayID=24672952)
update EdgeTags set TagValue=0 where edgeid=7920 and TagN
insert into NodeTags values(677289991.000000,'name',N'???? ?????')
exec IncreaseWeight 'W',31590420,500