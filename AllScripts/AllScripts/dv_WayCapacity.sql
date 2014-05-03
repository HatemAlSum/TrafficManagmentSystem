CREATE VIEW [dbo].[WayCapacity]
AS
select W.WayID,Sum(convert(decimal,ET.TagValue)) as Distance,
Case dbo.isHighways (W.WayID)
When 1 then 'Highway'
else        'Regular'
end as WayClass,
Case dbo.isHighways (W.WayID)
when 1 then 
	convert(int, ROUND( Sum(convert(decimal,ET.TagValue)) /5 * 4,0)) 
else
	convert(int, ROUND( Sum(convert(decimal,ET.TagValue)) /5 * 2,0)) 
end AS NoOfCars
from Edges E
inner join EdgeTags ET on E.EdgeID=ET.EdgeID and ET.TagName = 'Distance'
inner join Ways W on E.WayID =W.WayID
group by W.WayID