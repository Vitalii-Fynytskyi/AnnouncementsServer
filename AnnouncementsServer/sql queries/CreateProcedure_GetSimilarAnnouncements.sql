CREATE PROCEDURE GetSimilarAnnouncements
    @Id INT,
    @Title NVARCHAR(4000),
    @Description NVARCHAR(4000),
    @Top INT
AS
BEGIN
    DECLARE @query NVARCHAR(MAX);
    SET @query = N'
        SELECT TOP(' + CAST(@Top AS NVARCHAR(10)) + N') A.* 
        FROM Announcements A 
        JOIN FREETEXTTABLE(Announcements, Title, @Title) AS FtTbl1 ON A.Id = FtTbl1.[Key]
        JOIN FREETEXTTABLE(Announcements, Description, @Description) AS FtTbl2 ON A.Id = FtTbl2.[Key]
        WHERE A.Id <> @Id
        ORDER BY (FtTbl1.RANK + FtTbl2.RANK) DESC';
    EXECUTE sp_executesql @query, N'@Id INT, @Title NVARCHAR(4000), @Description NVARCHAR(4000)', @Id, @Title, @Description;
END