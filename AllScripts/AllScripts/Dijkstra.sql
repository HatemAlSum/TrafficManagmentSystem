USE Maps;
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dijkstra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Dijkstra]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEdgeWeight]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].GetEdgeWeight
GO
if exists (select * from sys.types where name='TblWightFactors')
BEGIN
	drop type TblWightFactors
END
GO
CREATE TYPE dbo.TblWightFactors AS TABLE(TagName varchar(32),Factor float, DefaultValue decimal(21,6));
GO

CREATE FUNCTION GetEdgeWeight 
(	
	@EdgeID	int,
	@Distance decimal(21,6),
	@TblWightFactors dbo.TblWightFactors Readonly
)
RETURNS Decimal(21,6)
AS
BEGIN
	Declare @Weight decimal(21,6)
	select  @Weight = sum(convert(decimal(21,6),isnull(et.TagValue,F.DefaultValue)) * F.Factor) 
		from Edges e inner join EdgeTags et on e.EdgeID = @EdgeID and e.EdgeID = et.EdgeID
		inner join @TblWightFactors F on et.TagName = F.TagName
	select  @Weight = @Weight + sum(convert(decimal(21,6),
		isnull(case when wt.TagName = 'maxspeed' then convert(decimal(21,6),wt.TagValue)/@Distance else wt.TagValue end,F.DefaultValue)) * F.Factor) 
		from Edges e inner join WayTags wt on e.EdgeID = @EdgeID and wt.WayID = e.WayID 
		right outer join @TblWightFactors F on wt.TagName = F.TagName
	Return @Weight
END
GO


CREATE PROCEDURE [dbo].[Dijkstra] (@StartNode decimal(21,0), @EndNode decimal(21,0) = NULL, @DrawResult bit=0,@TblWightFactors TblWightFactors Readonly ) 
-- Dijkstra 96621052,34712118,1
AS
BEGIN
	-- Runs Dijkstras algorithm from the specified node.
	-- @StartNode: Id of node to start from.
	-- @EndNode: Stop the search when the shortest path to this node is found.
	--			 Specify NULL find shortest path to all nodes.

    -- Automatically rollback the transaction if something goes wrong.    
    SET XACT_ABORT ON    
    BEGIN TRAN
    
	-- Increase performance and do not intefere with the results.
    SET NOCOUNT ON;

    -- Create a temporary table for storing the estimates as the algorithm runs
	CREATE TABLE #Nodes
	(
		Id decimal(21,0) NOT NULL PRIMARY KEY,    -- The Node Id
		EdgeID	int,
		Name	nvarchar(max),
		Estimate decimal(21,6) NOT NULL,    -- What is the distance to this node, so far?
		Predecessor decimal(21,0) NULL,    -- The node we came from to get to this node with this distance.
		Done bit NOT NULL        -- Are we done with this node yet (is the estimate the final distance)?
	)

    -- Fill the temporary table with initial data
    INSERT INTO #Nodes (Id,EdgeID,Name, Estimate, Predecessor, Done)
    SELECT DISTINCT Node1Id,0,'', 99999999999999.999999, NULL, 0 FROM dbo.Edges
    
     INSERT INTO #Nodes (Id,EdgeID,Name, Estimate, Predecessor, Done)
    SELECT DISTINCT Node2Id,0,'', 99999999999999.999999, NULL, 0 FROM dbo.Edges e where Node2ID not in (select ID from #nodes)
    
    -- Set the estimate for the node we start in to be 0.
    UPDATE #Nodes SET Estimate = 0,Name='Start' WHERE Id = @StartNode
    IF @@rowcount <> 1
    BEGIN
        DROP TABLE #Nodes
        RAISERROR ('Could not set start node', 11, 1) 
        ROLLBACK TRAN        
        RETURN 1
    END

    DECLARE @FromNode decimal(21,0), @CurrentEstimate decimal(21,6)

    -- Run the algorithm until we decide that we are finished
    WHILE 1 = 1
    BEGIN
        -- Reset the variable, so we can detect getting no records in the next step.
        SELECT @FromNode = NULL

        -- Select the Id and current estimate for a node not done, with the lowest estimate.
        SELECT TOP 1 @FromNode = Id, @CurrentEstimate = Estimate
        FROM #Nodes WHERE Done = 0 AND Estimate < 99999999999999.999999
        ORDER BY Estimate
        
        -- Stop if we have no more unvisited, reachable nodes.
        IF @FromNode IS NULL OR @FromNode = @EndNode BREAK

        -- We are now done with this node.
        UPDATE #Nodes SET Done = 1 WHERE Id = @FromNode

        -- Update the estimates to all neighbour node of this one (all the nodes
        -- there are edges to from this node). Only update the estimate if the new
        -- proposal (to go via the current node) is better (lower).
        UPDATE #Nodes
		SET Estimate = @CurrentEstimate + dbo.GetEdgeWeight(e.EdgeID,convert(decimal(21,6),et.TagValue),@TblWightFactors), Predecessor = @FromNode,
		Name = ISNULL(wt.tagvalue,''),
		EdgeID = e.EdgeID
        FROM #Nodes n INNER JOIN dbo.Edges e ON n.Id = e.Node2ID 
        INNER JOIN EdgeTags et on e.edgeid=et.edgeid and et.tagname='Distance'
        inner join ways w on w.wayid=e.wayid left outer join waytags wt on wt.wayid=w.wayid and wt.tagname='name'
        WHERE Done = 0 AND e.Node1ID = @FromNode 
        AND (@CurrentEstimate + dbo.GetEdgeWeight(e.EdgeID,convert(decimal(21,6),et.TagValue),@TblWightFactors)) < n.Estimate
        
    END;
    
    CREATE TABLE #Path
	(
		Id int IDENTITY(1,1),    
		NodeID decimal(21,0),		-- The Node Id
		EdgeID	int,
		Name	nvarchar(max),
		Estimate decimal(21,6) NOT NULL    -- What is the distance to this node, so far?
	)
	insert into #Path (NodeID,EdgeID,Name,Estimate) select Id,edgeid,Name,Estimate from #nodes where Id=@EndNode
	declare @Predecessor NodeId;
	DECLARE @EdgeIDTbl dbo.TblEdges
	select @Predecessor = Predecessor from #nodes where Id=@EndNode
	while(@Predecessor is not null)
	Begin
		insert into #Path (NodeID,EdgeID,Name,Estimate) 
		select Id,EdgeID,Name,Estimate from #nodes where Id=@Predecessor
		
		if(@DrawResult =1)
		Begin
			insert into @EdgeIDTbl 
			select EdgeID from #nodes where Id=@Predecessor 
		End
	
		select @Predecessor = Predecessor from #nodes where Id=@Predecessor
	END
	select * from #Path order by Id desc
	if(@DrawResult =1)
	Begin
		select * from dbo.DrawEdges(@EdgeIDTbl);
	End
    --declare @count int;
    --select @count = count(ID) from #Nodes where Done=1
 --   if @count <100
 --   BEGIN
	--	-- Select the results. We use a recursive common table expression to
	--	-- get the full path from the start node to the current node.
	--	WITH BacktraceCTE(Id, Name, Distance, Path, NamePath)
	--	AS
	--	(
	--		-- Anchor/base member of the recursion, this selects the start node.
	--		SELECT n.Id, n.Name, n.Estimate, CAST(n.Id AS varchar(max)),
	--			CAST(n.Name AS nvarchar(max))
	--		FROM #Nodes n
	--		WHERE n.Id = @StartNode
			
	--		UNION ALL
			
	--		-- Recursive member, select all the nodes which have the previous
	--		-- one as their predecessor. Concat the paths together.
	--		SELECT n.Id, n.Name, n.Estimate,
	--			CAST(cte.Path + ',' + CAST(n.Id as varchar(30)) as varchar(max)),
	--			CAST(cte.NamePath + ',' + n.Name AS nvarchar(max))
	--		FROM #Nodes n JOIN BacktraceCTE cte ON n.Predecessor = cte.Id
	--	)
	--	SELECT Id, Name, Distance, Path, NamePath FROM BacktraceCTE
	--	WHERE Id = @EndNode OR @EndNode IS NULL -- This kind of where clause can potentially produce
	--	ORDER BY Id								-- a bad execution plan, but I use it for simplicity here.
	--END    
    DROP TABLE #Nodes
    DROP Table #Path
    COMMIT TRAN
    RETURN 0
END    

GO
