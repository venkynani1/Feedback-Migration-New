-- =============================================
-- Author:		ANITHA G
-- Create date: 05 SEP 2008
-- Description:	FUNCTION TO CREATE REQUEST NO
-- =============================================
ALTER FUNCTION [dbo].[FN_GET_REQUEST_NO]
(
	-- Add the parameters for the function here
	@LOC VARCHAR(20)
)
RETURNS VARCHAR(50)
AS
BEGIN
	DECLARE @S_REQUEST_NO VARCHAR(50)
	DECLARE @MAX AS VARCHAR(25)
	SELECT @MAX = ISNULL(MAX(N_SERIAL_NO),1)+1 
	FROM dbo.M_VISA_DETAILS 
	IF @LOC IN ('US','UK','CHE','ESP','BEL','NLD','NOR')
	BEGIN
		IF LEN(@MAX)<4
		BEGIN
					IF LEN(@MAX)= 3 
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING(@LOC,0,3) +'0'+@MAX
					END
					ELSE IF LEN(@MAX)= 2
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING(@LOC,0,3) +'00'+@MAX
					END
					ELSE IF LEN(@MAX)= 1
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING(@LOC,0,3) +'000'+@MAX
					END
		END
		ELSE
		BEGIN
					SET @S_REQUEST_NO = SUBSTRING(@LOC,0,3) + @MAX
		END
	END
	ELSE IF @LOC IN ('MEX') 
	BEGIN
		IF LEN(@MAX)<4
		BEGIN
					IF LEN(@MAX)= 3 
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING('MX',0,3) +'0'+@MAX
					END
					ELSE IF LEN(@MAX)= 2
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING('MX',0,3) +'00'+@MAX
					END
					ELSE IF LEN(@MAX)= 1
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING('MX',0,3) +'000'+@MAX
					END
		END
		ELSE
		BEGIN
					SET @S_REQUEST_NO = SUBSTRING('MX',0,3) + @MAX
		END
	END

	ELSE IF @LOC IN ('SWE') 
		BEGIN
		IF LEN(@MAX)<4
		BEGIN
					IF LEN(@MAX)= 3 
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING('SE',0,3) +'0'+@MAX
					END
					ELSE IF LEN(@MAX)= 2
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING('SE',0,3) +'00'+@MAX
					END
					ELSE IF LEN(@MAX)= 1
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING('SE',0,3) +'000'+@MAX
					END
		END
		ELSE
		BEGIN
					SET @S_REQUEST_NO = SUBSTRING('SE',0,3) + @MAX
		END
	END
	---------------------------------Changes done to add SINGAPORE --------------------


	ELSE IF @LOC IN ('SIN') 
		BEGIN
		IF LEN(@MAX)<4
		BEGIN
					IF LEN(@MAX)= 3 
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING('SN',0,3) +'0'+@MAX
					END
					ELSE IF LEN(@MAX)= 2
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING('SN',0,3) +'00'+@MAX
					END
					ELSE IF LEN(@MAX)= 1
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING('SN',0,3) +'000'+@MAX
					END
		END
		ELSE
		BEGIN
					SET @S_REQUEST_NO = SUBSTRING('SN',0,3) + @MAX
		END
	END


	--------------------------------------------------------------------------------------
	ELSE
	BEGIN
	IF LEN(@MAX)<4
		BEGIN
					IF LEN(@MAX)= 3 
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING(@LOC,0,2) +'0'+@MAX
					END
					ELSE IF LEN(@MAX)= 2
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING(@LOC,0,2) +'00'+@MAX
					END
					ELSE IF LEN(@MAX)= 1
					BEGIN
						SET @S_REQUEST_NO = SUBSTRING(@LOC,0,2) +'000'+@MAX
					END
		END
		ELSE
		BEGIN
					SET @S_REQUEST_NO = SUBSTRING(@LOC,0,2) + @MAX
		END
	END	
		RETURN @S_REQUEST_NO	
END