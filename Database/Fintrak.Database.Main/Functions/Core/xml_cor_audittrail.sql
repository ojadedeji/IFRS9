/** Converts XML Action descriptions to Text**/

CREATE function [dbo].[xml_cor_audittrail]
(
	@AuditTrailId int
)
RETURNS varchar(max)
as
Begin 

declare @ActionDesc varchar(max)
declare @Actions int

	declare @Output Table  
	(
		AuditTrailId int,
		Actions    int,
		NodeName Varchar(200) ,
		Value1 Varchar(max) ,
		Value2 Varchar(max)
	)

	DECLARE @XML1 XML
	DECLARE @XML2 XML
	DECLARE @Result   varchar(max)
	Declare @header  varchar(max)
	
	Declare @XML1Var TABLE 
	(
		NodeName Varchar(200),
		Value Varchar(350)
	)

	Declare @XML2Var TABLE 
	(
		NodeName Varchar(200),
		Value Varchar(350)
	)

	Declare @XMLFinal TABLE 
	(
		AuditTrailId int,
		NodeName Varchar(200),
		Value1 Varchar(max),
		Value2 Varchar(max)
	)


	select @header=TableName,@XML1=OldData,@XML2= NewData, @Actions = Actions  from cor_audittrail where  AuditTrailId=@AuditTrailId;

	Insert into @XML1Var(NodeName,Value)
		  select T.N.value('local-name(.)', 'nvarchar(100)') as NodeName,
				 T.N.value('.', 'nvarchar(100)') as Value
		  from @XML1.nodes('/*[local-name(.)=sql:variable("@header")]/*') as T(N)
		
		Insert into @XML2Var(NodeName,Value)
		  select T.N.value('local-name(.)', 'nvarchar(100)') as NodeName,
				 T.N.value('.', 'nvarchar(100)') as Value
		  from @XML2.nodes('/*[local-name(.)=sql:variable("@header")]/*') as T(N)
		
		insert into @XMLFinal
		select @AuditTrailId,coalesce(XML1.NodeName, XML2.NodeName) as NodeName, 
			   XML1.Value as Value1, 
			   XML2.Value as Value2
		from @XML1Var XML1
		  full outer join @XML2Var XML2
			on XML1.NodeName = XML2.NodeName
			where coalesce(XML1.Value, '') <> coalesce(XML2.Value, '') 
		and 
		case when @Actions = 1 then XML2.NodeName else XML1.NodeName end not in ('Active','Deleted','CreatedBy','CreatedOn','UpdatedBy','UpdatedOn')


		if @Actions = 1 
		Begin
			SELECT @ActionDesc =  COALESCE(@ActionDesc+', ' ,'')  + NodeName + ' = ' + Value2 from @XMLFinal
			Set @ActionDesc = 'Created record ' + @ActionDesc
		End
		Else if @Actions = 2
		Begin
			SELECT @ActionDesc =  COALESCE(@ActionDesc+', ' ,'')  + NodeName + ' from ' + Value1 + ' to ' + Value2 from @XMLFinal
			Set @ActionDesc = 'Modify Column(s) ' + @ActionDesc
		End
		Else if @Actions = 3
		Begin
			SELECT @ActionDesc =  COALESCE(@ActionDesc+', ' ,'')  + NodeName + ' = ' + Value1  from @XMLFinal
			Set @ActionDesc = 'Delete record with Column(s) ' + @ActionDesc
		End
		

	return  @ActionDesc	

End
