USE Maps
GO
if exists (select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME ='DecreaseWeight')
Begin 
	drop proc  DecreaseWeight
End
Go
Create Procedure DecreaseWeight
/*
	CreationDate	: 02/05/2013 
	Purpose			: decrease Weight Value for both edges and ways 
	@type	: contain two values E for Edges and W for Ways
	@id		: eigther EdgeId or WayID
	@weight : Value for weight tag deducted to current Value.
*/
@type Char(1)='E',
@id	int=-1,
@weight decimal=50
as 
if(@id=-1)
Begin
	Update Maps..EdgeTags
	Set TagValue=TagValue-@weight
	where TagName='Weight'
End
else if(@type='E') --Edges
Begin
	Update Maps..EdgeTags
	Set TagValue=TagValue-@weight
	where TagName='Weight' and EdgeID=@id
End
else if(@type='W')
	Update Maps..EdgeTags
	Set TagValue=TagValue-@weight
	where TagName='Weight' and EdgeID in
	(			
		select E.EdgeID from Edges E 
		inner join Ways W on W.WayID=E.WayID and W.WayID=@id
	)
GO
