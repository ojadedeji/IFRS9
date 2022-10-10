/* Scalar function  to get day 1 of month*/

CREATE FUNCTION [dbo].[fn_getfirstday]
(
	@date date
)
RETURNS Date
AS
BEGIN
	Declare @Day1 Date = (SELECT DATEADD(month,1,DATEADD(mm, DATEDIFF(mm, 0, @date) - 1, 0))  ) 
	RETURN @Day1
END
