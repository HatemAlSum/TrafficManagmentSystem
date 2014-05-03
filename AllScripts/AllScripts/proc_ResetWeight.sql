USE Maps
GO
if exists (select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME ='ResetWeight')
Begin 
	drop proc  ResetWeight
End
Go
Create Procedure ResetWeight
/*
	CreationDate	: 02/05/2013 
	Purpose			: Reset all Weights for Edges 
*/
as 
	Update Maps..EdgeTags
	Set TagValue=0
	where TagName='Weight'
GO