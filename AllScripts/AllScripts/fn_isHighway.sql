
CREATE FUNCTION [dbo].[isHighways]
(
	@WayID int
)
RETURNS bit
AS
BEGIN

declare @highway int=0
declare @isHighway bit=0
SELECT     @highway=count(1)
FROM         WayTags
WHERE     (TagName = 'highway') AND (TagValue IN (N'primary', N'secondary'))
		   and WayID=@WayID

if (@highway > 0)
	set @isHighway=1
else
	set @isHighway=0

return @isHighway
END

GO
