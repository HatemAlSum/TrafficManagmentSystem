USE Maps;
GO
IF EXISTS (SELECT * FROM sys.procedures WHERE name='Breadth_First_Search')
BEGIN
	DROP PROCEDURE Breadth_First_Search;
END
GO
CREATE PROCEDURE dbo.Breadth_First_Search (@StartNode decimal(21,0), @EndNode decimal(21,0) = NULL)-- Breadth_First_Search 96621052
AS
-- Runs breadth-first search from a specific node.
-- @StartNode: If of node to start the search at.
-- @EndNode: Stop the search when node with this id is found. Specify NULL
--			 to traverse the whole graph.
BEGIN
    -- Automatically rollback the transaction if something goes wrong.    
    SET XACT_ABORT ON    
    BEGIN TRAN
    
	-- Increase performance and do not intefere with the results.
    SET NOCOUNT ON;

    -- Create a temporary table for storing the discovered nodes as the algorithm runs
	CREATE TABLE #Discovered
	(
		Id decimal(21,0) NOT NULL PRIMARY KEY,    -- The Node Id
		Name	nvarchar(max),
		Predecessor decimal(21,0) NULL,    -- The node we came from to get to this node.
		OrderDiscovered int -- The order in which the nodes were discovered.
	)

    -- Initially, only the start node is discovered.
    INSERT INTO #Discovered (Id,Name, Predecessor, OrderDiscovered)
    VALUES (@StartNode,'Start', NULL, 0)

	-- Add all nodes that we can get to from the current set of nodes,
	-- that are not already discovered. Run until no more nodes are discovered.
	WHILE @@ROWCOUNT > 0
    BEGIN
		-- If we have found the node we were looking for, abort now.
		IF @EndNode IS NOT NULL
			IF EXISTS (SELECT TOP 1 1 FROM #Discovered WHERE Id = @EndNode)
				BREAK    
  --  SELECT DISTINCT e.Node2ID,MIN(ISNULL(wt.tagvalue,'')), MIN(e.Node1ID), MIN(d.OrderDiscovered) + 1
		--FROM #Discovered d JOIN dbo.Edges e ON d.Id = e.Node1ID
		--inner join ways w on w.wayid=e.wayid left outer join waytags wt on wt.wayid=w.wayid
		--WHERE e.Node2ID NOT IN (SELECT Id From #Discovered) and wt.tagname = 'name'
		--GROUP BY e.Node2ID
		
		-- We need to group by ToNode and select one FromNode since multiple
		-- edges could lead us to new same node, and we only want to insert it once.
		INSERT INTO #Discovered (Id,Name, Predecessor, OrderDiscovered)
		SELECT DISTINCT e.Node2ID,MIN(ISNULL(wt.tagvalue,'')), MIN(e.Node1ID), MIN(d.OrderDiscovered) + 1
		FROM #Discovered d JOIN dbo.Edges e ON d.Id = e.Node1ID
		inner join ways w on w.wayid=e.wayid left outer join waytags wt on wt.wayid=w.wayid
		WHERE e.Node2ID NOT IN (SELECT Id From #Discovered) and wt.tagname = 'name'
		GROUP BY e.Node2ID
    END;
    select *  from #Discovered order by orderdiscovered;
    declare @maxOrder int;
    select @maxOrder = max(orderdiscovered) from #Discovered
    if @maxOrder <100
    BEGIN
		-- Select the results. We use a recursive common table expression to
		-- get the full path from the start node to the current node.
		WITH BacktraceCTE(Id, Name, OrderDiscovered, Path, NamePath)
		AS
		(
			-- Anchor/base member of the recursion, this selects the start node.
			SELECT n.NodeId, d.Name, d.OrderDiscovered, CAST(n.NodeId AS varchar(MAX)),
				CAST(d.Name AS nvarchar(MAX))
			FROM #Discovered d JOIN dbo.Nodes n ON d.Id = n.NodeID
			WHERE d.Id = @StartNode
			
			UNION ALL
			
			-- Recursive member, select all the nodes which have the previous
			-- one as their predecessor. Concat the paths together.
			SELECT n.NodeId, d.Name, d.OrderDiscovered,
				CAST(cte.Path + ',' + CAST(n.NodeId as varchar(30)) as varchar(MAX)),
				CAST(cte.NamePath + ',' + d.Name as nvarchar(MAX))
			FROM #Discovered d JOIN BacktraceCTE cte ON d.Predecessor = cte.Id
			JOIN dbo.Nodes n ON d.Id = n.NodeId
		)
		
		SELECT Id, Name, OrderDiscovered, Path, NamePath FROM BacktraceCTE
		WHERE Id = @EndNode OR @EndNode IS NULL -- This kind of where clause can potentially produce
		ORDER BY OrderDiscovered				-- a bad execution plan, but I use it for simplicity here.
    END
    DROP TABLE #Discovered
    COMMIT TRAN
    RETURN 0
END
